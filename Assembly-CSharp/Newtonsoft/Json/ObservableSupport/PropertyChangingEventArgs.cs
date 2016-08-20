using System;

namespace Newtonsoft.Json.ObservableSupport
{
	public class PropertyChangingEventArgs : EventArgs
	{
		public PropertyChangingEventArgs(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		public virtual string PropertyName { get; set; }
	}
}
