<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_Status.aspx.cs" MasterPageFile="~/LoginMaster.master"  Inherits="Login_Status" %>


<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  

   <script type="text/javascript" language="javascript">

       function ShowValue() {
           $.fn.colorbox({ width: "80%", height: "80%", iframe: true, href: "Applicant_status.aspx" });
       }
    </script>
<link rel="stylesheet" href="Code/colorbox/colorbox.css" />
<script src="Code/colorbox/jquery.min.js"></script>
<script src="Code/colorbox/jquery.colorbox.js"></script>
<script>
    $(document).ready(function() { $(".iframe").colorbox({ iframe: true, width: "80%", height: "80%" }); });
 </script>

</asp:Content>

<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server"> 
 <table style="width:100%;min-height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient"></td></tr>
 </table></td></tr>
 </table> 
</asp:Content>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  	
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
    <table style="width:100%" class="search_css" >
                                            <tr>
                                                <td align="center"                                                   
                                                    style="font-weight: bold; font-size: 16px; color: black;
                                                    letter-spacing: 5px; background-color: transparent; text-align: left; height: 52px;" 
                                                    colspan="2">
                                                    Inquiry
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="fileno" runat="server"  ForeColor="White" Style="font-weight: bold; font-size: 12px; color: black">File Number:</asp:Label>
                                                </td>
                                                <td style="text-align: left">                                                    
                                                    <asp:TextBox ID="txtfile" runat="server"></asp:TextBox>
                                                     <sv:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfile" ForeColor="Red">*</sv:RequiredFieldValidator>
                                                    <asp:Button ID="statusButton" runat="server" BackColor="#FFFBFF" 
                                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" 
                                                        Style="font-size: 12px" Text="Show Status" onclick="statusButton_Click" />                                                          
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 18px; text-align: left" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 18px; text-align: left" colspan="2">
                                                    <asp:Label ID="txterror" runat="server"  Font-Bold="True" Font-Italic="True" Text="* The application file number entered is invalid. Please check your file number and try again."  Visible="False" ForeColor="Black"></asp:Label>
                                                </td>                                             
                                            </tr>
                                        </table>        
<br />
<br />                                                                                                    
</asp:Content>


