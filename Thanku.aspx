<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Thanku.aspx.cs" MasterPageFile="~/AppMaster.master"  Inherits="Thanku" %>

<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  
<style type="text/css">
        .style4
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Homeheader" ContentPlaceHolderID ="Header" runat="server">  
  <table style="width:100%;min-height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient">Online Payment</td></tr>
 </table></td></tr>
 </table>  	
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
    <br />
<br />     
    <table width="100%"><tr><td>
     <br />
        <div class="headertag">Thank You</div>
        <br />       
             <table class="search_css" >           
                     <tr><td colspan="4" id="msg" runat="server"></td></tr>
             </table> 
             </td></tr></table>
                  <table id="processctrl" runat="server"  width="100%">                            
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
          <br />
<br />                                                                                                    
</asp:Content>
