using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace DustinHorne.Json.Examples
{
	public class JNPolymorphismSample
	{
		public void Sample()
		{
			List<JNSimpleObjectModel> list = new List<JNSimpleObjectModel>();
			for (int i = 0; i < 3; i++)
			{
				list.Add(this.GetBaseModel());
			}
			for (int j = 0; j < 2; j++)
			{
				list.Add(this.GetSubClassModel());
			}
			for (int k = 0; k < 3; k++)
			{
				list.Add(this.GetBaseModel());
			}
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All
			};
			string value = JsonConvert.SerializeObject(list, Formatting.None, settings);
			List<JNSimpleObjectModel> list2 = JsonConvert.DeserializeObject<List<JNSimpleObjectModel>>(value, settings);
			for (int l = 0; l < list2.Count; l++)
			{
				JNSimpleObjectModel jnsimpleObjectModel = list2[l];
				if (jnsimpleObjectModel.ObjectType == JNObjectType.SubClass)
				{
					Debug.Log((jnsimpleObjectModel as JNSubClassModel).SubClassStringValue);
				}
				else
				{
					Debug.Log(jnsimpleObjectModel.StringValue);
				}
			}
		}

		private JNSimpleObjectModel GetBaseModel()
		{
			return new JNSimpleObjectModel
			{
				IntValue = this._rnd.Next(),
				FloatValue = (float)this._rnd.NextDouble(),
				StringValue = Guid.NewGuid().ToString(),
				IntList = new List<int>
				{
					this._rnd.Next(),
					this._rnd.Next(),
					this._rnd.Next()
				},
				ObjectType = JNObjectType.BaseClass
			};
		}

		private JNSubClassModel GetSubClassModel()
		{
			return new JNSubClassModel
			{
				IntValue = this._rnd.Next(),
				FloatValue = (float)this._rnd.NextDouble(),
				StringValue = Guid.NewGuid().ToString(),
				IntList = new List<int>
				{
					this._rnd.Next(),
					this._rnd.Next(),
					this._rnd.Next()
				},
				ObjectType = JNObjectType.SubClass,
				SubClassStringValue = "This is the subclass value."
			};
		}

		private Random _rnd = new Random();
	}
}
