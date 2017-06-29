<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Application.aspx.cs" MasterPageFile="~/secure/EditMaster.master" Inherits="secure_Edit_Application" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Edit Application</span>  
 <div class="submenu_style">            
             <div class="buttons">    
      
</div>  
		</div>
		<br />
		<br />       
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">

<script language="javascript" type="text/javascript">
        $(document).bind('cbox_closed', function() {
    document.getElementById('<%=refresh.ClientID%>').click();
    });
    
</script> 
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
<td><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png"/>
</td><td>Manage</td>
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
            <table width="100%"> 
            <tr><td>
            <div id="header">
                        <ul>                              
<li id="nav1holder" runat="server">
    <asp:LinkButton ID="nav1" 
        runat="server" onclick="nav_Click">Personal Information</asp:LinkButton></li>
<li id="nav2holder" runat="server" >
    <asp:LinkButton ID="nav2" runat="server" 
        onclick="nav_Click">Purpose</asp:LinkButton></li>
<li id="nav3holder" runat="server" >
    <asp:LinkButton ID="nav3" runat="server" 
        onclick="nav_Click">Education</asp:LinkButton></li>
<li id="nav4holder" runat="server" >
    <asp:LinkButton ID="nav4" runat="server" 
        onclick="nav_Click">Service</asp:LinkButton></li>
        <%--<li id="nav5holder" runat="server" >
    <asp:LinkButton ID="nav5" runat="server" 
        onclick="nav_Click">Review</asp:LinkButton></li>--%>
                        </ul>  
               <asp:Button ID="refresh" runat="server" CausesValidation="False" OnClick="refresh_Click" 
                            Text="refresh" style="display:none;"/> 
                </div>
             <hr />
  </td></tr>
            <tr><td>
             <asp:Panel ID="personalinfotab" runat="server" Enabled="false" Visible="false">
<table width="100%">    
<tr>
<td><div class="headertag">Personal Information</div>
</td>
</tr>
<tr><td>
<table class="edit_css">
  <tr><td  style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;">
                     <div class="submenu_block">            
           <div class="subbuttons"> 
            <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>  
           <td style="background-color:White;"></td>
           <td style="background-color:White;">  
    <a id="personalinfoedit" href="#" title="Personal Information :: Edit Block" runat="server" class="regular iframe">
        <img src="Code/icons/edit.ico" alt=""/> 
        <b>Edit</b>
    </a>    
    </td> 
    </tr> 
    </table> 
</div>         
		</div></td></tr>       
<tr><td>
<table width="100%">
<tr><td><table>
<tr>
<td style="width: 174px">
          First / Given Name: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Fname" runat="server" Width="129px" 
              ValidationGroup="frm1_group" TabIndex="1" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator2" runat="server" ControlToValidate="frm1_Fname"
        ErrorMessage="You must enter Firstname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 246px">
Gender: <span style="color:Red;">*</span><br />       
    <asp:RadioButtonList ID="frm1_option_gender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="4" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px">
        <asp:ListItem Value="True">Male</asp:ListItem><asp:ListItem Value="False" Selected="True">Female</asp:ListItem></asp:RadioButtonList></td><td> 
Is the name on your documents<br />different? <span style="color:Red;">*</span><br />
    <asp:RadioButtonList ID="frm1_optin_name" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="frm1_optin_name_SelectedIndexChanged" RepeatLayout="Flow" TabIndex="9">
        <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False" Selected="True">No</asp:ListItem></asp:RadioButtonList></td></tr><tr>
<td style="width: 174px">
 Middle Name:<br /> 
    <asp:TextBox ID="frm1_Mname" runat="server" Width="129px" TabIndex="2" 
        MaxLength="50" ></asp:TextBox></td><td style="width: 246px">Date of Birth: (mm/dd/yyyy) <span style="color:Red;">*</span><br />
     <asp:DropDownList ID="frm1_option_month" runat="server" Width="54px" TabIndex="5">                  
    </asp:DropDownList>
    <sv:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="frm1_option_month"
        ErrorMessage="You must select a Month" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>/   
     <asp:DropDownList ID="frm1_option_date" runat="server" Width="54px" TabIndex="6">               
    </asp:DropDownList> 
    <sv:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="frm1_option_date"
        ErrorMessage="You must select a Date" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>/&nbsp;<asp:DropDownList ID="frm1_option_year" runat="server" Width="54px" TabIndex="7">               
    </asp:DropDownList>          &nbsp;
    <sv:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="frm1_option_year"
        ErrorMessage="You must select a Year" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td id="frm1_optional" runat="server" visible="false" rowspan="2" >    
          First / Given Name: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optFname" runat="server" Width="129px" TabIndex="10" 
              MaxLength="50" ></asp:TextBox><br />
    Middle Name:<br /><asp:TextBox ID="frm1_optMname" runat="server" Width="129px" 
              TabIndex="11" MaxLength="50" ></asp:TextBox><br />
          Last / Family / Surname: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optLname" runat="server" Width="129px" TabIndex="12" 
              MaxLength="50" ></asp:TextBox></td></tr><tr>
