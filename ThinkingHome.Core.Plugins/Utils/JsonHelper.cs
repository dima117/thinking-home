using System;
using Newtonsoft.Json;

namespace ThinkingHome.Core.Plugins.Utils
{
	/// <summary>
	/// Преобразование объектов в JSON и обратно
	/// </summary>
	public static class JsonHelper
	{
		/// <summary>
		/// Сериализация в JSON
		/// </summary>
		/// <param name="obj">Сериализуемый объект</param>
		public static string ToJson(this object obj)
		{
			return obj == null ? string.Empty : JsonConvert.SerializeObject(obj);
		}

		/// <summary>
		/// Десериализация
		/// </summary>
		/// <typeparam name="T">Тип</typeparam>
		/// <param name="json">Строка JSON</param>
		public static T Get<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		/// <summary>
		/// Десериализация
		/// </summary>
		/// <param name="type">Тип</param>
		/// <param name="json">Строка JSON</param>
		public static object Get(Type type, string json)
		{
			return JsonConvert.DeserializeObject(json, type);
		}
	}
}