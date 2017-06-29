<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse_DeliveryType.aspx.cs" MasterPageFile="~/secure/Master.master" Inherits="secure_DeliveryType_Browse_DeliveryType" %>
<asp:Content ID="Homesubmenu" ContentPlaceHolderID ="Submenu" runat="server">  
 <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"   runat="server">          
            <ContentTemplate>             
 <span class="title" >Delivery Settings</span>
   <div class="submenu_style"> 
      <div class="buttons">   
 <a id="Addlink" runat="server"  href="#" class="regular">
        <img src="../Code/icons/irc-join.ico" alt=""/> 
        <b>Add Delivery Setting</b>
    </a>  
</div>          
		</div>
		<br />
		<br />		
		</ContentTemplate> 
		</asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">    
            <ContentTemplate>   
                 <div class="Select_client">Client:&nbsp;<asp:DropDownList ID="dpsubclients" 
                    runat="server" onselectedindexchanged="dpsubclients_SelectedIndexChanged" 
                    AutoPostBack="True">
    </asp:DropDownList></div>
               
    <br />
    <br />
<asp:GridView style="TEXT-ALIGN: center" id="grid_Delivery" runat="server" 
                    OnPageIndexChanging="grid_Delivery_PageIndexChanging" PageSize="25" 
                    AutoGenerateColumns="False" CssClass="gridview_css" 
                    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" >
<Columns>
<asp:TemplateField SortExpression="Name" HeaderText="Delivery Type">
<ItemTemplate>
<asp:HyperLink id="HyperLink1" runat="server" Text='<%# Eval("Name", "{0}") %>' NavigateUrl='<%# Eval("id", "~/secure/DeliveryType/Update_DeliveryType.aspx?delid={0}") %>' CssClass="link"></asp:HyperLink>
</ItemTemplate>
</asp:TemplateField>   
    <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Cost", "{0:C}") %>'></asp:Label>
                <asp:HiddenField ID="txtid" runat="server"  Value='<%# Bind("id", "{0}") %>'/> 
            <asp:HiddenField ID="txtorder" runat="server"  Value='<%# Bind("priority", "{0}") %>'/> 
            <%--<asp:Label ID="Label3" runat="server" Text='<%# Bind("id", "{0}") %>'></asp:Label>
            <asp:Label ID="Label4" runat="server" Text='<%# Bind("priority", "{0}") %>'></asp:Label>--%>
        </ItemTemplate>
    </asp:TemplateField> 
      <asp:TemplateField HeaderText="Sort Oder">
     <ItemTemplate>
        <table align="center"><tr>
     <td><asp:ImageButton ID="up" runat="server" 
            onclick="up_Click" ImageUrl="~/images/up.png" Width="25px" /></td>
            <td><asp:ImageButton ID="down" runat="server" 
            onclick="down_Click" ImageUrl="~/images/down.png" Width="25px" /></td>
            </tr></table> 
        
        </ItemTemplate>
    </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
<ItemStyle Width="20px" />
<ItemTemplate>
<asp:ImageButton ID="del" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/remove.png" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Service ?\\n" + ((string)Eval("Name"))+ "\");"%>' OnClick="del_Click" Text="Delete" />
</ItemTemplate>
</asp:TemplateField>    
</Columns>
    <PagerStyle CssClass="pgr" />
<EmptyDataTemplate>
                          No Delivery Type Available
                       
</EmptyDataTemplate>
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView> 
<br />
<br />
</ContentTemplate>       
</asp:UpdatePanel>           
</asp:Content>
