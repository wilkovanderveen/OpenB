using NUnit.Framework;
using OpenB.Core;
using OpenB.Serialization.Xml;
using System;

namespace OpenB.Serialization.Test
{
    [TestFixture]
    public class XsdCreationServiceTest
    {
        public abstract class BaseExampleModel : IModel
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

        public class ExampleModel : BaseExampleModel
        {
            public DateTime Date
            {
                get; set;
            }

            public bool Boolean
            {
                get; set;
            }
        }

        [Test]
        public void DoSomething()
        {
            XsdCreationService xsdCreator = new XsdCreationService();
            var xsd = xsdCreator.CreateXsd(typeof(ExampleModel));
        }
    }
}
