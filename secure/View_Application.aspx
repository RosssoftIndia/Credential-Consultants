<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_Application.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_View_Application" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >View Application</span>  
 <div class="submenu_style">            
             <div class="buttons">    
      
</div>  
		</div>
		<br />
		<br />       
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>
               <br />
                  <br />
                  <table width="100%"><tr><td>
                  <div style="float:right;">
            <table>           
            <tr align="center" valign="middle" >
<td><asp:ImageButton ID="btnedit" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png"/>
</td><td>Edit</td>
<td><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png"/>
</td><td>Manage</td>
<td><asp:ImageButton ID="btnreport" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png" />
</td><td>Notes</td>
<td><asp:ImageButton ID="btneval" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/links.png" />
</td><td>Evaluate</td>
<td><asp:ImageButton ID="btnattach" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/pdf_icon.png" />
</td><td>Attachments</td>
</tr>
                          </table>
                          </div>
                          </td></tr></table>
                        <br />
                        <br />
             <div class="headertag" style="border:none"  >
            <br />
            <br />
            <table>
            <tr><td>FileNo</td><td>:</td><td><asp:Label ID="lblfileno" runat="server" ></asp:Label></td></tr>
            <tr><td>Name</td><td>:</td><td><asp:Label ID="lblname" runat="server"></asp:Label></td></tr>
            <tr><td>Client</td><td>:</td><td><asp:Label ID="lblcompany" runat="server"></asp:Label></td></tr>
            </table> 
            <br />
            <br />             
            </div>
            <br />
            <br />
<div class="headertag" >Applicant Information</div>
<br />
<asp:DetailsView ID="DetailsView_personalinfo" runat="server" AutoGenerateRows="False" ondatabound="DetailsView_personalinfo_DataBound" OnLoad="DetailsView_personalinfo_Load" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">              
 <FieldHeaderStyle VerticalAlign="Top" Width="215px" />
                <Fields>
                  <asp:TemplateField HeaderText="File Number" SortExpression="Email">
                        <ItemTemplate>
                            <div>
                                <b><%# Eval("FileNumber") %></b></div>                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                               <ItemTemplate>             
                            First: <span><%# Eval("FirstName")%></span>&nbsp;<span style="color:Red;">|</span> Middle: <span><%# Eval("MiddleName")%></span>&nbsp;<span style="color:Red;">|</span> Last: <span><%# Eval("LastName")%></span>  
                               </ItemTemplate>
                           </asp:TemplateField>
                    <asp:BoundField DataField="Gender" HeaderText="Gender" 
                        SortExpression="Gender" />
                    <asp:BoundField DataField="othername" HeaderText="Alias Name" 
                        SortExpression="othername" />
                    <asp:TemplateField HeaderText="Date Of Birth" SortExpression="DateOfBirth">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateOfBirth","{0:d}") %>'></asp:Label>
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
                            <br />
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label>
                            <br />
                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                            <br />
                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label>
                            <br />
                            <asp:Label ID="Label15" runat="server" 
                                Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>
                            <br />
                            <asp:Label ID="Label16" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone" SortExpression="HomePhone">
                        <ItemTemplate>
                            Primary:<asp:Label ID="Label2" runat="server" Text='<%# Bind("HomePhone") %>'></asp:Label>
                            &nbsp;<span style="color:Red;">|</span> Secondary:<asp:Label ID="Label4" 
                                runat="server" Text='<%# Bind("WorkPhone") %>'></asp:Label>
                            &nbsp;<span style="color:Red;">|</span> Mobile:<asp:Label ID="Label5" runat="server" 
                                Text='<%# Bind("MobilePhone") %>'></asp:Label>
                            &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                     <asp:BoundField DataField="PreviousCredential_id" HeaderText="Previous Service" SortExpression="PreviousCredential_id" />                  
                </Fields>
            </asp:DetailsView> 
  <br />
