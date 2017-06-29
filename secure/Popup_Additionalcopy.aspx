<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Additionalcopy.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Additionalcopy" %>

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
<table width="100%"> 
<tr>
        <td>
        <br />
         <div class="headertag">Additional Copy Mailing Address</div>     
         <br />
         </td> </tr> 

<tr><td>
     <%--<div class="headertag">Select Delivery Address Information ( Additional Copy Requested )</div>--%>
           <br />
           <table class="search_css">
               <tr>
                   <td>
                       <asp:RadioButtonList ID="frm5_addlradiobtn" runat="server" AutoPostBack="True" 
                           OnSelectedIndexChanged="frm5_addlradiobtn_SelectedIndexChanged">
                           <asp:ListItem Value="False">Please send my Official Hard Copy to my primary mailing address</asp:ListItem>
                           <asp:ListItem Value="True">Please send this Official Hard Copy to a separate address.</asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
               </tr>
           </table>
       </td></tr>
<tr id="frm5_Additionalrequestform" runat="server" visible="false">
       <td>
       <br />
        <div class="headertag">Fill in Delivery Address Information ( Additional Copy Requested )</div>
        <br />
        <table class="search_css" > 
                    <tr> 
                    <td>
                        <table width="100%">
                            <tr ID="Tr1" runat="server" visible="false">
                                <td align="center" colspan="3">
                                    Note:Selected No of copies will be sent to the below Address</td>
                            </tr>
                            <tr ID="Tr2" runat="server" visible="true">
                                <td>
                                   <b>Please indicate the number of additional copies to be delivered to this address:</b><asp:DropDownList ID="frm5_copies_addl" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                        ControlToValidate="frm5_copies_addl" ErrorMessage="You must select No of Copies" 
                                        InitialValue="0" ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Name: <span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_Fnameaddl" runat="server" MaxLength="50" Width="313px"></asp:TextBox>                               
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                        ControlToValidate="frm5_Fnameaddl" ErrorMessage="You must enter Name" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 1: <span style="color:Red">*</span><br />
                                    <asp:TextBox ID="frm5_add1addl" runat="server" MaxLength="100" Width="208px"></asp:TextBox>                                   
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                        ControlToValidate="frm5_add1addl" ErrorMessage="You must enter your Address" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td rowspan="2">
                                    State/Province:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_stateaddl" runat="server" Height="64px" MaxLength="50" 
                                        TextMode="MultiLine" Width="129px"></asp:TextBox>                                 
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                        ControlToValidate="frm5_stateaddl" ErrorMessage="You must enter state" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    Country: <span style="color:Red">*</span><br />
                                    <asp:DropDownList ID="frm5_countryaddl" runat="server" AppendDataBoundItems="True" 
                                        Width="204px">
                                    </asp:DropDownList>                                  
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                        ControlToValidate="frm5_countryaddl" ErrorMessage="You must select Country" 
                                        InitialValue="0" ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 2:<br />
                                    <asp:TextBox ID="frm5_add2addl" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                </td>
                                <td>
                                    Delivery type:<span style="color:Red;">*</span><br />
                                    <asp:DropDownList ID="frm5_deliverytypeaddl" runat="server" 
                                        AppendDataBoundItems="true" Width="135px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                        ControlToValidate="frm5_deliverytypeaddl" 
                                        ErrorMessage="You must select Delivery Type" InitialValue="0" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_cityaddl" runat="server" MaxLength="50" Width="129px"></asp:TextBox>                                  
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                        ControlToValidate="frm5_cityaddl" ErrorMessage="You must enter city" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    postal/Zip Code:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_zipaddl" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                        ControlToValidate="frm5_zipaddl" ErrorMessage="You must enter zipcode" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_addlinstname" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                 <asp:Button ID="frm5_btn_canceladdl" runat="server" CssClass="btncolor" 
                                        OnClick="frm5_btn_canceladdl_Click" Text="Cancel" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="AdditionalAdd" runat="server" CausesValidation="False" 
                                        CssClass="btncolor" OnClick="btn_Click" Text="Submit" 
                                        ValidationGroup="frm5_addlgroup" />
                                        <asp:Button ID="AdditionalUpdate" runat="server" CausesValidation="False" 
                                        CssClass="btncolor" OnClick="btn_Click" Text="Update" 
                                        ValidationGroup="frm5_addlgroup" />
                                </td>
                            </tr>  
                                 <tr>
                <td  colspan="3">
                            <sv:ValidationSummary ID="frm5_Summary1" runat="server" CssClass="error_box_summary" ValidationGroup="frm5_addlgroup" />
                            </td>
                            </tr>                   
                        </table>
                        </td>                    
                </tr>
                      
                </table> 
                </td>
       </tr>
      
	 </table>                                                                                           
</asp:Content>