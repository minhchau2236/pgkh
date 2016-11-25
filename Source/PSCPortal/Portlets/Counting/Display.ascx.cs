using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using PSCPortal.Engine;

namespace PSCPortal.Portlets.Counting
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string subName = string.Empty;
            if (Request.Url.Host.IndexOf("www") > -1)
                subName = Request.Url.Host.Replace(ConfigurationManager.AppSettings["MainDomainName"], "");
            else
                subName = Request.Url.Host.Replace(ConfigurationManager.AppSettings["DomainName"], "");
            if (subName.Length > 0)
                subName = subName.Substring(0, subName.Length - 1);
            SubDomain subDomain = subName == string.Empty ? SubDomain.GetSubByName("HomePage") : SubDomain.GetSubByName(subName);
            if (subDomain == null)
                return;
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                SqlParameter prParameter = new SqlParameter("@SubDomainId", SqlDbType.UniqueIdentifier);
                prParameter.Value = subDomain.Id;
                com.Parameters.Add(prParameter);
                com.CommandType = System.Data.CommandType.Text;
                com.CommandText = "select count(*) from Log Where SubDomainId = @SubDomainId";
                string sl = com.ExecuteScalar().ToString();
                int soluongtruycap = Int32.Parse(sl) + subDomain.OldVisitors;
                Label1.Text = String.Format("{0:0,0}", soluongtruycap);
                // Label2.Text = Application["online"].ToString();
                //////////
                SqlCommand com1 = new SqlCommand();
                com1.Connection = con;
                com1.CommandType = System.Data.CommandType.Text;
                com1.Parameters.AddWithValue("@LogTime", DateTime.Now.Day);
                com1.CommandText = "select count(*) from Log where DAY(LogTime) = @LogTime AND SubDomainId = @SubDomainId";
                // Label3.Text= com1.ExecuteScalar().ToString();
                ////////
                //////////
                SqlCommand com2 = new SqlCommand();
                com2.Connection = con;
                com2.CommandType = System.Data.CommandType.Text;
                com2.Parameters.AddWithValue("@LogTime", DateTime.Now.Month);
                SqlParameter prSubDomain = new SqlParameter("@SubDomainId", SqlDbType.UniqueIdentifier);
                prSubDomain.Value = subDomain.Id;
                com2.Parameters.Add(prSubDomain);
                com2.CommandText = "select count(*) from Log where MONTH(LogTime) = @LogTime AND SubDomainId = @SubDomainId";
                string count = com2.ExecuteScalar().ToString();
                int truycapthang = Int32.Parse(count);
                Label4.Text = String.Format("{0:0,0}", truycapthang);

            }
        }

        protected override void DeleteData()
        {
        }
    }
}