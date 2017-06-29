<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Email.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Email" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  
<script type="text/javascript" language='JavaScript'>

    function fnTrapKD(btn, event) {
        if (document.all) {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                // btn.click();
            }
        }
        else if (document.getElementById) {
            if (event.which == 13) {
                event.returnValue = false;
                event.cancel = true;
                // btn.click();
            }
        }
        else if (document.layers) {
            if (event.which == 13) {
                event.returnValue = false;
                event.cancel = true;
                // btn.click();
            }
        }
    }
            </script>
    
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
  <div class="headertag" ID="title_frm5_fax" runat="server">Email Information</div> 
               <br />
                <table class="search_css">                                     
                         <tr>
                         <td> 
                             <span ID="title_frm5_fax1" runat="server">Recipient's Name:</span><span 
                                 style="color: #ff0000">*</span>
                                 </td>
                                 <td>
                            <asp:TextBox ID="frm5_ename" runat="server" MaxLength="50" 
                                 ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                             <sv:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                                 ControlToValidate="frm5_ename" ErrorMessage="You must enter Recipient's Name" 
                                 ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                             &nbsp;<br /> 
                         </td>
                         
                         </tr>
                         <tr>
                             <td>
                                 Email Address:<span style="color: #ff0000">*</span></td><td><asp:TextBox ID="frm5_email" 
                                     runat="server" MaxLength="50" ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                                 <sv:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                     ControlToValidate="frm5_email" ErrorMessage="You must enter Email Address" 
                                     ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                             </td>
                         </tr>                       
                         <tr>
                             <td colspan="2" style="text-align: center">
                                 &nbsp;&nbsp;
                                 <asp:Button ID="EmailUpdate" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="btn_Click" Text="Submit" 
                                     ValidationGroup="frm5_group3" />
                                     <asp:Button ID="EmailAdd" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="btn_Click" Text="Submit" 
                                     ValidationGroup="frm5_group3" />
                             </td>
                         </tr>   
                         <tr align="left" >
                         <td></td>
                         <td>
     <sv:ValidationSummary ID="frm5_Summary2" runat="server" EnableTheming="True" 
                                     ValidationGroup="frm5_group3" 
         CssClass="error_box_summary" />
 </td></tr>                    
             </table>
             
              </asp:Content>