<td style="width: 174px">
    <br />
    <br />
        Last / Family / Surname: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Lname" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TabIndex="3" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator1" runat="server" ControlToValidate="frm1_Lname" ErrorMessage="You must enter Lastname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 246px">
    <br />
    <br />
 Country of Birth:<br /><asp:DropDownList id="frm1_Country_birth" AppendDataBoundItems="true" runat="server" Width="189px" TabIndex="8">
    </asp:DropDownList>
    <sv:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="frm1_Country_birth"
        ErrorMessage="You must select Country of Birth" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr></table></td></tr></table>
 </td></tr><tr>
<td >
    <br />
<br />
<table width="100%">
<tr><td><table> 
<tr>
<td style="width: 238px">
Address Line 1: <span style="color:Red">*</span><br /> 
<asp:TextBox ID="frm1_address1" runat="server" Width="208px" 
        ValidationGroup="frm1_group" MaxLength="100" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator3" runat="server" ControlToValidate="frm1_address1"
        ErrorMessage="You must enter your Address" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td rowspan="2">
State/Province:<span style="color:Red;">*</span><br /> 
   <asp:TextBox ID="frm1_state" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TextMode="MultiLine" Height="64px" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator5" runat="server" ControlToValidate="frm1_state"
        ErrorMessage="You must enter state" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td>
Country: <span style="color:Red">*</span><br /> 
    <asp:DropDownList ID="frm1_option_country" AppendDataBoundItems="true" runat="server" Width="204px">
    </asp:DropDownList>          
    <sv:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="frm1_option_country"
        ErrorMessage="You must select Country" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr><tr>
<td style="width: 238px">
Address Line 2:<br /> 
   <asp:TextBox ID="frm1_address2" runat="server" Width="208px" MaxLength="100" ></asp:TextBox></td></tr><tr>
<td style="width: 238px">
City:<span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_city" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator4" runat="server" ControlToValidate="frm1_city"
        ErrorMessage="You must enter city" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td>
    Postal/Zip Code:<span style="color:Red;">*</span><br /> 
 <asp:TextBox ID="frm1_zip" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator6" runat="server" ControlToValidate="frm1_zip"
        ErrorMessage="You must enter zipcode" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td></tr></table></td></tr></table></td></tr><tr>
<td>
<br />
<br />
<table width="100%"><tr><td>
<table > 
<tr>
<td style="width: 158px">
    Primary Phone: <span style="color:Red;">*</span><br />
    <asp:TextBox ID="frm1_home_phone" runat="server" Width="200px" 
        ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator7" runat="server" ControlToValidate="frm1_home_phone"
        ErrorMessage="You must enter Primary Phone" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td><td style="width: 49px"></td>
<td>
Primary Email Address:<br /><asp:TextBox ID="frm1_primarymail" runat="server" 
        Width="274px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:RegularExpressionValidator ID="frm1_ExpressionFieldValidator4" runat="server" ControlToValidate="frm1_primarymail"
        ErrorMessage="You must enter a valid E-mail ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="frm1_group">*</sv:RegularExpressionValidator ></td></tr><tr>
<td style="width: 158px">
    Secondary Phone:<br />
    <asp:TextBox ID="frm1_work_phone" runat="server" 
        Width="200px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox></td><td style="width: 49px"></td> 
<td>
Confirm Primary Email Address:<br /><asp:TextBox ID="frm1_confrprimary" 
        runat="server" Width="274px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox><sv:CompareValidator ID="frm1_comparevalidator1" runat="server" ControlToValidate="frm1_confrprimary"
        ErrorMessage="E-mail Address entered seems to miss match" ControlToCompare="frm1_primarymail" ValidationGroup="frm1_group">*</sv:CompareValidator></td></tr><tr>
