using System;

namespace WowStaticData
{
	public class FactionRec : MODBRec
	{
		public int ID { get; private set; }

		public ushort ParentFactionID { get; private set; }

		public string Name { get; private set; }

		public byte Flags { get; private set; }

		public byte FriendshipRepID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ParentFactionID = Convert.ToUInt16(valueText);
				break;
			case 2:
				this.Name = valueText;
				break;
			case 3:
				this.Flags = Convert.ToByte(valueText);
				break;
			case 4:
				this.FriendshipRepID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
