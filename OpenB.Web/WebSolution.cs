using System;
using OpenB.Web.Elements;
using OpenB.Web.Theming;
using OpenB.Web.OpenBML;

namespace OpenB.Web
{
    public class WebSolution
    {
        private readonly IWebThemePackage webThemePackage;

        public WebSolution(IWebThemePackage webThemePackage, RenderContext renderContext)
        {
            if (webThemePackage == null) throw new ArgumentNullException(nameof(webThemePackage));
            this.webThemePackage = webThemePackage;
            this.RenderContext = renderContext;
        }

        public RenderContext RenderContext { get; }

        public void Initialize()
        {
            if (!isInitialized)
            {
                RenderContext.RequestManager.Register(webThemePackage.StyleSheetProvider);
                RenderContext.RequestManager.Register(webThemePackage.JavaScriptProvider);
                RenderContext.RequestManager.Register(webThemePackage.JSONMapProvider);
                RenderContext.RequestManager.Register(new IconProvider());
                RenderContext.RequestManager.Register(new OpenBMLProvider(new OpenBML.XmlParser(new ElementFactory(this.RenderContext))));

                isInitialized = true;
            }
        }

        private bool isInitialized;

        private static WebSolution _webSolution;

        public static WebSolution GetInstance(IWebThemePackage themePackage)
        {
            if (_webSolution == null)
            {
                _webSolution = new WebSolution(themePackage, new RenderContext(new RequestManager()));
            }

            return _webSolution;
        }
    }
}