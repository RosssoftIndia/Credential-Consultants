using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Collections;
using System.Data;

public partial class Thanku : System.Web.UI.Page
{
    string Subdomain = "";
    string fileno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bool ClientIsValid = false;
        if (Request.QueryString["subdomain"] != null)
        {
            Subdomain = Request.QueryString["subdomain"].ToString();
        }
        else
        {
            Subdomain = "nosubdomain";
        }
        ClientIsValid = Authentication.Utility.ClientIsValid(Request.Url, Subdomain);
        //Client Check

        if (ClientIsValid)
        {
            int clientid = 0;
            Authentication.Utility.DomainAttributes dm = Authentication.Utility.GetClient(Request.Url, Subdomain);
          
            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Session["Customer_id"] = dm.SubDmID;
                
            }
            else
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Session["Customer_id"] = dm.DmID;
              
            }
                Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
            }

        if (!Page.IsPostBack)
        {
            //load cost
            string status = Request.QueryString["valid"].ToString();
            switch (status)
            {
                case "True":
                msg.InnerHtml = "Your transaction has been successfully processed and an E-mail has been sent to your account";
                    break;
                case "False":
                msg.InnerHtml = "Your transaction has been unsuccessfully processed Contact Admin";
                    break;
                default :
                    msg.InnerHtml = "you have chosen to pay later..<br/>" +                     
		        "<ul><li>After printing the application summary/review, please fill out the credit card section by hand.</li>" +
		        "<li>You may also pay with a credit card over the phone.</li></ul>";	
                    break;
            }
            
            string mode = Request.QueryString["mode"].ToString();
            switch (mode)
            {
                case "1":
                    processctrl.Visible = true;   
                    break;
                case "2":
                    processctrl.Visible = false;  
                    break;
            }
        }


    }


    protected void frm7_btn_Continue_Click(object sender, ImageClickEventArgs e)
    {
        if (Subdomain == "nosubdomain")
        {
        Response.Redirect("~/msg.aspx?id=" + Request.QueryString["id"].ToString());
    }
        else
        {
              Response.Redirect("~/msg.aspx?id=" + Request.QueryString["id"].ToString() + "&subdomain=" + Subdomain);
        }       
     
    }
}

