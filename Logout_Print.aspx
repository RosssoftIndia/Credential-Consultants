<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout_Print.aspx.cs" MasterPageFile="~/LoginMaster.master"  Inherits="Logout_Print" %>

<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
<table style="width:100%;height:110px;" >
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
 <table class="search_css">
                                            <tr>
                                                <td align="center" colspan="2" style="font-weight: bold; font-size: 16px; color: black;
                                                    letter-spacing: 5px; background-color: transparent; text-align: left; height: 52px;">
                                                    Print Error
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="msgLabel" runat="server" ForeColor="White"
                                                        Style="font-weight: bold; font-size: 12px; color: black">The application file number obtained is invalid. Please check your file number and try again.</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2" style="height: 18px;">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>

<br />
<br />                                                                                                    
</asp:Content>

   
                                           
