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
using System.Net.Mail;
namespace PSCPortal.CMS
{
    public partial class SendArticle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string nameSender = txtSenderName.Text;
                string emailSender = txtSenderEmail.Text;
                string emailReceipt = txtReceiptEmail.Text;
                string tieude = txtTitle.Text;
                string loinhan = txtMessage.Text;
                SendMail(nameSender, emailSender, emailReceipt, tieude, loinhan);
                ClearEditor();
            }
            catch (Exception ex)
            {
                lbthongbao.Text = "Gởi không thành công.";
            }
            lbthongbao.Visible = true;
            string key=System.Guid.NewGuid().ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), key, "<script type='text/javascript'>window.close();alert('Gởi thành công');</script>");
        }
       
        protected void SendMail(string nameSender, string emailSender, string emailReceipts, string tieude, string loinhan)
        {
           // string url = System.Configuration.ConfigurationManager.AppSettings["URL"].ToString();
            string url = "http://" + Request.Url.Host;
            string link=HiddenField1.Value.ToString();
            try
            {
                string body ="Chào bạn!<br/>Bạn nhận được một tin nhắn từ "+nameSender+ "(" + emailSender + ") mời bạn đọc bài viết trên trang <a href='"+url+"'>"+url+"</a><br/>"+"Tiêu đề : " + tieude+"<br/>";
                body += "Tại đường dẫn: <a href='"+link+"'>" +link+ "</a><br/>Với lời nhắn: " + loinhan;
                string[] diachiEmail = emailReceipts.Split(new char[] { ';' });
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
                    oMsg.Subject = tieude;
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
                    lbthongbao.Text = "Gởi thành công.";
                    oMsg = null;
                }
            }
            catch (Exception ex)
            {

                try
                {
                    string body = "Chào bạn!<br/>Bạn nhận được một tin nhắn từ " + nameSender + "(" + emailSender + ") mời bạn đọc bài viết trên trang <a href='" + url + "'>" + url + "</a><br/>" + "Tiêu đề : " + tieude + "<br/>";
                    body += "Tại đường dẫn: <a href='" + link + "'>" + link + "</a><br/>Với lời nhắn: " + loinhan;
                    string[] diachiEmail = emailReceipts.Split(new char[] { ';' });
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
                        oMsg.Subject = tieude;
                        oMsg.Priority = MailPriority.High;
                        oMsg.IsBodyHtml = true;
                        oMsg.Body = body;
                        oMsg.Sender = new MailAddress(mailFrom); ;

                        SmtpClient smtp = new SmtpClient();
                        smtp.EnableSsl = true;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 465;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(usename, pass);

                        smtp.Send(oMsg);
                        lbthongbao.Text = "Gởi thành công.";
                        oMsg = null;
                    }
                }
                catch (Exception ex1)
                {
                    lbthongbao.Text = ex1.ToString();
                }
              
            }
            
        }
        private void ClearEditor()
        {
            txtSenderName.Text = string.Empty;
            txtSenderEmail.Text = string.Empty;
            txtReceiptEmail.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtMessage.Text = string.Empty;
            lbthongbao.Visible = false;
        }
    }
}
