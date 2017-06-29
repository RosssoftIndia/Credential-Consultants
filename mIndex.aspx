<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mIndex.aspx.cs" MasterPageFile="~/AppMaster.master"   Inherits="mIndex" %>

<%@ Register Src="~/mMenu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>


<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">       
  <%--  <link href="style.css" rel="stylesheet" type="text/css" />   --%> 
 <script type="text/javascript" language="javascript">

     function fnCheckUnCheck(objId) {
         var grd = document.getElementById("<%= servicegrid.ClientID %>");

         //Collect A
         var rdoArray = grd.getElementsByTagName("input");

         for (i = 0; i <= rdoArray.length - 1; i++) {
             if (rdoArray[i].type == 'radio') {
                 if (rdoArray[i].id != objId) {
                     rdoArray[i].checked = false;
                 }
             }
         }
     } 
</script>
 <script type="text/javascript" language='JavaScript'>

     function fnTrapKD(btn, event) {
         if (document.all) {
             if (event.keyCode == 13) {
                 event.returnValue = false;
                 event.cancel = true;
                 // btn.click();
             }
         }
         else if (document.getElementById) {
             if (event.which == 13) {
                 event.returnValue = false;
                 event.cancel = true;
                 // btn.click();
             }
         }
         else if (document.layers) {
             if (event.which == 13) {
                 event.returnValue = false;
                 event.cancel = true;
                 // btn.click();
             }
         }
     }
            </script>

<link rel="stylesheet" href="Code/colorbox/colorbox.css" />
<script src="Code/colorbox/jquery.min.js"></script>
<script src="Code/colorbox/jquery.colorbox.js"></script>
<script>
    $(document).ready(function() {$(".iframe").colorbox({ iframe: true, width: "80%", height: "80%" });});
</script>
<script language="javascript" type="text/javascript">
    $(document).bind('cbox_closed', function() {
    window.scroll(0, 0);
    document.getElementById('<%=refresh.ClientID%>').click();
    });
</script>   
<script type="text/javascript">
<!--
          var message = "Function Disabled!";

          ///////////////////////////////////
          function clickIE4() {
              if (event.button == 2) {
                  alert(message);
                  return false;
              }
          }

          function clickNS4(e) {
              if (document.layers || document.getElementById && !document.all) {
                  if (e.which == 2 || e.which == 3) {
                      alert(message);
                      return false;
                  }
              }
          }

          if (document.layers) {
              document.captureEvents(Event.MOUSEDOWN);
              document.onmousedown = clickNS4;
          }
          else if (document.all && !document.getElementById) {
              document.onmousedown = clickIE4;
          }

          document.oncontextmenu = new Function("alert(message);return false")

// --> 
</script>

</asp:Content>
<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
<table style="width:100%;height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient"></td></tr>
 </table></td></tr>
 </table>
</asp:Content>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
    <center ><uc1:Menu ID="Menu1" runat="server" />   </center>         	
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
    <br />
<br />     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>             
            <asp:MultiView ID="FormViewcontrol" runat="server">            
<asp:View ID="Personal_Information" runat="server">
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
<table width="100%"> 
<tr>
<td>       
<br />
<div class="headertag">Personal Information</div>
<br />
<table class="search_css">
<tr><td><table>
<tr>
<td style="width: 174px">
          First / Given Name: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Fname" runat="server" Width="129px" 
              ValidationGroup="frm1_group" TabIndex="1" MaxLength="50" ></asp:TextBox>
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator2" runat="server" ControlToValidate="frm1_Fname"
        ErrorMessage="You must enter Firstname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
<td style="width: 246px">
Gender: <span style="color:Red;">*</span><br />       
    <asp:RadioButtonList ID="frm1_option_gender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="4" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px">
        <asp:ListItem Value="True">Male</asp:ListItem>
        <asp:ListItem Value="False" Selected="True">Female</asp:ListItem>
    </asp:RadioButtonList>

</td>
<td> 
Is the name on your documents<br />different? <span style="color:Red;">*</span><br />
    <asp:RadioButtonList ID="frm1_optin_name" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="frm1_optin_name_SelectedIndexChanged" RepeatLayout="Flow" TabIndex="9">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
    </asp:RadioButtonList>
</td> 
</tr>
<tr>
<td style="width: 174px">
 Middle Name:<br /> 
    <asp:TextBox ID="frm1_Mname" runat="server" Width="129px" TabIndex="2" 
        MaxLength="50" ></asp:TextBox>
