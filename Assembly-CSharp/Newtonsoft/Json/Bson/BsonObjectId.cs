using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	public class BsonObjectId
	{
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new Exception("An ObjectId must be 12 bytes");
			}
			this.Value = value;
		}

		public byte[] Value { get; private set; }
	}
}
