using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NUnit.Framework;
using OpenB.Core.ACL;
using OpenB.Modeling;

namespace OpenB.DataStore.MongoDB.Test
{
    [TestFixture]
    public class ModelRetrievalTest
    {
        private class AuditableModel : IAuditableModel
        {
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            public string Key { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }


            public AuditRegistration Created { get; set; }
            public AuditRegistration Modified { get; set; }
            public AuditRegistration Accessed { get; set; }
        }

        [Test]
        public void GetAuditableModel_ReturnsModel()
        {
            var userGroup = new UserGroup("MY_USERGROUP", "MyUserGroup", "My usergroup");
            var user = new User(userGroup);

            IAuditableModel model = new AuditableModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Key = "MYFIRSTMODEL",
                Name = "MyFirstModel",
                IsActive = true,
                Description = "My first model",
                Created = new AuditRegistration(user, DateTime.Now),
                Modified = new AuditRegistration(user, DateTime.Now),
                Accessed = new AuditRegistration(user, DateTime.Now),
            };

            var dataStoreService = new MongoDataStoreService("192.168.56.1", "mydb");
            dataStoreService.CreateModel(model);
            IAuditableModel result = dataStoreService.GetModel<AuditableModel>("MYFIRSTMODEL");

            Assert.That(result, Is.Not.Null);
        }
    }
}