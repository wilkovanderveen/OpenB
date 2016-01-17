using System.Collections.Generic;
using System.Web.UI;

namespace OpenB.Web.Elements
{
    public interface IElementContainer : IElement 
    {
        IList<IElement> Elements { get; }
        void RenderChildren(HtmlTextWriter htmlTextWriter);
    }
}