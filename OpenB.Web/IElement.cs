using System.Collections;
using System.Web.UI;

namespace OpenB.Web
{
    public interface IElement
    {
        void Initialize();
        void Render(HtmlTextWriter textWriter);
    }

   
}