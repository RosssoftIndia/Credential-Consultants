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

public partial class secure_Popup_Editpersonalinfo : System.Web.UI.Page
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
            Page_Control_Initialization();
            if (Request.QueryString["id"] != null)
            {
                DataSet ds = ClientAdmin.Utility.Edit_application(Request.QueryString["id"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    frm1_Fname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    frm1_Mname.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                    frm1_Lname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                    bool gender = false;
                    if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Male") { gender = true; } else { gender = false; };
                    frm1_option_gender.SelectedValue = gender.ToString();

                    if (ds.Tables[0].Rows[0]["otherFirstName"].ToString() != "")
                    {
                        frm1_optin_name.SelectedValue = "True";
                        frm1_optional.Visible = true;
                        frm1_optFname.Text = ds.Tables[0].Rows[0]["otherFirstName"].ToString();
                        frm1_optMname.Text = ds.Tables[0].Rows[0]["otherMiddleName"].ToString();
                        frm1_optLname.Text = ds.Tables[0].Rows[0]["otherLastName"].ToString();
                    }

                    DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateOfBirth"].ToString());
                    frm1_option_month.SelectedValue = dt.Month.ToString();
                    frm1_option_date.SelectedValue = dt.Day.ToString();
                    frm1_option_year.SelectedValue = dt.Year.ToString();
                    frm1_address1.Text = ds.Tables[0].Rows[0]["Addressline1"].ToString();
                    frm1_address2.Text = ds.Tables[0].Rows[0]["Addressline2"].ToString();
                    frm1_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    frm1_Country_birth.SelectedValue = ds.Tables[0].Rows[0]["Countryofbirth"].ToString();
                    frm1_option_country.SelectedValue = ds.Tables[0].Rows[0]["CountryId"].ToString();
                    frm1_state.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                    frm1_zip.Text = ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString();
                    frm1_home_phone.Text = ds.Tables[0].Rows[0]["HomePhone"].ToString();
                    frm1_work_phone.Text = ds.Tables[0].Rows[0]["WorkPhone"].ToString();
                    frm1_cell_phone.Text = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                    frm1_primarymail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    frm1_confrprimary.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    if (ds.Tables[0].Rows[0]["PreviousCredential_id"].ToString() != "")
                    {
                        frm1_option_service.SelectedValue = "True";
                        frm1_optional1.Visible = true;
                        frm1_previousid.Text = ds.Tables[0].Rows[0]["PreviousCredential_id"].ToString();
                    }
                }
            }
        }
    }
    private void Page_Control_Initialization()
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        #region personalinfo
        ClientAdmin.Utility.Getmonth(frm1_option_month);
        ClientAdmin.Utility.Getdate(frm1_option_date);
        ClientAdmin.Utility.Getyear(frm1_option_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
        ClientAdmin.Utility.Getcountry(frm1_option_country);
        ClientAdmin.Utility.Getcountry(frm1_Country_birth);
        frm1_summary.HeaderText = "";
        #endregion       

    }

   

    #region personalinformation tab
    protected void frm1_optin_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm1_optin_name.SelectedValue == "True")
        {
            frm1_optional.Visible = true;

        }
        else
        {
            frm1_optional.Visible = false;
            frm1_optFname.Text = "";
            frm1_optMname.Text = "";
            frm1_optLname.Text = "";
        }
        // Credentialpage.Utility.Getoptionalcell(frm1_optin_name, frm1_optional);
    }
    protected void frm1_option_service_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm1_option_service.SelectedValue == "True")
        {
            frm1_optional1.Visible = true;

        }
        else
        {
            frm1_optional1.Visible = false;
            frm1_previousid.Text = "";
        }
        //Credentialpage.Utility.Getoptional(frm1_option_service, frm1_optional1);
    }
    protected void updatebtn_Click(object sender, EventArgs e)
    {
        bool result = false;
        Button btn = (Button)sender;
        switch (btn.ID)
        {
            case "personalinfoupdate":
                Page.Validate("frm1_group");
                if (Page.IsValid)
                {
                    string birth = frm1_option_year.SelectedValue.ToString() + "/" + frm1_option_month.SelectedValue.ToString() + "/" + frm1_option_date.SelectedValue.ToString();
                    result = ClientAdmin.Utility.update_Applicante(frm1_Fname.Text, frm1_Mname.Text, frm1_Lname.Text, frm1_option_gender.SelectedItem.ToString(), birth, frm1_address1.Text, frm1_address2.Text, frm1_city.Text, Convert.ToInt32(frm1_option_country.SelectedValue.ToString()), frm1_state.Text, frm1_zip.Text.ToString(), frm1_home_phone.Text.ToString(), frm1_work_phone.Text.ToString(), frm1_cell_phone.Text.ToString(), frm1_primarymail.Text, Convert.ToInt32(Session["Customer_id"].ToString()), frm1_optFname.Text, frm1_optMname.Text, frm1_optLname.Text, frm1_previousid.Text, Convert.ToInt32(frm1_Country_birth.SelectedValue.ToString()), 0, Session["Trackingcode"].ToString());
                    if (result)
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=1");
                    }
                    else
                    {
                        Response.Redirect("~/secure/Request_complete.aspx?id=0");
                    }
                }
                break;

        }

    }
    #endregion

 
 
  
}
