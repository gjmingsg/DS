<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!doctype html>
<html>
<head>
<meta charset="utf-8" />
<meta name="renderer" content="webkit" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="keywords" content='广州市涅普敦进出口贸易有限公司,美酒' />
<meta name="description" content='广州市涅普敦进出口贸易有限公司,美酒' />
<title>广州市涅普敦进出口贸易有限公司</title>
<%--<link rel='' href="../../Images/logoico.png"  type="image/x-icon" />--%>
<link rel="stylesheet" href="../../content/reset.css" />
<link rel="stylesheet" href="../../content/main.css" />
</head>
<body class="bg">
 <div class="header">
	<div class="head-nav m"  style="margin-top:35px">
        
        
    	 <ul class="clearfix">
    	    <li class="nav-img"><img src="../../images/leftnav.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href='<%=Url.Action("index","home")%>'><img src="../../images/Home.jpg" alt="首页" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg"  alt=""/></li>
    	 	<li class="nav-img"><a href='<%=Url.Action("company","home")%>'><img src="../../images/company.jpg" alt="涅普敦" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img sec-menu" >
                <a href='<%=Url.Action("Occastion","home") %>'>
                    <img src="../../images/Occastion.jpg" alt="场合选酒" />
                </a>
                
                <div class="list">
		            <a href="<%=Url.Action("Occastion","home",new {TypeID=Nepton.Controllers.HomeController.WeddingId})%>">婚宴推荐</a><br />
                    <hr />
                    <a href="<%=Url.Action("Occastion","home",new {TypeID=Nepton.Controllers.HomeController.BusinessId})%>">商务宴请</a><br />
	            </div>

            </li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
            <li class="nav-img sec-menu">
                <a href='<%=Url.Action("ProductList","home") %>'>
                    <img src="../../images/Product.jpg" alt="产品中心" />
                </a>
                <div class="list">
		            <a href="<%=Url.Action("ProductList","home",new {BigTypeID=Nepton.Controllers.HomeController.RedId})%>">红酒</a><br />
                    <hr />
                    <a href="<%=Url.Action("ProductList","home",new {BigTypeID=Nepton.Controllers.HomeController.WhiteId})%>">白兰地</a><br />
	            </div>
            </li>
            <li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
            <li class="nav-img"><a href='<%=Url.Action("join","home")%>'><img src="../../images/Join.jpg" alt="招商加盟" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href='<%=Url.Action("culture","home")%>'><img src="../../images/Culture.jpg" alt="美酒文化" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href='<%=Url.Action("Contact","home")%>'><img src="../../images/Contact.jpg" alt="联系我们" /></a></li>
       		<li class="nav-img"><img src="../../images/rightnav.jpg" alt=""/></li>
    	 </ul>
          
    </div>
 </div>
 <div class="sub-header"></div>
    <div class="content" style="margin-top:130px">
 	<div class="menu lfloat">
 		<div class="submenu">
            <div class="menu1">
              <img src="../../images/logo2.jpg" class="pic1" alt="logo"/>
              <a href='<%=Url.Action("ProductList","home") %>'>
                <img src="../../images/Product_link.jpg" class="pic2" alt="产品中心"/>
              </a>
               <a href='<%=Url.Action("Join","home") %>'>
                <img src="../../images/Join_link.jpg" class="pic2" alt="招商加盟"/>
              </a>
            </div>
        </div>
 		<div class="submenu">
            <div class="menu2">
      
              <img src="../../Images/contact_tip.jpg" class="pic1" alt="联系我们"/>
              <div class="contact-txt">
                  <div><%=ViewData["Address"] %></div>
                  <div>Tel:<%=ViewData["Phone"]%></div>
                  <div>E-mail:<a href="mailto:<%=ViewData["Mail"] %>"><%=ViewData["Mail"]%></a></div>
              </div>
            </div>
        </div>
        <div class="friendlink">
            <div>
                <img src="../../images/Recommend_Link.jpg" alt="本月推荐"/>
                 <a href="<%=Url.Action("NewsMore","home",new {TypeID=Nepton.Controllers.HomeController.BBS}) %>"  class="rfloat" style="margin-top:15px;">
                   more>>
                 </a>
            </div>
            <%
                var bbs = ViewData["BBS"] as List<Nepton.Models.NT_Article>;
                foreach (var item in bbs)
                {  %>
                
            <div >
             <a  href='<%=Url.Action("news","home",new {id=item.ArticleID} ) %>'><%=item.Title %></a>
            </div>
            <% }%>
            
        </div>
 	</div>
 	<div class="main lfloat">
 		
        <!--start-->
        <div class="content_show lfloat">
        	<div class="wen_play" id="content_play">
                <div class="wen_play_body">
                    <ul class="wen_play_box clearfix">
                     <%
                         var piclist = (ViewData["ScrollPic"] as List<Nepton.Models.NT_Config>);
                         foreach (var item in piclist)
                        {  %>
                        <li class="play_list" style="display: none; position: absolute;">
                        
                        <a href="<%=item.Value3 %>">
                        
                        	<img alt="<%=item.Value1 %>" src="../..<%=item.Value2 %>" />
                        </a>
                            <div class="show_txt"><p><%=item.Value1 %></p></div>
                        </li>
                        <%} %>
                    </ul>
                </div>
			<div class="wen_page"><%for (int i = 1; i <= piclist.Count; i++){%><span><%=i %></span><%}%></div></div>
        </div>
        <!--end-->
 		
        <div class="News lfloat">
            <table>
            <thead >
                <tr>
                <th><img src="../../images/newstitle.jpg" alt="公司新闻" class="lfloat"/></th>
                <th>
                    <a href="<%=Url.Action("NewsMore","home") %>"  class="rfloat">
                        <img src="../../images/more.jpg" alt="更多"/>
                    </a>
                </th>
                </tr>
            </thead>
            <tbody>
                <%foreach (var item in (ViewData["News"] as List<Nepton.Models.NT_Article>))
                  {%>
                <tr>
                    <td><a href='<%=Url.Action("news","home",new {id=item.ArticleID} ) %>'><%=item.Title %></a></td>
                    <td><%=String.Format("{0:yyyy-MM-dd}", item.CreateTime) %></td>
                </tr>
                 <%} %>
            </tbody>
            
            </table>
        </div>

        <div class="Video lfloat">
            <div>
                <%--<embed class="edui-faked-video" pluginspage="http://www.macromedia.com/go/getflashplayer" src="http://player.youku.com/player.php/sid/XNzM1NjkxMTE2/v.swf" type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="never" menu="false" loop="false" play="true" wmode="transparent" />--%>
                <%=ViewData["Video"]%>
            </div>
            <div>
                <a href='<%=Url.Action("video","home") %>'>
                <img src="../../images/videobar.jpg" alt="" width="335px"/>
                </a>
            </div>
        </div>
 	</div>
 </div>
    <div class="clearfix"></div>
    <div class="foot">
    <div style="height:8px;"><img src="../../images/bottom_line.png"/></div>
    <div class="foot-bg">
        <div class="companyname lfloat">
            <img src="../../images/companyname.png"/>
        </div>
        <div class="companyaddress lfloat">
            <p>Add:<%=ViewData["Address"] %></p>
            <p>Tel:<%=ViewData["Tel"] %></p>
        </div>
    </div>
 </div>
