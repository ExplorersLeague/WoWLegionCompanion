using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[ExecuteInEditMode]
	public class LocalizedText : Text
	{
		protected override void Awake()
		{
			if (!this.overrideFont)
			{
				base.font = FontLoader.LoadFont(this.fontType);
			}
			this.waitingForDB = !StaticDB.StringsAvailable();
			this.LoadStringFromDB();
		}

		public void SetNewStringKey(string key)
		{
			this.baseTag = key;
			this.text = StaticDB.GetString(this.baseTag, this.baseTag);
		}

		private void LoadStringFromDB()
		{
			if (StaticDB.StringsAvailable() && !string.IsNullOrEmpty(this.baseTag))
			{
				this.text = StaticDB.GetString(this.baseTag, this.fallbackString);
				this.waitingForDB = false;
			}
		}

		private void Update()
		{
			if (!this.dynamic && this.waitingForDB)
			{
				this.LoadStringFromDB();
			}
		}

		[Header("Localization keys")]
		public string baseTag;

		public string fallbackString;

		public FontType fontType;

		public bool dynamic;

		public bool overrideFont;

		private bool waitingForDB;
	}
}
