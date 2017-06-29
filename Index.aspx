<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" MasterPageFile="~/AppMaster.master"   Inherits="Index" %>

<%@ Register Src="~/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>


<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">       
   <%-- <link href="style.css" rel="stylesheet" type="text/css" />    --%>
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
      
    <style type="text/css">
        .style3
        {
            height: 34px;
        }
        </style>
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
Address Line 1: <span style="color:Red">*</span><br /> 
<asp:TextBox ID="frm1_address1" runat="server" Width="208px" 
        ValidationGroup="frm1_group" MaxLength="100" TabIndex="13" ></asp:TextBox>
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator3" runat="server" ControlToValidate="frm1_address1"
        ErrorMessage="You must enter your Address" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
<td rowspan="2">
State/Province:<span style="color:Red;">*</span><br /> 
   <asp:TextBox ID="frm1_state" runat="server" Width="129px" 
        ValidationGroup="frm1_group" TextMode="MultiLine" Height="64px" 
        MaxLength="50" TabIndex="16" ></asp:TextBox>
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator5" runat="server" ControlToValidate="frm1_state"
        ErrorMessage="You must enter state" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
<td>
Country: <span style="color:Red">*</span><br /> 
    <asp:DropDownList ID="frm1_option_country" AppendDataBoundItems="true" 
        runat="server" Width="204px" TabIndex="18">
    </asp:DropDownList>          
    <sv:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="frm1_option_country"
        ErrorMessage="You must select Country" InitialValue="0" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
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
City:<span style="color:Red;">*</span><br /> 
    <asp:TextBox ID="frm1_city" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="15" ></asp:TextBox>
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator4" runat="server" ControlToValidate="frm1_city"
        ErrorMessage="You must enter city" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</td>
<td>
    Postal/Zip Code:<span style="color:Red;">*</span><br /> 
 <asp:TextBox ID="frm1_zip" runat="server" Width="129px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="17" ></asp:TextBox> 
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator6" runat="server" ControlToValidate="frm1_zip"
        ErrorMessage="You must enter zipcode" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
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
    Primary Phone: <span style="color:Red;">*</span><br />
    <asp:TextBox ID="frm1_home_phone" runat="server" Width="200px" 
        ValidationGroup="frm1_group" MaxLength="50" TabIndex="19" ></asp:TextBox> 
    <sv:RequiredFieldValidator ID="frm1_RequiredFieldValidator7" runat="server" ControlToValidate="frm1_home_phone"
        ErrorMessage="You must enter Primary Phone" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
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
Confirm Primary Email Address:<br /><asp:TextBox ID="frm1_confrprimary" 
        runat="server" Width="274px" ValidationGroup="frm1_group" MaxLength="50" 
        TabIndex="23" ></asp:TextBox>
 <sv:CompareValidator ID="frm1_comparevalidator1" runat="server" ControlToValidate="frm1_confrprimary"
        ErrorMessage="E-mail Address entered seems to miss match" ControlToCompare="frm1_primarymail" ValidationGroup="frm1_group">*</sv:CompareValidator>
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
<tr> 
<td colspan="3">            
Have you ever used our services before? <span style="color:Red;">*</span> 
<br /> 
    <asp:RadioButtonList ID="frm1_option_service" runat="server" 
        RepeatDirection="Horizontal" 
        OnSelectedIndexChanged="frm1_option_service_SelectedIndexChanged" 
        AutoPostBack="True" TabIndex="24">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
    </asp:RadioButtonList>
    &nbsp;</td> 
</tr> 
<tr id="frm1_optional1" visible="false" runat="server"> 
<td colspan="3" style="height: 94px">
Previous file number or exact name and year of previous evaluation:<br />
<asp:TextBox ID="frm1_previousid" runat="server" Width="545px" Height="73px" 
        TextMode="MultiLine" MaxLength="50" TabIndex="25" ></asp:TextBox>
</td>            
</tr>        
</table> 
</td></tr>
</table> 
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
<tr><td></td></tr>
<tr>
<td ></td>
<td style="width: 50%; text-align: left;">
    &nbsp;</td>
<td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
    Continue to Step 2:<br />
    <span style="color:#AEAEAE; font-style: italic;">Purpose</span></td>
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
<asp:View ID="Educational_HighSchool" runat="server">
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
 <table width="100%" > 	
 <tr>
 <td>
 <div class="headertag">Instructions</div>
 <br /> 
 <table>
<tr>
<td class="warning_box_msg">
<div id="frm2_msginfo" runat="server" ></div><br /> 
<%--<div style="padding-left:40px;">
<b>please describe your secondary (high school) education before continuing, even if you do not want your secondary (high school) education to be evaluated.</b>   
</div>--%>

</td>
</tr> 
<tr><td></td></tr>
<tr>
 <td  class="warning_box">please describe your secondary (high school) education before continuing, even if you do not want your secondary (high school) education to be evaluated.</td>
 </tr>
</table>
</td> 
 </tr> 
	<tr id="frm2_display" runat="server" visible="false">
	<td>
	<br />
<div class="headertag">My Secondary Education</div>
<br />        
<asp:GridView ID="Highschoolgrid" runat="server" AutoGenerateColumns="False"  OnLoad="Highschoolgrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">              
              <Columns>
                  <asp:BoundField DataField="Name" HeaderText="Institution" SortExpression="Name" />
                  <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                  <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                  <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
              </Columns>              
              <EmptyDataTemplate>
                  No High school history entered
              </EmptyDataTemplate>                           
          </asp:GridView>
  </td>
  </tr>
  <tr>
  <td>
  <br />
   <div class="headertag">Secondary Education (also called high school or preparatory school education)</div>
   <br />
 <table class="search_css" > 
<tr> 
<td>Country of Study:&nbsp;<span style="color:Red;">*</span>
 <asp:DropDownList ID="frm2_opt_country" runat="server" Width="204px" AutoPostBack="True" OnSelectedIndexChanged="frm2_opt_country_SelectedIndexChanged" AppendDataBoundItems="true">
</asp:DropDownList>
</td> 
</tr> 
<tr  id="frm2_details" runat="server" visible="false"><td> 
 <table> 
 <tr>
 <td style="width: 279px">
 Name of Institution: <span style="color:Red;">*</span><br /><ajaxToolkit:ToolkitScriptManager runat="server" ID="frm2_ScriptManager1" />
					<asp:TextBox runat="server" ID="frm2_institution" Width="220px" 
         autocomplete="off" MaxLength="180" />
     <sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator3" runat="server" ControlToValidate="frm2_institution" ErrorMessage="You must Enter an Institution" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator>
    <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="frm2_autoComplete1" 
                TargetControlID="frm2_institution"
                ServicePath="Highschool.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="1" 
                CompletionInterval="30"
                EnableCaching="false"
                CompletionSetCount="20"
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"                
                ContextKey="CountryId" UseContextKey="True"><Animations>
                     <OnShow>
                        <Sequence>
                            
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide></Animations></ajaxToolkit:AutoCompleteExtender>
 </td>
 <td>
 Education Program:<br /> 
<asp:DropDownList ID="frm2_option_degree" AppendDataBoundItems="true" runat="server" Width="380px" AutoPostBack="True" OnSelectedIndexChanged="frm2_option_degree_SelectedIndexChanged">                                                         
</asp:DropDownList>
     <sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator4" runat="server" ControlToValidate="frm2_option_degree"
ErrorMessage="You must select a Degree" InitialValue="0" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator>
<asp:TextBox ID="frm2_degree" runat="server" Visible="False" Width="350px" 
         MaxLength="255"></asp:TextBox>
<sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator5" runat="server" ControlToValidate="frm2_degree"
ErrorMessage="You must enter a Education program" ValidationGroup="frm2_group2">*</sv:RequiredFieldValidator>
 </td>
 </tr>
 <tr>
 <td style="width: 279px">
 City: <span style="color:Red;">*</span><br /><asp:TextBox ID="frm2_city" 
         runat="server" Width="220px" MaxLength="50" ></asp:TextBox>
     <sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator8" runat="server" ControlToValidate="frm2_city" ErrorMessage="You must select a City" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator><br />
     <br />
  State/Province:<br /><asp:TextBox ID="frm2_state" runat="server" Width="220px" 
         TextMode="MultiLine" MaxLength="50" ></asp:TextBox>
 </td> 
 <td style="padding-right: 0px; padding-left: 0px" >
 <table cellpadding="0" align="center" border="0" style="width: 422px; height: 121px"> 
 <tr>
 <td style="width: 132px; vertical-align: top; text-align: left;" rowspan="2">
  Dates Attended: <span style="color:Red;">*</span>
  <br /> 
   Start :<asp:DropDownList ID="frm2_start_year" runat="server" Width="58px" 
         onselectedindexchanged="frm2_start_year_SelectedIndexChanged" 
         AutoPostBack="True"></asp:DropDownList>
     <sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator2" runat="server" ControlToValidate="frm2_start_year" ErrorMessage="You must select Start Year" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator>
     <br />
     <br />
      End &nbsp; :<asp:DropDownList ID="frm2_end_year" runat="server" Width="58px"></asp:DropDownList>
     <sv:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="frm2_end_year" ErrorMessage="You must select End Year" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator>
        <sv:CompareValidator ID="frm2_CompareValidator1" runat="server" 
         ControlToCompare="frm2_start_year" ControlToValidate="frm2_end_year" 
         ErrorMessage="Select a Valid End Year" Operator="GreaterThanEqual" 
         ValidationGroup="frm2_group">*</sv:CompareValidator>&nbsp;
    <br />  
     </td>  
 <td style="vertical-align: top; text-align: left; height: 21%;">
     Did you graduate?:<br />
     <asp:DropDownList ID="frm2_option_graduate" runat="server" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="frm2_option_graduate_SelectedIndexChanged">
<asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
<asp:ListItem Value="True">Yes</asp:ListItem>
<asp:ListItem Value="False">No</asp:ListItem>
</asp:DropDownList>
<sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator6" runat="server" ControlToValidate="frm2_option_graduate"
    ErrorMessage="You must select Did you graduate" InitialValue="0" ValidationGroup="frm2_group">*</sv:RequiredFieldValidator>
     </td>
             </tr>
 <tr>
<td id="frm2_optional" runat="server" visible="false" style="vertical-align: top; text-align: left; height: 46%;" >
Date of graduation: (mm/dd/yyyy<span style="color: #ff0000">*</span>)<br />
                            <span style="color:Red;"></span>                
                <asp:DropDownList ID="frm2_month" runat="server" Width="54px">               
                </asp:DropDownList> / 
                <asp:DropDownList ID="frm2_date" runat="server" Width="54px">                  
                </asp:DropDownList> / 
                <asp:DropDownList ID="frm2_year" runat="server" Width="54px">               
                </asp:DropDownList>          
                            <sv:RequiredFieldValidator ID="frm2_RequiredFieldValidator7" runat="server" ControlToValidate="frm2_year"
                                ErrorMessage="You must select Year of graduation" ValidationGroup="frm2_group3">*</sv:RequiredFieldValidator>
                                <br />
                              <span style="color:Red;">Note:</span>  Only year is required
</td>
     </tr> 
</table> 
</td> 
 </tr> 
<tr>
<td colspan="2" >
<br />
</td>
</tr>
<tr> 
<td colspan="2"  align="center"> 
<asp:Button ID="frm2_btn_clear" runat="server" Text="Clear" OnClick="frm2_btn_clear_Click" CssClass="btncolor"/>&nbsp;&nbsp;<asp:Button ID="frm2_btn_submit"
runat="server" Text="Submit" OnClick="frm2_btn_submit_Click" ValidationGroup="frm2_group" CausesValidation="False" CssClass="btncolor"/>
</td> 
</tr> 
</table> 
</td></tr>
</table>
</td>
</tr>
<tr  id="frm2_details1" runat="server" visible="false">
<td class="warning_box">
Be sure to 'Submit' any data you enter before continuing.This section is for high school education ONLY. When you are done entering your high school education in this section, click Continue.
</td>
</tr>
<tr id="frm2_details2" runat="server" visible="false">
<td>
<sv:ValidationSummary id="frm2_summary" CssClass="error_box_summary"  runat="server"  ValidationGroup="frm2_group" /><sv:ValidationSummary id="frm2_summary1" runat="server" CssClass="error_box_summary" ValidationGroup="frm2_group1" />
<sv:ValidationSummary id="frm2_summary2" CssClass="error_box_summary" runat="server" ValidationGroup="frm2_group2" /><sv:ValidationSummary id="frm2_summary3" runat="server" CssClass="error_box_summary" ValidationGroup="frm2_group3"/>
 </td> 
	</tr>
	<tr>
	<td><br /></td>
	</tr>
	   <tr>
<td style="height: 24px"> 
<div>
<table width="100%" >
<tr>
<td ><asp:ImageButton ID="frm2_btn_previous" OnClick="frm2_btn_previous_Click" 
        runat="server" ImageUrl="~/images/l-arrow1.png" 
        ImageAlign="Baseline" /></td>
<td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
    Go Back to Step 2:<br />
    <span style="color:#AEAEAE; font-style: italic;">Purpose</span> </td>
<td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
    Continue to Step 4:<br />
   <span style="color:#AEAEAE; font-style: italic;">Post - Secondary History</span> </td>
<td><asp:ImageButton ID="frm2_btn_continue" OnClick="frm2_Btn_continue_Click"  
        runat="server" ImageUrl="~/images/r-arrow1.png" 
        ImageAlign="Baseline" /></td>
</tr>
</table>
</div> 
</td>
</tr>  	
</table>
 </asp:View> 
<asp:View ID="Educational_University" runat="server">    
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
    <table width="100%"> 	
    <tr>
 <td>
  <div class="headertag">Instructions</div>
  <br />
 <table width="100%">
<tr>
<td class="warning_box_msg">
<div id="frm3_msginfo" runat="server"  ></div>
</td>
</tr>
</table>
</td> 
 </tr>
	<tr id="frm3_display" runat="server" visible="false">
	<td>
	<br />
	 <div class="headertag">My Higher Education</div>
	 <br />
<asp:GridView ID="Universitygrid" runat="server" AutoGenerateColumns="False" OnLoad="Universitygrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
              <EmptyDataTemplate>
                   No University History entered
              </EmptyDataTemplate>
              <Columns>
                  <asp:BoundField DataField="Name" HeaderText="Institution" SortExpression="Name" />
                  <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                  <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                  <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
              </Columns>                
          </asp:GridView>         
      </td> 
  </tr>
  <tr>
  <td>
  <br /> 
    <div class="headertag">Higher Education (also called college or university education)</div>
    <br />
 <table class="search_css" >
<tr> 
						<td>Country of Study:&nbsp;<span style="color:Red;">*</span>
                            <asp:DropDownList ID="frm3_opt_country" runat="server" Width="204px" AutoPostBack="True" OnSelectedIndexChanged="frm3_opt_country_SelectedIndexChanged" AppendDataBoundItems="True">                            
                            </asp:DropDownList></td> 
					</tr> 
<tr  id="frm3_details" runat="server" visible="false">
<td>

<table> 		     
<tr>
<td style="width: 293px">
Name of Institution: <span style="color:Red;">*</span><br />
<asp:TextBox runat="server" ID="frm3_institution" Width="220px" autocomplete="off" 
        MaxLength="180" />
    <sv:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="frm3_institution" ErrorMessage="You must Enter an Institution" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator>
						   <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="frm3_AutoCompleteExtender1" 
                TargetControlID="frm3_institution"
                ServicePath="University.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="1" 
                CompletionInterval="30"
                 EnableCaching="false"
                CompletionSetCount="20"
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"                
                ContextKey="CountryId" UseContextKey="True"><Animations>
                    <OnShow>
                        <Sequence>
                            
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide></Animations></ajaxToolkit:AutoCompleteExtender>
                        
</td>
<td>
Education Program:<br /><asp:DropDownList ID="frm3_option_degree" runat="server" Width="359px" AppendDataBoundItems ="true" AutoPostBack="True" OnSelectedIndexChanged="frm3_option_degree_SelectedIndexChanged">                  
</asp:DropDownList>
<sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator6" runat="server" ControlToValidate="frm3_option_degree"
ErrorMessage="You must select a Degree" InitialValue="0" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator>
    <asp:TextBox
ID="frm3_degree" runat="server" Visible="False" Width="338px" MaxLength="255"></asp:TextBox>
    <sv:RequiredFieldValidator
