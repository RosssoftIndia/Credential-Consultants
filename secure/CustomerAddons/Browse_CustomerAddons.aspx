<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_CustomerAddons.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_CustomerAddons_Browse_CustomerAddons" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Application Settings</span>   
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>
             <div class="Select_client">Client:&nbsp;<asp:DropDownList ID="dpsubclients" 
                    runat="server" onselectedindexchanged="dpsubclients_SelectedIndexChanged" 
                    AutoPostBack="True">
    </asp:DropDownList></div>
               
            <br />
            <br />
<asp:DetailsView id="DetailsView_Customer" runat="server" AutoGenerateRows="False" 
                    CssClass="detailview_css"  PagerStyle-CssClass="pgr"   
                    AlternatingRowStyle-CssClass="alt">   
    <PagerStyle CssClass="pgr" />
<Fields>
<asp:TemplateField ShowHeader="False"><ItemTemplate>
<table width="100%"><tr><td></td><td width="100px">
    <asp:Button ID="Updatetop" runat="server" CssClass="btncolor" 
         Text="Edit Settings" 
        PostBackUrl='<%#"~/secure/CustomerAddons/Update_CustomerAddons.aspx?clid=" + DataBinder.Eval(Container.DataItem,"Customer_Id")%>' />   
   </td></tr></table>             
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
   Terms And Condition Section
    </ItemTemplate>     
     <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="Terms And Condition" ShowHeader="false">
<ItemTemplate>
    <asp:Label ID="lblTerms" runat="server" Text='<%# Bind("Terms_And_Condition") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" /> 
</asp:TemplateField>
    <asp:TemplateField ShowHeader="false" >
<ItemTemplate>
   Education Section
</ItemTemplate>
     <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Education Instructions">
<ItemTemplate>
    <asp:Label ID="lblEducationInstructions" runat="server" Text='<%# Bind("Education_Instructions") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Document Instructions">
<ItemTemplate>
    <asp:Label ID="lblDocumentInstructions" runat="server" Text='<%# Bind("Document_Instructions") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" />
</asp:TemplateField>

 <asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
    Delivery Section
    </ItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
    <asp:BoundField DataField="Delivery_copy" HeaderText="No of Delivery copies" >
    <HeaderStyle Width="200px" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="Delivery Instructions">
<ItemTemplate>
    <asp:Label ID="lblDeliveryInstructions" runat="server" Text='<%# Bind("Delivery_Instructions") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" />
</asp:TemplateField>
    <asp:TemplateField ShowHeader="false" >
<ItemTemplate>
    Payment Section
</ItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
</asp:TemplateField>
      <asp:TemplateField ShowHeader="false" >
<ItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Online Payments</div>
  <div style="float:right;padding-right: 10px;">
<asp:RadioButtonList ID="searchoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("CreditCard") %>' Enabled="false">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>      
  </div>
  </ItemTemplate>
  </asp:TemplateField>
  <asp:TemplateField ShowHeader="false" >
 <ItemTemplate>
<div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">CreditCard</div>
<div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="Onlinecc" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Onlinecc") %>' Enabled="false">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>  
   </div>
  </ItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField>
  <asp:TemplateField HeaderText="CreditCard" ShowHeader="false" >
<ItemTemplate>      
   <asp:Label ID="CreditCardinst" runat="server" Text='<%# Bind("Creditcard_Instructions") %>' ></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="false" >
   <ItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;">Supported Cards</div>
</ItemTemplate> 
  </asp:TemplateField>
<asp:TemplateField HeaderText="Supported Cards" ShowHeader="false">
<ItemTemplate>
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" Width="100%" RepeatColumns="3" RepeatLayout="Table" Enabled="false">           
       </asp:CheckBoxList>
</ItemTemplate>
</asp:TemplateField>
  <asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Money Order</div>
  <div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="morder" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Moneyorder") %>'  Enabled="false">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>  
   </div>
   </ItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField> 
