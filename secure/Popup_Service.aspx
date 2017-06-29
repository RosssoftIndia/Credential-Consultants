<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Service.aspx.cs" MasterPageFile="~/secure/popupMaster.master" Inherits="secure_Popup_Service" %>

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
         <div class="headertag">Basic Service Type</div>     
         <br />        
         <table width="100%"><tr>
         <td>
           <asp:GridView ID="servicegrid" runat="server" AutoGenerateColumns="False"  
                 DataKeyNames="id"  style="TEXT-ALIGN: left" CssClass="gridview_css" 
                 PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                 ondatabound="servicegrid_DataBound" >
                   <Columns>
            <asp:TemplateField ShowHeader="False" SortExpression="Name">                           
                           <ItemTemplate>                              
                               <input id="Checkbox1" runat="server"  value='<%# Eval("id") %>' type="checkbox" />                               
                               <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                 <asp:DropDownList  ID="drpCount" Visible="false"  runat="server"></asp:DropDownList> 
                           </ItemTemplate>                
                       </asp:TemplateField>
                       <%--<asp:TemplateField>
                           <ItemTemplate>
                           <center>
                               <a id="link" href='<%# Eval("id", "Service_description.aspx?cus={0}") %>' class='iframe'  title="Description :: Information Block. :: width:500, height:300">Description</a>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>  --%>                    
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
       <tr><td>
        <br />
           <div class="headertag">Additional Services</div>  
           <br />         
        <table width="100%"> 
      <tr id="frm6_optional" runat="server">
        <td>
        <table width="100%">           
               <tr>
               <td>
             <asp:GridView ID="addservicegrid" runat="server" AutoGenerateColumns="False" 
                       DataKeyNames="id" style="TEXT-ALIGN: left" 
                       CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" ondatabound="addservicegrid_DataBound" >
                   <Columns>
            <asp:TemplateField ShowHeader="False" SortExpression="Name">                           
                           <ItemTemplate>                              
                               <input id="Checkbox2" runat="server"  value='<%# Eval("id") %>' type="checkbox" />
                               <asp:Label ID="Label3" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                               <asp:DropDownList  ID="drpCount" Visible="false"  runat="server"></asp:DropDownList>   
                           </ItemTemplate>            
                       </asp:TemplateField>
                       <%--<asp:TemplateField>
                           <ItemTemplate>
                           <center>
                               <a id="link" href='<%# Eval("id", "Service_description.aspx?cus={0}") %>' rel="iframe"  class='iframe' title="Description :: Information Block. :: width:500, height:300">Description</a>
                               </center>
                           </ItemTemplate>
                       </asp:TemplateField>--%>
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
              
	</table>   
	</td></tr>
	<tr align="right"><td><asp:Button ID="serviceAdd" runat="server"  
            CssClass="btncolor" Text="Add" onclick="btn_Click" Visible="False" />
        <asp:Button ID="serviceupdate" runat="server"  CssClass="btncolor" 
            Text="update" onclick="btn_Click" Visible="False" /></td></tr>
	 </table>                                                                                           
</asp:Content>