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

public partial class Login : System.Web.UI.Page
{
    Authentication.Utility.AdminDomainAttributes dm = new Authentication.Utility.AdminDomainAttributes();
     
    protected void Page_Load(object sender, EventArgs e)
    {
         bool ClientIsValid = Authentication.Utility.AdminClientIsValid(Request.Url);
         if (ClientIsValid)
         { 
             dm = Authentication.Utility.AdminGetClient(Request.Url);          
            
             Page.Title = dm.DmName; 
             OrgTitle.InnerHtml = dm.DmName;
             Session["Clientsettings"] = dm;
             Session["Admin_Customer"] = dm.DmID;
             Session["Customer_id"] = dm.DmID;
           Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
         }
              
        HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
        //msg.InnerHtml  = " Copyright © 2008 Credential Consultants Inc. All Rights Reserved.<a href='' style='text-decoration: none'>Privacy Policy</a>|<a href='' style='text-decoration: none'>Terms of Service</a>";
        msg.InnerHtml = " Copyright © 2008 " + OrgTitle.InnerHtml + ". All Rights Reserved.";
       


    }
   
    protected void Loginctrl_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string result = Authentication.Utility.Login(Loginctrl.UserName.ToString(), Loginctrl.Password.ToString(),dm.Subdm);

        DataSet ds;
        Session["User"] = Loginctrl.UserName.ToString();
        switch(result) 
        {
            case "USER":
            e.Authenticated = true;
            Session["Authenticate"] = "Approved";
            ds = Authentication.Utility.Logininfo(Loginctrl.UserName.ToString(), Loginctrl.Password.ToString());
                Session["Admin_Customer"] = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
                Session["Admin_Type"] = "USER";
            FormsAuthentication.RedirectFromLoginPage(Loginctrl.UserName, Loginctrl.RememberMeSet);                     
            Response.Redirect("~/secure/Home.aspx");
                break;
            case "ADMIN":
                e.Authenticated = true;
                Session["Authenticate"] = "Approved";
                ds = Authentication.Utility.Logininfo(Loginctrl.UserName.ToString(), Loginctrl.Password.ToString());
                Session["Admin_Customer"] = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
                //Session["Customer_id"] = ds.Tables[0].Rows[0]["Customer_Id"].ToString();  
                Session["Admin_Type"] = "ADMIN";
                FormsAuthentication.RedirectFromLoginPage(Loginctrl.UserName, Loginctrl.RememberMeSet);       
                Response.Redirect("~/secure/Home.aspx");
                break;
            case "Access_Denied":
            e.Authenticated = false;
            Session["Authenticate"] = "Declined";
                Session["Admin_Type"] = "Declined";
                break;

        }
    }   
}
