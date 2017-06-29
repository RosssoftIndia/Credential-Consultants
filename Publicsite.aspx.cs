using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Publicsite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //title
        OrgTitle.InnerHtml = "WELCOME TO " + Authentication.Utility.GetSubDomain("", "", Request.Url, "title");
        // session customerid
        Session["Customer_id"] = Authentication.Utility.GetSubDomain("", "", Request.Url, "check_customer");



        btn1.Attributes.Add("onclick", "PopupCenter('Default.aspx', 'Online_application',0,0);");
        btn2.Attributes.Add("onclick", "PopupCenter('Login_Status.aspx','Application_Status',0,0);");
        btn3.Attributes.Add("onclick", "PopupCenter('Login_Payment.aspx','Payment_Processing',0,0);");      
        btn4.Attributes.Add("onclick", "PopupCenter('Login_Print.aspx','Application_Print',0,0);");
        
      
    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        txtbox.Text = Credentialpage.Utility.create_Randomid();
    }
}
