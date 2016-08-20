using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	public class JsonSerializerSettings
	{
		public JsonSerializerSettings()
		{
			this.ReferenceLoopHandling = ReferenceLoopHandling.Error;
			this.MissingMemberHandling = MissingMemberHandling.Ignore;
			this.ObjectCreationHandling = ObjectCreationHandling.Auto;
			this.NullValueHandling = NullValueHandling.Include;
			this.DefaultValueHandling = DefaultValueHandling.Include;
			this.PreserveReferencesHandling = PreserveReferencesHandling.None;
			this.TypeNameHandling = TypeNameHandling.None;
			this.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
			this.Context = JsonSerializerSettings.DefaultContext;
			this.Converters = new List<JsonConverter>();
		}

		public ReferenceLoopHandling ReferenceLoopHandling { get; set; }

		public MissingMemberHandling MissingMemberHandling { get; set; }

		public ObjectCreationHandling ObjectCreationHandling { get; set; }

		public NullValueHandling NullValueHandling { get; set; }

		public DefaultValueHandling DefaultValueHandling { get; set; }

		public IList<JsonConverter> Converters { get; set; }

		public PreserveReferencesHandling PreserveReferencesHandling { get; set; }

		public TypeNameHandling TypeNameHandling { get; set; }

		public FormatterAssemblyStyle TypeNameAssemblyFormat { get; set; }

		public ConstructorHandling ConstructorHandling { get; set; }

		public IContractResolver ContractResolver { get; set; }

		public IReferenceResolver ReferenceResolver { get; set; }

		public SerializationBinder Binder { get; set; }

		public EventHandler<ErrorEventArgs> Error { get; set; }

		public StreamingContext Context { get; set; }

		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		internal const FormatterAssemblyStyle DefaultTypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;

		internal static readonly StreamingContext DefaultContext = default(StreamingContext);
	}
}
