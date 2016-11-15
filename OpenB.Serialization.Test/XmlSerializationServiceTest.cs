using NUnit.Framework;
using OpenB.Core;
using OpenB.Serialization.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Serialization.Test
{
    [TestFixture]
    public class XmlSerializationServiceTest
    {
        public class ExampleModel : IModel
        {
            public string Description
            {
                get; set;
            }

            public bool IsActive
            {
                get; set;
            }

            public string Key
            {
                get; set;
            }

            public string Name
            {
                get; set;
            }
        }

        [Test]
        public void DoSomething()
        {

            XmlSerializationService xmlService = new XmlSerializationService();
            ExampleModel model = new Test.XmlSerializationServiceTest.ExampleModel();
            model.Description = "My First Example model description";

            var xmlString = xmlService.Serialize(model);
        }

    }
}
