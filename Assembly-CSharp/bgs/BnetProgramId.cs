using System;

namespace bgs
{
	[Serializable]
	public class BnetProgramId : FourCC
	{
		public BnetProgramId()
		{
		}

		public BnetProgramId(uint val) : base(val)
		{
		}

		public BnetProgramId(string stringVal) : base(stringVal)
		{
		}

		public new BnetProgramId Clone()
		{
			return (BnetProgramId)base.MemberwiseClone();
		}

		public static string GetTextureName(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string result = null;
			BnetProgramId.s_textureNameMap.TryGetValue(programId, out result);
			return result;
		}

		public static string GetNameTag(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string result = null;
			BnetProgramId.s_nameStringTagMap.TryGetValue(programId, out result);
			return result;
		}

		public bool IsGame()
		{
			return this != BnetProgramId.PHOENIX && this != BnetProgramId.PHOENIX_OLD;
		}

		public bool IsPhoenix()
		{
			return this == BnetProgramId.PHOENIX || this == BnetProgramId.PHOENIX_OLD;
		}

		public static readonly BnetProgramId HEARTHSTONE = new BnetProgramId("WTCG");

		public static readonly BnetProgramId WOW = new BnetProgramId("WoW");

		public static readonly BnetProgramId DIABLO3 = new BnetProgramId("D3");

		public static readonly BnetProgramId STARCRAFT2 = new BnetProgramId("S2");

		public static readonly BnetProgramId BNET = new BnetProgramId("BN");

		public static readonly BnetProgramId PHOENIX = new BnetProgramId("App");

		public static readonly BnetProgramId PHOENIX_OLD = new BnetProgramId("CLNT");

		public static readonly BnetProgramId HEROES = new BnetProgramId("Hero");

		public static readonly BnetProgramId OVERWATCH = new BnetProgramId("Pro");

		private static readonly Map<BnetProgramId, string> s_textureNameMap = new Map<BnetProgramId, string>
		{
			{
				BnetProgramId.HEARTHSTONE,
				"HS"
			},
			{
				BnetProgramId.WOW,
				"WOW"
			},
			{
				BnetProgramId.DIABLO3,
				"D3"
			},
			{
				BnetProgramId.STARCRAFT2,
				"SC2"
			},
			{
				BnetProgramId.PHOENIX,
				"BN"
			},
			{
				BnetProgramId.PHOENIX_OLD,
				"BN"
			},
			{
				BnetProgramId.HEROES,
				"Heroes"
			},
			{
				BnetProgramId.OVERWATCH,
				"Overwatch"
			}
		};

		private static readonly Map<BnetProgramId, string> s_nameStringTagMap = new Map<BnetProgramId, string>
		{
			{
				BnetProgramId.HEARTHSTONE,
				"GLOBAL_PROGRAMNAME_HEARTHSTONE"
			},
			{
				BnetProgramId.WOW,
				"GLOBAL_PROGRAMNAME_WOW"
			},
			{
				BnetProgramId.DIABLO3,
				"GLOBAL_PROGRAMNAME_DIABLO3"
			},
			{
				BnetProgramId.STARCRAFT2,
				"GLOBAL_PROGRAMNAME_STARCRAFT2"
			},
			{
				BnetProgramId.PHOENIX,
				"GLOBAL_PROGRAMNAME_PHOENIX"
			},
			{
				BnetProgramId.PHOENIX_OLD,
				"GLOBAL_PROGRAMNAME_PHOENIX"
			},
			{
				BnetProgramId.HEROES,
				"GLOBAL_PROGRAMNAME_HEROES"
			},
			{
				BnetProgramId.OVERWATCH,
				"GLOBAL_PROGRAMNAME_OVERWATCH"
			}
		};
	}
}
