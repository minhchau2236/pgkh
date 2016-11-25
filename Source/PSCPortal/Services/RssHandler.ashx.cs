using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using PSCPortal.CMS;
using RssToolkit.Rss;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for GetImage
    /// </summary>
    public class RssHandler : RssToolkit.Rss.RssDocumentHttpHandler
    {
        protected override void PopulateRss(string channelName, string userName)
        {
            string topicId = HttpContext.Current.Request.QueryString["TopicId"];
            Rss.Version = "2.0";
            Rss.Channel = new RssChannel();
            //Rss.Channel.Generator = "Trường Đại Học Kinh Tế Luật - UEL";
            Rss.Channel.Link = System.Configuration.ConfigurationManager.AppSettings["MainDomainName"];
            Rss.Channel.Copyright = "Trường Đại Học Đà Lạt - DLU";
            //Rss.Channel.Description = "Định dạng theo tiêu chuẩn XML";
            Rss.Channel.Title = "Tin mới nhất - Trường Đại Học Đà Lạt";            
            Rss.Channel.PubDate = string.Format("{0:dddd, MMMM d, yyyy HH:mm:ss}", DateTime.Now);
            Rss.Channel.WebMaster = "webadmin@dlu.edu.vn";
            //Rss.Channel.LastBuildDate = string.Format("{0:dddd, MMMM d, yyyy HH:mm:ss}", DateTime.Now);
            var list = ArticleCollection.GetArticleCollection(new Topic { Id = new Guid(topicId) });
            Rss.Channel.Items = new List<RssItem>();
            foreach (var item in list)
            {
                var rss = new RssItem();
                rss.Title = item.Title;
                rss.Description = item.GetDescription();
                rss.PubDate = string.Format("{0:dddd, MMMM d, yyyy HH:mm:ss}", item.CreatedDate);
                rss.Link = System.Configuration.ConfigurationManager.AppSettings["MainDomainName"] + "/?ArticleId=" + item.Id;
                Rss.Channel.Items.Add(rss);
            }
        }
    }
}