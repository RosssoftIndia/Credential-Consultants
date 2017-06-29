<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Gradescale.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Gradescale_Update_Gradescale" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Gradescale</span>
 <div class="submenu_style">  
  <div class="buttons">   
 <a href='Browse_Gradescale.aspx?search=<%=Request.QueryString["search"].ToString()%>&t1=<%=Request.QueryString["t1"].ToString()%>' class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Gradescales</b>
    </a>  
    <a  href="Add_Gradescale.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Gradescale</b>
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
   <asp:DetailsView ID="DetailsView_Gradescale" runat="server" AutoGenerateRows="False" 
                    DefaultMode="Edit" OnLoad="DetailsView_Gradescale_Load"  
                    CssClass="detailview_css"  PagerStyle-CssClass="pgr"   
                    AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_Gradescale_DataBound" >
        <Fields>
            <asp:TemplateField HeaderText="Name" >           
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' Width="50%" MaxLength="225"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Country">              
               <EditItemTemplate>
                    <asp:DropDownList ID="Countrydp" runat="server" OnLoad="Countrydp_Load">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Select Country" ControlToValidate="Countrydp" InitialValue="0"></asp:RequiredFieldValidator>
                     <asp:Label ID="temp" Visible="false"  runat="server" Text='<%# Eval("Country_ID") %>'></asp:Label>
               </EditItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Description">                 
                <EditItemTemplate>
                 <div class="descouterblk">
                  <div class="clientdescblk"><CKEditor:CKEditorControl ID="destxt"  Text='<%# Eval("Description") %>' runat="server"  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic" ></CKEditor:CKEditorControl></div> 
                  <div id="masterdescblk"  class="masterdescblk" style="display:none; overflow:hidden; height:150px;"><div id="masterdesc" runat="server" class="masterdescblkscroll"  ><%# Eval("masterdesc")%></div><br /><div style="text-align:right;">  <asp:Button ID="swap" runat="server" CssClass="btncolor" CausesValidation="false"  Text="Copy" OnClick="Swap_Click" /></div></div>
                  <div id="extratab" runat="server" class="extratab" onmousedown="toggleSlide('masterdescblk');">Master Description</div>   
                          </div> 
                </EditItemTemplate>
            </asp:TemplateField>               
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" OnClick="Update_Click"
                        Text="Update" Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>       
    </asp:DetailsView>
 <br />
 <br />
</ContentTemplate>       
</asp:UpdatePanel>      
</asp:Content>
