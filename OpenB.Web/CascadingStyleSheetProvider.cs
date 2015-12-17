using System;
using System.IO;
using System.Reflection;

namespace OpenB.Web
{
    public class CascadingStyleSheetProvider : ITextFileProvider
    {
        private string assemblyName;
        private string scriptNamespace;

        public CascadingStyleSheetProvider(string assemblyName, string scriptNamespace)
        {
            if (scriptNamespace == null)
                throw new ArgumentNullException(nameof(scriptNamespace));
            if (assemblyName == null)
                throw new ArgumentNullException(nameof(assemblyName));

            this.assemblyName = assemblyName;
            this.scriptNamespace = scriptNamespace;
        }

        public string FileType
        {
            get { return "text/css"; }
        }

        public string Provide(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            Assembly assem = Assembly.Load(assemblyName);

            filename = filename.Replace('/', '.');

            string resourceName = string.Concat(scriptNamespace, filename);

            Stream stream = assem.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ResourceNotFoundException($"Cannot provide for file {filename}. Resource not found {resourceName} in assembly {assemblyName}. ");
            }

            using (stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string FileExtension
        {
            get { return "css"; }
        }
    }
}