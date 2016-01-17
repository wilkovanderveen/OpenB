using System;
using System.Collections.Generic;
using System.Web.UI;

namespace OpenB.Web.Elements
{
    public interface IRequestManager
    {
        void Register(IFileProvider provider);
        IFileProvider GetProvider(string path);
    }

    public abstract class BaseElementContainer : BaseElement, IElementContainer
    {
        protected BaseElementContainer(RenderContext renderContext, string key) : base(renderContext, key)
        {
            Elements = new List<IElement>();
        }

        public IList<IElement> Elements { get; }

        protected abstract void InnerInitialize();

        public void Initialize()
        {
            InnerInitialize();

            foreach (IElement element in Elements)
            {
                BaseElement baseElement = element as BaseElement;

                if (baseElement != null)
                {
                    baseElement.SetParent(this);
                }
                else
                {
                    throw new NotSupportedException("Element must be convertable to BaseElement");
                }

                element.Initialize();
            }
        }

        public void RenderChildren(HtmlTextWriter textWriter)
        {
            foreach (IElement element in Elements)
            {
                element.Render(textWriter);
            }
        }
    }

    public class Page : BaseElementContainer
    {
        public Page(RenderContext renderContext, string key) : base(renderContext, key)
        {           
            
        }

        protected override void InnerInitialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("jquery-1.9.1.min.js"));
            RenderContext.Scripts.Add(new JavaScriptSource("bootstrap.min.js"));
            RenderContext.Scripts.Add(new JavaScriptSource("angular.min.js"));

            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("bootstrap.min.css"));
            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("bootstrap-theme.min.css"));
        }

        public override void Render(HtmlTextWriter textWriter)
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

       

       
    }
}