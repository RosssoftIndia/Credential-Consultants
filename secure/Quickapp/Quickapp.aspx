<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quickapp.aspx.cs"  Inherits="secure_Quickapp_Quickapp" %>
<%@ Register src="dots.ascx" tagname="dots" tagprefix="uc1" %>
<%@ Register src="../Menuctrl.ascx" tagname="Menuctrl" tagprefix="uc2" %>
<%@ Register src="../Userctrl.ascx" tagname="Userctrl" tagprefix="uc3" %>
<%@ Register src="trail.ascx" tagname="trial" tagprefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Code/menu/menu.css" rel="stylesheet" type="text/css" />    
    <link href="../Code/navigation/Navigation.css" rel="stylesheet" type="text/css" /> 
    <link href="../Code/menu/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../Code/Jquery/jquery-1.8.0.js"></script>
    <script type="text/javascript" src="../Code/Jquery/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../Code/Jquery/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../Code/Jquery/jquery.ui.position.js"></script>
	<script type="text/javascript" src="../Code/Jquery/jquery.ui.autocomplete.js"></script>
    <script type="text/javascript">
      
    $(document).ready(function() {
        jQueryInit();
    });

    function load() {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jQueryInit);
    }

    function jQueryInit() {
       
        //Upper education
        $("#<%=txtupper_institution.ClientID%>").autocomplete({
            source: function(request, response) {
            var varprefix = document.getElementById('txtupper_institution').value;
            var e = document.getElementById("drp_upper_country");
            var varcountry = e.options[e.selectedIndex].value;               
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Quickapp.aspx/GetInstitutionData",
                    data: "{'prefix':'" + varprefix + "','Country':'" + varcountry + "','type':'0'}",
                    dataType: "json",
                    success: function(data) {
                        response(data.d);
                    },
                    error: function(result) {
                        alert("Error");
                    }
                });
            }
            
        });
//        $("#<%=txtupper_degree.ClientID%>").autocomplete({
//            source: function(request, response) {
//            var varprefix = document.getElementById('txtupper_degree').value;
//                var e = document.getElementById("drp_upper_country");
//                var varcountry = e.options[e.selectedIndex].value;
//                $.ajax({
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    url: "Quickapp.aspx/GetdegreeData",
//                    data: "{'prefix':'" + varprefix + "','Country':'" + varcountry + "','type':'0'}",
//                    dataType: "json",
//                    success: function(data) {
//                        response(data.d);
//                    },
//                    error: function(result) {
//                        alert("Error");
//                    }
//                });
//            }
//        });

        //Post education
        $("#<%=txtpost_institution.ClientID%>").autocomplete({
            source: function(request, response) {
                var varprefix = document.getElementById('txtpost_institution').value;
                var e = document.getElementById("drp_post_country");
                var varcountry = e.options[e.selectedIndex].value;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Quickapp.aspx/GetInstitutionData",
                    data: "{'prefix':'" + varprefix + "','Country':'" + varcountry + "','type':'1'}",
                    dataType: "json",
                    success: function(data) {
                        response(data.d);
                    },
                    error: function(result) {
                        alert("Error");
                    }
                });
            }
        });
//        $("#<%=txtpost_degree.ClientID%>").autocomplete({
//            source: function(request, response) {
//                var varprefix = document.getElementById('txtpost_degree').value;
//                var e = document.getElementById("drp_post_country");
//                var varcountry = e.options[e.selectedIndex].value;
//                $.ajax({
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    url: "Quickapp.aspx/GetdegreeData",
//                    data: "{'prefix':'" + varprefix + "','Country':'" + varcountry + "','type':'1'}",
//                    dataType: "json",
//                    success: function(data) {
//                        response(data.d);
//                    },
//                    error: function(result) {
//                        alert("Error");
//                    }
//                });
//            }
//        });
//        $("#<%=txtpost_major.ClientID%>").autocomplete({
//            source: function(request, response) {
//            var varprefix = document.getElementById('txtpost_major').value;
//                var e = document.getElementById("drp_post_country");
//                var varcountry = e.options[e.selectedIndex].value;
//                $.ajax({
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    url: "Quickapp.aspx/GetmajorData",
//                    data: "{'prefix':'" + varprefix + "','Country':'" + varcountry + "'}",
//                    dataType: "json",
//                    success: function(data) {
//                        response(data.d);
//                    },
//                    error: function(result) {
//                        alert("Error");
//                    }
//                });
//            }
//        });
    } 
   
</script> 
</head>
            
