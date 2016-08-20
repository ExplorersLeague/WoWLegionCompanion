using System;

namespace bgs
{
	public class EventListener<Delegate>
	{
		public EventListener()
		{
		}

		public EventListener(Delegate callback, object userData)
		{
			this.m_callback = callback;
			this.m_userData = userData;
		}

		public override bool Equals(object obj)
		{
			EventListener<Delegate> eventListener = obj as EventListener<Delegate>;
			if (eventListener == null)
			{
				return base.Equals(obj);
			}
			return this.m_callback.Equals(eventListener.m_callback) && this.m_userData == eventListener.m_userData;
		}

		public override int GetHashCode()
		{
			int num = 23;
			if (this.m_callback != null)
			{
				num = num * 17 + this.m_callback.GetHashCode();
			}
			if (this.m_userData != null)
			{
				num = num * 17 + this.m_userData.GetHashCode();
			}
			return num;
		}

		public Delegate GetCallback()
		{
			return this.m_callback;
		}

		public void SetCallback(Delegate callback)
		{
			this.m_callback = callback;
		}

		public object GetUserData()
		{
			return this.m_userData;
		}

		public void SetUserData(object userData)
		{
			this.m_userData = userData;
		}

		protected Delegate m_callback;

		protected object m_userData;
	}
}
