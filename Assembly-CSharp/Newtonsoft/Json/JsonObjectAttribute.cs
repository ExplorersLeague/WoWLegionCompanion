using System;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		public JsonObjectAttribute()
		{
		}

		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		public JsonObjectAttribute(string id) : base(id)
		{
		}

		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		private MemberSerialization _memberSerialization;
	}
}
