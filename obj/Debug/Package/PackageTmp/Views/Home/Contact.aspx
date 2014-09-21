<%@ Page Title="联系我们" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Contact>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	联系我们
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
   <div class="txt-box">
     
     <h2 style="text-align:center;margin-bottom:20px">
        <img src="../../Images/title-line-bar.jpg" alt="标题"/>
        <br />Contact From
     </h2>
    <%Html.EnableClientValidation(true);%>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <div class="contact-base"> 
            <div class="editor-label">
                <%= Html.LabelFor(model => model.UserName) %>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.UserName) %>
                <%= Html.ValidationMessageFor(model => model.UserName) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Company) %>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Company) %>
                <%= Html.ValidationMessageFor(model => model.Company) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Tel) %>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Tel) %>
                <%= Html.ValidationMessageFor(model => model.Tel) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Email) %>：
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Email) %>
                <%= Html.ValidationMessageFor(model => model.Email) %>
            </div>
        </div>

        <div class="contact-detail">
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Msg) %>：
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Msg,6,20,null) %>
                <%= Html.ValidationMessageFor(model => model.Msg) %>
            </div>
            
            <p style="margin-top:15px">
                <input type="submit" value="提交" style=" background-color:#99906c; border:0px; color:White" />
            </p>
         </div>

    <% } %>
   
    <div class="contact-way">
     <img src="../../Images/contact_3.png" style="margin:20px 0px 10px 180px;" />
     <div class="contact-txt">
        <p><b>地址：</b><%=ViewData["Address"] %></p>
        <p><b>电话：</b><%=ViewData["Phone"] %></p>
        <p><b>邮箱：</b><a href="mailto:<%=ViewData["Mail"] %>"><%=ViewData["Mail"] %></a></p>
     </div>
    </div>
   </div>
   <div class="pic-box">
    <img src="../../Images/contact_1.jpg" class="pic-item" />
    <img src="../../Images/contact_2.jpg"  class="pic-item"/>
   </div>
 
</asp:Content>

