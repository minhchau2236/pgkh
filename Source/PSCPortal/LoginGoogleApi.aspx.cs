using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Security;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OpenId.Extensions.OAuth;
using System.Xml.Linq;
using System.Text;
using PSCPortal.Security;
namespace PSCPortal
{

    public partial class LoginGoogleApi : System.Web.UI.Page
    {
        private static readonly GoogleClient googleClient = new GoogleClient
        {
            ClientIdentifier = ConfigurationManager.AppSettings["googleClientID"],
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationManager.AppSettings["googleClientSecret"]),
        };

        protected void page_load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    IAuthorizationState authorization = googleClient.ProcessUserAuthorization();
                    if (authorization == null)
                    {
                        googleClient.RequestUserAuthorization(scope: new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email", "https://www.googleapis.com/auth/plus.me" });
                        googleClient.RequestUserAuthorization(scope: new[] { GoogleClient.Scopes.UserInfo.Profile, GoogleClient.Scopes.UserInfo.Email });
                    }
                    else
                    {
                        UserCollection list = UserCollection.GetUserCollection();
                        IOAuth2Graph oauth2Graph = googleClient.GetGraph(authorization);
                        string _email = HttpUtility.HtmlEncode(oauth2Graph.Email);
                        User user = list.Single(a => a.Email == _email);
                        FormsAuthentication.SetAuthCookie(user.Name, true);
                        Response.Redirect("~/Systems/Default.aspx");
                    }
                }
                catch (Exception ex) { }
            }
        }

    }
}
