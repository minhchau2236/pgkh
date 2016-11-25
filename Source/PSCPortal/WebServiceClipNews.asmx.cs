using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;

namespace PSCPortal
{
    /// <summary>
    /// Summary description for WebServiceClipNews
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebServiceClipNews : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
  
        [WebMethod]
        public object GetClips()
        {
            PSCPortal.CMS.ClipNewCollection listClip = PSCPortal.CMS.ClipNewCollection.GetClipNewCollectionAll();

            List<PSCPortal.CMS.ClipNew> list = new List<PSCPortal.CMS.ClipNew>();

            for (int i = 0; i < listClip.Count; i++)
            {
                PSCPortal.CMS.ClipNew item = new PSCPortal.CMS.ClipNew();
                item.Id = listClip[i].Id;
                item.Name = listClip[i].Name;
                item.Link = listClip[i].Link.Substring(1);
                item.Description = listClip[i].Description;

                list.Add(item);
            }


            return list;
        }

        //[WebMethod]
        //public object GetListClipSongs(string id)
        //{
        //    PSCPortal.CMS.ClipSongsCollection listClip = PSCPortal.CMS.ClipSongsCollection.GetClipSongsCollection(new Guid(id));
        //    PSCPortal.CMS.ClipSongsCollection list = new PSCPortal.CMS.ClipSongsCollection();
        //    for (int i = 0; i < listClip.Count; i++)
        //    {
        //        PSCPortal.CMS.ClipSongs item = new PSCPortal.CMS.ClipSongs();
        //        item.Id = listClip[i].Id;
        //        item.ClipName = listClip[i].ClipName;
        //        item.ClipLink = listClip[i].ClipLink;
        //        item.Note = listClip[i].Note;
        //        list.Add(item);
        //    }
        //    //  System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    //   string chuoi = js.Serialize(list);
        //    return list;

        //}
        //[WebMethod]
        //public string GetListNotes(string id)
        //{
        //    PSCPortal.CMS.ClipSongs listClip = PSCPortal.CMS.ClipSongs.GetClipSongs(new Guid(id));
        //    string chuoi = listClip.Note;

        //    return chuoi;
        //}       
    }
}
