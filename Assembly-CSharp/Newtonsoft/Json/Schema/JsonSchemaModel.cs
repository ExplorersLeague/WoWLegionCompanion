using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	internal class JsonSchemaModel
	{
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.Required = false;
		}

		public bool Required { get; set; }

		public JsonSchemaType Type { get; set; }

		public int? MinimumLength { get; set; }

		public int? MaximumLength { get; set; }

		public double? DivisibleBy { get; set; }

		public double? Minimum { get; set; }

		public double? Maximum { get; set; }

		public bool ExclusiveMinimum { get; set; }

		public bool ExclusiveMaximum { get; set; }

		public int? MinimumItems { get; set; }

		public int? MaximumItems { get; set; }

		public IList<string> Patterns { get; set; }

		public IList<JsonSchemaModel> Items { get; set; }

		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		public JsonSchemaModel AdditionalProperties { get; set; }

		public bool AllowAdditionalProperties { get; set; }

		public IList<JToken> Enum { get; set; }

		public JsonSchemaType Disallow { get; set; }

		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, schema);
			}
			return jsonSchemaModel;
		}

		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			bool required2;
			if (!model.Required)
			{
				bool? required = schema.Required;
				required2 = (required != null && required.Value);
			}
			else
			{
				required2 = true;
			}
			model.Required = required2;
			JsonSchemaType type = model.Type;
			JsonSchemaType? type2 = schema.Type;
			model.Type = (type & ((type2 == null) ? JsonSchemaType.Any : type2.Value));
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			bool exclusiveMinimum2;
			if (!model.ExclusiveMinimum)
			{
				bool? exclusiveMinimum = schema.ExclusiveMinimum;
				exclusiveMinimum2 = (exclusiveMinimum != null && exclusiveMinimum.Value);
			}
			else
			{
				exclusiveMinimum2 = true;
			}
			model.ExclusiveMinimum = exclusiveMinimum2;
			bool exclusiveMaximum2;
			if (!model.ExclusiveMaximum)
			{
				bool? exclusiveMaximum = schema.ExclusiveMaximum;
				exclusiveMaximum2 = (exclusiveMaximum != null && exclusiveMaximum.Value);
			}
			else
			{
				exclusiveMaximum2 = true;
			}
			model.ExclusiveMaximum = exclusiveMaximum2;
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.AllowAdditionalProperties = (model.AllowAdditionalProperties && schema.AllowAdditionalProperties);
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, new JTokenEqualityComparer());
			}
			JsonSchemaType disallow = model.Disallow;
			JsonSchemaType? disallow2 = schema.Disallow;
			model.Disallow = (disallow | ((disallow2 == null) ? JsonSchemaType.None : disallow2.Value));
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
