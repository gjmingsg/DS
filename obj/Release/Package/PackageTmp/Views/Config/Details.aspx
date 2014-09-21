<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Config>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>配置详细信息</h2>
 
       
        <div class="display-label">配置名称：</div>
        <div class="display-field"><%: Model.Name %></div>
        <br />
        <div class="display-label">值1：</div>
        <div class="display-field"><%: Model.Value1 %></div>
        <br />
        <div class="display-label">值2：</div>
        <div class="display-field"><%: Model.Value2 %></div>
        <br />
        <div class="display-label">值3：</div>
        <div class="display-field"><%: Model.Value3 %></div>
        <br />
        <div class="display-label">值4：</div>
        <div class="display-field"><%: Model.Value4 %></div>
        <br />
   
    <p>

        <%: Html.ActionLink("编辑", "Edit", new { id = Model.ConfigID, srcPage = ViewData["index"] })%> |
        <%: Html.ActionLink("返回", ViewData["index"].ToString())%>
    </p>

</asp:Content>

