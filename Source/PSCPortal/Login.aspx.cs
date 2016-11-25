using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Engine;
using PSCPortal.Framework;
using PSCPortal.Libs;

namespace PSCPortal
{
    public partial class Login : PSCPage
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    DataBind();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            if (Session["Count"] == null)
            {
                Session["Count"] = 1;
            }
            else
            {
                if ((int)Session["Count"] >= 4)
                {
                    radCaptchaValidate.Visible = true;

                    radCaptchaValidate.Validate();

                }
                Session["Count"] = (int)Session["Count"] + 1;
            }
            string subDomain = Ultility.GetSubDomain();
            if (subDomain != string.Empty)
                Response.Redirect("http://" + System.Configuration.ConfigurationManager.AppSettings["MainDomainName"] + "/Login.aspx");
        }
        protected void lbtVietnamese_Click(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("UICulture", "vi-vn"));
            Response.Redirect(Request.Path);
        }

        protected void lbtEnglish_Click(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("UICulture", "en-us"));
            Response.Redirect(Request.Path);
        }
    }
}
