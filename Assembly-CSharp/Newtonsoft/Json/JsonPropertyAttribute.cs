using System;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		public JsonPropertyAttribute()
		{
		}

		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				return (nullValueHandling == null) ? NullValueHandling.Include : nullValueHandling.Value;
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				return (defaultValueHandling == null) ? DefaultValueHandling.Include : defaultValueHandling.Value;
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				return (referenceLoopHandling == null) ? ReferenceLoopHandling.Error : referenceLoopHandling.Value;
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				return (objectCreationHandling == null) ? ObjectCreationHandling.Auto : objectCreationHandling.Value;
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				return (typeNameHandling == null) ? TypeNameHandling.None : typeNameHandling.Value;
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

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

		public int Order
		{
			get
			{
				int? order = this._order;
				return (order == null) ? 0 : order.Value;
			}
			set
			{
				this._order = new int?(value);
			}
		}

		public string PropertyName { get; set; }

		public Required Required { get; set; }

		internal NullValueHandling? _nullValueHandling;

		internal DefaultValueHandling? _defaultValueHandling;

		internal ReferenceLoopHandling? _referenceLoopHandling;

		internal ObjectCreationHandling? _objectCreationHandling;

		internal TypeNameHandling? _typeNameHandling;

		internal bool? _isReference;

		internal int? _order;
	}
}
