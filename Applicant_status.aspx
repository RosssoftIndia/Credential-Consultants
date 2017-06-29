<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Applicant_status.aspx.cs" MasterPageFile="~/popupMaster.master"   Inherits="Applicant_status" %>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />

<table class="search_css" style="width:800px;">                      
                   
                                <tr>
                                    <td colspan="2">
                                        <b>File Number:</b>&nbsp;<label id="appl_number" runat="server"></label>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                        <b>Applicant Name:&nbsp;</b><label id="appl_name" runat="server"></label>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                        <b>Purpose:</b>&nbsp;<label id="appl_purpose" runat="server"></label>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2"> <hr style="border-style: groove" /></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">                                   
                                           Application Received</td>                                  
                                    <td>                                    
                                        <asp:Image ID="img1" runat="server" ImageUrl="~/images/accept.png" 
                                            Visible="False" />     
                                            <div id="blk1" runat="server" visible="false">No</div>                                 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">                                 
                                        Documents Received
                                        </td>                                  
                                    <td>                                       
                                        <asp:Image ID="img2" runat="server" ImageUrl="~/images/accept.png" 
                                            Visible="False" />
                                             <div id="blk2" runat="server" visible="false">No</div>                                 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">                                       
                                        Payment Received
                                        </td>                                  
                                    <td>
                                        <asp:Image ID="img3" runat="server" ImageUrl="~/images/accept.png" 
                                            Visible="False" />
                                             <div id="blk3" runat="server" visible="false">No</div>                                 
                                             </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">                                       
                                        Service in Process</td>                                  
                                    <td>
                                         <asp:Image ID="img4" runat="server" ImageUrl="~/images/accept.png" 
                                             Visible="False" />
                                              <div id="blk4" runat="server" visible="false">No</div>                                 
                                              </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">                                       
                                        Initial Report Prepared</td>                                
                                    <td>
                                         <asp:Image ID="img5" runat="server" ImageUrl="~/images/accept.png" 
                                             Visible="False" />
                                              <div id="blk5" runat="server" visible="false">No</div>                                 
                                              </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">
                                      
                                        Report Approved</td>
                                    <td>
                                        <asp:Image ID="img6" runat="server" ImageUrl="~/images/accept.png" 
                                            Visible="False" />
                                             <div id="blk6" runat="server" visible="false">No</div>                                 
                                            </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">
                                      
                                        Final Report Prepared, Service Complete</td>                                
                                    <td>
                                         <asp:Image ID="img7" runat="server" ImageUrl="~/images/accept.png" 
                                             Visible="False" />
                                              <div id="blk7" runat="server" visible="false">No</div>                                 
                                              </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700">
                                       Report Sent Out</td>                                  
                                    <td><asp:Image ID="img8" runat="server" ImageUrl="~/images/accept.png" 
                                            Visible="False" />
                                             <div id="blk8" runat="server" visible="false">No</div>
                                                                              </td>
                                </tr>   
                                    <tr>
                                    <td colspan="2">
                                     <br />
<%--  <div class="headertag" >Applicant Notes</div> 
<br />    
  <asp:GridView id="Grid_applicantNotes" AutoGenerateColumns="false"  runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
 <Columns> 
  <asp:BoundField DataField="Notes" HeaderText="Applicant Note"  />
    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" />
 </Columns>
<EmptyDataTemplate>
No Applicant Note Available                       
</EmptyDataTemplate>
</asp:GridView>  
 <br />--%>
  <div class="headertag" >Status Notes</div> 
<br />
 <asp:GridView id="Grid_status" AutoGenerateColumns="false"  runat="server" CssClass="gridview_css" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
 <Columns> 
  <asp:BoundField DataField="Notes" HeaderText="Status Note"  />
    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" />
 </Columns>
<EmptyDataTemplate>
No Status Note Available                       
</EmptyDataTemplate>
</asp:GridView> 
<br />
                                  
                                    </td>
                                </tr>                            
                            </table>    
                           
<br />
<br />                                                                                                    
</asp:Content>


