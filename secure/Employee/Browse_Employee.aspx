<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Employee.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Employee_Browse_Employee" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Browse Employee</span>
   <div class="submenu_style">            
            <div class="buttons">    
    <a  href="Add_Employee.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Create Employee</b>
    </a>  
</div>    
		</div>
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
<asp:GridView style="TEXT-ALIGN: center" id="grid_Employee" runat="server" OnPageIndexChanging="grid_Employee_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_Employee_Load" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False">
<Columns>
<asp:TemplateField SortExpression="Name" HeaderText="Name"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%# Eval("id", "~/secure/Employee/Update_Employee.aspx?Empid={0}") %>' CssClass="link"></asp:HyperLink>
</ItemTemplate>
</asp:TemplateField>       
    <asp:TemplateField HeaderText="Password" SortExpression="Password">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text="******"></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>
<EmptyDataTemplate>
                          No Employee Available                       
</EmptyDataTemplate>
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
