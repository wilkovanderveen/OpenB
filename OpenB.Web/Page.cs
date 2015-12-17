using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace OpenB.Web
{
    public class ScriptSourceComparer : IEqualityComparer<IScriptSource>
    {
        public bool Equals(IScriptSource x, IScriptSource y)
        {
            if (x == null || y == null)
                return false;

            return (x.HtmlTag.Equals(y.HtmlTag));
        }
        
        public int GetHashCode(IScriptSource obj)
        {
            return obj.HtmlTag.GetHashCode();
        }
    }

    public class RenderContext
    {
        public RenderContext(IRequestManager requestManager)
        {
            if (requestManager == null) throw new ArgumentNullException(nameof(requestManager));
            RequestManager = requestManager;

            Scripts = new HashSet<IScriptSource>(new ScriptSourceComparer());
            Stylesheets = new HashSet<CascadingStyleSheetSource>(new ScriptSourceComparer());
        }

        public HashSet<IScriptSource> Scripts { get; set; }
        public HashSet<CascadingStyleSheetSource> Stylesheets { get; set; }
        public IRequestManager RequestManager { get; }
    }

    public interface IRequestManager
    {
        void Register(IFileProvider provider);
        IFileProvider GetProvider(string path);
    }

    public class Page : BaseElement, IElementContainer
    {
        public Page(RenderContext renderContext, string key) : base(renderContext, key)
        {           
            Elements = new List<IElement>();
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("jquery-1.9.1.min.js"));
            RenderContext.Scripts.Add(new JavaScriptSource("bootstrap.min.js"));

            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("bootstrap.min.css"));
            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("bootstrap-theme.min.css"));
          

            foreach (IElement element in Elements)
            {
                element.Initialize();
            }
        }

        public void Render(HtmlTextWriter textWriter)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));

            textWriter.WriteLine("<!DOCTYPE html>");
            textWriter.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");

            textWriter.AddAttribute("lang", "en");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Html);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Head);

            foreach (IScriptSource scriptSource in RenderContext.Scripts)
            {
                textWriter.Write(scriptSource.HtmlTag);
            }

            foreach (CascadingStyleSheetSource styleSheet in RenderContext.Stylesheets)
            {
                textWriter.Write(styleSheet.HtmlTag);
            }

            textWriter.RenderEndTag();
            textWriter.RenderBeginTag(HtmlTextWriterTag.Body);

            RenderChildren(textWriter);

            textWriter.RenderEndTag();
            textWriter.RenderEndTag();
        }

        public IList<IElement> Elements { get; }

        public void RenderChildren(HtmlTextWriter textWriter)
        {
            foreach (IElement element in Elements)
            {
                element.Render(textWriter);
            }
        }
    }
}