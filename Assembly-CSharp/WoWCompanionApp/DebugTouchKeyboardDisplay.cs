using System;
using AdvancedInputFieldPlugin;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class DebugTouchKeyboardDisplay : MonoBehaviour
	{
		private void Start()
		{
			this.m_textDisplay.text = string.Empty;
			NativeKeyboardManager.AddKeyboardHeightChangedListener(delegate(int h)
			{
				this.height = h;
			});
		}

		private void Update()
		{
			this.m_textDisplay.text = string.Empty;
			Text textDisplay = this.m_textDisplay;
			textDisplay.text = textDisplay.text + "Visible: " + TouchScreenKeyboard.visible;
			Text textDisplay2 = this.m_textDisplay;
			string text = textDisplay2.text;
			textDisplay2.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"Width/Height: [",
				TouchScreenKeyboard.area.width,
				", ",
				TouchScreenKeyboard.area.height,
				"]"
			});
			Text textDisplay3 = this.m_textDisplay;
			text = textDisplay3.text;
			textDisplay3.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"x/y: [",
				TouchScreenKeyboard.area.x,
				", ",
				TouchScreenKeyboard.area.y,
				"]"
			});
			Text textDisplay4 = this.m_textDisplay;
			text = textDisplay4.text;
			textDisplay4.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"xMin/xMax: [",
				TouchScreenKeyboard.area.xMin,
				", ",
				TouchScreenKeyboard.area.xMax,
				"]"
			});
			Text textDisplay5 = this.m_textDisplay;
			text = textDisplay5.text;
			textDisplay5.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"yMin/yMax: [",
				TouchScreenKeyboard.area.yMin,
				", ",
				TouchScreenKeyboard.area.yMax,
				"]"
			});
			Text textDisplay6 = this.m_textDisplay;
			text = textDisplay6.text;
			textDisplay6.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"size: ",
				TouchScreenKeyboard.area.size
			});
			Text textDisplay7 = this.m_textDisplay;
			text = textDisplay7.text;
			textDisplay7.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"position: ",
				TouchScreenKeyboard.area.position
			});
			Text textDisplay8 = this.m_textDisplay;
			text = textDisplay8.text;
			textDisplay8.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"Android Height?: ",
				this.GetAndroidKeyboardHeight()
			});
			Text textDisplay9 = this.m_textDisplay;
			text = textDisplay9.text;
			textDisplay9.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"Window Height: ",
				Screen.height
			});
			Text textDisplay10 = this.m_textDisplay;
			text = textDisplay10.text;
			textDisplay10.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"Safe Area: ",
				Screen.safeArea
			});
			Text textDisplay11 = this.m_textDisplay;
			text = textDisplay11.text;
			textDisplay11.text = string.Concat(new object[]
			{
				text,
				Environment.NewLine,
				"NKM height: ",
				this.height
			});
		}

		private int GetAndroidKeyboardHeight()
		{
			int result;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView", new object[0]);
				using (AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android.graphics.Rect", new object[0]))
				{
					androidJavaObject.Call("getWindowVisibleDisplayFrame", new object[]
					{
						androidJavaObject2
					});
					result = Screen.height - androidJavaObject2.Call<int>("height", new object[0]);
				}
			}
			return result;
		}

		public Text m_textDisplay;

		private int height;
	}
}
