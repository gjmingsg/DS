<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
 Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Config>>" %>
<%@ Import Namespace="MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th>操作</th>
            
            <th>
                配置项
            </th>
            <th>
                值1
            </th>
            <th>
                值2
            </th>
            <th>
                值3
            </th>
            <th>
                值4
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("编辑", "Edit", new { id=item.ConfigID }) %> |
                <%= Html.ActionLink("查看", "Details", new { id=item.ConfigID })%> |
                <%= Html.ActionLink("删除", "Delete", new { id=item.ConfigID })%>
            </td>
            
            <td>
                <%= Html.Encode(item.Key) %>
            </td>
            <td>
                <%= Html.Encode(item.Value1) %>
            </td>
            <td>
                <%= Html.Encode(item.Value2) %>
            </td>
            <td>
                <%= Html.Encode(item.Value3) %>
            </td>
            <td>
                <%= Html.Encode(item.Value4) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <div class="pagination">
        <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
    </div>
    <p>
        <%= Html.ActionLink("添加新配置项", "Create") %>
    </p>

</asp:Content>

