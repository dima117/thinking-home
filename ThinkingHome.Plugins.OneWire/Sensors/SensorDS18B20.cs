using DalSemi.OneWire.Adapter;
using DalSemi.Utils;
using System;
using System.Threading;
using ThinkingHome.Plugins.OneWire.Attributes;

namespace ThinkingHome.Plugins.OneWire.Sensors
{
    [SensorType(0x28)]
    public class SensorDS18B20 : TemperatureSensorBase
    {
        #region Const
        /** DS18B20 12-bit resolution constant for CONFIG byte  */
        public const byte RESOLUTION_12_BIT = (byte)0x7F;

        /** DS18B20 11-bit resolution constant for CONFIG byte  */
        public const byte RESOLUTION_11_BIT = (byte)0x5F;

        /** DS18B20 10-bit resolution constant for CONFIG byte  */
        public const byte RESOLUTION_10_BIT = (byte)0x3F;

        /** DS18B20 9-bit resolution constant for CONFIG byte   */
        public const byte RESOLUTION_9_BIT = (byte)0x1F;
        #endregion

        #region Constructors
        public SensorDS18B20(byte[] address, OneWireAdapter adapter)
            :base(address, adapter)
        {
        }
        #endregion

        #region Override
        public override string DeviceName
		{
			get { return "DS18B20 temperature sensor"; }
		}
        public override byte DeviceType
        {
            get
            {
                return 0x28;
            }
        }
        #endregion

        #region Methods sensor
        public double GetTemperature(byte[] state)
        {
            // Take these three steps:
            // 1)  Make an 11-bit integer number out of MSB and LSB of the first 2 bytes from scratchpad
            // 2)  Divide final number by 16 to retrieve the floating point number.
            // 3)  Afterwards, test for the following temperatures:
            //     0x07D0 = 125.0C
            //     0x0550 = 85.0C
            //     0x0191 = 25.0625C
            //     0x00A2 = 10.125C
            //     0x0008 = 0.5C
            //     0x0000 = 0.0C
            //     0xFFF8 = -0.5C
            //     0xFF5E = -10.125C
            //     0xFE6F = -25.0625C
            //     0xFC90 = -55.0C
            double theTemperature = (double)0.0;
            int inttemperature = state[1];   // inttemperature is automatically sign extended here.

            inttemperature = (inttemperature << 8) | (state[0] & 0xFF);   // this converts 2 bytes into integer
            theTemperature = (double)((double)inttemperature / (double)16);   // converts integer to a double

            return (theTemperature);
        }

        public byte[] ReadScratchpad()
        {
            byte[] result_block;

            // select the device
            if (Adapter.SelectDevice(Address, 0))
            {
                // create a block to send that reads the scratchpad
                byte[] send_block = new byte[10];

                // read scratchpad command
                send_block[0] = (byte)0xBE;

                // now add the read bytes for data bytes and crc8
                for (int i = 1; i < 10; i++)
                    send_block[i] = (byte)0xFF;

                // send the block
                //Adapter.DataBlock(send_block, 0, send_block.Length);
                Adapter.DataBlock(send_block);

                // now, send_block contains the 9-byte Scratchpad plus READ_SCRATCHPAD_COMMAND byte
                // convert the block to a 9-byte array representing Scratchpad (get rid of first byte)
                result_block = new byte[9];

                for (int i = 0; i < 9; i++)
                {
                    result_block[i] = send_block[i + 1];
                }

                // see if CRC8 is correct
                if (CRC8.Compute(send_block, 1, 9) == 0)
                    return (result_block);
            }

            return null;
        }

        public void WriteDevice(byte[] state)
        {
            byte[] temp = new byte[3];

            temp[0] = state[2];
            temp[1] = state[3];
            temp[2] = state[4];

            // Write it to the Scratchpad.
            WriteScratchpad(temp);

            // Place in memory.
            CopyScratchpad();
        }

