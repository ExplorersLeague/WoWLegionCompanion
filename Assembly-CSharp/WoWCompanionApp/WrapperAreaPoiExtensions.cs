using System;

namespace WoWCompanionApp
{
	public static class WrapperAreaPoiExtensions
	{
		public static bool IsHordeAssault(this WrapperAreaPoi areaPoi)
		{
			return areaPoi.AreaPoiID == 5896 || areaPoi.AreaPoiID == 5966 || areaPoi.AreaPoiID == 5964;
		}

		public static bool IsAllianceAssault(this WrapperAreaPoi areaPoi)
		{
			return areaPoi.AreaPoiID == 5969 || areaPoi.AreaPoiID == 5973 || areaPoi.AreaPoiID == 5970;
		}
	}
}
