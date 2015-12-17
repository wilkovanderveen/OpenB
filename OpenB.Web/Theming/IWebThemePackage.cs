namespace OpenB.Web.Theming
{
    public interface IWebThemePackage
    {
        /// <summary>
        /// Provides javascript files for this theme.
        /// </summary>
        ITextFileProvider JavaScriptProvider { get;  }
        ITextFileProvider JSONMapProvider { get;  }
        ITextFileProvider StyleSheetProvider { get;  }
    }
}