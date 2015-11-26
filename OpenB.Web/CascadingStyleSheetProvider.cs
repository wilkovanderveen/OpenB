using System;
using System.IO;

namespace OpenB.Web
{
    public class CascadingStyleSheetProvider : ITextFileProvider
    {
        public string FileType
        {
            get { return "text/css"; }
        }

        public string Provide(string filename)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var completefilename = string.Concat(currentPath, filename);

            return File.OpenText(completefilename).ReadToEnd();
        }

        public string FileExtension
        {
            get { return "css"; }
        }
    }
}