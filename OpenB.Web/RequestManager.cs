using System;
using System.Collections.Generic;
using System.IO;

namespace OpenB.Web
{
    public class RequestManager : IRequestManager
    {
        public RequestManager()
        {
            FileProviders = new Dictionary<string, ITextFileProvider>();
        }

        private IDictionary<string, ITextFileProvider> FileProviders { get; }

        public void Register(ITextFileProvider provider)
        {
            FileProviders.Add(provider.FileExtension, provider);
        }

        public ITextFileProvider GetProvider(string path)
        {
            var lastIndexOfDot = path.LastIndexOf(".", StringComparison.Ordinal);

            if (lastIndexOfDot <= 0)
                return null;

            var extension = path.Substring(lastIndexOfDot + 1, path.Length - (lastIndexOfDot) - 1);

            ITextFileProvider provider;

            if (FileProviders.TryGetValue(extension, out provider))
            {
                return provider;
            }

            throw new NotSupportedException($"No provider was registered for extension {extension}.");
        }
    }
}