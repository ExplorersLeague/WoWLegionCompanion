using System;
using System.ComponentModel;

namespace JamLib
{
	public class NonStringExpandableObjectConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType != typeof(string) && base.CanConvertTo(context, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType != typeof(string) && base.CanConvertFrom(context, sourceType);
		}
	}
}
