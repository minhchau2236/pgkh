using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PSCPortal.Portlets.TabTopicDisplay
{
    public partial class Edit : Engine.PortletEditControl
    {        
        protected int NumberArticle
        {
            get
            {
                return (int)ViewState["NumberArticle"];
            }
            set
            {
                ViewState["NumberArticle"] = value;
            }
        }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadListTopic();
                LoadData();
            }
        }
        protected void LoadListTopic()
        {
            lbxTopicSource.DataSource = CMS.TopicCollection.GetTopicCollection();
            lbxTopicSource.DataTextField = "Path";
            lbxTopicSource.DataValueField = "Id";
            lbxTopicSource.DataBind();
        }
        protected void LoadData()
        {
            gvListTopic.DataSource = Lib.TabTopicDisplayCollection.GetTabTopicDisplayCollectionByDataId(DataId);
            gvListTopic.DataKeyNames = new[] { "DataId", "TopicId" };
            gvListTopic.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
           Lib.TabTopicDisplayCollection ListTabDisplayTopic = Lib.TabTopicDisplayCollection.GetTabTopicDisplayCollectionByDataId(DataId);
            for (int i = 0; i < lbxTopicSource.Items.Count; i++)
            {
                if (lbxTopicSource.Items[i].Selected)
                {
                    Lib.TabTopicDisplay tabTopic = new Lib.TabTopicDisplay
                    {
                        DataId = DataId,
                        NumberDisplay = txtNumberAirticle.Text != "" ? int.Parse(txtNumberAirticle.Text) : 0,
                        TopicId = new Guid(lbxTopicSource.Items[i].Value)
                    };
                    ListTabDisplayTopic.AddDB(tabTopic);

                }
                LoadData();
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Accept();
        }

        protected void gvListTopic_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Lib.TabTopicDisplayCollection ListTabDisplayTopic = Lib.TabTopicDisplayCollection.GetTabTopicDisplayCollectionByDataId(DataId);
            var dataKey = gvListTopic.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                if (dataKey.Values != null)
                {
                    Guid dataId = new Guid(dataKey.Values[0].ToString());
                    Guid topicId =  new Guid (dataKey.Values[1].ToString());
                    Lib.TabTopicDisplay tabTopic = new Lib.TabTopicDisplay();
                    foreach (Lib.TabTopicDisplay item in ListTabDisplayTopic)
                    {
                        if (item.DataId == dataId && item.TopicId == topicId)
                        {
                            tabTopic = item;

                        }
                    }
                    ListTabDisplayTopic.RemoveDB(tabTopic);
                }
            }
            LoadData();
        }

        protected void gvListTopic_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Lib.TabTopicDisplayCollection ListTabDisplayTopic = Lib.TabTopicDisplayCollection.GetTabTopicDisplayCollectionByDataId(DataId);
            gvListTopic.EditIndex = -1;
            var dataKey = gvListTopic.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                if (dataKey.Values != null)
                {
                    Guid topicId = ((Guid)dataKey.Values[1]);
                    Lib.TabTopicDisplay tab = ListTabDisplayTopic.Single(t => t.TopicId == topicId);
                    int numberTopic = int.Parse(((TextBox)gvListTopic.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
                    int numberOrder = int.Parse(((TextBox)gvListTopic.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
                    tab.NumberDisplay = numberTopic;
                    tab.TabOrder = numberOrder;
                    tab.Update();
                }
            }
            LoadData();
        }

        protected void gvListTopic_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvListTopic.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvListTopic_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvListTopic.EditIndex = -1;
            LoadData();

        }
    }
}