</td>
<td style="width: 246px">Date of Birth: (mm/dd/yyyy) <span style="color:Red;">*</span><br />
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
        ErrorMessage="You must select a Year" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></td>
<td id="frm1_optional" runat="server" visible="false" rowspan="2" >    
          First / Given Name: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optFname" runat="server" Width="129px" TabIndex="10" 
              MaxLength="50" ></asp:TextBox>
    <br />
    Middle Name:<br /><asp:TextBox ID="frm1_optMname" runat="server" Width="129px" 
              TabIndex="11" MaxLength="50" ></asp:TextBox>
    <br />
          Last / Family / Surname: <span style="color:Red;">*</span><br />
          <asp:TextBox ID="frm1_optLname" runat="server" Width="129px" TabIndex="12" 
              MaxLength="50" ></asp:TextBox>    
</td> 
</tr>
<tr>
<td style="width: 174px">
    <br />
    <br />
        Last / Family / Surname: <span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_Lname" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TabIndex="3" MaxLength="50" ></asp:TextBox>
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator1" runat="server" ControlToValidate="frm1_Lname" ErrorMessage="You must enter Lastname" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
<td style="width: 246px">
    <br />
    <br />
 Country of Birth:<br /><asp:DropDownList id="frm1_Country_birth" AppendDataBoundItems="true" runat="server" Width="189px" TabIndex="8">
    </asp:DropDownList>
    <sv:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="frm1_Country_birth"
        ErrorMessage="You must select Country of Birth" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
</tr>  
</table></td></tr>
</table> 
</td> 
</tr>
<tr>
<td >
<br />
<div class="headertag">Address Information</div> 
<br />
<table class="search_css">
<tr><td><table> 
<tr>
<td style="width: 238px">
Address Line 1:<br /> 
<asp:TextBox ID="frm1_address1" runat="server" Width="208px" 
        ValidationGroup="frm1_group" MaxLength="100" TabIndex="13" ></asp:TextBox>
</td>
<td rowspan="2">
State/Province:<br /> 
   <asp:TextBox ID="frm1_state" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TextMode="MultiLine" Height="64px" 
        MaxLength="50" TabIndex="16" ></asp:TextBox>
</td>
<td>
Country:<br /> 
    <asp:DropDownList ID="frm1_option_country" AppendDataBoundItems="true" 
        runat="server" Width="204px" TabIndex="18">
    </asp:DropDownList>          
      </td>
</tr>
<tr>
<td style="width: 238px">
Address Line 2:<br /> 
   <asp:TextBox ID="frm1_address2" runat="server" Width="208px" MaxLength="100" 
        TabIndex="14" ></asp:TextBox>
</td>
</tr>
<tr>
<td style="width: 238px">
City:<br /> 
    <asp:TextBox ID="frm1_city" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="15" ></asp:TextBox>
</td>
<td>
    Postal/Zip Code:<br /> 
 <asp:TextBox ID="frm1_zip" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="17" ></asp:TextBox> 
</td>
</tr>
</table></td></tr>
</table>   

</td>
</tr>
<tr>
<td>
<br />
<div class="headertag">Contact Information</div>
<br />
<table class="search_css"><tr><td>
<table > 
<tr>
<td style="width: 158px">
    Primary Phone: <br />
    <asp:TextBox ID="frm1_home_phone" runat="server" Width="200px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="19" ></asp:TextBox> 
</td>
<td style="width: 49px"></td>
<td>
Primary Email Address: <span style="color:Red;">*</span><br />
    <asp:TextBox ID="frm1_primarymail" runat="server" 
        Width="274px" ValidationGroup="frm1_group" MaxLength="50" TabIndex="22" ></asp:TextBox> 
        <sv:RequiredFieldValidator ID="rq_primary_email" runat="server" ControlToValidate="frm1_primarymail"
        ErrorMessage="You must enter a Primary Email Address"  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
  <sv:RegularExpressionValidator ID="frm1_ExpressionFieldValidator4" runat="server" ControlToValidate="frm1_primarymail"
        ErrorMessage="You must enter a valid E-mail ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="frm1_group">*</sv:RegularExpressionValidator >
