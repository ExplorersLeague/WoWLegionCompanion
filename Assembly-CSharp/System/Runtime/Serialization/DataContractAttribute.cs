using System;

namespace System.Runtime.Serialization
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public sealed class DataContractAttribute : Attribute
	{
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		public bool IsReference { get; set; }

		private string name;

		private string ns;
	}
}
