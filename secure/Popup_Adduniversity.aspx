<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Adduniversity.aspx.cs" MasterPageFile="~/secure/popupMaster.master"  Inherits="secure_Popup_Adduniversity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  
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
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br /> 
    <table width="100%"> 	
     <tr>
     <td>
     <br />
     <div class="headertag">Higher Education / University Degree History</div>
     <br />
     <table  class="search_css">   
    <tr>
	<td colspan="3">Country of Study:&nbsp;<span style="color:Red;">*</span>
    <asp:DropDownList ID="frma2_opt_country" runat="server" Width="204px" AutoPostBack="True" OnSelectedIndexChanged="frma2_opt_country_SelectedIndexChanged" AppendDataBoundItems="True">                            
    </asp:DropDownList></td> 
					</tr> 
</table> 
	</td>
	</tr>
	<tr>
	<td>
	<table id="frma2_details" runat="server" visible="true" class="search_css"> 		     
	<tr>
    <td style="width: 293px">
Name of Institution: <span style="color:Red;">*</span><br />
<ajaxToolkit:ToolkitScriptManager ID="frma1_ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:TextBox runat="server" ID="frma2_institution" Width="220px" autocomplete="off" /><sv:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="frma2_institution" ErrorMessage="You must Enter an Institution" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
						   <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="frma2_AutoCompleteExtender1" 
                TargetControlID="frma2_institution"
                ServicePath="../University.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="1" 
                CompletionInterval="30"
                EnableCaching="true"
                CompletionSetCount="20"
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                ContextKey="CountryId" UseContextKey="True">
                <Animations>
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
                    </OnHide></Animations>
            </ajaxToolkit:AutoCompleteExtender>
                        
</td>
<td>
Education Program:<br /><asp:DropDownList ID="frma2_option_degree" runat="server" Width="359px" AppendDataBoundItems ="true" AutoPostBack="True" OnSelectedIndexChanged="frma2_option_degree_SelectedIndexChanged"></asp:DropDownList>
<sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator6" runat="server" ControlToValidate="frma2_option_degree" ErrorMessage="You must select a Degree" InitialValue="0" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
<asp:TextBox  ID="frma2_degree" runat="server" Visible="False" Width="338px"></asp:TextBox>
    <sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator11" runat="server" ControlToValidate="frma2_degree" ErrorMessage="You must enter a Degree" ValidationGroup="frma2_group2">*</sv:RequiredFieldValidator>
</td>
</tr> 
<tr>
<td style="width: 293px">
City: <span style="color:Red;">*</span><br /> 
<asp:TextBox ID="frma2_city" runat="server" Width="220px" ></asp:TextBox>
<sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator2" runat="server" ControlToValidate="frma2_city" ErrorMessage="You must select a City" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
</td> 
<td>
Major:<br /><asp:DropDownList ID="frma2_option_major" runat="server" Width="110px" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="frma2_option_major_SelectedIndexChanged"></asp:DropDownList>
<sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator7" runat="server" ControlToValidate="frma2_option_major" ErrorMessage="You must select a Major" InitialValue="0" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
<asp:TextBox ID="frma2_major" runat="server" Visible="False"></asp:TextBox>
    <sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator12" runat="server" ControlToValidate="frma2_major" ErrorMessage="You must enter a Major" ValidationGroup="frma2_group3">*</sv:RequiredFieldValidator>
</td>
</tr> 
<tr>
<td style="width: 293px">
State/Province:<br /><asp:TextBox ID="frma2_state" runat="server" Width="220px" TextMode="MultiLine" ></asp:TextBox>
</td>
<td style="padding-right: 0px; padding-left: 0px">
<table cellpadding="0" align="center" border="0" style="width: 422px; height: 121px; border-top-style: none; border-right-style: none; border-left-style: none; border-collapse: collapse; border-bottom-style: none;"> 
<tr>
<td style="vertical-align: top; text-align: left" rowspan="2">
Dates Attended : <span style="color:Red;">*</span>
<br /> 
Start :<asp:DropDownList ID="frma2_start_year" runat="server" Width="58px" 
        AutoPostBack="True" 
        onselectedindexchanged="frma2_start_year_SelectedIndexChanged"></asp:DropDownList>
    <sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator4" runat="server" ControlToValidate="frma2_start_year" ErrorMessage="You must select Start Year" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
    <br />
    <br />