<body onload="load()">
    <form id="form1" runat="server">          
       <div id="wrapper">           
	<div id="top">	
		<uc3:Userctrl ID="Headerctrl" runat="server" />	
		</div>   	
	<div class="mainmenu">       
        <uc2:Menuctrl ID="Menuctrl1" runat="server" />       
  </div>  
  <br />
  <br />	
  <br />
  <br />  
  
 <div id="sub">
 	 <span class="title" >Quick App</span>   
		<br />
		<br />	
            <br />
	</div>
	
	<div id="main">
		<div id="container">

<asp:ScriptManager ID="ScriptManager1" runat="server">

</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
<ContentTemplate>
            <div class="app">           
    <div class="app-container">
    <div class="app-container-inner">  
    <div class="heading">
 <uc4:trial ID="trialctrl" runat="server" />	
    </div>
    <br /> 
       <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="Personalinfo" runat="server">      
   <fieldset class="row2" style="float:right;">
   <div>
   <lb>Client:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="dpsubclients" runat="server" AppendDataBoundItems="true"  CssClass="drp">                
</asp:DropDownList>
  <sv:RequiredFieldValidator ID="dpsubclientsValidator" runat="server" ControlToValidate="dpsubclients"
        ErrorMessage="Client Required" InitialValue="0"  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</div>
<div>
  <lb>Purpose:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="dppurpose" runat="server" AppendDataBoundItems="true"  CssClass="drp">                
</asp:DropDownList>
  <sv:RequiredFieldValidator ID="dppurposeValidator" runat="server" ControlToValidate="dppurpose"
        ErrorMessage="Purpose Required" InitialValue="0"  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</div>
   </fieldset>
<fieldset class="row1">
<legend> Personal Information</legend>                            
</fieldset>  
<fieldset class="row2">
<div><lb>First Name:&nbsp<span style="color:Red;">*</span></lb><asp:TextBox ID="txtfirstname" runat="server" 
        CssClass="ctrl" ></asp:TextBox> 
<sv:RequiredFieldValidator ID="txtfirstnameValidator" runat="server" ControlToValidate="txtfirstname"
        ErrorMessage="Firstname Required" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator></div>
<div><lb>Middle Name:</lb><asp:TextBox ID="txtmiddlename" runat="server" 
        CssClass="ctrl" ></asp:TextBox></div>    
<div><lb>Last Name:&nbsp<span style="color:Red;">*</span></lb><asp:TextBox ID="txtlastname" runat="server" 
        CssClass="ctrl"></asp:TextBox>
        <sv:RequiredFieldValidator ID="txtlastnameValidator" runat="server" ControlToValidate="txtlastname"
        ErrorMessage="Lastname Required" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
        </div>
<div><lb>Gender:&nbsp<span style="color:Red;">*</span></lb>
<asp:RadioButtonList ID="rbgender" runat="server" RepeatDirection="Horizontal" 
        RepeatLayout="Flow">
<asp:ListItem Text="Male" Value="Male"></asp:ListItem>  
<asp:ListItem Text="Female" Value="Female"></asp:ListItem>  
</asp:RadioButtonList>
<sv:RequiredFieldValidator ID="rbgenderValidator" runat="server" ControlToValidate="rbgender"
        ErrorMessage="Gender Required" InitialValue=""  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</div>
<div><lb>Country Of Birth:&nbsp<span style="color:Red;">*</span></lb> <asp:DropDownList ID="drpcountrybirth" AppendDataBoundItems="true"
        runat="server"  CssClass="drp">               
</asp:DropDownList>
<sv:RequiredFieldValidator ID="drpcountrybirthValidator" runat="server" ControlToValidate="drpcountrybirth"
        ErrorMessage="Country of Birth Required" InitialValue="0"  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
 </div> 
</fieldset> 
<fieldset class="row3">
<div><lb>DOB(mm-dd-yyyy):</lb>

<asp:DropDownList ID="drp_month" runat="server" Width="54px"  CssClass="drp">                  
</asp:DropDownList>   

<asp:DropDownList ID="drp_date" runat="server" Width="54px" CssClass="drp">               
</asp:DropDownList>

<asp:DropDownList ID="drp_year" runat="server" Width="69px" CssClass="drp">               
</asp:DropDownList>   
</div>
<div><lb>Is the name on your document different?</lb>
<asp:RadioButtonList ID="opt_name_select" runat="server" 
        RepeatDirection="Horizontal" 
        onselectedindexchanged="opt_name_select_SelectedIndexChanged" 
        AutoPostBack="True" >
<asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>  
<asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>  
</asp:RadioButtonList>
</div>    
<div id="opt_firstname" runat="server" visible="false" ><lb>First Name:</lb>
    <asp:TextBox ID="txtofirstname" runat="server" CssClass="ctrl"></asp:TextBox></div>
