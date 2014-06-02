using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThinkingHome.Core.Plugins.Utils
{
	public static class Extensions
	{
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

		public static TResult GetValueOrDefault<T, TResult>(this T obj, Func<T, TResult> func) 
			where T: class
		{
			return obj == null ? default(TResult) : func(obj);
		}
	}
}
