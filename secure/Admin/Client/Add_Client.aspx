<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Client.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Admin_Client_Add_Client" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Add Client</span> 
  <div class="submenu_style">
  <div class="buttons">   
 <a href="Browse_Client.aspx" class="regular">
        <img src="../../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Client</b>
    </a>
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
             <asp:DetailsView ID="DetailsView_Client" runat="server" CssClass="detailview_css" 
                    PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" 
                    AutoGenerateRows="False" DefaultMode="Insert" >
        <PagerStyle CssClass="pgr" />
        <Fields>
        <asp:TemplateField  HeaderText="Client Name"><InsertItemTemplate>
        <asp:TextBox ID="txtName" Width="300px" MaxLength="200"  runat="server"  ></asp:TextBox>
        <sv:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
        ErrorMessage="Client Name Required" >*</sv:RequiredFieldValidator>
   </InsertItemTemplate>   
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Address">                               
                  <InsertItemTemplate>
                    <asp:TextBox ID="txtAddress"  Width="300px" MaxLength="50" runat="server" ></asp:TextBox>                
             </InsertItemTemplate>   
             </asp:TemplateField>   
            <asp:TemplateField HeaderText="City">                 
            <InsertItemTemplate> 
                   <asp:TextBox ID="txtCity" runat="server" MaxLength="50" ></asp:TextBox>
                 </InsertItemTemplate>
            </asp:TemplateField> 
              <asp:TemplateField HeaderText="State">                 
            <InsertItemTemplate> 
                   <asp:TextBox ID="txtState" runat="server" MaxLength="50" ></asp:TextBox>
                 </InsertItemTemplate>
            </asp:TemplateField>  
               <asp:TemplateField HeaderText="Zipcode">                 
            <InsertItemTemplate> 
                   <asp:TextBox ID="txtZipcode" runat="server" MaxLength="50" ></asp:TextBox>
                 </InsertItemTemplate>
            </asp:TemplateField>     
             <asp:TemplateField HeaderText="DomainName">                 
            <InsertItemTemplate> 
                   <asp:TextBox ID="txtDomainName" runat="server" Width="300px" MaxLength="100" ></asp:TextBox>
                    <sv:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDomainName"
        ErrorMessage="Domain Name Required" >*</sv:RequiredFieldValidator>
                 </InsertItemTemplate>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="Type">                 
            <InsertItemTemplate>                 
                <asp:DropDownList ID="dpclients" runat="server">
                </asp:DropDownList>            
                  </InsertItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField ShowHeader="False">          
               <InsertItemTemplate>              
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />              
                   <asp:Button ID="Add" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" />
                </InsertItemTemplate>
            </asp:TemplateField>
        </Fields>
        <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
<br />
<br />
</ContentTemplate>       
         </asp:UpdatePanel>         
</asp:Content>