<div id="opt_middlename" runat="server" visible="false"><lb>Middle Name:</lb>
    <asp:TextBox ID="txtomiddlename" runat="server" CssClass="ctrl"></asp:TextBox></div>
<div id="opt_lastname" runat="server" visible="false"><lb>Last Name:</lb>
    <asp:TextBox ID="txtolastname" runat="server" CssClass="ctrl"></asp:TextBox></div>
</fieldset> 
<fieldset class="row1">
<legend>Address Information</legend>   
<div><lb>Address 1:</lb><asp:TextBox ID="txtaddress1" runat="server" 
        CssClass="ctrl"></asp:TextBox></div>

<div><lb>Address 2:</lb><asp:TextBox ID="txtaddress2" runat="server" 
        CssClass="ctrl"></asp:TextBox></div>

<div><lb>City:</lb><asp:TextBox ID="txtcity" runat="server" CssClass="ctrl"></asp:TextBox></div>

<div><lb>State:</lb><asp:TextBox ID="txtstate" runat="server" CssClass="ctrl"></asp:TextBox></div>

<div><lb>Zip Code:</lb><asp:TextBox ID="txtzipcode" runat="server" CssClass="ctrl"></asp:TextBox><lb>Country:&nbsp<span style="color:Red;">*</span></lb> 
<asp:DropDownList ID="drpcountry" runat="server" AppendDataBoundItems="true"  CssClass="drp">               
    </asp:DropDownList> 
    <sv:RequiredFieldValidator ID="drpcountryValidator" runat="server" ControlToValidate="drpcountry"
        ErrorMessage="Country Required" InitialValue="0"  ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
    </div>
</fieldset> 
<fieldset class="row1">
<legend>Contact Information</legend>   
<div><lb>Primary Phone:&nbsp<span style="color:Red;">*</span></lb><asp:TextBox ID="txtprimaryphone" runat="server" CssClass="ctrl" ></asp:TextBox>
 <sv:RequiredFieldValidator ID="txtprimaryphoneValidator" runat="server" ControlToValidate="txtprimaryphone"
        ErrorMessage="PrimaryPhone Required" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
</div>
<div><lb>Primary Email:&nbsp<span style="color:Red;">*</span></lb><asp:TextBox ID="txtemail" runat="server" CssClass="ctrl"></asp:TextBox>
  <sv:RequiredFieldValidator ID="txtemailValidator" runat="server" ControlToValidate="txtemail"
        ErrorMessage="Email Required" ValidationGroup="frm1_group">*</sv:RequiredFieldValidator>
        <span style="margin:-11px;" > <sv:RegularExpressionValidator ID="txtemailvalidValidator" runat="server" ControlToValidate="txtemail"
        ErrorMessage="valid Email Required" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="frm1_group">*</sv:RegularExpressionValidator ></span>
</div>
<div><lb>Secondary Phone:</lb><asp:TextBox ID="txtsecondaryphone" runat="server" CssClass="ctrl" ></asp:TextBox></div>
<div><lb>Mobile Phone:</lb><asp:TextBox ID="txtmobile" runat="server" CssClass="ctrl" ></asp:TextBox></div>
</fieldset>         
<br />
<fieldset class="row2" style="float:none;">
<div>
 <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="frm1_group" CssClass="summarybox" />
 </div>
</fieldset>  
<br /> 
           <div class="footer">
           <table>
      <tr>
      <td style="width:150px;" ></td>
      <td id="Stage1dot" runat="server" style="width:auto;margin:0px auto;vertical-align:middle;">
         <uc1:dots ID="dots1" runat="server" />
           </td>
      <td style="width:100px;" class="container_buttons" >
            <asp:LinkButton ID="Stage1next" runat="server" class="rbtn" ValidationGroup="frm1_group"
                onclick="next_Click">Next&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>   
      </td>
      </tr>
      </table>          
          
    </div>
            </asp:View>
           <asp:View ID="Uppersecondary" runat="server">
<fieldset class="row1">
<legend>Education History</legend>  
<div>
    <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Auto">