<td style="width: 158px">
    Mobile Phone:<br /><asp:TextBox ID="frm1_cell_phone" runat="server" 
        Width="129px" ValidationGroup="frm1_group" MaxLength="50" ></asp:TextBox></td><td style="width: 49px"></td>
</tr>
<tr> 
<td></td>
<td style="width: 49px"></td>
</tr> 
<tr> 
<td colspan="3">            
Have you ever used our services before? <span style="color:Red;">*</span> 
<br /> 
    <asp:RadioButtonList ID="frm1_option_service" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="frm1_option_service_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Selected="True" Value="False">No</asp:ListItem></asp:RadioButtonList>&nbsp;
  </td></tr>
  <tr id="frm1_optional1" visible="false" runat="server"> 
<td colspan="3" style="height: 94px">
Previous file number or exact name and year of previous evaluation:<br />
<asp:TextBox ID="frm1_previousid" runat="server" Width="545px" Height="73px" 
        TextMode="MultiLine" MaxLength="50" ></asp:TextBox></td></tr></table></td></tr></table></td></tr>
<tr>
<td  style="background-color:white;border-top: dashed 1px black;height: 36px;"> 
</td> 
</tr> 
</table>
</td>
</tr>
</table> 
</asp:Panel>  
</td></tr>         
                    <tr><td>  
                    <asp:Panel ID="purposetab" runat="server" Enabled="false" Visible="false">
                     <table width="100%" >                    
        <tr>
        <td>               
         <div class="headertag">Purpose</div>
        </td></tr>          
         <tr><td >
           <table class="edit_css">   
              <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;">
                     <div class="submenu_block">            
           <div class="subbuttons">   
            <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>  
           <td style="background-color:White;"></td>
           <td style="background-color:White;">  
    <a id="purposeedit" href="#" title="Purpose :: Edit Block." runat="server" class="regular iframe">
        <img src="Code/icons/edit.ico" alt=""/> 
        <b>Edit</b>
    </a>    
    </td> 
    </tr> 
    </table> 
</div>         
		</div></td></tr>          
            <tr> 
               <td><br /><br />
               What is the purpose of this service :&nbsp;<asp:TextBox ID="txtpurpose" 
                       runat="server" Width="300px"></asp:TextBox></td>
           </tr>
           <tr>
               <td style="text-align: left" >
                   <asp:Label ID="lblcaption" runat="server" ></asp:Label><br /><br />
                   &nbsp;<asp:TextBox ID="txtcontent" runat="server" Width="300px"></asp:TextBox>&nbsp;<asp:Label ID="lblstatecaption" runat="server" Text="state:" ></asp:Label><asp:TextBox ID="txtstate" runat="server"></asp:TextBox>
                   <br />
                   <br />
               </td>
               </tr>   
               <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>          
             
                        </table>
        </td> 
        </tr> 
        </table> 
        </asp:Panel> 
         
                        
		                </td></tr>               
                   <tr><td>
                       <asp:Panel ID="secondarytab" runat="server" Enabled="true"  Visible="false">
                     <table width="100%">  
                      <tr><td><div class="headertag">Secondary Education History</div></td></tr>  
                     <tr><td>
                      <table class="edit_css">    
                     <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;">
                     <div class="submenu_block">            
           <div class="subbuttons">  
            <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
            <tr>
           <td style="background-color:White;"></td>  
           <td style="background-color:White;"></td>
           <td style="background-color:White;">  
    <a href="Popup_Addschool.aspx" title="Secondary Education (also called high school or preparatory school education) :: secondary Education Block." runat="server"  class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>    
    </td> 
    </tr> 
    </table> 
