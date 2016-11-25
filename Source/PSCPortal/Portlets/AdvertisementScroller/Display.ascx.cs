using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.AdvertisementScroller
{
    public partial class Display : Engine.PortletControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string arrtopicLinks = "['";
            string arrImages = "[";
            if (!Directory.Exists(Server.MapPath("~/Resources/Images/Advertisement/") + Portlet.PortletInstance.Id))
            {
                Directory.CreateDirectory(Server.MapPath("~/Resources/Images/Advertisement/") + Portlet.PortletInstance.Id);
            }
            
            string obsoluteUrl = Server.MapPath("~/Resources/Images/Advertisement/" + Portlet.PortletInstance.Id);

            string[] files = Directory.GetFiles(obsoluteUrl);
            List<string> fileNames = new List<string>();
           
            foreach (string image in files)
            {
                int plashIndex = image.LastIndexOf("\\", StringComparison.Ordinal);
                string fileName =image.Substring(plashIndex + 1);
                string imageLink="Resources/Images/Advertisement/" + Portlet.PortletInstance.Id+"/"+fileName;
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand com = new SqlCommand {Connection = con};
                    con.Open();
                    com.Parameters.AddWithValue("@DataId", Portlet.PortletInstance.Id);
                    com.Parameters.AddWithValue("@Image", imageLink);
                    com.CommandText = "Select * from Advertisement where DataId=@DataId and Image=@Image";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string link = reader["Link"].ToString();
                            string img = reader["Image"].ToString();                            
                            fileNames.Add(img);                            
                            arrtopicLinks += link+ "'";
                        }
                    }
                }
                arrtopicLinks += ",'";
            }
            for (int i = 0; i < fileNames.Count; i++)
            {                
                if (i == 0)
                {
                    arrImages += "'" + fileNames[i] + "'";
                }
                else
                {
                    arrImages += ",'" + fileNames[i] + "'";
                }
            }

            int startIndex = arrtopicLinks.IndexOf("[", StringComparison.Ordinal);
            int linkIndex = arrtopicLinks.LastIndexOf(",'", StringComparison.Ordinal);
            arrtopicLinks = linkIndex > 0 ? arrtopicLinks.Substring(startIndex, linkIndex - startIndex - 1) : string.Empty;
            arrtopicLinks += "']";
            arrImages += "]";
            string key = Guid.NewGuid().ToString();
            if (!Page.ClientScript.IsStartupScriptRegistered("ImageScroller"))
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ImageScroller", "<script src='/Portlets/AdvertisementScroller/Scripts/crawler_vertical.js' type='text/javascript' language='javascript'></script>");
            string script = "<script type='text/javascript'> LoadImages('" + vMarquee.ClientID + "'," + arrImages + ","+ arrtopicLinks + "); "+"  marqueeInit({uniqueid: '" + vMarquee.ClientID + "',style: {'width': '210px','height': '100px'},inc: 1, mouse: 'cursor driven', moveatleast: 1,neutral: 150,savedirection: true}); </script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), key, script);
 
        }

        protected override void DeleteData()
        {

        }
    }
}