﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" 
Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Article>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["TypeName"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=ViewData["TypeName"] %></h2>
    
    <table class="table_solid fullwidth">
        <tr>
            <th style="width:150px">操作</th>
            <th>
                标题
            </th>
            <th>
                发布时间
            </th>
            <%-- <th>
            内容
            </th>
           <th>
                发布状态
            </th>--%>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td style=" width:200px">
                <%= Html.ActionLink("编辑", "Edit", new { ArticleID = item.ArticleID, TypeID = ViewData["TypeID"] }, new { @class = "btn2" })%> |
                <%= Html.ActionLink("查看", "news", "home", new { id = item.ArticleID }, new { @class = "btn5" })%> |
                <%= Html.ActionLink("删除", "Delete", new { ArticleID = item.ArticleID, TypeID = ViewData["TypeID"] }, new { @class = "btn3" })%>
            </td>
            <td>
                <%= Html.Encode(item.Title) %><div id="div<%=item.ArticleID %>"></div>
            </td>
            
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreateTime)) %>
            </td>
           <%--  <td>
                <%= Html.Encode(item.Content) %>
            </td>
           <td>
                <%= Html.Encode(item.Status) %>
            </td>--%>
        </tr>
    
    <% } %>

    </table>
    <div class="page_list">
         <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
    </div>
    <p>
        <%= Html.ActionLink("新增", "Create", new  {TypeID = ViewData["TypeID"] })%>
    </p>
    <script src="../../Scripts/jquery.tableui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $(".table_solid").tableUI();
        })
	</script>

</asp:Content>