        public void CopyScratchpad()
        {

            // first, let's read the scratchpad to compare later.
            byte[] readfirstbuffer;

            readfirstbuffer = ReadScratchpad();

            // second, let's copy the scratchpad.
            if (Adapter.SelectDevice(Address, 0))
            {

                // apply the power delivery
                //_adapter.SetPowerDuration(OWPowerTime.DELIVERY_INFINITE);
                //_adapter.StartPowerDelivery(OWPowerStart.CONDITION_AFTER_BYTE);
                Adapter.SetPowerDuration((int)OWPowerTime.DELIVERY_INFINITE);
                Adapter.StartPowerDelivery((int)OWPowerStart.CONDITION_AFTER_BYTE);

                // send the convert temperature command
                Adapter.PutByte(0x48);

                // sleep for 10 milliseconds to allow copy to take place.
                Thread.Sleep(10);

                // Turn power back to normal.
                Adapter.SetPowerNormal();
            }
            else
            {
                // device must not have been present
                throw new Exception("OneWireContainer28-Device not found on 1-Wire Network");
            }

            // third, let's read the scratchpad again with the recallE2 command and compare.
            byte[] readlastbuffer;

            readlastbuffer = RecallE2();

            if ((readfirstbuffer[2] != readlastbuffer[2])
                    || (readfirstbuffer[3] != readlastbuffer[3])
                    || (readfirstbuffer[4] != readlastbuffer[4]))
            {

                // copying to scratchpad failed
                throw new Exception("OneWireContainer28-Error copying scratchpad to E2 memory.");
            }
        }

        public byte[] RecallE2()
        {
            byte[] scratchBuff;

            // select the device
            if (Adapter.SelectDevice(Address, 0))
            {

                // send the Recall E-squared memory command
                Adapter.PutByte(0xB8);

                // read scratchpad
                scratchBuff = ReadScratchpad();

                return scratchBuff;
            }

            // device must not have been present
            throw new Exception("OneWireContainer28-Device not found on 1-Wire Network");
        }

        public void WriteScratchpad(byte[] data)
        {

            // setup buffer to write to scratchpad
            byte[] writeBuffer = new byte[4];

            writeBuffer[0] = 0x4E;
            writeBuffer[1] = data[0];
            writeBuffer[2] = data[1];
            writeBuffer[3] = data[2];

            // send command block to device
            if (Adapter.SelectDevice(Address, 0))
            {
                //_adapter.DataBlock(writeBuffer, 0, writeBuffer.Length);
                Adapter.DataBlock(writeBuffer);

                // double check by reading scratchpad
                byte[] readBuffer;

                readBuffer = ReadScratchpad();

                if ((readBuffer[2] != data[0]) || (readBuffer[3] != data[1])
                        || (readBuffer[4] != data[2]))
                {
                    // writing to scratchpad failed
                    throw new Exception("OneWireContainer28-Error writing to scratchpad");
                }
            }
        }

        public void DoTemperatureConvert(byte[] state)
        {
            int msDelay = 750;   // in milliseconds

            // select the device
            if (Adapter.SelectDevice(Address, 0))
            {

                // Setup Power Delivery
                Adapter.SetPowerDuration((int)OWPowerTime.DELIVERY_INFINITE);
                Adapter.StartPowerDelivery((int)OWPowerStart.CONDITION_AFTER_BYTE);
                
                // send the convert temperature command
                Adapter.PutByte(0x44);

                // calculate duration of delay according to resolution desired
                switch (state[4])
                {

                    case RESOLUTION_9_BIT:
                        msDelay = 94;
                        break;
                    case RESOLUTION_10_BIT:
                        msDelay = 188;
                        break;
                    case RESOLUTION_11_BIT:
                        msDelay = 375;
                        break;
                    case RESOLUTION_12_BIT:
                        msDelay = 750;
                        break;
                    default:
                        msDelay = 750;
                        break;
                }   // switch

                // delay for specified amount of time
                Thread.Sleep(msDelay);

                // Turn power back to normal.
                Adapter.SetPowerNormal();

                // check to see if the temperature conversion is over
                if (Adapter.GetByte() != 0xFF)
                    throw new Exception("OneWireContainer28-temperature conversion not complete");
            }
            else
            {

                // device must not have been present
                throw new Exception("OneWireContainer28-device not present");
            }
        }

        public byte[] ReadDevice()
        {
            byte[] data;

            data = RecallE2();

            return data;
        }
        #endregion

        #region TemperatureSensorBase
        public override double GetTemperature()
        {
            double lastTemperature;
            
            byte[] state = ReadDevice();

            DoTemperatureConvert(state);

            state = ReadDevice();

            lastTemperature = GetTemperature(state);

            return lastTemperature;
        }
        #endregion
    }
}