ID="frm3_RequiredFieldValidator11" runat="server" ControlToValidate="frm3_degree" 
        ErrorMessage="You must enter a Education program" ValidationGroup="frm3_group2">*</sv:RequiredFieldValidator>
</td>
</tr>
<tr>
<td style="width: 279px">
City: <span style="color:Red;">*</span><br /> 
                         <asp:TextBox ID="frm3_city" runat="server" Width="220px" 
        MaxLength="50" ></asp:TextBox>
                            <sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator2" runat="server" ControlToValidate="frm3_city"
                                ErrorMessage="You must select a City" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator>
</td>
<td>
Major:<br /><asp:DropDownList ID="frm3_option_major" runat="server" Width="110px" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="frm3_option_major_SelectedIndexChanged">                                       
</asp:DropDownList>
<sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator7" runat="server" ControlToValidate="frm3_option_major"
ErrorMessage="You must select a Major" InitialValue="0" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator>
    <asp:TextBox
ID="frm3_major" runat="server" Visible="False" MaxLength="50"></asp:TextBox><sv:RequiredFieldValidator
ID="frm3_RequiredFieldValidator12" runat="server" ControlToValidate="frm3_major" ErrorMessage="You must enter a Major" ValidationGroup="frm3_group3">*</sv:RequiredFieldValidator>
</td>
</tr>
<tr>
<td style="width: 293px">
 State/Province:<br /><asp:TextBox ID="frm3_state" runat="server" Width="220px" 
        TextMode="MultiLine" Height="47px" MaxLength="50" ></asp:TextBox>
</td>
<td style="padding-right: 0px; padding-left: 0px; border-top-style: none; border-right-style: none; border-left-style: none; border-collapse: collapse; border-bottom-style: none">

 <table cellpadding="0" align="center" border="0" style="width: 422px; height: 121px"> 
 <tr>
 <td style="width: 123px; vertical-align: top; text-align: left;" rowspan="2">
 Dates Attended: <span style="color:Red;">*</span>
 <br /> 
 Start :<asp:DropDownList ID="frm3_start_year" runat="server" Width="58px" 
         AutoPostBack="True" 
         onselectedindexchanged="frm3_start_year_SelectedIndexChanged"></asp:DropDownList>
    <sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator4" runat="server" ControlToValidate="frm3_start_year" ErrorMessage="You must select Start Year" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator> 
     <br />
     <br />
 End &nbsp; :<asp:DropDownList ID="frm3_end_year" runat="server" Width="58px"></asp:DropDownList>
    <sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator5" runat="server" ControlToValidate="frm3_end_year" ErrorMessage="You must select End Year" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator>
<sv:CompareValidator ID="frm3_CompareValidator1" runat="server" 
         ControlToCompare="frm3_start_year" ControlToValidate="frm3_end_year" 
         ErrorMessage="Select a Valid End Year" Operator="GreaterThanEqual" 
         ValidationGroup="frm3_group">*</sv:CompareValidator> 
 </td>
 <td style="vertical-align: top; text-align: left; height: 32%;">
 Did you graduate?: 
     <br />
     <asp:DropDownList ID="frm3_option_graduate" runat="server" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="frm3_option_graduate_SelectedIndexChanged">
<asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
<asp:ListItem Value="True">Yes</asp:ListItem>
<asp:ListItem Value="False">No</asp:ListItem>
</asp:DropDownList>
<sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator8" runat="server" ControlToValidate="frm3_option_graduate" ErrorMessage="You must select Did you graduate" InitialValue="0" ValidationGroup="frm3_group">*</sv:RequiredFieldValidator> 
 </td>
 </tr>
 <tr>
 <td id="frm3_optional" runat="server" visible="false" style="vertical-align: top; text-align: left; height: 59%;">
Date of graduation: (mm/dd/yyyy<span style="color: #ff0000">*</span>)<span style="color:Red;"></span>               
    <br />
<asp:DropDownList ID="frm3_month" runat="server" Width="54px"></asp:DropDownList> / 
<asp:DropDownList ID="frm3_date" runat="server" Width="54px"></asp:DropDownList> / 
<asp:DropDownList ID="frm3_year" runat="server" Width="54px"></asp:DropDownList>          
<sv:RequiredFieldValidator ID="frm3_RequiredFieldValidator9" runat="server" ControlToValidate="frm3_year" ErrorMessage="You must select Year of graduation" ValidationGroup="frm3_group4">*</sv:RequiredFieldValidator> 
   <br />
          <span style="color:Red;">Note:</span>  Only year is required
 </td>
</tr>
</table>
</td> 
</tr>  
	<tr>
<td colspan="2" >
<br />
</td>
</tr>				
<tr> 
	<td colspan="2" align="center"> 
        <asp:Button ID="frm3_btn_clear" runat="server" Text="Clear" OnClick="frm3_btn_clear_Click" CssClass="btncolor" />&nbsp;&nbsp;<asp:Button ID="frm3_btn_submit"
            runat="server" Text="Submit" OnClick="frm3_btn_submit_Click" ValidationGroup="frm3_group" CausesValidation="False" CssClass="btncolor" />
  </td> 
</tr> 
</table>
</td>
</tr>
</table>
	</td> 
	</tr>
	<tr  id="frm3_details1" runat="server" visible="false">
<td class="warning_box">
Be sure to 'Submit' any data you enter before continuing.
</td>
</tr>
<tr id="frm3_details2" runat="server" visible="false">
<td>				
<sv:ValidationSummary id="frm3_summary" runat="server" CssClass="error_box_summary"  ValidationGroup="frm3_group" ></sv:ValidationSummary><sv:ValidationSummary id="frm3_summary1" runat="server" CssClass="error_box_summary"  ValidationGroup="frm3_group1" ></sv:ValidationSummary>
<sv:ValidationSummary ID="frm3_summary2" runat="server" ValidationGroup="frm3_group2" CssClass="error_box_summary" ></sv:ValidationSummary>
<sv:ValidationSummary ID="frm3_summary3" runat="server" ValidationGroup="frm3_group3" CssClass="error_box_summary"  ></sv:ValidationSummary><sv:ValidationSummary ID="frm3_summary4" runat="server" ValidationGroup="frm3_group4"  CssClass="error_box_summary"></sv:ValidationSummary>            

</td> 
</tr> 
	  <tr>
<td style="height: 24px"> 
<div>
<table width="100%">
<tr><td ></td></tr>
<tr>
<td ><asp:ImageButton ID="frm3_btn_previous" OnClick="frm3_btn_previous_Click" 
        runat="server" ImageUrl="~/images/l-arrow1.png" 
        ImageAlign="Baseline" /></td>
<td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
    Go Back to Step 3:<br />
    <span style="color:#AEAEAE; font-style: italic;">Upper - Secondary History</span> </td>
<td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
    Continue to Step 5:<br />
   <span style="color:#AEAEAE; font-style: italic;">Delivery Instructions</span> </td>
<td><asp:ImageButton ID="frm3_btn_continue" OnClick="frm3_Btn_continue_Click"  
        runat="server" ImageUrl="~/images/r-arrow1.png" 
        ImageAlign="Baseline" /></td>
</tr>
</table>   
</div> 
</td>
</tr>       
</table>
 </asp:View>
