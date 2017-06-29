<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pdf</title>
 <link href="PrintStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /> 
       <div id="Pdfsave"  runat="server">   
          <table style="width:750px;">
          <!-- Client Name -->
          <tr>
          <td>        
          <div id="OrgTitlepdf" runat="server" ></div>
          </td>
          </tr>
          <!-- Personal Info -->
          <tr><td align="center" style="font-size:15px;font-weight:bold;">Personnal Information<br /><br /></td></tr>  
          <tr>
          <td>               
           <asp:DetailsView ID="Applicant" runat="server"  AutoGenerateRows="False" GridLines="None" style="text-align: left;font-size:10px;width:750px;" OnDataBound="Applicant_DataBound" OnLoad="Applicant_Load">
                       <Fields>
                           <asp:TemplateField HeaderText="File Number" SortExpression="Email">                              
                               <ItemTemplate>                         
                               <div><b><%# Eval("FileNumber") %></b></div>                                                        
                               </ItemTemplate> 
                                 <HeaderStyle Width="180px" />                              
                             </asp:TemplateField>
                             <asp:TemplateField>
                             <ItemTemplate >
                             
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
                                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label>
                                   <asp:Label ID="Label21" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label>
                                   <asp:Label ID="lblcity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                   <asp:Label ID="Label14" runat="server" Text='<%# Bind("State_or_province") %>'></asp:Label>
                                   <asp:Label ID="Label15" runat="server" Text='<%# Bind("Zip_or_PostalCode") %>'></asp:Label>
                                   <asp:Label ID="Label16" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="HomePhone" HeaderText="Primary Phone" SortExpression="HomePhone" />
                           <asp:BoundField DataField="WorkPhone" HeaderText="Secondary Phone" SortExpression="WorkPhone" />
                           <asp:BoundField DataField="MobilePhone" HeaderText="Mobile Phone" SortExpression="MobilePhone" />
                           <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />                                            
                       </Fields>                     
                   </asp:DetailsView>          
          </td>
          </tr>
          <!-- Payment Info -->
           <tr><td align="center" style="font-size:15px;font-weight:bold;">Payment Mode<br /><br /></td></tr>    
          <tr id="paymentblock" runat="server">
               <td>                          
               <table style="width:750px;">
               <tr>
               <td style="font-size:10px;width:180px">Payment Mode</td>
               <td style="font-size:10px;text-align:left">
                   <asp:Label ID="pymode" Font-Size="10px"  runat="server"></asp:Label>
                   </td>
               </tr>
                <tr id="Tyblk" runat="server">
               <td style="font-size:10px;"><b>Creditcard Type</b></td>
               <td style="font-size:10px;">
                   <asp:Label ID="Ctype" Font-Size="12px" runat="server"></asp:Label>
                    </td>
               </tr>
               </table> 
               </td>
               </tr>        
          <!-- Education Info -->
          <tr  id="edu" runat="server">
            <td>                          
                  <table>  
                   <tr><td align="center" style="font-size:15px;font-weight:bold;">Educational History<br /><br /></td></tr>                     
                   <tr>
                   <td>                  
                
                </td>
                </tr>                    
                <tr>              
               <td align="left"  style="font-size:11px;color:Gray;font-weight:bold;">                
               HighSchool History<br /><br />
               </td>
               </tr>
               <tr>
               <td style="text-align: left">
               <asp:GridView ID="hischoolgrid" runat="server" AutoGenerateColumns="False" 
                      ForeColor="#333333" GridLines="None"  
                        style="TEXT-ALIGN: left;font-size:10px;width:750px;" OnLoad="hischoolgrid_Load" 
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
                        <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>                        
                   </asp:GridView>      
                   <br /><br />                             
               </td>
               </tr>    
               <tr>
               <td align="left" style="font-size:11px;color:Gray;font-weight:bold;">                
               University History<br /><br />
             </td>
               </tr>
                <tr>
               <td style="text-align: left"> 
               <asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" 
                      ForeColor="#333333" GridLines="None" 
                       OnLoad="univgrid_Load" 
                       ondatabound="univgrid_DataBound"  style="TEXT-ALIGN: left;font-size:10px;width:750px;">                      
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
                        <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>                         
                   </asp:GridView>     
                   <br /><br />                                 
               </td>
               </tr>  
               </table>
               
            </td>
            </tr> 
          <!-- Purpose Info -->
          <tr>
            <td>
               <table>    
                <tr><td align="center" style="font-size:15px;font-weight:bold;">Purpose<br /><br /></td></tr>                                 
               <tr>
               <td style="text-align: left">               
                   <asp:DetailsView ID="purposegrid" runat="server" 
                       AutoGenerateRows="False" GridLines="None" 
                       OnLoad="purposegrid_Load" ondatabound="purposegrid_DataBound"  style="TEXT-ALIGN: left;font-size:10px;width:750px;">
                       <Fields>
                         <asp:TemplateField HeaderText="Main Purpose:" SortExpression="Evaluation_Name">
                               <ItemTemplate>
                                   <asp:Label ID="Eval_Name" runat="server" Text='<%# Bind("Evaluation_Name") %>'></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="180px" />
                           </asp:TemplateField>                                                      
                           <asp:BoundField DataField="Name" HeaderText="Report Type:" SortExpression="Name" />
                            <asp:TemplateField HeaderText="Target Institution:" SortExpression="DateOfBirth">
                               <ItemTemplate>
                                   <asp:Label ID="institution" runat="server" Text='<%# Bind("Eval_institution") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Fields>                        
                   </asp:DetailsView>  
                   <br /><br />         
               </td>
               </tr>  
               </table>
                 
            
            </td>
            </tr>   
          <!-- Service Info -->  
          <tr>
            <td>
