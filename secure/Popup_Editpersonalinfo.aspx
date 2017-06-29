<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Editpersonalinfo.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Editpersonalinfo" %>

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
<table width="100%"> 
<tr>
<td>       
<br />
<div class="headertag">Personal Information</div><br />
<table class="search_css">
<tr><td><table>
<tr>
<td style="width: 174px">
          First / Given Name: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Fname" runat="server" Width="129px" 
              ValidationGroup="frm1_group" TabIndex="1" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator2" runat="server" ControlToValidate="frm1_Fname"
        ErrorMessage="You must enter Firstname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 246px">
Gender: <span style="color:Red;">*</span><br />       
    <asp:RadioButtonList ID="frm1_option_gender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="4" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px">
        <asp:ListItem Value="True">Male</asp:ListItem><asp:ListItem Value="False" Selected="True">Female</asp:ListItem></asp:RadioButtonList></td><td> 
Is the name on your documents<br />different? <span style="color:Red;">*</span><br />
    <asp:RadioButtonList ID="frm1_optin_name" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="frm1_optin_name_SelectedIndexChanged" RepeatLayout="Flow" TabIndex="9">
        <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False" Selected="True">No</asp:ListItem></asp:RadioButtonList></td></tr><tr>
<td style="width: 174px">
 Middle Name:<br /> 
    <asp:TextBox ID="frm1_Mname" runat="server" Width="129px" TabIndex="2" 
        MaxLength="50" ></asp:TextBox></td><td style="width: 246px">Date of Birth: (mm/dd/yyyy) <span style="color:Red;">*</span><br />
     <asp:DropDownList ID="frm1_option_month" runat="server" Width="54px" TabIndex="5">                  
    </asp:DropDownList>
    <sv:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="frm1_option_month"
        ErrorMessage="You must select a Month" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>/   
     <asp:DropDownList ID="frm1_option_date" runat="server" Width="54px" TabIndex="6">               
    </asp:DropDownList> 
    <sv:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="frm1_option_date"
        ErrorMessage="You must select a Date" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>/&nbsp;<asp:DropDownList ID="frm1_option_year" runat="server" Width="54px" TabIndex="7">               
    </asp:DropDownList>          &nbsp;
    <sv:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="frm1_option_year"
        ErrorMessage="You must select a Year" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td id="frm1_optional" runat="server" visible="false" rowspan="2" >    
          First / Given Name: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optFname" runat="server" Width="129px" TabIndex="10" 
              MaxLength="50" ></asp:TextBox><br />
    Middle Name:<br /><asp:TextBox ID="frm1_optMname" runat="server" Width="129px" 
              TabIndex="11" MaxLength="50" ></asp:TextBox><br />
          Last / Family / Surname: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optLname" runat="server" Width="129px" TabIndex="12" 
              MaxLength="50" ></asp:TextBox></td></tr><tr>
<td style="width: 174px">
    <br />
    <br />
        Last / Family / Surname: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Lname" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TabIndex="3" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator1" runat="server" ControlToValidate="frm1_Lname" ErrorMessage="You must enter Lastname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 246px">
    <br />
    <br />
 Country of Birth:<br /><asp:DropDownList id="frm1_Country_birth" AppendDataBoundItems="true" runat="server" Width="189px" TabIndex="8">
    </asp:DropDownList>
    <sv:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="frm1_Country_birth"
        ErrorMessage="You must select Country of Birth" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr></table></td></tr></table></td></tr><tr>
<td >
<br />
<div class="headertag">Address Information</div><br />
<table class="search_css">
<tr><td><table> 
<tr>
<td style="width: 238px">
Address Line 1: <span style="color:Red">*</span><br /> 
<asp:TextBox ID="frm1_address1" runat="server" Width="208px" 
        ValidationGroup="frm1_group" MaxLength="100" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator3" runat="server" ControlToValidate="frm1_address1"
        ErrorMessage="You must enter your Address" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td rowspan="2">
