using System;
using System.Collections.Generic;
using System.IO;

namespace OpenB.Web
{
    public class RequestManager : IRequestManager
    {
        public RequestManager()
        {
            FileProviders = new Dictionary<string, IFileProvider>();
        }

        private IDictionary<string, IFileProvider> FileProviders { get; }

        public void Register(IFileProvider provider)
        {
            FileProviders.Add(provider.FileExtension, provider);
        }

        public IFileProvider GetProvider(string path)
        {
            var lastIndexOfDot = path.LastIndexOf(".", StringComparison.Ordinal);

            if (lastIndexOfDot <= 0)
                return null;

            var extension = path.Substring(lastIndexOfDot + 1, path.Length - (lastIndexOfDot) - 1);

            IFileProvider provider;

            if (FileProviders.TryGetValue(extension, out provider))
            {
                return provider;
            }

            throw new NotSupportedException($"No provider was registered for extension {extension}.");
        }
    }
}