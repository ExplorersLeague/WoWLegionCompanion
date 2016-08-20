using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	public class JsonSchemaResolver
	{
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		public virtual JsonSchema GetSchema(string id)
		{
			return this.LoadedSchemas.SingleOrDefault((JsonSchema s) => s.Id == id);
		}
	}
}
