using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		public XmlElementWrapper(XmlElement element) : base(element)
		{
			this._element = element;
		}

		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		public string GetPrefixOfNamespace(string namespaceURI)
		{
			return this._element.GetPrefixOfNamespace(namespaceURI);
		}

		private XmlElement _element;
	}
}
