using System;

namespace WowStaticData
{
	public class UiTextureAtlasMemberRec
	{
		public int ID { get; private set; }

		public string CommittedName { get; private set; }

		public uint UiTextureAtlasID { get; private set; }

		public uint CommittedLeft { get; private set; }

		public uint CommittedRight { get; private set; }

		public uint CommittedTop { get; private set; }

		public uint CommittedBottom { get; private set; }

		public uint CommittedFlags { get; private set; }

		public uint Priority { get; private set; }

		public void Deserialize(string valueLine)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = valueLine.IndexOf('\t', num);
				if (num3 >= 0)
				{
					string valueText = valueLine.Substring(num, num3 - num).Trim();
					this.DeserializeIndex(num2, valueText);
					num2++;
				}
				num = num3 + 1;
			}
			while (num3 > 0);
		}

		private void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.CommittedName = valueText;
				break;
			case 2:
				this.UiTextureAtlasID = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.CommittedLeft = Convert.ToUInt32(valueText);
				break;
			case 4:
				this.CommittedRight = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.CommittedTop = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.CommittedBottom = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.CommittedFlags = Convert.ToUInt32(valueText);
				break;
			case 8:
				this.Priority = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
