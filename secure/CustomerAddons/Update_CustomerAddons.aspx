<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_CustomerAddons.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_CustomerAddons_Update_CustomerAddons" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Update Application Settings</span>   
  <div class="submenu_style">   
  <div class="buttons">   
 <a href="Browse_CustomerAddons.aspx" class="regular">
        <img src="../Code/icons/find-new-users.ico" alt=""/> 
        <b>Browse Settings</b>
    </a>  
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
<asp:DetailsView id="DetailsView_customer" runat="server" OnLoad="DetailsView_customer_Load" 
                    DefaultMode="Edit" AutoGenerateRows="False" CssClass="detailview_css"  
                    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_customer_DataBound">
    <PagerStyle CssClass="pgr" />
<Fields>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
<table width="100%"><tr><td style="font-weight:bold;padding-left: 0px;">Client:&nbsp;
  <asp:Label ID="clienttop" CssClass="SubclientEntry" runat="server" Text="Label"></asp:Label>  
</td><td width="100px">
<asp:Button ID="Updatetop" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" Text="Update" OnClientClick='<%# "javascript:return confirm(\"Are you sure you want to Update Changes"+ " ?\");"%>' OnClick="Update_Click" />
</td></tr></table>
</EditItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
</asp:TemplateField>
<asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
   Terms And Condition Section
    </EditItemTemplate>     
     <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="Terms And Condition" ShowHeader="false" ><EditItemTemplate>
    <CKEditor:CKEditorControl ID="txt_toc" runat="server" Width="100%" Height="300px"  BasePath="~/Code/fckeditor/" Text='<%# Bind("Terms_And_Condition") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
    <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
   Education Section
</EditItemTemplate>
     <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
</asp:TemplateField>
<asp:TemplateField SortExpression="Education_Instructions" HeaderText="Education Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="edinst" runat="server"  Width="100%" Height="300px"  BasePath="~/Code/fckeditor/" Text='<%# Bind("Education_Instructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Document Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="url" runat="server"  Width="100%" Height="300px"  BasePath="~/Code/fckeditor/" Text='<%# Bind("Document_Instructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
 <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
    Delivery Section
    </EditItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
<asp:TemplateField SortExpression="Delivery_copy" HeaderText="No of Delivery copies"><EditItemTemplate>
<asp:TextBox ID="dlcopy" runat="server" Text='<%# Bind("Delivery_copy") %>'></asp:TextBox>
<asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
                 
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="Delivery_Instructions" HeaderText="Delivery Instructions"><EditItemTemplate>
<CKEditor:CKEditorControl ID="dlinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Delivery_Instructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
 <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
   Payment Section
</EditItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
</asp:TemplateField>
    <asp:TemplateField ShowHeader="false" >
<EditItemTemplate>
    <div style="width:100%;" >
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Online Payments</div>  
  <div style="float:right;padding-right: 10px;">
<asp:RadioButtonList ID="searchoption" runat="server" RepeatDirection="Horizontal" 
        onselectedindexchanged="searchoption_SelectedIndexChanged" 
        AutoPostBack="True" SelectedValue='<%# Eval("CreditCard") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>    
   </div>
   </div>
     <div id="creditinfoblock" runat="server" visible="false" style="padding-top:35px;">
     <hr style="border-top: 0px solid #eac7c7;padding: 0px;margin: 0px 0px 0px -11px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 121px">
                 Login ID :
                </td>
                <td>
                    <asp:TextBox ID="logintextbox"  runat="server" 
                        Width="200px" MaxLength="50"></asp:TextBox> <sv:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="logintextbox"  ErrorMessage="You must Enter Login ID">*</sv:RequiredFieldValidator>
                </td>               
            </tr>
            <tr>
                <td style="width: 121px">
                Transaction Key :
                </td>
                <td>
                  <asp:TextBox ID="transkeybox" runat="server" Width="300px" 
                        MaxLength="50"></asp:TextBox> <sv:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="logintextbox"  ErrorMessage="You must Enter Transaction Key">*</sv:RequiredFieldValidator>
                </td>               
            </tr>
                        <tr>
                <td style="width: 121px">
                Email-Id :
                </td>
                <td>
                  <asp:TextBox ID="mailid" Text='<%# Eval("Email") %>'  runat="server" Width="200px" MaxLength="50"></asp:TextBox> 
                  <sv:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="mailid"  ErrorMessage="You must Enter Email-Id">*</sv:RequiredFieldValidator>
                 <sv:RegularExpressionValidator ID="frm1_ExpressionFieldValidator4" runat="server" ControlToValidate="mailid" ErrorMessage="You must enter a valid Email-ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></sv:RegularExpressionValidator >
                </td>               
            </tr>
            <tr><td colspan="2" ><br />Note: Any sucessful customer transaction will be mailed to the Above Email-Id</td></tr>                   
        </table>
        <br />
    </div>
    <asp:Label ID="tempchk1" Visible="false" Text='<%# Eval("LoginId") %>'   runat="server" ></asp:Label>
    <asp:Label ID="tempchk2" Visible="false" Text='<%# Eval("Transkey") %>' runat="server" ></asp:Label>
    </EditItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField>  
  <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
<div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">CreditCard</div>
<div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="Onlinecc" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Onlinecc") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>  
   </div>
  </EditItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="CreditCard" ShowHeader="false" >
<EditItemTemplate>  
     <CKEditor:CKEditorControl ID="CreditCardinst" runat="server" Width="100%" Height="150px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Creditcard_Instructions") %>' ></CKEditor:CKEditorControl>  
     </EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;">Supported Cards</div>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Supported Cards" ShowHeader="false">
