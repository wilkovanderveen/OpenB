using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenB.DSL
{
    public interface AIExpression
    {
        string Key { get; }
        string Description { get; }
        bool Evaluate();
    }

    public class ClassGenerator
    {
        public void GenerateClass(string key, string description)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("OpenB.Expressions");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            CodeTypeDeclaration targetClass = new CodeTypeDeclaration("CodeDOMCreatedClass");
            targetClass.IsClass = true;
            targetClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);

        }
    }
}
