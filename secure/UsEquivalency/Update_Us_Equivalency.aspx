<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Us_Equivalency.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_UsEquivalency_Update_Us_Equivalency" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Equivalency</span>
   <div class="submenu_style">  
     <div class="buttons">   
 <a href='Browse_Us_Equivalency.aspx?search=<%=Request.QueryString["search"].ToString()%>' class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Equivalencies</b>
    </a>  
    <a href="Add_Us_Equivalency.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Equivalency</b>
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
   <asp:DetailsView ID="DetailsView_Equivalency" runat="server" AutoGenerateRows="False" DefaultMode="Edit" OnLoad="DetailsView_Equivalency_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">       
        <Fields>
            <asp:TemplateField HeaderText="Name" >           
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' Width="50%" MaxLength="225"  ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Description">                 
                <EditItemTemplate>
               <CKEditor:CKEditorControl ID="destxt"  Text='<%# Eval("Description") %>' runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                                 
                </EditItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Update_Click"
                        Text="Update" Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>      
    </asp:DetailsView>  
</td> 
</tr> 
</table> 
</ContentTemplate>       
</asp:UpdatePanel>        
</asp:Content>
