<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Client.aspx.cs" MasterPageFile="~/secure/ClientMaster.master" Inherits="secure_ClientAdmin_Client_Browse_Client" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Browse Client</span>
<div class="submenu_style">
  <div class="buttons">   
    <a href="Add_Client.aspx" class="regular">
        <img src="../../Code/icons/irc-join.ico" alt=""/> 
      <b>Add Client</b>
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
          	<div id="list" runat="server"></div>
<br />
<br />
</ContentTemplate>       
         </asp:UpdatePanel>       
</asp:Content>
