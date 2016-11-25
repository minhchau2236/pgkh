using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Framework.Helpler;
using PSCPortal.Engine;

namespace PSCPortal.Portlets.Popup
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
            if (!IsPostBack)
                LoadCustomEditor();
        }
        protected void LoadData()
        {
            RadEditor1.Content = HTMLBlogContent;
        }
        protected void LoadCustomEditor()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId == Guid.Empty))
            {
                SubDomain subdomain =
                    SubDomainCollection.GetSubDomainCollection().SingleOrDefault(sub => sub.Id == subId);
                if (subdomain == null) // homepage
                {
                    Libs.Ultility.SettingEditor(RadEditor1, "");
                }
                else
                {
                    Libs.Ultility.SettingEditor(RadEditor1, subdomain.Name);
                }
            }
        }
        protected void lbtAccept_Click(object sender, EventArgs e)
        {
            string txt = "<select onchange='if (this.value!=0)window.open(this.value);' size='1'>";
            txt = txt + "<option selected='selected'>Website</option>";

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
                #region HTMLBlogContent
                DbParameter prHTMLBlogContent = database.GetParameter();
                prHTMLBlogContent.DbType = System.Data.DbType.String;
                prHTMLBlogContent.Direction = System.Data.ParameterDirection.InputOutput;
                prHTMLBlogContent.ParameterName = "@HTMLBlogContent";
                //prHTMLBlogContent.Value = txt+ RadEditor1.Content+ "</select>'";
                prHTMLBlogContent.Value = RadEditor1.Content;
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
                connection.Open();
                command.ExecuteNonQuery();
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