<asp:TemplateField HeaderText="Money Order" ShowHeader="false">
<ItemTemplate>
<asp:Label ID="morderinst" runat="server" Text='<%# Bind("Moneyorder_Instructions") %>' ></asp:Label> 
</ItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField ShowHeader="false" >
   <ItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Personal check</div>
  <div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="pcheck" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Personalcheck") %>'  Enabled="false">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList> 
     </div>
   </ItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField> 
<asp:TemplateField HeaderText="Personal check" ShowHeader="false">
<ItemTemplate>
  <asp:Label ID="pcheckinst" runat="server"  Text='<%# Bind("Personalcheck_Instructions") %>' ></asp:Label>
</ItemTemplate>
</asp:TemplateField> 
  <asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
    Review Section
    </ItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
     <asp:TemplateField HeaderText="Special Instructions">
<ItemTemplate>
    <asp:Label ID="lblspecialInstructions" runat="server" Text='<%# Bind("Spl_Instruction") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" />
</asp:TemplateField>
     <asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
    Completed Section
    </ItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
         <asp:TemplateField HeaderText="Completed Instructions">
<ItemTemplate>
    <asp:Label ID="lblcompletedInstructions" runat="server" Text='<%# Bind("Completed_Instruction") %>'></asp:Label>
</ItemTemplate>
<ItemStyle CssClass="justify" />
</asp:TemplateField>
  <asp:TemplateField ShowHeader="false" >
    <ItemTemplate>
    Configurable Sections
    </ItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="Application Type">
<ItemTemplate>
    <asp:RadioButtonList ID="List_App_type" runat="server" RepeatDirection="Vertical" SelectedValue='<%# Eval("Application_Type") %>' Enabled="false"   >
    <asp:ListItem Text="Full Process with Education History" Value="1"></asp:ListItem> 
    <asp:ListItem Text="Type 2" Value="2"></asp:ListItem> 
    <asp:ListItem Text="Short Process without Education History" Value="3"></asp:ListItem> 
    </asp:RadioButtonList>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Service">
<ItemTemplate>
<table>
<tr><td><asp:CheckBox ID="chk_Mail" runat="server" Text="Additional" Checked='<%# Eval("Additional_Section") %>' Enabled="false" /></td></tr>
<tr><td><asp:CheckBox ID="chk_Fax" runat="server" Text="Fax" Checked='<%# Eval("Fax_Section") %>' Enabled="false" /></td></tr>
<tr><td><asp:CheckBox ID="chk_Email" runat="server" Text="Email" Checked='<%# Eval("Email_Section") %>' Enabled="false" /></td></tr>
</table>
</ItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Talent Database">
<ItemTemplate>
<asp:RadioButtonList ID="tdbuoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Talent_Database") %>' Enabled="false" >
      <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>      
    </asp:RadioButtonList>          
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Purpose Lock">
<ItemTemplate>
<asp:RadioButtonList ID="Purposeoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Purpose_Section") %>' Enabled="false">
        <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>        
    </asp:RadioButtonList>      
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Applicant Upload">
<ItemTemplate>
<asp:RadioButtonList ID="Uploadoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Applicant_Upload") %>' Enabled="false">
        <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>        
    </asp:RadioButtonList>      
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Thank You Page">
<ItemTemplate>
<asp:RadioButtonList ID="thankuoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("ThkuPage") %>' Enabled="false">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>          
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False"><ItemTemplate>
<table width="100%"><tr><td></td><td width="100px">
    <asp:Button ID="Update" runat="server" CssClass="btncolor" 
         Text="Edit Settings" 
        PostBackUrl='<%#"~/secure/CustomerAddons/Update_CustomerAddons.aspx?clid=" + DataBinder.Eval(Container.DataItem,"Customer_Id")%>' />   
              </td></tr></table>  
</ItemTemplate>
</asp:TemplateField>
</Fields>    
    <AlternatingRowStyle CssClass="alt" />
</asp:DetailsView>
<br />
<br />
</ContentTemplate>       
        </asp:UpdatePanel>  
</asp:Content>
