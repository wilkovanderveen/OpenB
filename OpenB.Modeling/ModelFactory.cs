using System;
using OpenB.Core;

namespace OpenB.Modeling
{
    public class ModelFactory : IModelFactory 
    {
        
        private readonly ModelCreationService _modelCreationService;
        private readonly ModelAuthorizationService _modelAuthorizationService;

        public ModelFactory(Project project)
        {

            _modelCreationService = new ModelCreationService(project.Name);
            
        }

        public object CreateInstance(ModelDefinition definition, string key, string name, string description)
        {
            return  _modelCreationService.InstantiateModel(definition, key, name, description);
        }


        public T GetInstance<T>(string key) where T : class, IModel 
        {
            if (typeof(T) == typeof(IAuthorizebleModel))
            {
                
            }
            throw new NotImplementedException();
        }
    }
}