</div>         
		</div></td></tr>                      
                                   
                     <tr><td>
                     <br />
                     <asp:GridView ID="hischoolgrid" runat="server" 
                             AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                             CssClass="gridview_css" OnDataBound="hischoolgrid_DataBound" 
                             PagerStyle-CssClass="pgr" style="TEXT-ALIGN: center">
                             <Columns>
                                 <asp:TemplateField HeaderText="Institution" SortExpression="Name">
                                     <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                         <asp:Label ID="Label5" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Degree" SortExpression="Expr1">
                                     <ItemTemplate>
                                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("Expr1") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                                     <ItemTemplate>
                                         <asp:Label ID="Label3" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                                     <ItemTemplate>
                                         <asp:Label ID="Label4" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Edit">
                                     <ItemTemplate>
                                         <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                                     </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                     <ItemStyle Width="20px" />
                                     <ItemTemplate>
                                         <asp:ImageButton ID="hischoolgrid_del" runat="server" CausesValidation="False" 
                                             CommandName="Delete" ImageUrl="~/images/remove.png" 
                                             OnClick="hischoolgrid_del_Click" Text="Delete" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                             <EmptyDataTemplate>
                                 No High school history entered
                             </EmptyDataTemplate>
                         </asp:GridView>
                      <br />
                      </td></tr> 
                       <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                     </table> 
                          </td></tr>  
                        <tr><td><br /></td></tr> 
                         <tr><td><div class="headertag">Higher Education History</div></td></tr>                              
                        <tr><td>                       
                      <table class="edit_css">   
                            <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;">
                     <div class="submenu_block">            
           <div class="subbuttons">   
            <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>  
           <td style="background-color:White;"></td>
           <td style="background-color:White;">  
    <a  href="Popup_Adduniversity.aspx" title="Higher Education (also called college or university education) :: Higher Education Block." runat="server" class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>    
    </td></tr>
    </table> 
