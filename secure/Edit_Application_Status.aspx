<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Application_Status.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Edit_Application_Status" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
    <span class="title" >Manage Application</span>
<br />
<br />       
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<script language="javascript" type="text/javascript">
    function call_myservice(input) {
      RecomEngine.Generate_Recommendation(input,OnSuccess)        
    }
    function OnSuccess(msg) {
//        alert(msg);
    }
</script> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="false">
    <Services>
    <asp:ServiceReference  Path="~/RecomEngine.asmx" />
</Services> 
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
<td><asp:ImageButton ID="btnreport" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png" />
</td><td>Notes</td>
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
<div class="headertag" >Application Status</div>
<br />
<asp:DetailsView id="DetailsView_ApplEdit" runat="server" OnLoad="DetailsView_ApplEdit_Load"  DefaultMode="Edit" AutoGenerateRows="False" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
                       <PagerStyle CssClass="pgr" />
                       <Fields>
                           <asp:TemplateField HeaderText="Application Received" SortExpression="Application_Recieved">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk1" runat="server" SelectedValue='<%# Bind("Application_Recieved") %>'>
                                       <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                               <ItemTemplate>
                                   &nbsp;
                               </ItemTemplate>
                               <HeaderStyle Width="300px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Documents Received" SortExpression="Documents_Recieved">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk2" runat="server" SelectedValue='<%# Bind("Documents_Recieved") %>'>
                                       <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Payment Received" SortExpression="Payment_Recieved">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk3" runat="server" SelectedValue='<%# Bind("Payment_Recieved") %>'>
                                       <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Service in Process" SortExpression="Evaluation_Complete">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk4" runat="server" SelectedValue='<%# Bind("Evaluation_Complete") %>'>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Initial Report Prepared" SortExpression="Verification_Complete">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk5" runat="server" SelectedValue='<%# Bind("Verification_Complete") %>'>
                                         <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Report Approved" SortExpression="Evaluation_Approved">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk6" runat="server" SelectedValue='<%# Bind("Evaluation_Approved") %>'>
                                       <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Final Report Prepared, Service Complete" SortExpression="Packaging_Complete">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk7" runat="server" SelectedValue='<%# Bind("Packaging_Complete") %>'>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Report Sent Out" SortExpression="Delievery_Complete">
                               <EditItemTemplate>
                                   <asp:DropDownList ID="chk8" runat="server" SelectedValue='<%# Bind("Delievery_Complete") %>'>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                       <asp:ListItem Value="0">No</asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="False">
                               <ItemTemplate>  
                                <table width="100%" ><tr><td><table>
                                <tr><td>Updated By:</td><td><asp:Label ID="lblby" runat="server" Text='<%# Bind("UpdatedBy") %>'></asp:Label></td></tr>
                                <tr><td>Last Update:</td><td><asp:Label ID="lblTimestamp" runat="server" Text='<%# Bind("Timestamp") %>'></asp:Label></td></tr>                              
                                </table>
                                   </td><td style="width:150px">                             
                                   <asp:Button ID="Update" runat="server" CssClass="btncolor" Width="100px" CausesValidation="false" CommandName="" Text="Update" OnClick="Update_Click" />
                                   </td></tr></table>  
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Fields>                      
                       <AlternatingRowStyle CssClass="alt" />
                   </asp:DetailsView>
 <br />
 <br />
<div class="headertag" >Internal Notes</div> 
<br />
<asp:DetailsView ID="DetailsView_status" runat="server" AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
        <Fields>
            <asp:TemplateField HeaderText="Internal Note">
                <InsertItemTemplate>
                <table width="100%" ><tr><td> <asp:TextBox ID="internalNote" runat="server"  Height="73px" TextMode="MultiLine" Width="100%"></asp:TextBox></td><td style="width:150px"> <asp:Button ID="internal_Add" runat="server" CausesValidation="false" CssClass="btncolor" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" /></td></tr></table>                   
                </InsertItemTemplate>
            </asp:TemplateField>               
              </Fields>        
    </asp:DetailsView>
    <br />
<asp:GridView  id="Grid_internalNotes" runat="server" CssClass="gridview_css" 
                     AutoGenerateColumns="false"  PagerStyle-CssClass="pgr" 
                     AlternatingRowStyle-CssClass="alt" ondatabound="Grid_internalNotes_DataBound" >
 <Columns>     
  <asp:TemplateField HeaderText="Notes" ShowHeader="true">
   <ItemTemplate>
  <asp:Label ID="note" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
  </ItemTemplate> 
  </asp:TemplateField>        
        <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" ItemStyle-Width="160px"  />
      <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                     <ItemStyle Width="20px" />
                                     <ItemTemplate>
                                         <asp:Label ID="msgid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                         <asp:ImageButton ID="internal_del" runat="server" CausesValidation="False" 
                                             CommandName="Delete" ImageUrl="~/images/remove.png" 
                                             OnClick="internal_del_Click" Text="Delete" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
