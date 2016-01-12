using System.Web.UI;

namespace OpenB.Web.Elements
{
    public class Button : BaseElement, IElement
    {
        public Button(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }

        [MarkupLanguageProperty("text")]
        public string Text { get; set; }

        public void Initialize()
        {
            RenderContext.Scripts.Add(new JavaScriptSource("OpenB.Controls.js"));
            RenderContext.Stylesheets.Add(new CascadingStyleSheetSource("controls.css"));
        }

        public override void Render(HtmlTextWriter textWriter)
        {
            textWriter.AddAttribute("ng-click", $"update({ElementContainer.Key})");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Value, Text);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            
            textWriter.RenderEndTag();
            
        }
    }
}