using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public abstract class MODB<KeyType, RecType> where RecType : MODBRec, new()
	{
		public RecType GetRecord(KeyType key)
		{
			return (!this.m_records.ContainsKey(key)) ? ((RecType)((object)null)) : this.m_records[key];
		}

		public IEnumerable<RecType> GetRecordsWhere(Func<RecType, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public RecType GetRecordFirstOrDefault(Func<RecType, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public abstract bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale);

		protected bool Load(string filePath, AssetBundle bundle)
		{
			if (this.m_records.Count > 0)
			{
				Debug.Log("Already loaded static db " + filePath);
				return false;
			}
			TextAsset textAsset = bundle.LoadAsset<TextAsset>(filePath);
			if (textAsset == null)
			{
				Debug.Log("Unable to load static db " + filePath);
				return false;
			}
			string text = textAsset.ToString();
			int num = 0;
			int num2;
			do
			{
				num2 = text.IndexOf('\n', num);
				if (num2 >= 0)
				{
					string valueLine = text.Substring(num, num2 - num + 1).Trim();
					RecType rec = Activator.CreateInstance<RecType>();
					rec.Deserialize(valueLine);
					this.AddRecord(rec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		protected abstract void AddRecord(RecType rec);

		protected Dictionary<KeyType, RecType> m_records = new Dictionary<KeyType, RecType>();
	}
}
