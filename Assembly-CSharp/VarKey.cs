using System;

public class VarKey
{
	public VarKey(string key)
	{
		this.m_key = key;
	}

	public VarKey(string key, string subKey)
	{
		this.m_key = key + "." + subKey;
	}

	public VarKey Key(string subKey)
	{
		return new VarKey(this.m_key, subKey);
	}

	public string GetStr(string def)
	{
		if (VarsInternal.Get().Contains(this.m_key))
		{
			return VarsInternal.Get().Value(this.m_key);
		}
		return def;
	}

	public int GetInt(int def)
	{
		if (VarsInternal.Get().Contains(this.m_key))
		{
			string str = VarsInternal.Get().Value(this.m_key);
			return GeneralUtils.ForceInt(str);
		}
		return def;
	}

	public float GetFloat(float def)
	{
		if (VarsInternal.Get().Contains(this.m_key))
		{
			string str = VarsInternal.Get().Value(this.m_key);
			return GeneralUtils.ForceFloat(str);
		}
		return def;
	}

	public bool GetBool(bool def)
	{
		if (VarsInternal.Get().Contains(this.m_key))
		{
			string strVal = VarsInternal.Get().Value(this.m_key);
			return GeneralUtils.ForceBool(strVal);
		}
		return def;
	}

	private string m_key;
}