<div class="headertag" >Applicant Purpose</div>
  <br />
  <asp:DetailsView ID="DetailsView_purpose" runat="server" AutoGenerateRows="False" 
                    ondatabound="DetailsView_purpose_DataBound" CssClass="detailview_css"  
                    PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">              
      <FieldHeaderStyle VerticalAlign="Top" Width="215px" />
      <PagerStyle CssClass="pgr" />
     <Fields>
    
                    <asp:TemplateField HeaderText="Purpose" SortExpression="Evaluation_Name">
                        <ItemTemplate>
                            <asp:Label ID="Eval_Name" runat="server" Text='<%# Bind("Evaluation_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Target Institution">
                        <ItemTemplate>
                            <asp:Label ID="institution" runat="server"  Text='<%# Bind("Eval_institution") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                     <asp:TemplateField HeaderText="Employer">
                        <ItemTemplate>
                            <asp:Label ID="organization" runat="server" Text='<%# Bind("Eval_organization") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attorney or Law firm">
                        <ItemTemplate>
                            <asp:Label ID="attorney" runat="server" Text='<%# Bind("Eval_Attorney") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>       
                              <asp:TemplateField HeaderText="Board">
                        <ItemTemplate>
                            <asp:Label ID="board" runat="server" Text='<%# Bind("Eval_Board") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            <asp:Label ID="city" runat="server" Text='<%# Bind("Eval_State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Military Recruiter">
                        <ItemTemplate>
                            <asp:Label ID="military" runat="server" Text='<%# Bind("Eval_Military_Recruiter") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="How did you hear about us?">
                        <ItemTemplate>
                            <asp:Label ID="other" runat="server" Text='<%# Bind("Eval_other") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Fields>           
                                         
                    <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView> 
    <br />
    <div id="senderblock" runat="server" >
           <div class="headertag"> Sender&#39;s Information</div>
           <br />
            <asp:DetailsView ID="DetailsView_Sender" runat="server" AutoGenerateRows="False"  CssClass="detailview_css" PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">              
      <FieldHeaderStyle VerticalAlign="Top" Width="215px" />
      <PagerStyle CssClass="pgr" />
     <Fields>   
     <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="sendername" runat="server" Text='<%# Bind("Senders_Name") %>'></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Contact">
                        <ItemTemplate>
                            <asp:Label ID="sendercontact" runat="server" Text='<%# Bind("Senders_Contact") %>'></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField> 
                    </Fields>           
                                         
                    <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView> 
                    </div> 
                 
     <br />
<div class="headertag" >Payment Information</div>
  <br />
   <asp:DetailsView ID="DetailsView_payment" runat="server" AutoGenerateRows="False" 
                    CssClass="detailview_css"  PagerStyle-CssClass="pgr"   
                    AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_payment_DataBound">       
                     <FieldHeaderStyle VerticalAlign="Top" Width="215px" />       
       <PagerStyle CssClass="pgr" />
     <Fields>
                    <asp:TemplateField HeaderText="Mode Of Payment">
                        <ItemTemplate>
                            <asp:Label ID="pymode" runat="server"  Text='<%# Bind("Paymentmode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Status">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="pystatus" runat="server" 
                                RepeatDirection="Horizontal" SelectedValue='<%# Eval("Paymentstatus") %>'>
                                <asp:ListItem Value="1">Transaction Complete</asp:ListItem>
                                <asp:ListItem Value="0">Transaction Incomplete</asp:ListItem>
                            </asp:RadioButtonList>                           
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Authorizecode" >
                        <ItemTemplate>
                            <asp:Label ID="autho" runat="server" Text='<%# Eval("Authorizecode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Transactioncode" >
                        <ItemTemplate>
                            <asp:Label ID="trans" runat="server" Text='<%# Eval("Transactioncode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
                    <AlternatingRowStyle CssClass="alt" />
            </asp:DetailsView> 
  <br />
  <br />
<div class="headertag" >HighSchool History</div>
<br />
<asp:GridView ID="hischoolgrid" runat="server" AutoGenerateColumns="False" ondatabound="hischoolgrid_DataBound" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
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
            <asp:TemplateField HeaderText="Date of Graduation" 
                SortExpression="DateDegreeAwarded">
                <ItemTemplate>
                    <asp:Label ID="grad" runat="server" Text='<%# Bind("DateDegreeAwarded") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No High school history entered
        </EmptyDataTemplate>  
    </asp:GridView>
    <br />
    <br />
