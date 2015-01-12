﻿using System.Collections.Generic;
using NUnit.Framework;
using OpenB.Core;

namespace OpenB.Modeling.Test
{
    [TestFixture]
    public class CompilerTest
    {
        [Test]
        public void Compile_SimpleModel_KeyNameDescription_AreFilled()
        {
            EncapsulatedModelDefinition encapsulatedModelDefinition = new EncapsulatedModelDefinition("STRING", "string", "string", typeof(string));
            PropertyDefinition propertyDefinition = new PropertyDefinition("MYFIRSTMODELPROPERTY",
                encapsulatedModelDefinition);

            ModelDefinition definition = new ModelDefinition("MYFIRSTDEFININTION", "MyFirstDefinition",
                "My first definition", new List<PropertyDefinition>() { propertyDefinition }, DefinitionFlags.None);


            ModelFactory factory = new ModelFactory(new Project("MyFirstProject"));
            IModel model = (IModel)factory.CreateInstance(definition, "KEY", "NAME", "DESCRIPTION");

            Assert.That(model.GetType().Assembly.GetName().Name, Is.EqualTo("MyFirstProject"));
            Assert.That(model.GetType().Name, Is.EqualTo("MyFirstDefinition"));

            Assert.That(model.Key, Is.EqualTo("KEY"));
            Assert.That(model.Name, Is.EqualTo("NAME"));
            Assert.That(model.Description, Is.EqualTo("DESCRIPTION"));
        }
    }
}