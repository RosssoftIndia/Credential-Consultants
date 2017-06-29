<%@ Page Language="C#" AutoEventWireup="true" CodeFile="msg.aspx.cs" MasterPageFile="~/AppMaster.master"  Inherits="msg" %>


<asp:Content ID="htmlheader" ContentPlaceHolderID ="pageHeader"  runat="server">  

    <script type="text/javascript" >
     function PopupCenter(pageURL, title, w, h) {
         var left = (screen.width / 2) - (w / 2);
         var top = (screen.height / 2) - (h / 2);
         var w1 = (screen.width - 30);
         var h1 = (screen.height - 100);
         var left1 = 10;
         var top1 = 10;
         var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars= yes, resizable=no, copyhistory=no, width=' + w1 + ', height=' + h1 + ', top=' + top1 + ', left=' + left1);
     } 
</script>
<link rel="stylesheet" href="Code/colorbox/colorbox.css" />
<script src="Code/colorbox/jquery.min.js"></script>
<script src="Code/colorbox/jquery.colorbox.js"></script>
<script>
    $(document).ready(function() { $(".iframe").colorbox({ iframe: true, width: "80%", height: "80%" }); });
</script>
<script language="javascript" type="text/javascript">
    $(document).bind('cbox_closed', function() {
        window.scroll(0, 0);      
    });
</script>   

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
   <table style="width:100%;min-height:110px;" >
  <tr><td><img id="logo" runat="server" alt="logo" visible="false" /><span id="OrgTitle" runat="server" class="clientTitle" visible="false"></span> </td></tr>
 <tr><td><table style="float:right;vertical-align:bottom;" > 
 <tr><td id="Subclient" runat="server" class="Subclient"></td></tr>
 </table></td></tr>
 </table>
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
    <br />
<br />   
       <asp:MultiView ID="FormViewcontrol2" runat="server">            
       <asp:View ID="submit_block" runat="server">
       <table width="100%"> 
       <tr>
       <td colspan="4" >
       <br />
        <div class="headertag">Submit Application</div>
        <br />
       <table width="100%">              
		        <tr>     
            <td colspan="4" class="warning_box_msg" >                         
                <%--<p>Before you submit your application, please read and agree to our Terms &amp; Conditions as well as the Privacy Policy and Terms of Service for online processing.</p>
                                <p>
                                    <i>By checking the boxes below, you indicate that you are electronically signing and
                                        agreeing to be bound by all of the terms, conditions, and notices contained or referenced
                                        on this site:</i></p>
                       <asp:CheckBox ID="frm8_chk1" runat="server" Text="I agree to the "/><a id="toc" runat="server" href="#" class='iframe' title="Terms and Conditions" >Terms and Conditions</a> 
                of this application. I also agree to <a href="PrivacyPolicy.htm" class='iframe' title="Privacy Policy" >Privacy Policy</a> and <a href="TermsOfService.htm" class='iframe' title="Terms of Service">Terms of Service</a> for processing my order online. 
                <br /><br /><br />--%>
                    <asp:CheckBox ID="frm8_Optional" runat="server" Text="I want to join Credential Connection&#39;s Talent Database. By joining Credential Connection's Talent Database, I authorize Credential Connection to release my name, email address, phone, mailing address, educational history, and evaluation results to potential employers and other institutions." Checked="False"/><br />                
                    </td>               
	</tr>   
       </table> 
       </td>
       </tr>
       <tr>
                  <td>                                   
                                   <table width="100%">                           
                            <tr>
                                <td>
                                    
                                </td>
                                <td style="width: 50%; text-align: left; font-weight: bold; font-size: 10px; color: #584B42;">
                                    </td>
                                <td style="width: 50%; text-align: right;font-weight:bold; font-size: 10px; color: #584B42;">
                                    Submit Application<br />                                    
                                </td>
                                <td>
                                    <asp:ImageButton ID="frm7_btn_submit" runat="server" ImageAlign="Baseline" 
                                        ImageUrl="~/images/r-arrow1.png" OnClick="frm7_btn_submit_Click" 
                                        ValidationGroup="frm7_group1" />
                                </td>
                            </tr>
                        </table>
                                </td>
                            </tr>
           </table> 
       </asp:View> 
