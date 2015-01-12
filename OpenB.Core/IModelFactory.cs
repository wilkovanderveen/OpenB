namespace OpenB.Core
{
    public interface IModelFactory
    {
        T GetInstance<T>(string key) where T : class, IModel;
    }
}