<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/LoginMaster.master"  %>

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
  <asp:Login ID="Loginctrl" runat="server" Width="100%"  OnAuthenticate="Loginctrl_Authenticate">
                        <LayoutTemplate>
                            <table class="search_css" >
                                <tr>                                  
                                                <td align="center" colspan="2" style="font-weight: bold; font-size: 16px; color: black;
                                                    letter-spacing: 5px; background-color: transparent; text-align: left; height: 52px;">
                                                    Log In<hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 237px">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" ForeColor="White"
                                                        Style="font-weight: bold; font-size: 12px; color: black">User Name:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" Style="font-size: 12px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 237px; height: 22px;">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" ForeColor="White"
                                                        Style="font-weight: bold; font-size: 12px; color: black;">Password:</asp:Label></td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" Style="font-size: 12px"
                                                        TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                               <tr>
                                            <td></td>
                                                <td >
                                                    <asp:Button ID="LoginButton" runat="server" 
                                                       
                                                      CommandName="Login" CssClass="btncolor" 
                                                        
                                                        Text="Log In" ValidationGroup="Login1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: red; height: 12px">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                         
                                        </table>                                  
                        </LayoutTemplate>
                    </asp:Login>
<br />
<br />                                                                                                    
</asp:Content>



                  
                 
       
     