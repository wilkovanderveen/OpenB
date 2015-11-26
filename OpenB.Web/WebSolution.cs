using System;
using OpenB.Web.Theming;

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
                RenderContext.RequestManager.Register(new CascadingStyleSheetProvider());
                RenderContext.RequestManager.Register(new JavaScriptProvider());

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