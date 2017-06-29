<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Institution.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Institution_Update_Institution" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Institution</span>
   <div class="submenu_style"> 
    <div class="buttons">   
 <a href='Browse_Institution.aspx?search=<%=Request.QueryString["search"].ToString()%>&t1=<%=Request.QueryString["t1"].ToString()%>&t2=<%=Request.QueryString["t2"].ToString()%>&t3=<%=Request.QueryString["t3"].ToString()%>&t4=<%=Request.QueryString["t4"].ToString()%>' class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Institutions</b>
    </a>  
    <a  href="Add_Institution.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Institution</b>
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
   <asp:DetailsView ID="DetailsView_institution" runat="server" AutoGenerateRows="False" DefaultMode="Edit" OnLoad="DetailsView_institution_Load" ondatabound="DetailsView_institution_DataBound" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
       <Fields>
            <asp:TemplateField HeaderText="Institution Name" SortExpression="Name">                   
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' Width="50%" MaxLength="225" ></asp:TextBox>
                    <asp:Label ID="hidenctrl" runat="server" Text='<%# Eval("Customer_Id") %>' Visible="false"></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country" SortExpression="Country">                 
                <EditItemTemplate>
                    <asp:DropDownList ID="Countrydp" runat="server" OnLoad="Countrydp_Load">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Select Country" ControlToValidate="Countrydp" InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:Label ID="temp" Visible="false"  runat="server" Text='<%# Eval("Country_ID") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="Type">          
                <EditItemTemplate>
                    <asp:DropDownList ID="type" runat="server" SelectedValue='<%# Eval("Type") %>'>
                        <asp:ListItem>HighSchool</asp:ListItem>
                        <asp:ListItem>University</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Confirmed" SortExpression="Confirmed">           
                <EditItemTemplate>
                    <asp:DropDownList ID="confirmed" runat="server" SelectedValue='<%# Eval("Confirmed") %>'>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Degree Mill" >           
                <EditItemTemplate>
                    <asp:DropDownList ID="degmill" runat="server" SelectedValue='<%# Eval("IsDegreeMill") %>'>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">              
                  <EditItemTemplate>
                     <div class="descouterblk">
                  <div class="clientdescblk"><CKEditor:CKEditorControl ID="destxt" runat="server" Text='<%# Eval("Description") %>'  Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl></div> 
                   <div id="masterdescblk"  class="masterdescblk" style="display:none; overflow:hidden;  height:150px;"><div id="masterdesc" runat="server" class="masterdescblkscroll"   style="width:100%;"><%# Eval("masterdesc")%></div><br /><div style="width:100%;text-align:right;">  <asp:Button ID="swap" runat="server" CssClass="btncolor" CausesValidation="false"  Text="Copy" OnClick="Swap_Click" /></div></div>
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
