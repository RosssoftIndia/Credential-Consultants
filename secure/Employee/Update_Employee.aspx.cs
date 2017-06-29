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

public partial class secure_Employee_Update_Employee : System.Web.UI.Page
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
                result = ClientAdmin.Utility.Grid_LoginUpdate(Convert.ToInt32(Session["Admin_Customer"].ToString()), Name.Text, Password.Text, Convert.ToInt32(Session["Employee_id"].ToString()));
                break;
            case "ADMIN":                
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }           
       
        if (result == true)
        {
            Response.Redirect("~/secure/Employee/Browse_Employee.aspx");
        }

    }
    protected void DetailsView_employee_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                  ClientAdmin.Utility.Grid_LoginSelect(DetailsView_employee, Convert.ToInt32(Session["Employee_id"].ToString()));
                    break;
                case "ADMIN":
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
                    ClientAdmin.Utility.Getclient(clientdrp, Session["Customer_id"].ToString());
                    break;
                case "ADMIN":                 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }

    }
}
