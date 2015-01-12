using System;

namespace OpenB.Modeling
{
    /// <summary>
    ///     Encapsulated model defintion, encapsulating an existing .NET type.
    /// </summary>
    public class EncapsulatedModelDefinition : ModelDefinition
    {
        private readonly Type _encapsulatedType;

        public EncapsulatedModelDefinition(string key, string name, string description, Type encapsulatedType)
            : base(key, name, description)
        {
            _encapsulatedType = encapsulatedType;
        }
    }
}