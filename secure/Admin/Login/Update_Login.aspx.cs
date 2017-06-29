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

public partial class secure_Admin_Login_Update_Login : System.Web.UI.Page
{
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Employee_id"] = Request.QueryString["Empid"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }             
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        bool result = false;
        TextBox Name = (TextBox)DetailsView_employee.FindControl("Name");
        TextBox Password = (TextBox)DetailsView_employee.FindControl("Password");
        
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_clientLoginUpdate(Name.Text, Password.Text,Session["Employee_id"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }   
        
        if (result == true)
        {
            Response.Redirect("~/secure/Admin/Login/Browse_Login.aspx");
        }

    }
    protected void DetailsView_employee_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":                  
                  MasterAdmin.Utility.Grid_clientLoginSelect(DetailsView_employee, Session["Employee_id"].ToString());               
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }         
        }
    }
    protected void Clientdrp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList clientdrp = (DropDownList)DetailsView_employee.FindControl("Clientdrp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Getclient(clientdrp);
                    MasterAdmin.Utility.Selectclient(Session["Employee_id"].ToString(),clientdrp);
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }

    }
}
