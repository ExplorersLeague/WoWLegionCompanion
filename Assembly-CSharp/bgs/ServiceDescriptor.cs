using System;

namespace bgs
{
	public class ServiceDescriptor
	{
		public ServiceDescriptor(string n)
		{
			this.name = n;
			this.id = 255u;
			this.hash = Compute32.Hash(this.name);
			Console.WriteLine(string.Concat(new object[]
			{
				"service: ",
				n,
				", hash: ",
				this.hash
			}));
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

		public uint Hash
		{
			get
			{
				return this.hash;
			}
		}

		public void RegisterMethodListener(uint method_id, RPCContextDelegate callback)
		{
			if (this.Methods != null && method_id > 0u && (ulong)method_id <= (ulong)((long)this.Methods.Length))
			{
				this.Methods[(int)((UIntPtr)method_id)].RegisterListener(callback);
			}
		}

		public string GetMethodName(uint method_id)
		{
			if (this.Methods != null && method_id > 0u && (ulong)method_id <= (ulong)((long)this.Methods.Length))
			{
				return this.Methods[(int)((UIntPtr)method_id)].Name;
			}
			return string.Empty;
		}

		public int GetMethodCount()
		{
			if (this.Methods == null)
			{
				return 0;
			}
			return this.Methods.Length;
		}

		public MethodDescriptor GetMethodDescriptor(uint method_id)
		{
			if (this.Methods == null)
			{
				return null;
			}
			if ((ulong)method_id >= (ulong)((long)this.Methods.Length))
			{
				return null;
			}
			return this.Methods[(int)((UIntPtr)method_id)];
		}

		public MethodDescriptor GetMethodDescriptorByName(string name)
		{
			if (this.Methods == null)
			{
				return null;
			}
			foreach (MethodDescriptor methodDescriptor in this.Methods)
			{
				if (methodDescriptor != null && methodDescriptor.Name == name)
				{
					return methodDescriptor;
				}
			}
			return null;
		}

		public MethodDescriptor.ParseMethod GetParser(uint method_id)
		{
			if (this.Methods == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, no methods have been set.");
				return null;
			}
			if (method_id <= 0u)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, invalid index={0}/{1}", new object[]
				{
					method_id,
					this.Methods.Length
				});
				return null;
			}
			if ((ulong)method_id >= (ulong)((long)this.Methods.Length))
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, invalid index={0}/{1}", new object[]
				{
					method_id,
					this.Methods.Length
				});
				return null;
			}
			if (this.Methods[(int)((UIntPtr)method_id)].Parser == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, invalid index={0}/{1}", new object[]
				{
					method_id,
					this.Methods.Length
				});
				return null;
			}
			return this.Methods[(int)((UIntPtr)method_id)].Parser;
		}

		public bool HasMethodListener(uint method_id)
		{
			return this.Methods != null && method_id > 0u && (ulong)method_id <= (ulong)((long)this.Methods.Length) && this.Methods[(int)((UIntPtr)method_id)].HasListener();
		}

		public void NotifyMethodListener(RPCContext context)
		{
			if (this.Methods != null && context.Header.MethodId > 0u && (ulong)context.Header.MethodId <= (ulong)((long)this.Methods.Length))
			{
				this.Methods[(int)((UIntPtr)context.Header.MethodId)].NotifyListener(context);
			}
		}

		private const uint INVALID_SERVICE_ID = 255u;

		private string name;

		private uint id;

		private uint hash;

		protected MethodDescriptor[] Methods;
	}
}
