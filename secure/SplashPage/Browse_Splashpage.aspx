<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_Splashpage.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_SplashPage_Browse_Splashpage" %>

<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <span class="title" >Splash Settings</span>   
		<br />
		<br />		
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>
              <div class="Select_client">Client:&nbsp;<asp:DropDownList ID="dpsubclients" 
                    runat="server" onselectedindexchanged="dpsubclients_SelectedIndexChanged" 
                    AutoPostBack="True">
    </asp:DropDownList></div>
               
            <br />
            <br />
<asp:DetailsView  id="DetailsView_Splash" runat="server" 
                    AutoGenerateRows="False" CssClass="detailview_css"  PagerStyle-CssClass="pgr"   
                    AlternatingRowStyle-CssClass="alt" 
                    ondatabound="DetailsView_Splash_DataBound">   
    <PagerStyle CssClass="pgr" />
<Fields>
<asp:TemplateField ShowHeader="False"><ItemTemplate>
<table width="100%"><tr><td></td><td width="100px">
    <asp:Button ID="Updatetop" runat="server" CssClass="btncolor" 
         Text="Edit Settings" 
        PostBackUrl='<%#"~/secure/SplashPage/Update_Splashpage.aspx?clid=" + DataBinder.Eval(Container.DataItem,"Customer_Id")%>' />
                   </td></tr></table>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="App Instructions">
<ItemTemplate>
    <asp:Label ID="lblAppInstructions" runat="server" Text='<%# Bind("AppInstructions") %>'></asp:Label>
    <asp:Label id="lblclientid" runat="server" Visible="false" Text='<%# Bind("Customer_Id") %>'></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Browser Instructions">
<ItemTemplate>
    <asp:Label ID="lblBrowserInstructions" runat="server" Text='<%# Bind("BrowserInstructions") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<%--<asp:TemplateField HeaderText="Client Instructions">
<ItemTemplate>
    <asp:Label ID="lblClientInstructions" runat="server" Text='<%# Bind("ClientInstructions") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
<asp:TemplateField HeaderText="OfflineApp Instructions">
<ItemTemplate>
    <asp:Label ID="lbloffInstructions" runat="server" Text='<%# Bind("OfflineAppInstructions") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Offline Application">
<ItemTemplate>
<table>
<tr><td>
    <asp:RadioButtonList ID="Rbt" runat="server" Enabled="false"  RepeatDirection="Horizontal"  SelectedValue='<%# Bind("OfflineApp") %>'   >
    <asp:ListItem Text="Yes" Value="True"  ></asp:ListItem>
    <asp:ListItem Text="No" Value="False"  ></asp:ListItem>
    </asp:RadioButtonList>
</td>
<td>
    <asp:Panel ID="pdfpanel" runat="server">
    <table>
    <tr><td style="text-align:center;" >
        <asp:HyperLink ID="Imgpdf" Target="_blank"  runat="server"><asp:Image ID="img" runat="server" ImageUrl="~/secure/Code/icons/pdf_icon.png" width="50px" Height="50px" /></asp:HyperLink></td></tr>
    <tr><td>Application.pdf</td></tr>
    </table>     
    </asp:Panel>
     </td></tr>
</table>
    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Online Application">
    <ItemTemplate>
  <asp:RadioButtonList ID="onlineapp" runat="server" Enabled="false" RepeatDirection="Horizontal" SelectedValue='<%# Eval("OnlineApp") %>'>
        <asp:ListItem Value="1">Enable</asp:ListItem>
        <asp:ListItem Value="0">Disable</asp:ListItem>        
    </asp:RadioButtonList>    
    </ItemTemplate>
         <ItemStyle Height="15px"/>
    </asp:TemplateField>
<asp:TemplateField ShowHeader="False"><ItemTemplate>
<table width="100%"><tr><td></td><td width="100px">
    <asp:Button ID="Update" runat="server" CssClass="btncolor" 
         Text="Edit Settings" 
        PostBackUrl='<%#"~/secure/SplashPage/Update_Splashpage.aspx?clid=" + DataBinder.Eval(Container.DataItem,"Customer_Id")%>' />
        </td></tr></table>           
</ItemTemplate>
</asp:TemplateField>
</Fields>    
    <AlternatingRowStyle CssClass="alt" />
</asp:DetailsView>
<br />
<br />
</ContentTemplate>       
        </asp:UpdatePanel>    
</asp:Content>
