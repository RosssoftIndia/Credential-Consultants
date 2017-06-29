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

public partial class secure_Popup_Addschool : System.Web.UI.Page
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
    {   //track buttons
        SetDefaultButton(this.Page, frma1_institution, frma1_btn_submit);
        SetDefaultButton(this.Page, frma1_degree, frma1_btn_submit);
        SetDefaultButton(this.Page, frma1_city, frma1_btn_submit);
        SetDefaultButton(this.Page, frma1_state, frma1_btn_submit);

        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            ClientAdmin.Utility.Getcountry(frma1_opt_country);
            ClientAdmin.Utility.Getyear(frma1_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
            ClientAdmin.Utility.Getyear(frma1_start_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
            ClientAdmin.Utility.Getyear(frma1_end_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
            ClientAdmin.Utility.Getmonth(frma1_month);
            ClientAdmin.Utility.Getdate(frma1_date);
           

            //wait process
            frma1_opt_country.Attributes.Add("onchange", "Loading(true);");
            frma1_btn_clear.Attributes.Add("onClick", "Loading(true);");
            frma1_btn_submit.Attributes.Add("onClick", "Loading(true);");
            frma1_option_degree.Attributes.Add("onchange", "Loading(true);");
            frma1_option_graduate.Attributes.Add("onchange", "Loading(true);");


          
        }

    }
    protected void frma1_opt_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        selected_country("New"); 
    }   
    protected void frma1_btn_clear_Click(object sender, EventArgs e)
    {
        clear_school("clearbtn");
    }
    protected void frma1_btn_submit_Click(object sender, EventArgs e)
    {
        //check for new entry
        int institution_id = 0;
        int degree_id = 0;
        Page.Validate("frma1_countrygroup");
        if (Page.IsValid)
        {
            Page.Validate("frma1_group");
            if (Page.IsValid)
            {
                institution_id = ClientAdmin.Utility.AddNew_institution(frma1_institution.Text, Convert.ToInt32(frma1_opt_country.SelectedValue.ToString()), "HighSchool", Session["Admin_Customer"].ToString());
            }
            if (frma1_option_graduate.SelectedItem.ToString() == "Yes")
            {
                Page.Validate("frma1_group3");
            }

            if (frma1_degree.Visible == true)
            {
                Page.Validate("frma1_group2");
                if (Page.IsValid)
                {
                    degree_id = ClientAdmin.Utility.AddNew_degree(frma1_degree.Text, Convert.ToInt32(frma1_opt_country.SelectedValue.ToString()), "HighSchool", Session["Admin_Customer"].ToString());
                }
            }
            else
            {
                degree_id = Convert.ToInt32(frma1_option_degree.SelectedValue.ToString());
            }

            if (Page.IsValid)
            {
                string DateDegreeAwarded;
                int graduated;
                if (frma1_option_graduate.SelectedValue.ToString() == "True")
                {
                    DateDegreeAwarded = frma1_date.SelectedValue.ToString() + "/" + frma1_month.SelectedValue.ToString() + "/" + frma1_year.SelectedValue.ToString();
                }
                else
                {
                    DateDegreeAwarded = "Null";
                }
                if (frma1_option_graduate.SelectedValue.ToString() == "True")
                {
                    graduated = 1;
                }
                else
                {
                    graduated = 0;
                }
                bool result = ClientAdmin.Utility.create_education(Convert.ToInt32(Session["Request_id"].ToString()), Convert.ToInt32(frma1_opt_country.SelectedValue.ToString()), 1, institution_id, frma1_start_year.SelectedItem.ToString(), frma1_end_year.SelectedItem.ToString(), degree_id, graduated, DateDegreeAwarded, 1, frma1_city.Text, frma1_state.Text);

                if (result == true)
                {
                    //1
                    //frma1_option_institution.Visible = true;
                    //frma1_RequiredFieldValidator3.Visible = true;
                    //frma1_institution.Visible = false;
                    //2
                    frma1_option_degree.Visible = true;
                    frma1_RequiredFieldValidator4.Visible = true;
                    frma1_degree.Visible = false;
                    clear_school("submitbtn");

                    Response.Redirect("~/secure/Request_complete.aspx?id=3");

                }
                else
                { Response.Redirect("~/secure/Request_complete.aspx?id=2"); }
            }
        }
    }
    protected void frma1_option_graduate_SelectedIndexChanged(object sender, EventArgs e)
    {
        selected_graduate(); 
    }
    protected void frma1_option_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frma1_option_degree.SelectedItem.ToString() == "Add New")
        {
            ClientAdmin.Utility.SetFocus(frma1_degree);
            frma1_option_degree.Visible = false;
            frma1_RequiredFieldValidator4.Visible = false;
            frma1_degree.Visible = true;
        }
    }


    //common functions
    public void clear_school(string clear_mode)
    {
        // clear fields       
        frma1_city.Text = "";
        frma1_date.SelectedIndex = 0;
        frma1_end_year.SelectedIndex = 0;
        frma1_month.SelectedIndex = 0;
        frma1_option_graduate.SelectedIndex = 0;
        frma1_start_year.SelectedIndex = 0;
        frma1_year.SelectedIndex = 0;
        frma1_optional.Visible = false;
        frma1_state.Text = "";
        frma1_institution.Text = "";
        frma1_degree.Text = "";

        //clearing old institution and degree
       // frma1_option_institution.Items.Clear();
        frma1_option_degree.Items.Clear();

        //hide fields
        //1
       // frma1_option_institution.Visible = true;
        //frma1_RequiredFieldValidator3.Visible = true;
        //frma1_institution.Visible = false;
        //2
        frma1_option_degree.Visible = true;
        frma1_RequiredFieldValidator4.Visible = true;
        frma1_degree.Visible = false;


        switch (clear_mode)
        {
            case "clearbtn":

                ClientAdmin.Utility.Getdegree(frma1_option_degree, 0, Convert.ToInt32(frma1_opt_country.SelectedValue.ToString()), Session["Admin_Customer"].ToString());
                ClientAdmin.Utility.Add_New(frma1_option_degree);
                break;
            case "submitbtn":
                frma1_opt_country.SelectedIndex = 0;
                break;
        }

    }
    public void selected_country(string option)
    {
        if (option == "New")
        {
            clear_school("countrybtn");
        }

        if (frma1_opt_country.SelectedIndex != 0)
        {
            frma1_autoComplete1.ContextKey = frma1_opt_country.SelectedValue.ToString() + "|" + Session["Admin_Customer"].ToString();  
           ClientAdmin.Utility.Getdegree(frma1_option_degree, 0, Convert.ToInt32(frma1_opt_country.SelectedValue.ToString()), Session["Admin_Customer"].ToString());
           ClientAdmin.Utility.Add_New(frma1_option_degree);
           
        }
        else
        {          
            clear_school("countrybtn");
        }
    }
    public void selected_graduate()
    {
        if (frma1_option_graduate.SelectedValue.ToString()  == "True")
        {
            frma1_optional.Visible = true;
        }
        else
        {
            frma1_optional.Visible = false;
        }
    }

    //script
    public void SetDefaultButton(Page page, TextBox textControl, Button defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    }
    protected void frma1_start_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frma1_start_year.SelectedValue.ToString() != "")
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            frma1_year.Items.Clear();
            frma1_end_year.Items.Clear();
            ClientAdmin.Utility.Getyear(frma1_year, Convert.ToInt32(frma1_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
            ClientAdmin.Utility.Getyear(frma1_end_year, Convert.ToInt32(frma1_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
        }
    }
}
