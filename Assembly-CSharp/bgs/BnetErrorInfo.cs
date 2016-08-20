using System;

namespace bgs
{
	public class BnetErrorInfo
	{
		private BnetErrorInfo()
		{
		}

		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error)
		{
			this.m_feature = feature;
			this.m_featureEvent = featureEvent;
			this.m_error = error;
			this.m_clientContext = 0;
		}

		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error, int context)
		{
			this.m_feature = feature;
			this.m_featureEvent = featureEvent;
			this.m_error = error;
			this.m_clientContext = context;
		}

		public BnetFeature GetFeature()
		{
			return this.m_feature;
		}

		public void SetFeature(BnetFeature feature)
		{
			this.m_feature = feature;
		}

		public BnetFeatureEvent GetFeatureEvent()
		{
			return this.m_featureEvent;
		}

		public void SetFeatureEvent(BnetFeatureEvent featureEvent)
		{
			this.m_featureEvent = featureEvent;
		}

		public BattleNetErrors GetError()
		{
			return this.m_error;
		}

		public void SetError(BattleNetErrors error)
		{
			this.m_error = error;
		}

		public string GetName()
		{
			return this.m_error.ToString();
		}

		public int GetContext()
		{
			return this.m_clientContext;
		}

		public override string ToString()
		{
			string result;
			if (Enum.IsDefined(typeof(BattleNetErrors), this.m_error))
			{
				result = string.Format("[event={0} error={1} {2}]", this.m_featureEvent, (int)this.m_error, this.m_error.ToString());
			}
			else
			{
				result = string.Format("[event={0} code={1} name={2}]", this.m_featureEvent, (int)this.m_error, this.m_error.ToString());
			}
			return result;
		}

		private BnetFeature m_feature;

		private BnetFeatureEvent m_featureEvent;

		private BattleNetErrors m_error;

		private int m_clientContext;
	}
}
