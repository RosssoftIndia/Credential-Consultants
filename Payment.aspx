<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" MasterPageFile="~/AppMaster.master"   Inherits="Payment" %>

<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  

    <style type="text/css">
        .style1
        {
            width: 320px;
        }
        .style2
        {
            width: 162px;
        }
        .style3
        {
            width: 142px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
  <table style="width:100%;min-height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient">Online Payment</td></tr>
 </table></td></tr>
 </table>  
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
    <br />
<br /> 
<table width="100%">
<tr>
<td>
<br />
<div class="headertag">Personal Information</div>
<br />
<table style="background-color:#ECEFF1;"  class="search_css" >           
                <tr>
                    <td>
                        First Name<br />
                        <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="FirstNameTextBox"
                            runat="server" ErrorMessage="First Name is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FirstNameTextBox"
                            ErrorMessage="Please limit the length of the content entered to 40 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,40}$">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        Last Name<br />
                        <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="LastNameTextBox"
                            runat="server" ErrorMessage="Last Name is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="LastNameTextBox"
                            ErrorMessage="Please limit the length of the content entered to 40 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,40}$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Address<br />
                        <asp:TextBox ID="AddressTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="AddressTextBox"
                            runat="server" ErrorMessage="Address is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="AddressTextBox"
                            ErrorMessage="Please limit the length of the content entered to 60 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,60}$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        City<br />
                        <asp:TextBox ID="CityTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="CityTextBox"
                            runat="server" ErrorMessage="City is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="CityTextBox"
                            ErrorMessage="Please limit the length of the content entered to 40 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,40}$">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        State<br />
                        <asp:TextBox ID="StateTextBox" runat="server" MaxLength="30" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="StateTextBox"
                            runat="server" ErrorMessage="State is Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="StateTextBox"
                            ErrorMessage="Please limit the length of the content entered to 40 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,40}$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Postal/Zip Code<br />
                        <asp:TextBox ID="ZipTextBox" runat="server" CssClass="mediumTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ZipTextBox"
                            runat="server" ErrorMessage="Zip Code Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="ZipTextBox"
                            ErrorMessage="Please limit the length of the content entered to 15 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,15}$">*</asp:RegularExpressionValidator>
                    </td>                  
                    <td>
                        Country<br />
                        <asp:TextBox ID="CountryTextBox" runat="server" CssClass="mediumTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="CountryTextBox"
                            runat="server" ErrorMessage="Country Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                            ControlToValidate="CountryTextBox" ErrorMessage="Please limit the length of the content entered to 40 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,40}$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Phone<br />
                        <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="PhoneTextBox"
                            runat="server" ErrorMessage="Phone is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Email<br />
                        <asp:TextBox ID="EmailTextBox" runat="server" CssClass="xx-largeTextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="EmailTextBox"
                            runat="server" ErrorMessage="Email is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="EmailTextBox"
                            ErrorMessage="Please limit the length of the content entered to 255 characters"
                            ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,255}$">*</asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator49" runat="server"
                            ControlToValidate="EmailTextBox" ErrorMessage="Please Enter a valid Email Address"
                            ValidationGroup="Authorize" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                </table>
