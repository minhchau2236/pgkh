using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.CMS;
using System.Collections.Generic;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;
using PSCPortal.Framework;
using PSCPortal.Engine;
using System.Web.Services;
using System.IO;

namespace PSCPortal.Systems.CMS
{
    public partial class VideoClipManage : PSCPortal.Framework.PSCPage
    {
        protected static VideoClipCollection VideoClipList
        {
            get
            {
                if (DataStatic["VideoClipCollection"] == null)
                {
                    DataStatic["VideoClipCollection"] = VideoClipCollection.GetVideoClipCollection();

                }
                return DataStatic["VideoClipCollection"] as VideoClipCollection;
            }
            set
            {
                DataStatic["VideoClipCollection"] = value;
            }
        }

        [WebMethod]
        public static string GetList(int startIndex, int pageSize, string sortExpression)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["data"] = VideoClipList.GetSegment(startIndex, pageSize, sortExpression);
            result["total"] = VideoClipList.Count;
            return js.Serialize(result);
        }

        [WebMethod]
        public static void VideoClip_Add()
        {
            VideoClip item = new VideoClip();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new VideoClipArgs(item, false);
        }

        [WebMethod]
        public static void AddVideoClip()
        {
            VideoClip videoClip = ((VideoClipArgs)PSCDialog.DataShare).VideoClip;
            VideoClipList.AddDB(videoClip);
        }

        [WebMethod]
        public static void Video_Update(string Id)
        {
            VideoClip item = VideoClipList.Where(v => v.Id == new Guid(Id)).Single();
            PSCDialog.DataShare = new VideoClipArgs(item, true);
        }

        [WebMethod]
        public static void UpdateVideoClip()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.VideoClipArgs).VideoClip.Update();
            DataStatic["VideoClipCollection"] = null;
        }

        [WebMethod]
        public static void DeleteVideoClip(string[] arrId)
        {
            foreach (string Id in arrId)
            {
                VideoClip item = VideoClipList.Where(v => v.Id == new Guid(Id)).Single();
                string curFile = @"D:\myWorks\Web\PhatGiaoKhanhHoa2\Source\PSCPortal_Sln\Source\PSCPortal\Resources\VideoClips\HomePage\" + item.Id + item.FileExtension;
                if (File.Exists(curFile))
                    File.Delete(curFile);
                VideoClipList.RemoveDB(item);
            }
        }
    }
}