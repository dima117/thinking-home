using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThinkingHome.Core.Plugins.Utils
{
	public static class Extensions
	{
		#region json

		/// <summary>
		/// Сериализация в JSON
		/// </summary>
		public static string ToJson(this object obj, string defaultValue = "")
		{
			return obj == null ? defaultValue : JsonConvert.SerializeObject(obj);
		}

		/// <summary>
		/// Десериализация
		/// </summary>
		/// <typeparam name="T">Тип</typeparam>
		/// <param name="json">Строка JSON</param>
		public static T FromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		/// <summary>
		/// Десериализация
		/// </summary>
		/// <param name="type">Тип</param>
		/// <param name="json">Строка JSON</param>
		public static object FromJson(Type type, string json)
		{
			return JsonConvert.DeserializeObject(json, type);
		}

		public static dynamic FromJson(string json)
		{
			return JsonConvert.DeserializeObject(json);
		}

		#endregion

		#region parse

		public static int? ParseInt(this string stringValue)
		{
			int result;

			if (int.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		public static Guid? ParseGuid(this string stringValue)
		{
			Guid result;

			if (Guid.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		public static bool? ParseBool(this string stringValue)
		{
			bool result;

			if (bool.TryParse(stringValue, out result))
			{
				return result;
			}

			return null;
		}

		#endregion

		public static TResult GetValueOrDefault<TKey, TResult>(
			this IDictionary<TKey, TResult> dic, TKey key, TResult defaultValue = default (TResult))
		{
			return dic.ContainsKey(key) ? dic[key] : defaultValue;
		}

		public static TResult GetPropertyOrDefault<T, TResult>(this T obj, Func<T, TResult> func)
			where T : class
		{
			return obj == null ? default(TResult) : func(obj);
		}
	}
}
