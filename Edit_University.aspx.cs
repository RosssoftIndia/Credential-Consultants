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

public partial class Edit_University : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        //track buttons
        SetDefaultButton(this.Page, frma2_institution, frma2_btn_submit);
        SetDefaultButton(this.Page, frma2_degree, frma2_btn_submit);
        SetDefaultButton(this.Page, frma2_major, frma2_btn_submit);
        SetDefaultButton(this.Page, frma2_city, frma2_btn_submit);
        SetDefaultButton(this.Page, frma2_state, frma2_btn_submit);

        if (!Page.IsPostBack)
        {
            Credentialpage.Utility.Getcountry(frma2_opt_country);
            Credentialpage.Utility.Getyear(frma2_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
            Credentialpage.Utility.Getyear(frma2_start_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
            Credentialpage.Utility.Getyear(frma2_end_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
            Credentialpage.Utility.Getmonth(frma2_month);
            Credentialpage.Utility.Getdate(frma2_date);        

            //populate from db
           // Session["id"] = Convert.ToInt32(Request.QueryString["id"].ToString());
           // Session["Customer_id"] = Convert.ToInt32(Request.QueryString["cid"].ToString());
            Credentialpage.Utility.GetUniversitydata(frma2_opt_country, frma2_option_degree, frma2_city, frma2_state, frma2_start_year, frma2_end_year, frma2_option_graduate, frma2_month, frma2_date, frma2_year, frma2_institution, frma2_option_major, Convert.ToInt32(Request.QueryString["id"].ToString()));
            //action based on country
            selected_country("old");
            //action based on graduate
            selected_graduate();

            //wait process
            //frma2_opt_country.Attributes.Add("onchange", "ShowProcessMessage('ProcessingWindow')");
            //frma2_btn_clear.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");
            //frma2_btn_submit.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");
            //frma2_option_degree.Attributes.Add("onchange", "ShowProcessMessage('ProcessingWindow')");
            //frma2_option_graduate.Attributes.Add("onchange", "ShowProcessMessage('ProcessingWindow')");
            //frma2_option_major.Attributes.Add("onchange", "ShowProcessMessage('ProcessingWindow')");


          
        }

    }
    protected void frma2_opt_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        selected_country("New"); 
    }   
    protected void frma2_btn_clear_Click(object sender, EventArgs e)
    {
        clear_university("clearbtn");
    }
    protected void frma2_btn_submit_Click(object sender, EventArgs e)
    {
        //check for new entry
        int institution_id = 0;
        int degree_id = 0;
        int major_id = 0;

        Page.Validate("frma2_group");
        if (Page.IsValid)
        {
            institution_id = Credentialpage.Utility.AddNew_institution(frma2_institution.Text, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), "University", Request.QueryString["cid"].ToString());
        }
        if (frma2_option_graduate.SelectedItem.ToString() == "Yes")
        {
            Page.Validate("frma2_group4");
        }

        if (frma2_degree.Visible == true)
        {
            Page.Validate("frma2_group2");
            if (Page.IsValid)
            {
                degree_id = Credentialpage.Utility.AddNew_degree(frma2_degree.Text, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), "University", Request.QueryString["cid"].ToString());
            }
        }
        else
        {
            degree_id = Convert.ToInt32(frma2_option_degree.SelectedValue.ToString());
        }
        if (frma2_major.Visible == true)
        {
            Page.Validate("frma2_group3");
            if (Page.IsValid)
            {
                major_id = Credentialpage.Utility.AddNew_major(frma2_major.Text, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), Request.QueryString["cid"].ToString());
            }
        }
        else
        {
            major_id = Convert.ToInt32(frma2_option_major.SelectedValue.ToString());
        }
        if (Page.IsValid)
        {
            string DateDegreeAwarded;
            int graduated;
            if (frma2_option_graduate.SelectedValue.ToString() == "True")
            {
                DateDegreeAwarded = frma2_date.SelectedValue.ToString() + "/" + frma2_month.SelectedValue.ToString() + "/" + frma2_year.SelectedValue.ToString();
            }
            else
            {
                DateDegreeAwarded = "Null";
            }
            if (frma2_option_graduate.SelectedValue.ToString() == "True")
            {
                graduated = 1;
            }
            else
            {
                graduated = 0;
            }
            bool result = Credentialpage.Utility.update_education(Convert.ToInt32(Request.QueryString["rid"].ToString()), Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), major_id, institution_id, frma2_start_year.SelectedItem.ToString(), frma2_end_year.SelectedItem.ToString(), degree_id, graduated, DateDegreeAwarded, 1, frma2_city.Text, frma2_state.Text, Convert.ToInt32(Request.QueryString["id"].ToString()));

            if (result == true)
            {               
                //2
                frma2_option_degree.Visible = true;
                frma2_RequiredFieldValidator6.Visible = true;
                frma2_degree.Visible = false;

                //3
                frma2_option_major.Visible = true;
                frma2_RequiredFieldValidator7.Visible = true;
                frma2_major.Visible = false;
                clear_university("submitbtn");

                Response.Redirect("~/Request_complete.aspx");

            }
        }
    }
    protected void frma2_option_graduate_SelectedIndexChanged(object sender, EventArgs e)
    {
        selected_graduate(); 
    }
    protected void frma2_option_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frma2_option_degree.SelectedItem.ToString() == "Add New")
        {
            Credentialpage.Utility.SetFocus(frma2_degree);
            frma2_option_degree.Visible = false;
            frma2_RequiredFieldValidator6.Visible = false;
            frma2_degree.Visible = true;
        }
    }
    protected void frma2_option_major_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frma2_option_major.SelectedItem.ToString() == "Add New")
        {
            Credentialpage.Utility.SetFocus(frma2_major);
            frma2_option_major.Visible = false;
            frma2_RequiredFieldValidator7.Visible = false;
            frma2_major.Visible = true;
        }
    }

    //common functions
    public void clear_university(string clear_mode)
    {
        // clear fields       
        frma2_city.Text = "";
        frma2_date.SelectedIndex = 0;
        frma2_end_year.SelectedIndex = 0;
        frma2_month.SelectedIndex = 0;
        frma2_start_year.SelectedIndex = 0;
        frma2_year.SelectedIndex = 0;
        frma2_option_graduate.SelectedIndex = 0;
        frma2_optional.Visible = false;
        frma2_state.Text = "";
        frma2_institution.Text = "";
        frma2_degree.Text = "";
        frma2_major.Text = ""; 

        //clearing old institution and degree
       // frma1_option_institution.Items.Clear();
        frma2_option_degree.Items.Clear();
        frma2_option_major.Items.Clear(); 

        //hide fields
        //1
       // frma1_option_institution.Visible = true;
        //frma1_RequiredFieldValidator3.Visible = true;
        //frma1_institution.Visible = false;
        //2
        frma2_option_degree.Visible = true;
        frma2_RequiredFieldValidator6.Visible = true;
        frma2_degree.Visible = false;

        //3
        frma2_option_major.Visible = true;
        frma2_RequiredFieldValidator7.Visible = true;
        frma2_major.Visible = false;        



        switch (clear_mode)
        {
            case "clearbtn":
                               
                Credentialpage.Utility.Getdegree(frma2_option_degree, 1, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), Request.QueryString["cid"].ToString());
                Credentialpage.Utility.Add_New(frma2_option_degree);
                Credentialpage.Utility.Getmajor(frma2_option_major, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), Request.QueryString["cid"].ToString());
                Credentialpage.Utility.Add_New(frma2_option_major);               
                break;
            case "submitbtn":
                frma2_opt_country.SelectedIndex = 0;
                break;
        }

    }
    public void selected_country(string option)
    {
        if (option == "New")
        {
            clear_university("countrybtn");  
        }

        if (frma2_opt_country.SelectedIndex != 0)
        {
            frma2_AutoCompleteExtender1.ContextKey = frma2_opt_country.SelectedValue.ToString() + "|" + Request.QueryString["cid"].ToString();
            Credentialpage.Utility.Getdegree_update(frma2_option_degree, 1, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), Request.QueryString["cid"].ToString());
            Credentialpage.Utility.Add_New(frma2_option_degree);
            Credentialpage.Utility.Getmajor_update(frma2_option_major, Convert.ToInt32(frma2_opt_country.SelectedValue.ToString()), Request.QueryString["cid"].ToString());
            Credentialpage.Utility.Add_New(frma2_option_major);           
        }
        else
        {
            clear_university("countrybtn");  
        }
    }
    public void selected_graduate()
    {
        if (frma2_option_graduate.SelectedValue.ToString() == "True")
        {
            frma2_optional.Visible = true;
        }
        else
        {
            frma2_optional.Visible = false;
        }
    }

    //script
    public void SetDefaultButton(Page page, TextBox textControl, Button defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    } 
}
