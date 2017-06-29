<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Fax.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Fax" %>

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
  <div class="headertag" ID="title_frm5_fax" runat="server">Fax Information</div> 
               <br />
                <table class="search_css">                                     
                         <tr>
                         <td> 
                             <span ID="title_frm5_fax1" runat="server">Fax Number:</span><span 
                                 style="color: #ff0000">*</span>
                                 </td>
                                 <td>
                             <asp:TextBox ID="frm5_faxno" runat="server" MaxLength="50" 
                                 ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                             <sv:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                 ControlToValidate="frm5_faxno" ErrorMessage="You must enter Fax Number" 
                                 ValidationGroup="frm5_group2">*</sv:RequiredFieldValidator>
                             &nbsp;<br /> 
                         </td>
                         
                         </tr>
                         <tr>
                             <td>
                                 ATTN:<span style="color: #ff0000">*</span>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="frm5_attn" 
                                     runat="server" MaxLength="50" ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                                 <sv:RequiredFieldValidator ID="frm5_reqvalidator2" runat="server" 
                                     ControlToValidate="frm5_attn" ErrorMessage="You must enter ATTN" 
                                     ValidationGroup="frm5_group2">*</sv:RequiredFieldValidator>
                             </td>
                         </tr>                       
                         <tr>
                             <td colspan="2" style="text-align: center">
                                 &nbsp;&nbsp;
                                 <asp:Button ID="FaxUpdate" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="btn_Click" Text="Submit" 
                                     ValidationGroup="frm5_group2" />
                                     <asp:Button ID="FaxAdd" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="btn_Click" Text="Submit" 
                                     ValidationGroup="frm5_group2" />
                             </td>
                         </tr>   
                         <tr align="left" >
                         <td></td>
                         <td>
     <sv:ValidationSummary ID="frm5_Summary2" runat="server" EnableTheming="True" 
                                     ValidationGroup="frm5_group2" 
         CssClass="error_box_summary" />
 </td></tr>                    
             </table> </asp:Content>