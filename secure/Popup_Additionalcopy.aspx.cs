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

public partial class secure_Popup_Additionalcopy : System.Web.UI.Page
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
            if (Request.QueryString["cid"] != null)
            {
                ClientAdmin.Utility.Getdeliverytype(frm5_deliverytypeaddl, Request.QueryString["cid"].ToString());
            }
            ClientAdmin.Utility.Getcountry(frm5_countryaddl);
            if (Request.QueryString["id"] != null)
            {
                
                if (Request.QueryString["id"].ToString() == "1")
                {
                    AdditionalUpdate.Visible = true;
                    AdditionalAdd.Visible = false;
                    frm5_btn_canceladdl.Visible = false;
 
                    if (Request.QueryString["rowid"] != null)
                    {
                      string result=  ClientAdmin.Utility.CheckPrimary(Request.QueryString["rowid"].ToString(), Session["Request_id"].ToString());
                        ClientAdmin.Utility.populateAddress(frm5_Fnameaddl, frm5_add1addl, frm5_add2addl, frm5_cityaddl, frm5_stateaddl, frm5_zipaddl, frm5_countryaddl, frm5_deliverytypeaddl, frm5_copies_addl, Request.QueryString["rowid"].ToString(),frm5_addlinstname);
                      frm5_Additionalrequestform.Visible = true; 
                      if (result == "Primary")
                      {
                          frm5_addlradiobtn.SelectedValue = "False";
                      }
                      else
                      {
                          frm5_addlradiobtn.SelectedValue = "True"; 
                      }
                    }


                }
                else
                {
                 
                    AdditionalUpdate.Visible =false;
                    AdditionalAdd.Visible = true;
                }
                

            }
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        bool result = false;
        switch (btn.ID)
        {
            case "AdditionalAdd":

                Page.Validate("frm5_addlgroup");
                if (Page.IsValid)
                {
                    result = ClientAdmin.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_deliverytypeaddl.SelectedValue.ToString()), Convert.ToInt32(Session["Request_id"].ToString()), frm5_Fnameaddl.Text, frm5_add1addl.Text, frm5_add2addl.Text, frm5_cityaddl.Text, frm5_stateaddl.Text, frm5_zipaddl.Text, Convert.ToInt32(frm5_countryaddl.SelectedValue.ToString()), Convert.ToInt32(frm5_copies_addl.SelectedValue.ToString()), "Additional", "Additional",frm5_addlinstname.Text);
                   
                    frm5_btn_canceladdl_Click(this, EventArgs.Empty);
                    if (result)
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=13");
                    }
                    else { Response.Redirect("~/secure/Request_complete.aspx?id=12"); }
                }
                
                break;

            case "AdditionalUpdate":
                Page.Validate("frm5_addlgroup");
                if (Page.IsValid)
                {
                    if (Request.QueryString["rowid"] != null)
                    {
                        result = ClientAdmin.Utility.UpdateAdditionalCopy(Convert.ToInt32(frm5_deliverytypeaddl.SelectedValue.ToString()), Convert.ToInt32(Session["Request_id"].ToString()), frm5_Fnameaddl.Text, frm5_add1addl.Text, frm5_add2addl.Text, frm5_cityaddl.Text, frm5_stateaddl.Text, frm5_zipaddl.Text, Convert.ToInt32(frm5_countryaddl.SelectedValue.ToString()), Convert.ToInt32(frm5_copies_addl.SelectedValue.ToString()), "Additional", "Additional", Request.QueryString["rowid"].ToString(),frm5_addlinstname.Text);

                        frm5_btn_canceladdl_Click(this, EventArgs.Empty);
                        if (result)
                        {
                            Response.Redirect("~/secure/Request_complete.aspx?id=1");
                        }
                        else { Response.Redirect("~/secure/Request_complete.aspx?id=0"); }
                    }
                }
                
                break;



        }
    }
    protected void frm5_addlradiobtn_SelectedIndexChanged(object sender, EventArgs e)
    {

        frm5_Additionalrequestform.Visible = true;

        if (frm5_addlradiobtn.SelectedValue.ToString() == "False")
        {
            ClientAdmin.Utility.GetSameAddress(frm5_Fnameaddl, frm5_add1addl, frm5_add2addl, frm5_cityaddl, frm5_stateaddl, frm5_zipaddl, frm5_countryaddl, frm5_deliverytypeaddl, Convert.ToInt32(Session["Request_id"].ToString()));
        }
        else
        {
            frm5_Additionalrequestform.Visible = false; 
            clear(); 
            frm5_Additionalrequestform.Visible = true;
        }
        


    }
    protected void frm5_btn_canceladdl_Click(object sender, EventArgs e)
    {
        
        frm5_Additionalrequestform.Visible = false;
        frm5_addlradiobtn.ClearSelection();
        clear(); 
    }

    protected void clear()
    {
        frm5_copies_addl.SelectedIndex = 0;
        frm5_add1addl.Text = "";
        frm5_add2addl.Text = "";
        frm5_stateaddl.Text = "";
        frm5_cityaddl.Text = "";
        frm5_Fnameaddl.Text = "";
        frm5_deliverytypeaddl.SelectedIndex = 0;
        frm5_countryaddl.SelectedIndex = 0;
        frm5_zipaddl.Text = "";
        frm5_addlinstname.Text = ""; 
    }
   
}


            
