using System;
using System.Collections.Generic;
using System.Linq;
using PSCPortal.CMS;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace PSCPortal.Portlets.Media
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }

        public string ResultList
        {
            get;
            set;
        }

        protected void LoadData()
        {
            VideoClipCollection Top4VideoClip = VideoClipCollection.GetTop4VideoClipCollection();
            MusicCollection Top10Music = MusicCollection.GetTop10MusicClipCollection();

            var js = new JavaScriptSerializer();

            Dictionary<string, object> result = new Dictionary<string, object>();
            result["videos"] = Top4VideoClip;
            result["musics"] = Top10Music;

            ResultList = js.Serialize(result);
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand { Connection = con };
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletTabTopicDisplay Where DataId=@dataId";
                com.ExecuteNonQuery();
            }
        }
    }
}