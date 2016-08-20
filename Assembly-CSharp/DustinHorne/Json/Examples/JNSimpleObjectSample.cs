using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace DustinHorne.Json.Examples
{
	public class JNSimpleObjectSample
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
			string value2 = JsonConvert.SerializeObject(value);
			JNSimpleObjectModel jnsimpleObjectModel = JsonConvert.DeserializeObject<JNSimpleObjectModel>(value2);
			Debug.Log(jnsimpleObjectModel.IntList.Count);
		}
	}
}