<asp:GridView ID="GridView_UpperEdu" Width="100%" runat="server" DataKeyNames="Rid"
AutoGenerateColumns="false" onrowcommand="GridView_UpperEdu_RowCommand" CssClass="Grid" HeaderStyle-CssClass="GridHeader" AlternatingRowStyle-CssClass="GridAltItem" >
<Columns> 
<asp:TemplateField HeaderText="Id">                             
<ItemTemplate>                                 
<asp:Label ID="lblRid" runat="server" Text='<%# Bind("Rid") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Institution">                             
<ItemTemplate>                                 
<asp:Label ID="lblInstitution" runat="server" Text='<%# Bind("Institution") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Degree">                             
<ItemTemplate>                                 
<asp:Label ID="lblDegree" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Place">                             
<ItemTemplate>                                 
<asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
<asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
<asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Year">                             
<ItemTemplate>                                 
<asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>-<asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Completion">                             
<ItemTemplate>                                 
<asp:Label ID="lblGraduated" runat="server" Visible="false" Text='<%# Bind("Graduated") %>'></asp:Label>
<asp:Label ID="lblGraduation" runat="server" Text='<%# Bind("Graduation") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:ImageButton ID="delbtn" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Rid")%>'
ImageUrl="~/images/remove.png" Text="Delete" CommandName="DeleteRec" OnClientClick='javascript:return confirm("Are you sure want to delete this entry?");' Width="16px" />
</ItemTemplate>
</asp:TemplateField>
</Columns>                      
<EmptyDataTemplate>
No High school history entered
</EmptyDataTemplate>  
</asp:GridView>
    </asp:Panel>
</div> 

</fieldset> 
<fieldset class="row1">
<legend>Upper Secondary School/High School Entry</legend>  
</fieldset> 
<fieldset class="row2">
<div><lb>Country of Study:&nbsp<span style="color:Red;">*</span></lb>
    <asp:DropDownList ID="drp_upper_country" AppendDataBoundItems="true"  
        runat="server"  CssClass="drp" AutoPostBack="True" Width="232px"
        onselectedindexchanged="drp_upper_country_SelectedIndexChanged"></asp:DropDownList>     
        <sv:RequiredFieldValidator ID="upper_Validator0" runat="server" ControlToValidate="drp_upper_country" ErrorMessage="Country Required" InitialValue="0" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>
     </div>
<div><lb>Name of the Institution:&nbsp<span style="color:Red;">*</span></lb>
<asp:TextBox runat="server" ID="txtupper_institution" CssClass="ctrl" Width="220" MaxLength="180" />
<sv:RequiredFieldValidator ID="upper_Validator1" runat="server" ControlToValidate="txtupper_institution" ErrorMessage="Institution Required" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>
</div>
<div><lb>Education Program / Degree Plan:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="drp_upper_degree" AppendDataBoundItems="true" runat="server" 
        Width="232px" AutoPostBack="True" CssClass="drp"
        OnSelectedIndexChanged="drp_upper_degree_SelectedIndexChanged">                                                         
</asp:DropDownList>
<sv:RequiredFieldValidator ID="upper_Validator2" runat="server" ControlToValidate="drp_upper_degree" ErrorMessage="Degree Required" InitialValue="0" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>
<asp:TextBox ID="txtupper_degree" runat="server" CssClass="ctrl" Visible="false" Width="220" ></asp:TextBox></div>
<sv:RequiredFieldValidator ID="upper_Validator3" runat="server" ControlToValidate="txtupper_degree" ErrorMessage="Education program Required" ValidationGroup="frm2_group2">*</sv:RequiredFieldValidator>
<div><lb>City:</lb><asp:TextBox ID="txtuppercity" CssClass="ctrl" runat="server" ></asp:TextBox>
<%--<sv:RequiredFieldValidator ID="upper_Validator4" runat="server" ControlToValidate="txtuppercity" ErrorMessage="City Required" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>--%>
</div>
<div><lb>State / Province:</lb><asp:TextBox ID="txtupperstate" CssClass="ctrl" runat="server" ></asp:TextBox></div>
</fieldset> 
<fieldset class="row3">
<div><lb>Dates Attended:&nbsp<span style="color:Red;">*</span></lb></div>     
<div><lb>Start Date:</lb>
<asp:DropDownList ID="drp_upper_startdate" runat="server" Width="69px" 
        CssClass="drp" AutoPostBack="True" 
        onselectedindexchanged="drp_upper_startdate_SelectedIndexChanged">               
</asp:DropDownList>
<sv:RequiredFieldValidator ID="upper_Validator5" runat="server" ControlToValidate="drp_upper_startdate" ErrorMessage="Start Year Required" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>
</div> 
<div><lb>End Date:</lb>
<asp:DropDownList ID="drp_upper_enddate" runat="server" Width="69px" 
        CssClass="drp" AutoPostBack="True" 
        onselectedindexchanged="drp_upper_enddate_SelectedIndexChanged">               
</asp:DropDownList>
 <sv:RequiredFieldValidator ID="upper_Validator6" runat="server" ControlToValidate="drp_upper_enddate" ErrorMessage="End Year Required" ValidationGroup="frm2_group1">*</sv:RequiredFieldValidator>
  </div>
</fieldset> 
<fieldset class="row3">
<div><lb>Graduated ?:</lb><asp:RadioButtonList ID="rbuppergraduation" runat="server" 
        RepeatDirection="Horizontal" 
        onselectedindexchanged="rbuppergraduation_SelectedIndexChanged" 
        AutoPostBack="True" >
