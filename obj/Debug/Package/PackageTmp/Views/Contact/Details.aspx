<%@ Page Title="留言明细" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Contact>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	留言明细
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>留言明细</h2>

        
        <div class="display-label"><%: Html.LabelFor(model => model.UserName)%></div>
        <div class="display-field"><%: Model.UserName %></div>
        
        <div class="display-label"><%: Html.LabelFor(model => model.Company)%></div>
        <div class="display-field"><%: Model.Company %></div>
        
        <div class="display-label"><%: Html.LabelFor(model => model.Tel)%></div>
        <div class="display-field"><%: Model.Tel %></div>
        
        <div class="display-label"><%: Html.LabelFor(model => model.Email)%></div>
        <div class="display-field"><%: Model.Email %></div>
        
        <div class="display-label"> <%: Html.LabelFor(model => model.Msg)%></div>
        <div class="display-field"><%: Model.Msg %></div>
        
        <div class="display-label"> <%: Html.LabelFor(model => model.CreateTime)%></div>
        <div class="display-field"><%: String.Format("{0:g}", Model.CreateTime) %></div>
        
    <p>

        <%: Html.ActionLink("返回", "Index") %>
    </p>

</asp:Content>

