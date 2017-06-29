<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Employee.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Employee_Update_Employee" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Employee</span>
 <div class="submenu_style">   
  <div class="buttons">   
 <a href="Browse_Employee.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Employee</b>
    </a>  
    <a  href="Add_Employee.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Create Employee</b>
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
   <asp:DetailsView ID="DetailsView_employee" runat="server" AutoGenerateRows="False" DefaultMode="Edit" OnLoad="DetailsView_employee_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
<Fields>
          <asp:TemplateField  HeaderText="Client Name"> <EditItemTemplate>
<asp:DropDownList id="Clientdrp" runat="server" Enabled="false"  AppendDataBoundItems="True" 
                  onload="Clientdrp_Load"></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblerror" runat="server" ForeColor="Red"  Text=""></asp:Label> 
</EditItemTemplate>   
</asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name" SortExpression="Name">                               
                  <EditItemTemplate>
                    <asp:TextBox ID="Name" runat="server" Text='<%# Eval("Name") %>' ></asp:TextBox>                
             </EditItemTemplate>   
             </asp:TemplateField>   
            <asp:TemplateField HeaderText="Password" SortExpression="Password">                 
            <EditItemTemplate> 
                   <asp:TextBox ID="Password" runat="server" Text='<%# Eval("Password") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField ShowHeader="False">          
               <EditItemTemplate>              
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="false" CommandName="" OnClick="Update_Click"
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
