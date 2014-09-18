<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
 Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	添加<%=ViewData["TypeName"]%>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>添加<%=ViewData["TypeName"]%></h2>
   
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
  
             <p class="txt_r" style=" margin-right:400px">
                <input type="submit" value="保存" />
            </p>
          
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Title) %>
                <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
             <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("Status",(ViewData["ddStatus"] as List<SelectListItem>),"") %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
           
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Content) %>
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.Content) %>
                <%: Html.ValidationMessageFor(model => model.Content) %>
            </div>
            
            
            <p class="txt_r" style=" margin-right:400px">
                <input type="submit" value="保存" />
            </p>
         

    <% } %>

    
    <script src="../../Content/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="../../Content/ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editorOption = {
            initialFrameWidth: 900,
            initialFrameHeight: 300
        };
        var editor = new baidu.editor.ui.Editor(editorOption);
        editor.render('Content');
        $('form').submit(function () {
            $('#Content').val(editor.getContent());
        });
    </script>
</asp:Content>

