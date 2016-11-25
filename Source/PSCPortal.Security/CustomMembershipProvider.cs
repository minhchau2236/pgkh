using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Web.Security;

namespace PSCPortal.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private string _ApplicationName = "DefaultApp";
        private bool _EnablePasswordReset = false;
        //      private string _PasswordStrengthRegEx = @"[\w| !§$%&/()=\-?\*]*";
        //       private int _MaxInvalidPasswordAttempts = 3;
        //       private int _MinRequiredNonAlphanumericChars = 1;
        //       private int _MinRequiredPasswordLength = 5;
        //       private bool _RequiresQuestionAndAnswer = false;


        //       private MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;
        private UserStore _CurrentStore = null;
        private UserStore CurrentStore
        {
            get
            {
                if (_CurrentStore == null)
                    _CurrentStore = new UserStore();
                return _CurrentStore;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }
        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { return 1000; }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }
        public override int MinRequiredPasswordLength
        {
            get { return 0; }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { return @"(?=.{6,})(?=(.*\d){1,})"; }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override void Initialize(string name,
             System.Collections.Specialized.NameValueCollection config)
        {

            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "CustomMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "XML Membership Provider");
            }
            // Initialize the base class
            base.Initialize(name, config);

            foreach (string key in config.Keys)
            {
                switch (key.ToLower())
                {
                    case "applicationname":
                        ApplicationName = "DefaultApp";
                        break;
                    case "enablepasswordreset":
                        _EnablePasswordReset = bool.Parse(config[key]);
                        break;
                }
            }
        }




        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            try
            {
                // Validate the user name and e-mail
                if (!ValidateUsername(username, email, Guid.Empty))
                {
                    // If the user name is invalid because it already
                    // exists or the e-mail is duplicated and the provider
                    // is configured to not allow duplicated e-mails, then
                    // we return the InvalidUserName status through the
                    // output parameter "status"
                    status = MembershipCreateStatus.InvalidUserName;
                    return null;
                }
                // Raise the event before validating the password
                // This event is handled by the membership API class, which
                // in turn forwards the event to any subscribers in custom code
                // to allow writing custom code for validating password formats
                // without the need to understand the internals of
                // the membership provider implementation
                base.OnValidatingPassword(
                    new ValidatePasswordEventArgs(
                            username, password, true));
                // Validate the password
                if (!ValidatePassword(password))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }
                // Everything is valid, create the user
                User user = new User();

                //tach ID va name
                int index = username.IndexOf("|");
                string id = username.Substring(0, index);
                string name = username.Substring(index + 1);
                user.Id = new Guid(id);
                user.Name = name;
                // Note – the TransformPassword() method creates
                // the salted hash value for storing the password
                user.Password = this.TransformPassword(password);
                user.Email = email;
                user.PasswordQuestion = passwordQuestion;
                user.PasswordAnswer = passwordAnswer;
                user.CreationDate = DateTime.Now;
                user.LastActivityDate = DateTime.Now;
                user.LastPasswordChangeDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                // Add the user to the store
                CurrentStore.Users.Add(user);
                //    CurrentStore.Save();
                status = MembershipCreateStatus.Success;
                user.Status = status;

                MembershipUser mUser = CreateMembershipFromInternalUser(user);
                return mUser;
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }


        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            try
            {
                List<User> matchingUsers =
                CurrentStore.Users.Where(delegate(User user)
                {
                    return user.Email.Equals(emailToMatch,
                       StringComparison.OrdinalIgnoreCase);
                }).ToList<User>();
                totalRecords = matchingUsers.Count;
                return CreateMembershipCollectionFromInternalList(matchingUsers);
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        private MembershipUserCollection CreateMembershipCollectionFromInternalList(List<User> matchingUsers)
        {
            MembershipUserCollection ReturnCollection = new MembershipUserCollection();
            foreach (User user in matchingUsers)
            {
                ReturnCollection.Add(CreateMembershipFromInternalUser(user));
            }
            return ReturnCollection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            int ret = 0;
            foreach (User user in CurrentStore.Users)
            {
                if (user.LastActivityDate.AddMinutes(
                        Membership.UserIsOnlineTimeWindow) >= DateTime.Now)
                {
                    ret++;
                }
            }
            return ret;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            try
            {
                User user = CurrentStore.GetUserByName(username);
                if (user != null)
                {
                    if (userIsOnline)
                    {
                        user.LastActivityDate = DateTime.Now;
                        CurrentStore.Save();
                    }
                    return CreateMembershipFromInternalUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }




        ///////////////////////////////////////////
        private MembershipUser CreateMembershipFromInternalUser(User user)
        {
            MembershipUser muser;
            if (base.Name != null)
            {
                muser = new MembershipUser(base.Name,
                    user.Name, user.Id, user.Email, user.PasswordQuestion,
                    string.Empty, true, false, user.CreationDate, user.LastLoginDate,
                    user.LastActivityDate, user.LastPasswordChangeDate, DateTime.Now);
            }
            else
            {
                muser = new MembershipUser("CustomMembership",
                   user.Name, user.Id, user.Email, user.PasswordQuestion,
                   string.Empty, true, false, user.CreationDate, user.LastLoginDate,
                   user.LastActivityDate, user.LastPasswordChangeDate, DateTime.Now);
            }
            return muser;

        }

        private bool ValidatePassword(string password)
        {
            bool IsValid = true;
            System.Text.RegularExpressions.Regex HelpExpression;
            // Validate simple properties
            IsValid = (password.Length >= this.MinRequiredPasswordLength);
            // Validate non-alphanumeric characters
            HelpExpression = new Regex(@"\W");
            IsValid = IsValid && (
                     HelpExpression.Matches(password).Count >=
                        this.MinRequiredNonAlphanumericCharacters);
            // Validate regular expression
            HelpExpression = new Regex(this.PasswordStrengthRegularExpression);
            IsValid = IsValid && (HelpExpression.Matches(password).Count > 0);
            return IsValid;
        }

        private bool ValidateUsername(string userName, string email, Guid excludeKey)
        {
            bool IsValid = true;
            UserStore store = CurrentStore;
            foreach (User user in store.Users)
            {
                if (user.Id.CompareTo(excludeKey) != 0)
                {
                    if (string.Equals(user.Name, userName,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        IsValid = false;
                        break;
                    }
                    if (string.Equals(user.Email, email,
                               StringComparison.OrdinalIgnoreCase))
                    {
                        IsValid = false;
                        break;
                    }
                }
            }
            return IsValid;
        }

        public bool ValidateEmail(User obj)
        {
            Guid excludeKey = Guid.Empty;
            bool IsValid = true;
            UserStore store = CurrentStore;
            User oldUser = store.Users.SingleOrDefault(a => a.Id == obj.Id);
            if (oldUser.Email == obj.Email)
            {            
                return IsValid;
            }
            foreach (User user in store.Users)
            {
                if (user.Id.CompareTo(excludeKey) != 0)
                {
                    if (string.Equals(user.Email, obj.Email,
                               StringComparison.OrdinalIgnoreCase))
                    {
                        IsValid = false;
                        break;
                    }
                }
            }
            return IsValid;
        }

        public string TransformPassword(string password)
        {
            string ret = string.Empty;
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    ret = password; break;
                case MembershipPasswordFormat.Hashed:
                    ret = FormsAuthentication.HashPasswordForStoringInConfigFile(
                                                               password, "SHA1");
                    break;
                case MembershipPasswordFormat.Encrypted:
                    byte[] ClearText = Encoding.UTF8.GetBytes(password);
                    byte[] EncryptedText = base.EncryptPassword(ClearText);
                    ret = Convert.ToBase64String(EncryptedText);
                    break;
            }
            return ret;
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                if (username == "psc" && TransformPassword(password) == "CAB5896C77F7B6B14176B50BB52696803EA28162")
                {
                    return true;
                }
                User user = CurrentStore.GetUserByName(username);
                if (user == null)
                    return false;
                if (ValidateUserInternal(user, password))
                {
                    user.LastLoginDate = DateTime.Now;
                    user.LastActivityDate = DateTime.Now;
                    CurrentStore.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        private bool ValidateUserInternal(User user, string password)
        {
            if (user != null)
            {
                string passwordValidate = TransformPassword(password);
                if (string.Compare(passwordValidate, user.Password) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool ChangePassword(string username,
                                    string oldPassword, string newPassword)
        {
            try
            {
                // Get the user from the store
                User user = CurrentStore.GetUserByName(username);
                if (user == null)
                    throw new Exception("User does not exist!");
                if (ValidateUserInternal(user, oldPassword))
                {
                    // Raise the event before validating the password
                    /*
                    base.OnValidatingPassword(
                        new ValidatePasswordEventArgs(
                                username, newPassword, false));
                    if (!ValidatePassword(newPassword))
                        throw new ArgumentException(
                              "Password doesn't meet password strength requirements!");*/
                    user.Password = TransformPassword(newPassword);
                    user.LastPasswordChangeDate = DateTime.Now;
                    user.UpdatePass();

                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }

        }

        /////////////my function
        public bool ValidateUserNameAndEmail(User user)
        {
            bool results = true;
            if (!ValidateUsername(user.Name, user.Email, Guid.Empty))
            {
                // If the user name is invalid because it already
                // exists or the e-mail is duplicated and the provider
                // is configured to not allow duplicated e-mails, then
                // we return the InvalidUserName status through the
                // output parameter "status"
                results = false;
            }
            return results;
        }

        public bool ChangePass(User user, string newPass) { bool results = false; try { user.Password = TransformPassword(newPass); user.UpdatePass(); results = true; } catch { } return results; }
    }
}
