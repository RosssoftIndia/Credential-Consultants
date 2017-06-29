<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Client.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Admin_Browse_Client" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
   <span class="title" >Admin &#187; Client</span> 
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
<asp:GridView style="TEXT-ALIGN: center" id="grid_Employee" runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grid_Employee_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_Employee_Load" AutoGenerateColumns="False">
<Columns>
<asp:TemplateField SortExpression="Name" HeaderText="Name"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%# Eval("id", "~/secure/Admin/Client_Login.aspx?id={0}") %>' CssClass="link"></asp:HyperLink>
</ItemTemplate>
</asp:TemplateField>       
    <asp:TemplateField HeaderText="Password" SortExpression="Password">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text="******"></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>
<EmptyDataTemplate>
                          No Client Login Available
                       
</EmptyDataTemplate>
</asp:GridView>
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>          
       
</asp:Content>
