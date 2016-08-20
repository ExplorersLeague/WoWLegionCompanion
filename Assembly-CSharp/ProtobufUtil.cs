using System;
using System.IO;

public class ProtobufUtil
{
	public static byte[] ToByteArray(IProtoBuf protobuf)
	{
		uint serializedSize = protobuf.GetSerializedSize();
		byte[] array = new byte[serializedSize];
		MemoryStream stream = new MemoryStream(array);
		protobuf.Serialize(stream);
		return array;
	}

	public static T ParseFrom<T>(byte[] bytes, int offset = 0, int length = -1) where T : IProtoBuf, new()
	{
		if (length == -1)
		{
			length = bytes.Length;
		}
		MemoryStream stream = new MemoryStream(bytes, offset, length);
		T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
		result.Deserialize(stream);
		return result;
	}

	public static IProtoBuf ParseFromGeneric<T>(byte[] bytes) where T : IProtoBuf, new()
	{
		MemoryStream stream = new MemoryStream(bytes);
		T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
		t.Deserialize(stream);
		return t;
	}
}
