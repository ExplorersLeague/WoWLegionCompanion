using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class TreasureDB : MODB<int, TreasureRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/Treasure.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(TreasureRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}

		public bool IsCoverItem(int itemID)
		{
			if (this.coverItemIDs == null)
			{
				this.coverItemIDs = new HashSet<int>();
				foreach (KeyValuePair<int, TreasureRec> keyValuePair in this.m_records)
				{
					this.coverItemIDs.Add(keyValuePair.Value.CoverItemID);
				}
			}
			return this.coverItemIDs.Contains(itemID);
		}

		private HashSet<int> coverItemIDs;
	}
}
