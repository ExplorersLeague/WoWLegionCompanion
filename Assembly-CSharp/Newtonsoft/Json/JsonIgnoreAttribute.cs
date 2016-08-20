using System;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class JsonIgnoreAttribute : Attribute
	{
	}
}
