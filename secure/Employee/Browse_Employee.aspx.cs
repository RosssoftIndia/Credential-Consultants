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

public partial class secure_Employee_Browse_Employee : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }      


    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }      
    protected void grid_Employee_Load(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_LoginBrowse(grid_Employee, Convert.ToInt32(Session["Admin_Customer"].ToString()));
                break;
            case "ADMIN":               
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }   
     
    }
    protected void grid_Employee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Employee.PageIndex = e.NewPageIndex;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_LoginBrowse(grid_Employee, Convert.ToInt32(Session["Admin_Customer"].ToString()));
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        } 
    }
}
