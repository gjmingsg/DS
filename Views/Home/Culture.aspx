<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="txt-box">
     
     <h2 style="text-align:center;margin-bottom:20px">
        <img src="../../Images/title-line-bar.jpg" alt="标题"/>
        <br /> <%= Html.Encode(Model.Title) %>
     </h2>
        <div class="contact-base txt"> 
            <%= Model.Content %>
        </div>
   </div>
   <div class="pic-box">
    <img src="../../Images/culture_1.jpg" class="pic-item" />
    <img src="../../Images/culture_2.jpg"  class="pic-item"/>
   </div>
</asp:Content>

