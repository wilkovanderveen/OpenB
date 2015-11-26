namespace OpenB.Web
{
    public interface ITextFileProvider 
    {
        string FileType { get; }
        string FileExtension { get; }
        string Provide(string filename);
    }
}