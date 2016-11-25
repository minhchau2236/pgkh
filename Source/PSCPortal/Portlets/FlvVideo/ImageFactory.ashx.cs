using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.FlvVideo
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageFactory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] result = null;
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandType = System.Data.CommandType.Text;

                com.CommandText = "select [picture] from ClipImage where [clipnewid]= '" + context.Request.QueryString["img"] .ToString() + "'";
                com.Connection = con;
                SqlDataReader reader = com.ExecuteReader();
                
                while (reader.Read())
                {
                    result = (byte[])reader["picture"];
                }
            }

            context.Response.ContentType = "image/JPEG";
            context.Response.BinaryWrite(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
