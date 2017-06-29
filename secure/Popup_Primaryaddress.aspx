<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Primaryaddress.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Primaryaddress" %>

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
         <div class="headertag">Primary Mailing Address</div>     
         <br />
         <table width="100%" id="primaryeditmsg" runat="server"  >
          <tr>
                   <td class="editapp_warning" style="background-color:#fce9e9;" >Changes to the primary address will reflect in the below grid where the Mailing Address is set to "Primary" </td>
                   </tr>
                   </table>
         <br />        
         <table id="frm5_primaryform" runat= "server" width="100%">  
                  <tr><td colspan="2" ></td></tr>                                          
                      <tr>                   
                <td colspan="2"> 
                Name: <span style="color:Red;">*</span><br /> 
                    <asp:TextBox ID="frm5_pname" runat="server" Width="313px" MaxLength="50" ></asp:TextBox>
                    <sv:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="frm5_pstate" ErrorMessage="You must enter Name" 
                        ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>                         
                          </td>               
                    </tr>                    
                    <tr>
                    <td>
                    Address Line 1: <span style="color:Red">*</span><br /> 
                    <asp:TextBox ID="frm5_padd1" runat="server" Width="208px" MaxLength="100" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="frm5_pcity"
                            ErrorMessage="You must enter your Address" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td rowspan="2">
                    State/Province:<span style="color:Red;">*</span><br /> 
                       <asp:TextBox ID="frm5_pstate" runat="server" Width="129px" TextMode="MultiLine" 
                            Height="64px" MaxLength="50" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="frm5_pstate"
                            ErrorMessage="You must enter state" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td>
                    Country: <span style="color:Red">*</span><br /> 
                        <asp:DropDownList ID="frm5_pcountry" runat="server" Width="204px" 
                            AppendDataBoundItems="True">
                        </asp:DropDownList>          
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="frm5_pcountry"
                            ErrorMessage="You must select Country" InitialValue="0" 
                            ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    Address Line 2:<br />
                        <asp:TextBox ID="frm5_padd2" runat="server" Width="208px" 
                            MaxLength="100" ></asp:TextBox>
                    </td>
                    <td style="border:solid 1px red;">
                    Delivery type:<span style="color:Red;">*</span><br /> 
                        <asp:DropDownList ID="frm5_pdelivery" runat="server" Width="135px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="frm5_pdelivery"
                            ErrorMessage="You must select Delivery Type" InitialValue="0" 
                            ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    City:<span style="color:Red;">*</span><br /> 
                        <asp:TextBox ID="frm5_pcity" runat="server" Width="129px" MaxLength="50" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="frm5_pcity"
                            ErrorMessage="You must enter city" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td>
                    postal/Zip Code:<span style="color:Red;">*</span><br /> 
                     <asp:TextBox ID="frm5_pzip" runat="server" Width="129px" MaxLength="50" ></asp:TextBox> 
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="frm5_pzip"
                            ErrorMessage="You must enter zipcode" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>  
                    <td></td>               
                    </tr>                
                    <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_pinst" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>             
               <tr>
						<td colspan="3" align="center"> 
                            <asp:Button ID="primaryclear" runat="server" 
                                Text="Clear" OnClick="primaryclear_Click" CssClass="btncolor" 
                                CausesValidation="False" />&nbsp;&nbsp;<asp:Button ID="primarysubmit"
                                runat="server" Text="Submit" OnClick="btn_Click" 
                                ValidationGroup="frm5_group3" CausesValidation="False" 
                                CssClass="btncolor" Visible="False" />
                                <asp:Button ID="primaryupdate" runat="server"  CssClass="btncolor" 
            Text="update" onclick="btn_Click" Visible="False" />
                        </td> 
					</tr>  
					     <tr id="primaryeditgrid" runat="server" ><td colspan="3" > 
					     <br />
					     <hr />
                      <asp:GridView ID="deliverydisplaygrid" runat="server" 
                                 AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                 CssClass="gridview_css"
                                 PagerStyle-CssClass="pgr" 
                           style="TEXT-ALIGN: center">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Recipient">
                                         <ItemTemplate>
                                             <asp:Label ID="name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField> 
                                          <asp:TemplateField HeaderText="Address">                            
                                         <ItemTemplate>
                                             <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                             <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                             <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                             <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                         <ItemTemplate>                                                                                         
                                              <asp:Label ID="lbltype" runat="server" Text='<%# Eval("Type") %>'></asp:Label>                                            
                                               <asp:Label ID="lblid" runat="server" Text='<%# Eval("rowid") %>' Visible="false" ></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField> 
                                         <asp:TemplateField HeaderText="Mailing Address">
                                         <ItemTemplate>                                                                                       
                                              <asp:Label ID="lblsent" runat="server" Text='<%# Eval("Sentto") %>'></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField>                                          
                                          </Columns>
                                 <PagerStyle CssClass="pgr" />
                                 <EmptyDataTemplate>
                                     No Delivery Address Available
                                 </EmptyDataTemplate>
                                 <AlternatingRowStyle CssClass="alt" />
                             </asp:GridView>  
                   </td></tr>								
                    </table>  
        </td>
        </tr>       
	<tr align="right"><td><asp:Button ID="serviceAdd" runat="server"  
            CssClass="btncolor" Text="Add" onclick="btn_Click" Visible="False" />
        </td></tr>
	 </table>                                                                                           
</asp:Content>