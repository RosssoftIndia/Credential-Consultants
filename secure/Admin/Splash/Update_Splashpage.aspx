<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Splashpage.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Admin_Splash_Update_Splashpage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Splash Settings</span>   
  <div class="submenu_style">   
  <div class="buttons">   
<%--<a href="Browse_Splashpage.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Settings</b>
    </a>--%>
</div>      
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">  
        <Triggers>
        
        </Triggers>  
            <ContentTemplate>--%>
            <br />
            <br />
<asp:DetailsView id="DetailsView_Splash" runat="server" 
        OnLoad="DetailsView_Splash_Load" DefaultMode="Edit" AutoGenerateRows="False" 
        CssClass="detailview_css"  PagerStyle-CssClass="pgr"  
        AlternatingRowStyle-CssClass="alt" ondatabound="DetailsView_Splash_DataBound">
<Fields>
<asp:TemplateField  HeaderText="App Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="appinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("AppInstructions") %>' ></CKEditor:CKEditorControl>
    <asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Browser Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="brinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("BrowserInstructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
<%--<asp:TemplateField  HeaderText="Client Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="clinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("ClientInstructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>--%>
<asp:TemplateField  HeaderText="OfflineApp Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="offinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("OfflineAppInstructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Offline Application"><EditItemTemplate>
<table style="Width:100%;" >
<tr>
<td>Enable :</td>
<td>
<asp:RadioButtonList ID="Rbt" runat="server" RepeatDirection="Horizontal"  SelectedValue='<%# Bind("OfflineApp") %>'   >
    <asp:ListItem Text="Yes" Value="True"  ></asp:ListItem>
    <asp:ListItem Text="No" Value="False"  ></asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td>Upload File :</td>
<td> <asp:FileUpload ID="Appuploader" runat="server" /></td>
</tr>
<tr>
<td>Existing/Uploaded File :</td>
<td>
  <asp:Panel ID="pdfpanel" runat="server">
    <table>
    <tr><td style="text-align:center;" >
        <asp:HyperLink ID="Imgpdf" Target="_blank"  runat="server"><asp:Image ID="img" runat="server" ImageUrl="~/secure/Code/icons/pdf_icon.png" width="42px" Height="42px" /></asp:HyperLink></td></tr>
    <tr><td>Application.pdf</td></tr>
    </table>     
    </asp:Panel>
    <asp:Label ID="lblpdf" Visible="false"  runat="server" Text="No File Available!"></asp:Label>
</td>
</tr>
</table>

   
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False">
<EditItemTemplate>
<asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" Text="Update" OnClick="Update_Click" />
</EditItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
</asp:TemplateField>
</Fields>
</asp:DetailsView> 
<br />
<br />
<%--</ContentTemplate>       
         </asp:UpdatePanel> --%>          
   </asp:Content>
