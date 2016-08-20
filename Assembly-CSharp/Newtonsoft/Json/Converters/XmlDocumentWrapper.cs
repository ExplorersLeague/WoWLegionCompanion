using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		public XmlDocumentWrapper(XmlDocument document) : base(document)
		{
			this._document = document;
		}

		public IXmlNode CreateComment(string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		public IXmlNode CreateTextNode(string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		public IXmlNode CreateCDataSection(string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		public IXmlNode CreateWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlNodeWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		public IXmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceURI));
		}

		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		public IXmlNode CreateAttribute(string qualifiedName, string namespaceURI, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceURI))
			{
				Value = value
			};
		}

		public IXmlElement DocumentElement
		{
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		private XmlDocument _document;
	}
}
