using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	public class ValidationEventArgs : EventArgs
	{
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		private readonly JsonSchemaException _ex;
	}
}
