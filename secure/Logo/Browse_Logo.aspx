<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Logo.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Logo_Browse_Logo" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server"> 
  <span class="title" >Browse Logo</span>  
 <div class="submenu_style">            
<div class="buttons"> 
    <a href="Upload_Logo.aspx" class="regular">
       <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Upload Logo</b>
    </a>  
</div>      
		</div>
		<br />
		<br />			
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
 <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">   
<Triggers>
            <asp:AsyncPostBackTrigger  ControlID="drpclient" />                  
        </Triggers>        
            <ContentTemplate>                   
<div class="searchblk">
<table>
<tr>
<td>Client:</td>
<td>
 <asp:DropDownList ID="drpclient" runat="server" onload="drpclient_Load" 
            AppendDataBoundItems="True" AutoPostBack="True" 
            onselectedindexchanged="drpclient_SelectedIndexChanged">
        </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="drpclient" ErrorMessage="Select Client" InitialValue="0">*</asp:RequiredFieldValidator>
</td>
<td style="display:none;" >Sub Client</td>
    <td style="display:none;"> <asp:DropDownList ID="drpsubclient" runat="server"  AppendDataBoundItems="True">
        </asp:DropDownList></td>
<td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>      
</div> 
</ContentTemplate>
</asp:UpdatePanel>         
    <br />
    <br />
   
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">   
<Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />  
                
        </Triggers>        
            <ContentTemplate>       
<br />
<br />		

              <div id="holder" runat="server"  visible="false" style="width:100%;text-align: center;min-height:200px;border:solid;" >
           <div style="float:right;"><asp:ImageButton ID="del" runat="server"  ImageUrl="~/images/remove.png" Visible="false" OnClick="del_Click" Text="Delete" /></div>
     <asp:Image ID="logo" runat="server" />
    </div>
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>  
       
</asp:Content>