End &nbsp; :<asp:DropDownList ID="frma2_end_year" runat="server" Width="58px"></asp:DropDownList>
    <sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator5" runat="server" ControlToValidate="frma2_end_year" ErrorMessage="You must select End Year" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
<sv:CompareValidator ID="frma2_CompareValidator1" runat="server" ControlToCompare="frma2_start_year" ControlToValidate="frma2_end_year" ErrorMessage="Select a Valid End Year" Operator="GreaterThan" ValidationGroup="frma2_group">*</sv:CompareValidator>                                                        
</td>
<td style="vertical-align: top; text-align: left">
Did you graduate?: 
    <br />
    <asp:DropDownList ID="frma2_option_graduate" runat="server" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="frma2_option_graduate_SelectedIndexChanged">
        <asp:ListItem Value="0">Select</asp:ListItem>
<asp:ListItem Value="True">Yes</asp:ListItem>
<asp:ListItem Value="False">No</asp:ListItem>
</asp:DropDownList>
<sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator8" runat="server" ControlToValidate="frma2_option_graduate" ErrorMessage="You must select Did you graduate" InitialValue="0" ValidationGroup="frma2_group">*</sv:RequiredFieldValidator>
</td>
</tr>
<tr>
<td id="frma2_optional" runat="server" visible="false" style="vertical-align: top; text-align: left">
Date of graduation: (mm/dd/yyyy<span style="color: #ff0000">*</span>)<span style="color:Red;"></span>               
<br />                                      
<asp:DropDownList ID="frma2_month" runat="server" Width="54px"></asp:DropDownList> / 
 <asp:DropDownList ID="frma2_date" runat="server" Width="54px"></asp:DropDownList> / 
<asp:DropDownList ID="frma2_year" runat="server" Width="54px"></asp:DropDownList>          
<sv:RequiredFieldValidator ID="frma2_RequiredFieldValidator9" runat="server" ControlToValidate="frma2_year" ErrorMessage="You must select Year of graduation" ValidationGroup="frma2_group4">*</sv:RequiredFieldValidator>
</td>

</tr>
</table> 
</td>
</tr><tr>
<td colspan="2" >
<br />
</td>
</tr>
<tr> 
<td colspan="2"  align="center"> 	
        <asp:Button ID="frma2_btn_clear" runat="server" Text="Clear" CssClass="btncolor"  OnClick="frma2_btn_clear_Click" />&nbsp;&nbsp;<asp:Button 
            ID="frma2_btn_submit" CssClass="btncolor"
            runat="server" Text="Submit" OnClick="frma2_btn_submit_Click" 
            ValidationGroup="frma2_group" CausesValidation="False" />
  </td> 
</tr> 
</table>				
<sv:ValidationSummary id="frma2_summary" runat="server" Width="748px" ValidationGroup="frma2_group" ></sv:ValidationSummary><sv:ValidationSummary id="frma2_summary1" runat="server" Width="748px" ValidationGroup="frma2_group1" CssClass="listtype"></sv:ValidationSummary>
<sv:ValidationSummary ID="frma2_summary2" runat="server" ValidationGroup="frma2_group2" Width="748px" ></sv:ValidationSummary>
<sv:ValidationSummary ID="frma2_summary3" runat="server" ValidationGroup="frma2_group3" Width="748px"  ></sv:ValidationSummary><sv:ValidationSummary ID="frma2_summary4" runat="server" ValidationGroup="frma2_group4"  Width="748px" CssClass="listtype" ></sv:ValidationSummary>            

  </td> 
	</tr>	
</table>
                                                                                                 
</asp:Content>