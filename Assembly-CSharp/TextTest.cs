using System;
using UnityEngine;

public class TextTest : MonoBehaviour
{
	private void Start()
	{
		this._testNum = 0;
		this._statusText = this.TextObject.GetComponent<TextMesh>();
		this._statusText.text = "-- SERIALIZATION TESTS -- \r\n Tests are run with \r\n a three second delay \r\n Starting in 10 seconds.";
		this._tester = new JsonTestScript(this._statusText);
		this._refTime = DateTime.Now.AddSeconds(7.0);
	}

	private void Update()
	{
		if (!this._complete && (DateTime.Now - this._refTime).TotalSeconds >= 3.0)
		{
			this._testNum++;
			this.RunNextTest();
			this._refTime = DateTime.Now;
		}
	}

	private void RunNextTest()
	{
		switch (this._testNum)
		{
		case 1:
			this._tester.SerializeVector3();
			break;
		case 2:
			this._tester.GenericListSerialization();
			break;
		case 3:
			this._tester.PolymorphicSerialization();
			break;
		case 4:
			this._tester.DictionarySerialization();
			break;
		case 5:
			this._tester.DictionaryObjectValueSerialization();
			break;
		default:
			this._complete = true;
			this._statusText.text = "Tests Complete\r\nSee Console for Log";
			break;
		}
	}

	public GameObject TextObject;

	private TextMesh _statusText;

	private DateTime _refTime;

	private int _testNum = 5;

	private JsonTestScript _tester;

	private bool _complete;
}
