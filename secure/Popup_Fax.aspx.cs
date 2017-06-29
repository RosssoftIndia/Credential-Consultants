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

public partial class secure_Popup_Fax : System.Web.UI.Page
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
                    FaxUpdate.Visible = true;
                    FaxAdd.Visible = false;
                    if (Request.QueryString["rowid"] != null)
                    {

                        DataSet ds = ClientAdmin.Utility.getfax_byid(Request.QueryString["rowid"].ToString());
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            frm5_faxno.Text = ds.Tables[0].Rows[0]["Faxno"].ToString();
                            frm5_attn.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }
       
                }

                if (Request.QueryString["id"].ToString() == "0")
                {
                    FaxUpdate.Visible = false;
                    FaxAdd.Visible = true;

                }
            }
        }




    }

    protected void btn_Click(object sender, EventArgs e)
    {
        bool result=false;
        Page.Validate("frm5_group2");
        if (Page.IsValid)
        {
            if (Request.QueryString["cid"] != null)
            {
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "FaxAdd":

                        result = ClientAdmin.Utility.create_Evaluation_Delivery(4, Convert.ToInt32(Session["Request_id"].ToString()), frm5_attn.Text, frm5_faxno.Text, 1, 1, "Fax", Request.QueryString["cid"].ToString());
                    frm5_faxno.Text = "";
                    frm5_attn.Text = "";
                    if (result)
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=11");
                    }
                    else
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=10");
                    }
                    break;
                case "FaxUpdate":
                    if (Request.QueryString["rowid"] != null)
                    {
                        result = ClientAdmin.Utility.updatefax(frm5_attn.Text, frm5_faxno.Text, Request.QueryString["rowid"].ToString());
                        frm5_faxno.Text = "";
                        frm5_attn.Text = "";
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
    
   
}


            
