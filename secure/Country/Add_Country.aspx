<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Country.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Country_Add_Country" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Country</span>
 <div class="submenu_style">
  <div class="buttons">   
 <a href="Browse_Country.aspx?search=" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Countries</b>
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
   <asp:DetailsView ID="DetailsView_Country" runat="server" AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
        <Fields>
            <asp:TemplateField HeaderText="Country">              
                <InsertItemTemplate>
                    <asp:TextBox ID="Countrytxt" runat="server" Width="50%" MaxLength="225"></asp:TextBox>
                </InsertItemTemplate>               
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="Description">              
                <InsertItemTemplate>
                        <CKEditor:CKEditorControl ID="countrydesc" runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic" ></CKEditor:CKEditorControl>
                </InsertItemTemplate>               
            </asp:TemplateField>            
            <asp:TemplateField ShowHeader="False">
                <ControlStyle Width="100px" />
                <InsertItemTemplate>
                    <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" Text="Add" OnClick="Add_Click" />
                </InsertItemTemplate>
            </asp:TemplateField>            
        </Fields>      
    </asp:DetailsView>
  <br />
  <br />
</ContentTemplate>       
 </asp:UpdatePanel>     
</asp:Content>
