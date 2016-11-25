using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using PSCPortal.CMS;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
using System.Globalization;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor.DialogControls;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleDetail : PSCPortal.Framework.PSCDialog
    {
        public static PSCPortal.CMS.ArticleArgs Args
        {
            get
            {
                return DataShare as PSCPortal.CMS.ArticleArgs;
            }
        }
        protected static PageCollection PageList
        {
            get
            {
                if (DataStatic["PageList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        DataStatic["PageList"] = PageCollection.GetPageCollection();
                    else
                    {
                        PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                        DataStatic["PageList"] = subDomain.GetPagesBelongTo();
                    }
                }

                return DataStatic["PageList"] as PageCollection;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
            if (!IsPostBack)
                LoadCustomEditor();
        }
        protected void LoadCustomEditor()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId == Guid.Empty))
            {
                SubDomain subdomain =
                    SubDomainCollection.GetSubDomainCollection().SingleOrDefault(sub => sub.Id == subId);
                if (subdomain == null) // homepage
                {
                    Libs.Ultility.SettingEditor(reDescription, "");
                    Libs.Ultility.SettingEditor(reContent, "");

                }
                else
                {
                    Libs.Ultility.SettingEditor(reDescription, subdomain.Name);
                    Libs.Ultility.SettingEditor(reContent, subdomain.Name);
                }
            }
            else
            {
                reContent.DocumentManager.SearchPatterns = new[] { "*.*" };
                reContent.MediaManager.SearchPatterns = new[] { "*.*" };
                reContent.FlashManager.SearchPatterns = new[] { "*.*" };
                reContent.ImageManager.SearchPatterns = new[] { "*.*" };
                reDescription.DocumentManager.SearchPatterns = new[] { "*.*" };
                reDescription.MediaManager.SearchPatterns = new[] { "*.*" };
                reDescription.FlashManager.SearchPatterns = new[] { "*.*" };
                reDescription.ImageManager.SearchPatterns = new[] { "*.*" };
            }
            FileManagerDialogParameters documentManagerParameters = new FileManagerDialogParameters();
            documentManagerParameters.ViewPaths = new string[] { "~/Resources/AlbumBookSlider" };
            documentManagerParameters.UploadPaths = new string[] { "~/Resources/AlbumBookSlider" };
            documentManagerParameters.DeletePaths = new string[] { "~/Resources/AlbumBookSlider" };
            documentManagerParameters.MaxUploadFileSize = 52428800;
            documentManagerParameters.SearchPatterns = new[] { "*.pdf" };
            DialogDefinition documentManager = new DialogDefinition(typeof(DocumentManagerDialog), documentManagerParameters);
            documentManager.ClientCallbackFunction = "DocumentManagerFunction";
            documentManager.Width = Unit.Pixel(694);
            documentManager.Height = Unit.Pixel(440);
            DialogOpener1.DialogDefinitions.Add("DocumentManager", documentManager);
        }
        protected void LoadData()
        {
            txtId.Text = Args.Article.Id.ToString();
            txtName.Text = Args.Article.Name;
            txtTitle.Text = Args.Article.Title;
            rdiCreatedDate.SelectedDate = Args.Article.CreatedDate;
            rdiModifiedDate.SelectedDate = Args.Article.ModifiedDate;
            IsVisibleCreateDate.Checked = Args.Article.IsVisibleCreateDate.HasValue ? (bool)Args.Article.IsVisibleCreateDate : false;
            rcbPage.DataSource = PageList;
            rcbPage.DataBind();
            rcbArticleTemplate.SelectedValue = Args.Article.ArticleTemplate.ToString();
            PSCPortal.Engine.Page page = PageList.SingleOrDefault(p => p.Id == Args.Article.PageId);
            if (page != null)
                rcbPage.Items.FindItemByValue(Args.Article.PageId.ToString()).Selected = true;
            reDescription.Content = Args.Description;
            reContent.Content = Args.Content;
            //reAvatar.Content = Args.Avatar;
            txtArticleHang.SelectedDate = Args.Article.ArticleHangDate != null ? Args.Article.ArticleHangDate : DateTime.Now;
            cbxArticleHang.Checked = Args.Article.ArticleHangDate != null ? true : false;
            cbxComment.Checked = Article.CheckVisibleComment(Args.Article.Id);
            Article model = Article.GetArticleAlbum(Args.Article.Id);
            cbxNews.Checked = !string.IsNullOrEmpty(model.DocumentPath) || !string.IsNullOrEmpty(model.AlbumPath) ? true : false;
            txtAlbum.Text = model.AlbumPath;
            txtDocument.Text = model.DocumentPath;
            PSCPortal.Framework.PSCSubDialog.DataShare = Args;
            Session.Remove("LinkImage");
            Session.Remove("LinkImgPortlet");
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string title, DateTime createdDate,
            DateTime modifiedDate, Guid pageId, string description, string content,
            bool isVisibleCreateDate, DateTime? expireDate, bool articleHangChecked, int articleTemplate,
            bool commentChecked, string documentPath, string albumPath)
        {
            Args.Article.Name = name;
            Args.Article.Title = title;
            Args.Article.CreatedDate = createdDate;
            Args.Article.ModifiedDate = modifiedDate;
            Args.Article.PageId = pageId;
            Args.Article.Link = (string)HttpContext.Current.Session["LinkImage"];
            Args.Article.LinkImgPortlet = (string)HttpContext.Current.Session["LinkImgPortlet"];
            Args.Description = description;
            Args.Content = content;
            Args.Article.IsVisibleCreateDate = isVisibleCreateDate;
            Args.Article.ArticleHangDate = articleHangChecked ? expireDate : null;
            Args.Article.ArticleTemplate = articleTemplate;
            Args.Article.IsVisibleComment = commentChecked;
            Args.Article.DocumentPath = documentPath;
            Args.Article.AlbumPath = albumPath;
        }




    }
}