</td>
</tr>
<tr>
<td style="width: 158px">
    Secondary Phone:<br />
    <asp:TextBox ID="frm1_work_phone" runat="server" 
        Width="200px" ValidationGroup="frm1_group" MaxLength="50" TabIndex="20" ></asp:TextBox>
</td>
<td style="width: 49px"></td> 
<td>
Confirm Primary Email Address:<br />
    <asp:TextBox ID="frm1_confrprimary" 
        runat="server" Width="274px" ValidationGroup="frm1_group" MaxLength="50" 
        TabIndex="23" ></asp:TextBox>
 <sv:CompareValidator ID="frm1_comparevalidator1" runat="server" ControlToValidate="frm1_confrprimary"
        ErrorMessage="E-mail Address entered seems to miss match" ControlToCompare="frm1_primarymail"  ValidationGroup="frm1_group">*</sv:CompareValidator>
  </td>
</tr>
<tr>
<td style="width: 158px">
    Mobile Phone:<br />
    <asp:TextBox ID="frm1_cell_phone" runat="server" 
        Width="129px" ValidationGroup="frm1_group" MaxLength="50" TabIndex="21" ></asp:TextBox> 
</td>
<td style="width: 49px"></td>
</tr>
<tr> 
<td></td>
<td style="width: 49px"></td>
</tr>      
</table> 
</td></tr></table> 
</td>
</tr>
<tr>
<td  class="warning_box_msg">
<p>Before you Continue to next step, please read and agree to our Terms &amp; Conditions as well as the Privacy Policy and Terms of Service for online processing.</p>
                                <p>
                                    <i>By checking the boxes below, you indicate that you are electronically signing and
                                        agreeing to be bound by all of the terms, conditions, and notices contained or referenced
                                        on this site:</i></p>
 <asp:CheckBox ID="frm1_chk1" runat="server" ValidationGroup="frm1_group" Text="I agree to the "/><a id="toc" runat="server" href="#" class='iframe' title="Terms and Conditions" >Terms and Conditions</a> 
                of this application. I also agree to <a href="PrivacyPolicy.htm" class='iframe' title="Privacy Policy" >Privacy Policy</a> and <a href="TermsOfService.htm" class='iframe' title="Terms of Service">Terms of Service</a> for processing my order online. 
  <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="You must agree to Terms and Conditions" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="frm1_group"  ValidateEmptyText="true"  Text="*"></asp:CustomValidator>
</td>
</tr> 
<tr>
<td style="height: 24px"> 
<div>
<table width="100%">
<tr>
<td ></td>
<td style="width: 50%; text-align: left;">
    &nbsp;</td>
<td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
    Continue to Step 2:<br />
    <span style="color:#AEAEAE; font-style: italic;">Services &amp; Fees</span></td>
<td><asp:ImageButton ID="frm1_Btn_continue" OnClick="frm1_Btn_continue_Click"  
        runat="server" ImageUrl="~/images/r-arrow1.png" 
        ImageAlign="Baseline" /></td>