<asp:ListItem Text="Yes" Value="True"></asp:ListItem>  
<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>  
</asp:RadioButtonList></div> 
<div id="opt_upper_year" runat="server" visible="false"><lb>Year of Graduation:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="drp_upper_compdate" runat="server" Width="69px" 
        CssClass="drp">               
</asp:DropDownList>
  <sv:RequiredFieldValidator ID="upper_Validator7" runat="server" ControlToValidate="drp_upper_compdate"
                                ErrorMessage="Year of graduation Required" ValidationGroup="frm2_group3">*</sv:RequiredFieldValidator>
</div> 
</fieldset>  
<fieldset class="row" style="margin-left:48%; " >
<asp:LinkButton ID="upper_btnsubmit" runat="server" CssClass="submitbtn"  
        onclick="Education" ValidationGroup="upper_group" CausesValidation="False">Submit</asp:LinkButton>
</fieldset>  
<br />
    <div class="footer">
           <table>
      <tr>
      <td style="width:150px;" class="container_buttons">
    <asp:LinkButton ID="Stage2previous" runat="server" class="lbtn" 
              onclick="previous_Click">Previous</asp:LinkButton> 
      </td>
      <td id="Stage2dot" runat="server"  style="width:auto;margin:0px auto;vertical-align:middle;">
         <uc1:dots ID="dots2" runat="server" />
           </td>
      <td style="width:100px;" class="container_buttons">
            <asp:LinkButton ID="Stage2next" runat="server" class="rbtn"  onclick="next_Click">Next&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>   
      </td>
      </tr>
      </table>          
          
    </div>
           </asp:View>
              <asp:View ID="Postsecondary" runat="server">
<fieldset class="row1">
<legend>Education History</legend>  
<div>
    <asp:Panel ID="Panel2" runat="server" Height="100px" ScrollBars="Auto">
<asp:GridView ID="GridView_PostEdu" Width="100%" runat="server" DataKeyNames="Rid"
            AutoGenerateColumns="false" onrowcommand="GridView_PostEdu_RowCommand" CssClass="Grid" HeaderStyle-CssClass="GridHeader" AlternatingRowStyle-CssClass="GridAltItem" >
<Columns> 
<asp:TemplateField HeaderText="Id">                             
<ItemTemplate>                                 
<asp:Label ID="lblRid" runat="server" Text='<%# Bind("Rid") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Institution">                             
<ItemTemplate>                                 
<asp:Label ID="lblInstitution" runat="server" Text='<%# Bind("Institution") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Degree">                             
<ItemTemplate>                                 
<asp:Label ID="lblDegree" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Major">                             
<ItemTemplate>                                 
<asp:Label ID="lblMajor" runat="server" Text='<%# Bind("Major") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Place">                             
<ItemTemplate>                                 
<asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
<asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
<asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Year">                             
<ItemTemplate>                                 
<asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>-<asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Completion">                             
<ItemTemplate>                                 
<asp:Label ID="lblGraduated" runat="server" Visible="false" Text='<%# Bind("Graduated") %>'></asp:Label>
<asp:Label ID="lblGraduation" runat="server" Text='<%# Bind("Graduation") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:ImageButton ID="delbtn" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Rid")%>'
ImageUrl="~/images/remove.png" Text="Delete" CommandName="DeleteRec" OnClientClick='javascript:return confirm("Are you sure want to delete this entry?");' Width="16px" />
</ItemTemplate>
</asp:TemplateField>
</Columns>                      
<EmptyDataTemplate>
No High school history entered
</EmptyDataTemplate>  
</asp:GridView>
    </asp:Panel>
</div> 

</fieldset> 
<fieldset class="row1">
<legend>Post-Secondary degree/University Degree Entry</legend>  
</fieldset> 
<fieldset class="row2">
<div><lb>Country of Study:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="drp_post_country" AppendDataBoundItems="true"  
        runat="server"  CssClass="drp" AutoPostBack="True" Width="232px"
        onselectedindexchanged="drp_post_country_SelectedIndexChanged"></asp:DropDownList> 
        <sv:RequiredFieldValidator ID="post_Validator1" runat="server" ControlToValidate="drp_post_country" ErrorMessage="Country Required" InitialValue="0" ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>   
</div>
<div><lb>Name of the Institution:&nbsp<span style="color:Red;">*</span></lb>
<asp:TextBox runat="server" ID="txtpost_institution" CssClass="ctrl" Width="220" MaxLength="180" />
<sv:RequiredFieldValidator ID="post_Validator2" runat="server" ControlToValidate="txtpost_institution" ErrorMessage="Institution Required"  ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>   
</div>
<div><lb>Education Program / Degree Plan:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="drp_post_degree" AppendDataBoundItems="true" runat="server" 
        Width="232px" AutoPostBack="True" CssClass="drp"
        OnSelectedIndexChanged="drp_post_degree_SelectedIndexChanged">                                                         
