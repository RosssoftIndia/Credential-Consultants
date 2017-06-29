<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Template.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Template_Browse_Template" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server"> 
  <span class="title" >Browse Template</span>  
 <div class="submenu_style">            
<div class="buttons"> 
    <a href="Upload_Template.aspx" class="regular">
       <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Upload Templates</b>
    </a>  
</div>      
		</div>
		<br />
		<br />			
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
 <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">   
<Triggers>
            <asp:AsyncPostBackTrigger  ControlID="drpclient" />  
                
        </Triggers>        
            <ContentTemplate>                   
<div class="searchblk">
<table>
<tr>
<td>Client:</td>
<td>
 <asp:DropDownList ID="drpclient" runat="server" onload="drpclient_Load" 
            AppendDataBoundItems="True" AutoPostBack="True" 
            onselectedindexchanged="drpclient_SelectedIndexChanged">
        </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="drpclient" ErrorMessage="Select Client" InitialValue="0">*</asp:RequiredFieldValidator>
</td>
<td>Sub Client</td>
    <td> <asp:DropDownList ID="drpsubclient" runat="server"  AppendDataBoundItems="True">
        </asp:DropDownList></td>
<td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>      
</div> 
</ContentTemplate>
</asp:UpdatePanel>         
    <br />
    <br />
   
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">   
<Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />  
                
        </Triggers>        
            <ContentTemplate>       
<br />
<br />		
<asp:GridView style="TEXT-ALIGN: center" id="grid_Template" runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  PageSize="25" AllowPaging="True"  AutoGenerateColumns="False">
<Columns>
<asp:TemplateField HeaderText="Client">
<ItemTemplate>                         
<asp:Label ID="lblclient" runat="server" Text='<%#Eval("Client") %>'></asp:Label>
</ItemTemplate> 
</asp:TemplateField>   
<asp:TemplateField HeaderText="Template Name">
<ItemTemplate>                         
<asp:Label ID="lblfilename" runat="server" Text='<%#Eval("Template Name") %>'></asp:Label>
</ItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:Label ID="path" runat="server" Visible="False" Text='<%# Bind("Path") %>'></asp:Label>
<asp:ImageButton ID="del" runat="server" CausesValidation="False" 
CommandName="Delete" ImageUrl="~/images/remove.png" 
OnClick="del_Click" Text="Delete" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>
No Template Available                       
</EmptyDataTemplate>
</asp:GridView>
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>  
       
</asp:Content>