</div>         
		</div></td></tr>                             
                        <tr><td>
                        <br />
                        <asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" OnDataBound="univgrid_DataBound" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">                      
                       <Columns>                        
                           <asp:BoundField DataField="Name" HeaderText="Institution" SortExpression="Name" />
                           <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                           <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                           <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                          <asp:TemplateField HeaderText="Edit">
                               <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                   <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                               </ItemTemplate>
                              <ItemStyle Width="20px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                               <ItemStyle Width="20px" />
                               <ItemTemplate>
                                   <asp:ImageButton ID="univgrid_del" CommandName="Delete" runat="server" CausesValidation="False" ImageUrl="~/images/remove.png" Text="Delete" OnClick="univgrid_del_Click" />
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>                      
                       <EmptyDataTemplate>
                            No University History entered
                       </EmptyDataTemplate>                            
                   </asp:GridView>
                      <br />
                        </td></tr> 
                        <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                      </table>  
                        </td></tr>
                       
                        </table> 
                        </asp:Panel> 
                        </td></tr>                          
                    <tr><td>
                       <asp:Panel ID="servicetab" runat="server" Enabled="true"  Visible="false">
                     <table width="100%" > 
                     <tr><td><div class="headertag">General Service</div></td></tr> 
                     <tr><td>
                      <table class="edit_css">   
                     <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">
            <a id="servicegridEdit" href="Popup_Service.aspx?id=1" title="Service :: Edit service Block" runat="server"  class="regular iframe">
        <img src="Code/icons/edit.ico" alt=""/>
        <b>Edit</b> </a> 
           <a id="servicegridAdd" href="Popup_Service.aspx?id=0" title="Service :: Add service Block." runat="server" class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div></td></tr>   
		             <tr><td>  
		             <br /> 		            
                  <asp:GridView ID="servicegrid" runat="server" 
                                 AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                 CssClass="gridview_css" OnDataBound="servicegrid_DataBound" 
                                 PagerStyle-CssClass="pgr" ShowFooter="True" style="TEXT-ALIGN: center">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Service Type" SortExpression="Name">
                                         <ItemTemplate>
                                             <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                             <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>' 
                                                 Visible="False"></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                         <ItemTemplate>
                                             <asp:Label ID="Label6" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Count">
                                         <ItemTemplate>
                                             <asp:Label ID="lblcount" runat="server" Text='<%# Bind("Countno") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                         </FooterTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                       <PagerStyle CssClass="pgr" />
                                 <EmptyDataTemplate>
                                     No service requested
                                 </EmptyDataTemplate>
                                 <AlternatingRowStyle CssClass="alt" />
                             </asp:GridView>                                   
                   <br />
                   </td></tr>
                   <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                   </table>
                   </td> </tr> 
                   <tr><td><br /></td></tr>
                    <tr><td><br /><div class="headertag">Primary Mailing Address</div></td></tr> 	                                       
                     <tr><td>
                      <table class="edit_css">   
                     <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">
            <a id="primaryEdit" href="Popup_Primaryaddress.aspx?id=1" title="Primary Mail Address :: Edit Address Block." runat="server"  class="regular iframe">
        <img src="Code/icons/edit.ico" alt=""/>
        <b>Edit</b> </a> 
           <a id="primaryAdd" href="Popup_Primaryaddress.aspx?id=0" title="Primary Mail Address :: Add Address Block." runat="server"   class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div></td></tr>	 
		             <tr><td>  
		             <br /> 
		             <asp:DetailsView ID="frm5_primarygrid" runat="server" AutoGenerateRows="False" 
                        CssClass="detailview_css"  PagerStyle-CssClass="pgr"  
                        AlternatingRowStyle-CssClass="alt">
                       <Fields>
                                 <asp:TemplateField HeaderText="Name" >                          
                               <ItemTemplate>             
                               <span><%# Eval("Name")%></span> 
                               </ItemTemplate>
                           </asp:TemplateField>                         
                           <asp:TemplateField HeaderText="Address" SortExpression="Address">                             
                               <ItemTemplate>
                                  <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                             <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                             <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                             <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>                      
                       </Fields>   
                       <EmptyDataTemplate>
                                     No Primary Address Available
                                 </EmptyDataTemplate>                    
                   </asp:DetailsView>
                    <br />
                   </td></tr>  
                   <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                   </table>
                   </td>
                   </tr>                            
                   <tr><td><br /></td></tr>
                    <tr><td><br /><div class="headertag">Evaluation Mailing Address</div></td></tr>
                   <tr><td>
                      <table class="edit_css">   
                      <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div id="service_EvaluationEditmenu" runat="server"  class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">
            <a id="EvaluationEdit" href="Popup_Evaladdress.aspx?id=1" title="Evaluation Mail Address :: Edit Address Block." runat="server" class="regular iframe">
        <img src="Code/icons/edit.ico" alt=""/>
        <b>Edit</b> </a> 
           <a id="EvaluationAdd" href="Popup_Evaladdress.aspx?id=0" title="Evaluation Mail Address :: Add Address Block." runat="server" class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div> 
                         <div id="service_EvaluationEditmsg" runat="server"  class="editapp_inlinewarning" style="background-color:#fce9e9;">Please fill in the "Primary Mailing Address" before Editing this block.</div></td></tr>	  
                   <tr><td> 
                   
                   <br />
                      <asp:GridView ID="deliveryaddressgrid" runat="server" 
                                 AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                 CssClass="gridview_css"
                                 PagerStyle-CssClass="pgr" 
                           style="TEXT-ALIGN: center">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Recipient">
                                         <ItemTemplate>
                                             <asp:Label ID="name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField> 
                                          <asp:TemplateField HeaderText="Address">                            
                                         <ItemTemplate>
                                             <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                             <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                             <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                             <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                             <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                         <ItemTemplate>                                                                                         
                                              <asp:Label ID="lbltype" runat="server" Text='<%# Eval("Type") %>'></asp:Label>                                            
                                               <asp:Label ID="lblid" runat="server" Text='<%# Eval("rowid") %>' Visible="false" ></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField> 
                                         <asp:TemplateField HeaderText="Mailing Address">
                                         <ItemTemplate>                                                                                       
                                              <asp:Label ID="lblsent" runat="server" Text='<%# Eval("Sentto") %>'></asp:Label>                                            
                                         </ItemTemplate>
                                         </asp:TemplateField>                                          
                                          </Columns>
                                 <PagerStyle CssClass="pgr" />
                                 <EmptyDataTemplate>
                                     No Delivery Address Available
                                 </EmptyDataTemplate>
                                 <AlternatingRowStyle CssClass="alt" />
                             </asp:GridView>  
                         <br />
                   </td></tr>
                    <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                    </table>  
		             
		             </td></tr> 
                   <tr id="Editsecadditional" runat="server"><td>
                   <br /><div class="headertag">Additional Official Hard Copy Service</div>
                      <table class="edit_css" style="margin-top:5px;" >   
                      <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div id="service_additionlmenu" runat="server"  class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">           
           <a id="AdditionalAdd" href="Popup_Additionalcopy.aspx?id=0" title="Additional Copy Address :: Add Address Block." runat="server" class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div> 
                         <div id="service_additionlmsg" runat="server"  class="editapp_inlinewarning" style="background-color:#fce9e9;">
                             Please fill in the &quot;Primary Mailing Address&quot; before Adding or Editing this 
                             block.</div></td></tr>	  
                   <tr><td> 
                   
                   <br />
                     <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="copychargergrid_DataBound" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Type" SortExpression="Type">                                          
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                  <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="false" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count">
                               <EditItemTemplate>
                                                &nbsp;
                               </EditItemTemplate>
                               <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                        <asp:TemplateField HeaderText="Cost">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                     <ItemTemplate>
                                         <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                                     </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                 </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="copychargergrid_del" runat="server" 
                                                    CausesValidation="False" ImageUrl="~/images/remove.png" 
                                                    OnClick="copychargergrid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>                                  
                                    <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Additional Copies Requested
                                    </EmptyDataTemplate>                                 
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                         <br />
                   </td></tr>
                    <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                    </table>  
		             
		             </td></tr> 
		               <tr id="Editsecemail" runat="server"><td>
		                             <br /><div class="headertag">Official Electronic Service</div>
                      <table class="edit_css" style="margin-top:5px;">   
                      <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div  class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">            
           <a id="EmailAdd" runat="server"   href="Popup_Email.aspx?id=0" title="Email:: Email Data Entry Block." class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
		             
