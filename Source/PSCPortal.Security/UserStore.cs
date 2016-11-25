using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;

namespace PSCPortal.Security
{
    public class UserStore
    {
        private UserCollection _Users;


        public UserStore()
        {
          //  _Users = new UserCollection();
           // LoadStore();
        }
        public static UserStore GetStore()
        {
            return null;
        }

        private void LoadStore()
        {
            try
            {
                _Users = UserCollection.GetUserCollection() ;
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
        }
        private void SaveStore()
        {
            try
            {
              //  _Users.Save();
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
        }

        public UserCollection Users
        {
            get { 
                return UserCollection.GetUserCollectionLogin(); 
            }
        }
        public void Save()
        {
            SaveStore();
        }
        public User GetUserByName(string name)
        {
   
            return Users.Where(delegate(User user)
            {
                return string.Equals(name, user.Name);
            }).SingleOrDefault<User>();
        }
        public User GetUserByEmail(string email)
        {
            return _Users.Where(delegate(User user)
            {
                return string.Equals(email, user.Email);
            }).Single<User>();
        }
        public User GetUserByID(Guid key)
        {
            return _Users.Where(delegate(User user)
            {
                return (user.Id.CompareTo(key) == 0);
            }).Single<User>();
        }
    }
}
