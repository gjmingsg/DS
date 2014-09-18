<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	详情
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>详情</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%: Model.Title %></div>
        
       
        <div class="display-label">Creator</div>
        <div class="display-field"><%: Model.Creator %></div>
        
        <div class="display-label">CreateTime</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.CreateTime) %></div>
        
        <div class="display-label">Content</div>
        <div class="display-field"><%: Model.Content %></div>
        
        <div class="display-label">Status</div>
        <div class="display-field"><%: Model.Status %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ArticleID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

