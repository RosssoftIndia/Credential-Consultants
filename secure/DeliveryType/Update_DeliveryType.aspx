<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_DeliveryType.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_DeliveryType_Update_DeliveryType" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Delivery Setting</span>
  <div class="submenu_style">  
   <div class="buttons">   
 <a href="Browse_DeliveryType.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Delivery Settings</b>
    </a>  
  <%--  <a  href="Add_DeliveryType.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Delivery Setting</b>
    </a>  --%>
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
   <asp:DetailsView ID="DetailsView_Delivery" runat="server" AutoGenerateRows="False" 
                    DefaultMode="Edit" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" 
                    OnLoad="DetailsView_Delivery_Load" ondatabound="DetailsView_Delivery_DataBound">
   <PagerStyle CssClass="pgr" />                 
       
        <Fields>
            <asp:TemplateField HeaderText="Delivery Type" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="name" Width="50%" MaxLength="225"  runat="server" Text='<%# Eval("Name") %>' ></asp:TextBox>
                    <asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField SortExpression="Description" HeaderText="Description">
              <EditItemTemplate>
                  <CKEditor:CKEditorControl ID="desc" value='<%# Eval("Description") %>' runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                                                  
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Cost" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="Cost"  Width="10%"  runat="server" Text='<%# Eval("Cost") %>' ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="Type">          
                <EditItemTemplate>
                    <asp:DropDownList ID="type" runat="server" SelectedValue='<%# Eval("Type") %>'>
                        <asp:ListItem>Mail</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>                        
                           <asp:ListItem>Email</asp:ListItem>                       
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                <table width="100%"><tr><td style="font-weight:bold;padding-left: 0px;">Client:&nbsp;
<asp:Label ID="clientbottom" CssClass="SubclientEntry" runat="server" Text="Label"></asp:Label>  
</td><td width="100px">
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Update_Click"
                        Text="Update" Width="100px" />
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