</tr>
</table>
</div> 
</td>
</tr>  
<tr><td >
<sv:ValidationSummary id="frm1_summary" runat="server" CssClass="error_box_summary" ValidationGroup="frm1_group" />
</td></tr>     
</table>     
</asp:View> 
<asp:View ID="Service" runat="server">
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
    <table width="100%"> 
      <tr>
        <td>
        <br />
         <div class="headertag">Basic Service Type</div>     
         <br />
         <div class="warning_box_msg" contenteditable="false"> Please select the type of basic service you would like.</div>
         <br />   
         <table width="100%"><tr>
         <td>
           <asp:GridView ID="servicegrid" runat="server" AutoGenerateColumns="False"  
                 DataKeyNames="id" OnLoad="servicegrid_Load" style="TEXT-ALIGN: left" 
                 CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                 AlternatingRowStyle-CssClass="alt" ondatabound="servicegrid_DataBound" >
                   <Columns>
            <asp:TemplateField ShowHeader="False" SortExpression="Name">                           
                           <ItemTemplate>
                              <%-- <input id="Radio1" runat="server" type="radio" value='<%# Eval("id") %>' title='<%# Eval("Name") %>' onclick="fnCheckUnCheck(this.id);" />--%>
                               <input id="Checkbox1" runat="server"  value='<%# Eval("id") %>' type="checkbox" />                               
                               <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                           </ItemTemplate>                
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <center>
                               <a id="link" href='<%# Eval("id", "Service_description.aspx?cus={0}") %>' class='iframe'  title="Description :: Information Block">Description</a>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>                      
                       <asp:TemplateField ShowHeader="False" SortExpression="Cost">                         
                           <ItemTemplate>
                           <center>
                               <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>                       
                   </Columns>
                   <EmptyDataTemplate>
                       No Basic Service Type Available
                   </EmptyDataTemplate>                   
               </asp:GridView>      
         </td></tr></table>                        
        </td>
        </tr> 
        </table>
        <br />
           <div class="headertag">Additional Services</div>  
           <br />
           <div class="warning_box_msg"> Please select any additional services you would like.</div>
           <br />
        <table width="100%"> 
      <tr id="frm6_optional" runat="server">
        <td>
        <table width="100%">           
               <tr>
               <td>
             <asp:GridView ID="addservicegrid" runat="server" AutoGenerateColumns="False" 
                       DataKeyNames="id" OnLoad="addservicegrid_Load" style="TEXT-ALIGN: left" 
                       CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" ondatabound="addservicegrid_DataBound">
                   <Columns>
            <asp:TemplateField ShowHeader="False" SortExpression="Name">                           
                           <ItemTemplate>                              
                               <input id="Checkbox2" runat="server"  value='<%# Eval("id") %>' type="checkbox" />
                               <asp:Label ID="Label3" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                               <asp:DropDownList  ID="drpCount" Visible="false"  runat="server"></asp:DropDownList>   
                           </ItemTemplate>            
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <center>
                               <a id="link" href='<%# Eval("id", "Service_description.aspx?cus={0}") %>'  class='iframe' title="Description :: Information Block">Description</a>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField ShowHeader="False" SortExpression="Cost">                         
                           <ItemTemplate>
                           <center>
                               <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="false"  Text='<%# Eval("Type") %>'></asp:Label>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
                   <PagerStyle CssClass="pgr" />
                   <EmptyDataTemplate>
                       No Additional Service Available
                   </EmptyDataTemplate>
                   <AlternatingRowStyle CssClass="alt" />
               </asp:GridView>                
                   </td>
               </tr>   
            </table> 
        </td>
        </tr>    
          <tr>
                <td style="height: 24px">
                    <div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="frm6_btn_previous" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/l-arrow1.png" OnClick="frm6_btn_previous_Click" />
                                </td>
                                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                                    Go Back to Step 2:<br />
                                    <span style="color:#AEAEAE; font-style: italic;">Services &amp; Fees</span></td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Continue to Step 3:<br />
                                    <span style="color:#AEAEAE; font-style: italic;">Review</span>
                                </td>
                                <td>
                                    <asp:ImageButton ID="frm6_btn_continue" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/r-arrow1.png" OnClick="frm6_Btn_continue_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>              
	</table>     
