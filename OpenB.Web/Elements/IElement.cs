using System.Web.UI;

namespace OpenB.Web.Elements
{
    public interface IElement
    {
        string Key { get; }
        void Initialize();
        void Render(HtmlTextWriter textWriter);
        IElementContainer ElementContainer { get;  }
    }

   
}