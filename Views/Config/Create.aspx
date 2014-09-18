<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Config>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ConfigID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ConfigID) %>
                <%= Html.ValidationMessageFor(model => model.ConfigID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Key) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Key) %>
                <%= Html.ValidationMessageFor(model => model.Key) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value1) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value1) %>
                <%= Html.ValidationMessageFor(model => model.Value1) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value2) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value2) %>
                <%= Html.ValidationMessageFor(model => model.Value2) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value3) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value3) %>
                <%= Html.ValidationMessageFor(model => model.Value3) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Value4) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Value4) %>
                <%= Html.ValidationMessageFor(model => model.Value4) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