<asp:View ID="Purpose" runat="server">
 <div id="asteriskalert" runat="server" visible="false" style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
    <table width="100%" > 
        <tr>
        <td>       
        <br />
         <div class="headertag">Purpose</div>
         <br />
           <table class="search_css">          
            <tr> 
               <td> 
                    What is the purpose of this service : <span  id="asterik" runat="server" visible="false" style="color:Red;">*</span><asp:DropDownList
                        ID="frm4_option_purpose" runat="server" Width="251px" AutoPostBack="True" OnSelectedIndexChanged="frm4_option_purpose_SelectedIndexChanged" AppendDataBoundItems="true" ValidationGroup="frm4_group" >
                        <asp:ListItem Value="0" >Select</asp:ListItem>  
                    </asp:DropDownList>                    
                   <asp:Label ID="frm4_opPurpose" runat="server" Visible="false"  Text=""></asp:Label>                   
                   <sv:RequiredFieldValidator ID="frm4_RequiredFieldValidator1" runat="server" ControlToValidate="frm4_option_purpose"
                       ErrorMessage="You must select a purpose" InitialValue="0" ValidationGroup="frm4_group">*</sv:RequiredFieldValidator></td>
               </tr> 
               <tr id="frm4_optional" runat="server" visible="false">
               <td style="text-align: left" >
                  <%-- Note: Enter Name if you are applying for only one Instiution--%>Which educational institution referred you to us?<br />
                   <br />
                  <%-- Name of Institution applying to:--%><asp:TextBox ID="frm4_institution" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td>
               </tr> 
                <tr id="frm4_optional1" runat="server" visible="false">
               <td >
               <%-- Note: Enter Name if you are applying for only one Organization--%>Which employer referred you to us?<br />
                <br />
               <%--Name of Organization applying to--%><asp:TextBox ID="frm4_organization" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr> 
                <tr id="frm4_optional2" runat="server" visible="false">
               <td >
                <%--Note: Enter Name if you are applying for only one Attorney or Law firm--%>Which Attorney or Law firm referred you to us?<br />
                   <br />
               <%--Name of Attorney or Law firm referred you to us:--%><asp:TextBox ID="frm4_lawfirm" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                <tr id="frm4_optional3" runat="server" visible="false">
               <td >
                Note: Enter Name if you are applying for only one Board<br />
                   <br />
               Name of Board from which you seek licensing: <br /><asp:TextBox ID="frm4_board" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>&nbsp;state:<asp:TextBox 
                       ID="frm4_state" runat="server" Width="100px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                <tr id="frm4_optional4" runat="server" visible="false">
               <td >
               <%-- Note: Enter Name if you are applying for only one Military Recruiter--%>which Military Recruiter referred you to us?<br />
                   <br />
               <%--Name of Military Recruiter who referred you to us:--%><asp:TextBox ID="frm4_military" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                 <tr id="frm4_optional5" runat="server" visible="false">
               <td >
              <%--  Note: Enter Purpose of your Evaluation--%>How did you hear about us?<br />
                   <br />
               <%--How are you going to use your Evaluation:--%><asp:TextBox ID="frm4_evaluation" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>               
               </td>               
               </tr>                        
        </table>  
        <sv:ValidationSummary id="frm4_Summary" runat="server" CssClass="error_box_summary" ValidationGroup="frm4_group"  />      
        </td> 
        </tr>	
        <tr id="frm4_optional6" runat="server" >
        <td>
        <br />
           <div class="headertag">Document Requirements</div>
           <br />
        <table class="search_css"  > 
           
            <tr>
               <td>                        
               1)&nbsp;&nbsp;General Document Submission Instructions(<a id="frm4_instruction"  title="Document Instructions :: Information Block" class='iframe' runat="server" href="#">View</a>)<br /><br />           
               <asp:CheckBox ID="frm4_agree" runat="server" Text="I have reviewed and understood the Document Requirements outlined above." ValidationGroup="frm4_group1" />
                   </td>
               </tr>   
            </table>             
        </td>
        </tr>
        <tr>
<td style="height: 24px"> 
<div>
<table width="100%">
<tr><td class="style3"></td></tr>
<tr>
<td ><asp:ImageButton ID="frm4_btn_previous" OnClick="frm4_btn_previous_Click" 
        runat="server" ImageUrl="~/images/l-arrow1.png" 
        ImageAlign="Baseline" /></td>
<td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
    Go Back to Step 1:<br />
    <span style="color:#AEAEAE; font-style: italic;">Personal Information</span> </td>
<td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
    Continue to Step 3:<br />
   <span style="color:#AEAEAE; font-style: italic;">Upper - Secondary History</span> </td>
<td><asp:ImageButton ID="frm4_btn_continue" OnClick="frm4_Btn_continue_Click"  
        runat="server" ImageUrl="~/images/r-arrow1.png" 
        ImageAlign="Baseline" /></td>
</tr>
</table>
</div> 
</td>
</tr>                    
	</table>     
</asp:View>
<asp:View ID="Mailing_instructions" runat="server">
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
    <table width="100%"> 
        <tr>
        <td>       	
            <br /> 
               <div class="headertag">Delivery Options and Fees</div>
               <br />
            <table width="100%"> 
                <tr> 
                    <td>
                     <table class="search_css"> 
        <tr>
        <td  id="frm5_msginfo" runat="server" > </td> 
        </tr> 
        </table>                    
                    <br />                       
                        <asp:GridView ID="Grid_Mailcost" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnLoad="Grid_Mailcost_Load"  CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>                               
                                 <asp:TemplateField ShowHeader="False" SortExpression="Name">                           
                           <ItemTemplate>                                                           
                              <asp:Label ID="Label20" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                           </ItemTemplate>                
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <center>
                               <a id="link" href='<%# Eval("id", "mail_description.aspx?cus={0}") %>' class='iframe'  title="Description :: Information Block">Description</a>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>                      
                       <asp:TemplateField ShowHeader="False" SortExpression="Cost">                         
                           <ItemTemplate>
                           <center>
                               <asp:Label ID="Label19" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>                                                              
                            </Columns>
                            <EmptyDataTemplate>
                                No Basic Service Type Available
                            </EmptyDataTemplate>
                        </asp:GridView>
                </td>
                </tr>
            </table> 
        </td>
        </tr>  
<tr id="frm5_optional" runat="server" visible="true">
        <td>
            <br /> 
            <div class="headertag">Delivery (as part of your order)</div>
             <br />
             <br />               
                        <table width="100%">
                            <tr ID="frm5_evalgridblock" runat="server" visible="false">
                                <td>
                                    <asp:GridView ID="officialgrid" runat="server" 
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                        CssClass="gridview_css" OnLoad="officialgrid_Load" PagerStyle-CssClass="pgr" 
                                        style="TEXT-ALIGN: center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="Official Hard Copy"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Recipient" HeaderText="Recipient" ReadOnly="True" 
                                                SortExpression="Recipient" />
                                            <asp:BoundField DataField="Count" HeaderText="No Of Copies" 
                                                SortExpression="Count" />
                                            <asp:BoundField DataField="Name" HeaderText="Delivery Type" 
                                                SortExpression="Name" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Official Hard Copy Requested
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
               
               <table class="search_css"> 
               <tr>
               <td>  
                   <asp:Label ID="lbldelicopy" runat="server" ></asp:Label>                   
                  <table id="frm5_primaryform" runat= "server" width="100%">  
                  <tr><td colspan="2" ></td></tr>                                          
                      <tr>                   
                     <td style="border:solid 1px red;">
                    How do you want this copy delivered?:<span style="color:Red;">*</span><br /> 
                        <asp:DropDownList ID="frm5_pdelivery" runat="server" Width="135px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="frm5_pdelivery"
                            ErrorMessage="You must select Delivery Type" InitialValue="0" 
                            ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td></td>
                  </tr>                                       
                      <tr>                   
                <td colspan="2"> 
                Name: <span style="color:Red;">*</span><br /> 
                    <asp:TextBox ID="frm5_pname" runat="server" Width="313px" MaxLength="50" ></asp:TextBox>
                    <sv:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="frm5_pstate" ErrorMessage="You must enter Name" 
                        ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>                         
                          </td>               
                    </tr>                    
                    <tr>
                    <td>
                    Address Line 1: <span style="color:Red">*</span><br /> 
                    <asp:TextBox ID="frm5_padd1" runat="server" Width="208px" MaxLength="100" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="frm5_pcity"
                            ErrorMessage="You must enter your Address" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td rowspan="2">
                    State/Province:<span style="color:Red;">*</span><br /> 
                       <asp:TextBox ID="frm5_pstate" runat="server" Width="129px" TextMode="MultiLine" 
                            Height="64px" MaxLength="50" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="frm5_pstate"
                            ErrorMessage="You must enter state" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td>
                    Country: <span style="color:Red">*</span><br /> 
                        <asp:DropDownList ID="frm5_pcountry" runat="server" Width="204px" 
                            AppendDataBoundItems="True">
                        </asp:DropDownList>          
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="frm5_pcountry"
                            ErrorMessage="You must select Country" InitialValue="0" 
                            ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    Address Line 2:<br />
                        <asp:TextBox ID="frm5_padd2" runat="server" Width="208px" 
                            MaxLength="100" ></asp:TextBox>
                    </td>
                 <td></td>
                    </tr>
                    <tr>
                    <td>
                    City:<span style="color:Red;">*</span><br /> 
                        <asp:TextBox ID="frm5_pcity" runat="server" Width="129px" MaxLength="50" ></asp:TextBox>
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="frm5_pcity"
                            ErrorMessage="You must enter city" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>
                    <td>
                    postal/Zip Code:<span style="color:Red;">*</span><br /> 
                     <asp:TextBox ID="frm5_pzip" runat="server" Width="129px" MaxLength="50" ></asp:TextBox> 
                        <sv:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="frm5_pzip"
                            ErrorMessage="You must enter zipcode" ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                    </td>                 
                    </tr>                
                     <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_pinst" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>               
               <tr>
						<td colspan="3" align="center"> 
                            <asp:Button ID="frm5_primaryclear" runat="server" 
                                Text="Clear" OnClick="frm5_primaryclear_Click" CssClass="btncolor" 
                                CausesValidation="False" />&nbsp;&nbsp;<asp:Button ID="frm5_primarysubmit"
                                runat="server" Text="Submit" OnClick="frm5_primarysubmit_Click" 
                                ValidationGroup="frm5_group3" CausesValidation="False" 
                                CssClass="btncolor" />
                        </td> 
					</tr>  								
                    </table>  
               </td></tr>
                <tr id="frm5_primarysuccessmsg" runat= "server" visible="false"><td>
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
                                   <asp:Label ID="Label3" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />
                                   <asp:Label ID="Label21" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                   <asp:Label ID="Label13" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                   <br />
                                   <asp:Label ID="Label14" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                   <asp:Label ID="Label15" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label><br />
                                   <asp:Label ID="Label16" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>                      
                       </Fields>                       
                   </asp:DetailsView>
                </td></tr>                
                </table>              
       </td>
       </tr>
       <tr  id="frm5_primarymsg" runat="server" visible="True">
