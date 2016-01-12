using System;
using System.Collections.Generic;

namespace OpenB.Web.Elements
{
    public class RenderContext
    {
        public RenderContext(IRequestManager requestManager)
        {
            if (requestManager == null) throw new ArgumentNullException(nameof(requestManager));
            RequestManager = requestManager;

            Scripts = new HashSet<IScriptSource>(new ScriptSourceComparer());
            Stylesheets = new HashSet<CascadingStyleSheetSource>(new ScriptSourceComparer());
        }

        public HashSet<IScriptSource> Scripts { get; set; }
        public HashSet<CascadingStyleSheetSource> Stylesheets { get; set; }
        public IRequestManager RequestManager { get; }
    }
}