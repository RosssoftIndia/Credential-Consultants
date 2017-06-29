<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_ServiceType.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_ServiceType_Add_ServiceType" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Service Setting</span>
   <div class="submenu_style">            
            <div class="buttons">   
 <a href="Browse_ServiceType.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Service Settings</b>
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
<asp:DetailsView id="DetailsView_service" runat="server" DefaultMode="Insert" AutoGenerateRows="False" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
<Fields>
<asp:TemplateField SortExpression="Name" HeaderText="Service Name"><EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Width="50%" MaxLength="225"></asp:TextBox>
                
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Description" HeaderText="Description"><EditItemTemplate>
                    <CKEditor:CKEditorControl ID="desc" runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                                                  
                
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Name" HeaderText="Cost"><EditItemTemplate>
                    <asp:TextBox ID="Cost" runat="server" Width="10%" ></asp:TextBox>
                
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Type" HeaderText="Type"><EditItemTemplate>
<asp:DropDownList id="type" runat="server" __designer:wfdid="w9">
                        <asp:ListItem>Evaluation</asp:ListItem>
                         <asp:ListItem>Evaluation Multiplier</asp:ListItem>
                        <asp:ListItem>Additional</asp:ListItem>
                        <asp:ListItem>Additional Copy</asp:ListItem>
                        <asp:ListItem>Additional Multiplier</asp:ListItem>
                    </asp:DropDownList> 
</EditItemTemplate>
</asp:TemplateField>
  <asp:TemplateField HeaderText="Client" >          
                <EditItemTemplate>
                <asp:DropDownList ID="dpsubclients" runat="server" onload="dpsubclients_Load"></asp:DropDownList> 
                </EditItemTemplate>
            </asp:TemplateField>  
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
                <table width="100%"><tr><td style="font-weight:bold;">
</td><td width="100px">
<asp:Button id="Add" onclick="Add_Click" runat="server" Width="100px" __designer:wfdid="w10" Text="Add" CommandName="" CausesValidation="false" CssClass="btncolor"></asp:Button> 
</td></tr></table>
</EditItemTemplate>
</asp:TemplateField>
</Fields>
</asp:DetailsView>
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>    
</asp:Content>
