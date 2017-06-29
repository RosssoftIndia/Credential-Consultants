<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" MasterPageFile="~/LoginMaster.master"  Inherits="Logout" %>

<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
 <table style="width:100%;min-height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient">ADMINISTRATION</td></tr>
 </table></td></tr>
 </table>
</asp:Content>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  	
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
 <table class="search_css">
                                            <tr>
                                                <td align="center" colspan="2" style="font-weight: bold; font-size: 16px; color: black;
                                                    letter-spacing: 5px; background-color: transparent; text-align: left; height: 52px;">
                                                    Log Out
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="msgLabel" runat="server" ForeColor="White"
                                                        Style="font-weight: bold; font-size: 12px; color: black">You have successfully Logged Out......</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2" style="height: 18px;">
                                                    <asp:HyperLink ID="Login" runat="server" NavigateUrl="~/Login.aspx"  CssClass="btncolor" >Click To Login</asp:HyperLink>
                                                    </td>
                                            </tr>
                                        </table>

<br />
<br />                                                                                                    
</asp:Content>

   
                                           