</div>         
		</div>		
		                        </td></tr>	  
                   <tr><td> 
		             
                   <br />
                      <asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="email_grid_DataBound" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                     <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Type" SortExpression="Type">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count">
                                            <EditItemTemplate>
                                                &nbsp;
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                        <asp:TemplateField HeaderText="Cost">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Edit">
                                     <ItemTemplate>
                                         <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                                     </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                 </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="email_grid_del" runat="server" CausesValidation="False" 
                                                    ImageUrl="~/images/remove.png" OnClick="email_grid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>                                  
                                     <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Electronic Copies Requested
                                    </EmptyDataTemplate>                                   
                                     <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>                                 
                         <br />
                   </td></tr>
                    <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                    </table> 
		               </td> </tr>             
                   <tr id="Editsecfax" runat="server"><td>
                   <br /><div class="headertag">Fax Service</div>
                      <table class="edit_css" style="margin-top:5px;">   
                      <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div  class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">            
           <a id="FaxAdd" runat="server"  href="Popup_Fax.aspx?id=0" title="Fax :: Fax Data Entry Block." class="regular iframe">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div>		
		                        </td></tr>	  
                   <tr><td> 
                   
                   <br />
                      <asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="fax_grid_DataBound" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                     <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Type" SortExpression="Type">
                               <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                               </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count">
                               <EditItemTemplate>
                                                &nbsp;
                               </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Edit">
                                     <ItemTemplate>
                                         <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                                     </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                 </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="fax_grid_del" runat="server" CausesValidation="False" 
                                                    ImageUrl="~/images/remove.png" OnClick="fax_grid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                           </asp:TemplateField>
                                    </Columns>                                  
                                     <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>                                   
                                     <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>  
                         <br />
                   </td></tr>
                    <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>
                    </table>  
		             <tr><td style="display:none;" >
                       <br />
                             <asp:GridView ID="addongrid" runat="server" AlternatingRowStyle-CssClass="alt" 
                                 AutoGenerateColumns="False" CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                                 ShowFooter="True" style="TEXT-ALIGN: center" 
                           ondatabound="addongrid_DataBound">
                                 <PagerStyle CssClass="pgr" />
                                 <EmptyDataTemplate>
                                     No service requested
                                 </EmptyDataTemplate>
                                 <Columns>
                                     <asp:TemplateField HeaderText="Delivery Type" SortExpression="Name">
                               <EditItemTemplate>
                                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                               </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="lbldeliveryname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                         </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Service Type" SortExpression="Type" 
                                         Visible="False">
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                             Copy
                                         </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Qty">
                                         <ItemTemplate>
                                             1
                                         </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count" 
                                         Visible="False">
                               <EditItemTemplate>
                                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Count") %>'></asp:TextBox>
                               </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label4" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                         </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                               <ItemTemplate>  
                                             <asp:Label ID="Label2" runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                               </ItemTemplate>
                                         <FooterTemplate>
                                             <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                         </FooterTemplate>
                           </asp:TemplateField>
                                 </Columns>
                       <AlternatingRowStyle CssClass="alt" />
                             </asp:GridView>
 <br />
 <br />
                             <asp:Label ID="Reviewcost" runat="server" Font-Bold="True" Text="0"></asp:Label>
                      </td></tr>
		             </td></tr>
		             </table> 		             
		             </asp:Panel>  
		             </td> 
		             </tr> 
                      
                            
                             <%--<tr>
                             <td>
                                    <asp:Panel ID="ReviewTab" runat="server" Enabled="false" Visible="false">			               
                      <table class="edit_css">   
                      <tr><td style="background-color:white;border-bottom: dashed 1px black;padding:2px 5px 5px 5px;"><div  class="submenu_block">            
           <div class="subbuttons">   
           <table align="right" style="border-collapse:collapse;border-spacing: 0px;border:none;">
           <tr>
           <td style="background-color:White;"></td>
           <td style="background-color:White;"></td>
           <td style="background-color:White;">            
           <a href="Popup_Fax.aspx?id=0" title="Fax :: Fax Data Entry Block. :: width:1000, height:500" rel="iframe" class="regular lightview">
        <img src="Code/icons/irc-join.ico" alt=""/> 
        <b>Add</b>
    </a>     
           </td>
           </tr>
           </table> 
        
