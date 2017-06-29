<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachments.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Attachments" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Attachments</span>  
 <div class="submenu_style">            
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
<td ><asp:ImageButton ID="btnview" runat="server"  Width="32" Height="32" ImageUrl="~/secure/Code/icons/view.png"/> 
</td><td>View</td>
<td><asp:ImageButton ID="btnedit" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png"/>
</td><td>Edit</td>
<td><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png"/>
</td><td>Manage</td>
<td><asp:ImageButton ID="btnreport" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png" />
</td><td>Notes</td>
<td><asp:ImageButton ID="btneval" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/links.png" />
</td><td>Evaluate</td>
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
<div class="headertag" >Applicant Documents</div>
<br />
 <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">   
          <Triggers>
<asp:PostBackTrigger ControlID="docupload" /> 
 </Triggers>   
            <ContentTemplate>
   <table class="tbcontent">
   <tr  id="row" runat="server" visible="false"><td>File Name</td><td>
       <asp:TextBox ID="txtdocname" runat="server"></asp:TextBox>
       <%--<asp:RequiredFieldValidator ID="Validator1" runat="server" ControlToValidate="txtdocname"  ErrorMessage="File Name Required!" ValidationGroup="doc"></asp:RequiredFieldValidator>--%></td></tr>
    <tr><td>Upload File</td><td><asp:FileUpload ID="doc_uploader" runat="server" />
        <asp:Button ID="docupload" runat="server" Text="Upload" CausesValidation="true" 
            ValidationGroup="doc"  onclick="doc_upload_Click" />           
            </td></tr>
        <tr>
        <td colspan="2" >
    <asp:Label ID="docmsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label> 
    <%-- <asp:RegularExpressionValidator  ValidationExpression=".*\.(pdf|PDF)$" ControlToValidate="doc_uploader"  ID="RegularExpressionValidator2" runat="server"  Text="You can only specify a pdf file"></asp:RegularExpressionValidator>        --%>
</td>
        </tr>                 
    </table> 
 </ContentTemplate>       
 </asp:UpdatePanel>
                              
     <asp:GridView ID="Doc_grid" runat="server" AllowPaging="True" Width="100%"
                 AutoGenerateColumns="False" CssClass="gridview_css" 
        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   PageSize="10" 
        ondatabound="Doc_grid_DataBound" 
        onpageindexchanging="Doc_grid_PageIndexChanging">               
                    <PagerSettings Position="TopAndBottom"  />    
                    <PagerStyle HorizontalAlign="Right" />              
                      <Columns>
                                <asp:TemplateField HeaderText="Document">
                        <ItemTemplate>                           
                                <asp:Label ID="lblfilename" runat="server" Text='<%#Eval("File") %>'></asp:Label>
                                  <asp:Label ID="lblpath" runat="server" Text='<%#Eval("url") %>' Visible="false" ></asp:Label>
                        </ItemTemplate> 
                        </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Actions">                                      
                        <ItemTemplate> 
                            <table>           
            <tr align="center" valign="middle" >           
<td  style="border:none;">
  
                     <asp:ImageButton ID="btndownload"  width="32" Height="32"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 
      
                        </td> 
                        <td  style="border:none;">
                         <asp:ImageButton ID="doc_delete" ImageUrl="~/images/remove.png" runat="server" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this file-" + ((string)Eval("File"))+ " ?\");"%>' onclick="doc_delete_Click"></asp:ImageButton>                                 
                         </td>
                         </tr> 
                         </table> 
                         </ItemTemplate> 
                         <ItemStyle Width="50px"  /> 
                         </asp:TemplateField>                                                                                 
                     </Columns>              
                       <EmptyDataTemplate>
                          <center><span style="color:Red;"> No Documents Available</span></center>                      
                       </EmptyDataTemplate>   
                       <RowStyle VerticalAlign="Middle" />             
            </asp:GridView>
    
<br />
<div class="headertag" >Evaluation/Translation Reports</div>
<br />  
 <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">   
          <Triggers>
<asp:PostBackTrigger ControlID="reportupload" /> 
 </Triggers>   
            <ContentTemplate>      
 <table class="tbcontent">
   <tr id="row1" runat="server" visible="false"><td>File Name</td><td>
       <asp:TextBox ID="txtreportname" runat="server"></asp:TextBox>
       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ControlToValidate="txtreportname"  ErrorMessage="File Name Required!" 
           ValidationGroup="report"></asp:RequiredFieldValidator>--%></td></tr>
    <tr><td>Upload File</td><td><asp:FileUpload ID="report_uploader" runat="server" />
        <asp:Button ID="reportupload" runat="server" Text="Upload" 
            CausesValidation="true" ValidationGroup="report"  
            onclick="report_upload_Click" /></td></tr>
        <tr>
        <td colspan="2" >
        <%--<div width="100%" >
    Server Trace:
    <br />--%>
    <asp:Label ID="reportmsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>     
   <%-- <asp:RegularExpressionValidator  ValidationExpression=".*\.(pdf|PDF)$"
                    ControlToValidate="report_uploader"  ID="RegularExpressionValidator1" runat="server"
                    Text="You can only specify a pdf file"></asp:RegularExpressionValidator>     --%>    