<td class="warning_box">
Be sure to 'Submit' any data you enter before continuing.
</td>
</tr>
<tr> 
<td>
<sv:ValidationSummary id="frm5_psummary" runat="server" ValidationGroup="frm5_group3" CssClass="error_box_summary"></sv:ValidationSummary>
</td>
</tr>
<tr>
<td> 
 
<div id="dynamic_official" runat="server" class="infobox">
<div id="frm5_overlap" runat="server" class="more">
<div align="center"  style="position:absolute; top:60%; height:100%; width:100%;margin-top:-5em;" >
<table><tr><td><img src="Code/menu/warning.png"/> 
</td><td id="lbldelialert" runat="server"  style="vertical-align:middle;">Fill In the Official Hard Copy Delivery-1:(primary mailing address) for primary mailing address.</td></tr></table>
</div></div>
</div> 
   </td>  
</tr>

       <%--<tr ID="frm5_optional8" runat="server" visible="false"><td>
           <br />
           <div class="headertag">
               Additonal Evaluation Copy</div>
           <br />
           <table class="search_css">
               <tr>
                   <td>
                       <asp:RadioButtonList ID="frm5_evalradio" runat="server" AutoPostBack="True" 
                           OnSelectedIndexChanged="frm5_evalradio_SelectedIndexChanged">
                           <asp:ListItem Value="False">Please send my Official Hard Copy to my primary mailing address</asp:ListItem>
                           <asp:ListItem Value="True">Please send this Official Hard Copy to a separate address.</asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
               </tr>
           </table>
       </td></tr>--%>
        <tr id="frm5_evalform" runat="server" visible="false">
       <td>
       <br />
        <div class="headertag">Delivery Address Information&nbsp;<span id="frm5_evalformheader" runat="server"></span></div>
        <br />
        <table class="search_css" > 
                    <tr> 
                    <td>
                        <table width="100%">                           
                            <tr>
                                <td colspan="2">
                                    How do you want this copy delivered?:<span style="color:Red;">*</span><br />
                                    <asp:DropDownList ID="frm5_deliverytypeeval" runat="server" 
                                        AppendDataBoundItems="true" Width="135px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator7" runat="server" 
                                        ControlToValidate="frm5_deliverytypeeval" 
                                        ErrorMessage="You must select Delivery Type" InitialValue="0" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                   
                                </td>
                        </tr>                        
                            <tr>
                                <td colspan="2">
                                    Name: <span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_Fnameeval" runat="server" MaxLength="50" Width="313px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="frm5_Fnameeval" ErrorMessage="You must enter Name" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                  
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 1: <span style="color:Red">*</span><br />
                                    <asp:TextBox ID="frm5_add1eval" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="frm5_add1eval" ErrorMessage="You must enter your Address" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                    
                                </td>
                                <td rowspan="2">
                                    State/Province:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_stateeval" runat="server" Height="64px" MaxLength="50" 
                                        TextMode="MultiLine" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="frm5_stateeval" ErrorMessage="You must enter state" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                   
                                </td>
                                <td>
                                    Country: <span style="color:Red">*</span><br />
                                    <asp:DropDownList ID="frm5_countryeval" runat="server" AppendDataBoundItems="True" 
                                        Width="204px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="frm5_countryeval" ErrorMessage="You must select Country" 
                                        InitialValue="0" ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 2:<br />
                                    <asp:TextBox ID="frm5_add2eval" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                </td>
                             <td></td>
                            </tr>
                            <tr>
                                <td>
                                    City:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_cityeval" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="frm5_cityeval" ErrorMessage="You must enter city" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                    
                                </td>
                                <td>
                                    postal/Zip Code:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_zipeval" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="frm5_RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="frm5_zipeval" ErrorMessage="You must enter zipcode" 
                                        ValidationGroup="frm5_group">*</sv:RequiredFieldValidator>
                                   
                                </td>
                                <td>
                                    <asp:Label ID="frm5_hiddenvalue" runat="server" Visible="False"></asp:Label>                                   
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_instname" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                </td>
                                </tr>
                                <tr>
                                <td align="center" colspan="3">
                                 <asp:Button ID="frm5_btn_cleareval" runat="server" CssClass="btncolor" 
                                        OnClick="frm5_btn_cleareval_Click" Text="Clear" />
                                    &nbsp;&nbsp;<asp:Button ID="frm5_btn_submiteval" runat="server" CausesValidation="False" 
                                        CssClass="btncolor" OnClick="frm5_btn_submiteval_Click" Text="Submit" 
                                        ValidationGroup="frm5_group" />
                                </td>
                            </tr>                           
                        </table>
                        </td>                    
                </tr>
                </table> 
                </td>
       </tr>
   
       <tr  id="frm5_evalwarning" runat="server" visible="false">
