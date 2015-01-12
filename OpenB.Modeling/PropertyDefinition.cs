using System;

namespace OpenB.Modeling
{
    public class PropertyDefinition
    {
        public PropertyFlags PropertyFlags { get; private set; }
        public string Name { get; private set; }
        public ModelDefinition ModelDefinition { get; private set; }

        public PropertyDefinition(string name, ModelDefinition modelDefinition)
        {
            Name = name;
            ModelDefinition = modelDefinition;
        }

        public PropertyDefinition(string name, ModelDefinition modelDefinition, PropertyFlags propertyFlags) : this(name, modelDefinition)
        {
            PropertyFlags = propertyFlags;
        }
    }

    [Flags]
    public enum PropertyFlags
    {
        None,
        Required
    }
}