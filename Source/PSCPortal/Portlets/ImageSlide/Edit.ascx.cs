using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace PSCPortal.Portlets.ImageSlide
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void Page_Prerender(object sender, EventArgs e)
        {

        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Guid id = DataId;
            const string sPath = "~/Resources/ImagesInPortlet/ImageSlide/";
            bool isExists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(sPath + id));
            if (!isExists)
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(sPath + id));
            string fullfile = fuHinhAnh.PostedFile.FileName;
            string filename = Path.GetFileName(fullfile);
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
                    fuHinhAnh.PostedFile.SaveAs(Server.MapPath(sPath + id + "/" + filename));
                    lbThongBao.Text = @"Cập nhật thành công.";
                    lbThongBao.Visible = true;
                    LoadData();
                }
                else
                {
                    lbThongBao.Text = @"Cập nhật không thành công. Hình ảnh phải thuộc dạng : jpeg , gif , jpg , png.";
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
            bmpResized.Save(Server.MapPath("~/userfiles/ImagesInPortlets/ImageViewer/Thumbnail/") + ImageSavePath, fmtImageFormat);

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
            const string rootFolder = "~/Resources/ImagesInPortlet/ImageSlide/";
            bool isExists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootFolder + DataId));
            if (!isExists)
                return;
            string[] files = Directory.GetFiles(Server.MapPath(rootFolder + DataId));
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Path");

            foreach (string image in files)
            {
                //  FileInfo finfo = new FileInfo(image);
                int plashIndex = image.LastIndexOf("\\", StringComparison.Ordinal);
                string fileName = image.Substring(plashIndex + 1);
                int dotIndex = fileName.LastIndexOf('.');
                string extension = fileName.Substring(dotIndex + 1);
                if (extension == "jpg" || extension == "png" || extension == "gif" || extension == "jpeg" )
                {
                    DataRow row = table.NewRow();
                    row["Path"] = "~/Resources/ImagesInPortlet/ImageSlide/" + DataId + "/" + fileName;
                    row["Name"] = fileName;
                    table.Rows.Add(row);
                }
            }
            gvImage.DataSource = table;
            gvImage.DataBind();
        }
        protected void gvImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var dataKey = gvImage.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                string name = dataKey.Value.ToString();
                string fullName = "~/Resources/ImagesInPortlet/ImageSlide/" + DataId + "/" + name;
                File.Delete(Server.MapPath(fullName));
            }
            LoadData();
        }
    }
}