</asp:View>
<asp:View ID="Review" runat="server">  
    <table width="100%"> 
      <tr >
        <td>
        <br /> 
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">              
            <ContentTemplate> 
                      <div class="headertag">Instructions</div>
                      <br />
           <table width="100%" >             
            <tr><td class="warning_box">Please review the information in your application. You may change the information if needed. When all the information is correct, please continue to the next section.</td></tr>
            </table>
            <br />            
            <br />
            <div class="headertag">Personal Information</div>
            <br />
                        
                   <asp:DetailsView ID="Applicant" runat="server" AutoGenerateRows="False"  OnDataBound="Applicant_DataBound" OnLoad="Applicant_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                       <Fields>
                           <asp:TemplateField HeaderText="File Number" Visible="false">                              
                               <ItemTemplate>
                               <div><b><%# Eval("FileNumber") %></b></div>                                                             
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                             <ItemTemplate>
                             <br />
                             </ItemTemplate>
                             </asp:TemplateField>
                                 <asp:TemplateField HeaderText="FirstName" >                          
                               <ItemTemplate>             
                               <span><%# Eval("FirstName")%></span> 
                               </ItemTemplate>
                           </asp:TemplateField>
                              <asp:TemplateField HeaderText="MiddleName" >                          
                               <ItemTemplate>             
                                    <span><%# Eval("MiddleName")%></span>
                               </ItemTemplate>
                           </asp:TemplateField>
                              <asp:TemplateField HeaderText="LastName" >                          
                               <ItemTemplate>             
                                    <span><%# Eval("LastName")%></span>                                                                             
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Name" SortExpression="Name" Visible="false">                          
                               <ItemTemplate>                               
                                   <asp:Label ID="Label18" runat="server"></asp:Label>
                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                   <asp:Label ID="Label17" runat="server" Text='<%# Eval("Gender") %>' Visible="False"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:BoundField DataField="Gender" HeaderText="Gender:" SortExpression="Gender" />
                           <asp:TemplateField HeaderText="DateOfBirth" SortExpression="DateOfBirth">
                               <ItemTemplate>
                                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("DateOfBirth","{0:d}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Address" SortExpression="Address">                             
                               <ItemTemplate>
                                   <asp:Label ID="Label3" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />
                                   <asp:Label ID="Label21" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                   <asp:Label ID="Label13" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                   <br />
                                   <asp:Label ID="Label14" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                   <asp:Label ID="Label15" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label><br />
                                   <asp:Label ID="Label16" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="HomePhone" HeaderText="Primary Phone:" SortExpression="HomePhone" />
                           <asp:BoundField DataField="WorkPhone" HeaderText="Secondary Phone:" SortExpression="WorkPhone" />
                           <asp:BoundField DataField="MobilePhone" HeaderText="Mobile Phone" SortExpression="MobilePhone" />
                           <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />                           
                       </Fields>                       
                   </asp:DetailsView>                        
            
                         <br />                                             
                <div class="headertag">Purpose</div>  
                <br />                              
                   <asp:DetailsView ID="purposegrid" runat="server" AutoGenerateRows="False" OnLoad="purposegrid_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                       <Fields>
                           <asp:BoundField DataField="Evaluation_Name" HeaderText="Main Purpose:" SortExpression="Evaluation_Name" />
                           <asp:BoundField DataField="Name" HeaderText="Report Type:" SortExpression="Name" />
                       </Fields>                      
                   </asp:DetailsView>            
           
               <br />  
                   
                        <div class="headertag">General Service</div>
                        <br />
              <asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" OnDataBound="service1grid_DataBound" OnLoad="service1grid_Load" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <Columns>
                                    <asp:TemplateField HeaderText="Service Type" SortExpression="Name">                                       
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                               <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                                   <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server">Change</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="service1grid_del" runat="server" CausesValidation="False" 
                                                CommandName="Delete" ImageUrl="~/images/remove.png" 
                                                OnClick="service1grid_del_Click" Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <EmptyDataTemplate>
                                    No service requested
                                </EmptyDataTemplate>                               
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                      
                     <br />                             
                     <div class="headertag">Sender&#39;s Information</div>
                     <br />
                     <table width="100%">
                     <tr>
                     <td width="150px">Sender&#39;s Name</td>
                     <td>
                         <asp:TextBox ID="txtsendername" runat="server" Width="200px"></asp:TextBox></td>
                     </tr>
                        <tr>
                     <td>Sender&#39;s Contact</td>
                     <td><asp:TextBox ID="txtsendercontact" runat="server" Width="200px"></asp:TextBox></td>
                     </tr>
                        <tr>
                     <td>Target Institution</td>
                     <td><asp:TextBox ID="txttargetinst" runat="server" Width="200px"></asp:TextBox></td>
                     </tr>
                        <tr>
                     <td>Special Notes</td>
                     <td><asp:TextBox ID="Applicantmsg" runat="server" Height="150px" TextMode="MultiLine" 
                          Width="100%"></asp:TextBox>
                           <asp:Label ID="Filenumber" runat="server" Font-Bold="True" Text="Filenumber" 
                                        Visible="False"></asp:Label>
                                          <asp:Button ID="refresh" runat="server" CausesValidation="False" 
                                    CssClass="refreshbtn" OnClick="refresh_Click" Text="refresh" />
                          </td>
                     </tr>
                     </table>  
        </ContentTemplate>       
         </asp:UpdatePanel>           
        </td>
        </tr>     
               <tr>
                <td style="height: 24px">
                    <div>
                        <table width="100%">                           
                            <tr>
                                 <td>
                                    
                                </td>
                                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                                    </td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Continue <br />                                    
                                </td>
                                <td>
                                    <asp:ImageButton ID="frm8_Btn_continue" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/r-arrow1.png" OnClick="frm8_Btn_continue_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>              
	</table>     
</asp:View>

         </asp:MultiView>           

<br />
<br />                                                                                                    
</asp:Content>



