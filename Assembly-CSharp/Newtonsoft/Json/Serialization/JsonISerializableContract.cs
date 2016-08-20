using System;

namespace Newtonsoft.Json.Serialization
{
	public class JsonISerializableContract : JsonContract
	{
		public JsonISerializableContract(Type underlyingType) : base(underlyingType)
		{
		}

		public ObjectConstructor<object> ISerializableCreator { get; set; }
	}
}
