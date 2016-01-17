using System;
using System.Web.UI;

namespace OpenB.Web.Elements
{
    public abstract class BaseElement
    {
        public string Key { get; private set; }

        [MarkupLanguageProperty("visible")]
        public bool Visible { get; set; }

        internal void SetParent(IElementContainer elementContainer)
        {
            this.ElementContainer = elementContainer;
        }

        public IElementContainer ElementContainer { get; private set; }

        protected BaseElement(RenderContext renderContext, string key)
        {
            if (renderContext == null) throw new ArgumentNullException(nameof(renderContext));

            this.Key = key;
            RenderContext = renderContext;
        }

        public abstract void Render(HtmlTextWriter textWriter);

        protected RenderContext RenderContext { get; }
    }
}