using System;
using System.Collections.Generic;
using Assets.DustinHorne.JsonDotNetUnity.TestCases.TestModels;
using UnityEngine;

namespace Assets.DustinHorne.JsonDotNetUnity.TestCases
{
	public static class TestCaseUtils
	{
		public static SampleBase GetSampleBase()
		{
			SampleBase sampleBase = new SampleBase();
			sampleBase.TextValue = Guid.NewGuid().ToString();
			sampleBase.NumberValue = TestCaseUtils._rnd.Next();
			int num = TestCaseUtils._rnd.Next();
			int num2 = TestCaseUtils._rnd.Next();
			int num3 = TestCaseUtils._rnd.Next();
			sampleBase.VectorValue = new Vector3((float)num, (float)num2, (float)num3);
			return sampleBase;
		}

		public static SampleChild GetSampleChid()
		{
			SampleChild sampleChild = new SampleChild();
			sampleChild.TextValue = Guid.NewGuid().ToString();
			sampleChild.NumberValue = TestCaseUtils._rnd.Next();
			int num = TestCaseUtils._rnd.Next();
			int num2 = TestCaseUtils._rnd.Next();
			int num3 = TestCaseUtils._rnd.Next();
			sampleChild.VectorValue = new Vector3((float)num, (float)num2, (float)num3);
			sampleChild.ObjectDictionary = new Dictionary<int, SimpleClassObject>();
			for (int i = 0; i < 4; i++)
			{
				SimpleClassObject simpleClassObject = TestCaseUtils.GetSimpleClassObject();
				sampleChild.ObjectDictionary.Add(i, simpleClassObject);
			}
			sampleChild.ObjectList = new List<SimpleClassObject>();
			for (int j = 0; j < 4; j++)
			{
				SimpleClassObject simpleClassObject2 = TestCaseUtils.GetSimpleClassObject();
				sampleChild.ObjectList.Add(simpleClassObject2);
			}
			return sampleChild;
		}

		public static SimpleClassObject GetSimpleClassObject()
		{
			SimpleClassObject simpleClassObject = new SimpleClassObject();
			simpleClassObject.TextValue = Guid.NewGuid().ToString();
			simpleClassObject.NumberValue = TestCaseUtils._rnd.Next();
			int num = TestCaseUtils._rnd.Next();
			int num2 = TestCaseUtils._rnd.Next();
			int num3 = TestCaseUtils._rnd.Next();
			simpleClassObject.VectorValue = new Vector3((float)num, (float)num2, (float)num3);
			return simpleClassObject;
		}

		private static Random _rnd = new Random();
	}
}
