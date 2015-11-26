using System.Collections.Generic;

namespace OpenB.Web
{
    public interface IElementContainer : IElement 
    {
        IList<IElement> Elements { get; }
    }
}