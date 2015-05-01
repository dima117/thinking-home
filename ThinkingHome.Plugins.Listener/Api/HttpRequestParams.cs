using System;
using Microsoft.Owin;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class HttpRequestParams
	{
		public readonly IReadableStringCollection urlData;
		public readonly IFormCollection formData;

		public HttpRequestParams(IReadableStringCollection urlData, IFormCollection formData)
		{
			this.urlData = urlData;
			this.formData = formData;
		}

		public string GetString(string name)
		{
			var urlValue = urlData.Get(name);
			var formValue = formData.Get(name);

			if (string.IsNullOrWhiteSpace(urlValue))
			{
				return formValue;
			}

			if (string.IsNullOrWhiteSpace(formValue))
			{
				return urlValue;
			}

			return string.Format("{0},{1}", urlValue, formValue);
		}

		public int? GetInt32(string name)
		{
			var stringValue = GetString(name);
			int result;

			if (int.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		public Guid? GetGuid(string name)
		{
			var stringValue = GetString(name);
			Guid result;

			if (Guid.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		public bool? GetBool(string name)
		{
			var stringValue = GetString(name);
			bool result;

			if (bool.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		public string GetRequiredString(string name)
		{
			var value = GetString(name);

			if (string.IsNullOrEmpty(value))
			{
				string message = string.Format("parameter {0} is required", name);
				throw new NullReferenceException(message);
			}

			return value;
		}

		public int GetRequiredInt32(string name)
		{
			var value = GetInt32(name);

			if (!value.HasValue)
			{
				string message = string.Format("parameter {0} is required", name);
				throw new NullReferenceException(message);
			}

			return value.Value;
		}

		public Guid GetRequiredGuid(string name)
		{
			var value = GetGuid(name);

			if (!value.HasValue)
			{
				string message = string.Format("parameter {0} is required", name);
				throw new NullReferenceException(message);
			}

			return value.Value;
		}

		public bool GetRequiredBool(string name)
		{
			var value = GetBool(name);

			if (!value.HasValue)
			{
				string message = string.Format("parameter {0} is required", name);
				throw new NullReferenceException(message);
			}

			return value.Value;
		}
	}
}
