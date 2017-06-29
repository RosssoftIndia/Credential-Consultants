<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Evaladdress.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Evaladdress" %>

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
                            <tr ID="frm5_evalgridblock" runat="server" visible="false">
                                <td>
                                    <asp:GridView ID="officialgrid" runat="server" 
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                        CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                                        style="TEXT-ALIGN: center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="Official Hard Copy"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Recipient" HeaderText="Recipient" ReadOnly="True" 
                                                SortExpression="Recipient" />
                                            <asp:BoundField DataField="Count" HeaderText="No Of Copies" 
                                                SortExpression="Count" />
                                            <asp:BoundField DataField="Name" HeaderText="Delivery Type" 
                                                SortExpression="Name" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Official Hard Copy Requested
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
<table width="100%"  class="edit_css"> 
    <tr><td>      
    <b>Official Hard Copy Delivery-1:(primary mailing address)</b>
    <br />
		             <br /> 
		             <asp:DetailsView ID="frm5_primarygrid" runat="server" AutoGenerateRows="False" 
                        CssClass="detailview_css"  PagerStyle-CssClass="pgr"  
                        AlternatingRowStyle-CssClass="alt">
                       <Fields>                          
                                 <asp:TemplateField HeaderText="Name" >                          
                               <ItemTemplate>             
                               <span><%# Eval("Name")%></span> 
                               </ItemTemplate>
                           </asp:TemplateField>                         
                           <asp:TemplateField HeaderText="Address" SortExpression="Address">                             
                               <ItemTemplate>
                                  <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                             <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                             <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                             <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>                      
                       </Fields>   
                       <EmptyDataTemplate>
                                     No Primary Address Available
                                 </EmptyDataTemplate>                    
                   </asp:DetailsView>                  
                   </td></tr>  
      <tr>
        <td>                  
<div id="dynamic_official" runat="server" class="infobox">
<div id="frm5_overlap" runat="server" class="more">
<div align="center"  style="position:absolute; top:60%; height:100%; width:100%;margin-top:-5em;" >
<table><tr><td><img src="Code/menu/warning.png"/> 
</td><td style="vertical-align:middle;">Fill In the Official Hard Copy Delivery-1:(primary mailing address) for primary mailing address.</td></tr></table>
</div></div>
</div> 
<br />
        </td></tr>
           <tr id="frm5_evalform" runat="server" visible="false">
       <td>
       <br />
        <div class="headertag">Delivery Address Information&nbsp;<span id="frm5_evalformheader" runat="server"></span></div>
        <br />
        <table class="search_css" > 
                    <tr> 
                    <td>
                        <table width="100%">                           
                            <tr>
                                <td colspan="2">
                                    Name: <span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_Fnameeval" runat="server" MaxLength="50" Width="313px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="frm5_Fnameeval" ErrorMessage="You must enter Name" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                  
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 1: <span style="color:Red">*</span><br />
                                    <asp:TextBox ID="frm5_add1eval" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="frm5_add1eval" ErrorMessage="You must enter your Address" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                    
                                </td>
                                <td rowspan="2">
                                    State/Province:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_stateeval" runat="server" Height="64px" MaxLength="50" 
                                        TextMode="MultiLine" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="frm5_stateeval" ErrorMessage="You must enter state" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                   
                                </td>
                                <td>
                                    Country: <span style="color:Red">*</span><br />
                                    <asp:DropDownList ID="frm5_countryeval" runat="server" AppendDataBoundItems="True" 
                                        Width="204px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="frm5_countryeval" ErrorMessage="You must select Country" 
                                        InitialValue="0" ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 2:<br />
                                    <asp:TextBox ID="frm5_add2eval" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                </td>
                                <td>
                                    Delivery type:<span style="color:Red;">*</span><br />
                                    <asp:DropDownList ID="frm5_deliverytypeeval" runat="server" 
                                        AppendDataBoundItems="true" Width="135px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator7" runat="server" 
                                        ControlToValidate="frm5_deliverytypeeval" 
                                        ErrorMessage="You must select Delivery Type" InitialValue="0" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_cityeval" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="frm5_cityeval" ErrorMessage="You must enter city" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                    
                                </td>
                                <td>
                                    postal/Zip Code:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_zipeval" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="frm5_zipeval" ErrorMessage="You must enter zipcode" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                   
                                </td>
                                <td>
                                    <asp:Label ID="frm5_hiddenvalue" runat="server" Visible="False"></asp:Label>                                   
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_instname" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                 <asp:Button ID="frm5_btn_cleareval" runat="server" CssClass="btncolor" 
                                        OnClick="frm5_btn_cleareval_Click" Text="Clear" />
                                    &nbsp;&nbsp;<asp:Button ID="frm5_btn_submiteval" runat="server" CausesValidation="False" 
                                        CssClass="btncolor" OnClick="frm5_btn_submiteval_Click" Text="Submit" 
                                        ValidationGroup="frm5_group" />
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