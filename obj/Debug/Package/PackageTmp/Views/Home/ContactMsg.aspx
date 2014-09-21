<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["msg"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2  style="background-color:Silver;font: normal 14px Verdana;"><%=ViewData["msg"] %></h2>
    <%=Html.ActionLink("返回","Contact") %>
</asp:Content>
