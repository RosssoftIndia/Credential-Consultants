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



//page order 
public partial class mIndex : System.Web.UI.Page
{
    int Clientid = 0;
    #region Global variables     
    public int error_code = 0;
  

    Authentication.Utility.DomainAttributes dm = new Authentication.Utility.DomainAttributes();
    Authentication.Utility.SessionVariable sv = new Authentication.Utility.SessionVariable();                   
    RossSoft.Utility.AppConfig app = new RossSoft.Utility.AppConfig(); 

    #endregion
    string Subdomain = "";

  
    #region session

    protected void InjectSessionExpireScript()
    {       
        int sessiontime = Convert.ToInt32(app.SessionTime);
        string script = "<script> \n" +
        "function expireSession(){ \n" +
        " window.location = '" + "Timeout.aspx" + "'}\n" +
        "setTimeout('expireSession()', " + sessiontime * 60 * 1000 + " ); \n" +
        "</script>";
        this.Page.RegisterClientScriptBlock("expirescript", script);
    }

    # endregion


    #region requirements
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplicationSettings();
        this.MaintainScrollPositionOnPostBack = true;
        if ((Session["Clientsettings"].ToString() != "Empty") || (Session["Appsettings"].ToString() != "Empty"))
        {

            if (Session["Session_name"] == null)
            {
                Response.Redirect("~/Timeout.aspx");
            }
            else
            {
                InjectSessionExpireScript();
                if (!Page.IsPostBack)
                {
                    control_initialize();
                    FormViewcontrol.ActiveViewIndex = Convert.ToInt32(app.startpage);
                    Menu1.PublicMethodInUsercontrol(1);
                }

            }


        }
        else { Response.Redirect("~/Timeout.aspx"); }

