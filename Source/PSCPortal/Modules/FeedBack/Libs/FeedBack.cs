using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.Modules.FeedBack.Libs
{
    [Serializable]
    public class FeedBack
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private Guid _idArticle;
        public Guid IDArticle
        {
            get
            {
                return _idArticle;
            }
            set
            {
                _idArticle = value;
            }
        }

        private string _nameSender = string.Empty;
        public string NameSender
        {
            get
            {
                return _nameSender;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _nameSender = value;
            }
        }

        private string _emailSender = string.Empty;
        public string EmailSender
        {
            get
            {
                return _emailSender;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _emailSender = value;
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _title = value;
            }
        }

        private string _content = string.Empty;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _content = value;
            }
        }        

        private string _contentReply=string.Empty;
        public string ContentReply
        {
            get
            {
                return _contentReply;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _contentReply = value;
            }
        }

        private DateTime _feedBackDate;
        public DateTime FeedBackDate
        {
            get
            {
                return _feedBackDate;
            }
            set
            {
                _feedBackDate = value;
            }
        }

        private Boolean _isPublish = false;
        public Boolean IsPublish
        {
            get
            {
                return _isPublish;
            }
            set
            {
                _isPublish = value;
            }
        }

        public string PathImage
        {
            get
            {
                if (_isPublish)
                {
                    return "Images/choduyet.png";
                }
                else
                {
                    return "Images/boduyet.png";
                }
            }
        }

        public FeedBack()
        {
        }
        internal FeedBack(SqlDataReader reader)
        {
            _id = (int)reader["FeedBackId"];
            _idArticle = (Guid)reader["ArticleId"];
            _nameSender = (string)reader["NameSender"];
            _emailSender = (string)reader["EmailSender"];
            _title = (string)reader["FeedBackTitle"];
            _content = (string)reader["FeedBackContent"];
            _contentReply =(string) reader["FeedBackContentReply"];
            _feedBackDate = (DateTime) reader["FeedBackDate"];
        }

        internal void Insert()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = @"INSERT INTO [dbo].[ArticleFeedBack]
                                            (		
                                                ArticleId,
	                                            NameSender,	
	                                            EmailSender,	
	                                            FeedBackTitle,	
	                                            FeedBackContent,	
	                                            FeedBackContentReply,
	                                            FeedBackDate,
	                                            IsPublish	
                                            )
                                            VALUES
                                            (	
                                                @ArticleId,
	                                            @NameSender,	
	                                            @EmailSender,	
	                                            @FeedBackTitle,	
	                                            @FeedBackContent,
	                                            @FeedBackContentReply,
	                                            @FeedBackDate,
	                                            0	
                                            )

                                            set @FeedBackID=@@identity
                                            ";
                    SqlParameter prFeedBackId = command.CreateParameter();
                    prFeedBackId.ParameterName = "@FeedBackID";
                    prFeedBackId.DbType = System.Data.DbType.Int32;
                    prFeedBackId.Direction = System.Data.ParameterDirection.Output;

                    command.Parameters.Add(prFeedBackId); ;
                    command.Parameters.AddWithValue("@ArticleId", _idArticle);
                    command.Parameters.AddWithValue("@NameSender", _nameSender);
                    command.Parameters.AddWithValue("@EmailSender", _emailSender);
                    command.Parameters.AddWithValue("@FeedBackTitle", _title);
                    command.Parameters.AddWithValue("@FeedBackContent", _content);
                    command.Parameters.AddWithValue("@FeedBackContentReply", _contentReply);
                    command.Parameters.AddWithValue("@FeedBackDate", _feedBackDate);
                    command.ExecuteNonQuery();
                    _id = (int)prFeedBackId.Value;
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
            }
        }
        public void Update()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[ArticleFeedBack]
                                    SET	
                                        ArticleId=@ArticleId,
	                                    NameSender=@NameSender,	
	                                    EmailSender=@EmailSender,	
	                                    FeedBackTitle=@FeedBackTitle,	
	                                    FeedBackContent=@FeedBackContent,
	                                    FeedBackContentReply=@FeedBackContentReply,
	                                    FeedBackDate = @FeedBackDate,
	                                    IsPublish=@IsPublish
                                    WHERE
	                                    FeedBackID=@FeedBackID";
                cmd.Parameters.AddWithValue("@FeedBackID",_id);
                cmd.Parameters.AddWithValue("@ArticleId", _idArticle);
                cmd.Parameters.AddWithValue("@NameSender",_nameSender);
                cmd.Parameters.AddWithValue("@EmailSender", _emailSender);
                cmd.Parameters.AddWithValue("@FeedBackTitle",_title);
                cmd.Parameters.AddWithValue("@FeedBackContent",_content);
                cmd.Parameters.AddWithValue("@FeedBackContentReply",_contentReply);
                cmd.Parameters.AddWithValue("@FeedBackDate",_feedBackDate);
                cmd.Parameters.AddWithValue("@IsPublish", _isPublish);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
                cmd.CommandText = @"DELETE [dbo].[ArticleFeedBack]
                                    WHERE
	                                    FeedBackId=@FeedBackId";
                cmd.Parameters.AddWithValue("@FeedBackID",_id);
                cmd.ExecuteNonQuery();
            }
        }

        internal void Send()
        {

            try
            {
                string body = "Người gửi: " + _nameSender + "<br/>" + "email: " + _emailSender + "<br/>" + "Tiêu đề: " + _title;
                body += "<br/> <br/>" + _content;
                string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceipt"].Split(new char[] { ';' });
                MailAddressCollection mailCollection = new MailAddressCollection();
                for (int i = 0; i < diachiEmail.Length; i++)
                {
                    string email = diachiEmail[i];
                    mailCollection.Add(new MailAddress(email));
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
                    oMsg.Subject = _title;
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
                Console.WriteLine("{0} Exception caught.", ex.ToString());
                try
                {

                    string body = "Người gửi: " + _nameSender + "<br/>" + "email: " + _emailSender + "<br/>" + "Tiêu đề: " + _title;
                    body += "<br/> <br/>" + _content;
                    string[] diachiEmail = System.Configuration.ConfigurationManager.AppSettings["MailReceipt"].Split(new char[] { ';' });
                    MailAddressCollection mailCollection = new MailAddressCollection();
                    for (int i = 0; i < diachiEmail.Length; i++)
                    {
                        string email = diachiEmail[i];
                        mailCollection.Add(new MailAddress(email));
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
                        oMsg.Subject = _title;
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
                    }

                }
                catch (Exception ex1)
                {
                    Console.WriteLine("{0} Exception caught.", ex1.ToString());
                }
            }
        }

        public string GetContent()
        {
            return FeedBack.GetContent(_id);
        }
        public static string GetContent(int id)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region FeedBackId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FeedBackId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"select FeedBackContent from ArticleFeedBack where FeedBackId=@FeedBackId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = (string)reader["FeedBackContent"];
                }
            }
            return result;
        }
        public void UpdateContent(string Content)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region FeedBackId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FeedBackId", _id);
                command.Parameters.Add(prId);
                #endregion
                #region FeedBackContent
                DbParameter prContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContent", Content);
                command.Parameters.Add(prContent);
                #endregion

                command.CommandText = @"UPDATE dbo.[ArticleFeedBack] 
                                    SET [FeedBackContent] = @FeedBackContent 
                                    where [FeedBackId] = @FeedBackId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public string GetContentReplay()
        {
            return FeedBack.GetContentReplay(_id);
        }
        public static string GetContentReplay(int id)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region FeedBackId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FeedBackId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"select FeedBackContentReply from ArticleFeedBack where FeedBackId=@FeedBackId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = (string)reader["FeedBackContentReply"];
                }
            }
            return result;
        }
        public void UpdateContentReplay(string ContentReplay)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region FeedBackId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FeedBackId", _id);
                command.Parameters.Add(prId);
                #endregion
                #region FeedBackContent
                DbParameter prContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContentReply", ContentReplay);
                command.Parameters.Add(prContent);
                #endregion

                command.CommandText = @"UPDATE dbo.[ArticleFeedBack] 
                                    SET [FeedBackContentReply] = @FeedBackContentReply 
                                    where [FeedBackId] = @FeedBackId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(FeedBack) && ((FeedBack)obj)._id == _id)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }

    public delegate void FeedBackDelegate(object sender, FeedBackArgs args);
    [Serializable]
    public class FeedBackArgs : EventArgs
    {
        private FeedBack _feedback;
        public FeedBack FeedBack
        {
            get
            {
                return _feedback;
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }

        private string _content = string.Empty;
        public string FeedBackContent
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        private string _contentreplay = string.Empty;
        public string FeedBackContentReply
        {
            get
            {
                return _contentreplay;
            }
            set
            {
                _contentreplay = value;
            }
        }

        public FeedBackArgs(FeedBack feedback, string content, string contentreplay, bool isEdit)
        {
            _feedback = feedback;
            _isEdit = isEdit;
            _content = content;
            _contentreplay = contentreplay;
        }
        public FeedBackArgs(FeedBack feedback, string contentreplay, bool isEdit)
        {
            _feedback = feedback;
            _isEdit = isEdit;
            _contentreplay = contentreplay;
        }
    }
   
    [Serializable]
    public class FeedBackCollection : List<FeedBack>
    {
        public FeedBackCollection()
            : base()
        {
        }

        public virtual void AddDB(FeedBack item)
        {
            Add(item);
            item.Insert();
        }
        public virtual void RemoveDB(FeedBack item)
        {
            base.Remove(item);
            item.Delete();
        }
        public static FeedBackCollection GetFeedBackOfYKienNguoiDanPublicCollection()
        {
            FeedBackCollection result = new FeedBackCollection();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command =new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"	
                                        SELECT *
	                                        from ArticleFeedBack
	                                        where IsPublish=1 and FeedBackContentReply like ''
                                        ";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new FeedBack(reader));
                    }
                }
            }
            return result;
        }
        public static FeedBackCollection GetFeedBackOfHopThuCongDanPublicCollection()
        {
            FeedBackCollection result = new FeedBackCollection();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"
                                        SELECT *
	                                        from ArticleFeedBack
	                                        where IsPublish=1 and FeedBackContentReply not like ''
                                        ";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new FeedBack(reader));
                    }
                }
            }
            return result;
        }
        public static FeedBackCollection GetFeedBackUnPublicCollection()
        {
            FeedBackCollection result = new FeedBackCollection();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"	
                                        SELECT *
	                                    from ArticleFeedBack
	                                    where IsPublish=0";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new FeedBack(reader));
                    }
                }
            }
            return result;
        }
        public static FeedBackCollection GetFeedBackPublicCollection()
        {
            FeedBackCollection result = new FeedBackCollection();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"	SELECT *
	                                        from ArticleFeedBack
	                                        where IsPublish=1
                                        ";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new FeedBack(reader));
                    }
                }
            }
            return result;
        }
        public static FeedBackCollection GetFeedBackPublicCollectionOfArticle(Guid articleId)
        {
            FeedBackCollection result = new FeedBackCollection();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.Parameters.AddWithValue("@ArticleId", articleId);
                command.CommandText = @"	SELECT *
	                                        from ArticleFeedBack 
	                                        where IsPublish=1 and ArticleId=@ArticleId
                                        ";
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new FeedBack(reader));
                    }
                }
            }
            return result;
        }
    }
}

