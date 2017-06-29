<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Us_Equivalency.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_UsEquivalency_Add_Us_Equivalency" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Equivalency</span>
   <div class="submenu_style">            
          <div class="buttons">   
 <a href="Browse_Us_Equivalency.aspx?search=" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Equivalencies</b>
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
   <asp:DetailsView ID="DetailsView_Equivalency" runat="server" AutoGenerateRows="False" DefaultMode="Insert"  CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
           <Fields>
            <asp:TemplateField HeaderText="Name" >                               
                <InsertItemTemplate>
                    <asp:TextBox ID="name" runat="server" Width="50%" MaxLength="225"  ></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>              
            <asp:TemplateField HeaderText="Description">                 
                <InsertItemTemplate>
                    <CKEditor:CKEditorControl ID="destxt" runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic" ></CKEditor:CKEditorControl>                    
                </InsertItemTemplate>
            </asp:TemplateField>                 
            <asp:TemplateField ShowHeader="False">          
                <InsertItemTemplate>
                   <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" />
                </InsertItemTemplate>
            </asp:TemplateField>
        </Fields>     
    </asp:DetailsView>
    <br />
    <br />
  </ContentTemplate>       
</asp:UpdatePanel>      
</asp:Content>
