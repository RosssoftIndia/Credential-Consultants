<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Major.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Major_Add_Major" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Major</span>
   <div class="submenu_style">            
             <div class="buttons">   
 <a href="Browse_Major.aspx?search=&t1=0&t2=0" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Majors</b>
    </a>    
</div>  
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <br />
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>
<asp:DetailsView id="DetailsView_Major" runat="server" DefaultMode="Insert" AutoGenerateRows="False" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
        <Fields>
            <asp:TemplateField HeaderText="Major" SortExpression="Name">
            <InsertItemTemplate >                
                    <asp:TextBox ID="name" runat="server" Width="50%" MaxLength="225" ></asp:TextBox>
                </InsertItemTemplate >
            </asp:TemplateField>
          <asp:TemplateField HeaderText="Country" SortExpression="Country">                 
                <InsertItemTemplate >
                    <asp:DropDownList ID="Countrydp" runat="server">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Country" ControlToValidate="Countrydp" InitialValue="0"></asp:RequiredFieldValidator>                    
                </InsertItemTemplate >
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Confirmed" SortExpression="Confirmed">           
                <InsertItemTemplate >
                    <asp:DropDownList ID="confirmed" runat="server" >
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
               </InsertItemTemplate >               
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Us Equivalency" SortExpression="EquiMajor_id">              
                <InsertItemTemplate>
                    <asp:DropDownList ID="equivalency" runat="server">                        
                    </asp:DropDownList>
                </InsertItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">              
                 <InsertItemTemplate>
                 <CKEditor:CKEditorControl ID="destxt" runat="server"   Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl>                    
               </InsertItemTemplate>    
            </asp:TemplateField>        
              <asp:TemplateField ShowHeader="False">          
                <InsertItemTemplate >
                    <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" />
                </InsertItemTemplate >
            </asp:TemplateField>
        </Fields>     
    </asp:DetailsView>
</ContentTemplate>       
</asp:UpdatePanel> 
<br />
<br />
</asp:Content>
