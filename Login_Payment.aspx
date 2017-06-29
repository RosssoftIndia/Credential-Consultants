<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_Payment.aspx.cs" MasterPageFile="~/LoginMaster.master"   Inherits="Login_Payment" %>


<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
 <div class="proj_block">    
     <div><div id="OrgTitle" runat="server" class="proj_header"></div><br /><br /><div class="proj_category">Online Payment</div><br /><div class="proj_logbox">
 
        </div></div> 
     </div>  	
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
                                                   Online Payment
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


       
                                        
                                                      
    