</div>         
		</div>		
		                        </td></tr>	 
		                        <tr><td>
		           
<div class="headertag" >General Service</div>
<br />
<asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                     <Columns>
                         <asp:TemplateField HeaderText="Service Type" SortExpression="Name">
                             <EditItemTemplate>
                                 &nbsp;
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Description" SortExpression="Description">
                             <ItemTemplate>
                                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Cost" SortExpression="Cost">                     
                                        <ItemTemplate>
                                            <asp:Label ID="txtprice" runat="server" Text='<%# Eval("price", "{0:C}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty">
                             <ItemTemplate>
                                  <asp:Label ID="Label5" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                             </ItemTemplate>
                             <FooterTemplate>
                                 <asp:Label ID="Label7" runat="server" Text="0" Font-Bold="True"></asp:Label>
                             </FooterTemplate>
            </asp:TemplateField>               
    </Columns>
                     <PagerStyle CssClass="pgr" />
    <EmptyDataTemplate>
                         No service requested
</EmptyDataTemplate>
                     <AlternatingRowStyle CssClass="alt" />
</asp:GridView> 
  <br />
<br />    
<div class="headertag" >Additional Official Hard Copy Service</div>
<br />       
 <asp:GridView ID="copychargergrid_display" runat="server" AutoGenerateColumns="False" DataKeyNames="id" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                       <Columns>
                         <asp:TemplateField HeaderText="Service Type" SortExpression="Type">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cost">
                             <ItemTemplate>
                                 <asp:Label ID="Label9" runat="server"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty" SortExpression="Count">
                             <EditItemTemplate>
                                 &nbsp;
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cost">
                             <FooterTemplate>
                                 <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                             </FooterTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label10" runat="server"></asp:Label>
                             </ItemTemplate>
            </asp:TemplateField>        
 </Columns>
                       <PagerStyle CssClass="pgr" />
<EmptyDataTemplate>
                         No Additional Copies Requested
</EmptyDataTemplate>
                       <AlternatingRowStyle CssClass="alt" />
</asp:GridView>  
<br />
<br />
<div class="headertag" >Fax Copy Service</div>
<br />     			
<asp:GridView ID="fax_grid_display" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  ShowFooter="True" style="text-align: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                                   <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Type" SortExpression="Type">
                <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count">                                         
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="Cost">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
            </asp:TemplateField>               
           
                                    </Columns>                                  
                                    <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>                                  
                                   <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
 <br />
 <br />
<div class="headertag" >Delivery Service - Official Hard Copy(ies) &amp; Additional Copies</div>
<br /> 
 
  <br />
<br />
<b>Total Amount Due=</b>
<br />                
<br />    
 </td></tr>	   
 <tr><td style="background-color:white;border-top: dashed 1px black;height: 36px;"></td></tr>  
 </table>          
		             
		     </asp:Panel>         
                             </td>
                         </tr>--%>
              
		             
		             </table>    
               
           

</ContentTemplate>       
</asp:UpdatePanel>    
</asp:Content>
