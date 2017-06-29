<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Country.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Country_Browse_Country" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Countries</span>
 
   <div class="submenu_style">   
    <div class="buttons">   
 <a id="opt" runat="server" visible="false"  href="Add_Country.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Country</b>
    </a>  
</div>                       
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
<div class="searchblk">
<table><tr><td><label for="searchtxt">Search Terms:</label><asp:TextBox ID="searchbox" runat="server" ></asp:TextBox><%--<sv:RequiredFieldValidator id="validator" runat="server" ErrorMessage="Enter value in the searchbox" ControlToValidate="searchbox">*</sv:RequiredFieldValidator>--%></td><td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>      
</div>           
    <br />
    <br />
<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
         <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />           
        </Triggers>
            <ContentTemplate>           

<asp:GridView style="TEXT-ALIGN: center" id="grid_Country" runat="server"   OnPageIndexChanging="grid_Country_PageIndexChanging" PageSize="25" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AllowPaging="True" OnLoad="grid_Country_Load" AutoGenerateColumns="False" ondatabound="grid_Country_DataBound">
<Columns>
<asp:TemplateField HeaderText="Country">
<ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%# Eval("Name", "{0}") %>' NavigateUrl='<%#"~/secure/Country/Update_Country.aspx?Ctrid=" + DataBinder.Eval(Container.DataItem,"Id") + "&role=" + DataBinder.Eval(Container.DataItem,"Type")%>'  CssClass="link"></asp:HyperLink>
 <asp:Label ID="country_id" Visible="false" runat="server" Text='<%#Eval("Id","{0}")%>'></asp:Label>
   <asp:Label ID="Role" Visible="false" runat="server" Text='<%#Eval("Type","{0}")%>'></asp:Label>
   <asp:Label ID="ClientId" Visible="false" runat="server" Text='<%#Eval("Customer_Id","{0}")%>'></asp:Label>
</ItemTemplate>    
</asp:TemplateField>
<asp:TemplateField HeaderText="Master Description" >
<ItemTemplate>
    <asp:Label ID="lbladmindes" runat="server" Text='<%#Eval("Admindesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Client Description" >
<ItemTemplate> 
 <asp:Label ID="lblclientdes" runat="server"   Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label> 
  <asp:Label ID="lblCdes" runat="server" Visible="false"  Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="" >
<ItemTemplate>  
<asp:ImageButton ID="btndescription" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Promote.png" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="IsEnabled" >
<ItemTemplate>
<center>
<asp:ImageButton ID="btn" runat="server" onclick="btn_Click" ImageUrl="~/secure/Code/button/Enable.png" />
    <asp:Label ID="IsEnable" runat="server" Visible="false"  Text='<%# Eval("IsEnabled") %>'></asp:Label>
</center>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:ImageButton ID="del" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/remove.png" OnClick="del_Click" Text="Delete" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
    <PagerStyle CssClass="pgr" />
<EmptyDataTemplate>
No Country Available                       
</EmptyDataTemplate>
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
