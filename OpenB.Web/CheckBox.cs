using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OpenB.Web
{
    public class CheckBox : BaseElement, IElement
    {
        public CheckBox(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }      

        public void Render(HtmlTextWriter textWriter)
        {
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
          
            textWriter.RenderEndTag();
        }
    }

    public class ComboBox : BaseElement, IElement
    {
        public ComboBox(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public void Render(HtmlTextWriter textWriter)
        {
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Select);
           
            textWriter.RenderEndTag();
        }
    }
}
