using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OpenB.Web
{
    public class Component : BaseElement, IElementContainer
    {
        public Component(RenderContext renderContext, string key) : base(renderContext, key)
        {
            Elements = new List<IElement>();
        }

        public IList<IElement> Elements { get; set; }
      

        public void Initialize()
        {
            foreach (IElement element in Elements)
            {
                element.Initialize();
            }
        }

        public void Render(HtmlTextWriter textWriter)
        {
            textWriter.RenderBeginTag(HtmlTextWriterTag.Form);

            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "container");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
         
            foreach (IElement element  in Elements)
            {
                element.Render(textWriter);
            }
                       
            textWriter.RenderEndTag();
            textWriter.RenderEndTag();
        }
    }
}
