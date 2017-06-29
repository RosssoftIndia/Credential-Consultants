<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Editpurpose.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Editpurpose" %>

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
      <div class="headertag">Purpose</div><br />
           <table class="search_css">          
            <tr> 
               <td> 
                    What is the purpose of this service : <span style="color:Red;">*</span><asp:DropDownList
                        ID="frm4_option_purpose" runat="server" Width="251px" AutoPostBack="True" OnSelectedIndexChanged="frm4_option_purpose_SelectedIndexChanged" AppendDataBoundItems="true" ValidationGroup="frm4_group" >
                        <asp:ListItem Value="0" >Select</asp:ListItem></asp:DropDownList><sv:RequiredFieldValidator ID="frm4_RequiredFieldValidator1" runat="server" ControlToValidate="frm4_option_purpose"
                       ErrorMessage="You must select a purpose" InitialValue="0" ValidationGroup="frm4_group">*</sv:RequiredFieldValidator></td></tr>
             <tr id="frm4_optional" runat="server" visible="false">
               <td style="text-align: left" >
                  <%-- Note: Enter Name if you are applying for only one Instiution--%>Which educational institution referred you to us?<br />
                   <br />
                  <%-- Name of Institution applying to:--%><asp:TextBox ID="frm4_institution" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td></tr>
             <tr id="frm4_optional1" runat="server" visible="false">
               <td >
               <%-- Note: Enter Name if you are applying for only one Organization--%>Which employer referred you to us?<br />
                <br />
               <%--Name of Organization applying to--%><asp:TextBox ID="frm4_organization" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td></tr>
              <tr id="frm4_optional2" runat="server" visible="false">
               <td >
                <%--Note: Enter Name if you are applying for only one Attorney or Law firm--%>Which Attorney or Law firm referred you to us?<br />
                   <br />
               <%--Name of Attorney or Law firm referred you to us:--%><asp:TextBox ID="frm4_lawfirm" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td></tr>
               <tr id="frm4_optional3" runat="server" visible="false">
               <td >
                Note: Enter Name if you are applying for only one Board<br />
                   <br />
               Name of Board from which you seek licensing: <asp:TextBox ID="frm4_board" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>&nbsp;state:<asp:TextBox 
                       ID="frm4_state" runat="server" Width="100px" MaxLength="50" ></asp:TextBox></td></tr>
                <tr id="frm4_optional4" runat="server" visible="false">
               <td >
               <%-- Note: Enter Name if you are applying for only one Military Recruiter--%>which Military Recruiter referred you to us?<br />
                   <br />
               <%--Name of Military Recruiter who referred you to us:--%><asp:TextBox ID="frm4_military" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td></tr>
                 <tr id="frm4_optional5" runat="server" visible="false">
               <td >
              <%--  Note: Enter Purpose of your Evaluation--%>How did you hear about us?<br />
                   <br />
               <%--How are you going to use your Evaluation:--%><asp:TextBox ID="frm4_evaluation" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td></tr>
                  <tr align="right"><td><asp:Button 
                    ID="personalinfoupdate" runat="server"  CssClass="btncolor" Text="update" onclick="updatebtn_Click" /></td></tr>
                  <tr><td> <sv:ValidationSummary id="frm4_Summary" runat="server" CssClass="error_box_summary" ValidationGroup="frm4_group"  />  </td></tr>
                </table>
  </td>
</tr>

</table>                                                                                               
</asp:Content>