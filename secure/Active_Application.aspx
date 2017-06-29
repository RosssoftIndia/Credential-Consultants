<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Active_Application.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Active_Application" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Active Applications</span>
   <div class="submenu_style" >     
             <div class="buttons">    
                
		</div>
		</div>
		<br />
		<br />       
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server">            
            </asp:ScriptManager>
                  <br />
                  <br />
                  <table width="100%"><tr><td>
                  <div style="float:right;">
            <table>           
            <tr align="center" valign="middle" >           
<td ><asp:Image ID="img1" runat="server"  Width="32" Height="32" ImageUrl="~/secure/Code/icons/view.png"/>
</td><td>View</td>
<td><asp:Image ID="img2" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png" />
</td><td>Edit</td>
<td><asp:Image ID="img3" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png" />
</td><td>Manage</td>
<td><asp:Image ID="img4" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/delete.png"/>
</td><td>Delete</td>
<td><asp:Image ID="img5" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png"/>
</td><td>Notes</td>
<td><asp:Image ID="img6" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/links.png"/>
</td><td>Evaluate</td>
<td><asp:Image ID="img7" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/pdf_icon.png"/>
</td><td>Attachments</td>
</tr>
                          </table>
                          </div>
                          </td></tr></table>
            
                    <div class="submenu_style" >     
     <ul id="submenu" style="margin-top:20px;">
               <li class="single"><asp:RadioButtonList  ID="searchoption" runat="server" 
                       Repeatdirection="Horizontal"  cellpadding="5" CellSpacing="5" 
                       AutoPostBack="True" CausesValidation="True">
        <asp:ListItem>ALL</asp:ListItem>
        <asp:ListItem>Step 1</asp:ListItem>
        <asp:ListItem>Step 2</asp:ListItem>
        <asp:ListItem>Step 3</asp:ListItem>
        <asp:ListItem>Step 4</asp:ListItem>      
    </asp:RadioButtonList>  </li>	          	
		</ul>   
                
		</div>
           <div class="searchblk">
<table>
<tr>
<td>
<label for="searchtxt">Search Terms:</label>
</td>
</tr>
<tr>
<td>
 <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional"   runat="server">  
 <Triggers>
 <asp:AsyncPostBackTrigger ControlID="searchoption1"  />
 <asp:AsyncPostBackTrigger ControlID="dpemployee" />
 </Triggers>
<ContentTemplate>   
<asp:TextBox ID="searchbox" runat="server" ></asp:TextBox>
</ContentTemplate>
</asp:UpdatePanel>
</td>
<td><asp:DropDownList ID="dpsubclients" runat="server" AppendDataBoundItems="true">
    </asp:DropDownList>
</td>
<td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>
<br />
    <div class="group">
       <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">  
            <ContentTemplate>   
        <ul>
            <li>
             <asp:RadioButtonList ID="searchoption1" runat="server" AutoPostBack="true" 
        Repeatdirection="Horizontal" Width="100%" cellpadding="0" CellSpacing="0" 
                    RepeatColumns="2" 
                    onselectedindexchanged="searchoption1_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="Name">Name</asp:ListItem>
        <asp:ListItem Value="DOB">DOB (yyyy-mm-dd)</asp:ListItem>
        <asp:ListItem>Country (Country Of Birth)</asp:ListItem>
        <asp:ListItem>Email</asp:ListItem>
        <asp:ListItem>Filenumber</asp:ListItem>
        <asp:ListItem>Highschool</asp:ListItem>
        <asp:ListItem>Employee</asp:ListItem>
        <asp:ListItem>University</asp:ListItem>
    </asp:RadioButtonList>
     </li>
     <li style="padding-left:20px;">
         <asp:DropDownList ID="dpemployee" runat="server" Visible="false"  AutoPostBack="true"
             onselectedindexchanged="dpemployee_SelectedIndexChanged">       
         </asp:DropDownList>
     </li>
        </ul>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</div>
 <br />
 <br />      

        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
         <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />   
            <asp:AsyncPostBackTrigger ControlID="searchoption" />        
        </Triggers>
            <ContentTemplate>                 
                        <br />
                        <br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_activeapplication" runat="server" OnPageIndexChanging="grid_activeapplication_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_activeapplication_Load"  CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  AutoGenerateColumns="False" ondatabound="grid_activeapplication_DataBound">
<Columns>
<asp:BoundField DataField="Createdon" SortExpression="Createdon" HeaderText="Date"></asp:BoundField>
<asp:TemplateField HeaderText="Client"><ItemTemplate>
<asp:Label id="lblclient" runat="server" ></asp:Label> 
</ItemTemplate>
<ItemStyle Width="100px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="FileNumber"><ItemTemplate>
<asp:Label id="lblfileno" runat="server" Text='<%# Bind("FileNumber") %>'></asp:Label> 
<asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Name" HeaderText="Name"><ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label> 
</ItemTemplate>
</asp:TemplateField>

<asp:BoundField DataField="Evaluation_Name" SortExpression="Evaluation_Name" HeaderText="Purpose"></asp:BoundField>
<asp:BoundField DataField="InternalFileNumber" SortExpression="InternalFileNumber" HeaderText="InternalFileNumber"></asp:BoundField>
<asp:BoundField DataField="Amount"  HeaderText="Amount ($)"></asp:BoundField>
<asp:TemplateField HeaderText="Actions"><ItemTemplate>
<center>
<br />
<table><tr>
<td  style="border:none;"><asp:ImageButton ID="btnview" runat="server"  Width="32" Height="32" ImageUrl="~/secure/Code/icons/view.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/View_Application.aspx?tc={0}") %>'  /></td>
<td  style="border:none;"><asp:ImageButton ID="btnedit" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/Edit_Application.aspx?tc={0}") %>'  /></td>
<td  style="border:none;"><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/Edit_Application_Status.aspx?tc={0}") %>'  /></td>
<td  style="border:none;"><asp:ImageButton ID="btndelete" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/delete.png" OnClick="application_del_Click" /></td>
<td  style="border:none;"><asp:ImageButton ID="btnreport" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/Report_Status.aspx?tc={0}") %>'  /></td>
<td  style="border:none;"><asp:ImageButton ID="btneval" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/links.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/Evaluate.aspx?tc={0}") %>'  /></td>
<td  style="border:none;"><asp:ImageButton ID="btnpdf" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/pdf_icon.png" PostBackUrl='<%# Eval("FileNumber", "~/secure/Attachments.aspx?tc={0}") %>' /></td>
</tr></table>
<br />
</center>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>
No Active Application Available                       
</EmptyDataTemplate>
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>    
</asp:Content>
