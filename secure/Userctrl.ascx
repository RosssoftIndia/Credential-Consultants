<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Userctrl.ascx.cs" Inherits="secure_Userctrl" %>

	 <table style="width:100%;height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient">ADMINISTRATION</td></tr>
 </table></td></tr>
 <tr><td class="proj_logbox">
         <asp:LoginView ID="LoginView" runat="Server">
            <AnonymousTemplate>                   
                   <asp:LoginStatus ID="LoginStatus" runat="server" LoginText="Sign In!" LogoutPageUrl="~/Logout.aspx" LogoutAction="Redirect" />                                           
            </AnonymousTemplate>
            <LoggedInTemplate>
                Welcome,
                <asp:LoginName ID="LoginName" runat="Server" /><span style="color:White;font-size:12px;font-weight:bold;">|</span>                                                            
                <asp:LoginStatus ID="LoginStatus" runat="server" LoginText="Sign In!" LogoutPageUrl="~/Logout.aspx" LogoutAction="Redirect"  />
            </LoggedInTemplate>                                        
        </asp:LoginView>    
       </td></tr>
 </table>