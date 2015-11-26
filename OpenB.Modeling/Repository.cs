using System;
using OpenB.Core.ACL;
using OpenB.Core.Data;
using OpenB.Core;

namespace OpenB.Modeling
{
    public class Repository
    {
        private readonly IDataStoreService _dataStoreService;
        private readonly IModelAuthorizationService _modelAuthorizationService;
        private readonly User _user;

        public Repository(IDataStoreService dataStoreService, IModelAuthorizationService modelAuthorizationService, User user)
        {
            _dataStoreService = dataStoreService;
            _modelAuthorizationService = modelAuthorizationService;
            _user = user;
        }

        public T ReadModel<T>(string key) where T : IModel
        {
            throw new NotImplementedException();
        }        
    }
}