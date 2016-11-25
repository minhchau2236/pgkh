using System;
using System.Collections.Generic;
using System.Linq;
using PSCPortal.CMS;
using Telerik.Web.UI;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.TabTopicDisplay
{
    public partial class Display_Khoa : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTabControl();
        }

        private void LoadTabControl()
        {
            Lib.TabTopicDisplayCollection listTabTopicDisplay = Lib.TabTopicDisplayCollection.GetTabTopicDisplayCollection();   
            List<Lib.TabTopicDisplay> listDisplayTopic = listTabTopicDisplay.Where(item => item.DataId == Portlet.PortletInstance.Id).ToList();
            radTabTopicDisplay.Tabs.Clear();
            radMultiPageTopic.PageViews.Clear();
            for (int i = 0; i < listDisplayTopic.Count; i++)
            {
                var temp = Topic.GetTopic(listDisplayTopic[i].TopicId.ToString());
                if (temp == null)
                    return;
                RadTab tab = new RadTab {Text = temp.Name};
                radTabTopicDisplay.Tabs.Add(tab);
                RadPageView pvTopic = new RadPageView {ID = "pvTopic" + i};
                radMultiPageTopic.PageViews.Add(pvTopic);
                const string userControlName = "Portlets/TabTopicDisplay/Lib/DisplayArticleTopic_Khoa.ascx";
                Lib.DisplayArticleTopic_Khoa userControl = (Lib.DisplayArticleTopic_Khoa)Page.LoadControl(userControlName);
                pvTopic.Controls.Add(userControl);
                userControl.Topic = temp;
                userControl.Number = listDisplayTopic[i].NumberDisplay;
                userControl.LoadData();
            }
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletTabTopicDisplay Where DataId=@dataId";
                com.ExecuteNonQuery();
            }
        }
    }
}