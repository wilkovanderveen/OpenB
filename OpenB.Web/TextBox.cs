using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OpenB.Web
{
    public class TextBox : BaseElement, IElement
    {
        public TextBox(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public string Value { get; set; }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public void Render(HtmlTextWriter textWriter)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));

            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
                      
            textWriter.Write(Value);
           
            textWriter.RenderEndTag();
        }
    }
}
