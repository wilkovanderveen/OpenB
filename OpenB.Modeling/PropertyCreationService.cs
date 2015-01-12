using System;
using OpenB.Core.Utils;

namespace OpenB.Modeling
{
    /// <summary>
    /// Property creation service.
    /// </summary>
    public class PropertyCreationService
    {
        public void CreatePropertyDefinition(PropertyDefinition propertyDefinition, string modelName, FormattedStringBuilder classStringBuilder)
        {
            classStringBuilder.AppendLine(string.Format("private {0} _{1};", modelName, propertyDefinition.Name));
            if (classStringBuilder == null)
                throw new ArgumentNullException("classStringBuilder");

            classStringBuilder.AppendLine(string.Format("public {0} {1}", modelName, propertyDefinition.Name));
            classStringBuilder.AppendLine("{");
            classStringBuilder.LevelDown();
            classStringBuilder.AppendLine();
            BuildGetter(propertyDefinition.Name, classStringBuilder);
            BuildSetter(propertyDefinition.Name, classStringBuilder);
            classStringBuilder.LevelUp();
            classStringBuilder.AppendLine("}");
        }

        static void BuildGetter(string name, FormattedStringBuilder classStringBuilder)
        {
            classStringBuilder.Append("get { return _");
            classStringBuilder.Append(string.Format("{0};", name));
            classStringBuilder.Append(" }");
            classStringBuilder.AppendLine();
        }

        static void BuildSetter(string name, FormattedStringBuilder classStringBuilder)
        {
            classStringBuilder.AppendLine("set ");
            classStringBuilder.LevelDown();
            classStringBuilder.AppendLine("{");
            classStringBuilder.LevelDown();
            classStringBuilder.AppendLine(string.Format("if (!_{0}.Equals(value))", name));
            classStringBuilder.AppendLine("{");
            classStringBuilder.LevelDown();
            classStringBuilder.AppendLine(string.Format("_{0} = value;", name));
            classStringBuilder.AppendLine("IsDirty = true;");
            classStringBuilder.LevelUp();
            classStringBuilder.AppendLine("}");
            classStringBuilder.LevelUp();
            classStringBuilder.AppendLine("}");
            classStringBuilder.LevelUp();
           
        }
    }
}