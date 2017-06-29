<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menuctrl.ascx.cs" Inherits="secure_Menuctrl" %>

<ul id="adminblk" runat="server" visible="false"  class="ws_css_cb_menu">          
   <li>&nbsp;&nbsp;&nbsp;</li>
  <li><a href="/secure/Home.aspx" title="Home">Home</a></li>
  <li><a href="#" title=""><span>Maintenance</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>    
<li><a href="/secure/Country/Browse_Country.aspx?search=" title="">Countries</a></li>
<li><a href="/secure/Documents/Browse_Documents.aspx?search=&t1=0" title="">Documents</a></li>
<li><a href="/secure/EducationProgram/Browse_DegreePlan.aspx?search=&t1=0&t2=0" title="">Education Program</a></li>
<li><a href="/secure/Gradescale/Browse_Gradescale.aspx?search=&t1=0" title="">Gradescale</a></li>  
<li><a href="/secure/Major/Browse_Major.aspx?search=&t1=0&t2=0" title="">Major/Emphasis</a></li>  
<li><a href="/secure/Institution/Browse_Institution.aspx?search=&t1=0&t2=0&t3=0&t4=0" title="">School/Institution</a></li>  
<li><a href="/secure/Source/Browse_Source.aspx?search=&t1=0" title="">Source</a></li>                            
</ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li> 
     <li><a href="#" title=""><span>Client Management</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>
     <li><a href="/secure/Admin/Client/Browse_Client.aspx" title="">Clients</a></li> 
    <li><a href="/secure/Admin/Login/Browse_Login.aspx" title="">Client Login</a></li> 
    <li><a href="/secure/Template/Browse_Template.aspx" title="">Template</a></li> 
    <li><a href="/secure/Logo/Browse_Logo.aspx" title="">Logo</a></li> 
<%--	<li><a href="/secure/Admin/Browse_Client.aspx" title="" >Admin &#187; Client</a></li>  --%>                           
    </ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li>
</ul>  
 <ul id="clientblk" runat="server" visible="false" class="ws_css_cb_menu">          
  <li>&nbsp;&nbsp;&nbsp;</li>
    <li><a href="/secure/Home.aspx" title="Home">Home</a></li>
  <li><a href="#" title=""><span>Applications</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>
      <li><a href="/secure/Active_Application.aspx" title="View all Active Applications">Active Applications</a></li>
      <li><a href="/secure/Archives_Application.aspx" title="View all Archive Applications">Archive Applications</a></li>
      <li><a href="/secure/Quickapp/Quickapp.aspx" title="">Quick App</a></li>     
    </ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li>
  <li><a href="#" title=""><span>Maintenance</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>   
<li><a href="/secure/Country/Browse_Country.aspx?search=" title="">Countries</a></li>
<li><a href="/secure/Documents/Browse_Documents.aspx?search=&t1=0" title="">Documents</a></li>
<li><a href="/secure/EducationProgram/Browse_DegreePlan.aspx?search=&t1=0&t2=0" title="">Education Program</a></li>
<li><a href="/secure/Gradescale/Browse_Gradescale.aspx?search=&t1=0" title="">Gradescale</a></li>  
<li><a href="/secure/Major/Browse_Major.aspx?search=&t1=0&t2=0" title="">Major/Emphasis</a></li>  
<li><a href="/secure/Institution/Browse_Institution.aspx?search=&t1=0&t2=0&t3=0&t4=0" title="">School/Institution</a></li>  
<li><a href="/secure/Source/Browse_Source.aspx?search=&t1=0" title="">Source</a></li>
<li><a href="/secure/UsEquivalency/Browse_Us_Equivalency.aspx?search=" title="">Us Equivalency</a></li>
    </ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li> 
    <li><a href="#" title=""><span>Settings</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>    
<li><a href="/secure/CustomerAddons/Browse_CustomerAddons.aspx" title="">Application</a></li>
<li><a href="/secure/DeliveryType/Browse_DeliveryType.aspx" title="">Delivery</a></li>
<li><a href="/secure/ServiceType/Browse_ServiceType.aspx" title="">Service</a></li>                            
<li><a href="/secure/SplashPage/Browse_Splashpage.aspx" title="">Splash Screen</a></li>   
    </ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li> 
    <li><a href="#" title=""><span>Client Accounts</span>
    <![if gt IE 6]>
    </a>
    <![endif]>
    <!--[if lte IE 6]><table><tr><td><![endif]-->
    <ul class='ws_css_cb_menum'>    
<li><a href="/secure/ClientAdmin/Client/Browse_Client.aspx" title="">Browse Clients</a></li>
<li><a href="/secure/Employee/Browse_Employee.aspx" title="">Employee</a></li> 
    </ul>
    <!--[if lte IE 6]></td></tr></table></a><![endif]-->
  </li>          
</ul>  