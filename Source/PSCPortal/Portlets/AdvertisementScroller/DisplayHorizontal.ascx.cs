using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;


namespace PSCPortal.Portlets.AdvertisementScroller
{
    public partial class DisplayHorizontal : PSCPortal.Engine.PortletControl
    {      
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }
        protected void LoadData()
        {

            List<string> list = new List<string>();
            string link = "";
            string img = "";
            if (!Directory.Exists(Server.MapPath("~/Resources/Images/Advertisement/") + Portlet.PortletInstance.Id))
            {
                Directory.CreateDirectory(Server.MapPath("~/Resources/Images/Advertisement/") + Portlet.PortletInstance.Id);
            }

            string obsoluteUrl = Server.MapPath("~/Resources/Images/Advertisement/" + Portlet.PortletInstance.Id);

            string[] files = Directory.GetFiles(obsoluteUrl);
            List<string> fileNames = new List<string>();
           // List<ImageObj> list1 = new List<ImageObj>();
            foreach (string image in files)
            {
                int plashIndex = image.LastIndexOf("\\");
                string fileName = image.Substring(plashIndex + 1);
                int dotIndex = fileName.LastIndexOf('.');
                string extension = fileName.Substring(dotIndex + 1).ToLower();
                if (extension == "jpg" || extension == "jpeg" || extension == "gif" || extension == "png")
                {
                    fileNames.Add(fileName);
                }
                string imgLink="Resources/Images/Advertisement/" + Portlet.PortletInstance.Id+"/"+fileName;
                /////////////////
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    com.Parameters.AddWithValue("@DataId", Portlet.PortletInstance.Id);
                    com.Parameters.AddWithValue("@Image", imgLink);
                    com.CommandText = "Select * from Advertisement where DataId=@DataId and Image=@Image";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            link = reader["Link"].ToString();
                            img = reader["Image"].ToString();

                            list.Add("<a target='_blank' href='" + link + "'><img class='scroll' src='" + img + "' /></a>");                           
                        }
                    }
                }
            }
            radRotator.DataSource = list;
            radRotator.DataBind();
        }
        protected override void DeleteData()
        {

        }
    }
}