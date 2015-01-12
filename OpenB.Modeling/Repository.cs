using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using OpenB.Core.ACL;
using OpenB.Core.Data;

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

        public T GetModel<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}