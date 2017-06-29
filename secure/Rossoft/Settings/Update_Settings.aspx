<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Settings.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="_secure_Rossoft_Settings_Update_Settings" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Update Client</span>
  		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
 	            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>
<br />
<br />
   <asp:DetailsView ID="DetailsView_Appsettings" runat="server" CssClass="detailview_css" 
                    PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" 
                    AutoGenerateRows="False" DefaultMode="Edit" 
                    OnLoad="DetailsView_Appsettings_Load" 
                    ondatabound="DetailsView_Appsettings_DataBound">
        <PagerStyle CssClass="pgr" />
        <Fields>
    <asp:TemplateField HeaderText="CustomerId" >
<EditItemTemplate>    
     <asp:TextBox ID="txtCustomerId" runat="server" Text='<%#Eval("CustomerId","{0}") %>'></asp:TextBox>
</EditItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Customer" >
<EditItemTemplate>
<asp:TextBox ID="txtCustomers" runat="server" Text='<%#Eval("Customers","{0}") %>'></asp:TextBox>
</EditItemTemplate>
</asp:TemplateField> 
<asp:TemplateField HeaderText="Startpage" >
<EditItemTemplate>    
    <asp:TextBox ID="txtStartpage" runat="server" Text='<%#Eval("Startpage","{0}") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField> 
    <asp:TemplateField HeaderText="Payment" >
<EditItemTemplate>
     <asp:TextBox ID="txtPayment" runat="server" Text='<%#Eval("Payment","{0}") %>'></asp:TextBox>
     </EditItemTemplate>
</asp:TemplateField> 
     <asp:TemplateField HeaderText="TypeSwitcher" >
<EditItemTemplate>
    <asp:TextBox ID="txtTypeSwitcher" runat="server" Text='<%#Eval("TypeSwitcher","{0}") %>'></asp:TextBox>
      </EditItemTemplate>
</asp:TemplateField> 
      <asp:TemplateField HeaderText="AdminId" >
<EditItemTemplate>
       <asp:TextBox ID="txtAdminId" runat="server" Text='<%#Eval("AdminId","{0}") %>'></asp:TextBox>
       </EditItemTemplate>
</asp:TemplateField>        
       <asp:TemplateField HeaderText="Deslimit" >
<EditItemTemplate>
    <asp:TextBox ID="txtdeslimit" runat="server" Text='<%#Eval("Deslimit","{0}") %>'></asp:TextBox>
     </EditItemTemplate>
</asp:TemplateField> 
     <asp:TemplateField HeaderText="SessionTime" >
<EditItemTemplate>
    <asp:TextBox ID="txtSessionTime" runat="server" Text='<%#Eval("SessionTime","{0}") %>'></asp:TextBox>
      </EditItemTemplate>
</asp:TemplateField> 
      <asp:TemplateField HeaderText="Startyear" >
<EditItemTemplate>
         <asp:TextBox ID="txtStartyear" runat="server" Text='<%#Eval("Startyear","{0}") %>'></asp:TextBox>
           </EditItemTemplate>
</asp:TemplateField> 
           <asp:TemplateField HeaderText="Endyear" >
<EditItemTemplate>
    <asp:TextBox ID="txtEndyear" runat="server" Text='<%#Eval("Endyear","{0}") %>'></asp:TextBox>
      </EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
<asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" Text="Update" OnClick="Update_Click" />
                
</EditItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
</asp:TemplateField> 
        </Fields>
        <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
 <br />
 <br />
</ContentTemplate>       
</asp:UpdatePanel>   
</asp:Content>
