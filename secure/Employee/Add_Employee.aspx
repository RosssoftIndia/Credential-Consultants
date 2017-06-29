<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Employee.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Employee_Add_Employee" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Create Employee</span>
   <div class="submenu_style">            
            <div class="buttons">   
 <a href="Browse_Employee.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Employee</b>
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
   <asp:DetailsView ID="DetailsView_employee" runat="server"  AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
       <Fields>
        <asp:TemplateField  HeaderText="Client Name"><InsertItemTemplate>
<asp:DropDownList id="Clientdrp" runat="server" Enabled="false"  AppendDataBoundItems="True"></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblerror" runat="server" ForeColor="Red"  Text=""></asp:Label> 
</InsertItemTemplate>
</asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name" SortExpression="Name">                               
                <InsertItemTemplate>
                    <asp:TextBox ID="Name" runat="server" Text='<%# Eval("Name") %>' ></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Password" SortExpression="Password">                 
                <InsertItemTemplate>
                   <asp:TextBox ID="Password" runat="server" Text='<%# Eval("Password") %>' TextMode="Password" ></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField ShowHeader="False">          
                <InsertItemTemplate>
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
