<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timeout.aspx.cs" Inherits="Timeout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Application Timeout!</title>    
    <style type="text/css">
        .style2
        {
            height: 34px;
        }
    </style>   
                <script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-20673314-1']);
  _gaq.push(['_setDomainName', '.credentialconnection.com']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>  
    <center>
    <table style="top: 200px; position: relative;">
    <tr><td></td></tr>    
    <tr><td>
            <asp:Image ID="Image1" runat="server" Height="170px" ImageUrl="~/images/AlertSign.jpg"
                 Width="211px" />    
        </td>
        <td><table width="100%" style="height: 132px" ><tr><td class="style2" 
                style="text-align: center"> <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black" Height="23px"
                Text="Note:"
                Width="36px"></asp:Label></td></tr>
        <tr><td style="text-align: center"> <asp:Label ID="notetxt" runat="server" 
                Font-Bold="True" ForeColor="Red" Height="51px"
                Text="Your session has timed out. Please close ALL browsers on your computer before trying another application."
                Width="461px"></asp:Label></td></tr></table></td>
        </tr>
    <tr><td></td></tr>    
    </table>      
       </center>           
    </div>
    </form>
</body>
</html>
