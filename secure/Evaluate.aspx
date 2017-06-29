<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/secure/Master.master" CodeFile="Evaluate.aspx.cs" Inherits="secure_Evaluate" %>

<asp:Content ID="homeheader" ContentPlaceHolderID ="header" runat="server"> 
<script language="javascript" type="text/javascript">
    $(document).bind('cbox_closed', function() {
        window.scroll(0, 0);
        window.location.reload();
    });
</script> 
</asp:Content>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
<span class="title" >Evaluate Applications</span>
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
<td ><asp:ImageButton ID="btnview" runat="server"  Width="32" Height="32" ImageUrl="~/secure/Code/icons/view.png" CausesValidation="false"/> 
</td><td>View</td>
<td><asp:ImageButton ID="btnedit" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/edit.png" CausesValidation="false"/>
</td><td>Edit</td>
<td><asp:ImageButton ID="btnstatus" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/status.png" CausesValidation="false"/>
</td><td>Manage</td>
<td><asp:ImageButton ID="btnreport" runat="server" Width="32" Height="32"  ImageUrl="~/secure/Code/icons/note.png" CausesValidation="false" />
</td><td>Notes</td>
<td><asp:ImageButton ID="btnattach" runat="server" Width="32" Height="32" ImageUrl="~/secure/Code/icons/pdf_icon.png" CausesValidation="false" />
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
            <div class="headertag" >Map Equivalency</div>
<br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_Evaluate" runat="server" AllowPaging="True"  
                    CssClass="gridview_css" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt"  AutoGenerateColumns="False" 
                    ondatabound="grid_Evaluate_DataBound" onload="grid_Evaluate_Load" 
                    PageSize="6" >
   <Columns>           
            <asp:TemplateField HeaderText="Institution" SortExpression="Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                    
                        <asp:Label ID="lbllnk" runat="server" Text='<%# Bind("Linkage") %>' Visible="False"></asp:Label>
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
            <asp:TemplateField HeaderText="Major/Emphasis">
                <ItemTemplate>
                    <asp:Label ID="major" runat="server" Text='<%# Bind("major") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>             
              <asp:TemplateField HeaderText="Equivalency Info" >
                <ItemTemplate>
             <asp:Label ID="lblequi" runat="server"></asp:Label> <asp:Label ID="seperator" runat="server" Text="|"></asp:Label><asp:Label ID="lblgrad" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Map Equivalency" >
                <ItemTemplate>
                 <a id="btneval" style="color: White; text-decoration: none; background: white;"  href='<%# "~/secure/Evalpopup.aspx?tc="+ DataBinder.Eval(Container.DataItem,"eduid") + "&Insid=" + DataBinder.Eval(Container.DataItem,"instituteId") + "&id=" + DataBinder.Eval(Container.DataItem,"id") + "&Cid=" + DataBinder.Eval(Container.DataItem,"Cid") + "&Lid=" + DataBinder.Eval(Container.DataItem,"Linkage")+ "&Rid=" + DataBinder.Eval(Container.DataItem,"Evaluation_Request_Id") %>' runat="server"  title="Evaluation Links" class='iframe'><img id="btnimg" runat="server" src="Code/button/Equivalency.png" alt="Eval"/></a> 
                 </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    <PagerStyle CssClass="pgr" />
<EmptyDataTemplate>
No Education History                     
</EmptyDataTemplate>
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView>                 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>    
   <div class="headertag" >Generate Report</div>
<br />
<table class="search_css">
                 <tr style="visibility:hidden;" >
                        <td style="width:150px;">
                          
                        </td>
                        <td>                                                                                  
                            <asp:TextBox ID="txtfileno" runat="server" Width="100px" ReadOnly="True" Visible="false"></asp:TextBox>                                                                                   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtfileno" EnableTheming="True" 
                                ErrorMessage="Enter a File Number"></asp:RequiredFieldValidator>
                        </td>
                        </tr> 
                    <tr>
                        <td style="width:150px;">
                           Select Report
                        </td>
                        <td>                           
                            <asp:DropDownList ID="Drptemplate" runat="server" AppendDataBoundItems="True">
                            </asp:DropDownList>                                                                                   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="Drptemplate" EnableTheming="True" 
                                ErrorMessage="Select a Report" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
<asp:Button ID="btn" runat="server" Text="Generate Report" 
        CssClass="btncolor" onclick="btn_Click" />
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                        </td>                        
                    </tr>   
                      <tr style="visibility:hidden;" >
                        <td style="width:150px;">
                        <asp:TextBox ID="txtinternal" runat="server" Width="100px" ReadOnly="True" 
                                Visible="False"></asp:TextBox> <br />
                </td> 
                </tr>               
                </table>
                <br />
                <br />   
</asp:Content>