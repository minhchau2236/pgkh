<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicTripleDisplay.ascx.cs" Inherits="PSCPortal.Portlets.TopicDisplay.TopicTripleDisplay" %>
<div class="tintuc" id="topicTripleContain" runat="server">
    <div class="topic">
        <div class="L_topic">
            <img alt="" src="/Resources/ImagesPortal/HomePage/Imgs/arrow_yellow.jpg" />
            <a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a>
        </div>
        <!--L_topic-->
    </div>
    <!--topic-->
    <div class="row">
        <div data-bind="foreach: $data">
            <div class="col-md-4 nd_tin">
                <img alt="" class="img_tin" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Article.Id, retina: '/Services/GetArticleImage.ashx?Id=' + Article.Id }" />
                <a href="#" data-bind="html: Article.Title, attr: { href: '/?articleId=' + Article.Id }"></a>
                <p data-bind="html: Description"></p>
            </div>
        </div>

    </div>
    <!--End row-->
</div>
<script type="text/javascript" src="/Portlets/TopicDisplay/System.Utility.TopicTripleDisplay.js"></script>
<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.TopicTripleDisplay('<%#topicTripleContain.ClientID%>', '<%#ListArticle%>'));
    });
</script>
