using System.Collections.Generic;

namespace OpenB.Modeling
{
    public class ProtectedModelDefinition : ModelDefinition
    {
        public ProtectedModelDefinition(string key, string name, string description,
            IList<PropertyDefinition> properties) : base(key, name, description, properties, DefinitionFlags.Authorizable)
        {
        }
    }
}