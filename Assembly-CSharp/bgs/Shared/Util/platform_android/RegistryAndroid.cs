using System;

namespace bgs.Shared.Util.platform_android
{
	public class RegistryAndroid : IRegistry
	{
		public BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true)
		{
			vec = null;
			return BattleNetErrors.ERROR_REGISTRY_NOT_FOUND;
		}

		public BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false)
		{
			s = string.Empty;
			return BattleNetErrors.ERROR_REGISTRY_NOT_FOUND;
		}

		public BattleNetErrors RetrieveInt(string path, string name, out int i)
		{
			i = 0;
			return BattleNetErrors.ERROR_REGISTRY_NOT_FOUND;
		}

		public BattleNetErrors DeleteData(string path, string name)
		{
			return BattleNetErrors.ERROR_REGISTRY_DELETE_ERROR;
		}
	}
}
