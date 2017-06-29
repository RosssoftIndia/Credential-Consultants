using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class secure_Menuctrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
}
