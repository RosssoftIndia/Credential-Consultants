<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Client_Login.aspx.cs" Inherits="secure_Admin_Client_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>    
</head>
<body style="width: 100%; height: 100%">
    <form id="form1" runat="server">
    <div>
    <table style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; width: 100%; border-top-style: none; padding-top: 0px; border-right-style: none; border-left-style: none; height: 100%; border-bottom-style: none">
    <tr>
    <td style="text-align: left;" colspan="2" >
                    <img alt="logo" src="../../images/logo.png" /></td>  
    </tr>
    <tr>  
    <td style="width: 260px">
    </td>
    <td style="background-attachment: scroll; background-image: url(../../images/login-bg.png); width: 692px; background-repeat: no-repeat; height: 313px; background-position: right center; vertical-align:middle; text-align:right;">           
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px" style="background-attachment: scroll; background-image: url(images/login-bg.png); background-repeat: no-repeat; width: 692px; height: 313px; vertical-align: middle; text-align: center;">          
    <br />
    <br />   
    <br />
        <table style="width: 525px; height: 87px">
            <tr>
                <td style="width: 119px;" rowspan="2">
                </td>
                <td style="height: 33px">
                </td>
            </tr>
            <tr>
                <td>
   <%-- <asp:Login ID="Loginctrl" runat="server" BackColor="#A2A8A6" BorderColor="#A2A8A6" BorderPadding="4"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="12px"
                        ForeColor="#333333" Width="400px" OnAuthenticate="Loginctrl_Authenticate">
                        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <TextBoxStyle Font-Size="0.8em" />
                        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                        <LayoutTemplate>--%>
                            <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse;" >
                                <tr>
                                    <td style="text-align: right">
                                        <table border="0" cellpadding="0" style="width: 400px">
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
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" Style="font-size: 12px" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: red; height: 12px">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2" style="height: 18px; text-align: center">
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                                                        BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                                        Font-Size="0.8em" ForeColor="#284775" Style="font-size: 12px" Text="Log In" ValidationGroup="Login1" OnClick="LoginButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                   <%--    </LayoutTemplate>
                    </asp:Login>--%>
                </td>
            </tr>
        </table>
    <br />
    </asp:Panel>
     </td>
    </tr>
    <tr>
        <td colspan="3" style="text-align: right">
                           Copyright © 2008 Credential Consultants Inc. All Rights Reserved.<a href="" style="text-decoration: none">Privacy Policy</a>|<a href="" style="text-decoration: none">Terms of Service</a>
        </td>
    </tr>
    </table>
        
    </div>
    </form>
</body>
</html>
