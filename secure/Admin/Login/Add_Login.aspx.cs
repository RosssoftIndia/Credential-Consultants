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

public partial class secure_Admin_Login_Add_Login : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            DropDownList clientdrp = (DropDownList)DetailsView_employee.FindControl("Clientdrp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                   break;
                case "ADMIN":
                    MasterAdmin.Utility.Getclient(clientdrp);
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                
        }

    }
    protected void Add_Click(object sender, EventArgs e)
    {
        Label errorlbl = (Label)DetailsView_employee.FindControl("lblerror");
        DropDownList clientdrp = (DropDownList)DetailsView_employee.FindControl("Clientdrp");
        if (clientdrp.SelectedIndex != 0)
        {
           string result="";
            TextBox username = (TextBox)DetailsView_employee.FindControl("Name");
            TextBox Password = (TextBox)DetailsView_employee.FindControl("Password");         

            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":
                  result =  MasterAdmin.Utility.Grid_clientLoginAdd(Convert.ToInt32(clientdrp.SelectedValue.ToString()), username.Text, Password.Text);
                  if (result == "Login exists for this Client") { errorlbl.Text = "Login exists for this Client"; }
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }   

           
            if (result == "true")
            {
                Response.Redirect("~/secure/Admin/Login/Browse_Login.aspx");
            }
        }
        else
        {
            errorlbl.Text = "Select Client"; 
        }
    }
   
   
}
