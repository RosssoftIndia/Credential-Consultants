<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Printapplication.aspx.cs" Inherits="Printapplication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print</title>
    <link href="PrintStyle.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript">
function printit(){  
if (window.print) {
    window.print() ;  
} else {
    var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
document.body.insertAdjacentHTML('beforeEnd', WebBrowser);
    WebBrowser1.ExecWB(6, 6);//Use a 1 vs. a 2 for a prompting dialog box    WebBrowser1.outerHTML = "";  
}
}
</script>

<script type="text/javascript">  
var NS = (navigator.appName == "Netscape");
var VERSION = parseInt(navigator.appVersion);
if (VERSION > 3) {
    document.write('<form><input type=button value="Print Application" name="Print" onClick="printit()"></form>');        
}
</script>
    <style type="text/css">
        .style1
        {
            width: 143px;
        }
    </style>  
                <script type="text/javascript">
                    var _gaq = _gaq || [];
                    _gaq.push(['_setAccount', 'UA-20673314-1']);
                    _gaq.push(['_setDomainName', '.credentialconnection.com']);
                    _gaq.push(['_trackPageview']);

                    (function() {
                        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
                    })();

</script>      
</head>
<body>
    <form id="form1" runat="server">
    <div>    
       <table width="750" border="0" style=" height:32px;" align="center"   cellpadding="0" cellspacing="0"> 
      <tr >
        <td>
        <br /> 
    <hr />
    <div class="proj_block">
    <br />
    <div><div id="OrgTitle" runat="server" class="proj_header"></div></div>
     <br />   
     </div>             
    <hr />
    <br />   
       </td>
       </tr>
      <tr >
        <td>
        <br /> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="refresh"/> 
            </Triggers>
            <ContentTemplate>
            <table>
            <tr>
            <td>
            <table width="750" align="center" class="BorderedTable" cellpadding="4" > 
             <tr class="TableHeader"><td align="center" style="text-align: left">Personnal Information</td></tr>       
            <tr> 
               <td style="text-align: left">               
                   <asp:DetailsView ID="Applicant" runat="server" Height="50px" Width="420px" AutoGenerateRows="False" GridLines="None" style="text-align: left" OnDataBound="Applicant_DataBound" OnLoad="Applicant_Load">
                       <Fields>
                           <asp:TemplateField HeaderText="File Number" SortExpression="Email">                              
                               <ItemTemplate>                         
                               <div><b><%# Eval("FileNumber") %></b></div>                             
                                  <%--  <asp:Label ID="Label4" runat="server" Text='<%# Bind("FileNumber") %>'></asp:Label>--%>
                               </ItemTemplate>                               
                             </asp:TemplateField>
                             <asp:TemplateField>
                             <ItemTemplate >
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
                            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                           <asp:TemplateField HeaderText="DateOfBirth" SortExpression="DateOfBirth">
                               <ItemTemplate>
                                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("DateOfBirth","{0:d}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="CountryOfBirth" SortExpression="CountryOfBirth">
                               <ItemTemplate>
                                   <asp:Label ID="countrybirth" runat="server" Text='<%# Bind("CountryBirth") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Address" SortExpression="Address">                              
                               <ItemTemplate>
                                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />
                                   <asp:Label ID="Label21" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                   <asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label><br />
                                   <asp:Label ID="Label14" runat="server" Text='<%# Bind("State_or_province") %>'></asp:Label><br />
                                   <asp:Label ID="Label15" runat="server" Text='<%# Bind("Zip_or_PostalCode") %>'></asp:Label><br />
                                   <asp:Label ID="Label16" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="HomePhone" HeaderText="Primary Phone" SortExpression="HomePhone" />
                           <asp:BoundField DataField="WorkPhone" HeaderText="Secondary Phone" SortExpression="WorkPhone" />
                           <asp:BoundField DataField="MobilePhone" HeaderText="Mobile Phone" SortExpression="MobilePhone" />
                           <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />                                            
                       </Fields>
                         <FooterStyle CssClass="gridview_footer" />
                       <RowStyle CssClass="gridview_rowstyle" />
                       <PagerStyle CssClass="gridview_pager" />
                       <FieldHeaderStyle CssClass="gridview_header" />
                       <HeaderStyle CssClass="gridview_header" />
                       <AlternatingRowStyle CssClass="gridview_alternative" />
                   </asp:DetailsView>                        
               </td>                                    
               </tr> 
                <tr class="TableHeader"><td align="center" style="text-align: left">Payment Mode</td></tr>    
               <tr id="paymentblock" runat="server">
               <td>
               <table style="width: 420px">
               <tr>
               <td class="style1"><b>Payment Mode</b></td>
               <td>
                   <asp:Label ID="pymode" runat="server"></asp:Label>
                   </td>
               </tr>
                <tr id="Tyblk" runat="server">
               <td class="style1"><b>Creditcard Type</b></td>
               <td>
                   <asp:Label ID="Ctype" runat="server"></asp:Label>
                    </td>
               </tr>
               </table>     
               </td>
               </tr>
                         </table>
                         <br />                         
            </td>
            </tr>
            <tr  id="edu" runat="server">
            <td>
                          
                  <table width="750" align="center" class="BorderedTable" cellpadding="4" >  
                   <tr class="TableHeader"><td align="center" style="text-align: left">Educational History</td></tr>                     
                   <tr>
                   <td>                  
                <br />
                </td>
                </tr>                    
                <tr>              
               <td class="TableHeader" style="text-align: center">                
               HighSchool History
               </td>
               </tr>
               <tr>
               <td style="text-align: left">
               <asp:GridView ID="hischoolgrid" runat="server" AutoGenerateColumns="False" 
                       Width="715px" CellPadding="4" ForeColor="#333333" GridLines="None"  
                       style="text-align: center" OnLoad="hischoolgrid_Load" 
                       ondatabound="hischoolgrid_DataBound">                      
                       <Columns>
                           <asp:TemplateField HeaderText="Institution" SortExpression="Name">
                               <ItemTemplate>
                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Country" SortExpression="Country">                             
                               <ItemTemplate>
                                   <asp:Label ID="Country" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Date of Graduation" SortExpression="DateDegreeAwarded">                              
                               <ItemTemplate>
                                   <asp:Label ID="grad" runat="server" Text='<%# Bind("DateDegreeAwarded") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
                                                                           
                       </Columns>                      
                       <EmptyDataTemplate>
                           No High school history entered
                       </EmptyDataTemplate>
                      <RowStyle CssClass="gridview_rowstyle" />
              <HeaderStyle CssClass="gridview_header" />
              <AlternatingRowStyle CssClass="gridview_alternative" />
              <FooterStyle CssClass="gridview_footer" />           
              <PagerStyle CssClass="gridview_pager" />          
                   </asp:GridView>                                   
               </td>
               </tr>    
               <tr>
               <td class="TableHeader" style="text-align: center">                
               University History
               </td>
               </tr>
                <tr>
               <td style="text-align: left"> 
               <asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" 
                       Width="715px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                       style="text-align: center" OnLoad="univgrid_Load" 
                       ondatabound="univgrid_DataBound">                      
                       <Columns>
                           <asp:BoundField DataField="Name" HeaderText="Institution" SortExpression="Name" />
                            <asp:TemplateField HeaderText="Country" SortExpression="Country">                             
                               <ItemTemplate>
                                   <asp:Label ID="Country" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                           <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                           <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />                         
                           <asp:TemplateField HeaderText="Date of Graduation" SortExpression="DateDegreeAwarded">                              
                               <ItemTemplate>
                                   <asp:Label ID="grad" runat="server" Text='<%# Bind("DateDegreeAwarded") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
                       </Columns>                      
                       <EmptyDataTemplate>
                           No University History entered
                       </EmptyDataTemplate>
                       <RowStyle CssClass="gridview_rowstyle" />
              <HeaderStyle CssClass="gridview_header" />
              <AlternatingRowStyle CssClass="gridview_alternative" />
              <FooterStyle CssClass="gridview_footer" />           
              <PagerStyle CssClass="gridview_pager" />          
                   </asp:GridView>                                      
               </td>
               </tr>  
               </table>
               <br />
            </td>
            </tr>
            <tr>
            <td>
                    <table width="750" align="center" class="BorderedTable" cellpadding="4" >    
                <tr class="TableHeader"><td align="center" style="text-align: left">Purpose</td></tr>                                 
               <tr>
               <td style="text-align: left">               
                   <asp:DetailsView ID="purposegrid" runat="server" Height="50px" Width="420px" 
                       AutoGenerateRows="False" GridLines="None" style="text-align: left" 
                       OnLoad="purposegrid_Load" ondatabound="purposegrid_DataBound">
                       <Fields>
                         <asp:TemplateField HeaderText="Main Purpose:" SortExpression="Evaluation_Name">
                               <ItemTemplate>
                                   <asp:Label ID="Eval_Name" runat="server" Text='<%# Bind("Evaluation_Name") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>                           
                           <asp:BoundField DataField="Name" HeaderText="Report Type:" SortExpression="Name" />
                            <asp:TemplateField HeaderText="Target Institution:" SortExpression="DateOfBirth">
                               <ItemTemplate>
                                   <asp:Label ID="institution" runat="server" Text='<%# Bind("Eval_institution") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Fields>
                         <FooterStyle CssClass="gridview_footer" />
                       <RowStyle CssClass="gridview_rowstyle" />
                       <PagerStyle CssClass="gridview_pager" />
                       <FieldHeaderStyle CssClass="gridview_header" />
                       <HeaderStyle CssClass="gridview_header" />
                       <AlternatingRowStyle CssClass="gridview_alternative" />
                   </asp:DetailsView>            
               </td>
               </tr>  
               </table>
               <br />            
            
            </td>
            </tr>
            <tr>
            <td>
                      
                    <table width="750" align="center" class="BorderedTable" cellpadding="4" >    
             <tr class="TableHeader"><td align="center" style="text-align: left">Service</td></tr>                                                                                          
                 <tr  class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">   
                     <br />            
                        General Service
                        <br />
                        <hr />
                        </td>
                      </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                OnDataBound="service1grid_DataBound" OnLoad="service1grid_Load" 
                                ShowFooter="True" style="text-align: center" Width="715px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Service Type" SortExpression="Name">
                                        <EditItemTemplate>
                                            &nbsp;
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Subtotal" SortExpression="Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label7" runat="server" style="border-top:solid 1px black;" Text="0"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No service requested
                                </EmptyDataTemplate>
                                <RowStyle CssClass="gridview_rowstyle" />
                                <HeaderStyle CssClass="gridview_header" />
                                <AlternatingRowStyle CssClass="gridview_alternative" />
                                <FooterStyle CssClass="gridview_footer" />
                                <PagerStyle CssClass="gridview_pager" />
                            </asp:GridView>
                        </td>
                        </tr>
                        <tr id="Revsec_Additionalheader" runat="server"  class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">     
                     <br />          
                                Additional Official Hard Copy Service                                
                                <br />
                                <hr />
                                </td>
                        </tr>
                        <tr id="Revsec_Additional" runat="server">
                            <td style="text-align: left">
                                <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="copychargergrid_DataBound" 
                                    OnLoad="copychargergrid_Load" ShowFooter="True" style="text-align: center" 
                                    Width="715px">
                                    <FooterStyle CssClass="gridview_footer" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                        <asp:TemplateField SortExpression="id">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" style="border-top:solid 1px black;" Text="0"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="copychargergrid_del" runat="server" 
                                                    CausesValidation="False" ImageUrl="~/images/remove.png" 
                                                    OnClick="copychargergrid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="gridview_rowstyle" />
                                    <EmptyDataTemplate>
                                        No Additional Official Hard Copy Requested
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="gridview_pager" />
                                    <HeaderStyle CssClass="gridview_header" />
                                    <AlternatingRowStyle CssClass="gridview_alternative" />
                                </asp:GridView>
                            </td>
                        </tr>
                               <tr id="Revsec_Emailheader" runat="server"   class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">  
                     <br />             
                                Official Electronic Copy Service
                                <br /><hr /></td>
                        </tr>
                        <tr id="Revsec_Email" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="email_grid_DataBound" OnLoad="email_grid_Load" 
                                    ShowFooter="True" style="text-align: center" Width="715px">
                                    <FooterStyle CssClass="gridview_footer" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                        <asp:TemplateField SortExpression="id">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" style="border-top:solid 1px black;" Text="0"></asp:Label>                                           
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="email_grid_del" runat="server" CausesValidation="False" 
                                                    ImageUrl="~/images/remove.png" OnClick="email_grid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="gridview_rowstyle" />
                                    <EmptyDataTemplate>
                                        No Official Electronic Copy Requested
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="gridview_pager" />
                                    <HeaderStyle CssClass="gridview_header" />
                                    <AlternatingRowStyle CssClass="gridview_alternative" />
                                </asp:GridView>
                            </td>
                        </tr>
                          <tr id="Revsec_Faxheader" runat="server"   class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">  
                     <br />             
                                Fax Copy Service
                                <br /><hr /></td>
                        </tr>
                        <tr id="Revsec_Fax" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="fax_grid_DataBound" OnLoad="fax_grid_Load" 
                                    ShowFooter="True" style="text-align: center" Width="715px">
                                    <FooterStyle CssClass="gridview_footer" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                        <asp:TemplateField SortExpression="id">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <FooterTemplate>
                                                <asp:Label ID="Label11" runat="server" style="border-top:solid 1px black;" Text="0"></asp:Label>                                           
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="False">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="fax_grid_del" runat="server" CausesValidation="False" 
                                                    ImageUrl="~/images/remove.png" OnClick="fax_grid_del_Click" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="gridview_rowstyle" />
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="gridview_pager" />
                                    <HeaderStyle CssClass="gridview_header" />
                                    <AlternatingRowStyle CssClass="gridview_alternative" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr  class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">    
                     <br />           
                                Delivery Service - Official Hard Copy(ies) &amp; Additional Copies
                                <br /><hr /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:GridView ID="Delivery_Grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    OnDataBound="Delivery_Grid_DataBound" OnLoad="Delivery_Grid_Load" 
                                    ShowFooter="True" style="text-align: center" Width="715px">
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
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label8" runat="server" style="border-top:solid 1px black;" Text="0"></asp:Label>                                             
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
                                    <RowStyle CssClass="gridview_rowstyle" />
                                    <HeaderStyle CssClass="gridview_header" Width="200px" />
                                    <EditRowStyle Width="200px" />
                                    <AlternatingRowStyle CssClass="gridview_alternative" />
                                    <FooterStyle CssClass="gridview_footer" />
                                    <PagerStyle CssClass="gridview_pager" />
                                </asp:GridView>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>Total Amount Due =</b>
                                <asp:Label ID="Reviewcost" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                <asp:Button ID="refresh" runat="server" CausesValidation="False" 
                                    CssClass="refreshbtn" OnClick="refresh_Click" Text="refresh" />
                            </td>
                        </tr>
                    </tr>
        </table>  
            <br />
           <%-- </td>
            </tr>
            </table> --%>            
        </ContentTemplate>       
         </asp:UpdatePanel>           
        </td>
        </tr>   
        <tr>
        <td>
         <table width="750" align="center" class="BorderedTable" cellpadding="4">
	             <tr class="TableHeader">
		        <td  align="center">Delivery Details</td></tr>
		          <tr  class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">    
                     <br />           
                                Delivery Details - Official Hard Copy(ies) &amp; Additional Copies
                                <br /><hr /></td>
                        </tr>
		        <tr>
		        <td>
    <asp:GridView ID="deliverydetails" runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                     style="TEXT-ALIGN: center" Width="715px" onload="deliverydetails_Load">
                     <FooterStyle CssClass="gridview_footer" />
                     <RowStyle CssClass="gridview_rowstyle" />
                     <EmptyDataTemplate>
                         No Delivery Details Available
                     </EmptyDataTemplate>
                     <PagerStyle CssClass="gridview_pager" />
                     <HeaderStyle CssClass="gridview_header" Width="150px" />
                     <AlternatingRowStyle CssClass="gridview_alternative" />
                     <Columns>
                         <asp:BoundField DataField="Name" HeaderText="Recipient" SortExpression="Name" />
                         <asp:BoundField DataField="Count" HeaderText="No of copies" 
                             SortExpression="Count" />
                         <asp:TemplateField HeaderText="Address">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                 <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                 <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                 <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="Type" HeaderText="Type of Copy" 
                             SortExpression="Type" Visible="false" />
                     </Columns>
                 </asp:GridView>
                           </td> 
                           </tr> 
                            <tr  class="TableHeaderdf">
                     <td style="text-align:left;color:Gray; ">    
                     <br />           
                                Fax Copy Delivery Details
                                <br /><hr /></td>
                        </tr>
                           <tr><td>
                           <asp:GridView ID="fax_details" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnLoad="fax_details_Load" 
                                    ShowFooter="True" style="text-align: center" Width="715px">
                                    <FooterStyle CssClass="gridview_footer" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Recipient" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="FaxNo">
                                        <ItemTemplate>
                                        <span><%# Eval("Faxno") %></span>
                                        </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="gridview_rowstyle" />
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="gridview_pager" />
                                    <HeaderStyle CssClass="gridview_header" />
                                    <AlternatingRowStyle CssClass="gridview_alternative" />
                                </asp:GridView>
                           </td></tr>
                           
                           
                           
                           </table> 
          
        </td>
        </tr>
    <tr>
        <td>
         <br />
        <table align="center" cellpadding="4" class="BorderedTable" width="750">
            <tr>
                <td>
                    I agree to all terms and conditions set forth in the online application.
                    <br />
                    <br />
                    <br />
                    Signed by_______________________________Date_________________</td>
            </tr>
        </table>
        <br />
        </td>
        </tr>
    <tr>
            <td  id="creditblog" runat="server" visible="false">
            <br />
               <table width="750" align="center" class="BorderedTable" cellpadding="4">
	            <tr class="TableHeader">
		        <td  align="center">Credit Card Details</td></tr>
		        <tr>
		        <td>
                    <table style="width: 676px;">
                        <tr>
                            <td style="width: 366px;">
                                Card Type<br />
                                (You must use one of the accepted card types)</td>
                            <td colspan="2" style="width: 423px">
                                &nbsp; &nbsp; &nbsp;&nbsp;<br />
                                _______________________________________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 26px;">
                                Card Number</td>
                            <td colspan="2" style="width: 423px; height: 26px">
                                _______________________________________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 27px;">
                                Card Expiration Date</td>
                            <td style="width: 423px; height: 27px;" colspan="2">
                                ______________Security Code_________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px;height: 27px;">
                                Name of Cardholder<br />
                                (as it appears on the card)</td>
                            <td  style="width: 423px; height: 27px;" colspan="2">
                                _______________________________________________</td>
                        </tr>
                      
                          <tr>
                            <td style="width: 366px;">Billing Address Street</td>
                            <td style="width: 423px; height: 27px;" colspan="2">
                                _______________________________________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 27px;">
                                Billing Address City</td>
                            <td style="width: 423px; height: 27px;" colspan="2">
                                ______________State/Province_____Zipcode__________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 31px">
                                Cardholders Signature</td>
                            <td style="width: 423px; height: 27px;" colspan="2">
                                ________________________________</td>
                        </tr>
                        <tr>    
                         <td colspan="5"> 
                         <br />
                         <b>Note:</b> The Security code is the last 3-digits of a longer number on the back of the card, or a 4-digit number listed on the front of your card.                     
                         <br />
                        </td>                    
                        </tr>
                    </table>
                </td>
	</tr>
</table>

            </td>
            </tr>                
	</table>    
    </div>
    </form>
</body>
</html>
