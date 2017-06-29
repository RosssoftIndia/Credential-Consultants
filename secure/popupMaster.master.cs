using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class secure_popupMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Clientsettings"].ToString() != "Empty")
        {
            Authentication.Utility.AdminDomainAttributes dm = Authentication.Utility.AdminGetClient(Request.Url);     
            Page.Title = dm.DmName;         
        }
        else { Response.Redirect("~/Fail.aspx"); }           
          
           
    }
}