</td>
</tr>
<tr>
<td>
<br />
<div class="headertag">CreditCard Information</div>
<br />
 <table style="background-color:#ECEFF1;" class="search_css" >   
                  <tr>
                    <td colspan="3">
                    Credit Card Type<br />                    
                        <asp:DropDownList ID="Dropcardtype" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="0">select</asp:ListItem>                         
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="Dropcardtype"
                            runat="server" ErrorMessage="Credit Card Type is a Required Field" 
                            Text="*" ValidationGroup="Authorize" InitialValue="0"></asp:RequiredFieldValidator>
                    </td> 
                    </tr> 
                <tr>
                    <td colspan="3">
                        Credit Card Number<br />
                        <asp:TextBox ID="CreditCardTextBox" runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="CreditCardTextBox"
                            runat="server" ErrorMessage="Credit Card is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="CreditCardTextBox"
                            ErrorMessage="Please Enter a valid Credit Card Number" ValidationGroup="Authorize"
                            ValidationExpression="^[\s\S]{0,22}$">*</asp:RegularExpressionValidator>
                        <br />
                        <sup style="color: Red;">1111222233334444</sup>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" class="style3">
                        Expiration Month<br />
                        <asp:DropDownList ID="MonthDropDownList" runat="server">
                           <asp:ListItem Value="Select"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01 - January"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02 - February"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03 - March"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04 - April"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05 - May"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06 - June"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07 - July"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08 - August"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09 - September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10 - October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11 - November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12 - December"></asp:ListItem>
                        </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="MonthDropDownList" InitialValue="Select"
                            runat="server" ErrorMessage=" Expiration Month Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                  
                    </td>
                    <td colspan="1" class="style2">
                        Expiration Year<br />
                        <asp:DropDownList ID="YearDropDownList" AppendDataBoundItems="true" runat="server">   
                        <asp:ListItem Value="Select"></asp:ListItem>                                                
                        </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="YearDropDownList" InitialValue="Select"
                            runat="server" ErrorMessage="Expiration Year Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                  
                    </td>
                    <td rowspan="3">
                    <table>
                    <tr><td class="style1">
                     Credit Card Verification<br />
                        (See image below)<br />
                        <asp:TextBox ID="CCVTextBox" runat="server">
                        </asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="CCVTextBox"
                            runat="server" ErrorMessage="Credit Card Verification Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                 
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="CCVTextBox"
                            ErrorMessage="Please Enter a valid CCV Number" ValidationGroup="Authorize" ValidationExpression="^[\s\S]{0,4}$">*</asp:RegularExpressionValidator>
                    </td>
                   </tr>
                    <tr><td class="style1">
                    <img src="images/cccard.png" alt="credit card" 
                                style="width: 186px; height: 92px"/>
                    </td></tr>
                    </table>                       
                   </td>                   
                </tr>
                <tr>
                    <td colspan="2">
                        Amount<br />
                        <asp:TextBox ID="AmountTextBox" runat="server" Text="" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="AmountTextBox"
                            runat="server" ErrorMessage="Amount is a Required Field" Text="*" ValidationGroup="Authorize"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Currency" Operator="DataTypeCheck"
                            ValidationGroup="Authorize" ErrorMessage="Invalid Amount, only numbers and '.' allowed"
                            ControlToValidate="AmountTextBox">*</asp:CompareValidator><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="authorizecode" runat="server" Visible="false" ></asp:TextBox>
                    <asp:TextBox ID="transcode" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>          
                 
            </table>
</td>
</tr>
<tr>
<td>
<table width="100%">   
<tr><td  colspan="4" style="padding-left:20px; ">
<%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="Authorize"></asp:CustomValidator> 
--%><%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Authorize" />--%><asp:ValidationSummary 
        ID="ValidationSummary1" runat="server" ValidationGroup="Authorize" />
        <span runat="server" id="resultSpan"></span>
    </td></tr>                       
</table> 
</td>
</tr>
<tr>
<td>
<table width="100%">               

<tr>
                <td>
                    <asp:Button ID="btn" CssClass="btncolor"  runat="server" Text="Proceed & Pay Later" 
                        onclick="btn_Click" />
                </td>
                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                    </td>
                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                    Submit Transaction<br />                                    
                </td>
                <td>
                    <asp:ImageButton ID="btn_submit" runat="server" ImageAlign="Baseline" ImageUrl="~/images/r-arrow1.png" CausesValidation="false"  ValidationGroup="Authorize" OnClick="SubmitButton_Click"/>
                </td>
            </tr>
</table> 
</td>
</tr>

</table>

          
       
               
          
            
            <br />
            
         
 
   <br />
<br />                                                                                                    
</asp:Content>