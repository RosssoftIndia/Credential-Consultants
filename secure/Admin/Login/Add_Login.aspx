<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Login.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Admin_Login_Add_Login" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Client-Login</span> 
  <div class="submenu_style">
  <div class="buttons">   
 <a href="Browse_Login.aspx" class="regular">
        <img src="../../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Client-Login</b>
    </a>
    <a href="Add_Login.aspx" class="regular">
        <img src="../../Code/icons/irc-join.ico" alt=""/> 
      <b>Add Client-Login</b>
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
            <asp:DetailsView id="DetailsView_employee" runat="server" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" DefaultMode="Insert" AutoGenerateRows="False">
<Fields>
<asp:TemplateField  HeaderText="Client Name"><InsertItemTemplate>
<asp:DropDownList id="Clientdrp" runat="server" AppendDataBoundItems="True"></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblerror" runat="server" ForeColor="Red"  Text=""></asp:Label> 
</InsertItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Login Name"><InsertItemTemplate>
                   <asp:TextBox ID="Name" runat="server" Text="" ></asp:TextBox>
                
</InsertItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Password" HeaderText="Password"><InsertItemTemplate>
                   <asp:TextBox ID="Password" runat="server" Text='<%# Eval("Password") %>' TextMode="Password" ></asp:TextBox>
                
</InsertItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False"><InsertItemTemplate>
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