<td class="warning_box">
Be sure to 'Submit' any data you enter before continuing.
</td>
 </tr>
 <tr><td>
  <sv:ValidationSummary ID="frm5_Summary" runat="server" CssClass="error_box_summary" 
                                        ValidationGroup="frm5_group" />                                    
 </td></tr>
 <tr id="frm5_Additionalblock" runat="server" visible="true">
 <td>
 <div id="frm5_addlgridblock" runat="server" visible="true">
   <br /> 
               <div class="headertag">Purchase an additional Official Hard Copy(+$<asp:Label 
                       ID="additionalcost" runat="server" Text="0"></asp:Label>
                   )</div>  
               <br />
                    <div class="warning_box_msg">
                        If you would like to request an Additional Official Hard Copy at this time, please fill in the 
                        section below.</div>
                <br />
                <asp:GridView ID="addevalgrid" runat="server" 
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                    CssClass="gridview_css" OnLoad="addevalgrid_Load" PagerStyle-CssClass="pgr" 
                    style="TEXT-ALIGN: center">
                    <Columns>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text="Official Hard Copy"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Recipient" HeaderText="Recipient" ReadOnly="True" 
                            SortExpression="Recipient" />
                        <asp:BoundField DataField="Count" HeaderText="No Of Copies" 
                            SortExpression="Count" />
                        <asp:BoundField DataField="Name" HeaderText="Delivery Type" 
                            SortExpression="Name" />
                    </Columns>
                    <EmptyDataTemplate>
                        No Additional Official Hard Copy Requested
                    </EmptyDataTemplate>
                </asp:GridView>
                <br />
 </div>
 <div>
   <table width="100%">
                   <tr ID="frm5_Additionalrequestradiobtn" runat="server" visible="false"><td>
           <br />
           <div class="headertag">Select Delivery Address Information ( Additional Official Hard Copy Requested )</div>
           <br />
           <table class="search_css">
               <tr>
                   <td>
                       <asp:RadioButtonList ID="frm5_addlradiobtn" runat="server" AutoPostBack="True" 
                           OnSelectedIndexChanged="frm5_addlradiobtn_SelectedIndexChanged">
                           <asp:ListItem Value="False">Please send my Official Hard Copy to my primary mailing address</asp:ListItem>
                           <asp:ListItem Value="True">Please send this Official Hard Copy to a separate address.</asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
               </tr>
           </table>
       </td></tr>
                <tr id="frm5_Additionalrequestform" runat="server" visible="false">
       <td>
       <br />
        <div class="headertag">Fill in Delivery Address Information ( Additional Official Hard Copy Requested )</div>
        <br />
        <table class="search_css" > 
                    <tr> 
                    <td>
                        <table width="100%">
                            <tr ID="Tr1" runat="server" visible="false">
                                <td align="center" colspan="3">
                                    Note:Selected No of copies will be sent to the below Address</td>
                            </tr>
                            <tr ID="Tr2" runat="server" visible="true">
                                <td>
                                   <b>Please indicate the number of additional copies to be delivered to this address:</b><asp:DropDownList ID="frm5_copies_addl" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                        ControlToValidate="frm5_copies_addl" ErrorMessage="You must select No of Copies" 
                                        InitialValue="0" ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                              <td id="dtype" runat="server" colspan="2">
                                    How do you want this copy delivered?:<span style="color:Red;">*</span><br />
                                    <asp:DropDownList ID="frm5_deliverytypeaddl" runat="server" 
                                        AppendDataBoundItems="true" Width="135px">
                                    </asp:DropDownList>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                        ControlToValidate="frm5_deliverytypeaddl" 
                                        ErrorMessage="You must select Delivery Type" InitialValue="0" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Name: <span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_Fnameaddl" runat="server" MaxLength="50" Width="313px"></asp:TextBox>                               
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                        ControlToValidate="frm5_Fnameaddl" ErrorMessage="You must enter Name" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 1: <span style="color:Red">*</span><br />
                                    <asp:TextBox ID="frm5_add1addl" runat="server" MaxLength="100" Width="208px"></asp:TextBox>                                   
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                        ControlToValidate="frm5_add1addl" ErrorMessage="You must enter your Address" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td rowspan="2">
                                    State/Province:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_stateaddl" runat="server" Height="64px" MaxLength="50" 
                                        TextMode="MultiLine" Width="129px"></asp:TextBox>                                 
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                        ControlToValidate="frm5_stateaddl" ErrorMessage="You must enter state" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    Country: <span style="color:Red">*</span><br />
                                    <asp:DropDownList ID="frm5_countryaddl" runat="server" AppendDataBoundItems="True" 
                                        Width="204px">
                                    </asp:DropDownList>                                  
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                        ControlToValidate="frm5_countryaddl" ErrorMessage="You must select Country" 
                                        InitialValue="0" ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 2:<br />
                                    <asp:TextBox ID="frm5_add2addl" runat="server" MaxLength="100" Width="208px"></asp:TextBox>
                                </td>
                              <td></td>
                            </tr>
                            <tr>
                                <td>
                                    City:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_cityaddl" runat="server" MaxLength="50" Width="129px"></asp:TextBox>                                  
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                        ControlToValidate="frm5_cityaddl" ErrorMessage="You must enter city" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    postal/Zip Code:<span style="color:Red;">*</span><br />
                                    <asp:TextBox ID="frm5_zipaddl" runat="server" MaxLength="50" Width="129px"></asp:TextBox>
                                    <sv:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                        ControlToValidate="frm5_zipaddl" ErrorMessage="You must enter zipcode" 
                                        ValidationGroup="frm5_addlgroup">*</sv:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr visible="false">
                                <td colspan="3">
                                   Intended for Institution? Indicate name of Institution:(Optional)
                                </td>
                            </tr>
                            <tr visible="false">
                                <td colspan="3">
                                    <asp:TextBox ID="frm5_addlinstname" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                 <asp:Button ID="frm5_btn_canceladdl" runat="server" CssClass="btncolor" 
                                        OnClick="frm5_btn_canceladdl_Click" Text="Cancel" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="frm5_btn_submit1addl" runat="server" CausesValidation="False" 
                                        CssClass="btncolor" OnClick="frm5_btn_submit1addl_Click" Text="Submit" 
                                        ValidationGroup="frm5_addlgroup" />
                                </td>
                            </tr>  
                                                   
                        </table>
                        </td>                    
                </tr>
                      
                </table> 
                </td>
       </tr>
         <tr  id="frm5_addlwarning" runat="server" visible="false">
