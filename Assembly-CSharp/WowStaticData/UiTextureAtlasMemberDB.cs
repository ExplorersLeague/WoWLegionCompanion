using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class UiTextureAtlasMemberDB : MODB<int, UiTextureAtlasMemberRec>
	{
		public IEnumerable<UiTextureAtlasMemberRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.UiTextureAtlasID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/UiTextureAtlasMember.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(UiTextureAtlasMemberRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
