<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div>
     
     <h2 style="text-align:center;margin-bottom:20px">
        <img src="../../Images/title-line-bar.jpg" alt="标题"/>
        <br /> <%= Html.Encode(Model.Title) %>
     </h2>

         
        <div> 
            <%= Model.Content %>
        </div>
   </div>

    <p>
        <%= Html.ActionLink("返回首页", "Index") %>
    </p>

</asp:Content>

