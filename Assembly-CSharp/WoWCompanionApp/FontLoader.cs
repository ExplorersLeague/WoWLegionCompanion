using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class FontLoader : Singleton<FontLoader>
	{
		public static Font LoadFont(FontType fontType)
		{
			if (fontType != FontType.Fancy)
			{
				if (fontType != FontType.Standard)
				{
				}
				return FontLoader.LoadStandardFont();
			}
			return FontLoader.LoadFancyFont();
		}

		public static Font LoadFancyFont()
		{
			string locale = Main.instance.GetLocale();
			if (locale != null)
			{
				if (locale == "koKR")
				{
					return Singleton<FontLoader>.Instance.fancy_ko_KR;
				}
				if (locale == "zhCN")
				{
					return Singleton<FontLoader>.Instance.fancy_zh_CN;
				}
				if (locale == "zhTW")
				{
					return Singleton<FontLoader>.Instance.fancy_zh_TW;
				}
				if (locale == "ruRU")
				{
					return Singleton<FontLoader>.Instance.fancy_ru_RU;
				}
			}
			return Singleton<FontLoader>.Instance.fancy_Default;
		}

		public static Font LoadStandardFont()
		{
			string locale = Main.instance.GetLocale();
			if (locale != null)
			{
				if (locale == "koKR")
				{
					return Singleton<FontLoader>.Instance.standard_ko_KR;
				}
				if (locale == "zhCN")
				{
					return Singleton<FontLoader>.Instance.standard_zh_CN;
				}
				if (locale == "zhTW")
				{
					return Singleton<FontLoader>.Instance.standard_zh_TW;
				}
				if (locale == "ruRU")
				{
					return Singleton<FontLoader>.Instance.standard_ru_RU;
				}
			}
			return Singleton<FontLoader>.Instance.standard_Default;
		}

		[Header("Standard")]
		public Font standard_Default;

		public Font standard_zh_CN;

		public Font standard_zh_TW;

		public Font standard_ko_KR;

		public Font standard_ru_RU;

		[Header("Fancy")]
		public Font fancy_Default;

		public Font fancy_zh_CN;

		public Font fancy_zh_TW;

		public Font fancy_ko_KR;

		public Font fancy_ru_RU;
	}
}