<script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript" src="../../Scripts/jquery.wenplay.js"></script>
<script type="text/javascript">
    //内容1区图片切换
    $("#content_play").wen_play({
        show_n: "1",
        page_show: "num_opacity",
        speed: 1000,
        autospeed: 4000,
        autoplay: true
    });
    $(function () {
        var h = $(document).height();
        h = (h > $(window).height() ? h : $(window).height());
        $('.bg').height(h);

        $('.head-nav>ul>li>a>img').each(function () {
            var src = $(this).attr('src');

            $(this).mouseover(function () {
                $(this).attr('src', src.replace('.jpg', '_index_selected.jpg'));
            });
            $(this).mouseout(function () {
                $(this).attr('src', src.replace('_index_selected.jpg', '.jpg'));
            });
        });

        $('.pic2').each(function () {
            var src = $(this).attr('src');
            $(this).mouseover(function () {
                $(this).attr('src', src.replace('.jpg', '_selected.jpg'));
            });
            $(this).mouseout(function () {
                $(this).attr('src', src.replace('_selected.jpg', '.jpg'));
            });
        });

        $('.sec-menu').mouseover(
            function () {
                var _list = $(this).find('.list');
                _list.show();
                setTimeout(function () { _list.hide(); }, 1000 * 5);
            }
         ).find('.list').mouseout(
            function () {
                $(this).hide();
            }
         );


    });
</script>

</body>
</html>
