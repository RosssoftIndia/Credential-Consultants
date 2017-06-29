<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Status.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Report_Status" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Internal FileNotes</span>
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
                  <table width="100%"><tr><td>
                  <div style="float:right;">
            <table>           
            <tr align="center" valign="middle" >           
<td ><asp:ImageButton ID="btnview" runat="server"  Width="32" Height="32" ImageUrl="~/secure/Code/icons/view.png"/> 
</td><td>View</td>
<td><asp:ImageButton ID="btnedit" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png"/>
</td><td>Edit</td>
<td><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png"/>
</td><td>Manage</td>
<td><asp:ImageButton ID="btneval" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/links.png" />
</td><td>Evaluate</td>
<td><asp:ImageButton ID="btnattach" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/pdf_icon.png" />
</td><td>Attachments</td>
</tr>
                          </table>
                          </div>
                          </td></tr></table>
                        <br />
                        <br />
              <div class="headertag" style="border:none"  >
            <br />
            <br />
        <table>
            <tr><td>FileNo</td><td>:</td><td><asp:Label ID="lblfileno" runat="server" ></asp:Label></td></tr>
            <tr><td>Name</td><td>:</td><td><asp:Label ID="lblname" runat="server"></asp:Label></td></tr>
            <tr><td>Client</td><td>:</td><td><asp:Label ID="lblcompany" runat="server"></asp:Label></td></tr>
            </table>    
            <br />
            <br />             
            </div>
            <br />
            <br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_Notes" runat="server" 
                    OnPageIndexChanging="grid_Notes_PageIndexChanging" PageSize="25" 
                    AllowPaging="True" OnLoad="grid_Notes_Load" AutoGenerateColumns="False" 
                    CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt" ondatabound="grid_Notes_DataBound" >
<EmptyDataTemplate>
No Internal FileNote Available                       
</EmptyDataTemplate>
<Columns>     
        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
        <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" SortExpression="Timestamp" />
        <asp:TemplateField HeaderText="Actions">
<ItemTemplate>
    <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>' Visible="false" ></asp:Label>
<asp:ImageButton ID="btndelete" runat="server"  ImageUrl="~/images/remove.png" OnClick="rec_del_Click" /></td>
</ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
