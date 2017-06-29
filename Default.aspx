<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/Master.master"  Inherits="Default" %>
 <%-- <%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>--%>
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

<table style="width:100%;height:400px;" >
<tr>
<td>
<div id="list" runat="server"></div>
</td>
</tr>
<tr id="recaptchablk" runat="server" align="center"  >
<td>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="holder" DefaultButton="frm1_Btn_continue"   runat="server">
<%-- <recaptcha:RecaptchaControl
              ID="recaptcha"
              runat="server"
              Theme="red"
              PublicKey="6LfUqr8SAAAAAHO8duehS-4BdSq6v67kbk0DpAby"
              PrivateKey="6LfUqr8SAAAAACLRk6JBEkpxoJf2ZWburzhsq9gz"
              />--%>
              <div class="g-recaptcha" data-sitekey="6Lc2tg4TAAAAAOzwkN10Kf_vOsdHIiCCcoUqsNYq"></div>
      </asp:Panel>

      <asp:Button ID="Btn_continue" CssClass="btncolor btnCapatch" Visible="false"  
        runat="server" Text="Get Started- Type the Security Characters and Click Here" onclick="Btn_continue_Click" />
      <table >
<tr><td></td></tr>
<tr>
<td style="text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
   Get Started- Check above reCAPTCHA  :<br />
    <span style="color:#AEAEAE; font-style: italic;">&#38; Click Here</span></td>
<td><asp:ImageButton ID="frm1_Btn_continue" OnClick="frm1_Btn_continue_Click"  
        runat="server" ImageUrl="~/images/r-arrow1.png" 
        ImageAlign="Baseline" /></td>
</tr>
<tr>
<td colspan="2" align="center"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="frm1_Btn_continue" /> 
    </Triggers>
<ContentTemplate>
  <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
  </ContentTemplate>
    </asp:UpdatePanel>  
  </td></tr>		
</table>

</td>
</tr>
</table>
<br />
<br />                                                                          
</asp:Content>


                      
                 
 