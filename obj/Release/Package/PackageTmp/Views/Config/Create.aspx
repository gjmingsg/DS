<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Config>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	添加配置项
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>添加配置项</h2>

    <% using (Html.BeginForm("Create", "Config", new { srcPage = ViewData["index"] }))
       {%>
        <%= Html.ValidationSummary(true)%>

         
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name)%>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name)%>
                <%= Html.ValidationMessageFor(model => model.Name)%>
            </div>
            
          
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value1)%>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value1)%>
                <%= Html.ValidationMessageFor(model => model.Value1)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value2)%>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value2)%>
                <%= Html.ValidationMessageFor(model => model.Value2)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value3)%>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value3)%>
                <%= Html.ValidationMessageFor(model => model.Value3)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value4)%>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value4)%>
                <%= Html.ValidationMessageFor(model => model.Value4)%>
             
            </div>
            
            <p>
                <input type="submit" value="保存" />
            </p>
     

    <% } %>

    <div>
        <%= Html.ActionLink("返回", ViewData["index"].ToString())%>
    </div>

</asp:Content>

