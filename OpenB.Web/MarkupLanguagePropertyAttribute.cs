using System;

namespace OpenB.Web
{
    internal class MarkupLanguagePropertyAttribute : Attribute
    {
        public string PropertyName { get; }

        public MarkupLanguagePropertyAttribute(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(string.Format("{0} cannot be null or empty.", nameof(propertyName)));
            }
        }
    }
}