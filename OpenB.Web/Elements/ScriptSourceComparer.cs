using System.Collections.Generic;

namespace OpenB.Web.Elements
{
    public class ScriptSourceComparer : IEqualityComparer<IScriptSource>
    {
        public bool Equals(IScriptSource x, IScriptSource y)
        {
            if (x == null || y == null)
                return false;

            return (x.HtmlTag.Equals(y.HtmlTag));
        }
        
        public int GetHashCode(IScriptSource obj)
        {
            return obj.HtmlTag.GetHashCode();
        }
    }
}