        //sv.Applicant_id = 4;
        //sv.Request_id = 4;

    }
    #endregion

    public void control_initialize()
    {


        #region Settings
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }

        #region Service

          if (!Page.IsPostBack)
        {
            //Evaluation Service
            Credentialpage.Utility.Grid_servicegrid(servicegrid, Clientid);
            //Additional Service
            Credentialpage.Utility.Grid_addservicegrid(addservicegrid, Clientid);
        }

        #endregion

        #endregion

        #region PersonalInformation

        Credentialpage.Utility.Getmonth(frm1_option_month);
        Credentialpage.Utility.Getdate(frm1_option_date);
        Credentialpage.Utility.Getyear(frm1_option_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getcountry(frm1_option_country);
        Credentialpage.Utility.Getcountry(frm1_Country_birth);
        frm1_summary.HeaderText = "";

        SetDefaultButton(this.Page, frm1_address1, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_address2, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_cell_phone, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_city, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_confrprimary, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_Fname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_home_phone, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_Lname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_Mname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_optFname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_optLname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_optMname, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_primarymail, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_state, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_work_phone, frm1_Btn_continue);
        SetDefaultButton(this.Page, frm1_zip, frm1_Btn_continue);
       

        frm1_Btn_continue.Attributes.Add("onClick", "Loading(true);");
        toc.HRef = "OnlineTermsAndCondition.aspx?cus=" + Clientid.ToString();
        #endregion

        #region Service
        frm6_btn_continue.Attributes.Add("onClick", "Loading(true);");
        frm6_btn_previous.Attributes.Add("onClick", "Loading(true);");
        #endregion

        #region Review      
        frm8_Btn_continue.Attributes.Add("onClick", "Loading(true);");
        #endregion    

    }
    public void ApplicationSettings()
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

        if (ClientIsValid)
        {

            if (Session["Clientsettings"].ToString() != "Empty")
            { dm = (Authentication.Utility.DomainAttributes)Session["Clientsettings"]; }
            else { Response.Redirect("~/Timeout.aspx"); }
            if (Session["Appsettings"].ToString() != "Empty")
            { app = (RossSoft.Utility.AppConfig)Session["Appsettings"]; }
            else { Response.Redirect("~/Timeout.aspx"); }
            if (Session["SV"].ToString() != "Empty")
            { sv = (Authentication.Utility.SessionVariable)Session["SV"]; }
            else { Response.Redirect("~/Timeout.aspx"); }

            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Online Application for</static><br/>" + "<client>" + dm.SubDmName + "</client>";
                
            }
            else
            {
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Online Application</static>";
             
            }
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
          
        }


    }

    public void SetDefaultButton(Page page, TextBox textControl, Button defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    }
    public void SetDefaultButton(Page page, TextBox textControl, ImageButton defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    }

    #region Personnal Information Tab
    protected void frm1_optin_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Credentialpage.Utility.Getoptionalcell(frm1_optin_name, frm1_optional);
    }
  
    #region Continue Button
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (frm1_chk1.Checked)
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    protected void frm1_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        //process
        bool stage1 = false;
        Page.Validate("frm1_group");
        if (!Page.IsValid)
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Check Bottom of the page for errors')</script>");
        }
        else
        {
            int Clientid = 0;
            if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }

            if (Clientid != 0)
            {
                // create applicant
                string birth = frm1_option_year.SelectedValue.ToString() + "/" + frm1_option_month.SelectedValue.ToString() + "/" + frm1_option_date.SelectedValue.ToString();
                // string address = frm1_address1.Text + "  " + frm1_address2.Text;
                if (frm1_work_phone.Text == "") { frm1_work_phone.Text = "0"; } if (frm1_cell_phone.Text == "") { frm1_cell_phone.Text = "0"; }

                switch (sv.page1)
                {
                    case 0:
                        int applicant_id = Credentialpage.Utility.create_Applicante(frm1_Fname.Text, frm1_Mname.Text, frm1_Lname.Text, frm1_option_gender.SelectedItem.ToString(), birth, frm1_address1.Text, frm1_address2.Text, frm1_city.Text, Convert.ToInt32(frm1_option_country.SelectedValue.ToString()), frm1_state.Text, frm1_zip.Text.ToString(), frm1_home_phone.Text.ToString(), frm1_work_phone.Text.ToString(), frm1_cell_phone.Text.ToString(), frm1_primarymail.Text, Clientid, frm1_optFname.Text, frm1_optMname.Text, frm1_optLname.Text, "", Convert.ToInt32(frm1_Country_birth.SelectedValue.ToString()), 0);
                        if (applicant_id != 0)
                        {
                            sv.page1 = 1;
                            sv.Applicant_id = applicant_id;
                            stage1 = true;
                        }
                        else
                        {
                            error_code = 2;

                        }
                        break;
                    case 1:
                        bool result = Credentialpage.Utility.update_Applicante(frm1_Fname.Text, frm1_Mname.Text, frm1_Lname.Text, frm1_option_gender.SelectedItem.ToString(), birth, frm1_address1.Text, frm1_address2.Text, frm1_city.Text, Convert.ToInt32(frm1_option_country.SelectedValue.ToString()), frm1_state.Text, frm1_zip.Text.ToString(), frm1_home_phone.Text.ToString(), frm1_work_phone.Text.ToString(), frm1_cell_phone.Text.ToString(), frm1_primarymail.Text, Clientid, frm1_optFname.Text, frm1_optMname.Text, frm1_optLname.Text, "", Convert.ToInt32(frm1_Country_birth.SelectedValue.ToString()), 0, sv.Applicant_id);
                        if (result == true)
                        {
                            stage1 = true;

                        }
                        else
                        {
                            error_code = 3;

                        }
                        break;
                }
            }
            else
            {
                error_code = 1;
            }
            //bug
            switch (error_code)
            {
                case 1:
                    ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Process Failed! No customer Identified')</script>");
                    break;
                case 2:
                    ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Process Failed! failed to create applicant')</script>");
                    break;
                case 3:
                    ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Process Failed! failed to Update applicant')</script>");
                    break;
            }

            if (stage1)
            {
                switch (sv.page2)
                {
                    case 0:
                        int RequestID = 0;
                        string Eval_institution = "", Eval_organization = "", Eval_Attorney = "", Eval_Board = "", Eval_State = "", Eval_Military_Recruiter = "", Eval_other = "";
                        int purposeId = dm.Lock_PurposeId;
                        switch (dm.Lock_PurposeId.ToString())
                        {
                            case "1":
                               Eval_institution = dm.Lock_TargetName;
                                break;
                            case "7":
                                Eval_institution = dm.Lock_TargetName;
                                break;
                            case "2":
                                Eval_organization = dm.Lock_TargetName;
                                break;
                            case "3":
                                Eval_Attorney = dm.Lock_TargetName;
                                break;
                            case "4":
                                Eval_Board = dm.Lock_TargetName;
                                Eval_State = dm.Lock_State;
                                break;
                            case "5":
                                Eval_Military_Recruiter = dm.Lock_TargetName;
                                break;
                            case "6":
                                Eval_other = dm.Lock_TargetName;
                                break;
                            default :
                                purposeId = 7;
                                Eval_institution = ""; 
                                Eval_organization = "";
                                Eval_Attorney = "";
                                Eval_Board = "";
                                Eval_State = "";
                                Eval_Military_Recruiter = "";
                                Eval_other = "";
                                break;
                        }
                        RequestID = Credentialpage.Utility.create_purpose(sv.Applicant_id, purposeId,Eval_institution,Eval_organization,Eval_Attorney,Eval_Board,Eval_State,Eval_Military_Recruiter,Eval_other);
                        if (RequestID != 0)
                        {
                            sv.page2 = 1;
                            sv.Request_id = RequestID;
                        }
                        break;
                    case 1:
                        break;
                }
                Session["SV"] = sv;

                FormViewcontrol.ActiveViewIndex = 1;
                Menu1.PublicMethodInUsercontrol(2);

            }
        }

       

        //FormViewcontrol.ActiveViewIndex = 1;
        //Menu1.PublicMethodInUsercontrol(2);
    }
    #endregion
    #endregion
    #region Services & Fees Tab

    public void frm6_perform_check()
    {
        //check evaluation services
        int tagvalue = frm6_evaluation_check();
        if (tagvalue != 0)
        {
            //bool result = Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);

            //check aditional services
            frm6_Additional_check();
            refresh_Click(this, EventArgs.Empty);           
            sv.page3 = 1;
            Menu1.PublicMethodInUsercontrol(3);
            FormViewcontrol.ActiveViewIndex = 2;

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('You must select a Service to Continue')</script>");
        }
        Session["SV"] = sv;
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
                if (servicetag.Checked)
                {
                    tagvalue = Convert.ToInt32(servicetag.Value.ToString());
                    Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);

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
                            result = Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);
                        }
                    }
                    else
                    {
                        result = Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);
                    }
                    break;
                default:
                    break;
            }

        }
    }
    //service grid
    protected void servicegrid_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    Credentialpage.Utility.Grid_servicegrid(servicegrid, Convert.ToInt32( sv.Customer_id.ToString()));
        //}
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
    //additional service grid
    protected void addservicegrid_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    Credentialpage.Utility.Grid_addservicegrid(addservicegrid, Convert.ToInt32( sv.Customer_id.ToString()));
        //}
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

    //Previous & Continue Button
    protected void frm6_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        FormViewcontrol.ActiveViewIndex = 0;
        Menu1.PublicMethodInUsercontrol(1);
    }
    protected void frm6_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        switch (sv.page3)
        {
            case 0:
                frm6_perform_check();
                break;
            case 1:
                Credentialpage.Utility.clear_evaluation_services(sv.Request_id);
                frm6_perform_check();
                break;
        }

        //FormViewcontrol.ActiveViewIndex = 2;
        //Menu1.PublicMethodInUsercontrol(3);
    }
    #endregion

    #region Review Tab
    //Applicant
    protected void Applicant_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_Applicant(Applicant, sv.Applicant_id);
    }
    protected void Applicant_DataBound(object sender, EventArgs e)
    {
        if (Applicant.Rows.Count != 0)
        {
            Label gender = ((Label)Applicant.FindControl("Label17"));
            Label identity = ((Label)Applicant.FindControl("Label18"));

            switch (gender.Text)
            {
                case "Male":
                    identity.Text = "Mr. ";
                    break;
                case "Female":
                    identity.Text = "Ms. ";
                    break;
                default:
                    identity.Text = "";
                    break;
            }
        }

    }

  
    protected void purposegrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_purposegrid(purposegrid, sv.Applicant_id);
    }

    //general service
    protected void service1grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_service1grid(service1grid, sv.Request_id);
    }
    protected void service1grid_DataBound(object sender, EventArgs e)
    {
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }
        double final = 0.00;
        foreach (GridViewRow row in service1grid.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("service1grid_del"));
                Label type = ((Label)row.FindControl("Label3"));
                HyperLink edit = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = ((Label)row.FindControl("Label2"));

                edit.NavigateUrl = "~/Edit_Service.aspx?id=" + id.Text + "&cid=" + Clientid;
                edit.ToolTip = "Credential Consultant :: Edit Block.";
                edit.CssClass = "iframe";
                // edit.Attributes.Add("rel", "iframe");
                //edit.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");
                if (type.Text == "Evaluation")
                {
                    delete.Visible = false;
                }
                else
                {
                    edit.Visible = false;
                    Label info = ((Label)row.FindControl("Label4"));
                    delete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete this  \"" + info.Text + "\" Service ?')");

                }
            }

            Label total = ((Label)row.FindControl("Label1"));
            Label result = ((Label)service1grid.FooterRow.FindControl("Label7"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));



        }


    }
    protected void service1grid_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label2");
        int id = Convert.ToInt32(id_control.Text.ToString());
        Credentialpage.Utility.delete_evaluation_services(id);
        refresh_Click(this, EventArgs.Empty);
    }

  


    //review refresh
    protected void refresh_Click(object sender, EventArgs e)
    {
        Applicant_Load(this, EventArgs.Empty);
        purposegrid_Load(this, EventArgs.Empty);
        service1grid_Load(this, EventArgs.Empty);     
    }

    //Previous & Continue Button
    protected void frm8_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        Menu1.PublicMethodInUsercontrol(6);
        FormViewcontrol.ActiveViewIndex = 5;
    }
    protected void frm8_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        //int Clientid = 0;
        //if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }
        Credentialpage.Utility.senderinfo(sv.Applicant_id.ToString(), Applicantmsg.Text, "Client",txtsendername.Text,txtsendercontact.Text,txttargetinst.Text);

        Credentialpage.Utility.create_Applicantid(sv.Applicant_id);
        Credentialpage.Utility.get_Applicantid(Filenumber, sv.Applicant_id);
        if (Subdomain == "nosubdomain")
        {
            Response.Redirect("~/msg.aspx?id=" + Filenumber.Text);
        }
        else
        { Response.Redirect("~/msg.aspx?id=" + Filenumber.Text + "&subdomain=" + Subdomain); }
    }

    #endregion
#endregion



























}