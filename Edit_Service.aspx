<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Service.aspx.cs" MasterPageFile="~/popupMaster.master" Inherits="Edit_Service" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>

<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server"> 
 <script language ="javascript" type="text/javascript" >

     ShowProcessMessage = function(PanelName) {
         window.scroll(0, 0);
         document.getElementById(PanelName).style.visibility = "visible";
         return true;
     }
                  </script>
</asp:Content>
   


<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />

 <table width="100%"> 
      <tr >
        <td>        
        <br />
<div class="headertag">Service</div>
<br />       
<table class="search_css">    
  <tr>
   <td style="text-align: left">
      <asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" style="text-align: center" ShowFooter="false" OnLoad="service1grid_Load" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">                          
           <Columns>
            <asp:TemplateField InsertVisible="False" SortExpression="id" Visible="false" >
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                   </ItemTemplate>
                   <ItemStyle Width="1px" />
               </asp:TemplateField>
               <asp:TemplateField InsertVisible="False" ShowHeader="False" SortExpression="Type" Visible="false" >
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("Type") %>' Visible="False"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Service Type" SortExpression="Name">
                  <ItemTemplate>
                       <asp:DropDownList ID="dp_val" runat="server" OnLoad="dp_val_Load">
                       </asp:DropDownList>
                       <asp:Label ID="temp" runat="server" Text='<%# Bind("Service_Id") %>' Visible="False"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                   <ItemTemplate>
                       &nbsp;<asp:Button ID="Update" runat="server" OnClientClick="ShowProcessMessage('ProcessingWindow')"  OnClick="Update_Click" Text="Update" />
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Description" SortExpression="Description">
                   <ItemTemplate>
                       <asp:Label ID="Label6" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Total Cost" SortExpression="Cost">
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                   </ItemTemplate>
                    <FooterTemplate >
                        &nbsp;
        </FooterTemplate>
               </asp:TemplateField>
           </Columns>                          
           <EmptyDataTemplate>
               No service requested
           </EmptyDataTemplate>         
       </asp:GridView>      
   </td>
   </tr>
</table>  
</td> 
</tr> 
</table> 
   
<center > <div id="ProcessingWindow"  style="visibility:hidden;" class="container1" > 
<div id="wrap" style="background-color: transparent">
<div id="top1" style="background-color: transparent"></div> <h2><asp:Image ID="Image1" runat="server" ImageUrl="~/images/InProcess.gif"/>Please Wait ...</h2>
<div id="bottom"></div></div></div> </center>    

<br />
<br />                                                                                                    
</asp:Content>