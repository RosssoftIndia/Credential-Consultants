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

public partial class secure_Popup_Service : System.Web.UI.Page
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
                ClientAdmin.Utility.Grid_service(servicegrid, Request.QueryString["cid"].ToString());
                ClientAdmin.Utility.Grid_addservice(addservicegrid, Request.QueryString["cid"].ToString());
            }

            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].ToString() == "1")
                {
                    //button 
                    serviceAdd.Visible = false;
                    serviceupdate.Visible = true;
                    //Evaluation service
                    foreach (GridViewRow row in servicegrid.Rows)
                    {
                        HtmlInputCheckBox Checkboxtag = ((HtmlInputCheckBox)row.FindControl("Checkbox1"));
                        DropDownList dp = (DropDownList)row.FindControl("drpCount");
                        Label lblType = (Label)row.FindControl("lblType");
                        DataSet ds = ClientAdmin.Utility.Grid_Evaluationservice(Session["Request_id"].ToString());
                        if (lblType.Text == "Evaluation Multiplier")
                        {
                            Checkboxtag.Attributes.Add("onclick", "javascript:return " +
                             "confirm('Please indicate the quantity for this service.')");
                            ClientAdmin.Utility.GetCount(dp, 15);
                            dp.Visible = true;

                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                string serviceid = ds.Tables[0].Rows[i]["Service_Id"].ToString();
                                string checkboxvalue = Checkboxtag.Value.ToString();
                                if (serviceid == checkboxvalue)
                                {
                                    Checkboxtag.Checked = true;
                                    dp.SelectedValue = ds.Tables[0].Rows[i]["Countno"].ToString();
                                }
                            }
                        }
                    }

                    //Additional service

                    foreach (GridViewRow row in addservicegrid.Rows)
                    {
                        HtmlInputCheckBox Checkboxtag = ((HtmlInputCheckBox)row.FindControl("Checkbox2"));
                        DropDownList dp = (DropDownList)row.FindControl("drpCount");
                        DataSet ds = ClientAdmin.Utility.Grid_Additionalservice(Session["Request_id"].ToString());
                        Label lblType = (Label)row.FindControl("lblType");
                        if (lblType.Text == "Additional Multiplier")
                        {
                            Checkboxtag.Attributes.Add("onclick", "javascript:return " +
                             "confirm('Please indicate the quantity for this service.')");
                            ClientAdmin.Utility.GetCount(dp, 15);
                            dp.Visible = true;

                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                string serviceid = ds.Tables[0].Rows[i]["Service_Id"].ToString();
                                string checkboxvalue = Checkboxtag.Value.ToString();
                                if (serviceid == checkboxvalue)
                                {
                                    Checkboxtag.Checked = true;
                                    dp.SelectedValue = ds.Tables[0].Rows[i]["Countno"].ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    //button 
                    serviceAdd.Visible = true;
                    serviceupdate.Visible = false;
                }
            }
        }
    }

    protected void addservicegrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in addservicegrid.Rows)
        {
            DropDownList drp = (DropDownList)row.FindControl("drpCount");
            HtmlInputCheckBox Checkboxtag = ((HtmlInputCheckBox)row.FindControl("Checkbox2"));
            Label lblType = (Label)row.FindControl("lblType");
            if (lblType.Text == "Additional Multiplier")
            {
                Checkboxtag.Attributes.Add("onclick", "javascript:return " +
                 "confirm('Please indicate the quantity for this service.')");
                Credentialpage.Utility.GetCount(drp, 100);
                drp.Visible = true;

            }
        }
    }
    protected void servicegrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in servicegrid.Rows)
        {
            DropDownList drp = (DropDownList)row.FindControl("drpCount");
            HtmlInputCheckBox Checkboxtag = ((HtmlInputCheckBox)row.FindControl("Checkbox1"));
            Label lblType = (Label)row.FindControl("lblType");
            if (lblType.Text == "Evaluation Multiplier")
            {
                Checkboxtag.Attributes.Add("onclick", "javascript:return " +
                 "confirm('Please indicate the quantity for this service.')");
                Credentialpage.Utility.GetCount(drp, 100);
                drp.Visible = true;

            }
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        bool result = false;
        switch (btn.ID)
        {
            case "serviceAdd":
              result = frm6_perform_check();
              if (result)
              {
                  Response.Redirect("~/secure/Request_complete.aspx?id=4");
              }
                break;

            case "serviceupdate":
                ClientAdmin.Utility.clear_evaluation_services(Convert.ToInt32(Session["Request_id"].ToString()));
               result = frm6_perform_check();
               if (result)
               {
                   Response.Redirect("~/secure/Request_complete.aspx?id=5");
               }
                break;


        }


    }


    public bool frm6_perform_check()
    {
        bool result = false;
        //check evaluation services
        int tagvalue = frm6_evaluation_check();
        if (tagvalue != 0)
        {
            //check aditional services
            frm6_Additional_check();
            result = true;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('You must select a Service to Continue')</script>");
            result = false;
        }
        return result;
    }
    public int frm6_evaluation_check()
    {
        //check evaluation services
        int tagvalue = 0; bool process_valid = false;

        foreach (GridViewRow row in servicegrid.Rows)
        {
            HtmlInputCheckBox servicetag = ((HtmlInputCheckBox)row.FindControl("Checkbox1"));
            if (servicetag.Checked)
            {
                process_valid = true;
                break;
            }
            else
            {
                process_valid = false;
            }
        }

        if (process_valid)
        {
            foreach (GridViewRow row in servicegrid.Rows)
            {
                //HtmlInputRadioButton servicetag = ((HtmlInputRadioButton)row.FindControl("Radio1"));
                HtmlInputCheckBox servicetag = ((HtmlInputCheckBox)row.FindControl("Checkbox1"));
                Label lblType = (Label)row.FindControl("lblType");
                DropDownList drp = (DropDownList)row.FindControl("drpCount");
                if (servicetag.Checked)
                {
                    tagvalue = Convert.ToInt32(servicetag.Value.ToString());
                   if (lblType.Text == "Evaluation Multiplier")
                   {
                       for (int i = 0; i < Convert.ToInt32(drp.SelectedValue.ToString()); i++)
                       {
                           Credentialpage.Utility.create_Service(Convert.ToInt32(Session["Request_id"].ToString()), tagvalue);
                       }
                   }
                   else
                   {
                       Credentialpage.Utility.create_Service(Convert.ToInt32(Session["Request_id"].ToString()), tagvalue);
                   }
                   //ClientAdmin.Utility.create_Service(Convert.ToInt32(Session["Request_id"].ToString()), tagvalue);

                }

            }
            tagvalue = 1;
        }
        else
        {
            tagvalue = 0;
        }


        return tagvalue;
    }
    public void frm6_Additional_check()
    {
        //check additional services
        int tagvalue = 0;
        foreach (GridViewRow row in addservicegrid.Rows)
        {
            bool result = false;
            HtmlInputCheckBox servicetag = ((HtmlInputCheckBox)row.FindControl("Checkbox2"));
            Label lblType = (Label)row.FindControl("lblType");
            DropDownList drp = (DropDownList)row.FindControl("drpCount");

            switch (servicetag.Checked)
            {
                case true:
                    tagvalue = Convert.ToInt32(servicetag.Value.ToString());
                    if (lblType.Text == "Additional Multiplier")
                    {
                        for (int i = 0; i < Convert.ToInt32(drp.SelectedValue.ToString()); i++)
                        {
                            result = ClientAdmin.Utility.create_Service(Convert.ToInt32(Session["Request_id"].ToString()), tagvalue);
                        }
                    }
                    else
                    {
                        result = ClientAdmin.Utility.create_Service(Convert.ToInt32(Session["Request_id"].ToString()), tagvalue);
                    }
                    break;
                default:
                    break;
            }

        }
    }   
 
}





            
