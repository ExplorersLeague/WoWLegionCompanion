using System;
using System.Collections.Generic;
using UnityEngine;
using WowStaticData;

namespace WoWCompanionApp
{
	public class TextureAtlas
	{
		public static TextureAtlas instance
		{
			get
			{
				if (TextureAtlas.s_instance == null)
				{
					TextureAtlas.s_instance = new TextureAtlas();
					TextureAtlas.s_instance.InitAtlas();
				}
				return TextureAtlas.s_instance;
			}
		}

		public static Sprite GetSprite(int memberID)
		{
			return TextureAtlas.instance.GetAtlasSprite(memberID);
		}

		private void InitAtlas()
		{
			if (TextureAtlas.s_initialized)
			{
				Debug.Log("WARNING! ATTEMPTED TO INIT TEXTURE ATLAS, BUT IT IS ALREADY INITIALIZED!! IGNORING");
				return;
			}
			this.m_atlas = new Dictionary<int, Dictionary<int, Sprite>>();
			TextAsset textAsset = Resources.Load("TextureAtlas/AtlasDirectory") as TextAsset;
			string text = textAsset.ToString();
			int num = 0;
			int num2;
			string text2;
			for (;;)
			{
				num2 = text.IndexOf('\n', num);
				if (num2 >= 0)
				{
					text2 = text.Substring(num, num2 - num + 1).Trim();
					string value = text2.Substring(text2.Length - 10);
					int key = Convert.ToInt32(value);
					Sprite[] array = Resources.LoadAll<Sprite>("TextureAtlas/" + text2);
					if (array.Length <= 0)
					{
						break;
					}
					Dictionary<int, Sprite> dictionary = new Dictionary<int, Sprite>();
					foreach (Sprite sprite in array)
					{
						int key2 = Convert.ToInt32(sprite.name);
						dictionary.Add(key2, sprite);
					}
					this.m_atlas.Add(key, dictionary);
					num = num2 + 1;
				}
				if (num2 <= 0)
				{
					goto Block_5;
				}
			}
			Debug.Log("Found no sprites in atlas " + text2);
			num = num2 + 1;
			throw new Exception("Atlas in Resources folder is missing or has no sprites: " + text2);
			Block_5:
			this.m_atlasMemberIDCache = new Dictionary<string, int>();
			TextureAtlas.s_initialized = true;
		}

		private int GetOverrideMemberID(int memberID)
		{
			switch (memberID)
			{
			case 6128:
				memberID = 6100;
				break;
			case 6129:
				memberID = 6127;
				break;
			case 6130:
				memberID = 6126;
				break;
			case 6131:
				memberID = 6095;
				break;
			case 6132:
				memberID = 6097;
				break;
			}
			return memberID;
		}

		public Sprite GetAtlasSprite(int memberID)
		{
			memberID = this.GetOverrideMemberID(memberID);
			UiTextureAtlasMemberRec record = StaticDB.uiTextureAtlasMemberDB.GetRecord(memberID);
			if (record == null)
			{
				Debug.LogWarning("GetAtlasSprite(): Unknown member ID " + memberID);
				return null;
			}
			Dictionary<int, Sprite> dictionary;
			this.m_atlas.TryGetValue((int)record.UiTextureAtlasID, out dictionary);
			if (dictionary == null)
			{
				Debug.LogWarning("GetAtlasSprite(): Unknown atlas ID " + record.UiTextureAtlasID);
				return null;
			}
			Sprite result;
			dictionary.TryGetValue(record.ID, out result);
			return result;
		}

		public static int GetUITextureAtlasMemberID(string atlasMemberName)
		{
			int num = 0;
			if (atlasMemberName == null)
			{
				return num;
			}
			TextureAtlas.instance.m_atlasMemberIDCache.TryGetValue(atlasMemberName, out num);
			if (num > 0)
			{
				return num;
			}
			UiTextureAtlasMemberRec recordFirstOrDefault = StaticDB.uiTextureAtlasMemberDB.GetRecordFirstOrDefault((UiTextureAtlasMemberRec rec) => rec.CommittedName != null && rec.CommittedName.Equals(atlasMemberName, StringComparison.OrdinalIgnoreCase));
			if (recordFirstOrDefault != null)
			{
				num = recordFirstOrDefault.ID;
				TextureAtlas.instance.m_atlasMemberIDCache.Add(atlasMemberName, num);
			}
			return num;
		}

		private static TextureAtlas s_instance;

		private static bool s_initialized;

		private Dictionary<int, Dictionary<int, Sprite>> m_atlas;

		private Dictionary<string, int> m_atlasMemberIDCache;
	}
}
