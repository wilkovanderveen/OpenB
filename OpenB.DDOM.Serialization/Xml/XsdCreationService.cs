using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Serialization.Xml
{
    public class XsdCreationService
    {
        public IList<Type> serializedTypes;

        public XsdCreationService()
        {
            serializedTypes = new List<Type>();
        }

        public string CreateXsd(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            StringBuilder xsdStringBuilder = new StringBuilder($"<xsd:schema xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");

            xsdStringBuilder.Append(CreateComplexType(type));

            xsdStringBuilder.AppendLine("</xsd:schema>");

            return xsdStringBuilder.ToString();
        }

        public string CreateComplexType(Type type)
        {
            StringBuilder xsdStringBuilder = new StringBuilder();

            if (type.BaseType != typeof(object))
            {
                xsdStringBuilder.Append(CreateComplexType(type.BaseType));
            }

            xsdStringBuilder.AppendLine($"<xsd:complexType name=\"{type.Name}\" abstract=\"{type.IsAbstract}\">");
            xsdStringBuilder.AppendLine($"<xsd:sequence>");

            foreach (MemberInfo info in type.GetMembers(BindingFlags.Instance).Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property))
            {
                Type memberType = info.MemberType == MemberTypes.Field ? (info as FieldInfo).FieldType : (info as PropertyInfo).PropertyType;                              

                if (memberType == typeof(string))
                {
                    xsdStringBuilder.AppendLine(CreateElement(info.Name, "xsd:string", true));
                }

                if (memberType == typeof(DateTime))
                {
                    xsdStringBuilder.AppendLine(CreateElement(info.Name, "xsd:dateTime", false));
                }

                if (memberType == typeof(bool))
                {
                    xsdStringBuilder.AppendLine(CreateElement(info.Name, "xsd:boolean", false));
                }
            }
            xsdStringBuilder.AppendLine("</xsd:sequence>");
            xsdStringBuilder.AppendLine("</xsd:complexType>");          

            return xsdStringBuilder.ToString();
        }

        private string CreateElement(string name, string type, bool isNullable)
        {
            return $"<xsd:element name=\"{name}\" type=\"{type}\" nillable=\"{isNullable}\"  />"; 
        }
    }

    
}
