using System.Collections.Generic;

namespace OpenB.Core.ACL
{
    public class User
    {
        public User(UserGroup primairyUserGroup)
        {
            UserGroups = new List<UserGroup>();
            UserGroups.Add(primairyUserGroup);
        }

        public List<UserGroup> UserGroups { get; private set; }
    }

    public class UserGroup
    {
        public UserGroup(string key, string name, string description)
        {
            Description = description;
            Key = key;
            Name = name;
        }

        public string Name { get; private set; }
        public string Key { get; private set; }
        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is UserGroup && (obj as UserGroup).Key == this.Key)
            {
                return true;
            }
            return false;
        }
    }
    
   
}