<%@ Page Title="广州市涅普敦进出口贸易有限公司新闻" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<Nepton.Models.NT_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div>
     
     <h2 style="text-align:center;margin-bottom:20px">
        <img src="../../Images/title-line-bar.jpg" alt="标题"/>
        <br /> <%= Html.Encode(Model.Title) %>
     </h2>

        <div class="txt"> 
            <%= Model.Content %>
        </div>
        
        <br/>
        <br/>
        <br/>

        <div class="txt-foot">
            <div>作者：<%: Model.Creator %></div>
            <div>发布时间：<%: String.Format("{0:yyyy-MM-dd}", Model.CreateTime) %></div>
        </div>
   </div>

       
        
</asp:Content>

