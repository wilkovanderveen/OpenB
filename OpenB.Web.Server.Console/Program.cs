using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using OpenB.Web.Bootstrap;
using OpenB.Web.Theming;

namespace OpenB.Web.Server.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HttpServer.SimpleListenerExample(new[] {"http://localhost:18000/"});
        }
    }

    public class HttpServer
    {
        private static HttpListener listener;

        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                System.Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            listener = new HttpListener {AuthenticationSchemes = AuthenticationSchemes.Anonymous};

            // Add the prefixes.
            foreach (var s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();

            var Listening = true;

            while (Listening)
            {
                // wait for next incoming request
                IAsyncResult result = listener.BeginGetContext(ListenerCallback, listener);
                result.AsyncWaitHandle.WaitOne();
            }
        }

        private static void ListenerCallback(IAsyncResult result)
        {
            HttpListenerContext context = listener.EndGetContext(result);

            var path = context.Request.Url.AbsolutePath.Split('/');

            if (context.Request.Cookies["OpenB.SessionId"] == null)
            {
                context.Response.Cookies.Add(new Cookie("OpenB.SessionId", Guid.NewGuid().ToString(), "/"));
            }
                        
            System.Console.WriteLine("Request path: " + context.Request.Url.AbsolutePath);

            IWebThemePackage bootStrapWebThemePackage = new ThemeLoader().Initialize();

            WebSolution = WebSolution.GetInstance(bootStrapWebThemePackage);
            WebSolution.Initialize();

            ITextFileProvider provider = WebSolution.RenderContext.RequestManager.GetProvider(context.Request.Url.AbsolutePath) as ITextFileProvider;

            if (provider != null)
            {
                try
                {
                    var stringResource = provider.Provide(context.Request.Url.AbsolutePath);
                    if (stringResource != null)
                    {
                        var stringBuffer = Encoding.UTF8.GetBytes(stringResource);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = provider.FileType;
                        context.Response.OutputStream.Write(stringBuffer, 0, stringBuffer.Length);
                        context.Response.Close();
                    }
                }
                catch(ResourceNotFoundException rnex)
                {
                    context.Response.StatusCode = 404;
                    context.Response.Close();
                   
                }
               
            }            
        }

        public static WebSolution WebSolution { get; set; }

        private static byte[] CreateContent(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }

   

    internal class Button : BaseElement, IElement
    {
        public Button(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public string Text { get; set; }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("controls.css"));
        }

        public void Render(HtmlTextWriter textWriter)
        {
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            textWriter.Write(Text);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            textWriter.RenderEndTag();
        }
    }

   

    internal class RadioButton : BaseElement, IElement
    {
        public RadioButton(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public void Render(HtmlTextWriter textWriter)
        {
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
            textWriter.RenderEndTag();
        }
    }
}