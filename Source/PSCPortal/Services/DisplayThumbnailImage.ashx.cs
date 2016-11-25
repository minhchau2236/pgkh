using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using fbs.ImageResizer;
using System.Collections.Specialized;
using fbs;
using System.Drawing;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for DisplayThumbnailImage
    /// </summary>
    public class DisplayThumbnailImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string imagePath = context.Request.QueryString["ImagePath"];
            int width =Convert.ToInt32(context.Request.QueryString["width"].ToString());
            int height = Convert.ToInt32(context.Request.QueryString["height"].ToString());
            if (imagePath == string.Empty)
                return;
            if (!File.Exists(HttpContext.Current.Server.MapPath(imagePath)))
                return; 
            byte[] _buffer = File.ReadAllBytes(context.Server.MapPath(imagePath));
            ProceeImage(_buffer, context,width,height);
            //byte[] _buffer = GetByteImage(originalImageBytes,width,height);
            if (_buffer == null)
                _buffer = new byte[1024];
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(_buffer);
        }
        //protected static byte[] GetByteImage(byte[] imageLarge,int width, int height)
        //{
        //    byte[] result = null;
        //    if (imageLarge != null)
        //    {
        //        System.IO.Stream stream = new System.IO.MemoryStream(imageLarge);
        //        result = ResizeFromStream(stream, width,height);
        //    }
        //    return result;
        //}

        //public static byte[] ResizeFromStream(Stream Buffer,int width, int height)
        //{
        //    System.Drawing.Image imgInput = System.Drawing.Image.FromStream(Buffer);
        //    System.Drawing.Imaging.ImageFormat fmtImageFormat = imgInput.RawFormat;
        //    System.Drawing.Bitmap bmpResized = new System.Drawing.Bitmap(imgInput, width, height);
        //    MemoryStream ms = new MemoryStream();
        //    bmpResized.Save(ms, fmtImageFormat);
        //    Byte[] img = ms.ToArray();
        //    imgInput.Dispose();
        //    bmpResized.Dispose();
        //    Buffer.Close();
        //    return img;
        //}
        public void ProceeImage(byte[] buffer, HttpContext context,int width,int height)
        {
            NameValueCollection queryString = new yrl("?maxwidth="+width.ToString()+"&maxheight="+height.ToString()+"&format=gif").QueryString;
            context.Response.ContentType = "image/jpeg";
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            Bitmap bitmap = new Bitmap(Image.FromStream(ms));
            using (Bitmap img = ImageManager.getBestInstance().BuildImage(bitmap, bitmap.RawFormat, queryString))
            {
                ImageOutputSettings.SaveJpeg(img, 100, context.Response.OutputStream);
            }
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