<div class="headertag" >University History</div>
<br />       
<asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" ondatabound="univgrid_DataBound" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                     <Columns>                   
                         <asp:BoundField DataField="Name" HeaderText="Institution" 
                             SortExpression="Name" />
                         <asp:TemplateField HeaderText="Country" SortExpression="Country">
                             <ItemTemplate>
                                 <asp:Label ID="Country" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="Expr1" HeaderText="Degree" SortExpression="Expr1" />
                         <asp:BoundField DataField="StartDate" HeaderText="StartDate" 
                             SortExpression="StartDate" />
                         <asp:BoundField DataField="EndDate" HeaderText="EndDate" 
                             SortExpression="EndDate" />
                         <asp:TemplateField HeaderText="Date of Graduation" 
                             SortExpression="DateDegreeAwarded">
                             <ItemTemplate>
                                 <asp:Label ID="grad" runat="server" Text='<%# Bind("DateDegreeAwarded") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                     <EmptyDataTemplate>
                         No University History entered
                     </EmptyDataTemplate>                  
                 </asp:GridView>
 <br />
 <br />
<div class="headertag" >General Service</div>
<br />   
<asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                     <Columns>
                         <asp:TemplateField HeaderText="Service Type" SortExpression="Name">
                             <EditItemTemplate>
                                 &nbsp;
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Description" SortExpression="Description">
                             <ItemTemplate>
                                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                         <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                             </ItemTemplate>
                             <FooterTemplate>
                                 <asp:Label ID="Label7" runat="server" Text="0" Font-Bold="True"></asp:Label>
                             </FooterTemplate>
                         </asp:TemplateField>
                     </Columns>
                     <PagerStyle CssClass="pgr" />
                     <EmptyDataTemplate>
                         No service requested
                     </EmptyDataTemplate>                    
                     <AlternatingRowStyle CssClass="alt" />
                 </asp:GridView>
 <br />
 <br />
 <div id="revsec_Additional" runat="server" >
<div class="headertag" >Additional Official Hard Copy Service</div>
<br />       
 <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                       <Columns>
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
                         <asp:TemplateField HeaderText="Qty" SortExpression="Count">
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
                     </Columns>
                       <PagerStyle CssClass="pgr" />
                     <EmptyDataTemplate>
                         No Additional Copies Requested
                     </EmptyDataTemplate>                    
                       <AlternatingRowStyle CssClass="alt" />
                 </asp:GridView>
  </div>
  <br />
  <br />
 <div id="revsec_Email" runat="server" >
           <div class="headertag">Official Electronic Copy Service</div>
           <br />    
<asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" ShowFooter="True" style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                        </Columns>                                  
                         <PagerStyle CssClass="pgr" />
                        <EmptyDataTemplate>
                            No Electronic Copies Requested
                        </EmptyDataTemplate>                                   
                         <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
 </div> 
  <br />
  <br />
  <div id="revsec_Fax" runat="server" >
<div class="headertag" >Fax Copy Service</div>
<br />       
<asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  ShowFooter="True" style="text-align: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
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
                                       
                                    </Columns>                                  
                                    <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>                                  
                                   <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
 </div> 
 <br />
 <br />
<div class="headertag" >Delivery Service - Official Hard Copy(ies) &amp; Additional Copies</div>
<br /> 
 <asp:GridView ID="addongrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                    style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  
                    AlternatingRowStyle-CssClass="alt" ondatabound="addongrid_DataBound">
                     <PagerStyle CssClass="pgr" />
                     <EmptyDataTemplate>
                         No service requested
                     </EmptyDataTemplate>                   
                     <Columns>
                         <asp:TemplateField HeaderText="Delivery Type" SortExpression="Name">
                             <ItemTemplate>
                                 <asp:Label ID="lbldeliveryname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Service Type" SortExpression="Type" 
                             Visible="False">
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                 Copy
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty">
                             <ItemTemplate>
                                 1
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="No Of Copies" SortExpression="Count" 
                             Visible="False">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Count") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                             <ItemTemplate>
                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                             </ItemTemplate>
                             <FooterTemplate>
                                 <asp:Label ID="Label7" runat="server" Text="0" Font-Bold="True"></asp:Label>
                             </FooterTemplate>
                         </asp:TemplateField>
                     </Columns>
                     <AlternatingRowStyle CssClass="alt" />
                 </asp:GridView>
  <br />
  <br />
