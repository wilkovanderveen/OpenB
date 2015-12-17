using System;
using System.Xml;
using System.Linq;
using System.Diagnostics.Contracts;
using OpenB.Web;

namespace OpenB.Web.OpenBML
{
    public class XmlParser
    {
        public ElementFactory ElementFactory { get; }

        public XmlParser(ElementFactory elementFactory)
        {
            if (elementFactory == null) throw new ArgumentNullException(nameof(elementFactory));
            ElementFactory = elementFactory;
        }

        public IElement Parse(XmlElement xmlNode)
        {
            IElement element = ElementFactory.GetElement(xmlNode);

            IElementContainer elementContainer = element as IElementContainer;

            if (elementContainer != null)
            {
                foreach (var xmlChildNode in xmlNode.ChildNodes)
                {
                    var realXmlNode = xmlChildNode as XmlElement;

                    if (realXmlNode != null)
                    {
                       elementContainer.Elements.Add(Parse(realXmlNode));
                    }
                }

                return elementContainer;
            }

            return element;         
        }
    }

    public class ElementFactory
    {
        readonly RenderContext renderContext;

        public ElementFactory(RenderContext renderContext)
        {
            this.renderContext = renderContext;
            Contract.Requires(renderContext != null);
        }

        public IElement GetElement(XmlElement xmlElement)
        {
            var key = xmlElement.Attributes["key"].Value;
            switch (xmlElement.LocalName)
            {
                case "Page":                   
                    Page page = new Page(renderContext, key);
                    return page;                   

                case "TextBox":                 
                    TextBox  textBox = new TextBox(renderContext, key);
                    return textBox;

                case "CheckBox":
                    CheckBox checkBox = new CheckBox(renderContext, key);
                    return checkBox;

                case "ComboBox":
                    ComboBox comboBox = new ComboBox(renderContext, key);
                    return comboBox;

                case "Component":
                    Component component = new Component(renderContext, key);
                    return component;

                default:
                    throw new NotSupportedException(string.Format("Element type {0} is not supported.", xmlElement.LocalName));
            }
        }
    }
}
