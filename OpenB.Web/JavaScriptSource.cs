namespace OpenB.Web
{
    public class JavaScriptSource : IScriptSource
    {
        public JavaScriptSource(string scriptFile)
        {
            HtmlTag = $"<script src=\"{scriptFile}\" type=\"text/javascript\"></script>";
        }

        public string HtmlTag { get; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            JavaScriptSource javaScriptSource = obj as JavaScriptSource;

            if (javaScriptSource == null)
                return false;

            return (HtmlTag.Equals(javaScriptSource.HtmlTag));
        }
    }
}