using System;

namespace OpenB.Web
{
    public abstract class BaseModelBoundElement : BaseElement
    {
        protected BaseModelBoundElement(RenderContext renderContext, string key) : base(renderContext, key)
        {

        }      

        [MarkupLanguageProperty("Model")]
        public string ModelBindingExpression { get; set; }
    }

    public abstract class BaseElement
    {
        private readonly string key;

        protected BaseElement(RenderContext renderContext, string key)
        {
            if (renderContext == null) throw new ArgumentNullException(nameof(renderContext));

            this.key = key;           
            RenderContext = renderContext;
        }

        protected RenderContext RenderContext { get; }
    }
}