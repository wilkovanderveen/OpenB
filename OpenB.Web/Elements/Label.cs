using System.Web.UI;
using Microsoft.SqlServer.Server;

namespace OpenB.Web.Elements
{
    public class Label : BaseModelBoundElement, IElement
    {
        public Label(RenderContext renderContext, string key) : base(renderContext, key)
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
            textWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            textWriter.RenderEndTag();
        }
    }
}