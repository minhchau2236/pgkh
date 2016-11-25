using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Engine;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Security;
using PSCPortal.Systems.Security;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Portlets.Mobile.HTMLBlog
{
    public partial class Edit : PSCPortal.Engine.PortletEditControl
    {
        protected string HTMLBlogContent
        {
            get
            {
                if (DataSelf["HTMLBlogContent"] == null)
                    DataSelf["HTMLBlogContent"] = GetHTMLBlogContent();
                return DataSelf["HTMLBlogContent"] as string;
            }
        }

        //protected RoleCollection RoleList
        //{
        //    get
        //    {
        //        if (ViewState["RoleList"] == null)
        //        {
        //            ViewState["RoleList"] = PSCPortal.Security.RoleCollection.GetRoleCollection(
        //                    System.Web.HttpContext.Current.User.Identity.Name);
        //        }
        //        return ViewState["RoleList"] as RoleCollection;
        //    }
        //    set
        //    {
        //        ViewState["RoleList"] = value;
        //    }
        //}
        protected string Username
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
            if (!IsPostBack)
                LoadCustomEditor();
        }
        protected void LoadCustomEditor()
        {
            string subId = SessionHelper.GetSession(SessionKey.SubDomain);

            if (!subId.Equals(Guid.Empty.ToString()))
            {
                SubDomain subdomain = SubDomainCollection.GetSubDomainCollection().SingleOrDefault(sub => sub.Id == new Guid(subId));
                if (subdomain == null) // homepage
                {
                    Libs.Ultility.SettingEditor(redHTMLBlog, "");
                }
                else
                {
                    Libs.Ultility.SettingEditor(redHTMLBlog, subdomain.Name);
                }
            }
        }
        protected void LoadData()
        {
            redHTMLBlog.Content = HTMLBlogContent;
        }

        protected void lbtAccept_Click(object sender, EventArgs e)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                User user = UserCollection.GetUserCollection().SingleOrDefault(a => a.Name == Username);
                DbCommand commandLog = database.GetCommand(connection);
                #region LogId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", Guid.NewGuid());
                commandLog.Parameters.Add(prId);
                #endregion
                #region LogIp
                DbParameter prIp = database.GetParameter(System.Data.DbType.String, "@Ip", Request.UserHostAddress);
                commandLog.Parameters.Add(prIp);
                #endregion
                #region UserId
                DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", user == null ? Guid.Empty : user.Id);
                commandLog.Parameters.Add(prUserId);
                #endregion
                #region OldData
                DbParameter prOldData = database.GetParameter(System.Data.DbType.String, "@OldData", "");
                commandLog.Parameters.Add(prOldData);
                #endregion
                #region NewData
                DbParameter prNewData = database.GetParameter(System.Data.DbType.String, "@NewData", redHTMLBlog.Content);
                commandLog.Parameters.Add(prNewData);
                #endregion
                #region ObjectId
                DbParameter prObjectId = database.GetParameter(System.Data.DbType.Guid, "@ObjectId", DataId);
                commandLog.Parameters.Add(prObjectId);
                #endregion
                #region ObjectType
                DbParameter prObjectType = database.GetParameter(System.Data.DbType.String, "@ObjectType", "HTMLBlog");
                commandLog.Parameters.Add(prObjectType);
                #endregion
                #region ModifiedDate
                DbParameter prModifiedDate = database.GetParameter(System.Data.DbType.DateTime, "@ModifiedDate", DateTime.Now);
                commandLog.Parameters.Add(prModifiedDate);
                #endregion
                commandLog.CommandText = "IF (EXISTS(SELECT 1 FROM HTMLBlog WHERE DataId=@ObjectId)) ";
                commandLog.CommandText += "BEGIN ";
                commandLog.CommandText += "set @OldData = (select HTMLBlogContent from HTMLBlog where DataId=@ObjectId) ";
                commandLog.CommandText += "INSERT INTO LogData (Id,Ip,UserId,OldData,NewData,ObjectId,ObjectType,ModifiedDate) VALUES (@Id, @Ip,@UserId,@OldData,@NewData,@ObjectId,@ObjectType,@ModifiedDate) ";
                commandLog.CommandText += "END ";
                commandLog.CommandText += "ELSE ";
                commandLog.CommandText += "BEGIN ";
                commandLog.CommandText += "INSERT INTO LogData (Id,Ip,UserId,OldData,NewData,ObjectId,ObjectType,ModifiedDate) VALUES (@Id, @Ip,@UserId,NULL,@NewData,@ObjectId,@ObjectType,@ModifiedDate) ";
                commandLog.CommandText += "END ";
                commandLog.ExecuteNonQuery();

                DbCommand command = database.GetCommand(connection);
                #region DataId
                DbParameter prDataId = database.GetParameter();
                prDataId.DbType = System.Data.DbType.Guid;
                prDataId.Direction = System.Data.ParameterDirection.InputOutput;
                prDataId.ParameterName = "@DataId";
                prDataId.Value = DataId;
                command.Parameters.Add(prDataId);
                #endregion
                #region HTMLBlogContent
                DbParameter prHTMLBlogContent = database.GetParameter();
                prHTMLBlogContent.DbType = System.Data.DbType.String;
                prHTMLBlogContent.Direction = System.Data.ParameterDirection.InputOutput;
                prHTMLBlogContent.ParameterName = "@HTMLBlogContent";
                prHTMLBlogContent.Value = redHTMLBlog.Content;
                command.Parameters.Add(prHTMLBlogContent);
                #endregion
                command.CommandText = "IF (EXISTS(SELECT 1 FROM HTMLBlog WHERE DataId=@DataId)) ";
                command.CommandText += "BEGIN ";
                command.CommandText += "UPDATE HTMLBlog SET HTMLBlogContent=@HTMLBlogContent WHERE DataId=@DataId ";
                command.CommandText += "END ";
                command.CommandText += "ELSE ";
                command.CommandText += "BEGIN ";
                command.CommandText += "INSERT INTO HTMLBlog (DataId, HTMLBlogContent) VALUES (@DataId, @HTMLBlogContent) ";
                command.CommandText += "END ";
                command.ExecuteNonQuery();
                connection.Close();
            }
            Accept();
        }

        protected void lbtCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        protected string GetHTMLBlogContent()
        {
            string result = string.Empty;
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region DataId
                DbParameter prDataId = database.GetParameter();
                prDataId.DbType = System.Data.DbType.Guid;
                prDataId.Direction = System.Data.ParameterDirection.InputOutput;
                prDataId.ParameterName = "@DataId";
                prDataId.Value = DataId;
                command.Parameters.Add(prDataId);
                #endregion
                command.CommandText = "SELECT HTMLBlogContent FROM HTMLBlog WHERE DataId=@DataId";
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = (string)reader["HTMLBlogContent"];
            }
            return result;
        }
    }
}