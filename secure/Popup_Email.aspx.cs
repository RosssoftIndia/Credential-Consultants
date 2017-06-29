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

public partial class secure_Popup_Email : System.Web.UI.Page
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
            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].ToString() == "1")
                {
                    EmailUpdate.Visible = true;
                    EmailAdd.Visible = false;
                    if (Request.QueryString["rowid"] != null)
                    {

                        DataSet ds = ClientAdmin.Utility.getemail_byid(Request.QueryString["rowid"].ToString());
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            frm5_ename.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            frm5_email.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        }
                    }
       
                }
                else
                {
                    EmailUpdate.Visible = false;
                    EmailAdd.Visible = true;

                }
            }
        }




    }

    protected void btn_Click(object sender, EventArgs e)
    {
        bool result=false;
        Page.Validate("frm5_group3");
        if (Page.IsValid)
        {

            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "EmailAdd":
                    if (Request.QueryString["cid"] != null)
                    {
                        result = ClientAdmin.Utility.create_Email_Delivery(4, Convert.ToInt32(Session["Request_id"].ToString()), frm5_ename.Text, frm5_email.Text, 1, 1, "Email", Request.QueryString["cid"].ToString());
                        frm5_ename.Text = "";
                        frm5_email.Text = "";
                        if (result)
                        {
                            Response.Redirect("~/secure/Request_complete.aspx?id=15");
                        }
                        else
                        {
                            Response.Redirect("~/secure/Request_complete.aspx?id=14");
                        }
                    }
                    break;
                case "EmailUpdate":
                    if (Request.QueryString["rowid"] != null)
                    {
                        result = ClientAdmin.Utility.updateemail(frm5_ename.Text, frm5_email.Text, Request.QueryString["rowid"].ToString());
                        frm5_ename.Text = "";
                        frm5_email.Text = "";
                    }
                    if (result)
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=1");
                    }
                    else
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=0");
                    }
                    break;
            }
        }     

        
    }   
    
   
}


            