<b>Total Amount Due=</b><asp:Label ID="Reviewcost" runat="server" Text="0" Font-Bold="True"></asp:Label>
 <br />
 <br />
 <div class="headertag" >Delivery Details</div>
<br /> 
<asp:GridView ID="deliverydetails" runat="server" AutoGenerateColumns="False" 
                    style="TEXT-ALIGN: center" CssClass="gridview_css" PagerStyle-CssClass="pgr"  
                    AlternatingRowStyle-CssClass="alt" ondatabound="deliverydetails_DataBound">
                     <PagerStyle CssClass="pgr" />
                     <EmptyDataTemplate>
                         No Delivery Details Available
                     </EmptyDataTemplate>                    
              <Columns>
                         <asp:BoundField DataField="Name" HeaderText="Recipient" SortExpression="Name" />
                             <asp:TemplateField HeaderText="No of copies">                            
                             <ItemTemplate>
                               <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count") %>'></asp:Label>                         
                              </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Address">
                             <ItemTemplate>
                                 <b>Line1:</b><asp:Label ID="add1" runat="server" Text='<%# Bind("Addressline1") %>'></asp:Label><br />                               
                                 <b>Line2:</b><asp:Label ID="add2" runat="server" Text='<%# Bind("Addressline2") %>'></asp:Label><br />
                                 <b>City:</b><asp:Label ID="city" runat="server" Text='<%# Eval("City") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>State:</b><asp:Label ID="state" runat="server" Text='<%# Eval("State_or_province") %>'></asp:Label><br />
                                 <b>Zip:</b><asp:Label ID="zip" runat="server" Text='<%# Eval("Zip_or_PostalCode", "{0}") %>'></asp:Label>&nbsp;|&nbsp;
                                 <b>Country:</b><asp:Label ID="country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                            <asp:TemplateField HeaderText="DeliveryType">                            
                             <ItemTemplate>
                                 <asp:Label ID="lbltype" runat="server" Text='<%# Eval("deliveryservice") %>'></asp:Label>
                               </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Type of Copy">                            
                             <ItemTemplate>
                                 <asp:Label ID="lblcopytype" runat="server" Text='<%# Eval("Type") %>'  ></asp:Label>                                
                               </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField Visible="false"  HeaderText="CopyNo">                            
                             <ItemTemplate>
                                 <asp:Label ID="lblcopy" runat="server" Text='<%# Eval("CopyNo") %>'  ></asp:Label>
                               </ItemTemplate>
                         </asp:TemplateField>                         
                         <asp:BoundField DataField="Optional_InstitutionName" HeaderText="Institution" />                       
                     </Columns>
                     <AlternatingRowStyle CssClass="alt" />
                 </asp:GridView>
 <br />
 <br />
 <div id="email_detail" runat="server">
  <div class="headertag" >Official Electronic Copy Delivery Details</div>
<br />           
<asp:GridView ID="email_details" runat="server" AutoGenerateColumns="False" 
                    style="text-align: center"  CssClass="gridview_css" PagerStyle-CssClass="pgr"  
                    AlternatingRowStyle-CssClass="alt">
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
                                        <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                        <span><%# Eval("Email") %></span>
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
 <br />
 <div id="fax_detail" runat="server">
  <div class="headertag" >Fax Copy Delivery Details</div>
<br />           
 <asp:GridView ID="fax_details" runat="server" AutoGenerateColumns="False" 
                    style="text-align: center"  CssClass="gridview_css" PagerStyle-CssClass="pgr"  
                    AlternatingRowStyle-CssClass="alt">
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
                                    <PagerStyle CssClass="pgr" />
                                    <EmptyDataTemplate>                                    
                                        No Fax Copies Requested                                      
                                    </EmptyDataTemplate>                    
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
 </div>
   <div class="headertag" >Applicant Notes</div> 
<br />    
  <asp:GridView id="Grid_applicantNotes" AutoGenerateColumns="false"  runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
 <Columns> 
  <asp:BoundField DataField="Notes" HeaderText="Applicant Note"  />
    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" ItemStyle-Width="200px" />
 </Columns>
<EmptyDataTemplate>
No Applicant Note Available                       
</EmptyDataTemplate>
</asp:GridView>  
<br />
<br />
</ContentTemplate>       
 </asp:UpdatePanel>       
</asp:Content>
