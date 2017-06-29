<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Source.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Source_Add_Source" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Source</span>
   <div class="submenu_style">            
              <div class="buttons">   
 <a href="Browse_Source.aspx?search=&t1=0" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Sources</b>
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
   <asp:DetailsView ID="DetailsView_source" runat="server" AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">      
        <Fields>
            <asp:TemplateField HeaderText="Name" >                               
                <InsertItemTemplate>
                    <asp:TextBox ID="name" runat="server" Width="50%" MaxLength="225" ></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country">              
                <InsertItemTemplate>
                    <asp:DropDownList ID="Countrydp" runat="server" OnLoad="Countrydp_Load">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Country" ControlToValidate="Countrydp" InitialValue="0"></asp:RequiredFieldValidator>
                </InsertItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Description">                 
                <InsertItemTemplate>
                    <CKEditor:CKEditorControl ID="destxt" runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic" ></CKEditor:CKEditorControl>                    
                </InsertItemTemplate>
            </asp:TemplateField>                 
            <asp:TemplateField ShowHeader="False">          
                <InsertItemTemplate>
                   <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" />
                </InsertItemTemplate>
            </asp:TemplateField>
        </Fields>
        <FieldHeaderStyle CssClass="gridview_header" />
        <HeaderStyle CssClass="gridview_header" />
        <AlternatingRowStyle CssClass="gridview_alternative" />
    </asp:DetailsView>
  <br />
  <br />
</ContentTemplate>       
</asp:UpdatePanel>       
</asp:Content>