No Internal Note Available                       
</EmptyDataTemplate>
</asp:GridView> 
  <br />
  <div class="headertag" >Applicant Notes</div> 
<br />    
  <asp:GridView id="Grid_applicantNotes" AutoGenerateColumns="false"  runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
 <Columns> 
  <asp:BoundField DataField="Notes" HeaderText="Applicant Note"  />
    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" ItemStyle-Width="200px" />
 </Columns>
<EmptyDataTemplate>
No Applicant Note Available                       
</EmptyDataTemplate>
</asp:GridView>  
 <br />
  <div class="headertag" >Status Notes</div> 
<br />   
<asp:DetailsView ID="DetailsView_applicant" runat="server" AutoGenerateRows="False" DefaultMode="Insert" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
  <Fields> 
            <asp:TemplateField  HeaderText="Status Note">
                <InsertItemTemplate>
                  <table width="100%" ><tr><td> <asp:TextBox ID="applicantNote" runat="server"  Height="73px" TextMode="MultiLine" Width="100%"></asp:TextBox></td><td style="width:150px" > <asp:Button ID="applicant_Add" runat="server" CausesValidation="false" CssClass="btncolor" CommandName="" OnClick="Add_Click"
                        Text="Add" Width="100px" /></td></tr></table>     
                </InsertItemTemplate>
            </asp:TemplateField>        
        </Fields>        
    </asp:DetailsView>
<br />
 <asp:GridView id="Grid_status" AutoGenerateColumns="false"  runat="server" 
                     CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                     AlternatingRowStyle-CssClass="alt" ondatabound="Grid_status_DataBound" >
 <Columns> 
    <asp:TemplateField HeaderText="Notes" ShowHeader="true">
   <ItemTemplate>
  <asp:Label ID="note" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
  </ItemTemplate> 
  </asp:TemplateField>  
    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" ItemStyle-Width="160px"  />
      <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                     <ItemStyle Width="20px" />
                                     <ItemTemplate>
                                         <asp:Label ID="msgid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                         <asp:ImageButton ID="status_del" runat="server" CausesValidation="False" 
                                             CommandName="Delete" ImageUrl="~/images/remove.png" 
                                             OnClick="status_del_Click" Text="Delete" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
 </Columns>
<EmptyDataTemplate>
No Status Note Available                       
</EmptyDataTemplate>
</asp:GridView>  
<br />
<div class="headertag" >Internal File Number</div>  
<br />     			
 <asp:DetailsView ID="DetailsView_service" runat="server" AutoGenerateRows="False" DefaultMode="Edit" OnLoad="DetailsView_service_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
  <Fields>
        <asp:TemplateField HeaderText="InternalFileNumber" SortExpression="InternalFileNumber">                   
                <EditItemTemplate>
                 <asp:TextBox ID="file" runat="server" Text='<%# Eval("FileNumber") %>' Visible="false"></asp:TextBox>
                 <table width="100%" ><tr><td>
                    <asp:TextBox ID="internal" runat="server" Text='<%# Eval("InternalFileNumber") %>'  ></asp:TextBox>                 
                    </td><td style="width:150px" > 
                    <asp:Button ID="Update" runat="server" CausesValidation="false" CssClass="btncolor" CommandName="" OnClick="Update1_Click" Text="Update" Width="100px" />
                    </td></tr> 
                    </table> 
                </EditItemTemplate>
            </asp:TemplateField>               
           
        </Fields>       
</asp:DetailsView>
<br />
<br />                
    <div class="headertag" >Link Employee</div>
    <br />
    <table class="edit_css">
    <tr>
    <td style="width:300px;padding: 4px 0 4px 10px!important;">Amount</td>
    <td style="padding: 4px 0 4px 10px!important;">
        <asp:TextBox ID="txtamount" Width="80px" runat="server"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
            ID="Requiredamount" runat="server" ErrorMessage="*" ValidationGroup="emp" 
            ControlToValidate="txtamount"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td style="padding: 4px 0 4px 10px!important;">Employee</td>
    <td style="padding: 4px 0 4px 10px!important;">
        <asp:DropDownList ID="dpemployee" runat="server">
        </asp:DropDownList>&nbsp;
        <asp:RequiredFieldValidator
            ID="Requireddp" runat="server" ErrorMessage="*" ValidationGroup="emp"
            ControlToValidate="dpemployee" InitialValue="0"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr> 
    <td colspan="2">
    <table width="100%">
    <tr>
    <td></td>
    <td width="150px">
     <asp:Button ID="Update" runat="server" CssClass="btncolor" Width="100px" ValidationGroup="emp"
            CommandName="" Text="Update" onclick="Update_Employeeinfo"  />    
    </td>
    </tr>
    </table>
                                
    </td> 
    </tr>
    </table>
<br />
<br />                
<br />    
</ContentTemplate>       
</asp:UpdatePanel>    
</asp:Content>
