﻿using System;

namespace WowStatConstants
{
	public enum ITEM_BONUS_OUTPUT_FLAGS
	{
		ITEM_BONUS_OUTPUT_FIXED_SCALING_STATS = 1,
		ITEM_BONUS_OUTPUT_MODIFIED_QUALITY,
		ITEM_BONUS_OUTPUT_MODIFIED_ILVL = 4,
		ITEM_BONUS_OUTPUT_IS_CLIENT_PREVIEW = 8,
		ITEM_BONUS_OUTPUT_IS_ITEM_PYRAMID_ILVL = 16,
		ITEM_BONUS_OUTPUT_MODIFIED_BIND_TYPE = 32,
		ITEM_BONUS_OUTPUT_SET_RELIC_POWER_LABEL = 64,
		ITEM_BONUS_OUTPUT_SET_REQUIRED_LEVEL = 128,
		ITEM_BONUS_OUTPUT_ADD_REQUIRED_LEVEL = 256,
		ITEM_BONUS_OUTPUT_SET_AZERITE_UNLOCKSET = 512
	}
}
