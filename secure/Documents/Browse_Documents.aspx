<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Documents.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Documents_Browse_Documents" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Documents</span>
   <div class="submenu_style">            
           <div class="buttons">   
    <a  href="Add_Documents.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Documents</b>
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
           <asp:RadioButtonList ID="searchoption1" runat="server" 
        Repeatdirection="Horizontal"  cellpadding="5" CellSpacing="5">
        <asp:ListItem Selected="True">Name</asp:ListItem>       
        <asp:ListItem>Country</asp:ListItem>       
    </asp:RadioButtonList>
       </li>
        </ul>
    </div>
</div>           
    <br />
    <br />
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
         <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />           
        </Triggers>
            <ContentTemplate>--%>
<br />
<br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_Documents" runat="server"  OnPageIndexChanging="grid_Documents_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_Documents_Load"  AutoGenerateColumns="False" ondatabound="grid_Documents_DataBound" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
<Columns>
<asp:TemplateField HeaderText="Name"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%# "~/secure/Documents/Update_Documents.aspx?did=" + DataBinder.Eval(Container.DataItem,"Id") + "&role=" + DataBinder.Eval(Container.DataItem,"Category") %>' CssClass="link"></asp:HyperLink>
<asp:Label ID="doc_id" Visible="false" runat="server" Text='<%#Eval("Id","{0}")%>'></asp:Label>
<asp:Label ID="Role" Visible="false" runat="server" Text='<%#Eval("Category","{0}")%>'></asp:Label>
<asp:Label ID="country_id" Visible="false" runat="server" Text='<%#Eval("Country_ID","{0}")%>'></asp:Label>
 <asp:Label ID="lblfilename" runat="server" Text='<%# Eval("Attachment") %>' Visible="false"></asp:Label>
</ItemTemplate>
</asp:TemplateField>      
<asp:BoundField DataField="Country" HeaderText="Country"/> 
    <asp:TemplateField HeaderText="Master Description" >
<ItemTemplate>
    <asp:Label ID="lbladmindes" runat="server" Text='<%#Eval("Admindesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Client Description" >
<ItemTemplate>
    <asp:Label ID="lblclientdes" runat="server" Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label>
       <asp:Label ID="lblCdes" runat="server" Visible="false" Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Attachment" >
<ItemTemplate>
<asp:ImageButton ID="btndownload" ImageUrl="~/Code/icons/pdf_icon.png" width="32" Height="32"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 
<%--<a id="btnview" style="text-decoration: none;"  href='<%# "~/secure/viewpdf.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id")+".pdf&type=1" %>' runat="server" ><asp:Image ID="pdfimage" ImageUrl="~/secure/Code/icons/pdf_icon.png" width="42" Height="42"  runat="server" /></a>--%>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Promote" >
<ItemTemplate>
<br />
 <center>
<table>
<tr>
<td><asp:ImageButton ID="btndocuments" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Document.png" /></td>
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
<EmptyDataTemplate>
No Documents Available                       
</EmptyDataTemplate>
</asp:GridView> 
<br />
<br />
<%--</ContentTemplate>       
 </asp:UpdatePanel>--%>
</asp:Content>