<td class="warning_box">
Be sure to 'Submit' any data you enter before continuing.
</td>
 </tr>
                <tr>
                <td>
                            <sv:ValidationSummary ID="frm5_Summary1" runat="server" CssClass="error_box_summary" ValidationGroup="frm5_addlgroup" />
                            </td>
                            </tr> 
                    <tr>
                        <td align="center">
                            <br />
                            <asp:Button ID="frm5_btn_requestcopy" runat="server" CausesValidation="False" 
                                CssClass="btncolor" OnClick="frm5_btn_requestcopy_Click" 
                                Text="Request additional Official Hard Copy" Enabled="False" />
                        </td>
                    </tr>
                </table>
 </div>
 </td> 
 </tr>
  <tr id="frm5_Emailblock" runat="server" >
  <td>
  <div  id="frm5_optional11" runat="server" visible="true">
   <br />
          <div class="headertag">
              Official Electronic Copies(+$<asp:Label ID="Emailcost" runat="server" Text="0"></asp:Label>
              )</div>
          <br />
          <div class="warning_box_msg">
              If you would like to request a Official Electronic Copy at this time, please fill in the section 
              below.</div>
          <br />
          <asp:GridView ID="Emailgrid" runat="server" AlternatingRowStyle-CssClass="alt" 
              AutoGenerateColumns="False" CssClass="gridview_css" OnLoad="Emailgrid_Load" 
              PagerStyle-CssClass="pgr" style="TEXT-ALIGN: center">
              <Columns>
                  <asp:TemplateField HeaderText="Type">
                      <ItemTemplate>
                          <asp:Label ID="Label4" runat="server" Text="Official Electronic Copy"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="Name" HeaderText="Recipient" />
                  <asp:BoundField DataField="Count" HeaderText="No Of Copies" />
              </Columns>
              <EmptyDataTemplate>
                  No Official Electronic Copy Requested
              </EmptyDataTemplate>
          </asp:GridView>
          <br />
          <table width="100%">
              <tr>
                  <td align="center">
                      <br />
                      <asp:Button ID="frm5_btn_emailcopy" runat="server" CausesValidation="False" 
                          CssClass="btncolor" OnClick="frm5_btn_emailcopy_Click" 
                          Text="Request Official Electronic Copy" />
                  </td>
              </tr>
          </table>
  </div>
  <div id="frm5_optional12" runat="server" visible="false">
    <br /> 
            <div class="headertag" ID="title_frm5_email" runat="server">Official Electronic Copies</div>  
            <br />
            <table class="search_css">             
                         <tr>
                         <td style="width: 340px"> 
                             <span ID="Span1" runat="server">Recipient's Name:</span><span 
                                 style="color: #ff0000">*</span>
                             <asp:TextBox ID="frm5_ename" runat="server" MaxLength="50" 
                                 ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                             <sv:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                                 ControlToValidate="frm5_ename" ErrorMessage="You must enter Recipient's Name" 
                                 ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                             &nbsp;<br /> 
                         </td>
                             <td>
                                 Email Address:<span style="color: #ff0000">*</span><asp:TextBox ID="frm5_email" 
                                     runat="server" MaxLength="50" ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                                 <sv:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                     ControlToValidate="frm5_email" ErrorMessage="You must enter Email Address" 
                                     ValidationGroup="frm5_group3">*</sv:RequiredFieldValidator>
                             </td>
                         </tr>                  
                         <tr>
                             <td colspan="2" style="text-align: center">
                                 <asp:Button ID="frm5_btn_cancelemail" runat="server" CssClass="btncolor" 
                                     OnClick="frm5_btn_cancelemail_Click" Text="Cancel" />
                                 &nbsp;&nbsp;
                                 <asp:Button ID="frm5_btn_submitemail" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="frm5_btn_submitemail_Click" Text="Submit" 
                                     ValidationGroup="frm5_group3" />
                             </td>
                         </tr>
             </table>  
  </div>
  <div id="frm5_optional13" runat="server" visible="false">
  <br />
   <div class="warning_box_elements">
                  Be sure to &#39;Submit&#39; any data you enter before continuing. 
                 </div>
  </div>
  </td>
  </tr>
  <tr id="frm5_faxblock" runat="server" >
  <td>
  <div  id="frm5_optional6" runat="server" visible="true">
   <br />
          <div class="headertag">
              Faxed Copies(+$<asp:Label ID="Faxcost" runat="server" Text="0"></asp:Label>
              )</div>
          <br />
          <div class="warning_box_msg">
              If you would like to request a Fax copy at this time, please fill in the section 
              below.</div>
          <br />
          <asp:GridView ID="faxgrid" runat="server" AlternatingRowStyle-CssClass="alt" 
              AutoGenerateColumns="False" CssClass="gridview_css" OnLoad="faxgrid_Load" 
              PagerStyle-CssClass="pgr" style="TEXT-ALIGN: center">
              <Columns>
                  <asp:TemplateField HeaderText="Type">
                      <ItemTemplate>
                          <asp:Label ID="Label4" runat="server" Text="Fax Copy"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="Name" HeaderText="Recipient" SortExpression="Name" />
                  <asp:BoundField DataField="Count" HeaderText="No Of Copies" 
                      SortExpression="Count" />
              </Columns>
              <EmptyDataTemplate>
                  No Fax Copy Requested
              </EmptyDataTemplate>
          </asp:GridView>
          <br />
          <table width="100%">
              <tr>
                  <td align="center">
                      <br />
                      <asp:Button ID="frm5_btn_faxcopy" runat="server" CausesValidation="False" 
                          CssClass="btncolor" OnClick="frm5_btn_faxcopy_Click" Text="Request Fax copy" />
                  </td>
              </tr>
          </table>
  </div>
  <div id="frm5_optional7" runat="server" visible="false">
    <br /> 
            <div class="headertag" ID="title_frm5_fax" runat="server">Fax Information</div>  
            <br />
            <table class="search_css">             
                         <tr>
                         <td style="width: 340px"> 
                             <span ID="title_frm5_fax1" runat="server">Fax Number:</span><span 
                                 style="color: #ff0000">*</span>
                             <asp:TextBox ID="frm5_faxno" runat="server" MaxLength="50" 
                                 ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                             <sv:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                 ControlToValidate="frm5_faxno" ErrorMessage="You must enter Fax Number" 
                                 ValidationGroup="frm5_group2">*</sv:RequiredFieldValidator>
                             &nbsp;<br /> 
                         </td>
                             <td>
                                 ATTN:<span style="color: #ff0000">*</span><asp:TextBox ID="frm5_attn" 
                                     runat="server" MaxLength="50" ValidationGroup="frm5_group2" Width="129px"></asp:TextBox>
                                 <sv:RequiredFieldValidator ID="frm5_reqvalidator2" runat="server" 
                                     ControlToValidate="frm5_attn" ErrorMessage="You must enter ATTN" 
                                     ValidationGroup="frm5_group2">*</sv:RequiredFieldValidator>
                             </td>
                         </tr>                  
                         <tr>
                             <td colspan="2" style="text-align: center">
                                 <asp:Button ID="frm5_btn_cancelfax" runat="server" CssClass="btncolor" 
                                     OnClick="frm5_btn_cancelfax_Click" Text="Cancel" />
                                 &nbsp;&nbsp;
                                 <asp:Button ID="frm5_btn_submitfax" runat="server" CausesValidation="False" 
                                     CssClass="btncolor" onclick="frm5_btn_submitfax_Click" Text="Submit" 
                                     ValidationGroup="frm5_group2" />
                             </td>
                         </tr>
             </table>  
  </div>
  <div id="frm5_optional10" runat="server" visible="false">
   <div class="warning_box_elements">
                  Be sure to &#39;Submit&#39; any data you enter before continuing. 
                 </div>
  </div>
  </td>
  </tr> 
  <tr>
<td>
    <sv:ValidationSummary ID="frm5_Summary2" runat="server" 
        CssClass="error_box_summary" EnableTheming="True" 
        ValidationGroup="frm5_group2" />
</td>
 </tr>
 <tr><td style="height: 24px">
     <div>
         <table width="100%">
             <tr>
                 <td class="style3">
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:ImageButton ID="frm5_btn_previous" runat="server" ImageAlign="Baseline" 
                         ImageUrl="~/images/l-arrow1.png" OnClick="frm5_btn_previous_Click" />
                 </td>
                 <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                     Go Back to Step 4:<br />
                     <span style="color:#AEAEAE; font-style: italic;">Post - Secondary History</span>
                 </td>
                 <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                     Continue to Step 6:<br />
                     <span style="color:#AEAEAE; font-style: italic;">Services &amp; Fees</span>
                 </td>
                 <td>
                     <asp:ImageButton ID="frm5_btn_continue" runat="server" ImageAlign="Baseline" 
                         ImageUrl="~/images/r-arrow1.png" OnClick="frm5_Btn_continue_Click" />
                 </td>
             </tr>
         </table>
     </div>
 </td></tr>
 
        </table> 
       
</asp:View>
<asp:View ID="Service" runat="server">
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
                                 <asp:DropDownList  ID="drpCount" Visible="false"  runat="server"></asp:DropDownList> 
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
                                   <asp:Label ID="lblType" runat="server" Visible="false"  Text='<%# Eval("Type") %>'></asp:Label>
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
                                    Go Back to Step 5:<br />
                                    <span style="color:#AEAEAE; font-style: italic;">Delivery Instructions</span></td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Continue to Step 7:<br />
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
<asp:View ID="Payment" runat="server">
 <div style="float:right;"><span style="color:Red;font-style:italic;font-weight:bold;">Note: </span><span style="font-style:italic;">Each field with an asterisk (<span style="color:Red;font-style:italic;font-weight:bold;"> * </span>) is required. All other fields are optional.</span></div>                  
    <table width="100%" > 
      <tr>
        <td>
        <br /> 
         <div class="headertag">General Service</div>
          <br />
          
<asp:GridView ID="paymentgrid" runat="server" AutoGenerateColumns="False" OnDataBound="paymentgrid_DataBound" ShowFooter="True" OnLoad="paymentgrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">              
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Service Type" SortExpression="Name" />
                    <asp:TemplateField HeaderText="Cost" SortExpression="Cost">                     
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("price", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                        <asp:Label ID="Label7" runat="server" Text="0" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>               
                <PagerStyle CssClass="pgr" />
                <EmptyDataTemplate>
                    No Service Requested
                </EmptyDataTemplate>              
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>  
   <br />    
   <div id="paysec_Additional" runat="server" >
             <div class="headertag">Additional Official Hard Copy Service</div>
             <br />
             
            <asp:GridView ID="addcopygrid" runat="server" AutoGenerateColumns="False"  OnDataBound="addcopygrid_DataBound" ShowFooter="True" OnLoad="addcopygrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                 <Columns>
                  <asp:TemplateField HeaderText="Service Type">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <%--   <asp:BoundField DataField="Type" HeaderText="Service Type" SortExpression="Type" />--%>
                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" SortExpression="Count">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Count") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost">
                        <FooterTemplate >
                            <asp:Label ID="Label11" runat="server" Text="0" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
                 <PagerStyle CssClass="pgr" />
                <EmptyDataTemplate>
                    No Additional Copies Requested
                </EmptyDataTemplate>              
                 <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>   
            </div>   
       <br />
        <div id="paysec_Email" runat="server" >
           <div class="headertag">Official Electronic Copy Service</div>
           <br />
<asp:GridView ID="email_csgrid" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="id" OnDataBound="email_csgrid_DataBound" 
                OnLoad="email_csgrid_Load" ShowFooter="True" style="TEXT-ALIGN: center" 
                CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt">
