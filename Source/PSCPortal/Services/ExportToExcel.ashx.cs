using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSCPortal.Framework.Helpler;
using System.Data.SqlClient;
using Infragistics.Excel;
using PSCPortal.Systems.CMS;
using System.IO;
using System.Web.SessionState;
using PSCPortal.CMS;
using PSCPortal.Engine;


namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for ExportToExcel
    /// </summary>
    public class ExportToExcel : IHttpHandler, IRequiresSessionState
    {
        protected static string nameWorksheet = "Tất cả trang";
        public void ProcessRequest(HttpContext context)
        {
            string TuNgay =context.Request.Params["fromDate"]==""?"":context.Request.Params["fromDate"] ;
            string DenNgay = context.Request.Params["toDate"]==""?"":context.Request.Params["toDate"];
            string option = context.Request.Params["option"] == "" ? "" : context.Request.Params["option"];
            switch(option)
            {
                case "TKTruyCapTrang":
                    {
                        List<VisitorSubDomain> result = ThongKeSoLuongTruyCapTrang(TuNgay, DenNgay);
                        XuatExcelTKTruyCapTrang(TuNgay, DenNgay, result, context, true);
                        break;
                    }
                case "TKBaiVietDaDang":
                    {
                        List<Article> result=ThongKeBaiVietDaDang(TuNgay, DenNgay);
                        XuatExcelTKBaiVietDaDang(TuNgay, DenNgay, result, context, true);
                        break;
                    }
                case "TKTruyCapBaiViet":
                    {
                        List<Article> result=ThongKeTruyCapBaiViet(TuNgay, DenNgay);
                        XuatExcelTKTruyCapBaiViet(TuNgay, DenNgay, result, context, true);
                        break;
                    }
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //Thống kê số lượng truy cập trang
        public static  List<VisitorSubDomain> ThongKeSoLuongTruyCapTrang(string tungay, string denngay)
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            string query = string.Empty;
            query = "SELECT sb.[Description] as Title ,COUNT([SubDomainId]) as ViewTime  FROM [Log] l left join SubDomain sb on l.[SubDomainId]=sb.Id where 1=1";
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
            List<PSCPortal.Systems.CMS.VisitorSubDomain> result = new List<PSCPortal.Systems.CMS.VisitorSubDomain>();
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
                    PSCPortal.Systems.CMS.VisitorSubDomain itemp = new PSCPortal.Systems.CMS.VisitorSubDomain();
                    itemp.Title = (string)reader["Title"];
                    itemp.ViewTime = (int)reader["ViewTime"];
                    result.Add(itemp);
                }
            }            
            return result;
        }             
        public static void XuatExcelTKTruyCapTrang(string tungay, string denngay,List<VisitorSubDomain> VisitorSubDomainList, HttpContext context, bool response)
        {
            DateTime now = DateTime.Now;
            string nameExcel = "Thống kê số lượng truy cập trang";
            string str = string.Format("_{0}_{1}_{2}_{3}_{4}_{5}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Millisecond);
            str = "TKTruyCapTrang" + str + ".xls";            
            
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add(nameExcel);
            workbook.ActiveWorksheet = workbook.Worksheets[nameExcel];
            int colums = 3;//
            int rowStart = 1;
            sheet.Rows[rowStart].Cells[1].Value = "THỐNG KÊ SỐ LƯỢNG TRUY CẬP TRANG";//
            sheet.MergedCellsRegions.Add(rowStart, 0, rowStart, colums-1);
            sheet.Rows[rowStart].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart].Height = 17 * 20;            
            if (tungay != "" && denngay != "")
            {
                DateTime fromDate = DateTime.Parse(tungay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                DateTime toDate = DateTime.Parse(denngay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                sheet.Rows[rowStart+ 1].Cells[1].Value = "(Từ ngày: " + string.Format("{0: dd/MM/yyyy}", fromDate) + "   đến ngày: " + string.Format("{0:dd/MM/yyyy}", toDate) + ")";
            }
            sheet.MergedCellsRegions.Add(rowStart+1, 1, rowStart+ 1, colums-2);
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart+1].Height = 17 * 20;
                      
            //định dạng tiêu đề table
            //0 stt |1 Sub Domain |2 Lượng truy cập
            
            sheet.Columns[0].Width = 5 * 256;
            sheet.Columns[1].Width = 50 * 256;
            sheet.Columns[2].Width = 10 * 256;
            
            sheet.Rows[rowStart+2].Cells[0].Value = "STT";
            sheet.Rows[rowStart+2].Cells[1].Value = "Trang";
            sheet.Rows[rowStart+2].Cells[2].Value = "Lượng truy cập";
            
            for (int i = 0; i < colums; i++)
            {
                SetCellFormatHeader(sheet,rowStart+ 2, i);       
            }
            int indexRow = rowStart + 2;
            int stt = 0;
            foreach (VisitorSubDomain item in VisitorSubDomainList)
            {
                indexRow++;
                stt++;
                sheet.Rows[indexRow].Cells[0].Value = stt;
                sheet.Rows[indexRow].Cells[1].Value = item.Title;
                sheet.Rows[indexRow].Cells[2].Value = item.ViewTime;
                
                for (int i = 0; i < colums; i++)
                {
                    SetCellFormat(sheet, indexRow, i);
                }
            }            
            
            string filename = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["TempExcel"]);
            // Tạo folder
            if (Directory.Exists(filename) == false)
                Directory.CreateDirectory(filename);
            filename = filename + "/"+str;
            BIFF8Writer.WriteWorkbookToFile(workbook, filename);
            if (response)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.Open));
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=" + str);
                context.Response.BinaryWrite(reader.ReadBytes((int)(new FileInfo(filename)).Length));
                reader.Close();
                context.Response.Flush();
            }
        }
        //Thống kê bài viết đã đăng    
        public static List<Article> ThongKeBaiVietDaDang(string tungay, string denngay)
        {
            PSCPortal.CMS.ArticleCollection ArticleList=ArticleCollection.GetArticleViewTimeCollectionPublish();
            List<Article> result = new List<Article>();
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (subId == Guid.Empty)
            {
                result = ArticleList.ToList();
            }
            else
            {
                result = new List<Article>();
                PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain {Id = subId};                
                nameWorksheet =PSCPortal.Engine.SubDomain.GetSubById(subId.ToString()).Description;
                PageCollection listPage = subDomain.GetPagesBelongTo();
                foreach (var item in listPage)
                {
                    foreach (var article in ArticleList.Where(ar => ar.PageId == item.Id))
                    {
                        result.Add(article);
                    }
                }
            }
            if (tungay != string.Empty && denngay != string.Empty)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                DateTime startDate = DateTime.Parse(tungay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime endDate = DateTime.Parse(denngay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                result = result.Where(ar => ar.CreatedDate >= startDate && ar.CreatedDate <= endDate).OrderByDescending(t=>t.CreatedDate).ToList<Article>();                
                
            }            
            return result;
        }   
        public static void XuatExcelTKBaiVietDaDang(string tungay, string denngay,List<Article> DisplayArticleList, HttpContext context, bool response)
        {
            DateTime now = DateTime.Now;
            string nameExcel = "Thống kê bài viết đã đăng";
            string str = string.Format("_{0}_{1}_{2}_{3}_{4}_{5}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Millisecond);
            str = "TKBaiVietDaDang" + str + ".xls";  //          
            
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add(nameWorksheet);
            workbook.ActiveWorksheet = workbook.Worksheets[nameWorksheet];
            int colums = 3;//
            int rowStart = 1;
            sheet.Rows[rowStart].Cells[1].Value = "THỐNG KÊ BÀI VIẾT ĐÃ ĐĂNG";//
            sheet.MergedCellsRegions.Add(rowStart, 0, rowStart, colums-1);
            sheet.Rows[rowStart].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart].Height = 17 * 20;            
            if (tungay != "" && denngay != "")
            {
                DateTime fromDate = DateTime.Parse(tungay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                DateTime toDate = DateTime.Parse(denngay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                sheet.Rows[rowStart+ 1].Cells[1].Value = "(Từ ngày: " + string.Format("{0: dd/MM/yyyy}", fromDate) + "   đến ngày: " + string.Format("{0:dd/MM/yyyy}", toDate) + ")";
            }
            sheet.MergedCellsRegions.Add(rowStart+1, 1, rowStart+ 1, colums-2);
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart+1].Height = 17 * 20;
                      
            //định dạng tiêu đề table
            //0 stt |1 Tiêu đề|2 Ngày đăng 
            
            sheet.Columns[0].Width = 5 * 256;
            sheet.Columns[1].Width = 100 * 256;
            sheet.Columns[2].Width = 12 * 256;            
            
            sheet.Rows[rowStart+2].Cells[0].Value = "STT";
            sheet.Rows[rowStart+2].Cells[1].Value = "Tiêu đề";
            sheet.Rows[rowStart+2].Cells[2].Value = "Ngày đăng";            
            
            for (int i = 0; i < colums; i++)
            {
                SetCellFormatHeader(sheet,rowStart+ 2, i);       
            }
            int indexRow = rowStart + 2;
            int stt = 0;
            foreach (Article item in DisplayArticleList)
            {
                indexRow++;
                stt++;
                sheet.Rows[indexRow].Cells[0].Value = stt;
                sheet.Rows[indexRow].Cells[1].Value = item.Title;
                sheet.Rows[indexRow].Cells[2].Value = string.Format("{0: dd/MM/yyyy}", item.CreatedDate);                
                
                for (int i = 0; i < colums; i++)
                {
                    SetCellFormat(sheet, indexRow, i);
                }
            }            
            
            string filename = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["TempExcel"]);
            // Tạo folder
            if (Directory.Exists(filename) == false)
                Directory.CreateDirectory(filename);
            filename = filename + "/"+str;
            BIFF8Writer.WriteWorkbookToFile(workbook, filename);
            if (response)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.Open));
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=" + str);
                context.Response.BinaryWrite(reader.ReadBytes((int)(new FileInfo(filename)).Length));
                reader.Close();
                context.Response.Flush();
            }
        }
        //Thống kê truy cập bài viết
        public static List<Article> ThongKeTruyCapBaiViet(string tungay, string denngay)
        {
            PSCPortal.CMS.ArticleCollection ArticleList=ArticleCollection.GetArticleViewTimeCollectionPublish();
            List<Article> result = new List<Article>();
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (subId == Guid.Empty)
            {
                result = ArticleList.ToList();
            }
            else
            {
                result = new List<Article>();
                PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                nameWorksheet =PSCPortal.Engine.SubDomain.GetSubById(subId.ToString()).Description;
                PageCollection listPage = subDomain.GetPagesBelongTo();
                foreach (var item in listPage)
                {
                    foreach (var article in ArticleList.Where(ar => ar.PageId == item.Id))
                    {
                        result.Add(article);
                    }
                }
            }
            if (tungay != string.Empty && denngay != string.Empty)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                DateTime startDate = DateTime.Parse(tungay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime endDate = DateTime.Parse(denngay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                result = result.Where(ar => ar.CreatedDate >= startDate && ar.CreatedDate <= endDate).ToList<Article>();
            }            
            return result;
        }
        public static void XuatExcelTKTruyCapBaiViet(string tungay, string denngay,List<Article> DisplayArticleList, HttpContext context, bool response)
        {
            DateTime now = DateTime.Now;
            string nameExcel = "Thống kê số lượng truy cập bài viết";
            string str = string.Format("_{0}_{1}_{2}_{3}_{4}_{5}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Millisecond);
            str = "TKTruyCapBaiViet" + str + ".xls";            
            
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add(nameWorksheet);
            workbook.ActiveWorksheet = workbook.Worksheets[nameWorksheet];
            int colums = 4;//
            int rowStart = 1;
            sheet.Rows[rowStart].Cells[1].Value = "THỐNG KÊ SỐ LƯỢNG TRUY CẬP BÀI VIẾT";//
            sheet.MergedCellsRegions.Add(rowStart, 0, rowStart, colums-1);
            sheet.Rows[rowStart].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            sheet.Rows[rowStart].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart].Height = 17 * 20;            
            if (tungay != "" && denngay != "")
            {
                DateTime fromDate = DateTime.Parse(tungay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                DateTime toDate = DateTime.Parse(denngay, new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" });
                sheet.Rows[rowStart+ 1].Cells[1].Value = "(Từ ngày: " + string.Format("{0: dd/MM/yyyy}", fromDate) + "   đến ngày: " + string.Format("{0:dd/MM/yyyy}", toDate) + ")";
            }
            sheet.MergedCellsRegions.Add(rowStart+1, 1, rowStart+ 1, colums-2);
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Alignment = HorizontalCellAlignment.Center;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
            sheet.Rows[rowStart+1].Cells[1].CellFormat.Font.Height = 12 * 20;
            sheet.Rows[rowStart+1].Height = 17 * 20;
                      
            //định dạng tiêu đề table
            //0 stt |1 Tiêu đề|2 Ngày đăng |3 Lượng truy cập
            
            sheet.Columns[0].Width = 5 * 256;
            sheet.Columns[1].Width = 100 * 256;
            sheet.Columns[2].Width = 12 * 256;
            sheet.Columns[3].Width = 10 * 256;
            
            sheet.Rows[rowStart+2].Cells[0].Value = "STT";
            sheet.Rows[rowStart+2].Cells[1].Value = "Tiêu đề";
            sheet.Rows[rowStart+2].Cells[2].Value = "Ngày đăng";
            sheet.Rows[rowStart+2].Cells[3].Value = "Lượng truy cập";
            
            for (int i = 0; i < colums; i++)
            {
                SetCellFormatHeader(sheet,rowStart+ 2, i);       
            }
            int indexRow = rowStart + 2;
            int stt = 0;
            foreach (Article item in DisplayArticleList)
            {
                indexRow++;
                stt++;
                sheet.Rows[indexRow].Cells[0].Value = stt;
                sheet.Rows[indexRow].Cells[1].Value = item.Title;
                sheet.Rows[indexRow].Cells[2].Value = string.Format("{0: dd/MM/yyyy}", item.CreatedDate);
                sheet.Rows[indexRow].Cells[3].Value = item.ViewTime;
                
                for (int i = 0; i < colums; i++)
                {
                    SetCellFormat(sheet, indexRow, i);
                }
            }            
            
            string filename = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["TempExcel"]);
            // Tạo folder
            if (Directory.Exists(filename) == false)
                Directory.CreateDirectory(filename);
            filename = filename + "/"+str;
            BIFF8Writer.WriteWorkbookToFile(workbook, filename);
            if (response)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.Open));
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=" + str);
                context.Response.BinaryWrite(reader.ReadBytes((int)(new FileInfo(filename)).Length));
                reader.Close();
                context.Response.Flush();
            }
        }
        //Định dạng
        public static void SetCellFormatHeader(Worksheet sheet, int indexrow, int indexCell)
        {            
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.Alignment = HorizontalCellAlignment.Left;//canh trái
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.WrapText = ExcelDefaultableBoolean.True;
            //sheet.Rows[indexrow].Height = 35 * 20; //chiều cao
        }
        public static void SetCellFormat(Worksheet sheet, int indexrow, int indexCell)
        {
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.Alignment = HorizontalCellAlignment.Left;//canh trái
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
            sheet.Rows[indexrow].Cells[indexCell].CellFormat.WrapText = ExcelDefaultableBoolean.True;
            //sheet.Rows[indexrow].Height = 40 * 20;
        }
    }

}