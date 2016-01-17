using System.Web.UI;

namespace OpenB.Web.Elements
{
    public class CheckBox : BaseModelBoundElement, IElement
    {
        public CheckBox(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public override void Render(HtmlTextWriter textWriter)
        {
            textWriter.AddAttribute("ng-model", string.Join(".", ElementContainer.Key, Key));
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Id, Key);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            textWriter.RenderEndTag();
        }
    }
}
