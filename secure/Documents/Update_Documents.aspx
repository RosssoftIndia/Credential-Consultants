<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_Documents.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_Documents_Update_Documents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Documents</span>
  <div class="submenu_style">  
   <div class="buttons">   
 <a href='Browse_Documents.aspx?search=<%=Request.QueryString["search"].ToString()%>&t1=<%=Request.QueryString["t1"].ToString()%>' class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Documents</b>
    </a>  
    <a  href="Add_Documents.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Documents</b>
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
                  <Triggers>
<asp:PostBackTrigger ControlID="upload" /> 
 </Triggers> 
            <ContentTemplate>
            <br />
            <br />
   <asp:DetailsView ID="DetailsView_Documents" runat="server" AutoGenerateRows="False" 
        DefaultMode="Edit"  OnLoad="DetailsView_Documents_Load" CssClass="detailview_css"  
                    PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_Documents_DataBound">        
        <Fields>
            <asp:TemplateField HeaderText="Name" >           
                <EditItemTemplate>
                    <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' Width="50%" MaxLength="225"></asp:TextBox>
                    <asp:Label ID="lblfilename" runat="server" Text='<%# Eval("Attachment") %>' ></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Country">              
               <EditItemTemplate>
                    <asp:DropDownList ID="Countrydp" runat="server" OnLoad="Countrydp_Load">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Select Country" ControlToValidate="Countrydp" InitialValue="0"></asp:RequiredFieldValidator>
                     <asp:Label ID="temp" Visible="false"  runat="server" Text='<%# Eval("Country_ID") %>'></asp:Label>
               </EditItemTemplate>
            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">              
                  <EditItemTemplate>
                      <div class="descouterblk">
                  <div class="clientdescblk"><CKEditor:CKEditorControl ID="destxt" runat="server" Text='<%# Eval("Description") %>' Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Toolbar="Basic"></CKEditor:CKEditorControl></div>                     
                   <div id="masterdescblk"  class="masterdescblk" style="display:none; overflow:hidden; height:150px;"><div id="masterdesc" runat="server" class="masterdescblkscroll"   style="width:100%;"><%# Eval("masterdesc")%></div><br /><div style="width:100%;text-align:right;">  <asp:Button ID="swap" runat="server" CssClass="btncolor" CausesValidation="false"  Text="Copy" OnClick="Swap_Click" /></div></div>
                  <div id="extratab" runat="server"  class="extratab" onmousedown="toggleSlide('masterdescblk');">Master Description</div>   
                          </div>     
                 </EditItemTemplate>           
            </asp:TemplateField>                       
            <asp:TemplateField ShowHeader="False">          
                <EditItemTemplate>
                    <asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" OnClick="Update_Click"
                        Text="Update" Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>        
    </asp:DetailsView>
    <br />
    <br />
    <table id="uploadblk" runat="server" class="tbcontent" visible="false"  >    
    <tr><td>Upload File</td><td><asp:FileUpload ID="docuploader" runat="server" />
        <asp:Button ID="upload" runat="server" Text="Upload" onclick="upload_Click" /></td></tr>
        <tr>
        <td id="trace" runat="server"  colspan="2" >
<%--        <div style="width:100%" >
    Server Trace:
    <br />
    </div>--%>
    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>         
<asp:RegularExpressionValidator  ValidationExpression=".*\.(pdf|PDF)$" ControlToValidate="docuploader"  ID="RegularExpressionValidator2" runat="server"  Text="You can only specify a pdf file"></asp:RegularExpressionValidator>        
        
        </td>
        </tr>       
    </table>
  </ContentTemplate>       
 </asp:UpdatePanel>  
         
  <asp:GridView ID="Doc_grid" runat="server" visible="false" AllowPaging="True" Width="100%" 
                 AutoGenerateColumns="False" CssClass="gridview_css" 
                    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   PageSize="1"  
                    ondatabound="Doc_grid_DataBound" >               
                    <PagerSettings Position="TopAndBottom"  />    
                    <PagerStyle HorizontalAlign="Right" />              
                      <Columns>
                                <asp:TemplateField HeaderText="Document">
                        <ItemTemplate>                           
                                <asp:Label ID="lblvirtualfilename" runat="server" Text='<%#Eval("Vfile") %>'></asp:Label>
                                 <asp:Label ID="lblfilename" runat="server" Visible="false"  Text='<%#Eval("File") %>'></asp:Label>
                        </ItemTemplate> 
                        </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Actions">                                      
                        <ItemTemplate> 
                            <table>           
            <tr align="center" valign="middle" >           
<td  style="border:none;">
  
                <asp:ImageButton ID="btndownload" ImageUrl="~/Code/icons/pdf_icon.png" width="22" Height="22"   runat="server" onclick="btndownload_Click"></asp:ImageButton>                                                 
      
                        </td> 
                        <td  style="border:none;">
                         <asp:ImageButton ID="delete" ImageUrl="~/images/remove.png" runat="server" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this file-" + ((string)Eval("Vfile"))+ " ?\");"%>' onclick="delete_Click"></asp:ImageButton>                                 
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
                    <AlternatingRowStyle CssClass="alt" />
            </asp:GridView> 
 </asp:Content>
