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

public partial class secure_Admin_Client_Login : System.Web.UI.Page
{
    string validation = "direct";
    protected void Page_Load(object sender, EventArgs e)
    {
        string Clientid = Request.QueryString["id"].ToString();
        if (Clientid != "")
        {
            bool result = MasterAdmin.Utility.Admin_Client(Clientid.ToString(), UserName, Password);                      
            if(result == true)
            {
                validation ="success";             
            }
            else
            {
                validation ="failed"; 
            }
            LoginButton_Click(this, EventArgs.Empty);
        }
    }

 

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        DataSet ds;
        AuthenticateEventArgs custom_event = new AuthenticateEventArgs(); 
        if (validation.ToString() == "success")
        {

            custom_event.Authenticated = true;          
            Session["Authenticate"] = "Approved";
            ds = MasterAdmin.Utility.Logininfo(UserName.Text.ToString(), Password.Text.ToString());
            Session["Admin_Customer"] = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
            Session["Admin_Type"] = "USER";
            FormsAuthentication.RedirectFromLoginPage(UserName.Text.ToString(),true);
            Response.Redirect("~/secure/Active_Application.aspx");
        }
        else
        {
            custom_event.Authenticated = false;
                    Session["Authenticate"] = "Declined";
                    Session["Admin_Type"] = "Declined";
        }
    }
}
