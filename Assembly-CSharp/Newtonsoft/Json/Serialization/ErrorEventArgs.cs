using System;

namespace Newtonsoft.Json.Serialization
{
	public class ErrorEventArgs : EventArgs
	{
		public ErrorEventArgs(object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}

		public object CurrentObject { get; private set; }

		public ErrorContext ErrorContext { get; private set; }
	}
}
