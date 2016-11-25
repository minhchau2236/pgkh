using PSCPortal.CMS;
using PSCPortal.Modules.CMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PSCPortal.Systems.CMS
{
    public partial class LayoutDetail : PSCPortal.Framework.PSCDialog
    {
        private static PSCPortal.CMS.LayoutArgs Args
        {
            get
            {
                return DataShare as PSCPortal.CMS.LayoutArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Text = Args.Layout.Id.ToString();
            txtName.Text = Args.Layout.Name;
            if (Args.IsEdit)
            {
                Layout layout = LayoutCollection.GetLayOut(Args.Layout.Id.ToString()).SingleOrDefault();
                //string newPath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleDisplay"] + layout.Name + ".ascx");
                string newPath =HttpContext.Current.Server.MapPath(layout.NavigationUrl);
                editorForm.Value = File.ReadAllText(newPath);
            }
            else
            {
                PSCPortal.Engine.ArticleArgs articleArgs = new PSCPortal.Engine.ArticleArgs();
                string defaultPath = HttpContext.Current.Server.MapPath(articleArgs.Path);
                editorForm.Value = File.ReadAllText(defaultPath);
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string txtEditor)
        {
            string oldName = Args.Layout.Name;
            Args.Layout.Name = name;
            LayoutCollection list = LayoutCollection.GetAllLayout();
            PSCPortal.Engine.ArticleArgs articleArgs = new PSCPortal.Engine.ArticleArgs();
            string newpath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleDisplay"] + name + ".ascx");
            if (Args.IsEdit)
            {
                string oldPath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleDisplay"] + oldName + ".ascx");
                File.Move(oldPath, newpath);
            }
            else
            {
                string defaultPath = HttpContext.Current.Server.MapPath(articleArgs.Path);
                if (list.Where(a => a.Name.Equals(name)).Count() > 0)
                    return;
                File.Copy(defaultPath, newpath);
            }
            File.WriteAllText(newpath, txtEditor);
 
        }
    }
}