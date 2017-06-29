<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_DeliveryType.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_DeliveryType_Add_DeliveryType" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Delivery Setting</span>
   <div class="submenu_style">  
    <div class="buttons">   
 <a href="Browse_DeliveryType.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Delivery Settings</b>
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
    <asp:DetailsView ID="DetailsView_Delivery" runat="server" 
    AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css" 
     PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" >
        <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:TemplateField HeaderText="Delivery Type" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server"  Width="50%" MaxLength="225" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
                   <asp:TemplateField SortExpression="Description" HeaderText="Description">
              <EditItemTemplate>
                  <CKEditor:CKEditorControl ID="desc" runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                                                  
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Cost" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="Cost" runat="server"  Width="10%"  ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="Type">          
                <EditItemTemplate>
                    <asp:DropDownList ID="type" runat="server" >
                          <asp:ListItem>Mail</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>    
                           <asp:ListItem>Email</asp:ListItem>    
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>            
               <asp:TemplateField HeaderText="Client" >          
                <EditItemTemplate>
                <asp:DropDownList ID="dpsubclients" runat="server" onload="dpsubclients_Load"></asp:DropDownList> 
                </EditItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                                <table width="100%"><tr><td style="font-weight:bold;">
</td><td width="100px">
                     <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" />
                        </td></tr></table>
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>
          <AlternatingRowStyle CssClass="alt" />
          </asp:DetailsView>
  <br />
  <br />
</ContentTemplate>       
</asp:UpdatePanel>        
</asp:Content>
