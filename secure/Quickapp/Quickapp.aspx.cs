using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class secure_Quickapp_Quickapp : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        Isvalid(); 
        if (!Page.IsPostBack)
        {
             
            populatedata();
            setview(Personalinfo, false);

            //setview(Postsecondary, false); 

            ViewState["InfoData"] = "";
            ViewState["UppereduData"] = "";
            ViewState["PosteduData"] = "";
        }
        else
        {
            dots(MultiView1.GetActiveView().ID.ToString());
        }
    }

    protected void populatedata()
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        //client
        QuickappService.GetSubclients(dpsubclients, Convert.ToInt32(Session["Admin_Customer"].ToString()), false);  
        //purpose
        QuickappService.GetPurposeList(dppurpose);  
        //info
        QuickappService.Getmonth(drp_month);
        QuickappService.Getdate(drp_date);
        QuickappService.Getyear(drp_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear),false);
        QuickappService.Getcountry(drpcountry);
        QuickappService.Getcountry(drpcountrybirth);
        //upper
        QuickappService.Getcountry(drp_upper_country);
        QuickappService.Getyear(drp_upper_startdate, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear),false);
      
        //post
        QuickappService.Getcountry(drp_post_country);
        QuickappService.Getyear(drp_post_startdate, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear), false);
       
        DataTable dt = new DataTable();

        //grid
        gvbind(GridView_UpperEdu, dt, "UpperEdu-header", true);
        gvbind(GridView_PostEdu, dt, "PostEdu-header", true);        
    }


    
    #region tabs

    #region set view & dots

    protected void setview(View name, bool Viewcheck)
    {
        View currentview;
        if (Viewcheck)
        {
            currentview = MultiView1.GetActiveView();
        }
        else { currentview = name; }
        MultiView1.SetActiveView(currentview);
        dots(currentview.ID.ToString());
    }
    protected void dots(string view)
    {
        int pagecount = 5;
        switch (view)
        {
            case "Personalinfo":
                dots1.getCurrent(1, pagecount);
                trialctrl.getCurrent(1);
                break;
            case "Uppersecondary":
                dots2.getCurrent(2, pagecount);
                trialctrl.getCurrent(2);
                break;
            case "Postsecondary":
                dots3.getCurrent(3, pagecount);
                trialctrl.getCurrent(3);
                break;
            case "Review":
                dots4.getCurrent(4, pagecount);
                trialctrl.getCurrent(4);
                break;

        }
    }

    #endregion

    #region info
    protected void opt_name_select_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (opt_name_select.SelectedValue == "Yes")
        {
            opt_firstname.Visible = true;
            opt_middlename.Visible = true;
            opt_lastname.Visible = true;
        }
        else
        {
            opt_firstname.Visible = false;
            opt_middlename.Visible = false;
            opt_lastname.Visible = false;
        }
    }
    private void Info_BindGrid()
    {
        string birth = drp_year.SelectedValue.ToString() + "/" + drp_month.SelectedValue.ToString() + "/" + drp_date.SelectedValue.ToString();
        if (birth == " / / ")
        {
            birth = "";
        }
        DataTable dt = new DataTable();

        dt.Columns.Add("FirstName");
        dt.Columns.Add("MiddleName");
        dt.Columns.Add("LastName");
        dt.Columns.Add("Gender");
        dt.Columns.Add("otherFirstName");
        dt.Columns.Add("otherMiddleName");
        dt.Columns.Add("otherLastName");
        dt.Columns.Add("DateOfBirth");
        dt.Columns.Add("Addressline1");
        dt.Columns.Add("Addressline2");
        dt.Columns.Add("City");
        dt.Columns.Add("Countryofbirth");
        dt.Columns.Add("Country");
        dt.Columns.Add("State_or_province");
        dt.Columns.Add("Zip_or_PostalCode");
        dt.Columns.Add("HomePhone");
        dt.Columns.Add("WorkPhone");
        dt.Columns.Add("MobilePhone");
        dt.Columns.Add("Email");
        dt.Columns.Add("Purpose");

        DataRow dr = dt.NewRow();

        dr["FirstName"] = txtfirstname.Text;
        dr["MiddleName"] = txtmiddlename.Text;
        dr["LastName"] = txtlastname.Text;
        dr["Gender"] = rbgender.SelectedItem.ToString();
        dr["otherFirstName"] = txtofirstname.Text;
        dr["otherMiddleName"] = txtomiddlename.Text;
        dr["otherLastName"] = txtolastname.Text;
        dr["DateOfBirth"] = birth;
        dr["Addressline1"] = txtaddress1.Text;
        dr["Addressline2"] = txtaddress2.Text;
        dr["City"] = txtcity.Text;
        dr["Countryofbirth"] = drpcountrybirth.SelectedItem.ToString();
        dr["Country"] = drpcountry.SelectedItem.ToString();
        dr["State_or_province"] = txtstate.Text;
        dr["Zip_or_PostalCode"] = txtzipcode.Text;
        dr["HomePhone"] = txtprimaryphone.Text;
        dr["WorkPhone"] = txtsecondaryphone.Text;
        dr["MobilePhone"] = txtmobile.Text;
        dr["Email"] = txtemail.Text;
        dr["Purpose"] = dppurpose.SelectedItem.ToString();
        dt.Rows.Add(dr);
        ViewState["InfoData"] = dt;
    }
    #endregion

    #region upper
    protected void drp_upper_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear(true,false);
        if (drp_upper_country.SelectedIndex != 0)
        {
            QuickappService.Getdegree(drp_upper_degree, 0, Convert.ToInt32(drp_upper_country.SelectedValue.ToString()), HttpContext.Current.Session["Admin_Customer"].ToString());
            QuickappService.Add_New(drp_upper_degree);
        }
    }
    protected void drp_upper_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_upper_degree.SelectedItem.ToString() == "Add New")
        {
            drp_upper_degree.Visible = false;           
            txtupper_degree.Visible = true;
        }

    }
    protected void drp_upper_startdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        QuickappService.Getyear(drp_upper_enddate, Convert.ToInt32(drp_upper_startdate.SelectedValue.ToString()), Convert.ToInt32(app.Endyear), false);       

    }
    protected void drp_upper_enddate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        QuickappService.Getyear(drp_upper_compdate, Convert.ToInt32(drp_upper_enddate.SelectedValue.ToString()), Convert.ToInt32(app.Endyear), true);
       
    }
    protected void rbuppergraduation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbuppergraduation.SelectedValue == "True")
        {
            opt_upper_year.Visible = true;
        }
        else
        {
            opt_upper_year.Visible = false;
        }
    }
    #endregion

    #region post
    protected void drp_post_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear(false, false);
        if (drp_post_country.SelectedIndex != 0)
        {
            QuickappService.Getdegree(drp_post_degree, 1, Convert.ToInt32(drp_post_country.SelectedValue.ToString()), HttpContext.Current.Session["Admin_Customer"].ToString());
            QuickappService.Add_New(drp_post_degree);
            QuickappService.Getmajor(drp_post_major, Convert.ToInt32(drp_post_country.SelectedValue.ToString()), HttpContext.Current.Session["Admin_Customer"].ToString());
            QuickappService.Add_New(drp_post_major);
        }
    }
    protected void drp_post_major_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_post_major.SelectedItem.ToString() == "Add New")
        {
            drp_post_major.Visible = false;
            txtpost_major.Visible = true;
        }
    }
    protected void rbpostgraduation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbpostgraduation.SelectedValue == "True")
        {
            opt_post_year.Visible = true;
        }
        else
        {
            opt_post_year.Visible = false;
        }
    }
    protected void drp_post_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_post_degree.SelectedItem.ToString() == "Add New")
        {
            drp_post_degree.Visible = false;
            txtpost_degree.Visible = true;
        }

    }
    protected void drp_post_startdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        QuickappService.Getyear(drp_post_enddate, Convert.ToInt32(drp_post_startdate.SelectedValue.ToString()), Convert.ToInt32(app.Endyear), false);

    }
    protected void drp_post_enddate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        QuickappService.Getyear(drp_post_compdate, Convert.ToInt32(drp_post_enddate.SelectedValue.ToString()), Convert.ToInt32(app.Endyear), true);

    }
    #endregion

    #region Edu Submit
    protected void Education(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "upper_btnsubmit":
                Page.Validate("frm2_group1");
                if (drp_upper_country.SelectedIndex != 0)
                {
                    if (drp_upper_degree.SelectedItem.ToString() == "Add New")
                    {
                        Page.Validate("frm2_group2");
                    }
                }
                if (rbuppergraduation.SelectedItem.ToString() == "Yes")
                {
                    Page.Validate("frm2_group3");
                }

                if (Page.IsValid)
                {
                    if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
                    {
                        DataTable dt = (DataTable)ViewState["UppereduData"];
                        int count = dt.Rows.Count;
                        Upperedu_BindGrid(count, true);
                    }
                    else
                    {
                        Upperedu_BindGrid(1, false);
                    }
                    clear(true, true);
                }               
                break;
            case "post_btnsubmit":
                 Page.Validate("frm3_group1");
                if (drp_post_country.SelectedIndex != 0)
                {
                    if (drp_post_degree.SelectedItem.ToString() == "Add New")
                    {
                        Page.Validate("frm3_group2");
                    }
                    if (drp_post_major.SelectedItem.ToString() == "Add New")
                    {
                        Page.Validate("frm3_group3");
                    }


                }
                if (rbpostgraduation.SelectedItem.ToString() == "Yes")
                {
                    Page.Validate("frm3_group4");
                }

                if (Page.IsValid)
                {
                    if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
                    {
                        DataTable dt = (DataTable)ViewState["PosteduData"];
                        int count = dt.Rows.Count;
                        Postedu_BindGrid(count, true);
                    }
                    else
                    {
                        Postedu_BindGrid(1, false);
                    }
                    clear(false, true);
                }
                break;
        }
    }
    private void gvbind(GridView gv, DataTable dt, string action, bool Ishide)
    {
        switch (action)
        {
            case "bind":
                gv.DataSource = dt;
                gv.DataBind();
                break;
            case "nodata":
                dt.Rows.Add(dt.NewRow());
                gv.DataSource = dt;
                gv.DataBind();
                break;
            case "UpperEdu-header":
                dt = new DataTable();
                dt.Columns.Add("Rid");
                dt.Columns.Add("Country");
                dt.Columns.Add("Institution");
                dt.Columns.Add("Degree");
                dt.Columns.Add("StartDate");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("City");
                dt.Columns.Add("State");
                dt.Columns.Add("Graduated");
                dt.Columns.Add("Graduation");
                dt.Rows.Add(dt.NewRow());
                gv.DataSource = dt;
                gv.DataBind();
                break;

            case "PostEdu-header":
                dt = new DataTable();
                dt.Columns.Add("Rid");
                dt.Columns.Add("Country");
                dt.Columns.Add("Institution");
                dt.Columns.Add("Degree");
                dt.Columns.Add("major");
                dt.Columns.Add("StartDate");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("City");
                dt.Columns.Add("State");
                dt.Columns.Add("Graduated");
                dt.Columns.Add("Graduation");
                dt.Rows.Add(dt.NewRow());
                gv.DataSource = dt;
                gv.DataBind();
                break;


        }

        int count = gv.Columns.Count - 1;
        if (Ishide)
        {
            gv.Columns[count].Visible = false;
        }
        else { gv.Columns[count].Visible = true; }


    }
    protected void clear(bool Isclear,bool type)
    {
        if (Isclear)
        {           
            txtupper_institution.Text = "";
            txtupper_degree.Text = "";
            txtuppercity.Text = "";
            txtupperstate.Text = "";

            drp_upper_startdate.SelectedIndex = 0;
            drp_upper_enddate.Items.Clear();             
            rbuppergraduation.SelectedIndex = 1;
            opt_upper_year.Visible = false;
            drp_upper_compdate.Items.Clear();

            drp_upper_degree.Items.Clear();  
            drp_upper_degree.Visible = true;
            txtupper_degree.Visible = false;

        }
        else
        {           
            txtpost_institution.Text = "";
            txtpost_degree.Text = "";
            txtpost_major.Text = "";
            txtpostcity.Text = "";
            txtpoststate.Text = "";

            drp_post_startdate.SelectedIndex = 0;
            drp_post_enddate.Items.Clear();
          
            rbpostgraduation.SelectedIndex = 1;
            opt_post_year.Visible = false;
            drp_post_compdate.Items.Clear();

            drp_post_degree.Items.Clear();           
            drp_post_degree.Visible = true;
            txtpost_degree.Visible = false;

            drp_post_major.Items.Clear();
            drp_post_major.Visible = true;
            txtpost_major.Visible = false;
        }

        if (type)
        {
            drp_upper_country.SelectedIndex = 0;
            drp_post_country.SelectedIndex = 0;
        }
    }
    #region Upper
    private void Upperedu_BindGrid(int rowcount, bool Isinclude)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        DataColumn Rid = new DataColumn("Rid");
        Rid.AllowDBNull = false;
        Rid.DataType = typeof(string);
        Rid.MaxLength = 50;
        Rid.Unique = true;
        dt.Columns.Add(Rid);
        dt.Columns.Add("Country");
        dt.Columns.Add("Institution");
        dt.Columns.Add("Degree");
        dt.Columns.Add("StartDate");
        dt.Columns.Add("EndDate");
        dt.Columns.Add("City");
        dt.Columns.Add("State");
        dt.Columns.Add("Graduated");
        dt.Columns.Add("Graduation");
        dt.PrimaryKey = new DataColumn[] { Rid };

        if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
        {
            DataTable dtold = (DataTable)ViewState["UppereduData"];
            for (int i = 0; i < rowcount; i++)
            {
                if (dtold.Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["Rid"] = i + 1;
                    dr["Country"] = dtold.Rows[i]["Country"].ToString();
                    dr["Institution"] = dtold.Rows[i]["Institution"].ToString();
                    dr["Degree"] = dtold.Rows[i]["Degree"].ToString();
                    dr["StartDate"] = dtold.Rows[i]["StartDate"].ToString();
                    dr["EndDate"] = dtold.Rows[i]["EndDate"].ToString();
                    dr["City"] = dtold.Rows[i]["City"].ToString();
                    dr["State"] = dtold.Rows[i]["State"].ToString();
                    dr["Graduated"] = dtold.Rows[i]["Graduated"].ToString();
                    dr["Graduation"] = dtold.Rows[i]["Graduation"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            if (Isinclude)
            {
                dr = dt.NewRow();
                dr["Rid"] = rowcount + 1;
                dr["Country"] = drp_upper_country.SelectedItem.ToString();
                dr["Institution"] = txtupper_institution.Text;
                if (drp_upper_degree.SelectedItem.ToString() == "Add New")
                {
                    dr["Degree"] = txtupper_degree.Text;
                }
                else { dr["Degree"] = drp_upper_degree.SelectedItem.ToString(); }
                dr["StartDate"] = drp_upper_startdate.SelectedItem.ToString();
                dr["EndDate"] = drp_upper_enddate.SelectedItem.ToString();
                dr["City"] = txtuppercity.Text;
                dr["State"] = txtupperstate.Text;
                dr["Graduated"] = rbuppergraduation.SelectedValue.ToString();
                dr["Graduation"] = drp_upper_compdate.SelectedItem.ToString();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            dr = dt.NewRow();
            dr["Rid"] = rowcount;
            dr["Country"] = drp_upper_country.SelectedItem.ToString();
            dr["Institution"] = txtupper_institution.Text;
            if (drp_upper_degree.SelectedItem.ToString() == "Add New")
            {
                dr["Degree"] = txtupper_degree.Text;
            }
            else { dr["Degree"] = drp_upper_degree.SelectedItem.ToString(); }
            dr["StartDate"] = drp_upper_startdate.SelectedItem.ToString();
            dr["EndDate"] = drp_upper_enddate.SelectedItem.ToString();
            dr["City"] = txtuppercity.Text;
            dr["State"] = txtupperstate.Text;
            dr["Graduated"] = rbuppergraduation.SelectedValue.ToString();
            dr["Graduation"] = drp_upper_compdate.SelectedItem.ToString();
            dt.Rows.Add(dr);

        }

        ViewState["UppereduData"] = dt;

        if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
        {
            dt = (DataTable)ViewState["UppereduData"];
            if (dt.Rows.Count > 0)
            {
                gvbind(GridView_UpperEdu, dt, "bind", false);
            }
            else
            {
                gvbind(GridView_UpperEdu, dt, "nodata", true);
            }
        }



    }
    protected void GridView_UpperEdu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
        {
            DataTable dt = (DataTable)ViewState["UppereduData"];
            if (e.CommandName == "DeleteRec")
            {
                string Id = e.CommandArgument.ToString();
                dt.Rows.Find(System.Convert.ToInt64(Id)).Delete();
                dt.AcceptChanges();
                ViewState["UppereduData"] = dt;
                int count = dt.Rows.Count;
                if (count != 0)
                {
                    Upperedu_BindGrid(count, false);
                }
                else { gvbind(GridView_UpperEdu, dt, "UpperEdu-header", true); }
            }
        }
    }
    #endregion

    #region Post
    private void Postedu_BindGrid(int rowcount, bool Isinclude)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        DataColumn Rid = new DataColumn("Rid");
        Rid.AllowDBNull = false;
        Rid.DataType = typeof(string);
        Rid.MaxLength = 50;
        Rid.Unique = true;
        dt.Columns.Add(Rid);
        dt.Columns.Add("Country");
        dt.Columns.Add("Institution");
        dt.Columns.Add("Degree");
        dt.Columns.Add("Major");
        dt.Columns.Add("StartDate");
        dt.Columns.Add("EndDate");
        dt.Columns.Add("City");
        dt.Columns.Add("State");
        dt.Columns.Add("Graduated");
        dt.Columns.Add("Graduation");
        dt.PrimaryKey = new DataColumn[] { Rid };

        if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
        {
            DataTable dtold = (DataTable)ViewState["PosteduData"];
            for (int i = 0; i < rowcount; i++)
            {
                if (dtold.Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["Rid"] = i + 1;
                    dr["Country"] = dtold.Rows[i]["Country"].ToString();
                    dr["Institution"] = dtold.Rows[i]["Institution"].ToString();
                    dr["Degree"] = dtold.Rows[i]["Degree"].ToString();
                    dr["Major"] = dtold.Rows[i]["Major"].ToString();
                    dr["StartDate"] = dtold.Rows[i]["StartDate"].ToString();
                    dr["EndDate"] = dtold.Rows[i]["EndDate"].ToString();
                    dr["City"] = dtold.Rows[i]["City"].ToString();
                    dr["State"] = dtold.Rows[i]["State"].ToString();
                    dr["Graduated"] = dtold.Rows[i]["Graduated"].ToString();
                    dr["Graduation"] = dtold.Rows[i]["Graduation"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            if (Isinclude)
            {
                dr = dt.NewRow();
                dr["Rid"] = rowcount + 1;
                dr["Country"] = drp_post_country.SelectedItem.ToString();
                dr["Institution"] = txtpost_institution.Text;
                if (drp_post_degree.SelectedItem.ToString() == "Add New")
                {
                    dr["Degree"] = txtpost_degree.Text;
                }
                else { dr["Degree"] = drp_post_degree.SelectedItem.ToString(); }
                if (drp_post_major.SelectedItem.ToString() == "Add New")
                {
                    dr["Major"] = txtpost_major.Text;
                }else
                {
                    if (drp_post_major.SelectedItem.ToString() == "Select")
                    {
                        dr["Major"] = "";
                    }
                    else { dr["Major"] = drp_post_major.SelectedItem.ToString(); }
                }  
             
                dr["StartDate"] = drp_post_startdate.SelectedItem.ToString();
                dr["EndDate"] = drp_post_enddate.SelectedItem.ToString();
                dr["City"] = txtpostcity.Text;
                dr["State"] = txtpoststate.Text;
                dr["Graduated"] = rbpostgraduation.SelectedValue.ToString();
                dr["Graduation"] = drp_post_compdate.SelectedItem.ToString();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            dr = dt.NewRow();
            dr["Rid"] = rowcount;
            dr["Country"] = drp_post_country.SelectedItem.ToString();
            dr["Institution"] = txtpost_institution.Text;
            if (drp_post_degree.SelectedItem.ToString() == "Add New")
            {
                dr["Degree"] = txtpost_degree.Text;
            }
            else { dr["Degree"] = drp_post_degree.SelectedItem.ToString(); }
            if (drp_post_major.SelectedItem.ToString() == "Add New")
            {
                dr["Major"] = txtpost_major.Text;
            }
            else {
                if (drp_post_major.SelectedItem.ToString() == "Select")
                {
                    dr["Major"] = "";
                }
                else { dr["Major"] = drp_post_major.SelectedItem.ToString(); }
            
            }       
            dr["StartDate"] = drp_post_startdate.SelectedItem.ToString();
            dr["EndDate"] = drp_post_enddate.SelectedItem.ToString();
            dr["City"] = txtpostcity.Text;
            dr["State"] = txtpoststate.Text;
            dr["Graduated"] = rbpostgraduation.SelectedValue.ToString();
            dr["Graduation"] = drp_post_compdate.SelectedItem.ToString();
            dt.Rows.Add(dr);

        }


        ViewState["PosteduData"] = dt;

        if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
        {
            dt = (DataTable)ViewState["PosteduData"];
            if (dt.Rows.Count > 0)
            {
                gvbind(GridView_PostEdu, dt, "bind", false);
            }
            else { gvbind(GridView_PostEdu, dt, "nodata", true); }
        }
    }
    protected void GridView_PostEdu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
        {
            DataTable dt = (DataTable)ViewState["PosteduData"];
            if (e.CommandName == "DeleteRec")
            {
                string Id = e.CommandArgument.ToString();
                dt.Rows.Find(System.Convert.ToInt64(Id)).Delete();
                dt.AcceptChanges();
                ViewState["PosteduData"] = dt;
                int count = dt.Rows.Count;
                if (count != 0)
                {
                    Postedu_BindGrid(count, false);
                }
                else { gvbind(GridView_PostEdu, dt, "PostEdu-header", true); }
            }
        }
    }

    #endregion

    #endregion

    #region reviewTab
    protected void Reviewtab()
    {
        DataTable dt = new DataTable();
        if ((ViewState["InfoData"] != null) & (!ViewState["InfoData"].Equals("")))
        {
            dvpersonalinfo.DataSource = (DataTable)ViewState["InfoData"]; dvpersonalinfo.DataBind();
            dvAddressinfo.DataSource = (DataTable)ViewState["InfoData"]; dvAddressinfo.DataBind();
            dvContactinfo.DataSource = (DataTable)ViewState["InfoData"]; dvContactinfo.DataBind();
        }
        if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
        {
            dt = (DataTable)ViewState["UppereduData"];
            if (dt.Rows.Count > 0)
            {
                gvbind(gvupperedu, dt, "bind", true);
            }
            else
            {
                gvbind(gvupperedu, dt, "nodata", true);
            }
        }
        else
        { gvbind(gvupperedu, dt, "UpperEdu-header", true); }
        if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
        {
            dt = (DataTable)ViewState["PosteduData"];
            if (dt.Rows.Count > 0)
            {
                gvbind(gvpostedu, dt, "bind", true);
            }
            else { gvbind(gvpostedu, dt, "nodata", true); }
        }
        else { gvbind(gvpostedu, dt, "PostEdu-header", true); }

    }
    protected void newapp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/secure/Quickapp/Quickapp.aspx");
    }

    #endregion

    #region Next & Previous Category

    protected void next_Click(object sender, EventArgs e)
    {
        Isvalid();
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "Stage1next":
                Info_BindGrid();
                setview(Uppersecondary, false);
                break;
            case "Stage2next":
               // if (GridView_UpperEdu.Columns[GridView_UpperEdu.Columns.Count - 1].Visible == true)
               // {
                    setview(Postsecondary, false);
               // }
               // else
               // {
               //     string jv = "alert('Please Add an Education History');";
               //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", jv, true);

               // }
                break;
            case "Stage3next":
                Reviewtab();
                setview(Review, false);
                break;
            case "Stage4next":
                SubmitApp();
                setview(Track, false);
                break;
        }


    }
    protected void previous_Click(object sender, EventArgs e)
    {
        Isvalid();
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "Stage2previous":
                setview(Personalinfo, false);
                break;
            case "Stage3previous":
                setview(Uppersecondary, false);
                break;
            case "Stage4previous":
                setview(Postsecondary, false);
                break;
        }


    }
    protected void SubmitApp()
    {
        DataTable dtinfo = new DataTable();
        DataTable dtupper = new DataTable();
        DataTable dtpost = new DataTable();
        if ((ViewState["InfoData"] != null) & (!ViewState["InfoData"].Equals("")))
        {
            dtinfo = (DataTable)ViewState["InfoData"];
        }
        if ((ViewState["UppereduData"] != null) & (!ViewState["UppereduData"].Equals("")))
        {
            dtupper = (DataTable)ViewState["UppereduData"];

        }

        if ((ViewState["PosteduData"] != null) & (!ViewState["PosteduData"].Equals("")))
        {
            dtpost = (DataTable)ViewState["PosteduData"];

        }

        string result = QuickappService.SubmitApplication(dtinfo, dtupper, dtpost, dpsubclients.SelectedValue.ToString());

        int ncount = result.IndexOf("|");
        if (ncount > 0)
        {
            string[] Spliter = result.Split('|');
            string fileno = Spliter[0].ToString();
            string error = Spliter[1].ToString();
            string Isresult = Spliter[2].ToString();

            txtfileno.InnerText = fileno;
            txtstatus.InnerText = Isresult;
            txterror.InnerText = error;
        }
    }
    #endregion
    #endregion



   
 

    #region Web Method Category


    [WebMethod]

    public static List<string> GetInstitutionData(string prefix, string Country, string type)
    {
        List<string> result = new List<string>();
        if (Country != "0")
        {
            result = QuickappService.GetInstitutionList(prefix, type, Country, HttpContext.Current.Session["Admin_Customer"].ToString());           
        }
        return result;
       
    }

    [WebMethod]

    public static List<string> GetdegreeData(string prefix, string Country,string type)
    {
        List<string> result = new List<string>();
        if (Country != "0")
        {
           result = QuickappService.GetdegreeList(prefix,type,Country,HttpContext.Current.Session["Admin_Customer"].ToString());
        }
        return result;

    }

    [WebMethod]

    public static List<string> GetmajorData(string prefix, string Country)
    {
        List<string> result = new List<string>();
        if (Country != "0")
        {
            result = QuickappService.GetmajorList(prefix, Country, HttpContext.Current.Session["Admin_Customer"].ToString());
        }
        return result;

    }


 
    #endregion

   

    protected void Isvalid()
    {
        if (Session["Authenticate"].ToString() == "Approved")
        {
            if (Session["Clientsettings"].ToString() != "Empty")
            {
                Authentication.Utility.AdminDomainAttributes dm = Authentication.Utility.AdminGetClient(Request.Url);
                Page.Title = dm.DmName;
                Headerctrl.setTitle(dm.DmName, dm.DmID);
            }
            else
            {

                ViewState["InfoData"] = "";
                ViewState["UppereduData"] = "";
                ViewState["PosteduData"] = "";

                Response.Redirect("~/Fail.aspx");
            }
        }
        else
        {
            ViewState["InfoData"] = "";
            ViewState["UppereduData"] = "";
            ViewState["PosteduData"] = "";
            Response.Redirect("~/Fail.aspx");
        }

    }







}
