using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	public class XmlNodeConverter : JsonConverter
	{
		public string DeserializeRootElementName { get; set; }

		public bool WriteArrayAttribute { get; set; }

		public bool OmitRootObject { get; set; }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IXmlNode node = this.WrapXml(value);
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			this.PushParentNamespaces(node, manager);
			if (!this.OmitRootObject)
			{
				writer.WriteStartObject();
			}
			this.SerializeNode(writer, node, manager, !this.OmitRootObject);
			if (!this.OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		private IXmlNode WrapXml(object value)
		{
			throw new ArgumentException("Value must be an XML object.", "value");
		}

		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list != null)
			{
				list.Reverse();
				foreach (IXmlNode xmlNode2 in list)
				{
					manager.PushScope();
					foreach (IXmlNode xmlNode3 in xmlNode2.Attributes)
					{
						if (xmlNode3.NamespaceURI == "http://www.w3.org/2000/xmlns/" && xmlNode3.LocalName != "xmlns")
						{
							manager.AddNamespace(xmlNode3.LocalName, xmlNode3.Value);
						}
					}
				}
			}
		}

		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = (node.NamespaceURI != null && (!(node.LocalName == "xmlns") || !(node.NamespaceURI == "http://www.w3.org/2000/xmlns/"))) ? manager.LookupPrefix(node.NamespaceURI) : null;
			if (!string.IsNullOrEmpty(text))
			{
				return text + ":" + node.LocalName;
			}
			return node.LocalName;
		}

		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				return this.ResolveFullName(node, manager);
			case XmlNodeType.Attribute:
				if (node.NamespaceURI == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + this.ResolveFullName(node, manager);
			case XmlNodeType.Text:
				return "#text";
			case XmlNodeType.CDATA:
				return "#cdata-section";
			case XmlNodeType.ProcessingInstruction:
				return "?" + this.ResolveFullName(node, manager);
			case XmlNodeType.Comment:
				return "#comment";
			case XmlNodeType.Whitespace:
				return "#whitespace";
			case XmlNodeType.SignificantWhitespace:
				return "#significant-whitespace";
			case XmlNodeType.XmlDeclaration:
				return "?xml";
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
		}

		private bool IsArray(IXmlNode node)
		{
			IXmlNode xmlNode;
			if (node.Attributes != null)
			{
				xmlNode = node.Attributes.SingleOrDefault((IXmlNode a) => a.LocalName == "Array" && a.NamespaceURI == "http://james.newtonking.com/projects/json");
			}
			else
			{
				xmlNode = null;
			}
			IXmlNode xmlNode2 = xmlNode;
			return xmlNode2 != null && XmlConvert.ToBoolean(xmlNode2.Value);
		}

		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			Dictionary<string, List<IXmlNode>> dictionary = new Dictionary<string, List<IXmlNode>>();
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				IXmlNode xmlNode = node.ChildNodes[i];
				string propertyName = this.GetPropertyName(xmlNode, manager);
				List<IXmlNode> list;
				if (!dictionary.TryGetValue(propertyName, out list))
				{
					list = new List<IXmlNode>();
					dictionary.Add(propertyName, list);
				}
				list.Add(xmlNode);
			}
			foreach (KeyValuePair<string, List<IXmlNode>> keyValuePair in dictionary)
			{
				List<IXmlNode> value = keyValuePair.Value;
				if (value.Count == 1 && !this.IsArray(value[0]))
				{
					this.SerializeNode(writer, value[0], manager, writePropertyName);
				}
				else
				{
					string key = keyValuePair.Key;
					if (writePropertyName)
					{
						writer.WritePropertyName(key);
					}
					writer.WriteStartArray();
					for (int j = 0; j < value.Count; j++)
					{
						this.SerializeNode(writer, value[j], manager, false);
					}
					writer.WriteEndArray();
				}
			}
		}

		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				if (this.IsArray(node) && node.ChildNodes.All((IXmlNode n) => n.LocalName == node.LocalName) && node.ChildNodes.Count > 0)
				{
					this.SerializeGroupedNodes(writer, node, manager, false);
				}
				else
				{
					foreach (IXmlNode xmlNode in node.Attributes)
					{
						if (xmlNode.NamespaceURI == "http://www.w3.org/2000/xmlns/")
						{
							string prefix = (!(xmlNode.LocalName != "xmlns")) ? string.Empty : xmlNode.LocalName;
							manager.AddNamespace(prefix, xmlNode.Value);
						}
					}
					if (writePropertyName)
					{
						writer.WritePropertyName(this.GetPropertyName(node, manager));
					}
					if (this.ValueAttributes(node.Attributes).Count<IXmlNode>() == 0 && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == XmlNodeType.Text)
					{
						writer.WriteValue(node.ChildNodes[0].Value);
					}
					else if (node.ChildNodes.Count == 0 && CollectionUtils.IsNullOrEmpty<IXmlNode>(node.Attributes))
					{
						writer.WriteNull();
					}
					else
					{
						writer.WriteStartObject();
						for (int i = 0; i < node.Attributes.Count; i++)
						{
							this.SerializeNode(writer, node.Attributes[i], manager, true);
						}
						this.SerializeGroupedNodes(writer, node, manager, true);
						writer.WriteEndObject();
					}
				}
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				if (node.NamespaceURI == "http://www.w3.org/2000/xmlns/" && node.Value == "http://james.newtonking.com/projects/json")
				{
					return;
				}
				if (node.NamespaceURI == "http://james.newtonking.com/projects/json" && node.LocalName == "Array")
				{
					return;
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				writer.WriteValue(node.Value);
				return;
			case XmlNodeType.Comment:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
				}
				return;
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				this.SerializeGroupedNodes(writer, node, manager, writePropertyName);
				return;
			case XmlNodeType.XmlDeclaration:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			IXmlDocument xmlDocument = null;
			IXmlNode xmlNode = null;
			if (xmlDocument == null || xmlNode == null)
			{
				throw new JsonSerializationException("Unexpected type when converting XML: " + objectType);
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new JsonSerializationException("XmlNodeConverter can only convert JSON that begins with an object.");
			}
			if (!string.IsNullOrEmpty(this.DeserializeRootElementName))
			{
				this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, manager);
			}
			else
			{
				reader.Read();
				this.DeserializeNode(reader, xmlDocument, manager, xmlNode);
			}
			return xmlDocument.WrappedNode;
		}

		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (propertyName != null)
			{
				if (propertyName == "#text")
				{
					currentNode.AppendChild(document.CreateTextNode(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#cdata-section")
				{
					currentNode.AppendChild(document.CreateCDataSection(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#whitespace")
				{
					currentNode.AppendChild(document.CreateWhitespace(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#significant-whitespace")
				{
					currentNode.AppendChild(document.CreateSignificantWhitespace(reader.Value.ToString()));
					return;
				}
			}
			if (!string.IsNullOrEmpty(propertyName) && propertyName[0] == '?')
			{
				this.CreateInstruction(reader, document, currentNode, propertyName);
			}
			else
			{
				if (reader.TokenType == JsonToken.StartArray)
				{
					this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
					return;
				}
				this.ReadElement(reader, document, currentNode, propertyName, manager);
			}
		}

		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new JsonSerializationException("XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			Dictionary<string, string> dictionary = this.ReadAttributeElements(reader, manager);
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				string prefix2 = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
				IXmlNode attributeNode = string.IsNullOrEmpty(prefix2) ? document.CreateAttribute(keyValuePair.Key, keyValuePair.Value) : document.CreateAttribute(keyValuePair.Key, manager.LookupNamespace(prefix2), keyValuePair.Value);
				xmlElement.SetAttributeNode(attributeNode);
			}
			if (reader.TokenType == JsonToken.String)
			{
				xmlElement.AppendChild(document.CreateTextNode(reader.Value.ToString()));
			}
			else if (reader.TokenType == JsonToken.Integer)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((long)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Float)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((double)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Boolean)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((bool)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Date)
			{
				DateTime value = (DateTime)reader.Value;
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString(value, DateTimeUtils.ToSerializationMode(value.Kind))));
			}
			else if (reader.TokenType != JsonToken.Null)
			{
				if (reader.TokenType != JsonToken.EndObject)
				{
					manager.PushScope();
					this.DeserializeNode(reader, document, manager, xmlElement);
					manager.PopScope();
				}
			}
		}

		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read() && reader.TokenType != JsonToken.EndArray)
			{
				this.DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (this.WriteArrayAttribute)
			{
				this.AddJsonArrayAttribute(xmlElement, document);
			}
			if (num == 1 && this.WriteArrayAttribute)
			{
				IXmlElement element = xmlElement.ChildNodes.CastValid<IXmlElement>().Single((IXmlElement n) => n.LocalName == propertyName);
				this.AddJsonArrayAttribute(element, document);
			}
		}

		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
		}

		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = false;
			bool flag2 = false;
			if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null && reader.TokenType != JsonToken.Boolean && reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Date && reader.TokenType != JsonToken.StartConstructor)
			{
				while (!flag && !flag2 && reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.PropertyName)
					{
						if (tokenType != JsonToken.EndObject)
						{
							throw new JsonSerializationException("Unexpected JsonToken: " + reader.TokenType);
						}
						flag2 = true;
					}
					else
					{
						string text = reader.Value.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							char c = text[0];
							if (c != '@')
							{
								if (c != '$')
								{
									flag = true;
								}
								else
								{
									text = text.Substring(1);
									reader.Read();
									string text2 = reader.Value.ToString();
									string text3 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
									if (text3 == null)
									{
										int? num = null;
										while (manager.LookupNamespace("json" + num) != null)
										{
											num = new int?(num.GetValueOrDefault() + 1);
										}
										text3 = "json" + num;
										dictionary.Add("xmlns:" + text3, "http://james.newtonking.com/projects/json");
										manager.AddNamespace(text3, "http://james.newtonking.com/projects/json");
									}
									dictionary.Add(text3 + ":" + text, text2);
								}
							}
							else
							{
								text = text.Substring(1);
								reader.Read();
								string text2 = reader.Value.ToString();
								dictionary.Add(text, text2);
								string prefix;
								if (this.IsNamespaceAttribute(text, out prefix))
								{
									manager.AddNamespace(prefix, text2);
								}
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			return dictionary;
		}

		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string version = null;
				string encoding = null;
				string standalone = null;
				while (reader.Read() && reader.TokenType != JsonToken.EndObject)
				{
					string text = reader.Value.ToString();
					if (text != null)
					{
						if (text == "@version")
						{
							reader.Read();
							version = reader.Value.ToString();
							continue;
						}
						if (text == "@encoding")
						{
							reader.Read();
							encoding = reader.Value.ToString();
							continue;
						}
						if (text == "@standalone")
						{
							reader.Read();
							standalone = reader.Value.ToString();
							continue;
						}
					}
					throw new JsonSerializationException("Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
				}
				IXmlNode newChild = document.CreateXmlDeclaration(version, encoding, standalone);
				currentNode.AppendChild(newChild);
			}
			else
			{
				IXmlNode newChild2 = document.CreateProcessingInstruction(propertyName.Substring(1), reader.Value.ToString());
				currentNode.AppendChild(newChild2);
			}
		}

		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			return string.IsNullOrEmpty(elementPrefix) ? document.CreateElement(elementName) : document.CreateElement(elementName, manager.LookupNamespace(elementPrefix));
		}

		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.StartConstructor:
				{
					string propertyName2 = reader.Value.ToString();
					while (reader.Read() && reader.TokenType != JsonToken.EndConstructor)
					{
						this.DeserializeValue(reader, document, manager, propertyName2, currentNode);
					}
					goto IL_17B;
				}
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == XmlNodeType.Document && document.DocumentElement != null)
					{
						goto Block_4;
					}
					string propertyName = reader.Value.ToString();
					reader.Read();
					if (reader.TokenType == JsonToken.StartArray)
					{
						int num = 0;
						while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						{
							this.DeserializeValue(reader, document, manager, propertyName, currentNode);
							num++;
						}
						if (num == 1 && this.WriteArrayAttribute)
						{
							IXmlElement element = currentNode.ChildNodes.CastValid<IXmlElement>().Single((IXmlElement n) => n.LocalName == propertyName);
							this.AddJsonArrayAttribute(element, document);
						}
					}
					else
					{
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
					}
					goto IL_17B;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_17B;
				}
				break;
				IL_17B:
				if (reader.TokenType != JsonToken.PropertyName && !reader.Read())
				{
					return;
				}
			}
			if (tokenType != JsonToken.EndObject && tokenType != JsonToken.EndArray)
			{
				throw new JsonSerializationException("Unexpected JsonToken when deserializing node: " + reader.TokenType);
			}
			return;
			Block_4:
			throw new JsonSerializationException("JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifing a DeserializeRootElementName.");
		}

		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith("xmlns", StringComparison.Ordinal))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName[5] == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		private IEnumerable<IXmlNode> ValueAttributes(IEnumerable<IXmlNode> c)
		{
			return from a in c
			where a.NamespaceURI != "http://james.newtonking.com/projects/json"
			select a;
		}

		public override bool CanConvert(Type valueType)
		{
			return false;
		}

		private const string TextName = "#text";

		private const string CommentName = "#comment";

		private const string CDataName = "#cdata-section";

		private const string WhitespaceName = "#whitespace";

		private const string SignificantWhitespaceName = "#significant-whitespace";

		private const string DeclarationName = "?xml";

		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";
	}
}
