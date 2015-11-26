using System;

namespace OpenB.Web
{
    public abstract class BaseElement
    {
        private readonly string key;

        protected BaseElement(RenderContext renderContext, string key)
        {
            this.key = key;
            if (renderContext == null) throw new ArgumentNullException(nameof(renderContext));
            RenderContext = renderContext;
        }

        protected RenderContext RenderContext { get; }
    }
}