<asp:View ID="info" runat="server">
    <table width="100%"> 
      <tr >
        <td style="height: 123px">
        <br /> 
        <div class="headertag">Note</div>
        <br />
           <table>          
            <tr> 
               <td class="warning_box_msg"><p><%--Your application has been submitted electronically. To complete the application process, please print your application, sign it, and mail it to us. We will begin your evaluation upon receipt of your signed and dated application, documents, and payment.--%>
                Your file number is [ <asp:Label ID="Filenumber" runat="server" Text="Filenumber" Font-Bold="True"></asp:Label> ].Please use this file number in all correspondence regarding your service order and when checking your online status on our website.<br />               
                </p>
                <p id="submitnote" runat="server"></p></td>                                    
               </tr>                
        </table>             
        </td>
        </tr> 
        <tr>
                <td style="height: 24px">
                    <div>
                        <table width="100%">
                            <tr>
                            <td><br /></td>                             
                            </tr>
                                     <tr id="uploadsection" runat="server" >   
                            <td style="width: 57px">
                            <a id="uploadurl" runat="server"  href="#" class='iframe'><asp:Image ID="Image1" ImageUrl="~/images/r-arrow1.png" runat="server" /></a></td>                         
                             <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">Upload Documents</td>
                              </tr>
                            <tr>   
                            <td style="width: 57px"><asp:ImageButton ID="frm9_btn_print" runat="server" 
                                    ImageAlign="Baseline" ImageUrl="~/images/r-arrow1.png"/></td>                         
                             <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">Print Application</td>
                              </tr>
                               <tr>   
                            <td style="width: 57px"><asp:ImageButton ID="frm9_btn_pdf" runat="server" 
                                    ImageAlign="Baseline" ImageUrl="~/images/r-arrow1.png" OnClick="btnExport_Click" /></td>                         
                             <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">Save Application</td>
                              </tr>
                            <tr id="thblock" runat="server" visible="false">
                            <td><a id="cls" runat="server" style="color: White; text-decoration: none; background: white;" href="#"><img alt="" src="images/r-arrow1.png" /></a></td>                               
                            <td style="text-align: Left;font-weight:bold; font-size: 10px; color: #584B42;">Close</td>
                            </tr>
                            <tr><td></td></tr>
                        </table>
                    </div>
                </td>
            </tr>                 
	</table>    
	</asp:View> 
	</asp:MultiView>  
 
<br />
<br />                                                                                                    
   <div id="Pdfsave" style="display:none" runat="server">   
          <table>
          <!-- Client Name -->
          <tr>
          <td>        
          <div id="OrgTitlepdf" runat="server" ></div>
          </td>
          </tr>
          <!-- Personal Info -->
          <tr><td align="center" style="text-align: left">Personnal Information</td></tr>  
          <tr>
          <td>        
           <asp:DetailsView ID="Applicant" runat="server"  AutoGenerateRows="False" GridLines="None" style="text-align: left" OnDataBound="Applicant_DataBound" OnLoad="Applicant_Load">
                       <Fields>
                           <asp:TemplateField HeaderText="File Number" SortExpression="Email">                              
                               <ItemTemplate>                         
                               <div><b><%# Eval("FileNumber") %></b></div>                                                        
                               </ItemTemplate>                               
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
           <tr><td align="center" style="text-align: left">Payment Mode</td></tr>    
          <tr id="paymentblock" runat="server">
               <td>  
               <table style="width: 420px">
               <tr>
               <td><b>Payment Mode</b></td>
               <td>
                   <asp:Label ID="pymode" runat="server"></asp:Label>
                   </td>
               </tr>
                <tr id="Tyblk" runat="server">
               <td><b>Creditcard Type</b></td>
               <td>
                   <asp:Label ID="Ctype" runat="server"></asp:Label>
                    </td>
               </tr>
               </table> 
               </td>
               </tr>        
          <!-- Education Info -->
          <tr  id="edu" runat="server">
            <td>                          
                  <table>  
                   <tr><td align="center" style="text-align: left">Educational History</td></tr>                     
                   <tr>
                   <td>                  
                
                </td>
                </tr>                    
                <tr>              
               <td style="text-align: center">                
               HighSchool History
               </td>
               </tr>
               <tr>
               <td style="text-align: left">
               <asp:GridView ID="hischoolgrid" runat="server" AutoGenerateColumns="False" 
                      ForeColor="#333333" GridLines="None"  
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
                   </asp:GridView>                                   
               </td>
               </tr>    
               <tr>
               <td style="text-align: center">                
               University History
               </td>
               </tr>
                <tr>
               <td style="text-align: left"> 
               <asp:GridView ID="univgrid" runat="server" AutoGenerateColumns="False" 
                      ForeColor="#333333" GridLines="None" 
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
                   </asp:GridView>                                      
               </td>
               </tr>  
               </table>
               
            </td>
            </tr> 
          <!-- Purpose Info -->
          <tr>
            <td>
               <table>    
                <tr><td align="center" style="text-align: left">Purpose</td></tr>                                 
               <tr>
               <td style="text-align: left">               
                   <asp:DetailsView ID="purposegrid" runat="server" 
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
                   </asp:DetailsView>            
               </td>
               </tr>  
               </table>
                 
            
            </td>
            </tr>   
          <!-- Service Info -->  
          <tr>
            <td>
