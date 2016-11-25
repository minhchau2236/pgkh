using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using PSCPortal.Framework.Helpler;
using PSCPortal.Libs;
using Infragistics.Excel;
using System.IO;

namespace PSCPortal.Systems.CMS
{
    [Serializable]
    public class VisitorSubDomain
    {
        public string Title { get; set; }
        public int ViewTime { get; set; }
    }
    public partial class VisitorViewTimeReport : PSCPortal.Framework.PSCPage
    {
        protected static List<VisitorSubDomain> VisitorSubDomainList
        {
            get
            {
                return DataStatic["VisitorSubDomainList"] as List<VisitorSubDomain>;
            }
            set
            {
                DataStatic["VisitorSubDomainList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static object Search(string tungay, string denngay, int startIndex, int maximumRows, string sortExpressions)
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            string query = string.Empty;
            query = @"SELECT sb.[Description] as Title ,COUNT([SubDomainId]) as ViewTime  FROM [Log] l right join SubDomain sb on l.[SubDomainId]=sb.Id where 1=1";
            query += " and l.[SubDomainId] is not null";
            if (!(subId == Guid.Empty))
            {
                query += " and [SubDomainId] ='" + subId + "'";
            }
            if (tungay != string.Empty && denngay != string.Empty)
            {
                DateTime dateStart = DateTime.Parse(tungay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                denngay += " 23:59:59 PM";
                DateTime dateEnd = DateTime.Parse(denngay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                query += " and l.LogTime>='" + dateStart + "'";
                query += " and l.LogTime<= '" + dateEnd + "'";
            }
            query += " group by l.[SubDomainId],sb.[Description]";
            query += " order by ViewTime Desc";
            VisitorSubDomainList = new List<VisitorSubDomain>();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.CommandText = string.Format(query);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = con;
                con.Open();
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VisitorSubDomain itemp = new VisitorSubDomain();
                    itemp.Title = (string)reader["Title"];
                    itemp.ViewTime = (int)reader["ViewTime"];
                    VisitorSubDomainList.Add(itemp);
                }
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                result["Data"] = IEnumerableExtentionMethods.GetSegmentList(VisitorSubDomainList, startIndex, maximumRows, sortExpressions);
                result["Count"] = VisitorSubDomainList.Count();

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [System.Web.Services.WebMethod]
        public static object VisitorLoadCommand(int startIndex, int maximumRows, string sortExpressions)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["Data"] = IEnumerableExtentionMethods.GetSegmentList(VisitorSubDomainList, startIndex, maximumRows, sortExpressions);
            result["Count"] = VisitorSubDomainList.Count();
            return result;
        }        
    }
}