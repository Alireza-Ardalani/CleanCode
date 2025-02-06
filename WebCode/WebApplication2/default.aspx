<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication2._default" %>



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
<body style="font-family: Tahoma">
    <form id="form1" runat="server">
    
      <!-- Navigation -->
    <nav class="navbar navbar-expand-lg navbar-dark fixed-top" id="mainNav" >
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
                  <a class="nav-link " href="#">کتابخانه</a>
              </li>
              <li class="nav-item">
                  <a class="nav-link " href="#">درباره ما</a>
              </li>
              <li class="nav-item">
                  <a class="nav-link " href="#send">ارسال کد</a>
              </li>
            <li class="nav-item ">
              <a class="nav-link" href="#home">صفه اصلی</a>
            </li>
         
            
         
		
		
			
          </ul>
        </div>
      </div>
    </nav>
      <div>
    <section id="home" class="main-banner parallaxie" style="background: url('uploads/banner-01.jpg')">
		<div class="heading">
			<h1>Welcome to IranCleanCode</h1>			
			<h3 class="cd-headline clip is-full-width">
				
				<span class="cd-words-wrapper">
					<b class="is-visible">توجه</b>
					<b>کد خود را مورد بررسی قرار دهید</b>
					<b>رایگان</b>
					<b>این وب سایت توسط دانشجویان دکتر پارسا طراحی و پیاده سازی شده است</b>
                    <b>قرار دهید</b>
				</span>

				<div class="btn-ber">

                    <a class="get_btn hvr-bounce-to-top" href="#send">ارسال کد</a>
				
				</div>
			</h3>

		</div>

	</section>

    <div id="send" class="section wb">
        <div class="container">
            <div class="row">
                <!-- end col -->

                <div class="col-md-6">
                    <div class="right-box-pro wow fadeIn">
                        <img src="uploads/about_04.png" alt="" class="img-fluid img-rounded"/>
                    </div><!-- end media -->
                </div><!-- end col -->
                <div class="col-md-6">
                    <div class="message-box text-right">                        
                        <h3>IranCleanCode</h3>
                        <p> چگونه می‌توان تفاوت بین کد خوب و بد را بیان کرد

چگونه می‌توان کد خوب نوشت و کد بد را به کد خوب تبدیل کرد

چگونه خوب نام گذاری کنیم، توابع خوب، اشیا خوب و کلاس‌های خوب ایجاد کنیم

چگونه به کد فرمت بدهیم تا به حداکثر خوانایی ممکن برسیم

چگونه مدیریت خطای کاملی بدون مبهم کردن منطق کد را پیاده سازی کرد

چگونه آزمون واحد انجام داد و توسعه آزمون محور را تمرین کرد </p>
						<p>حتی کد بد هم می‌تواند کاربرد داشته باشد. اما اگر کد تمیز نیست، می‌تواند سازمانِ توسعه‌دهنده را به زانو درآورد. هر سال،
                            ساعت بی‌شمار و منابع قابل توجهی به دلیل نوشتن کد بد از دست می‌رود. اما لازم نیست این اتفاق رخ دهد.
رابرت سی. مارتین “عمو باب“ از سال 1970 یک حرفه‌ای نرم افزار و یک مشاور بین المللی نرم افزار از سال 1990 بوده است.
متخصص نرم افزار سرشناس، رابرت سی مارتین، یک پارادایم انقلابی را با کدنویسی تمیز ارائه می‌دهد: کتاب راهنمای توسعه نرم‌افزار به روش چابک
                            .
 </p>
                        <button type="button" class="sim-btn hvr-bounce-to-top" data-toggle="modal" data-target="#myModal">
                            آپلود فایل کد</button>
                    
                        <!-- Modal -->
                        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                      
                                     
                                    </div>
                                    <div class="modal-body">
                                        
                                            <div class="form-group">
                                                <label for="exampleInputEmail1"></label>
                                               
                                            </div>
                                           
                                            <div class="form-group text-left">
                                              
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <asp:Label ID="Label1" runat="server" CssClass=" text-info" Text="فایل کد را انتخاب کنید "></asp:Label>
                                                <p class="help-block"></p>
                                            </div>
                                         
                                            
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                       
                                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="ارسال" OnClick="Button1_Click" />
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                        
                    </div><!-- end messagebox -->
                </div>
            </div><!-- end row -->
        </div><!-- end container -->
    </div><!-- end section -->
           <div class="copyrights">
        <div class="container">
            <div class="footer-distributed">
				
                <div class="footer-center">
                   
                    <p class="footer-company-name">All Rights Reserved. &copy; 2020 <a href="#"></a>milad 
				
            </div>
</div>        </div><!-- end container -->
    </div><!-- end copyrights -->

    <a href="#" id="scroll-to-top" class="dmtop global-radius"><i class="fa fa-angle-up"></i></a>
    </div>
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
<%--  --%>
