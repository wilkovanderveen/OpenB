using System;

namespace OpenB.Modeling
{
    /// <summary>
    ///     Encapsulated model defintion, encapsulating an existing .NET type.
    /// </summary>
    public class EncapsulatedModelDefinition : ModelDefinition
    {
        public Type EncapsulatedType { get; private set; }

        public EncapsulatedModelDefinition(string key, string name, string description, Type encapsulatedType)
            : base(key, name, description)
        {
            EncapsulatedType = encapsulatedType;
        }
    }
}