State/Province:<span style="color:Red;">*</span><br /> 
   <asp:TextBox ID="frm1_state" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TextMode="MultiLine" Height="64px" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator5" runat="server" ControlToValidate="frm1_state"
        ErrorMessage="You must enter state" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td>
Country: <span style="color:Red">*</span><br /> 
    <asp:DropDownList ID="frm1_option_country" AppendDataBoundItems="true" runat="server" Width="204px">
    </asp:DropDownList>          
    <sv:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="frm1_option_country"
        ErrorMessage="You must select Country" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr><tr>
<td style="width: 238px">
Address Line 2:<br /> 
   <asp:TextBox ID="frm1_address2" runat="server" Width="208px" MaxLength="100" ></asp:TextBox></td></tr><tr>
<td style="width: 238px">
City:<span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_city" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator4" runat="server" ControlToValidate="frm1_city"
        ErrorMessage="You must enter city" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td>
    Postal/Zip Code:<span style="color:Red;">*</span><br /> 
 <asp:TextBox ID="frm1_zip" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator6" runat="server" ControlToValidate="frm1_zip"
        ErrorMessage="You must enter zipcode" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr></table></td></tr></table></td></tr><tr>
<td>
<br />
<div class="headertag">Contact Information</div><br />
<table class="search_css"><tr><td>
<table > 
<tr>
<td style="width: 158px">
    Primary Phone: <span style="color:Red;">*</span><br />
    <asp:TextBox ID="frm1_home_phone" runat="server" Width="200px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator7" runat="server" ControlToValidate="frm1_home_phone"
        ErrorMessage="You must enter Primary Phone" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 49px"></td>
<td>
Primary Email Address:<br /><asp:TextBox ID="frm1_primarymail" runat="server" 
        Width="274px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RegularExpressionValidator ID="frm1_ExpressionFieldValidator4" runat="server" ControlToValidate="frm1_primarymail"
        ErrorMessage="You must enter a valid E-mail ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="frm1_group">*</sv:RegularExpressionValidator ></td></tr><tr>
<td style="width: 158px">
    Secondary Phone:<br />
    <asp:TextBox ID="frm1_work_phone" runat="server" 
        Width="200px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox></td><td style="width: 49px"></td> 
<td>
Confirm Primary Email Address:<br /><asp:TextBox ID="frm1_confrprimary" 
        runat="server" Width="274px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:CompareValidator ID="frm1_comparevalidator1" runat="server" ControlToValidate="frm1_confrprimary"
        ErrorMessage="E-mail Address entered seems to miss match" ControlToCompare="frm1_primarymail" ValidationGroup="frm1_group">*</sv:CompareValidator></td></tr><tr>
<td style="width: 158px">
    Mobile Phone:<br /><asp:TextBox ID="frm1_cell_phone" runat="server" 
        Width="129px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox></td><td style="width: 49px"></td>
</tr>
<tr> 
<td></td>
<td style="width: 49px"></td>
</tr> 
<tr> 
<td colspan="3">            
Have you ever used our services before? <span style="color:Red;">*</span> 
<br /> 
    <asp:RadioButtonList ID="frm1_option_service" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="frm1_option_service_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Selected="True" Value="False">No</asp:ListItem></asp:RadioButtonList>&nbsp;
  </td></tr>
  <tr id="frm1_optional1" visible="false" runat="server"> 
<td colspan="3" style="height: 94px">
Previous file number or exact name and year of previous evaluation:<br />
<asp:TextBox ID="frm1_previousid" runat="server" Width="545px" Height="73px" 
        TextMode="MultiLine" MaxLength="50" ></asp:TextBox></td></tr></table></td></tr></table></td></tr>
      <tr align="right"><td><asp:Button 
                    ID="personalinfoupdate" runat="server"  CssClass="btncolor" Text="update" onclick="updatebtn_Click" /></td></tr>
        <tr>
<td style="height: 24px"> 
<div>
<sv:ValidationSummary id="frm1_summary" runat="server" CssClass="error_box_summary" ValidationGroup="frm1_group" />
</div> 
</td>
</tr>  
</table>                                                                                               
</asp:Content>