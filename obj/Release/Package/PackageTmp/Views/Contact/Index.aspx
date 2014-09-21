<%@ Page Title="客户留言" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" 
Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Contact>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	客户留言
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>客户留言</h2>

     <table class="table_solid fullwidth">
        <tr>
            <th>操作</th>
            <th>
                客户名
            </th>
            <th>
                公司
            </th>
            <th>
                电话
            </th>
            <th>
                邮箱
            </th>
            <th>
                留言
            </th>
            <th>
                留言时间
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("查看", "Details", new { id=item.ContactID })%> |
                <%: Html.ActionLink("删除", "Delete", new { id=item.ContactID })%>
            </td>
            <td>
                <%: item.UserName %>
            </td>
            <td>
                <%: item.Company %>
            </td>
            <td>
                <%: item.Tel %>
            </td>
            <td>
                <%: item.Email %>
            </td>
            <td>
                <%: item.Msg %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.CreateTime) %>
            </td>
           
        </tr>
    
    <% } %>

    </table>
     <div class="page_list">
         <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
    </div>
   
    
     <script src="../../Scripts/jquery.tableui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $(".table_solid").tableUI();
        })
	</script>

</asp:Content>

