using System.Runtime.InteropServices.ComTypes;

namespace OpenB.Core
{
    public class UserContext
    {
        private readonly IModelFactory _modelFactory;

        public UserContext(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public T GetInstance<T>(string key) where T : class, IModel
        {
           return  _modelFactory.GetInstance<T>(key);
        }
    }
}