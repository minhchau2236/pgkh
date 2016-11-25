using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.ImageViewer
{
    public partial class Edit : Engine.PortletEditControl
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
                LoadData();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sPath = string.Format("~/Resources/ImagesInPortlet/ImageViewer/{0}/Display/", DataId);
            string fullfile = fuHinhAnh.PostedFile.FileName;
            string filename = Path.GetFileName(fullfile);
            const string link = "";
            if (fuHinhAnh.PostedFile != null)
            {
                string extenfile = Path.GetExtension(fullfile);
                if (extenfile.ToLower() == ".jpeg" || extenfile.ToLower() == ".gif" || extenfile.ToLower() == ".jpg" || extenfile.ToLower() == ".png")
                {
                    int file_append = 0;
                    while (File.Exists(Server.MapPath(sPath + filename)))
                    {
                        file_append++;
                        filename = Path.GetFileNameWithoutExtension(sPath + filename)
                            + file_append + extenfile;
                    }
                    fuHinhAnh.PostedFile.SaveAs(Server.MapPath(sPath + filename));
                    lbThongBao.Text = @"Cập nhật thành công.";
                    lbThongBao.Visible = true;
                    FileStream fstream = new FileStream(Server.MapPath(sPath + filename), FileMode.Open);
                    ResizeFromStream(filename, 100, fstream);
                    using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand comDataIdExist = new SqlCommand {Connection = con};
                        comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                        SqlCommand com = new SqlCommand {Connection = con};

                        com.Parameters.AddWithValue("@DataId", DataId);
                        com.Parameters.AddWithValue("@Link", link);
                        com.Parameters.AddWithValue("@Image", "Resources/ImagesInPortlet/ImageViewer/" + DataId.ToString() + "/Display/" + filename);
                        com.CommandText = "Insert Into Advertisement( DataId, Link, Image) Values (@DataId,@Link,@Image)";
                        com.ExecuteNonQuery();
                        com.Dispose();
                        con.Close();
                    }
                    LoadData();
                }
                else
                {
                    lbThongBao.Text = @"Cập nhật không thành công. Hình ảnh phải thuộc dạng : jpeg , gif , jpg .";
                    lbThongBao.Visible = true;
                }
            }
            else
            {
                lbThongBao.Text = @"Cập nhật không thành công. Bạn phải nhập hình ảnh";
                lbThongBao.Visible = true;
            }
        }

        public void ResizeFromStream(string ImageSavePath, int MaxSideSize, Stream Buffer)
        {
            int intNewWidth;
            int intNewHeight;
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(Buffer);

            //Determine image format 
            System.Drawing.Imaging.ImageFormat fmtImageFormat = imgInput.RawFormat;

            //get image original width and height 
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            //determine if landscape or portrait 

            int intMaxSide = intOldWidth >= intOldHeight ? intOldWidth : intOldHeight;


            if (intMaxSide > MaxSideSize)
            {
                //set new width and height 
                double dblCoef = MaxSideSize/(double) intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef*intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef*intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }
            //create new bitmap 
            System.Drawing.Bitmap bmpResized = new System.Drawing.Bitmap(imgInput, intNewWidth, intNewHeight);

            //save bitmap to disk 
            bmpResized.Save(
                Server.MapPath("~/Resources/ImagesInPortlet/ImageViewer/" + DataId + "/Thumbnail/frame-" + ImageSavePath),
                fmtImageFormat);

            //release used resources 
            imgInput.Dispose();
            bmpResized.Dispose();
            Buffer.Close();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        protected void LoadData()
        {
            string pathOfStore = Server.MapPath("~/Resources/ImagesInPortlet/ImageViewer/" + DataId.ToString());
            if (!Directory.Exists(pathOfStore))
            {
                Directory.CreateDirectory(pathOfStore + "/Display");
                Directory.CreateDirectory(pathOfStore + "/Thumbnail");
                string ImageViewerXml = Server.MapPath("~/Resources/ImagesInPortlet/ImageViewer/ImageViewerManage.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(ImageViewerXml);
                XmlNodeList portlets = doc.SelectNodes("/Portlets");
                XmlElement item = doc.CreateElement("Portlet");
                XmlAttribute att = doc.CreateAttribute("id");
                att.Value = DataId.ToString();

                item.Attributes.Append(att);
                if (portlets != null) portlets[0].AppendChild(item);
                doc.Save(ImageViewerXml);
            }

            string rootFolder = "~/Resources/ImagesInPortlet/ImageViewer/"+DataId.ToString()+"/Display";

            string[] files = Directory.GetFiles(Server.MapPath(rootFolder));
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Path");
            table.Columns.Add("Link");

            foreach (string image in files)
            {
                int plashIndex = image.LastIndexOf("\\", StringComparison.Ordinal);
                string fileName = image.Substring(plashIndex + 1);
                int dotIndex = fileName.LastIndexOf('.');
                string imageLink = "Resources/ImagesInPortlet/ImageViewer/" + DataId + "/Display/" + fileName;
                string extension = fileName.Substring(dotIndex + 1).ToLower();
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand com = new SqlCommand {Connection = con};
                    con.Open();
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@Image", imageLink);
                    com.CommandText = "Select * from Advertisement where DataId=@DataId and Image=@Image";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string link = reader["Link"].ToString();
                            if (extension == "jpg" || extension == "jpeg" || extension == "png" || extension == "gif" || extension == "png")
                            {
                                DataRow row = table.NewRow();
                                row["Path"] = "~/Resources/ImagesInPortlet/ImageViewer/" + DataId.ToString() + "/Thumbnail/frame-" + fileName;
                                row["Name"] = fileName;
                                row["Link"] = link;
                                table.Rows.Add(row);
                            }
                        }
                    }
                    com.Dispose();
                    con.Close();
                }
            }
            gvImage.DataSource = table;
            gvImage.DataBind();
            gvImage.SelectedIndex = -1;
        }
        protected void gvImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var dataKey = gvImage.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                string name = dataKey.Value.ToString();
                string fullName = "~/Resources/ImagesInPortlet/ImageViewer/"+DataId+"/Thumbnail/frame-" + name;
                File.Delete(Server.MapPath(fullName));

                string fullFileName = "~/Resources/ImagesInPortlet/ImageViewer/"+DataId +"/Display/" + name;
                File.Delete(Server.MapPath(fullFileName));            

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand comDataIdExist = new SqlCommand {Connection = con};
                    comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                    SqlCommand com = new SqlCommand {Connection = con};

                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@Image", fullFileName);
                    com.CommandText = "Delete Advertisement where DataId=@DataId and Image=@Image";
                    com.ExecuteNonQuery();
                    com.Dispose();
                    con.Close();
                }
            }
            LoadData();
        }
        protected void gvImage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvImage.EditIndex = -1;
            LoadData();
        }

        protected void gvImage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvImage.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvImage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int dong = e.RowIndex;

            var dataKey = gvImage.DataKeys[dong];
            if (dataKey != null)
            {
                string name = dataKey.Value.ToString();
                var textBox = gvImage.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
                if (textBox != null)
                {
                    string link = textBox.Text;
                    using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand comDataIdExist = new SqlCommand {Connection = con};
                        comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                        SqlCommand com = new SqlCommand("Update Advertisement Set Link=@Link where DataId=@DataId and Image=@Image")
                        {
                            CommandType = CommandType.Text,
                            Connection = con
                        };

                        com.Parameters.AddWithValue("@DataId", SqlDbType.UniqueIdentifier).Value = DataId;
                        com.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = link;
                        com.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = "Resources/ImagesInPortlet/ImageViewer/" + DataId.ToString() + "/Display/" + name;
                        com.ExecuteNonQuery();
                        com.Dispose();
                        con.Close();
                    }
                }
            }
            gvImage.EditIndex = -1;
            LoadData();
        }                      
    }
}