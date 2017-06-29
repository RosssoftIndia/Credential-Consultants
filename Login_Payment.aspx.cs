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

public partial class Login_Payment : System.Web.UI.Page
{
    string txt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //title
        OrgTitle.InnerHtml = Authentication.Utility.GetSubDomain("0", "0", Request.Url, "title");
        // session customerid
        Session["Customer_id"] = Authentication.Utility.GetSubDomain("0", "0", Request.Url, "check_customer");

        HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
        msg.InnerHtml = "<span style='color:Red;font-weight:bold;'>Note:</span>&nbsp;To Make OnlinePayment,please enter the corresponding application file number.";
      
    }
   
    protected void statusButton_Click(object sender, EventArgs e)
    {
        Session["file"] = "Empty";
        if (Session["Customer_id"].ToString() != "Empty")
        {
            string result = ClientAdmin.Utility.check_filenumber(txtfile.Text.ToString(), Session["Customer_id"].ToString());
           if (result == "Access_Denied")
           {
               txterror.Visible = true;  
           }
           else
           {
               txterror.Visible = false;
               Session["file"] = txtfile.Text;
               Response.Redirect("~/Payment.aspx?id="+ txtfile.Text+"&mode=2");
               //Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "ShowValue()", true);              
              
           }
        }
    }

}
