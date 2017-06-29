<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Institution.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Institution_Browse_Institution" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Institutions</span>
   <div class="submenu_style">            
            <div class="buttons">    
    <a  href="Add_Institution.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Institution</b>
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
<br />
    <div class="group">
        <ul>
            <li>
             <asp:RadioButtonList ID="searchoption0" runat="server" 
        Repeatdirection="Horizontal" cellpadding="5" CellSpacing="5">
      <asp:ListItem Selected="True">Name</asp:ListItem>
        <asp:ListItem >Country</asp:ListItem>       
    </asp:RadioButtonList>
          </li> 
          <li>
            <asp:RadioButtonList ID="searchoption1" runat="server" 
        Repeatdirection="Horizontal"  cellpadding="5" CellSpacing="5">
        <asp:ListItem Selected="True">HighSchool</asp:ListItem>       
        <asp:ListItem>University</asp:ListItem>       
    </asp:RadioButtonList>
          </li>
          <li>
            <asp:RadioButtonList ID="searchoption3" runat="server" 
        Repeatdirection="Horizontal"  cellpadding="5" CellSpacing="5">
          <asp:ListItem Selected="True" Value="0">Degree Mill:No</asp:ListItem>    
        <asp:ListItem  Value="1">Degree Mill:Yes</asp:ListItem>                
    </asp:RadioButtonList>
          </li>
          <li>
             <asp:RadioButtonList ID="searchoption2" runat="server" 
        Repeatdirection="Horizontal" cellpadding="5" CellSpacing="5">
      <asp:ListItem Selected="True" Value="1">Confirmed</asp:ListItem>
        <asp:ListItem Value="0">Unconfirmed</asp:ListItem>    
         <asp:ListItem Value="2">Both</asp:ListItem>          
    </asp:RadioButtonList>
            </li>
        </ul>
    </div>
</div>        
           
<br />
<br />
 <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>   
<br />
<br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_institution" runat="server" OnPageIndexChanging="grid_institution_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_institution_Load" AutoGenerateColumns="False" ondatabound="grid_institution_DataBound" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
<Columns>
<asp:TemplateField SortExpression="Name" HeaderText="Institution Name"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%#"~/secure/Institution/update_Institution.aspx?instid=" + DataBinder.Eval(Container.DataItem,"Id") + "&role=" + DataBinder.Eval(Container.DataItem,"Category") %>' CssClass="link"></asp:HyperLink>    
<asp:Label ID="institution_id" Visible="false" runat="server" Text='<%#Eval("id","{0}")%>'></asp:Label>
<asp:Label ID="Role" Visible="false" runat="server" Text='<%#Eval("Category","{0}")%>'></asp:Label>
<asp:Label ID="ClientId" Visible="false" runat="server" Text='<%#Eval("Customer_Id","{0}")%>'></asp:Label>
<asp:Label ID="country_id" Visible="false" runat="server" Text='<%#Eval("Country_ID","{0}")%>'></asp:Label>
<asp:Label ID="lblenable" Visible="false" runat="server" Text='<%#Eval("IsEnabled","{0}")%>'></asp:Label>
<asp:Label ID="lblmill" Visible="false" runat="server" Text='<%#Eval("IsDegreeMill","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>      
   <asp:TemplateField HeaderText="Type">      
        <ItemTemplate>
            <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>   
    <asp:TemplateField HeaderText="Confirmed" SortExpression="Confirmed">      
        <ItemTemplate>
            <asp:Label ID="lblconfirmed" runat="server" Text='<%# Eval("Confirmed") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>     
    <asp:TemplateField HeaderText="Master Description" >
<ItemTemplate>
    <asp:Label ID="lbladmindes" runat="server" Text='<%#Eval("Admindesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Client Description" >
<ItemTemplate>
    <asp:Label ID="lblclientdes" runat="server" Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label>
      <asp:Label ID="lblCdes" runat="server" Visible="false"  Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Promote" >
<ItemTemplate>
<br />
 <center>
<table>
<tr>
<td><asp:ImageButton ID="btninstitution" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Institution.png" /></td>
</tr>
<tr>
<td><asp:ImageButton ID="btndescription" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Description.png" /></td>
</tr></table>
 </center>
<br />
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
No Institution Available                       
</EmptyDataTemplate>
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