</asp:DropDownList>
<sv:RequiredFieldValidator ID="post_Validator3" runat="server" ControlToValidate="drp_post_degree" ErrorMessage="Degree Required" InitialValue="0"  ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>   
<asp:TextBox ID="txtpost_degree" CssClass="ctrl" runat="server" Visible="false" Width="220"></asp:TextBox>
<sv:RequiredFieldValidator ID="post_Validator4" runat="server" ControlToValidate="txtpost_degree" ErrorMessage="Degree Required"  ValidationGroup="frm3_group2">*</sv:RequiredFieldValidator>   
</div>
<div><lb>Major:</lb>
<asp:DropDownList ID="drp_post_major" AppendDataBoundItems="true" runat="server" 
        Width="232px" AutoPostBack="True" CssClass="drp"
        OnSelectedIndexChanged="drp_post_major_SelectedIndexChanged">                                                         
</asp:DropDownList>
<%--<sv:RequiredFieldValidator ID="post_Validator5" runat="server" ControlToValidate="drp_post_major" ErrorMessage="Major Required" InitialValue="0"  ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>   --%>
<asp:TextBox ID="txtpost_major" CssClass="ctrl" runat="server" Visible="false" Width="220"  ></asp:TextBox>
<%--<sv:RequiredFieldValidator ID="post_Validator6" runat="server" ControlToValidate="txtpost_major" ErrorMessage="Major Required"  ValidationGroup="frm3_group3">*</sv:RequiredFieldValidator>   --%>
</div>
<div><lb>City:</lb><asp:TextBox ID="txtpostcity" CssClass="ctrl" runat="server" ></asp:TextBox>
<%--<sv:RequiredFieldValidator ID="post_Validator7" runat="server" ControlToValidate="txtpostcity" ErrorMessage="City Required"  ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>   --%>
</div>
<div><lb>State / Province:</lb><asp:TextBox ID="txtpoststate" CssClass="ctrl" runat="server" ></asp:TextBox></div>
</fieldset> 
<fieldset class="row3">
<div><lb>Dates Attended:&nbsp<span style="color:Red;">*</span></lb></div>     
<div><lb>Start Date:</lb>
<asp:DropDownList ID="drp_post_startdate" runat="server" Width="69px" 
        CssClass="drp" AutoPostBack="True" 
        onselectedindexchanged="drp_post_startdate_SelectedIndexChanged">               
</asp:DropDownList>
<sv:RequiredFieldValidator ID="post_Validator8" runat="server" ControlToValidate="drp_post_startdate" ErrorMessage="Start Year Required" ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>
</div> 
<div><lb>End Date:</lb>
<asp:DropDownList ID="drp_post_enddate" runat="server" Width="69px" 
        CssClass="drp" AutoPostBack="True" 
        onselectedindexchanged="drp_post_enddate_SelectedIndexChanged">               
</asp:DropDownList>
<sv:RequiredFieldValidator ID="post_Validator9" runat="server" ControlToValidate="drp_post_enddate" ErrorMessage="End Year Required" ValidationGroup="frm3_group1">*</sv:RequiredFieldValidator>
 </div>
</fieldset> 
<fieldset class="row3">
<div><lb>Graduated ?:</lb>
<asp:RadioButtonList ID="rbpostgraduation" runat="server" 
        RepeatDirection="Horizontal" 
        onselectedindexchanged="rbpostgraduation_SelectedIndexChanged" 
        AutoPostBack="True" >
<asp:ListItem Text="Yes" Value="True"></asp:ListItem>  
<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>  
</asp:RadioButtonList>
</div> 
<div id="opt_post_year" runat="server" visible="false"><lb>Year of Graduation:&nbsp<span style="color:Red;">*</span></lb>
<asp:DropDownList ID="drp_post_compdate" runat="server" Width="69px" 
        CssClass="drp">               
</asp:DropDownList>
 <sv:RequiredFieldValidator ID="post_Validator10" runat="server" ControlToValidate="drp_post_compdate" ErrorMessage="Year of graduation Required" ValidationGroup="frm3_group4">*</sv:RequiredFieldValidator>
</div> 
</fieldset> 
<fieldset class="row" style="margin-left:48%; ">
<asp:LinkButton ID="post_btnsubmit" runat="server"  CssClass="submitbtn"
              onclick="Education" CausesValidation="False">Submit</asp:LinkButton> 

</fieldset> 
 
