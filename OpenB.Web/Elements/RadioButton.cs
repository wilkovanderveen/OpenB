using System.Web.UI;

namespace OpenB.Web.Elements
{
    internal class RadioButton : BaseModelBoundElement, IElement
    {
        public RadioButton(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
        }

        public override void Render(HtmlTextWriter textWriter)
        {
            textWriter.AddAttribute("ng-model", string.Join(".", ElementContainer.Key, Key));
            textWriter.AddAttribute(HtmlTextWriterAttribute.Id, Key);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            textWriter.RenderEndTag();
        }
    }
}