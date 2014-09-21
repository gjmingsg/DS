<%@ Page Title="广州市涅普敦进出口贸易有限公司" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master"
 Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Article>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	广州市涅普敦进出口贸易有限公司--产品中心
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <img src="../../Images/product_tip.jpg" alt="产品中心" />
     <hr />
     <br />
    <div class="type-head">
        <div  class="type-txt">红酒</div>
        <ul>
     <%
           Guid? typeid = null;
         if(ViewData["TypeID"] !=null)
             typeid = Guid.Parse(ViewData["TypeID"].ToString());
         var RedTypeList = ViewData["RedTypeList"] as List<Nepton.Models.NT_ArticleType>;
         var WhiteTypeList = ViewData["WhiteTypeList"] as List<Nepton.Models.NT_ArticleType>;
         foreach (var item in RedTypeList)
       { %>
        <li  class="address-txt">
            <a href='<%=Url.Action("ProductList","home",new {TypeID=item.TypeID}) %>'><% =item.TypeName %></a>
            <%if (typeid.HasValue&&item.TypeID == typeid.Value)
              { %>
              <img  src="../../Images/tree_dnd_yes.png" alt="选中"/>
            <%} %>
        </li>
    <%} %>
    </ul>
    </div>
    <br />
    <div  class="type-head">
        <div class="type-txt">白兰地</div>
        <ul>
    <%
        
        foreach (var item in WhiteTypeList)
      { %>
        <li  class="address-txt">
        <a href='<%=Url.Action("ProductList","home",new {TypeID=item.TypeID}) %>'><% =item.TypeName %></a>
        <%if (typeid.HasValue && item.TypeID == typeid.Value)
              { %>
              <img  src="../../Images/tree_dnd_yes.png" alt="选中"/>
            <%} %>
        </li>
    <%} %>
    </ul>
    </div>
   
    <div class="clearfix"></div>
    <div class="product">
        <%
            if (Model.TotalItemCount == 0)
            {
                %>
                <div class="product-empty">对不起，暂时无该产品信息，如果有其他需求，可以给我们<%=Html.ActionLink("留言", "contact", "home") %></div>
                <%}
            foreach (var item in Model){%>
           <div class="product-item">
                 <a href='<%=Url.Action("ProductDetail","home", new { id=item.ArticleID }) %>'>
                 <%if (WhiteTypeList.Any(p => p.TypeID == item.NT_ArticleType.TypeID))
                   { %>
                    <img src="../../Images/white.jpg"  alt="查看<%= Html.Encode(item.Title) %>详情点击进入"/>
                    <%}
                   else
                   { %>
                    <img src="../../Images/red.jpg" alt="查看<%= Html.Encode(item.Title) %>详情点击进入" />
                    <%} %>
                 </a>   
                 <div>
                    <a href='<%=Url.Action("ProductDetail","home", new { id=item.ArticleID }) %>'>
                        <%= Html.Encode(item.Title) %>
                    </a>
                </div>
           </div>
        <% } %>
   </div>
   <br />
   <div class="clearfix"></div>
    <div class="page_list">
            <%= Html.Pager(Model.PageSize,Model.PageNumber,Model.TotalItemCount).ToHtmlString() %>
     </div>
       
</asp:Content>

