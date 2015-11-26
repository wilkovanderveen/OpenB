using System;
using System.IO;

namespace OpenB.Web
{
    public class JavaScriptProvider : ITextFileProvider
    {
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

            return null;
        }
    }
}