<Columns>                          
<asp:TemplateField HeaderText="Service Type" SortExpression="Type">
    <EditItemTemplate>
        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
         <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
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
<asp:TemplateField HeaderText="Cost" >
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
                    </div>         
         <br />
   <div id="paysec_Fax" runat="server" >
           <div class="headertag">Fax Copy Service</div>
           <br />
                              <asp:GridView ID="fax_csgrid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="fax_csgrid_DataBound" OnLoad="fax_csgrid_Load" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                           <Columns>                          
                                <asp:TemplateField HeaderText="Service Type" SortExpression="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
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
                                <asp:TemplateField HeaderText="Cost" >
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
                    </div>      
                  <br />
                  <div class="headertag">Delivery Service - Official Hard Copy(ies) &amp; Additional Copies</div>           
                  <br />

            <asp:GridView ID="DeliveryGrid" runat="server" AutoGenerateColumns="False" OnDataBound="DeliveryGrid_DataBound" ShowFooter="True" OnLoad="DeliveryGrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">                
                <PagerStyle CssClass="pgr" />
                <EmptyDataTemplate>
                    No Service Requested
                </EmptyDataTemplate>                
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Delivery Type" SortExpression="Name" />
                    <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Cost") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Service Type" SortExpression="Type" 
                        Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                            <asp:Label ID="Label12" runat="server" Text="Copy"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" SortExpression="Count" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Count") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                    <asp:Label ID="sqty" runat="server" Text="1"></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                        <asp:Label ID="Label8" runat="server" Text="0" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>                    
                </Columns>                   
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>            
          <br />
          <div align="right" class="valid_box_msg" >
         <b>Total Amount Due =</b><asp:Label ID="frm7_Amount" runat="server" Text="0" Font-Bold="True"></asp:Label>
         </div>
         <br />
          <br />
            <table class="search_css">
            <tr> 
               <td> Select payment method : <span style="color:Red;">*</span><asp:DropDownList
                        ID="frm7_payment" runat="server" Width="251px" AutoPostBack="True" OnSelectedIndexChanged="frm7_payment_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <sv:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="frm7_payment"
                            ErrorMessage="You must select a mode of payment" 
                       ValidationGroup="frm7_group1" InitialValue="Select">*</sv:RequiredFieldValidator>
                    <br />
                    <br />                  
               </td>
               </tr> 
            </table>
            </td>
            </tr>
            <tr id="frm7_optional" runat="server" visible="false">
            <td>
            <br />
             <div class="headertag">cashier's check/money order</div> 
             <br />                  
               <table width="100%" >
               <tr>
                <td id="mordertxt" runat="server" class="warning_box_msg" >
		        <%--<ul><li>You may send a money order with your application and documents.</li><li> Money orders are an acceptable payment for normal processing time orders.</li><li>If you requested any expedited service, please pay with a credit card</li></ul>--%>
		        </td>               
	            </tr>
</table>

            </td>
            </tr>
              <tr id="frm7_optional1" runat="server" visible="false">
            <td>
            <br />
              <div class="headertag">personal check</div> 
              <br />                           
               <table width="100%" >	           
		        <tr>
		        <td id="pchecktxt" runat="server" class="warning_box_msg">
		        <%--<ul><li>You may send a personal check with your application and documents.</li><li>Personal checks are an acceptable payment for normal processing time orders.</li><li>If you requested any expedited service, please pay with a credit card.</li></ul>--%>
		        </td> 
	</tr>
	</table>
	</td> 
	</tr> 
	<tr id="frm7_optional2" runat="server" visible="false">
            <td>
            <br />
            <div class="headertag">Credit Card</div>  
            <br />
               <table width="100%" >
	            <tr>
		        <td class="warning_box_msg">
		      <%--  <ul><li id="frm7_optional2_li1" runat="server"  >After printing the application summary/review, please fill out the credit card section by hand.</li>
		        <li id="frm7_optional2_li2" runat="server">You may also pay with a credit card over the phone.</li>
		        </ul>	--%>
		        <div id="creditcardtxt" runat="server"></div>
		        <div id="creditcardblk" runat="server"></div>	        
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
                                <td class="style3">
                                    <asp:Label ID="Filenumber" runat="server" Font-Bold="True" Text="Filenumber" 
                                        Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                </td>
                                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                                    </td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Continue <br />                                    
                                </td>
                                <td>
                                    <asp:ImageButton ID="frm7_btn_Continue" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/r-arrow1.png" OnClick="frm7_btn_Continue_Click" 
                                        ValidationGroup="frm7_group1" />
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
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="refresh"/> 
            </Triggers>
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
                        <div class="headertag">HighSchool History</div> 
                        <br />
               <asp:GridView ID="hischoolgrid" runat="server" AutoGenerateColumns="False" OnDataBound="hischoolgrid_DataBound" OnLoad="hischoolgrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">                      
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
                                   <asp:HyperLink ID="HyperLink1" class='iframe' title="Edit" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
                               </ItemTemplate>
                               <ItemStyle Width="20px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                               <ItemStyle Width="20px" />
                               <ItemTemplate>
                                   <asp:ImageButton ID="hischoolgrid_del" runat="server" CausesValidation="False" CommandName="Delete"
                                       ImageUrl="~/images/remove.png" Text="Delete" OnClick="hischoolgrid_del_Click" />
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>                      
                       <EmptyDataTemplate>
                           No High school history entered
                       </EmptyDataTemplate>                           
                   </asp:GridView>     
                   <br />                              
               <div class="headertag">University History</div> 
               <br />
                 <asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" OnDataBound="univgrid_DataBound" OnLoad="univgrid_Load" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">                      
                       <Columns>                        
                           <asp:BoundField DataField="Name" HeaderText="Institution" SortExpression="Name" />
                           <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                           <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                           <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                          <asp:TemplateField HeaderText="Edit">
                               <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                   <asp:HyperLink ID="HyperLink1" class='iframe' title="Edit" runat="server" ImageUrl="~/images/page_edit.png">HyperLink</asp:HyperLink>
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
                     <div id="revsec_Additional" runat="server">
                      <div class="headertag">Additional Official Hard Copy Service</div>
                      <br />
             <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="copychargergrid_DataBound" OnLoad="copychargergrid_Load" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
              </div>
                                            <br />  
                                              <div id="revsec_Email" runat="server">      
                                <div class="headertag">Official Electronic Copy Service</div>   
                    <br />                     
                    <asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="email_grid_DataBound" OnLoad="email_grid_Load"  ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                                </div>                           
                     <br />   
                     <div id="revsec_Fax" runat="server">      
                                <div class="headertag">Fax Copy Service</div>   
                                <br />                     
                                <asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnDataBound="fax_grid_DataBound" OnLoad="fax_grid_Load"  ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                                </div>                           
                     <br />                        
                     <div class="headertag">Delivery Service - Official Hard Copy(ies) &amp; Additional Copies</div>
                     <br />
  <asp:GridView ID="Delivery_Grid" runat="server" AutoGenerateColumns="False" OnDataBound="Delivery_Grid_DataBound" OnLoad="Delivery_Grid_Load"  ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
                                    <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Service Requested
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Delivery Type" 
                                            SortExpression="Name" />
                                        <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Cost") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Type" SortExpression="Type" 
                                            Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                <asp:Label ID="Label12" runat="server" Text="Copy"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="sqty" runat="server" Text="1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label8" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" SortExpression="Count" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Count") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="alt" />
                               </asp:GridView>
                              
                          <div align="right"  class="valid_box_msg"><b>Total Amount Due =</b>
                                <asp:Label ID="Reviewcost" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                <asp:Button ID="refresh" runat="server" CausesValidation="False" 
                                    CssClass="refreshbtn" OnClick="refresh_Click" Text="refresh" /></div> 
                                     <br />                        
                     <div class="headertag">Applicant Note</div>
                     <br />
                     <div id="spl_instruction" runat="server"></div>
                     <br />
                <asp:TextBox ID="Applicantmsg" runat="server" Height="150px" TextMode="MultiLine" 
                          Width="100%"></asp:TextBox>           
                                    
                   
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
                                    <asp:ImageButton ID="frm8_btn_previous" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/l-arrow1.png" OnClick="frm8_btn_previous_Click" />
                                </td>
                                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                                    Go Back to Step 6:<br />
                                    <span style="color:#AEAEAE; font-style: italic;">Services &amp; Fees </span></td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Continue to Step 8:<br />
                                    <span style="color:#AEAEAE; font-style: italic;">Payment</span>
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



