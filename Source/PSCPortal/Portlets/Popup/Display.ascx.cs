using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.Portlets.Popup
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }
        protected void LoadData()
        {

            
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
                prDataId.Value = Portlet.PortletInstance.Id;
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
        protected override void DeleteData()
        {
        }
    }
}