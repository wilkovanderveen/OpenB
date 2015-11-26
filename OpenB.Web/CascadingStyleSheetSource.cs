namespace OpenB.Web
{
    public class CascadingStyleSheetSource : IScriptSource
    {
        public CascadingStyleSheetSource(string scriptFile)
        {
            HtmlTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"css/{scriptFile}\" />";
        }

        public string HtmlTag { get; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CascadingStyleSheetSource cascadingStyleSheetSource = obj as CascadingStyleSheetSource;

            if (cascadingStyleSheetSource == null)
                return false;

            return (HtmlTag.Equals(cascadingStyleSheetSource.HtmlTag));
        }
    }
}