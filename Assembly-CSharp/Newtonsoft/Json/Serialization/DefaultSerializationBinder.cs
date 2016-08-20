using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	public class DefaultSerializationBinder : SerializationBinder
	{
		private static Type GetTypeFromTypeNameKey(DefaultSerializationBinder.TypeNameKey typeNameKey)
		{
			string assemblyName = typeNameKey.AssemblyName;
			string typeName = typeNameKey.TypeName;
			if (assemblyName == null)
			{
				return Type.GetType(typeName);
			}
			Assembly assembly = Assembly.Load(assemblyName);
			if (assembly == null)
			{
				throw new JsonSerializationException("Could not load assembly '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					assemblyName
				}));
			}
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					typeName,
					assembly.FullName
				}));
			}
			return type;
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			return this._typeCache.Get(new DefaultSerializationBinder.TypeNameKey(assemblyName, typeName));
		}

		internal static readonly DefaultSerializationBinder Instance = new DefaultSerializationBinder();

		private readonly ThreadSafeStore<DefaultSerializationBinder.TypeNameKey, Type> _typeCache = new ThreadSafeStore<DefaultSerializationBinder.TypeNameKey, Type>(new Func<DefaultSerializationBinder.TypeNameKey, Type>(DefaultSerializationBinder.GetTypeFromTypeNameKey));

		internal struct TypeNameKey
		{
			public TypeNameKey(string assemblyName, string typeName)
			{
				this.AssemblyName = assemblyName;
				this.TypeName = typeName;
			}

			public override int GetHashCode()
			{
				return ((this.AssemblyName == null) ? 0 : this.AssemblyName.GetHashCode()) ^ ((this.TypeName == null) ? 0 : this.TypeName.GetHashCode());
			}

			public override bool Equals(object obj)
			{
				return obj is DefaultSerializationBinder.TypeNameKey && this.Equals((DefaultSerializationBinder.TypeNameKey)obj);
			}

			public bool Equals(DefaultSerializationBinder.TypeNameKey other)
			{
				return this.AssemblyName == other.AssemblyName && this.TypeName == other.TypeName;
			}

			internal readonly string AssemblyName;

			internal readonly string TypeName;
		}
	}
}
