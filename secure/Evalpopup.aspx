<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evalpopup.aspx.cs" MasterPageFile="~/secure/popupMaster.master"   Inherits="secure_Evalpopup" %>

<asp:Content ID="HomeContent" ContentPlaceHolderID ="Content" runat="server">
<br />
<br />
<table class="search_css">
<tr><td>Education</td><td><asp:Label ID="lbleducation" runat="server" ></asp:Label></td></tr>
<tr id="row" runat="server" visible="false"><td>Linkage-Name</td><td><asp:TextBox ID="lnkname" runat="server"></asp:TextBox></td></tr>
<tr><td>Us Equivalency</td><td><asp:DropDownList ID="equivalency" runat="server" onload="equivalency_Load"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="equivalency" ErrorMessage="RequiredFieldValidator" 
        InitialValue="0">*</asp:RequiredFieldValidator>
    </td></tr>
<tr><td>Gradescale</td><td> <asp:DropDownList ID="gradescale" runat="server" onload="gradescale_Load"></asp:DropDownList></td></tr>
<tr><td>Issued GPA</td><td><asp:TextBox ID="txtissued" runat="server"></asp:TextBox></td></tr>
<tr><td>Converted GPA</td><td><asp:TextBox ID="txtconverted" runat="server"></asp:TextBox></td></tr>
<tr><td colspan="2" > <asp:Button ID="btn" runat="server" Text="Bind" CssClass="btncolor" onclick="btn_Click" /><asp:Label ID="lblid" runat="server" Visible="false"></asp:Label><asp:Label ID="lblRid" runat="server" Visible="false"></asp:Label></td></tr>
</table>                          
<br />
<br />                                                                                                    
</asp:Content>


