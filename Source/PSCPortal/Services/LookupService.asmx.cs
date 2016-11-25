using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;
using System.Web.Script.Services;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for LookupService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LookupService : System.Web.Services.WebService
    {

        public struct TestDayObject
        {
            public string year;
            public ArrayList testDays;
        }

        [WebMethod]
        [ScriptMethod]
        public string GetSchoolYears()
        {
            string result = string.Empty;

            try
            {
                ArrayList years = new ArrayList();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    con.Open();
                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
                    com.Connection = con;
                    com.CommandText = @"create table #temp
	                                        (
		                                        stt int identity(0,1),
		                                        YearId int
	                                        )  
                                        	
	                                        insert #temp(YearId)
	                                        select YearId from YearTHPT
	                                        order by YearId DESC

	                                        declare @index int
	                                        set @index = 0
                                        	
	                                        declare @max int
	                                        set @max = (select max(stt) from #temp)

	                                        declare @temp int

	                                        while (@index <= @max)
	                                        begin
		                                        set @temp = (select YearId from #temp where @index=stt)

		                                        select YearId
		                                        from YearTHPT
		                                        where YearId=@temp

		                                        select TestDayId
		                                        from TestDateTHPT
		                                        where YearId=@temp		

		                                        set @index = @index+1
	                                        end

	                                        Drop table #temp";
                    com.CommandType = CommandType.Text;
                    using (System.Data.SqlClient.SqlDataReader reader = com.ExecuteReader())
                    {
                        int resultIndex = 0;//chi muc cua NextResult, dnag o vi tri bang thu 0;
                        TestDayObject year = new TestDayObject();
                        do
                        {

                            while (reader.Read())
                            {

                                if (resultIndex % 2 == 0)//neu dang nhay den bang chua menu cha thi add no vao danh sach cha, va gan menu cha hien hanh
                                {
                                    year = new TestDayObject { year = reader["YearId"].ToString(), testDays = new ArrayList() };
                                    years.Add(year);
                                }
                                else//nguoc lai, add no vao menu cha cua no
                                {
                                    year.testDays.Add(reader["TestDayId"].ToString());
                                }
                            }

                            resultIndex++;
                        } while (reader.NextResult());
                    }
                }
                System.Web.Script.Serialization.JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                result = serialize.Serialize(years);
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod]
        public string GetSchoolYearsDegree()
        {
            string result = string.Empty;

            try
            {
                ArrayList years = new ArrayList();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    con.Open();
                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
                    com.Connection = con;
                    com.CommandText = @"create table #temp
	                                    (
		                                    stt int identity(0,1),
		                                    YearId int
	                                    )  
                                    	
	                                    insert #temp(YearId)
	                                    select YearId from YearTHPT
	                                    Order by YearId Desc
                                    	
	                                    declare @index int
	                                    set @index = 0
                                    	
	                                    declare @max int
	                                    set @max = (select max(stt) from #temp)

	                                    declare @temp int

	                                    while (@index <= @max)
	                                    begin
		                                    set @temp = (select YearId from #temp where @index=stt)

		                                    select distinct YearTHPT.YearId
		                                    from YearTHPT inner join TestDateTHPT on YearTHPT.YearId=TestDateTHPT.YearId
		                                    where YearTHPT.YearId=@temp and TestDateTHPT.HasDegree is null
                                    		
		                                    select TestDayId
		                                    from TestDateTHPT
		                                    where YearId=@temp and HasDegree is null

		                                    set @index = @index+1
	                                    end

	                                    Drop table #temp";
                    com.CommandType = CommandType.Text;
                    using (System.Data.SqlClient.SqlDataReader reader = com.ExecuteReader())
                    {
                        int resultIndex = 0;//chi muc cua NextResult, dnag o vi tri bang thu 0;
                        TestDayObject year = new TestDayObject();
                        do
                        {

                            while (reader.Read())
                            {

                                if (resultIndex % 2 == 0)//neu dang nhay den bang chua menu cha thi add no vao danh sach cha, va gan menu cha hien hanh
                                {
                                    year = new TestDayObject { year = reader["YearId"].ToString(), testDays = new ArrayList() };
                                    years.Add(year);
                                }
                                else//nguoc lai, add no vao menu cha cua no
                                {
                                    year.testDays.Add(reader["TestDayId"].ToString());
                                }
                            }

                            resultIndex++;
                        } while (reader.NextResult());
                    }
                }
                System.Web.Script.Serialization.JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                result = serialize.Serialize(years);
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return result;
        }

        public struct SubjectAndMark
        {
            public string subjectName;
            public string mark;
            public string markReverse;
        }

        public struct Student
        {
            public string FullName;
            public string StudentId;
            public string Birthday;
            public string Total;
            public string MarkEncourage;
            public string TotalMark;
            public string Section;
            public string RoundTotalMark;
        }

        /// ///////////////
        [WebMethod]
        [ScriptMethod]
        public string GetStudentsByYearIdAndTimesIdAndSchoolId(string schoolYear, string times, string schoolId)
        {
            string result = GetStudentsByYearIdAndTimesIdAndSchoolIdAndStudentName(schoolYear, times, "", " ", "1");
            return result;
        }

        [WebMethod]
        [ScriptMethod]
        public string GetStudentsByYearIdAndTimesIdAndSchoolIdAndStudentId(string schoolYear, string times, string schoolId, string StudentId)
        {
            string result = string.Empty;
            try
            {
                ArrayList students = new ArrayList();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    con.Open();
                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = @"
			                                    select StudentTHPT.TotalMark,[RoundTotalMark],StudentTHPT.YearId,StudentTHPT.TestDayId,StudentId,FirstName+' '+MiddleName+' '+LastName as FullName,Sex,Birthday,MarkEncourage,Section.Name from StudentTHPT inner join Section on StudentTHPT.SectionId = Section.SectionId 
			                                    where StudentTHPT.YearId=@yearId and StudentTHPT.TestDayId=@timeId and StudentId = @studentId
                                                Order by LastName
                                    ";

                    com.Parameters.Add("@yearId", SqlDbType.NChar);
                    com.Parameters["@yearId"].Value = schoolYear;

                    com.Parameters.Add("@timeId", SqlDbType.NVarChar);
                    com.Parameters["@timeId"].Value = times;

                    com.Parameters.Add("@studentId", SqlDbType.NVarChar);
                    com.Parameters["@studentId"].Value = StudentId;

                    using (System.Data.SqlClient.SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader["FullName"].ToString();
                            string birthday = reader["Birthday"].ToString().Trim();
   
                            string studentId = reader["StudentId"].ToString();

                        //    string markEncourage = reader["MarkEncourage"].ToString();
                       //     string kq = reader["Result"].ToString();
                        //    string rank = reader["Rank"].ToString();
                            string totalMark = reader["TotalMark"].ToString();
                            string section = reader["Name"].ToString();
                            string roundTotalMark = reader["RoundTotalMark"].ToString();
                            Student s = new Student { StudentId = studentId, FullName = fullName, Birthday = birthday,Section = section, TotalMark = totalMark,RoundTotalMark=roundTotalMark };
                            students.Add(s);
                        }
                    }
                }
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                result = serializer.Serialize(students);
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }

            return result;
        }

        [WebMethod]
        [ScriptMethod]
        public string GetStudentsByYearIdAndTimesIdAndSchoolIdAndStudentName(string schoolYear, string times, string schoolId, string StudentName, string pIndex)
        {
            string result = string.Empty;
            int pageIndex = Int32.Parse(pIndex);
            ArrayList students = new ArrayList();
            string pathOfIndexFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["StudentIndexing"].ToString());

            if (Int32.Parse(schoolYear) >= 2000)
            {
                pathOfIndexFile += "\\" + schoolYear + "\\Index";
            }

            string studentName = StudentName.Replace("\"", "");
            studentName = "\"" + studentName + "\"";

            Lucene.Net.Search.IndexSearcher iSearcher = new Lucene.Net.Search.IndexSearcher(pathOfIndexFile);

            Lucene.Net.QueryParsers.QueryParser qYearParser = new Lucene.Net.QueryParsers.QueryParser("YearId", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
            Lucene.Net.Search.Query iYearQuery = qYearParser.Parse(schoolYear);

            Lucene.Net.QueryParsers.QueryParser qTestDayParser = new Lucene.Net.QueryParsers.QueryParser("TestDayId", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
            Lucene.Net.Search.Query iTestDayQuery = qTestDayParser.Parse(times);

            Lucene.Net.QueryParsers.QueryParser qStudentIdParser = new Lucene.Net.QueryParsers.QueryParser("StudentID", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
            Lucene.Net.Search.Query iStudentIdQuery = qStudentIdParser.Parse("1");

            //////////////////////////////////////////////////////////////////////
            Lucene.Net.Search.BooleanQuery bQuery = new Lucene.Net.Search.BooleanQuery();
            bQuery.Add(iYearQuery, Lucene.Net.Search.BooleanClause.Occur.MUST);
            bQuery.Add(iTestDayQuery, Lucene.Net.Search.BooleanClause.Occur.MUST);


            if (StudentName != " " && StudentName != "")
            {
                Lucene.Net.QueryParsers.QueryParser qStudentParser = new Lucene.Net.QueryParsers.QueryParser("StudentName", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
                Lucene.Net.Search.Query iStudentQuery = qStudentParser.Parse(studentName);
                bQuery.Add(iStudentQuery, Lucene.Net.Search.BooleanClause.Occur.MUST);
            }

            Lucene.Net.Search.Hits iHits = iSearcher.Search(bQuery);

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();

                //paging
                for (int i = pageIndex * 20 - 20; i < pageIndex * 20 && i < iHits.Length(); i++)
                {
                    string yId = iHits.Doc(i).Get("YearId");
                    string stuId = iHits.Doc(i).Get("StudentID");
                    string testDayId = iHits.Doc(i).Get("TestDayId");

                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = @"   select StudentTHPT.TotalMark,[RoundTotalMark],StudentTHPT.YearId,StudentTHPT.TestDayId,StudentId,FirstName+' '+MiddleName+' '+LastName as FullName,Sex,Birthday,MarkEncourage,Section.Name from StudentTHPT inner join Section on StudentTHPT.SectionId = Section.SectionId 
			                                    where StudentTHPT.YearId=@yearId and StudentTHPT.TestDayId=@timeId and StudentId = @studentId
                                           Order by LastName
	                                   ";
                    com.Parameters.Add("@yearId", SqlDbType.NChar);
                    com.Parameters["@yearId"].Value = yId;

                    com.Parameters.Add("@timeId", SqlDbType.NVarChar);
                    com.Parameters["@timeId"].Value = testDayId;

                    com.Parameters.Add("@studentId", SqlDbType.NVarChar);
                    com.Parameters["@studentId"].Value = stuId;

                    using (System.Data.SqlClient.SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string fullName = reader["FullName"].ToString();
                            string birthday = reader["Birthday"].ToString().Trim();
                            string studentId = reader["StudentId"].ToString();
                            string total = iHits.Length().ToString();
                        //    string markEncourage = reader["MarkEncourage"].ToString();
                            string totalMark = reader["TotalMark"].ToString();
                            string section = reader["Name"].ToString();
                            string roundTotalMark = reader["RoundTotalMark"].ToString();
                            Student s = new Student { StudentId = studentId, FullName = fullName, Birthday = birthday, Total = total,Section =section, TotalMark = totalMark, RoundTotalMark=roundTotalMark };
                            students.Add(s);
                        }
                    }
                }
            }
            iSearcher.Close();

            System.Web.Script.Serialization.JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();

            result = serialize.Serialize(students);
            return result;
        }

        [WebMethod]
        [ScriptMethod]
        public string GetStudentMarkByYearIdAndTimesIdAndStudentId(string schoolYear, string times, string studentId)
        {

            string result = string.Empty;
            try
            {
                ArrayList marks = new ArrayList();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    con.Open();
                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = @"select SubjectName,Mark from MarkTHPT inner Join SubjectTHPT on MarkTHPT.SubjectId = SubjectTHPT.SubjectId and MarkTHPT.SectionId = SubjectTHPT.SectionId
	                                    where YearId = @yearId and TestDayId = @timeId and StudentId =@studentId
              
                                        ";
                    com.Parameters.Add("@yearId", SqlDbType.NChar);
                    com.Parameters["@yearId"].Value = schoolYear;

                    com.Parameters.Add("@timeId", SqlDbType.NVarChar);
                    com.Parameters["@timeId"].Value = times;

                    com.Parameters.Add("@studentId", SqlDbType.NVarChar);
                    com.Parameters["@studentId"].Value = studentId;
                    using (System.Data.SqlClient.SqlDataReader reader = com.ExecuteReader())
                    {
                        SubjectAndMark mark;
                        while (reader.Read())
                        {
                            try
                            {
                                string tempMark = reader["Mark"].ToString();
                                if (tempMark[0] != '1')
                                    tempMark = tempMark.Substring(1);
                                mark = new SubjectAndMark { subjectName = reader["SubjectName"].ToString(), mark =tempMark };
                            }
                            catch (Exception ex)
                            {
                                string err = ex.ToString();
                                mark = new SubjectAndMark { subjectName = reader["SubjectName"].ToString(), markReverse = reader["MarkReverse"].ToString() };
                            }
                            marks.Add(mark);
                        }
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    result = serializer.Serialize(marks);
                }
            }
            catch (Exception ex2)
            {
                string err2 = ex2.ToString();
            }

            return result;
        }
    }
}
