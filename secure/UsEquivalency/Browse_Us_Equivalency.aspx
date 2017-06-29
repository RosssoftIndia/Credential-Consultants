<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Us_Equivalency.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_UsEquivalency_Browse_Us_Equivalency" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Equivalencies</span>
   <div class="submenu_style">            
          <div class="buttons">     
    <a href="Add_Us_Equivalency.aspx" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Equivalency</b>
    </a>  
</div>  
		</div>
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
 <br />
            <br />
  <div class="searchblk">
<table><tr><td><label for="searchtxt">Search Terms:</label><asp:TextBox ID="searchbox" runat="server" ></asp:TextBox><%--<sv:RequiredFieldValidator id="validator" runat="server" ErrorMessage="Enter value in the searchbox" ControlToValidate="searchbox">*</sv:RequiredFieldValidator>--%></td><td><asp:ImageButton ID="searchbtn" runat="server" ImageUrl="~/secure/Code/button/search-btn.png" OnClick="searchbtn_Click" /></td></tr></table>      
</div>
    <br />
    <br />
<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
         <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="searchbtn" />           
        </Triggers>
            <ContentTemplate>
            <br />
            <br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_Equivalency" runat="server" OnPageIndexChanging="grid_Equivalency_PageIndexChanging" PageSize="25" AllowPaging="True" OnLoad="grid_Equivalency_Load" AutoGenerateColumns="False" ondatabound="grid_Equivalency_DataBound" CssClass="gridview_css" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
<Columns>
<asp:TemplateField HeaderText="Name"><ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%#Eval("Name","{0}") %>' NavigateUrl='<%# Eval("ID", "~/secure/UsEquivalency/Update_Us_Equivalency.aspx?eqlid={0}") %>' CssClass="link"></asp:HyperLink>
 <asp:Label ID="equi_id" Visible="false" runat="server" Text='<%#Eval("ID","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>      
    <asp:TemplateField HeaderText="Description" >
<ItemTemplate>
    <asp:Label ID="lbldes" runat="server" Text='<%#Eval("Description","{0}")%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>
No Us Equivalency Available                       
</EmptyDataTemplate>
</asp:GridView>
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
