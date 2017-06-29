<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App_Upload.aspx.cs" MasterPageFile="~/popupMaster.master" Inherits="App_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  

</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
<div class="headertag" >Applicant Documents</div>
<br />
<span style="color:#FF3300">Note:</span>You may upload document images here. Only PDF, JPEG, DOC and DOCX files are accepted.

   <table class="tbcontent">
   <tr id="row" runat="server" visible="false" ><td>File Name</td><td>
       <asp:TextBox ID="txtdocname" runat="server"></asp:TextBox>
       <%--<asp:RequiredFieldValidator ID="Validator1" runat="server" ControlToValidate="txtdocname"  ErrorMessage="File Name Required!" ValidationGroup="doc"></asp:RequiredFieldValidator>--%></td></tr>
    <tr><td>Upload File</td><td><asp:FileUpload ID="doc_uploader" runat="server" />
        <asp:Button ID="docupload" runat="server" Text="Upload" CausesValidation="true" 
            ValidationGroup="doc"  onclick="doc_upload_Click" /></td></tr>
        <tr>
        <td colspan="2" >
          <asp:Label ID="docmsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label> 
        </td>
        </tr>                 
    </table> 
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
   <asp:ImageButton ID="btndownload" width="32" Height="32"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 
                        </td> 
                        <td  style="border:none;">
                         <asp:ImageButton ID="doc_delete" ImageUrl="~/images/remove.png"  runat="server" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this file-" + ((string)Eval("File"))+ " ?\");"%>' onclick="doc_delete_Click"></asp:ImageButton>                                 
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
<br />                                                                                                    
</asp:Content>