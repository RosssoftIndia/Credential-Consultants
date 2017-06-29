<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_ServiceType.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_ServiceType_Update_ServiceType" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Service Setting</span>
   <div class="submenu_style">    
    <div class="buttons">   
 <a href="Browse_ServiceType.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Service Settings</b>
    </a>  
    <%--<a  href="Add_ServiceType.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Service Setting</b>
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
   <asp:DetailsView ID="DetailsView_service" runat="server" AutoGenerateRows="False" 
                    DefaultMode="Edit" OnLoad="DetailsView_service_Load" CssClass="detailview_css"  
                    PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_service_DataBound">
           <PagerStyle CssClass="pgr" />
           <Fields>
            <asp:TemplateField HeaderText="Service Name" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' Width="50%" MaxLength="225" ></asp:TextBox>
                    <asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">                   
                <EditItemTemplate>
                  <CKEditor:CKEditorControl ID="desc" runat="server" Text='<%# Eval("Description") %>'  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                                                                      
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Cost" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="Cost" runat="server" Text='<%# Eval("Cost") %>' Width="10%" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="Type">          
                <EditItemTemplate>
                    <asp:DropDownList ID="type" runat="server" SelectedValue='<%# Eval("Type") %>'>
                        <asp:ListItem>Evaluation</asp:ListItem>
                        <asp:ListItem>Evaluation Multiplier</asp:ListItem>
                        <asp:ListItem>Additional</asp:ListItem>
                        <asp:ListItem>Additional Copy</asp:ListItem>
                        <asp:ListItem>Additional Multiplier</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                                <table width="100%"><tr><td style="font-weight:bold;padding-left: 0px;">Client:&nbsp;
<asp:Label ID="clientbottom" CssClass="SubclientEntry" runat="server" Text="Label"></asp:Label>  
</td><td width="100px">
                    <asp:Button ID="Update" runat="server" CausesValidation="false" CssClass="btncolor" CommandName="" OnClick="Update_Click"
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
