<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Publicsite.aspx.cs" Inherits="Publicsite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styler.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
function PopupCenter(pageURL, title,w,h) {
var left = (screen.width/2)-(w/2);
var top = (screen.height / 2) - (h / 2);
var w1 = (screen.width-30);
var h1 = (screen.height-100);
var left1 = 10;
var top1 = 10;
var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars= yes, resizable=no, copyhistory=no,width=' + w1 + ', height=' + h1 + ', top=' + top1 + ', left=' + left1);
} 
</script>
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
<script type="text/javascript">
<!--
    var message = "Function Disabled!";

    ///////////////////////////////////
    function clickIE4() {
        if (event.button == 2) {
            alert(message);
            return false;
        }
    }

    function clickNS4(e) {
        if (document.layers || document.getElementById && !document.all) {
            if (e.which == 2 || e.which == 3) {
                alert(message);
                return false;
            }
        }
    }

    if (document.layers) {
        document.captureEvents(Event.MOUSEDOWN);
        document.onmousedown = clickNS4;
    }
    else if (document.all && !document.getElementById) {
        document.onmousedown = clickIE4;
    }

    document.oncontextmenu = new Function("alert(message);return false")

// --> 
</script>
    <form id="form1" runat="server">
    <div>
      <br />            
    <hr />
        <div class="proj_block">
    <br />
    <div><div id="OrgTitle" runat="server" class="proj_header"></div><div class="proj_category">Application</div></div>
     <br />   
     </div>  
    <hr />
    <br />
    <table>
    <tr>
                <td style="height: 24px">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0" 
                            style=" height:32px; width: 199px;">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtbox" runat="server"></asp:TextBox>
                                    <asp:Button ID="btncheck"
                                        runat="server" Text="check" onclick="btncheck_Click" /></td>
                            </tr>
                            <tr>
                            <td><br /></td>                             
                            </tr>
                            <tr>   
                            <td style="width: 57px"><asp:ImageButton ID="btn1" runat="server" ImageAlign="Baseline" ImageUrl="~/images/r-arrow1.png" /></td>                         
                             <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">
                                 Online Application</td>
                              </tr>
                            <tr>
                            <td><asp:ImageButton ID="btn2" runat="server" ImageAlign="Baseline" 
                                    ImageUrl="~/images/r-arrow1.png" /></td>                               
                            <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">
                                Check Status</td>
                            </tr>
                            <tr>
                            <td><asp:ImageButton ID="btn3" runat="server" ImageAlign="Baseline" 
                                    ImageUrl="~/images/r-arrow1.png" /></td>                               
                            <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">
                                CreditCard Payment</td>
                            </tr>
                            <tr>
                            <td><asp:ImageButton ID="btn4" runat="server" ImageAlign="Baseline" 
                                    ImageUrl="~/images/r-arrow1.png" /></td>                               
                            <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">
                                Print Application</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>            
            </table>
    </div>
    </form>
</body>
</html>
