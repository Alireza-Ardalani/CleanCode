<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result.aspx.cs" Inherits="WebApplication2.result" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>irancleancode</title>
     <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>   
   
    <!-- Mobile Metas -->
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
 
     <!-- Site Metas -->

    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <!-- Site Icons -->
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="apple-touch-icon" href="images/apple-touch-icon.png"/>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <!-- Site CSS -->
    <link rel="stylesheet" href="style.css"/>
    <!-- Responsive CSS -->
    <link rel="stylesheet" href="css/responsive.css"/>
    <!-- Custom CSS -->
    <link rel="stylesheet" href="css/custom.css"/>
	<script src="js/modernizr.js"></script> <!-- Modernizr -->

    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
  
      <!-- Navigation -->
    <nav class="navbar navbar-expand-lg navbar-dark"  id="mainNav" style="background-color: #3E3E42" >
      <div class="container">
        <a class="navbar-brand js-scroll-trigger" href="#page-top">
				<img class="img-fluid" src="images/logo.png" alt="" style="height:10%;width:40%" />
		</a>
        <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
          Menu
          <i class="fa fa-bars"></i>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive" style="font-family:Tahoma">
          <ul class="navbar-nav text-uppercase ml-auto">

            
              <li class="nav-item">
                  <a class="nav-link " href="default.aspx#">کتابخانه</a>
              </li>
              <li class="nav-item">
                  <a class="nav-link " href="default.aspx#">درباره ما</a>
              </li>
              <li class="nav-item">
                  <a class="nav-link " href="default.aspx#send">ارسال کد</a>
              </li>
            <li class="nav-item ">
              <a class="nav-link" href="default.aspx">صفه اصلی</a>
            </li>
         
            
         
		
		
			
          </ul>
        </div>
      </div>
    </nav>
    <br />
    <div class="container">
    <div class="row">
        <br />
        <div class="panel panel-default danger">
  <!-- Default panel contents -->
  <div class="panel-heading text-center">
            <asp:Label ID="Labtitle" runat="server" Text="CleanCode Principle"></asp:Label>
      <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>


                <asp:GridView ID="GridView1" runat="server"></asp:GridView>


<!-- Modal -->






<!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title" id="myModalLabel">more informationn</h4>
      </div>
      <div class="modal-body">
          <asp:Label ID="Label4" runat="server" Text="Label4"></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      
      </div>
    </div>
  </div>
</div>

  </div></div>
    </div>

           <div class="copyrights fixed-bottom">
        <div class="container">
            <div class="footer-distributed">
				
                <div class="footer-center">
                   
                    <p class="footer-company-name">All Rights Reserved. &copy; 2020 <a href="#"></a> milad 
				
            </div>
</div>        </div><!-- end container -->
    </div><!-- end copyrights -->

    <a href="#" id="scroll-to-top" class="dmtop global-radius"><i class="fa fa-angle-up"></i></a>
  
    </form>
    <!-- ALL JS FILES -->
    <script src="js/all.js"></script>
	<!-- Camera Slider -->
	<script src="js/jquery.mobile.customized.min.js"></script>
	<script src="js/jquery.easing.1.3.js"></script> 
	<script src="js/parallaxie.js"></script>
	<script src="js/headline.js"></script>
	<!-- Contact form JavaScript -->
    <script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>
    <!-- ALL PLUGINS -->
    <script src="js/custom.js"></script>
    <script src="js/jquery.vide.js"></script>
</body>
</html>