<EditItemTemplate>
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
           RepeatDirection="Horizontal" Width="100%" RepeatColumns="3" 
           RepeatLayout="Table">           
       </asp:CheckBoxList>
</EditItemTemplate>
</asp:TemplateField>
 <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Money Order</div>
  <div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="morder" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Moneyorder") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>  
   </div>
    </EditItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField> 
<asp:TemplateField HeaderText="Money Order" ShowHeader="false">
<EditItemTemplate> 
<CKEditor:CKEditorControl ID="morderinst" runat="server" Width="100%" Height="150px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Moneyorder_Instructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
  <div style="float:left;font-size:15px;font-weight:bold;padding-top: 5px;">Personal check</div>
  <div style="float:right;padding-right: 10px;">
  <asp:RadioButtonList ID="pcheck" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Personalcheck") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList> 
     </div>
    </EditItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField> 
<asp:TemplateField HeaderText="Personal check" ShowHeader="false">
<EditItemTemplate>
<CKEditor:CKEditorControl ID="pcheckinst" runat="server" Width="100%" Height="150px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Personalcheck_Instructions") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField ShowHeader="false" >
   <EditItemTemplate>
    Review Section
   </EditItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
     <asp:TemplateField HeaderText="Special Instructions">
<EditItemTemplate>
<CKEditor:CKEditorControl ID="splinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Spl_Instruction") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
     <asp:TemplateField ShowHeader="false" >
<EditItemTemplate>
    Completed Section
  </EditItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
         <asp:TemplateField HeaderText="Completed Instructions">
<EditItemTemplate>
<CKEditor:CKEditorControl ID="complinst" runat="server" Width="100%" Height="300px" BasePath="~/Code/fckeditor/" Text='<%# Bind("Completed_Instruction") %>' ></CKEditor:CKEditorControl>
</EditItemTemplate>
</asp:TemplateField>
 <asp:TemplateField ShowHeader="false" >
    <EditItemTemplate>
    Configurable Sections
    </EditItemTemplate>
         <ItemStyle Height="30px" Font-Bold="true" Font-Size="20px"  BackColor="White" />
    </asp:TemplateField> 
  <asp:TemplateField HeaderText="Application Type">
<ItemTemplate>
    <asp:RadioButtonList ID="List_App_type" runat="server" RepeatDirection="Vertical" SelectedValue='<%# Eval("Application_Type") %>' >
    <asp:ListItem Text="Full Process with Education History" Value="1"></asp:ListItem> 
    <asp:ListItem Text="Type 2" Value="2"></asp:ListItem> 
    <asp:ListItem Text="Short Process without Education History" Value="3"></asp:ListItem> 
    </asp:RadioButtonList>
</ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Service">
<EditItemTemplate>
<table>
<tr><td><asp:CheckBox ID="chk_Mail" runat="server" Text="Additional" Checked='<%# Eval("Additional_Section") %>' /></td></tr>
<tr><td><asp:CheckBox ID="chk_Fax" runat="server" Text="Fax" Checked='<%# Eval("Fax_Section") %>' /></td></tr>
<tr><td><asp:CheckBox ID="chk_Email" runat="server" Text="Email" Checked='<%# Eval("Email_Section") %>' /></td></tr>
</table>
</EditItemTemplate> 
</asp:TemplateField> 
<asp:TemplateField HeaderText="Talent Database">
<EditItemTemplate>
<asp:RadioButtonList ID="tdbuoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Talent_Database") %>'>
      <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>            
    </asp:RadioButtonList>          
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Purpose Lock">
<EditItemTemplate>
<asp:RadioButtonList ID="Purposeoption" runat="server" RepeatDirection="Horizontal"  AutoPostBack="True"
        SelectedValue='<%# Eval("Purpose_Section") %>' 
        onselectedindexchanged="Purposeoption_SelectedIndexChanged">
        <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>        
    </asp:RadioButtonList> 
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">  
   
            <ContentTemplate>
    <table id="purposeblock" runat="server" visible="false">          
            <tr> 
               <td> 
                    What is the purpose of this service : <span style="color:Red;">*</span><asp:DropDownList
                        ID="frm4_option_purpose" runat="server" Width="251px" AutoPostBack="True" 
                        OnSelectedIndexChanged="frm4_option_purpose_SelectedIndexChanged" 
                        AppendDataBoundItems="True"   ValidationGroup="frm4_group" >
                        <asp:ListItem Value="0" >Select</asp:ListItem>  
                    </asp:DropDownList>                    
                   <sv:RequiredFieldValidator ID="frm4_RequiredFieldValidator1" runat="server" ControlToValidate="frm4_option_purpose"
                       ErrorMessage="You must select a purpose" InitialValue="0" ValidationGroup="frm4_group">*</sv:RequiredFieldValidator>                       
               </td>
               </tr> 
               <tr id="targetblock" runat="server" visible="false">
               <td>
               <table style="width:100%;margin-bottom:-10px;">
                <tr>
                <td align="left" style="padding-left: 0px;">Lock Targets:</td>
                <td align="right"><asp:RadioButtonList ID="Targetoption" runat="server" RepeatDirection="Horizontal"  AutoPostBack="True"
        SelectedValue='<%# Eval("Target_Section") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList> 
               </td>
               </tr>
               </table>
               <hr />
               <table style="width:100%;">
               <tr id="frm4_optional" runat="server" visible="false">
               <td style="text-align: left" >
                 Which educational institution referred you to us?<br />
                   <br />
                 <asp:TextBox ID="frm4_institution" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox></td>
               </tr> 
                <tr id="frm4_optional1" runat="server" visible="false">
               <td >
              Which employer referred you to us?<br />
                <br />
              <asp:TextBox ID="frm4_organization" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr> 
                <tr id="frm4_optional2" runat="server" visible="false">
               <td >
               Which Attorney or Law firm referred you to us?<br />
                   <br />
              <asp:TextBox ID="frm4_lawfirm" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                <tr id="frm4_optional3" runat="server" visible="false">
               <td >
                Note: Enter Name if you are applying for only one Board<br />
                   <br />
               Name of Board from which you seek licensing: <asp:TextBox ID="frm4_board" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox><br />state:<asp:TextBox 
                       ID="frm4_state" runat="server" Width="100px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                <tr id="frm4_optional4" runat="server" visible="false">
               <td >
              which Military Recruiter referred you to us?<br />
                   <br />
               <asp:TextBox ID="frm4_military" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
               </td>
               </tr>
                 <tr id="frm4_optional5" runat="server" visible="false">
               <td >
            How did you hear about us?<br />
                   <br />
              <asp:TextBox ID="frm4_evaluation" 
                       runat="server" Width="200px" MaxLength="50" ></asp:TextBox>               
               </td>               
               </tr>                        
        </table>   
               </td>
               </tr>               
        </table>   
       <asp:TextBox ID="Lock_TargetName" runat="server" Visible="false"  Text='<%# Eval("Lock_TargetName") %>'/>
         <asp:TextBox ID="Lock_State" runat="server" Visible="false"   Text='<%# Eval("Lock_State") %>'/>
         <asp:TextBox ID="Lock_PurposeId" runat="server" Visible="false"   Text='<%# Eval("Lock_PurposeId") %>'/>
       </ContentTemplate>
       </asp:UpdatePanel> 
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Applicant Upload">
<EditItemTemplate>
<asp:RadioButtonList ID="Uploadoption" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Applicant_Upload") %>'>
      <asp:ListItem Value="True">Enable</asp:ListItem>
        <asp:ListItem Value="False">Disable</asp:ListItem>            
    </asp:RadioButtonList>          
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Thank You &lt;br/&gt;(Example: www.site.com. Do not use 'http'.)">
<EditItemTemplate>
<br />
<asp:RadioButtonList ID="thankuoption" runat="server" RepeatDirection="Horizontal" 
        onselectedindexchanged="thankuoption_SelectedIndexChanged" SelectedValue='<%# Eval("ThkuPage") %>'
        AutoPostBack="True">
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>   
    <br />
    <div id="Thankublock" runat="server" visible="false" >
        <table style="width: 100%;">
              <tr>
                <td style="width: 121px">
               Website Url
                </td>
                <td>
                   <asp:TextBox ID="WebUrl" runat="server" Width="200px" MaxLength="199"></asp:TextBox>                  
                </td>               
            </tr>   
            <tr><td></td><td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="You must enter a Url" ControlToValidate="WebUrl"></asp:RequiredFieldValidator><br />
                   <asp:RegularExpressionValidator ID="urlval" runat="server" ControlToValidate="WebUrl" ErrorMessage="You must enter a valid Url" ValidationExpression="^(((ftp)\://)|www\.)[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*$" style="left: 0px"></asp:RegularExpressionValidator>
            </td></tr>       
        </table>
    </div>
    <br />      
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
<table width="100%"><tr><td style="font-weight:bold;padding-left: 0px;">Client:&nbsp;
<asp:Label ID="clientbottom" CssClass="SubclientEntry" runat="server" Text="Label"></asp:Label>  
</td><td width="100px">
<asp:Button ID="Update" runat="server" CssClass="btncolor" CausesValidation="true" CommandName="" Text="Update" OnClientClick='<%# "javascript:return confirm(\"Are you sure you want to Update Changes"+ " ?\");"%>' OnClick="Update_Click" />
</td></tr></table>
</EditItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
</asp:TemplateField>
</Fields>
    <AlternatingRowStyle CssClass="alt" />
</asp:DetailsView> 
<br />
<br />
</ContentTemplate>       
         </asp:UpdatePanel>           
   </asp:Content>