<table>    
             <tr><td align="center" style="font-size:15px;font-weight:bold;">Service<br /><br /></td></tr>                                                                                          
                 <tr>
                     <td style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">                                  
                        General Service<br /><br />
                        </td>
                        </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                OnDataBound="service1grid_DataBound" OnLoad="service1grid_Load" 
                                ShowFooter="True"  style="TEXT-ALIGN: left;font-size:10px;width:750px;" >
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
                                 <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>                                 
                            </asp:GridView>
                            <br /><br />
                        </td>
                        </tr>
                        <tr id="Revsec_Additionalheader" runat="server"  >
                     <td style="text-align:left;color:Gray;font-size:11px;font-weight:bold;"> 
                                Additional Official Hard Copy Service<br /><br />                                
                                </td>
                        </tr>
                        <tr id="Revsec_Additional" runat="server">
                            <td style="text-align: left">
                                <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="copychargergrid_DataBound" 
                                    OnLoad="copychargergrid_Load" ShowFooter="True" style="TEXT-ALIGN: left;font-size:10px;width:750px;">                                  
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
                                    <EmptyDataTemplate>
                                        No Additional Official Hard Copy Requested
                                    </EmptyDataTemplate>   
                                     <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left" />                                    
                                </asp:GridView>
                                <br /><br />
                            </td>
                        </tr>
                               <tr id="Revsec_Emailheader" runat="server"   >
                     <td style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">                                    
                                Official Electronic Copy Service<br /><br />
                                </td>
                        </tr>
                        <tr id="Revsec_Email" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="email_grid_DataBound" OnLoad="email_grid_Load" 
                                    ShowFooter="True" style="TEXT-ALIGN: left;font-size:10px;width:750px;" >                                  
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
                                    <EmptyDataTemplate>
                                        No Official Electronic Copy Requested
                                    </EmptyDataTemplate>     
                                    <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left" />                                 
                                </asp:GridView>
                                <br /><br />
                            </td>
                        </tr>
                          <tr id="Revsec_Faxheader" runat="server"   >
                     <td  style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">  
                                Fax Copy Service<br /><br />
                                </td>
                        </tr>
                        <tr id="Revsec_Fax" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="fax_grid_DataBound" OnLoad="fax_grid_Load" 
                                    ShowFooter="True" style="TEXT-ALIGN: left;font-size:10px;width:750px;" >                                    
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
                                    </Columns>                               
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate> 
                                      <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>                                    
                                </asp:GridView>
                                <br /><br />
                            </td>
                        </tr>
                        <tr  >
                     <td style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">                                    
                                Delivery Service - Official Hard Copy(ies) &amp; Additional Copies<br /><br />
                                </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:GridView ID="Delivery_Grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    OnDataBound="Delivery_Grid_DataBound" OnLoad="Delivery_Grid_Load" 
                                    ShowFooter="True" style="TEXT-ALIGN: left;font-size:10px;width:750px;" >
                                    <EmptyDataTemplate>
                                        No Service Requested
                                    </EmptyDataTemplate>
                                      <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>  
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
                                </asp:GridView>
                                <br /><br />
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="font-size:10px;">
                                <b>Total Amount Due =</b>
                                <asp:Label ID="Reviewcost" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                <asp:Button ID="refresh" runat="server" CausesValidation="False" 
                                    OnClick="refresh_Click" Text="refresh" Visible ="false"  />
                            </td>
                        </tr>                        
                   
