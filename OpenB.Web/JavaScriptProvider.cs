using OpenB.Web.OpenBML;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace OpenB.Web
{
    public interface IBinairyStreamProvider : IFileProvider
    {

    }

    public class IconProvider : IBinairyStreamProvider
    {
        public string FileExtension
        {
            get
            {
                return "ico";
            }
        }

        public string FileType
        {
            get
            {
                return "image/icon";
            }
        }

        public Stream Provide(string filename)
        {
            throw new NotImplementedException();
        }
    }

    public class JSONMapProvider : ITextFileProvider
    {
        private string assemblyName;
        private string scriptNamespace;

        public JSONMapProvider(string assemblyName, string scriptNamespace)
        {
            if (scriptNamespace == null)
                throw new ArgumentNullException(nameof(scriptNamespace));
            if (assemblyName == null)
                throw new ArgumentNullException(nameof(assemblyName));

            this.assemblyName = assemblyName;
            this.scriptNamespace = scriptNamespace;
        }

        public string FileExtension
        {
            get
            {
               return "map";
            }
        }

        public string FileType
        {
            get
            {
                return "text/json";
            }
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
    }

    public class OpenBMLProvider : ITextFileProvider
    {
        public XmlParser XmlParser { get; }

        public OpenBMLProvider(XmlParser xmlParser)
        {
            if (xmlParser == null)
                throw new ArgumentNullException(nameof(xmlParser));

            XmlParser = xmlParser;
        }

        public string FileExtension
        {
            get
            {
                return "obml";
            }
        }

        public string FileType
        {
            get
            {
                return "text/html";
            }
        }

        public string Provide(string filename)
        {
            filename = filename.Remove(0, 1);

            var obmlFileFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            var fullFilePath = Path.Combine(obmlFileFolder, filename);

            XmlDocument obmlXml = new XmlDocument();
            obmlXml.Load(fullFilePath);

            var element = XmlParser.Parse(obmlXml.DocumentElement);

            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);

            element.Initialize();

            element.Render(textWriter);

            return stringBuilder.ToString();

        }

        private void DoSomething()
        {

            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            XhtmlTextWriter textWriter = new XhtmlTextWriter(stringWriter);
        }
    }

    public class JavaScriptProvider : ITextFileProvider
    {
        private readonly string scriptNamespace;
        private readonly string assemblyName;

        public JavaScriptProvider(string assemblyName, string scriptNamespace)
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
            get { return "text/javascript"; }
        }

        public string FileExtension
        {
            get { return "js"; }
        }

        public string Provide(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            Assembly assem = Assembly.Load(assemblyName);

            filename = filename.Replace('/', '.');

            using (Stream stream = assem.GetManifestResourceStream(string.Concat(scriptNamespace, filename)))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}