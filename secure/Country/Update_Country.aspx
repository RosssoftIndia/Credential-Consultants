<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Country.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Country_Update_Country" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Country</span>
 <div class="submenu_style">  
           <div class="buttons">   
 <a href='Browse_Country.aspx?search=<%=Request.QueryString["search"].ToString()%>' class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Countries</b>
    </a>  
     <a id="opt" runat="server" visible="false"  href="Add_Country.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Country</b>
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
   <asp:DetailsView ID="DetailsView_Country" runat="server" AutoGenerateRows="False" DefaultMode="Edit" OnLoad="DetailsView_Country_Load" ondatabound="DetailsView_Country_DataBound" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
        <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:TemplateField HeaderText="Country" SortExpression="Name">              
               <EditItemTemplate>
                    <asp:TextBox ID="Countrytxt" runat="server" Text='<%#Eval("Name","{0}")%>' Width="50%" MaxLength="225"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">              
                  <EditItemTemplate>
                  <div class="descouterblk">
                  <div class="clientdescblk"> <CKEditor:CKEditorControl ID="destxt" runat="server" Text='<%# Eval("Description") %>'  Width="100%" Height="300px" BasePath="~/Code/fckeditor/"  Toolbar="Basic"></CKEditor:CKEditorControl></div>
                  <div id="masterdescblk"  class="masterdescblk" style="display:none; overflow:hidden;  height:150px;"><div id="masterdesc" runat="server" class="masterdescblkscroll"   style="width:100%;"><%# Eval("masterdesc")%></div><br /><div style="width:100%;text-align:right;">  <asp:Button ID="swap" runat="server" CssClass="btncolor" CausesValidation="false"  Text="Copy" OnClick="Swap_Click" /></div></div>
                  <div id="extratab" runat="server" class="extratab" onmousedown="toggleSlide('masterdescblk');">Master Description</div>   
                          </div>              
                 </EditItemTemplate>           
            </asp:TemplateField>            
            <asp:TemplateField ShowHeader="False">
                <ControlStyle Width="100px" />              
                <EditItemTemplate>
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" Text="Update" OnClick="Update_Click" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>    
        <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
    <asp:HiddenField ID="searchtext"  Visible="true"   runat="server" />
   <br />
   <br />
</ContentTemplate>       
         </asp:UpdatePanel> 
</asp:Content>
