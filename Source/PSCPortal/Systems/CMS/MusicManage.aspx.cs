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

namespace PSCPortal.Systems.CMS
{
    public partial class MusicManage : PSCPortal.Framework.PSCPage
    {
        protected static MusicCollection MusicList
        {
            get
            {
                if (DataStatic["MusicCollection"] == null)
                {
                    DataStatic["MusicCollection"] = MusicCollection.GetMusicClipCollection();

                }
                return DataStatic["MusicCollection"] as MusicCollection;
            }
            set
            {
                DataStatic["MusicCollection"] = value;
            }
        }

        [WebMethod]
        public static string GetList(int startIndex, int pageSize, string sortExpression)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["data"] = MusicList.GetSegment(startIndex, pageSize, sortExpression);
            result["total"] = MusicList.Count;
            return js.Serialize(result);
        }

        [WebMethod]
        public static void Music_Add()
        {
            Music item = new Music();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new MusicArgs(item, false);
        }

        [WebMethod]
        public static void AddMusic()
        {
            Music music = ((MusicArgs)PSCDialog.DataShare).Music;
            MusicList.AddDB(music);
        }

        [WebMethod]
        public static void Music_Update(string Id)
        {
            Music item = MusicList.Where(v => v.Id == new Guid(Id)).Single();
            PSCDialog.DataShare = new MusicArgs(item, true);
        }

        [WebMethod]
        public static void UpdateMusic()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.MusicArgs).Music.Update();
            DataStatic["MusicCollection"] = null;
        }

        [WebMethod]
        public static void DeleteMusic(string[] arrId)
        {
            foreach (string Id in arrId)
            {
                Music item = MusicList.Where(v => v.Id == new Guid(Id)).Single();
                MusicList.RemoveDB(item);
            }
        }
    }
}