using System;
using System.Reflection;

namespace Newtonsoft.Json.Serialization
{
	public class JsonObjectContract : JsonContract
	{
		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
			this.ConstructorParameters = new JsonPropertyCollection(base.UnderlyingType);
		}

		public MemberSerialization MemberSerialization { get; set; }

		public JsonPropertyCollection Properties { get; private set; }

		public JsonPropertyCollection ConstructorParameters { get; private set; }

		public ConstructorInfo OverrideConstructor { get; set; }

		public ConstructorInfo ParametrizedConstructor { get; set; }
	}
}
