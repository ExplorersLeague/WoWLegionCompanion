using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using UnityEngine;

namespace DustinHorne.Json.Examples
{
	public class JNBsonSample
	{
		public void Sample()
		{
			JNSimpleObjectModel value = new JNSimpleObjectModel
			{
				IntValue = 5,
				FloatValue = 4.98f,
				StringValue = "Simple Object",
				IntList = new List<int>
				{
					4,
					7,
					25,
					34
				},
				ObjectType = JNObjectType.BaseClass
			};
			byte[] array = new byte[0];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BsonWriter bsonWriter = new BsonWriter(memoryStream))
				{
					JsonSerializer jsonSerializer = new JsonSerializer();
					jsonSerializer.Serialize(bsonWriter, value);
				}
				array = memoryStream.ToArray();
				string text = Convert.ToBase64String(array);
				Debug.Log(text);
			}
			JNSimpleObjectModel jnsimpleObjectModel;
			using (MemoryStream memoryStream2 = new MemoryStream(array))
			{
				using (BsonReader bsonReader = new BsonReader(memoryStream2))
				{
					JsonSerializer jsonSerializer2 = new JsonSerializer();
					jnsimpleObjectModel = jsonSerializer2.Deserialize<JNSimpleObjectModel>(bsonReader);
				}
			}
			if (jnsimpleObjectModel != null)
			{
				Debug.Log(jnsimpleObjectModel.StringValue);
			}
		}
	}
}
