<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
 Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Config>>" %>
<%@ Import Namespace="MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>详情信息</h2>

  <table class="table_solid fullwidth">
        

    <% foreach (var item in Model) { %>
    
        <tr>
            <td style=" width:200px">
                <%= Html.ActionLink("编辑", "Edit", new { id = item.ConfigID, srcPage = ViewData["index"] }, new { @class = "btn2" })%> |
                <%= Html.ActionLink("查看", "Details", new { id = item.ConfigID, srcPage = ViewData["index"] }, new { @class = "btn5" })%> |
                <%= Html.ActionLink("删除", "Delete", new { id = item.ConfigID, srcPage=ViewData["index"] }, new { @class = "btn3" })%>
            </td>
            
            <%if (item.Value1 != null)
              { %>
            <td>
                <%= Html.Encode(item.Value1)%>
            </td>
            <%} %>

              <%if (item.Value2 != null)
              { %>
            <td>
                <%= Html.Encode(item.Value2) %>
            </td>
              <%} %>

                <%if (item.Value3 != null)
              { %>
            <td>
                <%= Html.Encode(item.Value3) %>
            </td>
              <%} %>

                <%if (item.Value4 != null)
              { %>
            <td>
                <%= Html.Encode(item.Value4) %>
            </td>
             <%} %>
        </tr>
    
    <% } %>

    </table>
    <div class="pagination">
        <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
    </div>
    <%if (!"OtherIndex".Equals(ViewData["index"].ToString()))
      { %>
    <p>
        <%= Html.ActionLink("添加新配置项", "Create", new { srcPage = ViewData["index"] })%>
    </p>
    <%} %>
     <script src="../../Scripts/jquery.tableui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $(".table_solid").tableUI();
        })
	</script>
</asp:Content>

