using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenB.Web.Elements
{
    public class TextBox : BaseModelBoundElement, IElement
    {
        public TextBox(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public string Value { get; set; }
       

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public override void Render(HtmlTextWriter textWriter)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));

            textWriter.AddAttribute("ng-model", string.Join(".", ElementContainer.Key, Key));
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
                      
            textWriter.Write(Value);
           
            textWriter.RenderEndTag();
        }
    }
}