<br />
    <div class="footer">
           <table>
      <tr>
      <td style="width:150px;" class="container_buttons">
    <asp:LinkButton ID="Stage3previous" runat="server" class="lbtn" 
              onclick="previous_Click">Previous</asp:LinkButton> 
      </td>
      <td id="Td1" runat="server"  style="width:auto;margin:0px auto;vertical-align:middle;">
         <uc1:dots ID="dots3" runat="server" />
           </td>
      <td style="width:100px;" class="container_buttons">
            <asp:LinkButton ID="Stage3next" runat="server" class="rbtn"  onclick="next_Click">Next&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>   
      </td>
      </tr>
      </table>          
          
    </div>
           </asp:View>
           <asp:View ID="Review" runat="server" >         
<fieldset class="row1">
<legend>Personal Information</legend>  
<div>
    <asp:DetailsView ID="dvpersonalinfo" runat="server" Height="50px" 
        AutoGenerateRows="false" CssClass="detailview"  FieldHeaderStyle-CssClass="detailview_header"  AlternatingRowStyle-CssClass="alternate">
    <Fields>
    <asp:TemplateField HeaderText="First Name" HeaderStyle-ForeColor="White"   HeaderStyle-Width="200px">              
    <ItemTemplate>
   <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Middle Name">              
    <ItemTemplate>
   <asp:Label ID="lblMiddleName" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Last Name">              
    <ItemTemplate>
   <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Gender">              
    <ItemTemplate>
   <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
       <asp:TemplateField HeaderText="Birth Country">              
    <ItemTemplate>
   <asp:Label ID="lblCountryofbirth" runat="server" Text='<%# Bind("Countryofbirth") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Alternate FirstName">              
    <ItemTemplate>
   <asp:Label ID="lblotherFirstName" runat="server" Text='<%# Bind("otherFirstName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Alternate MiddleName">              
    <ItemTemplate>
   <asp:Label ID="lblotherMiddleName" runat="server" Text='<%# Bind("otherMiddleName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Alternate LastName">              
    <ItemTemplate>
   <asp:Label ID="lblotherLastName" runat="server" Text='<%# Bind("otherLastName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    </Fields> 
        <AlternatingRowStyle CssClass="alternate" />
    </asp:DetailsView>
</div> 
</fieldset> 
<fieldset class="row1">
<legend>Address Information</legend>  
<div>
    <asp:DetailsView ID="dvAddressinfo" runat="server" Height="50px" 
        AutoGenerateRows="false" CssClass="detailview"    FieldHeaderStyle-CssClass="detailview_header"  AlternatingRowStyle-CssClass="alternate">
    <Fields>
    <asp:TemplateField HeaderText="Addressline1" HeaderStyle-Width="200px">              
    <ItemTemplate>
   <asp:Label ID="lblAddressline1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Addressline2">              
    <ItemTemplate>
   <asp:Label ID="lblAddressline2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="City">              
    <ItemTemplate>
   <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="State">              
    <ItemTemplate>
   <asp:Label ID="lblState_or_province" runat="server" Text='<%# Bind("State_or_province") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
       <asp:TemplateField HeaderText="Zip">              
    <ItemTemplate>
   <asp:Label ID="lblZip_or_PostalCode" runat="server" Text='<%# Bind("Zip_or_PostalCode") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Country">              
    <ItemTemplate>
   <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>     
    </Fields> 
        <AlternatingRowStyle CssClass="alternate" />
    </asp:DetailsView>
</div> 
</fieldset> 
<fieldset class="row1">
<legend>Contact Information</legend>  
<div>
    <asp:DetailsView ID="dvContactinfo" runat="server" Height="50px" 
        AutoGenerateRows="false" CssClass="detailview"    FieldHeaderStyle-CssClass="detailview_header"  AlternatingRowStyle-CssClass="alternate">
    <Fields>
    <asp:TemplateField HeaderText="Primary Phone" HeaderStyle-Width="200px" >              
    <ItemTemplate>
   <asp:Label ID="lblHomePhone" runat="server" Text='<%# Bind("HomePhone") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Secondary Phone">              
    <ItemTemplate>
   <asp:Label ID="lblWorkPhone" runat="server" Text='<%# Bind("WorkPhone") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Mobile Phone">              
    <ItemTemplate>
   <asp:Label ID="lblMobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Email">              
    <ItemTemplate>
   <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>    
    </Fields> 
        <AlternatingRowStyle CssClass="alternate" />
    </asp:DetailsView>
</div> 
</fieldset>
<fieldset class="row1">
<legend>Upper Secondary Information</legend>  
<div>
    <asp:GridView ID="gvupperedu" Width="100%" runat="server" DataKeyNames="Rid"
