using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class CharShipmentDB : MODB<int, CharShipmentRec>
	{
		public IEnumerable<CharShipmentRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.ContainerID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/CharShipment.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(CharShipmentRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
