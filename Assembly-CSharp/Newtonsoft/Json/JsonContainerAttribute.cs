using System;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		protected JsonContainerAttribute()
		{
		}

		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		public string Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public bool IsReference
		{
			get
			{
				bool? isReference = this._isReference;
				return isReference != null && isReference.Value;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		internal bool? _isReference;
	}
}
