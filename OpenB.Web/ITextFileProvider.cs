namespace OpenB.Web
{
    public interface IFileProvider
    {
        string FileType { get; }
        string FileExtension { get; }
        
    }

    public interface ITextFileProvider : IFileProvider
    {
       
        string Provide(string filename);
    }
}