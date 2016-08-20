using System;

namespace Newtonsoft.Json.Bson
{
	internal enum BsonBinaryType : byte
	{
		Binary,
		Function,
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		Data,
		Uuid,
		Md5 = 5,
		UserDefined = 128
	}
}
