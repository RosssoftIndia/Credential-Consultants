<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Client.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Admin_Client_Update_Client" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Update Client</span>
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
                    AutoGenerateRows="False" DefaultMode="Edit" 
                    OnLoad="DetailsView_Client_Load" ondatabound="DetailsView_Client_DataBound">
        <PagerStyle CssClass="pgr" />
        <Fields>
        <asp:TemplateField  HeaderText="Client Name"><EditItemTemplate>
        <asp:TextBox ID="txtName" Width="300px" MaxLength="200"  runat="server" Text='<%# Eval("Name") %>' ></asp:TextBox>
   </EditItemTemplate>   
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Address">                               
                  <EditItemTemplate>
                    <asp:TextBox ID="txtAddress"  Width="300px" MaxLength="50" runat="server" Text='<%# Eval("Address") %>' ></asp:TextBox>                
             </EditItemTemplate>   
             </asp:TemplateField>   
            <asp:TemplateField HeaderText="City">                 
            <EditItemTemplate> 
                   <asp:TextBox ID="txtCity" runat="server" MaxLength="50" Text='<%# Eval("City") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField> 
              <asp:TemplateField HeaderText="State">                 
            <EditItemTemplate> 
                   <asp:TextBox ID="txtState" runat="server" MaxLength="50" Text='<%# Eval("State") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField>  
               <asp:TemplateField HeaderText="Zipcode">                 
            <EditItemTemplate> 
                   <asp:TextBox ID="txtZipcode" runat="server" MaxLength="50" Text='<%# Eval("ZipCode") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField>     
             <asp:TemplateField HeaderText="DomainName">                 
            <EditItemTemplate> 
                   <asp:TextBox ID="txtDomainName" runat="server" Width="300px" MaxLength="100" Text='<%# Eval("SubDomainName") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField> 
                <asp:TemplateField HeaderText="Type">                 
            <EditItemTemplate>  
            <asp:DropDownList ID="dpclients" runat="server" onload="dpclients_Load" > </asp:DropDownList>              
                   <asp:TextBox ID="txtType" runat="server" Visible="false"  MaxLength="50"  Text='<%# Eval("Parent") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField ShowHeader="False">          
               <EditItemTemplate>              
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Update_Click"
                        Text="Update" Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>
        <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
 <br />
 <br />
</ContentTemplate>       
</asp:UpdatePanel>   
</asp:Content>