</table>
      
            </td>
            </tr> 
          <!-- Delivery Info -->  
          <tr>
        <td>
         <table>
	             <tr>
		        <td  align="center"  style="font-size:15px;font-weight:bold;">Delivery Details<br /><br /></td></tr>
		          <tr  >
                     <td  style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">    
                                
                                Delivery Details - Official Hard Copy(ies) &amp; Additional Copies<br /><br />
                                </td>
                        </tr>
		        <tr>
		        <td>
    <asp:GridView ID="deliverydetails" runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                     style="TEXT-ALIGN: left;font-size:10px;width:750px;" onload="deliverydetails_Load">                   
                     <EmptyDataTemplate>
                         No Delivery Details Available
                     </EmptyDataTemplate>  
                     <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left" />    
                       <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left"/>                   
                     <Columns>
                         <asp:BoundField DataField="Name" HeaderText="Recipient" SortExpression="Name" />
                         <asp:BoundField DataField="Count" HeaderText="No of copies" 
                             SortExpression="Count" />
                         <asp:TemplateField HeaderText="Address">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label>                               
                                 <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label>
                                 <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label>
                                 <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="Type" HeaderText="Type of Copy" 
                             SortExpression="Type" Visible="false" />
                     </Columns>
                 </asp:GridView>
     <br /><br />
                           </td> 
                           </tr> 
                            <tr>
                     <td style="text-align:left;color:Gray;font-size:11px;font-weight:bold;">                                    
                                Fax Copy Delivery Details<br /><br />
                                </td>
                        </tr>
                           <tr><td>
                           <asp:GridView ID="fax_details" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnLoad="fax_details_Load" 
                                    ShowFooter="True" style="TEXT-ALIGN: left;font-size:10px;width:750px;" >                                 
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
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested                                      
                                    </EmptyDataTemplate>  
                                         
                                   <EmptyDataRowStyle Font-Size="10px" HorizontalAlign="Left" />                           
                                </asp:GridView>
                            <br /><br />
                           </td></tr>
                           
                           
                           
                           </table> 
          
        </td>
        </tr>  
               </table>
          </div> 
          
          <div id="pdfsign"  runat="server">   
          <table style="width:750px;">
          <!-- SignIn Info -->  
          <tr>
        <td>
         
        <table  style="width:750px;">
            <tr>
                <td style="font-size:10px;" >
                    I agree to all terms and conditions set forth in the online application.
                    
                    
                    
                    Signed by_______________________________Date_________________</td><br /><br /><br /><br />
            </tr>
        </table>
       
        </td>
        </tr>
          <!-- CreditCard Info -->  
          <tr>
            <td  id="creditblog" runat="server" visible="false">
            
               <table style="width:750px;">
	            <tr>
		        <td  align="center" style="font-size:15px;font-weight:bold;">Credit Card Details<br /><br /></td></tr>
		        <tr>
		        <td> 
                    <table style="width:750px;">
                        <tr>
                            <td style="width: 366px; height: 26px;font-size:10px;">
                                Card Type
                                (You must use one of the accepted card types)</td>
                            <td colspan="2" style="width: 423px;font-size:10px;">
                                &nbsp; &nbsp; &nbsp;&nbsp;
                                _______________________________________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 26px;font-size:10px;">
                                Card Number</td>
                            <td colspan="2" style="width: 423px; height: 26px;font-size:10px;">
                                _______________________________________________</td>
                        </tr>
                        <tr>
                            <td style="width: 366px; height: 27px;font-size:10px;">
                                Card Expiration Date</td>
                            <td style="width: 423px; height: 27px;font-size:10px;" colspan="2">
                                ______________Security Code_________________</td>
                        </tr>
                          <tr>
                            <td style="width: 366px;height: 27px;font-size:10px;">
                                Name of Cardholder
                                (as it appears on the card)</td>
                            <td  style="width: 423px; height: 27px;font-size:10px;" colspan="2">
                                _______________________________________________</td>
                        </tr>
                      
                          <tr>
                            <td style="width: 366px;font-size:10px;">Billing Address Street</td>
                            <td style="width: 423px; height: 27px;font-size:10px;" colspan="2">
                                _______________________________________________</td>
                        </tr>                       
                         <tr>
                            <td style="width: 366px; height: 27px;font-size:10px;">
                                Billing Address City</td>
                            <td style="width: 423px; height: 27px;font-size:10px;" colspan="2">
                                ______________State/Province_____Zipcode__________</td>
                        </tr>                       
                        <tr>
                            <td style="width: 366px; height: 31px;font-size:10px;">
                                Cardholders Signature</td>
                            <td style="width: 423px; height: 27px;font-size:10px;" colspan="2">
                                ________________________________</td>
                        </tr>
                        <tr>    
                         <td colspan="5" style="font-size:10px;"> 
                         
                         <b>Note:</b> The Security code is the last 3-digits of a longer number on the back of the card, or a 4-digit number listed on the front of your card.                     
                         
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

