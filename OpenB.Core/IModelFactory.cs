namespace OpenB.Core
{
    public interface IModelFactory
    {
        object ReadInstance(string definitionKey, string modelKey);
    }
}