<%--</div>--%>
        
        </td>
        </tr>               
    </table>
   </ContentTemplate>       
 </asp:UpdatePanel>
    <asp:GridView ID="report_grid" runat="server" AllowPaging="True" Width="100%" 
                 AutoGenerateColumns="False" CssClass="gridview_css" 
        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   PageSize="10" 
        onpageindexchanging="report_grid_PageIndexChanging" 
        ondatabound="report_grid_DataBound">               
                    <PagerSettings Position="TopAndBottom"  />    
                    <PagerStyle HorizontalAlign="Right" />              
                      <Columns>
                                <asp:TemplateField HeaderText="Document">
                        <ItemTemplate>                           
                                <asp:Label ID="lblfilename" runat="server" Text='<%#Eval("File") %>'></asp:Label>
                                  <asp:Label ID="lblpath" runat="server" Text='<%#Eval("url") %>' Visible="false" ></asp:Label>
                        </ItemTemplate> 
                        </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Actions">                                      
                        <ItemTemplate> 
                            <table>           
            <tr align="center" valign="middle" >           
<td  style="border:none;">
  
               <asp:ImageButton ID="btndownload" width="32" Height="32"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 
      
                        </td> 
                        <td  style="border:none;">
                         <asp:ImageButton ID="report_delete" ImageUrl="~/images/remove.png"  runat="server" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this file-" + ((string)Eval("File"))+ " ?\");"%>'   onclick="report_delete_Click"></asp:ImageButton>                                 
                         </td>
                         </tr> 
                         </table> 
                         </ItemTemplate> 
                         <ItemStyle Width="50px"  /> 
                         </asp:TemplateField>                                                                                 
                     </Columns>              
                       <EmptyDataTemplate>
                          <center><span style="color:Red;"> No Documents Available</span></center>                      
                       </EmptyDataTemplate>               
            </asp:GridView>
<div class="headertag" >Other Attachments</div>
<br />
<asp:UpdatePanel ID="OtherPanel" UpdateMode="Conditional"   runat="server">   
          <Triggers>
<asp:PostBackTrigger ControlID="otherupload" /> 
 </Triggers>   
            <ContentTemplate>
   <table class="tbcontent">
     <tr  id="Tr1" runat="server" visible="false"><td>File Name</td><td>
       <asp:TextBox ID="txtothername" runat="server"></asp:TextBox>
       </td></tr>   
    <tr><td>Upload File</td><td><asp:FileUpload ID="Other_uploader" runat="server" />
        <asp:Button ID="otherupload" runat="server" Text="Upload" CausesValidation="true" 
            ValidationGroup="other"  onclick="other_upload_Click" />           
            </td></tr>
        <tr>
        <td colspan="2" >
    <asp:Label ID="othermsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label> 
</td>
        </tr>                 
    </table> 
 </ContentTemplate>       
 </asp:UpdatePanel>
   <asp:GridView ID="Other_grid" runat="server" AllowPaging="True" Width="100%"
                 AutoGenerateColumns="False" CssClass="gridview_css" 
        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   PageSize="10" 
        ondatabound="Other_grid_DataBound" 
        onpageindexchanging="Other_grid_PageIndexChanging">               
                    <PagerSettings Position="TopAndBottom"  />    
                    <PagerStyle HorizontalAlign="Right" />              
                      <Columns>
                                <asp:TemplateField HeaderText="Document">
                        <ItemTemplate>                           
                                <asp:Label ID="lblfilename" runat="server" Text='<%#Eval("File") %>'></asp:Label>
                                  <asp:Label ID="lblpath" runat="server" Text='<%#Eval("url") %>' Visible="false" ></asp:Label>
                        </ItemTemplate> 
                        </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Actions">                                      
                        <ItemTemplate> 
                            <table>           
            <tr align="center" valign="middle" >           
<td  style="border:none;">
  
                     <asp:ImageButton ID="btndownload"  width="32" Height="32"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 

                        </td> 
                        <td  style="border:none;">
                         <asp:ImageButton ID="doc_delete" ImageUrl="~/images/remove.png" runat="server" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this file-" + ((string)Eval("File"))+ " ?\");"%>' onclick="other_delete_Click"></asp:ImageButton>                                 
                         </td>
                         </tr> 
                         </table> 
                         </ItemTemplate> 
                         <ItemStyle Width="50px"  /> 
                         </asp:TemplateField>                                                                                 
                     </Columns>              
                       <EmptyDataTemplate>
                          <center><span style="color:Red;"> No Documents Available</span></center>                      
                       </EmptyDataTemplate>   
                       <RowStyle VerticalAlign="Middle" />             
            </asp:GridView>
</asp:Content>