<table>    
             <tr><td align="center" style="text-align: left">Service</td></tr>                                                                                          
                 <tr>
                     <td style="text-align:left;color:Gray; ">                                  
                        General Service
                        </td>
                        </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:GridView ID="service1grid" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                OnDataBound="service1grid_DataBound" OnLoad="service1grid_Load" 
                                ShowFooter="True" style="text-align: center" >
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
                            </asp:GridView>
                        </td>
                        </tr>
                        <tr id="Revsec_Additionalheader" runat="server"  >
                     <td style="text-align:left;color:Gray; "> 
                                Additional Official Hard Copy Service                                
                                </td>
                        </tr>
                        <tr id="Revsec_Additional" runat="server">
                            <td style="text-align: left">
                                <asp:GridView ID="copychargergrid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="copychargergrid_DataBound" 
                                    OnLoad="copychargergrid_Load" ShowFooter="True" style="text-align: center">                                  
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
                                </asp:GridView>
                            </td>
                        </tr>
                               <tr id="Revsec_Emailheader" runat="server"   >
                     <td style="text-align:left;color:Gray; ">                                    
                                Official Electronic Copy Service
                                </td>
                        </tr>
                        <tr id="Revsec_Email" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="email_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="email_grid_DataBound" OnLoad="email_grid_Load" 
                                    ShowFooter="True" style="text-align: center" >                                  
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
                                </asp:GridView>
                            </td>
                        </tr>
                          <tr id="Revsec_Faxheader" runat="server"   >
                     <td style="text-align:left;color:Gray; ">  
                                Fax Copy Service
                                </td>
                        </tr>
                        <tr id="Revsec_Fax" runat="server"  >
                            <td style="text-align: left">
                                <asp:GridView ID="fax_grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnDataBound="fax_grid_DataBound" OnLoad="fax_grid_Load" 
                                    ShowFooter="True" style="text-align: center" >                                    
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
                                    <EmptyDataTemplate>
                                        No Fax Copies Requested
                                    </EmptyDataTemplate>                                   
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr  >
                     <td style="text-align:left;color:Gray; ">                                    
                                Delivery Service - Official Hard Copy(ies) &amp; Additional Copies
                                </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:GridView ID="Delivery_Grid" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    OnDataBound="Delivery_Grid_DataBound" OnLoad="Delivery_Grid_Load" 
                                    ShowFooter="True" style="text-align: center" >
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
                                </asp:GridView>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
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
		        <td  align="center">Delivery Details</td></tr>
		          <tr  >
                     <td style="text-align:left;color:Gray; ">    
                                
                                Delivery Details - Official Hard Copy(ies) &amp; Additional Copies
                                </td>
                        </tr>
		        <tr>
		        <td>
    <asp:GridView ID="deliverydetails" runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                     style="TEXT-ALIGN: center" onload="deliverydetails_Load">                   
                     <EmptyDataTemplate>
                         No Delivery Details Available
                     </EmptyDataTemplate>                   
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
     
                           </td> 
                           </tr> 
                            <tr  >
                     <td style="text-align:left;color:Gray; ">                                    
                                Fax Copy Delivery Details
                                </td>
                        </tr>
                           <tr><td>
                           <asp:GridView ID="fax_details" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    HorizontalAlign="Left" OnLoad="fax_details_Load" 
                                    ShowFooter="True" style="text-align: center" >                                 
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
                                </asp:GridView>
                           </td></tr>
                           
                           
                           
                           </table> 
          
        </td>
        </tr>   
          <!-- SignIn Info -->  
          <tr>
        <td>
         
        <table>
            <tr>
                <td>
                    I agree to all terms and conditions set forth in the online application.
                    
                    
                    
                    Signed by_______________________________Date_________________</td>
            </tr>
        </table>
       
        </td>
        </tr>
          <!-- CreditCard Info -->  
          <tr>
            <td  id="creditblog" runat="server" visible="false">
            
               <table>
	            <tr>
		        <td  align="center">Credit Card Details</td></tr>
		        <tr>
		        <td>
                    <table>
                        <tr>
                            <td style="width: 366px;font-size:10px;">
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
   
                                                                               
</asp:Content>

