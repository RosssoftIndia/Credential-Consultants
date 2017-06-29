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

public partial class secure_EditMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Authenticate"].ToString() == "Approved")
        {
            if (Session["Clientsettings"].ToString() != "Empty")
            {
                Authentication.Utility.AdminDomainAttributes dm = Authentication.Utility.AdminGetClient(Request.Url);     
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
            }
            else { Response.Redirect("~/Fail.aspx"); }           
          

           switch (Session["Admin_Type"].ToString())
           {
               case "ADMIN":
                   clientblk.Visible = false;
                   adminblk.Visible = true;
                   break;
               case "USER":
                   adminblk.Visible = false;
                   clientblk.Visible = true;                   
                   break;
               default:
                   Response.Redirect("~/Fail.aspx");
                   break;
           }

        }
        else
        {
            Response.Redirect("~/Fail.aspx");
        }
    }
}
