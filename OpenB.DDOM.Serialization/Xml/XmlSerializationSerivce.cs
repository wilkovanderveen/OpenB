using OpenB.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenB.Serialization.Xml
{
    public class XmlSerializationService
    {
        public string Serialize(IModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            StringBuilder stringBuilder = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(model.GetType());
            StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, model);

            return stringBuilder.ToString();
        }
    }
}
