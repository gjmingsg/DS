<%@ Page Title="广州市涅普敦进出口贸易有限公司" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master"
 Inherits="System.Web.Mvc.ViewPage<MvcPaging.IPagedList<Nepton.Models.NT_Article>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	广州市涅普敦进出口贸易有限公司--产品中心
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <img src="../../Images/product_tip.jpg" alt="产品中心" />
     <hr />
     <br />
     <%
         var RedTypeList = ViewData["RedTypeList"] as List<Nepton.Models.NT_ArticleType>;
         var WhiteTypeList = ViewData["WhiteTypeList"] as List<Nepton.Models.NT_ArticleType>;
         var PicList = ViewData["PicList"] as List<Dictionary<string, string>>;
         var BigTypeID = ViewData["BigTypeID"];
         Guid? typeid = null;
         if (RedTypeList != null)
         {
          %>
    <div class="type-head">
        <div  class="type-txt">红酒</div>
        <ul>
     <%
            
             
             if (ViewData["TypeID"] != null)
                 typeid = Guid.Parse(ViewData["TypeID"].ToString());
             foreach (var item in RedTypeList)
             { %>
        <li  class="address-txt">
            <a href='<%=Url.Action("ProductList","home",new {TypeID=item.TypeID,BigTypeID=BigTypeID}) %>'><% =item.TypeName%></a>
            <%if (typeid.HasValue && item.TypeID == typeid.Value)
              { %>
              <img  src="../../Images/tree_dnd_yes.png" alt="选中"/>
            <%} %>
        </li>
    <%} %>
    </ul>
    </div>
    <br />
    <%}
        if (WhiteTypeList != null)
        {
      %>
    <div  class="type-head">
        <div class="type-txt">白兰地</div>
        <ul>
    <%
        
            foreach (var item in WhiteTypeList)
            { %>
        <li  class="address-txt">
        <a href='<%=Url.Action("ProductList","home",new {TypeID=item.TypeID,BigTypeID=BigTypeID}) %>'><% =item.TypeName%></a>
        <%if (typeid.HasValue && item.TypeID == typeid.Value)
          { %>
              <img  src="../../Images/tree_dnd_yes.png" alt="选中"/>
            <%} %>
        </li>
    <%} %>
    </ul>
    </div>
   <%} %>
    <div class="clearfix"></div>
    <div class="product">
        <%
            if (Model.TotalItemCount == 0)
            {
                %>
                <div class="product-empty">对不起，暂时无该产品信息，如果有其他需求，可以给我们<%=Html.ActionLink("留言", "contact", "home") %><img alt="给我们留言" src="../../Images/mail-icon.png" /></div>
                <%}
            foreach (var item in Model){%>
           <div class="product-item">
                 <a href='<%=Url.Action("ProductDetail","home", new { id=item.ArticleID }) %>' onclick='return GetProductInfo("<%=item.ArticleID %>")'>
                 <%
                     var tId = item.ArticleID.ToString();

                     if (PicList!=null && PicList.Any(p => p["ArticleID"] == tId))
                     {
                        var url = PicList.Where(p => p["ArticleID"] == tId).FirstOrDefault()["Url"];
                     %>
                      <img src="<%=url %>"  alt="查看<%= Html.Encode(item.Title) %>详情点击进入" style=" width:200px; height:179px"/>
                     <%}
                     else if (WhiteTypeList!= null && WhiteTypeList.Any(p => p.TypeID == item.NT_ArticleType.TypeID))
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
       <script type="text/javascript">
           function GetProductInfo(id) {
               BindData(function (json) {
                   openNewDiv("ShowBoxID", json['Content']);
               }, {DatailId:id}, '<%=Url.Action("ProductDetailAjax","home") %>');
               return false;
           }
           var docEle = function () {
               return document.getElementById(arguments[0]) || false;
           }
           function openNewDiv(_id,text) {
               var m = "mask";
               if (docEle(_id)) document.removeChild(docEle(_id));
               if (docEle(m)) document.removeChild(docEle(m));
               //新激活图层
               var newDiv = document.createElement("div");
               newDiv.id = _id;
               newDiv.style.position = "absolute";
               newDiv.style.zIndex = "999";
               newDiv.style.width = parseInt(document.body.scrollWidth) -400 + "px";
               newDiv.style.height = "500px";
               newDiv.style.top =$(window).scrollTop()+100 +"px";
               newDiv.style.left = 400 / 2 + "px"; // 屏幕居中
               newDiv.style.background = "white";
               newDiv.style.border = "1px solid gray";
               newDiv.style.padding = "5px";
               newDiv.innerHTML = "<div id='ajax-txtbox'>" +text +"</div>";
               document.body.appendChild(newDiv);
               // mask图层
               var newMask = document.createElement("div");
               newMask.id = m;
               newMask.style.position = "absolute";
               newMask.style.zIndex = "1";
               newMask.style.width = document.body.scrollWidth + "px";
               newMask.style.height = document.body.scrollHeight + "px";
               newMask.style.top = "0px";
               newMask.style.left = "0px";
               newMask.style.background = "#000";
               newMask.style.filter = "alpha(opacity=40)";
               newMask.style.opacity = "0.40";
               document.body.appendChild(newMask);
               // 关闭mask和新图层
               var newA = document.createElement("a");
               newA.href = "#";
               newA.innerHTML = "<div style='position:absolute;top:10px;right:10px'>X</div>";
               newA.onclick = function () {
                   document.body.removeChild(docEle(_id));
                   document.body.removeChild(docEle(m));
                   return false;
               }
               newMask.onclick = function () {
                   document.body.removeChild(docEle(_id));
                   document.body.removeChild(docEle(m));
                   return false;
               }
               newDiv.appendChild(newA);
           }
       </script>
</asp:Content>

