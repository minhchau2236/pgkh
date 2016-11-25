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
using System.IO;
using PSCPortal.CMS;

namespace PSCPortal.Modules.CMS
{
    public partial class FeedBack : System.Web.UI.UserControl
    {
        public Topic Topic
        {
            get { return ViewState["Topic"] as Topic; }
            set { ViewState["Topic"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
            DataBind();
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (fckContent.Content == string.Empty)
            {
                lbthongbao.Text = "Nội dung không được rỗng.";
                lbthongbao.Visible = false;
                return;
            }
            string url = string.Empty;
            if (fileUpLoad.PostedFile.ContentLength > (1024 * 1024 * 4))
            {
                lbthongbao.Text = "File gởi không được lớn hơn 4 M.";
                lbthongbao.Visible = true;
                return;
            }
            try
            {
                string _path = "~/Resources/Temp/";
                string _fullfile = fileUpLoad.PostedFile.FileName;
                string _filename = Path.GetFileName(_fullfile);
                if (_filename != string.Empty)
                {
                    fileUpLoad.PostedFile.SaveAs(Server.MapPath(_path + _filename));
                    url = Server.MapPath(_path + _filename);
                    string nameSender = txtFullName.Text;
                    string emailSender = txtEmail.Text;
                    string phone = txtPhone.Text;
                    string title = txtTitle.Text;
                    string address = txtAddress.Text;
                    string content = fckContent.Content;
                    SendMail(nameSender, emailSender, phone, address, title, url, content);
                    File.Delete(url);
                }
                else
                {
                    SendMail(txtFullName.Text, txtEmail.Text, txtPhone.Text, txtAddress.Text, txtTitle.Text, url, fckContent.Content);
                }
                ClearEditor();
            }
            catch (Exception ex)
            {
                lbthongbao.Text = "Gởi không thành công.";
            }
            lbthongbao.Visible = true;
        }
        private void ClearEditor()
        {
            txtFullName.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            fckContent.Content = string.Empty;
            lbthongbao.Visible = false;
        }
        protected void SendMail(string nameSender, string emailSender, string dienthoai, string diachi, string tieude, string attachment, string noidung)
        {
            Attachment fileAttachment = null;
            try
            {
                string body = "Người gửi: " + nameSender + "<br/>" + "email: " + emailSender + "<br/>" + "điện thoai: " + dienthoai + "<br/>" + "địa chỉ: " + diachi + "<br/>" + "tiêu đề : " + tieude;
                body += "<br/> <br/>" + noidung;
                string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceipt"].Split(new char[] { ';' });
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
                    if (attachment != string.Empty)
                    {
                        fileAttachment = new Attachment(attachment);
                        oMsg.Attachments.Add(fileAttachment);
                    }
                    oMsg.To.Add(mailCollection[0]);//add email nhận đầu tiên vào trứơc
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
                    string body = "Người gửi: " + nameSender + "<br/>" + "email: " + emailSender + "<br/>" + "điện thoai: " + dienthoai + "<br/>" + "địa chỉ: " + diachi + "<br/>" + "tiêu đề : " + tieude;
                    body += "<br/> <br/>" + noidung;
                    string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceipt_GopYHienPhap"].Split(new char[] { ';' });
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
                        if (attachment != string.Empty)
                        {
                            fileAttachment = new Attachment(attachment);
                            oMsg.Attachments.Add(fileAttachment);
                        }
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

                        oMsg = null;
                        lbthongbao.Text = "Gởi thành công.";
                    }
                }
                catch (Exception ex1)
                {
                    lbthongbao.Text = ex1.ToString();
                }
                finally
                {
                    if (fileAttachment != null)
                        fileAttachment.Dispose();
                }
            }
            finally
            {
                if (fileAttachment != null)
                    fileAttachment.Dispose();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void LoadData()
        {
            string topicId = System.Configuration.ConfigurationManager.AppSettings["TopicFeedback"];
            try
            {
                Topic = TopicCollection.GetTopic(topicId);
                PSCPortal.CMS.ArticleCollection arList = PSCPortal.CMS.ArticleCollection.GetArticleCollectionPublish(Topic);
                System.Collections.Generic.IEnumerable<PSCPortal.CMS.Article> it = arList.Take(5);
                //RadListView1.DataSource = it;
                //RadListView1.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}