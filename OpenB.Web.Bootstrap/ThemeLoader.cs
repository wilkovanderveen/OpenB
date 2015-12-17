using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenB.Web.Theming;

namespace OpenB.Web.Bootstrap
{
    public class ThemeLoader : IWebThemeLoader
    {
        public ThemeLoader()
        {
            ThemeConfiguration = new ThemeConfiguration("OpenB.Web.Bootstrap")
            {
                FontFolder = "fonts",
                ContentFolder = "Content",
                ScriptFolder = "Scripts"
            };
        }

        public string Name
        {
            get { return "Bootstrap"; }
        }

        public ThemeConfiguration ThemeConfiguration { get; set; }

        public IWebThemePackage Initialize()
        {
            BootstrapWebThemePackage themePackage = new BootstrapWebThemePackage(ThemeConfiguration);

            var contentNamespace = string.Join(".", ThemeConfiguration.ThemeAssembly.GetName().Name,
                ThemeConfiguration.ContentFolder);
            var scriptNamespace = string.Join(".", ThemeConfiguration.ThemeAssembly.GetName().Name,
                ThemeConfiguration.ScriptFolder);

            var cssTypes =
                ThemeConfiguration.ThemeAssembly.GetManifestResourceNames()
                    .Where(t => t.StartsWith(contentNamespace) && t.EndsWith(".css"));
            var scriptTypes =
                ThemeConfiguration.ThemeAssembly.GetManifestResourceNames()
                    .Where(t => t.StartsWith(contentNamespace) && t.EndsWith(".js"));

            foreach (var cssSource in cssTypes)
            {
                themePackage.AddStylesheets(new CascadingStyleSheetSource(cssSource));
            }

            foreach (var javaScriptSource in scriptTypes)
            {
                themePackage.AddScript( new JavaScriptSource(javaScriptSource));
            }

            return themePackage;
        }
    }

    public class BootstrapWebThemePackage : IWebThemePackage
    {
        private readonly ThemeConfiguration themeConfiguration;
        private readonly IList<CascadingStyleSheetSource> _styleSheets;
        private readonly IList<JavaScriptSource > _scripts;

        public BootstrapWebThemePackage(ThemeConfiguration themeConfiguration)
        {
            if (themeConfiguration == null) throw new ArgumentNullException(nameof(themeConfiguration));
            _styleSheets = new List<CascadingStyleSheetSource>();
            _scripts = new List<JavaScriptSource>();

            this.themeConfiguration = themeConfiguration;
        }

        public IEnumerable<CascadingStyleSheetSource> Stylesheets
        {
            get { return _styleSheets; }
        }

        public IEnumerable<JavaScriptSource> Scripts
        {
            get { return _scripts; }
        }

        public ITextFileProvider JavaScriptProvider
        {
            get
            {
                return new JavaScriptProvider("OpenB.Web.Bootstrap", "OpenB.Web.Bootstrap.Scripts");
            }          
        }

        public ITextFileProvider JSONMapProvider
        {
            get
            {
                return new JSONMapProvider("OpenB.Web.Bootstrap", "OpenB.Web.Bootstrap.Scripts");
            }          
        }

        public ITextFileProvider StyleSheetProvider
        {
            get
            {
                return new CascadingStyleSheetProvider ("OpenB.Web.Bootstrap", "OpenB.Web.Bootstrap.Content");
            }           
        }

        public void AddStylesheets(CascadingStyleSheetSource cascadingStyleSheetSource)
        {
           _styleSheets.Add(cascadingStyleSheetSource );
        }

        public void AddScript(JavaScriptSource  scriptSource)
        {
            _scripts.Add(scriptSource);
        }
    }

    public class ThemeConfiguration
    {
        public ThemeConfiguration(string themeAssembly)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themeAssembly + ".dll");
            ThemeAssembly = Assembly.LoadFile(fullPath);
        }

        public Assembly ThemeAssembly { get; set; }

        public string FontFolder { get; set; }
        public string ContentFolder { get; set; }
        public string ScriptFolder { get; set; }
    }
}