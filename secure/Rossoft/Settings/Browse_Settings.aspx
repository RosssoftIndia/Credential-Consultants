<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Settings.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="_secure_Rossoft_Settings_Browse_Settings" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
   <span class="title" >App &#187; Setting</span> 
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
<asp:GridView style="TEXT-ALIGN: center" id="grid_Appsetting" runat="server" 
                    CssClass="gridview_css" PagerStyle-CssClass="pgr"  
                    AlternatingRowStyle-CssClass="alt" 
                    OnPageIndexChanging="grid_Appsetting_PageIndexChanging" PageSize="25" 
                    AllowPaging="True" OnLoad="grid_Appsetting_Load" AutoGenerateColumns="False" 
                    ondatabound="grid_Appsetting_DataBound">
<Columns>
<asp:TemplateField HeaderText="CustomerId" >
<ItemTemplate>
    <asp:Label ID="lblCustomerId" runat="server" Text='<%#Eval("CustomerId","{0}") %>'></asp:Label>
</ItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Customer" >
<ItemTemplate>
<asp:HyperLink id="linkCustomers" runat="server" Text='<%#Eval("Customers","{0}") %>' NavigateUrl='<%# "~/secure/Rossoft/Settings/Update_Settings.aspx?id="+ DataBinder.Eval(Container.DataItem,"Id")%>' CssClass="link"></asp:HyperLink>
</ItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Startpage" >
<ItemTemplate>    
    <asp:Label ID="lblStartpage" runat="server" Text='<%#Eval("Startpage","{0}") %>'></asp:Label>
    </ItemTemplate> 
</asp:TemplateField> 
    <asp:TemplateField HeaderText="Payment" >
<ItemTemplate>
     <asp:Label ID="lblPayment" runat="server" Text='<%#Eval("Payment","{0}") %>'></asp:Label>
     </ItemTemplate> 
</asp:TemplateField> 
     <asp:TemplateField HeaderText="TypeSwitcher" >
<ItemTemplate>
      <asp:Label ID="lblTypeSwitcher" runat="server" Text='<%#Eval("TypeSwitcher","{0}") %>'></asp:Label>
      </ItemTemplate> 
</asp:TemplateField> 
      <asp:TemplateField HeaderText="AdminId" >
<ItemTemplate>
       <asp:Label ID="lblAdminId" runat="server" Text='<%#Eval("AdminId","{0}") %>'></asp:Label>
       </ItemTemplate> 
</asp:TemplateField>        
       <asp:TemplateField HeaderText="Deslimit" >
<ItemTemplate>
     <asp:Label ID="lbldeslimit" runat="server" Text='<%#Eval("Deslimit","{0}") %>'></asp:Label>
     </ItemTemplate> 
</asp:TemplateField> 
     <asp:TemplateField HeaderText="SessionTime" >
<ItemTemplate>
      <asp:Label ID="lblSessionTime" runat="server" Text='<%#Eval("SessionTime","{0}") %>'></asp:Label>
      </ItemTemplate> 
</asp:TemplateField> 
      <asp:TemplateField HeaderText="Startyear" >
<ItemTemplate>
           <asp:Label ID="lblStartyear" runat="server" Text='<%#Eval("Startyear","{0}") %>'></asp:Label>
           </ItemTemplate> 
</asp:TemplateField> 
           <asp:TemplateField HeaderText="Endyear" >
<ItemTemplate>
      <asp:Label ID="lblEndyear" runat="server" Text='<%#Eval("Endyear","{0}") %>'></asp:Label>
      </ItemTemplate> 
</asp:TemplateField> 
</Columns>
<EmptyDataTemplate>
 No App Settings Available                       
</EmptyDataTemplate>
</asp:GridView>
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>          
       
</asp:Content>
