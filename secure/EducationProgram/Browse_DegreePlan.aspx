<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_DegreePlan.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_EducationProgram_Browse_DegreePlan" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Education Programs</span>
   <div class="submenu_style">            
             <div class="buttons">   
    <a  href="Add_DegreePlan.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Education Program</b>
    </a>  
</div>       
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
    <br />
    <br />
            <div class="searchblk">
<table><tr><td><label for="searchtxt">Search Terms:</label><asp:TextBox ID="searchbox" runat="server" ></asp:TextBox><%--<sv:RequiredFieldValidator id="validator" runat="server" ErrorMessage="Enter value in the searchbox" ControlToValidate="searchbox">*</sv:RequiredFieldValidator>--%></td><td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>
<br />
    <div class="group">
        <ul>
            <li>
                <asp:RadioButtonList ID="searchoption1" runat="server" cellpadding="5" 
                    CellSpacing="5" Repeatdirection="Horizontal">
                    <asp:ListItem Selected="True">Name</asp:ListItem>
                    <asp:ListItem>Country</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:RadioButtonList ID="searchoption2" runat="server" cellpadding="5" 
                    CellSpacing="5" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Confirmed</asp:ListItem>
                    <asp:ListItem Value="0">UnConfirmed</asp:ListItem>
                    <asp:ListItem Value="2">Both</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
    </div>
</div>   
    <br />
    <br />
<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
         <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />           
        </Triggers>
            <ContentTemplate>   
               
<asp:GridView style="TEXT-ALIGN: center" id="grid_Degree" runat="server" OnPageIndexChanging="grid_Degree_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_Degree_Load" AutoGenerateColumns="False" ondatabound="grid_Degree_DataBound" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
<Columns>
<asp:TemplateField SortExpression="Name" HeaderText="Degree Plan"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%#"~/secure/EducationProgram/Update_DegreePlan.aspx?Degid=" + DataBinder.Eval(Container.DataItem,"Id") + "&role=" + DataBinder.Eval(Container.DataItem,"Category")  %>' CssClass="link"></asp:HyperLink>
<asp:Label ID="Role" Visible="false" runat="server" Text='<%#Eval("Category","{0}")%>'></asp:Label>
<asp:Label ID="lbldegreeId" Visible="false" runat="server" Text='<%#Eval("Id","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>       
    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
    <asp:TemplateField HeaderText="Confirmed" SortExpression="Confirmed">
        <ItemTemplate>
            <asp:Label ID="lblconfirmed" runat="server" Text='<%# Eval("Confirmed") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>   
    <asp:TemplateField HeaderText="Master Description" >
<ItemTemplate>
    <asp:Label ID="lbladmindes" runat="server" Text='<%#Eval("Admindesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Client Description" >
<ItemTemplate>
    <asp:Label ID="lblclientdes" runat="server" Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label>
     <asp:Label ID="lblCdes" runat="server" Visible="false"  Text='<%#Eval("Clientdesc","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Promote" >
<ItemTemplate>
<br />
<center>
<table>
<tr>
<td><asp:ImageButton ID="btndegree" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Degree.png" /></td>
</tr>
<tr>
<td><asp:ImageButton ID="btndescription" runat="server" onclick="promote" ImageUrl="~/secure/Code/button/Description.png" /></td>
</tr></table>
</center>
<br />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="IsEnabled" >
<ItemTemplate>
<center>
<asp:ImageButton ID="btn" runat="server" onclick="btn_Click" ImageUrl="~/secure/Code/button/Enable.png" />
    <asp:Label ID="IsEnable" runat="server" Visible="false"  Text='<%# Eval("IsEnabled") %>'></asp:Label>
</center>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:ImageButton ID="del" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/remove.png" OnClick="del_Click" Text="Delete" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>
 No Education Program Available                       
</EmptyDataTemplate>
</asp:GridView>
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>           
 </asp:Content>
