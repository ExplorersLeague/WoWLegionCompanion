using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace JamLib
{
	public static class MessageFactory
	{
		static MessageFactory()
		{
			IEnumerable<Type> source = from t in Assembly.GetExecutingAssembly().GetTypes()
			where t.Namespace != null && t.Namespace.StartsWith("WowJamMessages") && t.IsClass
			select t;
			MessageFactory.s_messageDictionary = source.ToDictionary((Type t) => t.Name);
			MessageFactory.s_jsonSerializerSettings = new JsonSerializerSettings();
			List<JsonConverter> list = new List<JsonConverter>();
			list.Add(new StringEnumConverter());
			list.Add(new JamEmbeddedMessageConverter());
			list.Add(new ByteArrayConverter());
			MessageFactory.s_jsonSerializerSettings.Converters = list;
		}

		public static JsonSerializerSettings SerializerSettings
		{
			get
			{
				return MessageFactory.s_jsonSerializerSettings;
			}
		}

		public static Type GetMessageType(string nameSpace, string nameType)
		{
			return Type.GetType(nameSpace + "." + nameType);
		}

		public static Type GetMessageType(string nameType)
		{
			Type result;
			MessageFactory.s_messageDictionary.TryGetValue(nameType, out result);
			return result;
		}

		public static object Deserialize(string message)
		{
			int num = message.IndexOf(':');
			if (num <= 0)
			{
				return null;
			}
			string text = message.Substring(0, num);
			string text2 = message.Substring(num + 1);
			if (text.Length <= 0 || text2.Length <= 0)
			{
				return null;
			}
			Type messageType = MessageFactory.GetMessageType(text);
			if (messageType == null)
			{
				return null;
			}
			try
			{
				return JsonConvert.DeserializeObject(text2, messageType, MessageFactory.s_jsonSerializerSettings);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return null;
		}

		public static string Serialize(object message)
		{
			Type type = message.GetType();
			return type.Name + ":" + JsonConvert.SerializeObject(message, Formatting.None, MessageFactory.s_jsonSerializerSettings);
		}

		public static MessageDispatch GetDispatcher(Type handlerType)
		{
			IEnumerable<Type> source = MessageFactory.s_messageDictionary.Values.Where(delegate(Type t)
			{
				MethodInfo method = handlerType.GetMethod(t.Name + "Handler");
				return method != null && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == t;
			});
			Dictionary<Type, MethodInfo> dispatchDictionary = source.ToDictionary((Type t) => t, (Type t) => handlerType.GetMethod(t.Name + "Handler"));
			return delegate(object handler, object message)
			{
				MethodInfo methodInfo;
				if (dispatchDictionary.TryGetValue(message.GetType(), out methodInfo))
				{
					methodInfo.Invoke(handler, new object[]
					{
						message
					});
					return true;
				}
				return false;
			};
		}

		private static Dictionary<string, Type> s_messageDictionary;

		private static JsonSerializerSettings s_jsonSerializerSettings;
	}
}
