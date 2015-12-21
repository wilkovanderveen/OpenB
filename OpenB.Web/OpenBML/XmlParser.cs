using System;
using System.Xml;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Reflection;
using OpenB.Web.Elements;

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

            Type elementType =
                Assembly
                    .GetAssembly(typeof (IElement))
                    .GetExportedTypes().SingleOrDefault(t => typeof(BaseElement).IsAssignableFrom(t) && t.Name.Equals(xmlElement.LocalName) && !t.IsInterface && !t.IsAbstract);

            if (elementType != null)
            {
                object element = Activator.CreateInstance(elementType, renderContext, key);

                foreach (XmlAttribute xmlAttribute in xmlElement.Attributes)
                {
                    if (xmlAttribute.LocalName.Equals("key")  || xmlAttribute.Name.StartsWith("xmlns:"))
                        continue;

                    PropertyInfo  propertyInfo =
                        elementType.GetProperties().Where(p => p.GetCustomAttribute<MarkupLanguagePropertyAttribute>() != null && p.GetCustomAttribute<MarkupLanguagePropertyAttribute>()
                            .PropertyName.Equals(xmlAttribute.LocalName)).SingleOrDefault();
                        

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(element, xmlAttribute.InnerText);
                    }
                    else
                    {
                        throw new NotSupportedException($"Element type {xmlElement.LocalName} does not support {xmlAttribute.LocalName}.");
                    }
                }
                return element as IElement;
            }
            throw new NotSupportedException($"Element type {xmlElement.LocalName} is not supported.");
            
        }
    }
}
