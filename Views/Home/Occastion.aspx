﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" 
Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Article>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["TypeName"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 
     <div class="NewsMore lfloat">
            <table>
            <thead >
                <tr>
                <th>
                
                <h2 class="lfloat" style="margin-left:10px">	<%=ViewData["TypeName"] %></h2></th>
                <th>
                    
                </th>
                </tr>
            </thead>
            <tbody>
                <%foreach (var item in Model)
                  {%>
                <tr>
                    <td><a href='<%=Url.Action("news","home",new {id=item.ArticleID} ) %>'><%=item.Title %></a></td>
                    <td><%=String.Format("{0:yyyy-MM-dd}", item.CreateTime) %></td>
                </tr>
                 <%} %>
            </tbody>
            
            </table>
        </div>
         
       
         <div class="page_list">
            <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
         </div>
       

</asp:Content>

