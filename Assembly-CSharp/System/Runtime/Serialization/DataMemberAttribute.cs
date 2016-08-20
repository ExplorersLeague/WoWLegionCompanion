using System;

namespace System.Runtime.Serialization
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class DataMemberAttribute : Attribute
	{
		public bool EmitDefaultValue
		{
			get
			{
				return this.emit_default;
			}
			set
			{
				this.emit_default = value;
			}
		}

		public bool IsRequired
		{
			get
			{
				return this.is_required;
			}
			set
			{
				this.is_required = value;
			}
		}

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

		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		private bool is_required;

		private bool emit_default = true;

		private string name;

		private int order = -1;
	}
}
