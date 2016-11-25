using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace PSCPortal.Portlets.AdvertisementScroller
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {          
            string sPath = string.Format("~/Resources/Images/Advertisement/{0}/", DataId.ToString());
            
            string fullfile = fuHinhAnh.PostedFile.FileName;
            string filename = Path.GetFileName(fullfile);
            string link = "";
            if (fuHinhAnh.PostedFile != null)
            {
                string extenfile = Path.GetExtension(fullfile).ToLower();
                if (extenfile.ToLower() == ".jpeg" || extenfile.ToLower() == ".gif" || extenfile.ToLower() == ".jpg" || extenfile.ToLower() == ".png")
                {
                    int file_append = 0;
                    while (File.Exists(Server.MapPath(sPath + filename)))
                    {
                        file_append++;
                        filename = Path.GetFileNameWithoutExtension(sPath +"\\"+ filename)
                            + file_append.ToString() + extenfile;
                    }
                    fuHinhAnh.PostedFile.SaveAs(Server.MapPath(sPath + filename));
                    lbThongBao.Text = "Cập nhật thành công.";
                    lbThongBao.Visible = true;
                    FileStream fstream = new FileStream(Server.MapPath(sPath + filename), FileMode.Open);
                    //ResizeFromStream(filename, 100, fstream);
                    using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand comDataIdExist = new SqlCommand();
                        comDataIdExist.Connection = con;
                        comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;

                        com.Parameters.AddWithValue("@DataId", DataId);
                        com.Parameters.AddWithValue("@Link", link);
                        com.Parameters.AddWithValue("@Image", "Resources/Images/Advertisement/"+ DataId.ToString()+"/"+filename);
                        com.CommandText = "Insert Into Advertisement( DataId, Link, Image) Values (@DataId,@Link,@Image)";
                        com.ExecuteNonQuery();
                        com.Dispose();
                        con.Close();
                    }
                    LoadData();
                }
                else
                {
                    lbThongBao.Text = "Cập nhật không thành công. Hình ảnh phải thuộc dạng : jpeg , gif , jpg .";
                    lbThongBao.Visible = true;
                }
            }
            else
            {
                lbThongBao.Text = "Cập nhật không thành công. Bạn phải nhập hình ảnh";
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
            int intMaxSide;

            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }


            if (intMaxSide > MaxSideSize)
            {
                //set new width and height 
                double dblCoef = MaxSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }
            //create new bitmap 
            System.Drawing.Bitmap bmpResized = new System.Drawing.Bitmap(imgInput, intNewWidth, intNewHeight);

            //save bitmap to disk 
            bmpResized.Save(Server.MapPath("~/Resources/Images/ImagesInPortlet/ImageViewer/Thumbnail/frame-") + ImageSavePath, fmtImageFormat);

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
           
            string rootFolder = Server.MapPath("~/Resources/Images/Advertisement/"+ DataId.ToString());
            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }
            string[] files = Directory.GetFiles(rootFolder);
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Path");
            table.Columns.Add("Link");

            DataRow row;

            foreach (string image in files)
            {
                //  FileInfo finfo = new FileInfo(image);
                int plashIndex = image.LastIndexOf("\\");
                string fileName = image.Substring(plashIndex + 1);
                string imageLink="Resources/Images/Advertisement/" + DataId.ToString() + "/" + fileName;
                int dotIndex = fileName.LastIndexOf('.');
                string extension = fileName.Substring(dotIndex + 1).ToLower();               
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@Image", imageLink);
                    com.CommandText = "Select * from Advertisement where DataId=@DataId and Image=@Image";
                    string link = "";
                    string img = "";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            link = reader["Link"].ToString();
                            img = reader["Image"].ToString();
                           
                                if (extension == "jpg" || extension == "jpeg" || extension == "png" || extension == "gif")
                                {
                                    row = table.NewRow();
                                    row["Path"] = "~/"+img;
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
            string name = gvImage.DataKeys[e.RowIndex].Value.ToString();

            string fullFileName = "Resources/Images/Advertisement/" + DataId.ToString() + "/" + name;
            File.Delete(Server.MapPath(fullFileName));

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand comDataIdExist = new SqlCommand();
                comDataIdExist.Connection = con;
                comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                SqlCommand com = new SqlCommand();
                com.Connection = con;

                com.Parameters.AddWithValue("@DataId", DataId);
                com.Parameters.AddWithValue("@Image", fullFileName);
                com.CommandText = "Delete Advertisement where DataId=@DataId and Image=@Image";
                com.ExecuteNonQuery();
                com.Dispose();
                con.Close();
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

            string name = gvImage.DataKeys[dong].Value.ToString();
            string link = (gvImage.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand comDataIdExist = new SqlCommand();
                comDataIdExist.Connection = con;
                comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                SqlCommand com = new SqlCommand("Update Advertisement Set Link=@Link where DataId=@DataId and Image=@Image");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@DataId", SqlDbType.UniqueIdentifier).Value = DataId;                
                com.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = link;
                com.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = "Resources/Images/Advertisement/" + DataId.ToString() + "/" + name;
                com.ExecuteNonQuery();
                com.Dispose();
                con.Close();
            }
            gvImage.EditIndex = -1;
            LoadData();
        }                      
    }
}