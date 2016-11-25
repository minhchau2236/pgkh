using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Engine
{

      public enum Template: int
      {
            TrangChu=2,
            Khoa=9,
            TrungTam=10,
            PhongTS = 12
      }

      public class PageTemplate
      {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get;set;}
            public string ImgPath { get; set;}
            public string FileASCXPath { get;set;}
            public string FileCSSPath { get; set;}

            public List<Portlet> Portlets { get; set; }
            public List<Module> Modules { get; set; }
      }


      public class PageTemplateCollection
      {
            
            public PageTemplateCollection()
            {
                  // Init list PageTemplates
                  PageTemplates = new Dictionary<Template, PageTemplate>();

                  // Insert pageTemplate into PageTemplates
                  GetPageTemplate();
            }

            public PageTemplate this[Template template]
            {
                  get
                  {

                      if (PageTemplates.ContainsKey(template))
                          return PageTemplates[template];
                      else
                          return null;

                  }
            }

            private  Dictionary<Template, PageTemplate> PageTemplates { get;set;}

            private void GetPageTemplate()
            {
                  // Create template

                  PageTemplate tpTrangChu=new PageTemplate() { Id=(int)Template.TrangChu, Name="Trang Chu",Description="", FileASCXPath= "~/PageTemplate/HomePage.ascx",ImgPath="" };
                 //Add danh sach portlet theo Template TrangChu
                 // tpTrangChu.Portlets = new List<Portlet>();
                 //   Portlet pt=new Portlet(){ Id}
                  // Add danh sach Modules theo Template TrangChu
                  //tpTrangChu.Modules = new List<Module>();
                  
                
                  PageTemplate tpTrangKhoa = new PageTemplate() { Id = (int)Template.Khoa, Name = "Khoa", Description = "", FileASCXPath = "~/PageTemplate/Khoa.ascx", ImgPath = "" };
                  PageTemplate tpTrungTam = new PageTemplate() { Id = (int)Template.TrungTam, Name = "Trung Tam", Description = "", FileASCXPath = "~/PageTemplate/TrungTam.ascx", ImgPath = "" };
                  PageTemplate tpPhongTS = new PageTemplate() { Id = (int)Template.PhongTS, Name = "Phong Tuyen Sinh", Description = "", FileASCXPath = "~/PageTemplate/PhongTS.ascx", ImgPath = "" };

                  // add template to dictionary
                  PageTemplates.Add(Template.TrangChu, tpTrangChu);
                  PageTemplates.Add(Template.Khoa, tpTrangKhoa);
                  PageTemplates.Add(Template.TrungTam, tpTrungTam);
                  PageTemplates.Add(Template.PhongTS, tpPhongTS);
            }

      }
}
