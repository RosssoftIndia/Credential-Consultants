<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true"  CodeFile="Upload_Logo.aspx.cs" MasterPageFile="~/secure/Master.master"   Inherits="secure_Logo_Upload_Logo" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
    <span class="title" >Upload Logo</span> 
  <div class="submenu_style">            
 <div class="buttons">              
  <a href="Browse_Logo.aspx" class="regular">
       <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Logo</b>
    </a> 
</div>         
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">	

              <br />
            <br /> 
<table class="search_css">
    <tr>
    <td>Client</td>
    <td>
        <asp:DropDownList ID="drpclient" runat="server" onload="drpclient_Load" 
            AppendDataBoundItems="True" AutoPostBack="True" 
            onselectedindexchanged="drpclient_SelectedIndexChanged">
        </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="drpclient" ErrorMessage="Select Client" InitialValue="0">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr style="display:none;">
    <td>Sub Client</td>
    <td> <asp:DropDownList ID="drpsubclient" runat="server"  AppendDataBoundItems="True">
        </asp:DropDownList></td>
    </tr>
<tr><td>Upload .png File</td><td>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="FileUpload1" ErrorMessage="png File Required">*</asp:RequiredFieldValidator>
    </td></tr>
<tr><td colspan="2" > <asp:Button ID="btn" runat="server" Text="Upload" 
        CssClass="btncolor" onclick="btn_Click" />
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label ID="lblresult" runat="server" ForeColor="#FF3300"></asp:Label></td></tr>
</table>                          
<br />
<br />        
</asp:Content>
                                                                                               



