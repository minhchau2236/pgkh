using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;
using System.Data.SqlClient;
using PSCPortal.Engine;
using System.Web.Routing;
using System.Net.Mail;

namespace PSCPortal
{
    public class Global : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("Topic", "vi/{TopicTitle}/{TopicId1}", "~/Default.aspx");
            routes.MapPageRoute("Article", "vi/{TopicTitle}/{ArticleTitle}/{Id}", "~/Default.aspx");
            routes.MapPageRoute("Module1", "module/{ModuleTitle}/{ModuleId}", "~/Default.aspx");
            routes.MapPageRoute("Module", "module/{ModuleTitle}/{ModuleId}/{TopicId2}", "~/Default.aspx");
            routes.MapPageRoute("Page", "PageId/{*PageId}", "~/Default.aspx");
            //routes.MapPageRoute("Module", "ModuleId/{*ModuleId}", "~/Default.aspx");
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["online"] = 0;
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string subName = Libs.Ultility.GetSubDomain();
            SubDomain subDomain = subName == string.Empty ? SubDomain.GetSubByName("HomePage") : SubDomain.GetSubByName(subName);
            if (subDomain == null)
                return;
            Application["online"] = int.Parse(Application["online"].ToString()) + 1;
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@LogIp", Request.UserHostAddress);
                command.Parameters.AddWithValue("@LogTime", DateTime.Now);
                command.Parameters.AddWithValue("@SubDomainId", subDomain.Id);
                command.CommandText = "insert into Log (LogIp, LogTime, SubDomainId) values(@LogIp, @LogTime, @SubDomainId)";
                command.ExecuteNonQuery();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Send_Report(string url, string error)
        {

            try
            {
                string body = "Website : " + url + "<br/> <br/>" + "Error: " + error;
                string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceiptError"].Split(new char[] { ';' });
                MailAddressCollection mailCollection = new MailAddressCollection();
                for (int i = 0; i < diachiEmail.Length; i++)
                {
                    string emailReceipt = diachiEmail[i];
                    mailCollection.Add(new MailAddress(emailReceipt));
                }
                if (mailCollection.Count > 0)
                {
                    string[] mailSenderData = System.Configuration.ConfigurationManager.AppSettings["MailSender"].ToString().Split(new char[] { ';' });
                    string mailFrom = mailSenderData[0];
                    string usename = mailSenderData[1];
                    string pass = mailSenderData[2];
                    MailMessage oMsg = new MailMessage();
                    oMsg.From = new MailAddress(mailFrom);
                    oMsg.To.Add(mailCollection[0]);
                    //bo di email dau tien
                    for (int i = 1; i < mailCollection.Count; i++)
                        oMsg.CC.Add(mailCollection[i]);
                    oMsg.Subject = url;
                    oMsg.Priority = MailPriority.High;
                    oMsg.IsBodyHtml = true;
                    oMsg.Body = body;
                    oMsg.Sender = new MailAddress(mailFrom); ;

                    SmtpClient smtp = new SmtpClient();
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(usename, pass);

                    smtp.Send(oMsg);
                    oMsg = null;
                }
            }
            catch (Exception ex)
            {

                try
                {
                    string body = "Website : " + url + "<br/> <br/>" + "Error: " + error;
                    string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceiptError"].Split(new char[] { ';' });
                    MailAddressCollection mailCollection = new MailAddressCollection();
                    for (int i = 0; i < diachiEmail.Length; i++)
                    {
                        string emailReceipt = diachiEmail[i];
                        mailCollection.Add(new MailAddress(emailReceipt));
                    }
                    if (mailCollection.Count > 0)
                    {
                        string[] mailSenderData = System.Configuration.ConfigurationManager.AppSettings["MailSender"].ToString().Split(new char[] { ';' });
                        string mailFrom = mailSenderData[0];
                        string usename = mailSenderData[1];
                        string pass = mailSenderData[2];
                        MailMessage oMsg = new MailMessage();
                        oMsg.From = new MailAddress(mailFrom);
                        oMsg.To.Add(mailCollection[0]);
                        //bo di email dau tien
                        for (int i = 1; i < mailCollection.Count; i++)
                            oMsg.CC.Add(mailCollection[i]);
                        oMsg.Subject = url;
                        oMsg.Priority = MailPriority.High;
                        oMsg.IsBodyHtml = true;
                        oMsg.Body = body;
                        oMsg.Sender = new MailAddress(mailFrom);

                        SmtpClient smtp = new SmtpClient();
                        smtp.EnableSsl = true;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 465;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(usename, pass);

                        smtp.Send(oMsg);
                        oMsg = null;
                    }
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message);
                }
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        { 
            if (base.Context.Error.InnerException != null)
            {
                string error = "Ip: " + Request.UserHostAddress + base.Context.Error.Message + "<br/>" +
                               base.Context.Error.InnerException.Message;
                string url = base.Context.Request.Url.OriginalString;
                Send_Report(url, error);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["online"] = int.Parse(Application["online"].ToString()) - 1;
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}