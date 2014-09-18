<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<!doctype html>
<html>
<head>
<meta charset="utf-8" />
<meta name="renderer" content="webkit" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="keywords" content='' />
<meta name="description" content='' />
<title>广州市涅普敦进出口贸易有限公司</title>
<link rel='' href="../../Images/logoico.png"  type="image/x-icon" />
<link rel="stylesheet" href="../../content/reset.css" />
<link rel="stylesheet" href="../../content/main.css" />
</head>
<body class="bg">
 <div class="header">
	<div class="head-nav m">
        <div style="position:absolute;left:50%;margin-left:-59px;z-index:20"><img src="../../images/logo.jpg"/></div>
        <img src="../../images/left_top_line.jpg" style="margin-top:35px"/>
        <img src="../../images/right_top_line.jpg" style="margin-left:113px;margin-top:35px" />
    	 <ul class="clearfix">
    	    <li class="nav-img"><img src="../../images/leftnav.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href="/home/index"><img src="../../images/Home.jpg" alt="首页" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg"  alt=""/></li>
    	 	<li class="nav-img"><a href="/home/company"><img src="../../images/company.jpg" alt="涅普敦" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href="/home/productlist"><img src="../../images/Product.jpg" alt="产品中心" /></a></li>
    	 	<li><div style="width:117px"></div></li>
    	 	<li class="nav-img"><a href="/home/join"><img src="../../images/Join.jpg" alt="招商加盟" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href="/home/culture"><img src="../../images/Culture.jpg" alt="美酒文化" /></a></li>
    	 	<li class="nav-img"><img src="../../images/gap.jpg" alt=""/></li>
    	 	<li class="nav-img"><a href="/Home/Contact"><img src="../../images/Contact.jpg" alt="联系我们" /></a></li>
       		<li class="nav-img"><img src="../../images/rightnav.jpg" alt=""/></li>
    	 </ul>
         <img src="../../images/left_bottom_line.jpg"  alt=""/>
         <img src="../../images/right_bottom_line.jpg" style="margin-left:113px;"  alt=""/>
    </div>
 </div>
 <div class="sub-header"></div>
    <div class="content" style="margin-top:30px">
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
              <%--  <img src="../../images/contact_detail.jpg" class="pic1"/>--%>
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
                <img src="../../images/friend_line.jpg" alt="友情链接"/>
            </div>
            <div>
            <%=Html.DropDownList("Link")%>
            </div>
        </div>
 	</div>
 	<div class="main lfloat">
 		
        <!--start-->
        <div class="content_show lfloat">
        	<div class="wen_play" id="content_play">
                <div class="wen_play_body">
                    <ul class="wen_play_box clearfix">
                     <% foreach (var item in (ViewData["ScrollPic"] as List<Nepton.Models.NT_Config>))
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
			<div class="wen_page"><span>1</span><span>2</span><span>3</span><span>4</span><span>5</span><span>6</span></div></div>
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
    });
</script>

</body>
</html>