AutoGenerateColumns="false" CssClass="Grid" HeaderStyle-CssClass="GridHeader" AlternatingRowStyle-CssClass="GridAltItem" >
<Columns> 
<asp:TemplateField HeaderText="Id">                             
<ItemTemplate>                                 
<asp:Label ID="lblRid" runat="server" Text='<%# Bind("Rid") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Institution">                             
<ItemTemplate>                                 
<asp:Label ID="lblInstitution" runat="server" Text='<%# Bind("Institution") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Degree">                             
<ItemTemplate>                                 
<asp:Label ID="lblDegree" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Place">                             
<ItemTemplate>                                 
<asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
<asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
<asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Year">                             
<ItemTemplate>                                 
<asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>-<asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Completion">                             
<ItemTemplate>                                 
<asp:Label ID="lblGraduated" runat="server" Visible="false" Text='<%# Bind("Graduated") %>'></asp:Label>
<asp:Label ID="lblGraduation" runat="server" Text='<%# Bind("Graduation") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemTemplate>
</ItemTemplate>
</asp:TemplateField>
</Columns>                      
<EmptyDataTemplate>
No High school history entered
</EmptyDataTemplate>  
</asp:GridView>
</div> 
</fieldset>
<fieldset class="row1">
<legend>Post Secondary Information</legend>  
<div>
    <asp:GridView ID="gvpostedu" Width="100%" runat="server" DataKeyNames="Rid"
            AutoGenerateColumns="false" onrowcommand="GridView_PostEdu_RowCommand" CssClass="Grid" HeaderStyle-CssClass="GridHeader" AlternatingRowStyle-CssClass="GridAltItem" >
<Columns> 
<asp:TemplateField HeaderText="Id">                             
<ItemTemplate>                                 
<asp:Label ID="lblRid" runat="server" Text='<%# Bind("Rid") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Institution">                             
<ItemTemplate>                                 
<asp:Label ID="lblInstitution" runat="server" Text='<%# Bind("Institution") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Degree">                             
<ItemTemplate>                                 
<asp:Label ID="lblDegree" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Major">                             
<ItemTemplate>                                 
<asp:Label ID="lblMajor" runat="server" Text='<%# Bind("Major") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Place">                             
<ItemTemplate>                                 
<asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
<asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
<asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Year">                             
<ItemTemplate>                                 
<asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>-<asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Completion">                             
<ItemTemplate>                                 
<asp:Label ID="lblGraduated" runat="server" Visible="false" Text='<%# Bind("Graduated") %>'></asp:Label>
<asp:Label ID="lblGraduation" runat="server" Text='<%# Bind("Graduation") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemTemplate>
</ItemTemplate>
</asp:TemplateField>
</Columns>                      
<EmptyDataTemplate>
No High school history entered
</EmptyDataTemplate>  
</asp:GridView>
</div> 
</fieldset>
 
<br />
    <div class="footer">
           <table>
      <tr>
      <td style="width:150px;" class="container_buttons">
    <asp:LinkButton ID="Stage4previous" runat="server" class="lbtn" 
              onclick="previous_Click">Previous</asp:LinkButton> 
      </td>
      <td id="Td2" runat="server"  style="width:auto;margin:0px auto;vertical-align:middle;">
         <uc1:dots ID="dots4" runat="server" />
           </td>
      <td style="width:100px;" class="container_buttons">
            <asp:LinkButton ID="Stage4next" runat="server" class="rbtn"  onclick="next_Click">Submit&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>   
      </td>
      </tr>
      </table>          
          
    </div>
           </asp:View>
                <asp:View ID="Track" runat="server" >          
<fieldset class="row1">
<div class="summarybox">
        <h4>Application Information</h4>
        <p>Application submitted has been <span id="txtstatus" runat="server" ></span></p>
        <p><b>FileNumber:</b><span id="txtfileno" runat="server" ></span></p>
        <% if (txtstatus.InnerText == "UnSuccessful")
           {  %>
        <p><b>Error:</b><span id="txterror" runat="server" ></span></p>              
        <%} %>
       <p class="container_buttons"><asp:LinkButton ID="newapp" runat="server" 
               class="rbtn" onclick="newapp_Click"  >New App</asp:LinkButton> </p>
      </div>
</fieldset> 

</asp:View> 
      </asp:MultiView>      
        
        </div>  
    </div>
   </div>          
  </ContentTemplate>
  </asp:UpdatePanel>
  
</div>
		
		<div id="bottom">
		 <p ID="Msgbox" runat="server"></p>		
		</div>
			
			<div class="clear"></div>
		
		</div>
		
		<div id="page-bottom">
		 
		</div>

	</div>     
  
      
   
    </form>
</body>
</html>
