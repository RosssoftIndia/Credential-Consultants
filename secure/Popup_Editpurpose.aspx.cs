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

public partial class secure_Popup_Editpurpose : System.Web.UI.Page
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
            #region purpose
            ClientAdmin.Utility.Getpurpose(frm4_option_purpose);
            #endregion

            if (Request.QueryString["id"] != null)
            {
                DataSet ds = ClientAdmin.Utility.Purpose_application(Request.QueryString["id"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    frm4_option_purpose.SelectedValue = ds.Tables[0].Rows[0]["Purpose_Id"].ToString();
                    purpose_selected(frm4_option_purpose.SelectedItem.ToString());
                    frm4_board.Text = ds.Tables[0].Rows[0]["Eval_Board"].ToString();
                    frm4_evaluation.Text = ds.Tables[0].Rows[0]["Eval_other"].ToString();
                    frm4_institution.Text = ds.Tables[0].Rows[0]["Eval_institution"].ToString();
                    frm4_lawfirm.Text = ds.Tables[0].Rows[0]["Eval_Attorney"].ToString();
                    frm4_military.Text = ds.Tables[0].Rows[0]["Eval_Military_Recruiter"].ToString();
                    frm4_state.Text = ds.Tables[0].Rows[0]["Eval_State"].ToString();
                    frm4_organization.Text = ds.Tables[0].Rows[0]["Eval_organization"].ToString();

                }

            }
        }

        
    }


    #region purpose tab
    protected void frm4_option_purpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        frm4_institution.Text = "";
        frm4_lawfirm.Text = "";
        frm4_military.Text = "";
        frm4_organization.Text = "";
        frm4_state.Text = "";
        frm4_board.Text = "";
        frm4_evaluation.Text = "";
        purpose_selected(frm4_option_purpose.SelectedItem.ToString());

    }
    private void purpose_selected(string selecteditem)
    {
        switch (selecteditem)
        {
            case "Admission to High School":
                frm4_optional.Visible = true;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
            case "Admission to College/University":
                frm4_optional.Visible = true;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
            case "Employment":
                frm4_optional.Visible = false;
                frm4_optional1.Visible = true;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
            case "Immigration":
                frm4_optional.Visible = false;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = true;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
            case "Professional Licensing/Registration":
                frm4_optional.Visible = false;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = true;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
            case "Military":
                frm4_optional.Visible = false;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = true;
                frm4_optional5.Visible = false;
                break;
            case "Other":
                frm4_optional.Visible = false;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = true;
                break;
            default:
                frm4_optional.Visible = false;
                frm4_optional1.Visible = false;
                frm4_optional2.Visible = false;
                frm4_optional3.Visible = false;
                frm4_optional4.Visible = false;
                frm4_optional5.Visible = false;
                break;
        }
    }
    protected void updatebtn_Click(object sender, EventArgs e)
    {
        bool result = false;
        Page.Validate("frm4_group");
        if (Page.IsValid)
        {
            result = ClientAdmin.Utility.update_purpose(Convert.ToInt32(Session["Applicant_id"].ToString()), Convert.ToInt32(frm4_option_purpose.SelectedValue.ToString()), frm4_institution.Text, frm4_organization.Text, frm4_lawfirm.Text, frm4_board.Text, frm4_state.Text, frm4_military.Text, frm4_evaluation.Text, Convert.ToInt32(Session["Request_id"].ToString()));
            if (result)
            {              
                Response.Redirect("~/secure/Request_complete.aspx?id=1");
            }
            else
            {
                Response.Redirect("~/secure/Request_complete.aspx?id=0");
            }
        }

    }
    #endregion
 
}
