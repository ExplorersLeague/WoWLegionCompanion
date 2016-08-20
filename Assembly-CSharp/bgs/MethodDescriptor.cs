using System;

namespace bgs
{
	public class MethodDescriptor
	{
		public MethodDescriptor(string n, uint i, MethodDescriptor.ParseMethod parseMethod)
		{
			this.name = n;
			this.id = i;
			this.m_parseMethod = parseMethod;
			if (this.m_parseMethod == null)
			{
				BattleNet.Log.LogError("MethodDescriptor called with a null method type!");
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public uint Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public MethodDescriptor.ParseMethod Parser
		{
			get
			{
				return this.m_parseMethod;
			}
		}

		public void RegisterListener(RPCContextDelegate d)
		{
			this.listener = d;
		}

		public void NotifyListener(RPCContext context)
		{
			if (this.listener != null)
			{
				this.listener(context);
			}
		}

		public bool HasListener()
		{
			return this.listener != null;
		}

		private string name;

		private uint id;

		private RPCContextDelegate listener;

		private MethodDescriptor.ParseMethod m_parseMethod;

		public delegate IProtoBuf ParseMethod(byte[] bs);
	}
}
