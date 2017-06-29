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
public partial class nIndex : System.Web.UI.Page
{
    int Clientid = 0;
    #region Global variables     
    public int error_code = 0;
  

    Authentication.Utility.DomainAttributes dm = new Authentication.Utility.DomainAttributes();
    Authentication.Utility.SessionVariable sv = new Authentication.Utility.SessionVariable();                   
    RossSoft.Utility.AppConfig app = new RossSoft.Utility.AppConfig(); 

    #endregion
    string Subdomain = "";
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
            Authentication.Utility.checklogo(dm.DmID, OrgTitle, logo);

            spl_instruction.InnerHtml = dm.Spl_Instruction;  
        }  
         
      
    }

    #region dynamic radiobutton control for delivery service
    public void InitializeComponent()
    {

    }
    override protected void OnInit(EventArgs e)
    {
        ApplicationSettings();

        #region Generate Dynamic Radio button
        //Zero copy
        lbldelicopy.Text = "<b>" + Credentialpage.Utility.GetCopyHeader((1).ToString(), dm.DmID.ToString()) + " (primary mailing address)</b>";
        lbldelialert.InnerHtml = "Fill In the " + lbldelicopy.Text + " for primary mailing address.";

        if (dm.IncludedCopies != 0)
        {
            for (int j = 1; j <= dm.IncludedCopies - 1; j++)
            {
                HtmlGenericControl title = new HtmlGenericControl("p");
                dynamic_official.Controls.Add(title);
                title.InnerHtml = Credentialpage.Utility.GetCopyHeader((j + 1).ToString(), dm.DmID.ToString()); //"<b>Official Hard Copy Delivery-" + (j + 1).ToString() + ":</b>";

                RadioButtonList block = new RadioButtonList();
                block.ID = "block" + j.ToString();
                block.AutoPostBack = true;
                block.Items.Add(new ListItem("Please send my Official Hard Copy to my primary mailing address", "False"));
                block.Items.Add(new ListItem("Please send this Official Hard Copy to a separate address.", "True"));
                block.SelectedIndexChanged += new EventHandler(this.frm5_evalradio_SelectedIndexChanged);
                dynamic_official.Controls.Add(block);

            }

        }
        else
        {
            dynamic_official.Visible = false;
        }

        InitializeComponent();
        base.OnInit(e);
        #endregion

    }
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
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
           
        #region Application

        #region Instructions

        #region EducationalInstruction
        frm2_msginfo.InnerHtml = dm.EducationalInstruction; 
        frm3_msginfo.InnerHtml = dm.EducationalInstruction;    
        #endregion

        #region DeliveryInstruction
        frm5_msginfo.InnerHtml = dm.DeliveryInstruction;
        if (dm.DocumentInstruction != "")
        {
            frm4_instruction.HRef = "Document_instruction.aspx?cus=" + Clientid;
            
        }
        else { frm4_optional6.Visible = false; }
        #endregion

        #endregion

        #region creditcard

        string card = dm.SupportedCards;
        if (card != "")
        {
            string[] str1 = card.Split('|');
            card = "<ul>";
            for (int j = 0; j <= str1.Length - 1; j++)
            {
                card = card + "<li>" + str1[j].ToString() + "</li>";

            }
            card = card + "</ul>";
        }
        else { creditcardblk.Visible = false; }
        creditcardtxt.InnerHtml = dm.Creditcard_Instructions;
        mordertxt.InnerHtml = dm.Moneyorder_Instructions;
        pchecktxt.InnerHtml = dm.Personalcheck_Instructions; 
        creditcardblk.InnerHtml = "<br/><b>Credit Cards Accepted:</b><br/><br/>" + card;

        //if (dm.IsCreditcard == 1)
        //{
        //    frm7_optional2_li1.InnerHtml = "Please enter your credit/debit card information in the next section.";
        //    frm7_optional2_li2.InnerHtml = "You will then submit and print your information to complete the application process.";
        //    creditcardblk.InnerHtml = "<br/><b>Credit Cards Accepted:</b><br/><br/>" + card;
        //}
        //else
        //{

        //    frm7_optional2_li1.InnerHtml = "After printing the application summary/review, please fill out the credit card section by hand.";
        //    frm7_optional2_li2.InnerHtml = "You may also pay with a credit card over the phone.";
        //    creditcardblk.InnerHtml = "<br/><b>Credit Cards Accepted:</b><br/><br/>" + card;
        //}
        #endregion

        #region section
        if (!dm.AddSection)
        {
            frm5_Additionalblock.Visible = false;
            paysec_Additional.Visible = false;
            revsec_Additional.Visible = false; 
        }
        if (!dm.FaxSection)
        {
            frm5_faxblock.Visible = false;
            paysec_Fax.Visible = false;
            revsec_Fax.Visible = false; 
        }
        if (!dm.EmailSection)
        {
            frm5_Emailblock.Visible = false;
            paysec_Email.Visible = false;
            revsec_Email.Visible = false; 
        }
        #endregion

        #region purpose
        if (dm.PurposeSection)
        {
            
            frm4_option_purpose.SelectedValue = dm.Lock_PurposeId.ToString();
            frm4_opPurpose.Text = Credentialpage.Utility.Getpurposevalue(dm.Lock_PurposeId.ToString());  
            frm4_optional.Visible = false;
            frm4_optional1.Visible = false;
            frm4_optional2.Visible = false;
            frm4_optional3.Visible = false;
            frm4_optional4.Visible = false;
            frm4_optional5.Visible = false;

            int txtwidth = dm.Lock_TargetName.Length * 8;

            if (txtwidth < 200)
            {
                txtwidth = 200;
            }
            else if (txtwidth > 700)
            {
                txtwidth = 700;
            }
        
            switch (dm.Lock_PurposeId.ToString())
            {
                  
                case "1":
                    frm4_optional.Visible = true;
                    frm4_institution.Text = dm.Lock_TargetName;
                    frm4_institution.Width = txtwidth;
                    break;
                case "7":
                    frm4_optional.Visible = true;
                    frm4_institution.Text = dm.Lock_TargetName;
                    frm4_institution.Width = txtwidth;
                    break;
                case "2":
                    frm4_optional1.Visible = true;
                    frm4_organization.Text = dm.Lock_TargetName;
                    frm4_organization.Width = txtwidth;
                    break;
                case "3":
                    frm4_optional2.Visible = true;
                    frm4_lawfirm.Text = dm.Lock_TargetName;
                    frm4_lawfirm.Width = txtwidth;
                    break;
                case "4":
                    frm4_optional3.Visible = true;
                    frm4_board.Text = dm.Lock_TargetName;
                    frm4_board.Width = txtwidth;
                    frm4_state.Text = dm.Lock_State;
                    break;
                case "5":
                    frm4_optional4.Visible = true;
                    frm4_military.Text = dm.Lock_TargetName;
                    frm4_military.Width = txtwidth;
                    break;
                case "6":
                    frm4_optional5.Visible = true;
                    frm4_evaluation.Text = dm.Lock_TargetName;
                    frm4_evaluation.Width = txtwidth;
                    break;
            }
            frm4_option_purpose.Enabled = false;
            if (dm.Target_Section == 0)
            {
                frm4_institution.Enabled = true;
                frm4_lawfirm.Enabled = true;
                frm4_military.Enabled = true;
                frm4_organization.Enabled = true;
                frm4_state.Enabled = true;
                frm4_board.Enabled = true;
                frm4_evaluation.Enabled = true;
            }
            else
            {
                frm4_institution.Enabled = false;
                frm4_lawfirm.Enabled = false;
                frm4_military.Enabled = false;
                frm4_organization.Enabled = false;
                frm4_state.Enabled = false;
                frm4_board.Enabled = false;
                frm4_evaluation.Enabled = false;
            }
           
            frm4_opPurpose.Visible = true;
            frm4_option_purpose.Visible = false;
            asteriskalert.Visible = false;
            asterik.Visible = false;
        }
        else
        {
            frm4_opPurpose.Visible = false;
            frm4_option_purpose.Visible = true;
            asteriskalert.Visible = true;
            asterik.Visible = true;
        }
   
        #endregion


        #endregion

        #region Delivery

        Credentialpage.Utility.Getdeliverytype(frm5_deliverytypeeval, Clientid );
        Credentialpage.Utility.Getdeliverytype(frm5_deliverytypeaddl, Clientid);
        Credentialpage.Utility.Getdeliverytype(frm5_pdelivery, Clientid);

        //Mailing Service 
        Credentialpage.Utility.Grid_Mailcost(Grid_Mailcost, Clientid);       
        //Fax service 
        Credentialpage.Utility.GetFaxcost(Faxcost, Clientid);
        //Email service 
        Credentialpage.Utility.GetEmailcost(Emailcost, Clientid);


        #endregion

        #region Service

        //Additional Copy Cost
        Credentialpage.Utility.GetAdditionalcost(additionalcost, Clientid);

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
        SetDefaultButton(this.Page, frm1_previousid, frm1_Btn_continue);

        frm1_Btn_continue.Attributes.Add("onClick", "Loading(true);");
        toc.HRef = "OnlineTermsAndCondition.aspx?cus=" + Clientid.ToString();
        #endregion

        #region Purpose
        Credentialpage.Utility.Getpurpose(frm4_option_purpose);

        SetDefaultButton(this.Page, frm4_board, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_evaluation, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_institution, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_lawfirm, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_military, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_organization, frm4_btn_continue);
        SetDefaultButton(this.Page, frm4_state, frm4_btn_continue);

        frm4_option_purpose.Attributes.Add("onchange", "Loading(true);");
        frm4_btn_previous.Attributes.Add("onClick", "Loading(true);");
        frm4_btn_continue.Attributes.Add("onClick", "Loading(true);");
        #endregion

        #region SecondaryEducation
        Credentialpage.Utility.Getcountry(frm2_opt_country);
        Credentialpage.Utility.Getyear(frm2_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getyear(frm2_start_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getyear(frm2_end_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getmonth(frm2_month);
        Credentialpage.Utility.Getdate(frm2_date);

        SetDefaultButton(this.Page, frm2_institution, frm2_btn_submit);
        SetDefaultButton(this.Page, frm2_degree, frm2_btn_submit);
        SetDefaultButton(this.Page, frm2_city, frm2_btn_submit);
        SetDefaultButton(this.Page, frm2_state, frm2_btn_submit);

        frm2_opt_country.Attributes.Add("onchange", "Loading(true);");
        frm2_btn_clear.Attributes.Add("onClick", "Loading(true);");
        frm2_btn_submit.Attributes.Add("onClick", "Loading(true);");
        frm2_option_degree.Attributes.Add("onchange", "Loading(true);");
        frm2_option_graduate.Attributes.Add("onchange", "Loading(true);");
        frm2_start_year.Attributes.Add("onchange", "Loading(true);");
        frm2_option_graduate.Attributes.Add("onchange", "Loading(true);");

        frm2_btn_continue.Attributes.Add("onClick", "Loading(true);");
        frm2_btn_previous.Attributes.Add("onClick", "Loading(true);");
        #endregion

        #region HigherEducation
        Credentialpage.Utility.Getcountry(frm3_opt_country);
        Credentialpage.Utility.Getyear(frm3_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getyear(frm3_start_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getyear(frm3_end_year, Convert.ToInt32(app.Endyear), Convert.ToInt32(app.Endyear));
        Credentialpage.Utility.Getmonth(frm3_month);
        Credentialpage.Utility.Getdate(frm3_date);

        SetDefaultButton(this.Page, frm3_institution, frm3_btn_submit);
        SetDefaultButton(this.Page, frm3_degree, frm3_btn_submit);
        SetDefaultButton(this.Page, frm3_major, frm3_btn_submit);
        SetDefaultButton(this.Page, frm3_city, frm3_btn_submit);
        SetDefaultButton(this.Page, frm3_state, frm3_btn_submit);

        frm3_opt_country.Attributes.Add("onchange", "Loading(true);");
        frm3_btn_clear.Attributes.Add("onClick", "Loading(true);");
        frm3_btn_submit.Attributes.Add("onClick", "Loading(true);");
        frm3_option_degree.Attributes.Add("onchange", "Loading(true);");
        frm3_option_graduate.Attributes.Add("onchange", "Loading(true);");
        frm3_option_major.Attributes.Add("onchange", "Loading(true);");
        frm3_start_year.Attributes.Add("onchange", "Loading(true);");

        frm3_btn_continue.Attributes.Add("onClick", "Loading(true);");
        frm3_btn_previous.Attributes.Add("onClick", "Loading(true);");
        #endregion     

        #region Delivery   
        Credentialpage.Utility.Getcountry(frm5_countryeval);
        Credentialpage.Utility.Getcountry(frm5_countryaddl);
        Credentialpage.Utility.Getcountry(frm5_pcountry);
       
        //eval primary
        SetDefaultButton(this.Page, frm5_padd1, frm5_primarysubmit);
        SetDefaultButton(this.Page, frm5_padd2, frm5_primarysubmit);
        SetDefaultButton(this.Page, frm5_pcity, frm5_primarysubmit);
        SetDefaultButton(this.Page, frm5_pname, frm5_primarysubmit);
        SetDefaultButton(this.Page, frm5_pstate, frm5_primarysubmit);
        SetDefaultButton(this.Page, frm5_pzip, frm5_primarysubmit);

        //eval extra
        SetDefaultButton(this.Page, frm5_add1eval, frm5_btn_submiteval);
        SetDefaultButton(this.Page, frm5_add2eval, frm5_btn_submiteval);
        SetDefaultButton(this.Page, frm5_cityeval, frm5_btn_submiteval);
        SetDefaultButton(this.Page, frm5_Fnameeval, frm5_btn_submiteval);
        SetDefaultButton(this.Page, frm5_stateeval, frm5_btn_submiteval);
        SetDefaultButton(this.Page, frm5_zipeval, frm5_btn_submiteval);
        //fax
        SetDefaultButton(this.Page, frm5_attn, frm5_btn_submitfax);
        SetDefaultButton(this.Page, frm5_faxno, frm5_btn_submitfax);
        //addtitonal copy
        SetDefaultButton(this.Page, frm5_add1addl, frm5_btn_submit1addl);
        SetDefaultButton(this.Page, frm5_add2addl, frm5_btn_submit1addl);
        SetDefaultButton(this.Page, frm5_cityaddl, frm5_btn_submit1addl);
        SetDefaultButton(this.Page, frm5_Fnameaddl, frm5_btn_submit1addl);
        SetDefaultButton(this.Page, frm5_stateaddl, frm5_btn_submit1addl);
        SetDefaultButton(this.Page, frm5_zipaddl, frm5_btn_submit1addl);

        frm5_primarysubmit.Attributes.Add("onClick", "Loading(true);");
        frm5_primaryclear.Attributes.Add("onClick", "Loading(true);");
        frm5_btn_requestcopy.Attributes.Add("onClick", "Loading(true);");
        frm5_btn_faxcopy.Attributes.Add("onClick", "Loading(true);");
        frm5_btn_submiteval.Attributes.Add("onClick", "Loading(true);");
        frm5_btn_submitfax.Attributes.Add("onClick", "Loading(true);");
        refresh.Attributes.Add("onClick", "Loading(true);");

        frm5_btn_continue.Attributes.Add("onClick", "Loading(true);");
        frm5_btn_previous.Attributes.Add("onClick", "Loading(true);");
        #endregion

        #region Service
        frm6_btn_continue.Attributes.Add("onClick", "Loading(true);");
        frm6_btn_previous.Attributes.Add("onClick", "Loading(true);");
        #endregion

        #region Review
        frm8_btn_previous.Attributes.Add("onClick", "Loading(true);");
        frm8_Btn_continue.Attributes.Add("onClick", "Loading(true);");    
        #endregion

        #region Payment
        //Credentialpage.Utility.Getpaymentmode(frm7_payment);
        Credentialpage.Utility.Getpaymentmode(frm7_payment,dm.Moneyorder,dm.Onlinecc,dm.Personalcheck);

        frm7_payment.Attributes.Add("onchange", "Loading(true);");   
        #endregion   

    } 
    
    public void SetDefaultButton(Page page, TextBox textControl, Button defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    }
    public void SetDefaultButton(Page page, TextBox textControl, ImageButton defaultButton)
    {
        textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)");
    }
  

    

    #region Tabs



    #region Highschool & University clear function
    public void clear_school(string clear_mode)
    {
        // clear fields       
        frm2_city.Text = "";
        frm2_date.SelectedIndex = 0;
        frm2_end_year.SelectedIndex = 0;
        frm2_month.SelectedIndex = 0;
        frm2_option_graduate.SelectedIndex = 0;
        frm2_start_year.SelectedIndex = 0;
        frm2_year.SelectedIndex = 0;
        frm2_optional.Visible = false;
        frm2_state.Text = "";
        frm2_institution.Text = "";
        frm2_degree.Text = "";
        frm2_option_degree.Items.Clear();

        frm2_option_degree.Visible = true;
        frm2_RequiredFieldValidator4.Visible = true;
        frm2_degree.Visible = false;


        switch (clear_mode)
        {
            case "clearbtn":

                //new insertion of institution and degree
                //Credentialpage.Utility.Getinstitution(frm2_option_institution, 0, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()));
                //Credentialpage.Utility.Add_New(frm2_option_institution);
                Credentialpage.Utility.Getdegree(frm2_option_degree, 0, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()),  sv.Customer_id.ToString());
                Credentialpage.Utility.Add_New(frm2_option_degree);
                break;
            case "submitbtn":
                frm2_opt_country.SelectedIndex = 0;
                break;
        }

    }
    public void clear_university(string clear_mode)
    {
        // clear fields       
        frm3_city.Text = "";
        frm3_date.SelectedIndex = 0;
        frm3_end_year.SelectedIndex = 0;
        frm3_month.SelectedIndex = 0;
        frm3_start_year.SelectedIndex = 0;
        frm3_year.SelectedIndex = 0;
        frm3_option_graduate.SelectedIndex = 0;
        frm3_optional.Visible = false;
        frm3_state.Text = "";
        frm3_institution.Text = "";
        frm3_degree.Text = "";
        frm3_major.Text = "";


        frm3_option_degree.Items.Clear();
        frm3_option_major.Items.Clear();
        //2
        frm3_option_degree.Visible = true;
        frm3_RequiredFieldValidator6.Visible = true;
        frm3_degree.Visible = false;

        //3
        frm3_option_major.Visible = true;
        frm3_RequiredFieldValidator7.Visible = true;
        frm3_major.Visible = false;

        switch (clear_mode)
        {
            case "clearbtn":
                Credentialpage.Utility.Getdegree(frm3_option_degree, 1, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()),  sv.Customer_id.ToString());
                Credentialpage.Utility.Add_New(frm3_option_degree);
                Credentialpage.Utility.Getmajor(frm3_option_major, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()),  sv.Customer_id.ToString());
                Credentialpage.Utility.Add_New(frm3_option_major);
                break;
            case "submitbtn":
                frm3_opt_country.SelectedIndex = 0;
                break;
        }

    }
    #endregion



    #region Personnal Information Tab
    protected void frm1_optin_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Credentialpage.Utility.Getoptionalcell(frm1_optin_name, frm1_optional);
    }
    protected void frm1_option_service_SelectedIndexChanged(object sender, EventArgs e)
    {
        Credentialpage.Utility.Getoptional(frm1_option_service, frm1_optional1);
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

                        int applicant_id = Credentialpage.Utility.create_Applicante(frm1_Fname.Text, frm1_Mname.Text, frm1_Lname.Text, frm1_option_gender.SelectedItem.ToString(), birth, frm1_address1.Text, frm1_address2.Text, frm1_city.Text, Convert.ToInt32(frm1_option_country.SelectedValue.ToString()), frm1_state.Text, frm1_zip.Text.ToString(), frm1_home_phone.Text.ToString(), frm1_work_phone.Text.ToString(), frm1_cell_phone.Text.ToString(), frm1_primarymail.Text, Clientid, frm1_optFname.Text, frm1_optMname.Text, frm1_optLname.Text, frm1_previousid.Text, Convert.ToInt32(frm1_Country_birth.SelectedValue.ToString()), 0);
                        if (applicant_id != 0)
                        {
                            sv.page1 = 1;
                            sv.Applicant_id = applicant_id;                         
                            //  access to form 2               
                            FormViewcontrol.ActiveViewIndex = 3;
                            Menu1.PublicMethodInUsercontrol(2);

                        }
                        else
                        {
                            error_code = 2;

                        }
                        break;
                    case 1:
                        bool result = Credentialpage.Utility.update_Applicante(frm1_Fname.Text, frm1_Mname.Text, frm1_Lname.Text, frm1_option_gender.SelectedItem.ToString(), birth, frm1_address1.Text, frm1_address2.Text, frm1_city.Text, Convert.ToInt32(frm1_option_country.SelectedValue.ToString()), frm1_state.Text, frm1_zip.Text.ToString(), frm1_home_phone.Text.ToString(), frm1_work_phone.Text.ToString(), frm1_cell_phone.Text.ToString(), frm1_primarymail.Text,Clientid, frm1_optFname.Text, frm1_optMname.Text, frm1_optLname.Text, frm1_previousid.Text, Convert.ToInt32(frm1_Country_birth.SelectedValue.ToString()), 0,sv.Applicant_id);
                        if (result == true)
                        {

                            FormViewcontrol.ActiveViewIndex = 3;
                            Menu1.PublicMethodInUsercontrol(2);
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
        }
        Session["SV"] = sv;
    }
    #endregion
    #endregion

    #region Purpose Tab
    protected void frm4_option_purpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        frm4_institution.Text = "";
        frm4_lawfirm.Text = "";
        frm4_military.Text = "";
        frm4_organization.Text = "";
        frm4_state.Text = "";
        frm4_board.Text = "";
        frm4_evaluation.Text = "";
        switch (frm4_option_purpose.SelectedItem.ToString())
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
    protected void frm4_action()
    {
        switch (sv.page2)
        {
            case 0:
                int RequestID = 0;
                RequestID = Credentialpage.Utility.create_purpose(sv.Applicant_id, Convert.ToInt32(frm4_option_purpose.SelectedValue.ToString()), frm4_institution.Text, frm4_organization.Text, frm4_lawfirm.Text, frm4_board.Text, frm4_state.Text, frm4_military.Text, frm4_evaluation.Text);

                if (RequestID != 0)
                {
                    sv.page2 = 1;
                    sv.Request_id = RequestID;                  
                    //redirect to next page
                    FormViewcontrol.ActiveViewIndex = 4;
                    Menu1.PublicMethodInUsercontrol(3);

                    //  populate primary address
                    Credentialpage.Utility.GetPrimaryAddress(frm5_pname, frm5_padd1, frm5_padd2, frm5_pcity, frm5_pstate, frm5_pzip, frm5_pcountry, sv.Request_id);
          
                }
                break;
            case 1:
                bool result = Credentialpage.Utility.update_purpose(sv.Applicant_id, Convert.ToInt32(frm4_option_purpose.SelectedValue.ToString()), frm4_institution.Text, frm4_organization.Text, frm4_lawfirm.Text, frm4_board.Text, frm4_state.Text, frm4_military.Text, frm4_evaluation.Text, sv.Request_id);

                if (result == true)
                {
                    FormViewcontrol.ActiveViewIndex = 4;
                    Menu1.PublicMethodInUsercontrol(3);
                }
                break;
        }
        Session["SV"] = sv;
    }
    #region Previous Button
    protected void frm4_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        FormViewcontrol.ActiveViewIndex = 0;
        Menu1.PublicMethodInUsercontrol(1);
    }
    #endregion
    #region Continue Button
    protected void frm4_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        //Process
        Page.Validate("frm4_group");
        if (Page.IsValid)
        {
            if (frm4_optional6.Visible == true)
            {
                if (frm4_agree.Checked == true)
                {
                    frm4_action();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('You must Accept the Terms and Condition to Continue')</script>");
                }
            }
            else
            {
                frm4_action();
            }
        }
    }
    #endregion
    #endregion


    #region Upper Secondary or Higher secondary Tab
    protected void frm2_option_graduate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm2_option_graduate.SelectedIndex == 1)
        {
            frm2_optional.Visible = true;
        }
        else
        {
            frm2_optional.Visible = false;
        }
    }
    protected void frm2_opt_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear_school("countrybtn");
        if (frm2_opt_country.SelectedIndex != 0)
        {
            frm2_autoComplete1.ContextKey = frm2_opt_country.SelectedValue.ToString() + "|" +  sv.Customer_id.ToString();
            // Credentialpage.Utility.Getinstitution(frm2_option_institution, 0, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()));
            // Credentialpage.Utility.Add_New(frm2_option_institution);
            Credentialpage.Utility.Getdegree(frm2_option_degree, 0, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()),  sv.Customer_id.ToString());
            Credentialpage.Utility.Add_New(frm2_option_degree);
            //visibility on
            frm2_details.Visible = true;
            frm2_details1.Visible = true;
            frm2_details2.Visible = true;
        }
        else
        {
            //visibility off
            frm2_details.Visible = false;
            frm2_details1.Visible = false;
            frm2_details2.Visible = false;
            clear_school("countrybtn");

        }


    }
    protected void frm2_option_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //frm2_degree.Text = frm2_option_degree.SelectedValue.ToString();   
        if (frm2_option_degree.SelectedItem.ToString() == "Add New")
        {
            Credentialpage.Utility.SetFocus(frm2_degree);
            frm2_option_degree.Visible = false;
            frm2_RequiredFieldValidator4.Visible = false;
            frm2_degree.Visible = true;
        }
    }
    protected void frm2_start_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm2_start_year.SelectedValue.ToString() != "")
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            frm2_year.Items.Clear();
            frm2_end_year.Items.Clear();
            Credentialpage.Utility.Getyear(frm2_year, Convert.ToInt32(frm2_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
            Credentialpage.Utility.Getyear(frm2_end_year, Convert.ToInt32(frm2_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
        }
    }

    //clear & submit Button
    protected void frm2_btn_clear_Click(object sender, EventArgs e)
    {
        clear_school("clearbtn");

    }
    protected void frm2_btn_submit_Click(object sender, EventArgs e)
    {

        //check for new entry
        int institution_id = 0;
        int degree_id = 0;

        Page.Validate("frm2_group");
        if (Page.IsValid)
        {
            institution_id = Credentialpage.Utility.AddNew_institution(frm2_institution.Text.Replace("'", "''"), Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()), "HighSchool", sv.Customer_id.ToString());
        }

        if (frm2_option_graduate.SelectedItem.ToString() == "Yes")
        {
            Page.Validate("frm2_group3");
        }

        if (frm2_degree.Visible == true)
        {
            Page.Validate("frm2_group2");
            if (Page.IsValid)
            {
                degree_id = Credentialpage.Utility.AddNew_degree(frm2_degree.Text, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()), "HighSchool", sv.Customer_id.ToString());
            }
        }
        else
        {
            degree_id = Convert.ToInt32(frm2_option_degree.SelectedValue.ToString());
        }

        if (Page.IsValid)
        {
            string DateDegreeAwarded;
            int graduated;
            if (frm2_option_graduate.SelectedIndex == 1)
            {
                DateDegreeAwarded = frm2_date.SelectedValue.ToString() + "/" + frm2_month.SelectedValue.ToString() + "/" + frm2_year.SelectedValue.ToString();
            }
            else
            {
                DateDegreeAwarded = "Null";
            }
            if (frm2_option_graduate.SelectedValue.ToString() == "True")
            {
                graduated = 1;
            }
            else
            {
                graduated = 0;
            }
            bool result = Credentialpage.Utility.create_education(sv.Request_id, Convert.ToInt32(frm2_opt_country.SelectedValue.ToString()), 1, institution_id, frm2_start_year.SelectedItem.ToString(), frm2_end_year.SelectedItem.ToString(), degree_id, graduated, DateDegreeAwarded, 1, frm2_city.Text, frm2_state.Text);

            if (result == true)
            {
                Education_Refresh();
                //visibility off and clear func
                frm2_details.Visible = false;
                //1
                //frm2_option_institution.Visible = true;
                frm2_RequiredFieldValidator3.Visible = true;
                // frm2_institution.Visible = false;
                //2
                frm2_option_degree.Visible = true;
                frm2_RequiredFieldValidator4.Visible = true;
                frm2_degree.Visible = false;
                clear_school("submitbtn");

            }
        }

        
    }
    //High school Grid
    protected void Highschoolgrid_Load(object sender, EventArgs e)
    {
        if (sv.Request_id  != 0)
        {
            Highschoolgrid.DataSource = Credentialpage.Utility.GetHighschool(sv.Request_id);
            Highschoolgrid.DataBind();

            if (Highschoolgrid.Rows.Count > 0)
            {
                frm2_display.Visible = true;
            }
            else
            {
                frm2_display.Visible = false;
            }
        }
    }
    public void Education_Refresh()
    {
        Highschoolgrid_Load(this, EventArgs.Empty);
        Universitygrid_Load(this, EventArgs.Empty);

    }

    //Previous & Continue Button
    protected void frm2_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        FormViewcontrol.ActiveViewIndex = 3;
        Menu1.PublicMethodInUsercontrol(2);
    }
    protected void frm2_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        //process
        if (Highschoolgrid.Rows.Count > 0)
        {
            FormViewcontrol.ActiveViewIndex = 2;
            Menu1.PublicMethodInUsercontrol(4);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('You must Enter Secondary Education / High School History to Proceed')</script>");
        }

    }
    #endregion




    #region Post Secondary or University Tab
    protected void frm3_option_graduate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm3_option_graduate.SelectedIndex == 1)
        {
            frm3_optional.Visible = true;
        }
        else
        {
            frm3_optional.Visible = false;
        }
    }
    protected void frm3_opt_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear_university("countrybtn");
        if (frm3_opt_country.SelectedIndex != 0)
        {
            frm3_AutoCompleteExtender1.ContextKey = frm3_opt_country.SelectedValue.ToString() + "|" + sv.Customer_id.ToString();
            // Credentialpage.Utility.Getinstitution(frm3_option_institution, 1, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()));
            //Credentialpage.Utility.Add_New(frm3_option_institution);
            Credentialpage.Utility.Getdegree(frm3_option_degree, 1, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()), sv.Customer_id.ToString());
            Credentialpage.Utility.Add_New(frm3_option_degree);
            Credentialpage.Utility.Getmajor(frm3_option_major, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()), sv.Customer_id.ToString());
            Credentialpage.Utility.Add_New(frm3_option_major);
            //visibility on
            frm3_details.Visible = true;
            frm3_details1.Visible = true;
            frm3_details2.Visible = true;

        }
        else
        {
            //visibility off
            frm3_details.Visible = false;
            frm3_details1.Visible = false;
            frm3_details2.Visible = false;
            clear_university("countrybtn");
        }
    }
    protected void frm3_option_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm3_option_degree.SelectedItem.ToString() == "Add New")
        {
            Credentialpage.Utility.SetFocus(frm3_degree);
            frm3_option_degree.Visible = false;
            frm3_RequiredFieldValidator6.Visible = false;
            frm3_degree.Visible = true;
        }

    }
    protected void frm3_option_major_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm3_option_major.SelectedItem.ToString() == "Add New")
        {
            Credentialpage.Utility.SetFocus(frm3_major);
            frm3_option_major.Visible = false;
            frm3_RequiredFieldValidator7.Visible = false;
            frm3_major.Visible = true;
        }
    }
    protected void frm3_start_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (frm3_start_year.SelectedValue.ToString() != "")
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            frm3_year.Items.Clear();
            frm3_end_year.Items.Clear();
            Credentialpage.Utility.Getyear(frm3_year, Convert.ToInt32(frm3_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
            Credentialpage.Utility.Getyear(frm3_end_year, Convert.ToInt32(frm3_start_year.SelectedValue.ToString()), Convert.ToInt32(app.Endyear));
        }
    }
    //clear & submit Button
    protected void frm3_btn_clear_Click(object sender, EventArgs e)
    {
        clear_university("clearbtn");

    }
    protected void frm3_btn_submit_Click(object sender, EventArgs e)
    {
        //check for new entry
        int institution_id = 0;
        int degree_id = 0;
        int major_id = 0;

        Page.Validate("frm3_group");
        if (Page.IsValid)
        {
            institution_id = Credentialpage.Utility.AddNew_institution(frm3_institution.Text, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()), "University",  sv.Customer_id.ToString());
        }
        if (frm3_option_graduate.SelectedItem.ToString() == "Yes")
        {
            Page.Validate("frm3_group4");
        }

        if (frm3_degree.Visible == true)
        {
            Page.Validate("frm3_group2");
            if (Page.IsValid)
            {
                degree_id = Credentialpage.Utility.AddNew_degree(frm3_degree.Text, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()), "University",  sv.Customer_id.ToString());
            }
        }
        else
        {
            degree_id = Convert.ToInt32(frm3_option_degree.SelectedValue.ToString());
        }
        if (frm3_major.Visible == true)
        {
            Page.Validate("frm3_group3");
            if (Page.IsValid)
            {
                major_id = Credentialpage.Utility.AddNew_major(frm3_major.Text, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()),  sv.Customer_id.ToString());
            }
        }
        else
        {
            major_id = Convert.ToInt32(frm3_option_major.SelectedValue.ToString());
        }
        if (Page.IsValid)
        {
            string DateDegreeAwarded;
            int graduated;
            if (frm3_option_graduate.SelectedIndex == 1)
            {
                DateDegreeAwarded = frm3_date.SelectedValue.ToString() + "/" + frm3_month.SelectedValue.ToString() + "/" + frm3_year.SelectedValue.ToString();
            }
            else
            {
                DateDegreeAwarded = "Null";
            }
            if (frm3_option_graduate.SelectedValue.ToString() == "True")
            {
                graduated = 1;
            }
            else
            {
                graduated = 0;
            }
            bool result = Credentialpage.Utility.create_education(sv.Request_id, Convert.ToInt32(frm3_opt_country.SelectedValue.ToString()), major_id, institution_id, frm3_start_year.SelectedItem.ToString(), frm3_end_year.SelectedItem.ToString(), degree_id, graduated, DateDegreeAwarded, 1, frm3_city.Text, frm3_state.Text);

            if (result == true)
            {

                Education_Refresh();
                //visibility off and clear func
                frm3_details.Visible = false;
                //1
                //frm3_option_institution.Visible = true;
                //frm3_RequiredFieldValidator1.Visible = true;
                //frm3_institution.Visible = false; 

                //2
                frm3_option_degree.Visible = true;
                frm3_RequiredFieldValidator6.Visible = true;
                frm3_degree.Visible = false;

                //3
                frm3_option_major.Visible = true;
                frm3_RequiredFieldValidator7.Visible = true;
                frm3_major.Visible = false;
                clear_university("submitbtn");

            }
        }

    }
    //University Grid
    protected void Universitygrid_Load(object sender, EventArgs e)
    {
        Universitygrid.DataSource = Credentialpage.Utility.GetUniversity(sv.Request_id);
        Universitygrid.DataBind();

        if (Universitygrid.Rows.Count > 0)
        {
            frm3_display.Visible = true;
        }
        else
        {
            frm3_display.Visible = false;
        }
    }
    //Previous & Continue Button
    protected void frm3_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        FormViewcontrol.ActiveViewIndex = 1;
        Menu1.PublicMethodInUsercontrol(3);

    }
    protected void frm3_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        if (sv.page4 == 0)
        {
            //  populate primary address
            Credentialpage.Utility.GetPrimaryAddress(frm5_pname, frm5_padd1, frm5_padd2, frm5_pcity, frm5_pstate, frm5_pzip, frm5_pcountry, sv.Request_id);
            sv.page4 = 1;
        }
        //redirect to next page
        FormViewcontrol.ActiveViewIndex = 4;
        Menu1.PublicMethodInUsercontrol(5);
        Session["SV"] = sv;
    }

    #endregion

    #region Delivery Service Tab


    protected void frm5_primarysubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("frm5_group3");
        if (Page.IsValid)
        {
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_pdelivery.SelectedValue.ToString()), sv.Request_id, frm5_pname.Text, frm5_padd1.Text, frm5_padd2.Text, frm5_pcity.Text, frm5_pstate.Text, frm5_pzip.Text, Convert.ToInt32(frm5_pcountry.SelectedValue.ToString()), 1, "Evaluation", "primary",frm5_pinst.Text);
            frm5_evalgridblock.Visible = true;
            Credentialpage.Utility.displayprimary(frm5_primarygrid, sv.Request_id);
            frm5_primarysuccessmsg.Visible = true;
            frm5_primaryform.Visible = false;
            Mailing_Refresh();
            frm5_btn_requestcopy.Enabled = true;
            frm5_overlap.Visible = false;
            frm5_primarymsg.Visible = false;
        }
    }
    protected void frm5_primaryclear_Click(object sender, EventArgs e)
    {
        frm5_primaryform.Visible = false;
        Credentialpage.Utility.displayprimary(frm5_primarygrid, sv.Request_id);
        frm5_primarysuccessmsg.Visible = true;

        frm5_padd1.Text = "";
        frm5_padd2.Text = "";
        frm5_pstate.Text = "";
        frm5_pcity.Text = "";
        frm5_pname.Text = "";
        frm5_pdelivery.SelectedIndex = 0;
        frm5_pcountry.SelectedIndex = 0;
        frm5_pzip.Text = "";
    }
    //evalextra
    protected void frm5_evalradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        // frm5_evalradio.Visible = false;
        RadioButtonList blockcntrl = (RadioButtonList)sender;
        string evalcount = "Copy" + (Convert.ToInt32(blockcntrl.ID.ToString().Substring(5)) + 1).ToString();
        frm5_hiddenvalue.Text = evalcount;
        //db check on existing value
        Credentialpage.Utility.Check_copy(sv.Request_id, evalcount);

        if (blockcntrl.SelectedValue.ToString() == "False")
        {    //transform

            int result = Credentialpage.Utility.SaveSameAddress(officialgrid, sv.Request_id, sv.Customer_id, evalcount);
            if (result == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Fill In the '" + Credentialpage.Utility.GetCopyHeader((1).ToString(), dm.DmID.ToString()).Replace("'", "''") + "' for primary mailing address')</script>");
                blockcntrl.ClearSelection();
            }
            else
            {
                Mailing_Refresh();

            }
        }
        else
        {
            dynamic_official.Visible = false;
            frm5_evalformheader.InnerHtml = Credentialpage.Utility.GetCopyHeader((Convert.ToInt32(blockcntrl.ID.ToString().Substring(5)) + 1).ToString(), dm.DmID.ToString()); //"( Official Hard Copy Delivery - " + (Convert.ToInt32(blockcntrl.ID.ToString().Substring(5)) + 1).ToString() + " ) :";
            frm5_evalform.Visible = true;
            frm5_evalwarning.Visible = true;
        }

    }
    protected void frm5_btn_cleareval_Click(object sender, EventArgs e)
    {
        frm5_add1eval.Text = "";
        frm5_add2eval.Text = "";
        frm5_stateeval.Text = "";
        frm5_cityeval.Text = "";
        frm5_Fnameeval.Text = "";
        frm5_deliverytypeeval.SelectedIndex = 0;
        frm5_countryeval.SelectedIndex = 0;
        frm5_zipeval.Text = "";
        frm5_instname.Text = ""; 
    }

    protected void frm5_btn_submiteval_Click(object sender, EventArgs e)
    {

        Page.Validate("frm5_group");
        if (Page.IsValid)
        {
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_deliverytypeeval.SelectedValue.ToString()), sv.Request_id, frm5_Fnameeval.Text, frm5_add1eval.Text, frm5_add2eval.Text, frm5_cityeval.Text, frm5_stateeval.Text, frm5_zipeval.Text, Convert.ToInt32(frm5_countryeval.SelectedValue.ToString()), 1, "Evaluation", frm5_hiddenvalue.Text.ToString(),frm5_instname.Text);
            Mailing_Refresh();
            frm5_evalwarning.Visible = false;
            frm5_evalform.Visible = false;
            dynamic_official.Visible = true;
            frm5_btn_cleareval_Click(this, EventArgs.Empty);
        }

    }

    //additional copy
    protected void frm5_btn_requestcopy_Click(object sender, EventArgs e)
    {
        frm5_addlgridblock.Visible = false;
        frm5_btn_requestcopy.Visible = false;
        frm5_Additionalrequestradiobtn.Visible = true;
    }
    protected void frm5_addlradiobtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        frm5_Additionalrequestradiobtn.Visible = false;
        frm5_Additionalrequestform.Visible = true;
        frm5_addlwarning.Visible = true;
        if (frm5_addlradiobtn.SelectedValue.ToString() == "False")
        {
            dtype.Visible = false;
            Credentialpage.Utility.GetSameAddress(frm5_Fnameaddl, frm5_add1addl, frm5_add2addl, frm5_cityaddl, frm5_stateaddl, frm5_zipaddl, frm5_countryaddl, frm5_deliverytypeaddl, sv.Request_id);
        }
        else
        {
            dtype.Visible = true;
        }

    }
    protected void frm5_btn_canceladdl_Click(object sender, EventArgs e)
    {
        frm5_addlgridblock.Visible = true;
        frm5_addlwarning.Visible = false;
        frm5_btn_requestcopy.Visible = true;
        frm5_Additionalrequestform.Visible = false;
        frm5_addlradiobtn.ClearSelection();
        frm5_Additionalrequestradiobtn.Visible = false;

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
    protected void frm5_btn_submit1addl_Click(object sender, EventArgs e)
    {        
        Page.Validate("frm5_addlgroup");
        if (Page.IsValid)
        {
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_deliverytypeaddl.SelectedValue.ToString()), sv.Request_id, frm5_Fnameaddl.Text, frm5_add1addl.Text, frm5_add2addl.Text, frm5_cityaddl.Text, frm5_stateaddl.Text, frm5_zipaddl.Text, Convert.ToInt32(frm5_countryaddl.SelectedValue.ToString()), Convert.ToInt32(frm5_copies_addl.SelectedValue.ToString()), "Additional", "Additional",frm5_addlinstname.Text);
            Mailing_Refresh();
            frm5_btn_canceladdl_Click(this, EventArgs.Empty);
        }
    }

    //fax
    protected void frm5_btn_cancelfax_Click(object sender, EventArgs e)
    {
        Mailing_Refresh();
        frm5_optional6.Visible = true;
        frm5_optional7.Visible = false;
        frm5_optional10.Visible = false;
    }
    protected void frm5_btn_faxcopy_Click(object sender, EventArgs e)
    {
        frm5_faxno.Text = "";
        frm5_attn.Text = "";
        frm5_optional6.Visible = false;
        frm5_optional7.Visible = true;
        frm5_optional10.Visible = true;
    }
    protected void frm5_btn_submitfax_Click(object sender, EventArgs e)
    {
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }     
        Page.Validate("frm5_group2");
        if (Page.IsValid)
        {
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(4, sv.Request_id, frm5_attn.Text, frm5_faxno.Text, 1, 1, "Fax", Clientid.ToString());
            Mailing_Refresh();
            frm5_faxno.Text = "";
            frm5_attn.Text = "";
            frm5_optional6.Visible = true;
            frm5_optional7.Visible = false;
            frm5_optional10.Visible = false;

        }
    }

    //Email
    protected void frm5_btn_cancelemail_Click(object sender, EventArgs e)
    {
        Mailing_Refresh();
        frm5_optional11.Visible = true;
        frm5_optional12.Visible = false;
        frm5_optional13.Visible = false;
    }
    protected void frm5_btn_emailcopy_Click(object sender, EventArgs e)
    {
        frm5_ename.Text = "";
        frm5_email.Text = "";
        frm5_optional11.Visible = false;
        frm5_optional12.Visible = true;
        frm5_optional13.Visible = true;
    }
    protected void frm5_btn_submitemail_Click(object sender, EventArgs e)
    {
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }      
        Page.Validate("frm5_group3");
        if (Page.IsValid)
        {
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(sv.Request_id, frm5_ename.Text, frm5_email.Text, 1, 1, Clientid.ToString()  ,"Email");
            Mailing_Refresh();
            frm5_ename.Text = "";
            frm5_email.Text = "";
            frm5_optional11.Visible = true;
            frm5_optional12.Visible = false;
            frm5_optional13.Visible = false;

        }
    }


    //official grid
    protected void officialgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_officialgrid(officialgrid, sv.Request_id);
    }
    //aditional grid
    protected void addevalgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_addevalgrid(addevalgrid, sv.Request_id);
    }
    //fax grid
    protected void faxgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_faxgrid(faxgrid, sv.Request_id);
    }
    //Emailgrid
    protected void Emailgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_emailgrid(Emailgrid, sv.Request_id);
    }
    //mailcost grid
    protected void Grid_Mailcost_Load(object sender, EventArgs e)
    {
        //Credentialpage.Utility.Grid_Mailcost(Grid_Mailcost, Convert.ToInt32( sv.Customer_id.ToString()));
        ////additonal and fax cost
        //Credentialpage.Utility.GetFaxcost(Faxcost, Convert.ToInt32( sv.Customer_id.ToString()));
        //Credentialpage.Utility.GetAdditionalcost(additionalcost, Convert.ToInt32( sv.Customer_id.ToString()));
    }

    public void Mailing_Refresh()
    {
        Grid_Mailcost_Load(this, EventArgs.Empty);
        officialgrid_Load(this, EventArgs.Empty);
        addevalgrid_Load(this, EventArgs.Empty);
        faxgrid_Load(this, EventArgs.Empty);
        Emailgrid_Load(this, EventArgs.Empty);
    }

    //Previous & Continue Button
    protected void frm5_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        FormViewcontrol.ActiveViewIndex = 3;
        Menu1.PublicMethodInUsercontrol(2);
    }
    protected void frm5_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        if (frm5_primaryform.Visible != true)
        {
            Menu1.PublicMethodInUsercontrol(4);
            FormViewcontrol.ActiveViewIndex = 5;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('You must submit the Primary Address to Continue')</script>");
        }
    }

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
            Applicant_Load(this, EventArgs.Empty);
            sv.page3 = 1;
            Menu1.PublicMethodInUsercontrol(5);
            FormViewcontrol.ActiveViewIndex = 7;

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
                Label lblType = (Label)row.FindControl("lblType");
                DropDownList drp = (DropDownList)row.FindControl("drpCount");

                if (servicetag.Checked)
                {
                    tagvalue = Convert.ToInt32(servicetag.Value.ToString());
                   // Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);
                    if (lblType.Text == "Evaluation Multiplier")
                    {
                        for (int i = 0; i < Convert.ToInt32(drp.SelectedValue.ToString()); i++)
                        {
                    Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);
                        }
                    }
                    else
                    {
                        Credentialpage.Utility.create_Service(sv.Request_id, tagvalue);
                    }

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
        FormViewcontrol.ActiveViewIndex = 4;
        Menu1.PublicMethodInUsercontrol(3);
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

    //hischoolgrid
    //protected void hischoolgrid_Load(object sender, EventArgs e)
    //{
    //    Credentialpage.Utility.Grid_hischoolgrid(hischoolgrid, sv.Request_id);
    //}
    //protected void hischoolgrid_DataBound(object sender, EventArgs e)
    //{       
    //    foreach (GridViewRow row in hischoolgrid.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
    //            Label id = ((Label)row.FindControl("Label5"));
    //            link.NavigateUrl = "~/Edit_school.aspx?id=" + id.Text + "&cid=" + dm.DmID + "&rid=" + sv.Request_id;
    //            //link.ToolTip = "Credential Consultant :: Edit Block. :: width:900, height: 450";
    //            //link.CssClass = "lightview";
    //            //link.Attributes.Add("rel", "iframe");
    //            link.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");

    //            ImageButton l = (ImageButton)row.FindControl("hischoolgrid_del");
    //            l.Attributes.Add("onclick", "javascript:return " +
    //            "confirm('Are you sure you want to delete this HighSchool record?')");

    //        }
    //    }

    //}
    //protected void hischoolgrid_del_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton deletebtn = (ImageButton)sender;
    //    GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
    //    Label id_control = (Label)grdRow.FindControl("Label5");
    //    int id = Convert.ToInt32(id_control.Text.ToString());
    //    Credentialpage.Utility.delete_Applicant_Education_History(id);
    //    refresh_Click(this, EventArgs.Empty);
    //}

    //univgrid
    //protected void univgrid_Load(object sender, EventArgs e)
    //{
    //    Credentialpage.Utility.Grid_univgrid(univgrid, sv.Request_id);
    //}
    //protected void univgrid_DataBound(object sender, EventArgs e)
    //{      
    //    foreach (GridViewRow row in univgrid.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
    //            Label id = ((Label)row.FindControl("Label1"));
    //            link.NavigateUrl = "~/Edit_University.aspx?id=" + id.Text + "&cid=" + dm.DmID + "&rid=" + sv.Request_id;
    //            //link.ToolTip = "Credential Consultant :: Edit Block. :: width:900, height: 480";
    //            //link.CssClass = "lightview";
    //            //link.Attributes.Add("rel", "iframe");
    //            link.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");

    //            ImageButton l = (ImageButton)row.FindControl("univgrid_del");
    //            l.Attributes.Add("onclick", "javascript:return " +
    //            "confirm('Are you sure you want to delete this University record?')");

    //        }
    //    }



    //}
    //protected void univgrid_del_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton deletebtn = (ImageButton)sender;
    //    GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
    //    Label id_control = (Label)grdRow.FindControl("Label1");
    //    int id = Convert.ToInt32(id_control.Text.ToString());
    //    Credentialpage.Utility.delete_Applicant_Education_History(id);
    //    refresh_Click(this, EventArgs.Empty);
    //}

    //purposegrid
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

                edit.NavigateUrl = "~/Edit_Service.aspx?id=" + id.Text + "&cid=" +  Clientid;
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

    //delivery grid
    protected void Delivery_Grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_DeliveryGrid(Delivery_Grid, sv.Request_id);
    }
    protected void Delivery_Grid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Delivery_Grid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            Label count = ((Label)row.FindControl("Label2"));
            Label cost = ((Label)row.FindControl("Label1"));
            Label total = ((Label)row.FindControl("Label7"));

            if (type.Text == "Fax")
            {
                int result = (Convert.ToInt32(count.Text) * Convert.ToInt32(cost.Text));
                total.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));

            }
            else
            {
                total.Text = String.Format("{0:c}", Convert.ToDouble(cost.Text));
            }

            //display cost
            string temp = cost.Text;
            cost.Text = String.Format("{0:c}", Convert.ToDouble(temp.ToString()));

        }
        double final = 0.00;
        foreach (GridViewRow row in Delivery_Grid.Rows)
        {
            Label total = ((Label)row.FindControl("Label7"));
            Label result = ((Label)Delivery_Grid.FooterRow.FindControl("Label8"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));


        }
    }

    //Additional copy grid
    protected void copychargergrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_copycharger(copychargergrid, sv.Request_id, "Additional");
    }
    protected void copychargergrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.AdditionalCopy;
        foreach (GridViewRow row in copychargergrid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("copychargergrid_del"));
                Label info = ((Label)row.FindControl("Label3"));

                delete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this  \"" + info.Text + "\" Service ?')");

            }
            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Hard Copy";
            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)copychargergrid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }
    }
    protected void copychargergrid_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label2");
        int id = Convert.ToInt32(id_control.Text.ToString());
        Credentialpage.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
    }

    //fax grid
    protected void fax_grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_copycharger(fax_grid, sv.Request_id, "Fax");
    }
    protected void fax_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.FaxCopy;
        foreach (GridViewRow row in fax_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("fax_grid_del"));
                Label info = ((Label)row.FindControl("Label3"));

                delete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this  \"" + info.Text + "\" Service ?')");

            }


            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)fax_grid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }
    }
    protected void fax_grid_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label2");
        int id = Convert.ToInt32(id_control.Text.ToString());
        Credentialpage.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
    }

    //email grid
    protected void email_grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_copycharger(email_grid, sv.Request_id, "Email");
    }
    protected void email_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.EmailCopy;
        foreach (GridViewRow row in email_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("email_grid_del"));
                Label info = ((Label)row.FindControl("Label3"));

                delete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this  \"" + info.Text + "\" Service ?')");

            }

            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Electronic Copy";
            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)email_grid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }
    }
    protected void email_grid_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label2");
        int id = Convert.ToInt32(id_control.Text.ToString());
        Credentialpage.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
    }

    //review refresh
    protected void refresh_Click(object sender, EventArgs e)
    {
        //hischoolgrid_Load(this, EventArgs.Empty);
        //univgrid_Load(this, EventArgs.Empty);
        purposegrid_Load(this, EventArgs.Empty);
        service1grid_Load(this, EventArgs.Empty);
        copychargergrid_Load(this, EventArgs.Empty);
        Delivery_Grid_Load(this, EventArgs.Empty);
        fax_grid_Load(this, EventArgs.Empty);
        email_grid_Load(this, EventArgs.Empty);
        Review_total_Amount();

    }

    //review cost
    public void Review_total_Amount()
    {
        double sum1 = 0.00, sum2 = 0.00, sum3 = 0.00, sum4 = 0.00, sum5 = 0.00;
        String delim = "$";
        if (service1grid.Rows.Count != 0)
        {
            Label result1 = ((Label)service1grid.FooterRow.FindControl("Label7"));
            String str1 = result1.Text;
            sum1 = Convert.ToDouble(str1.Trim(delim.ToCharArray()));
        }
        else
        {
            sum1 = 0.00;
        }
        if (Delivery_Grid.Rows.Count != 0)
        {
            Label result2 = ((Label)Delivery_Grid.FooterRow.FindControl("Label8"));
            String str2 = result2.Text;
            sum2 = Convert.ToDouble(str2.Trim(delim.ToCharArray()));

        }
        else
        {
            sum2 = 0.00;
        }
        if (copychargergrid.Rows.Count != 0)
        {
            Label result3 = ((Label)copychargergrid.FooterRow.FindControl("Label11"));

            String str3 = result3.Text;
            sum3 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));
        }
        else
        {
            sum3 = 0.00;
        }
        if (fax_grid.Rows.Count != 0)
        {
            Label result3 = ((Label)fax_grid.FooterRow.FindControl("Label11"));

            String str3 = result3.Text;
            sum4 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));
        }
        else
        {
            sum4 = 0.00;
        }
        if (email_grid.Rows.Count != 0)
        {
            Label result3 = ((Label)email_grid.FooterRow.FindControl("Label11"));

            String str3 = result3.Text;
            sum5 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));
        }
        else
        {
            sum5 = 0.00;
        }

        double final = sum1 + sum2 + sum3 + sum4 + sum5;

        Reviewcost.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));


    }
    //Previous & Continue Button
    protected void frm8_btn_previous_Click(object sender, ImageClickEventArgs e)
    {
        Menu1.PublicMethodInUsercontrol(4);
        FormViewcontrol.ActiveViewIndex = 5;
    }
    protected void frm8_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        int Clientid = 0;
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }
        Credentialpage.Utility.Applicantnote(sv.Applicant_id.ToString(), Applicantmsg.Text, "Client");    

        payment_refresh();
        FormViewcontrol.ActiveViewIndex = 6;
        Menu1.PublicMethodInUsercontrol(6);
    }

    #endregion

    #region Payment Tab
    protected void frm7_payment_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (frm7_payment.SelectedItem.ToString())
        {
            case "cashiers check/money order":
                frm7_optional.Visible = true;
                frm7_optional1.Visible = false;
                frm7_optional2.Visible = false;
                break;
            case "credit card":
                frm7_optional.Visible = false;
                frm7_optional1.Visible = false;
                frm7_optional2.Visible = true;           
                break;
            case "personal check":
                frm7_optional.Visible = false;
                frm7_optional1.Visible = true;
                frm7_optional2.Visible = false;
                break;
            default:
                frm7_optional.Visible = false;
                frm7_optional1.Visible = false;
                frm7_optional2.Visible = false;
                break;
        }
    }
    //payment grid
    protected void paymentgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_paymentgrid(paymentgrid, sv.Request_id);
    }
    protected void paymentgrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        foreach (GridViewRow row in paymentgrid.Rows)
        {
            Label total = ((Label)row.FindControl("Label6"));
            Label result = ((Label)paymentgrid.FooterRow.FindControl("Label7"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));

        }
    }
    //additional grid
    protected void addcopygrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_addcopygrid(addcopygrid, sv.Request_id);
    }
    protected void addcopygrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.AdditionalCopy; 
        foreach (GridViewRow row in addcopygrid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Hard Copy";

            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)addcopygrid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }

    }
    //fax grid
    protected void fax_csgrid_Load(object sender, EventArgs e)
    {
        //Credentialpage.Utility.Grid_copycharger(fax_csgrid, sv.Request_id, "Fax");
        Credentialpage.Utility.Grid_faxcopygrid(fax_csgrid, sv.Request_id);
    }
    protected void fax_csgrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.FaxCopy;
        foreach (GridViewRow row in fax_csgrid.Rows)
        {

            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)fax_csgrid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }

    }
    //Email grid
    protected void email_csgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_emailcopygrid(email_csgrid, sv.Request_id);
    }
    protected void email_csgrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = dm.EmailCopy;
        foreach (GridViewRow row in email_csgrid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Electronic Copy";

            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)email_csgrid.FooterRow.FindControl("Label11"));
            cost.Text = price.ToString();
            double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
            totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
            cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

            String str1 = totalcost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());
            total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }

    }
    //delivery grid
    protected void DeliveryGrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_DeliveryGrid(DeliveryGrid, sv.Request_id);
    }
    protected void DeliveryGrid_DataBound(object sender, EventArgs e)
    {

        foreach (GridViewRow row in DeliveryGrid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            Label count = ((Label)row.FindControl("Label2"));
            Label cost = ((Label)row.FindControl("Label1"));
            Label total = ((Label)row.FindControl("Label7"));

            if (type.Text == "Fax")
            {
                int result = (Convert.ToInt32(count.Text) * Convert.ToInt32(cost.Text));
                total.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));

            }
            else
            {
                total.Text = String.Format("{0:c}", Convert.ToDouble(cost.Text));
            }

            //display cost
            string temp = cost.Text;
            cost.Text = String.Format("{0:c}", Convert.ToDouble(temp.ToString()));

        }
        double final = 0.00;
        foreach (GridViewRow row in DeliveryGrid.Rows)
        {
            Label total = ((Label)row.FindControl("Label7"));
            Label result = ((Label)DeliveryGrid.FooterRow.FindControl("Label8"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));


        }

    }

    public void payment_refresh()
    {
        paymentgrid_Load(this, EventArgs.Empty);
        DeliveryGrid_Load(this, EventArgs.Empty);
        addcopygrid_Load(this, EventArgs.Empty);
        Payment_total_Amount();

    }
    public void Payment_total_Amount()
    {
        double sum1 = 0.00, sum2 = 0.00, sum3 = 0.00, sum4 = 0.00 , sum5 = 0.00;
        String delim = "$";
        if (DeliveryGrid.Rows.Count != 0)
        {
            Label result1 = ((Label)DeliveryGrid.FooterRow.FindControl("Label8"));
            String str1 = result1.Text;
            sum1 = Convert.ToDouble(str1.Trim(delim.ToCharArray()));

        }
        else
        {
            sum1 = 0.00;
        }
        if (paymentgrid.Rows.Count != 0)
        {
            Label result2 = ((Label)paymentgrid.FooterRow.FindControl("Label7"));
            String str2 = result2.Text;
            sum2 = Convert.ToDouble(str2.Trim(delim.ToCharArray()));

        }
        else
        {
            sum2 = 0.00;
        }
        if (addcopygrid.Rows.Count != 0)
        {
            Label result3 = ((Label)addcopygrid.FooterRow.FindControl("Label11"));
            String str3 = result3.Text;
            sum3 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));

        }
        else
        {
            sum3 = 0.00;
        }
        if (fax_csgrid.Rows.Count != 0)
        {
            Label result3 = ((Label)fax_csgrid.FooterRow.FindControl("Label11"));

            String str3 = result3.Text;
            sum4 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));
        }
        else
        {
            sum4 = 0.00;
        }
        if (email_csgrid.Rows.Count != 0)
        {
            Label result3 = ((Label)email_csgrid.FooterRow.FindControl("Label11"));
            String str3 = result3.Text;
            sum5 = Convert.ToDouble(str3.Trim(delim.ToCharArray()));
        }
        else
        {
            sum5 = 0.00;
        }


        double final = sum1 + sum2 + sum3 + sum4 + sum5;

        frm7_Amount.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));


    }
    //Continue Button
    protected void frm7_btn_Continue_Click(object sender, ImageClickEventArgs e)
    {
        //save Amount and mode  
        String delim = "$";
        double str2 = Convert.ToDouble(frm7_Amount.Text.Trim(delim.ToCharArray()));
        int str3 = Convert.ToInt32(str2);
        bool result = Credentialpage.Utility.update_Cost(frm7_payment.SelectedItem.ToString(), str3.ToString(), sv.Applicant_id.ToString());
        if (result)
        {
            Credentialpage.Utility.create_Applicantid(sv.Applicant_id);
            Credentialpage.Utility.get_Applicantid(Filenumber, sv.Applicant_id);

            //creditcard processing 
            if (frm7_payment.SelectedItem.ToString() == "credit card")
            {
              
                if (dm.IsCreditcard == 1)
                {
                    if (Subdomain == "nosubdomain")
                    {
                        Response.Redirect("~/Payment.aspx?id=" + Filenumber.Text + "&mode=1");
                    }
                    else
                    {
                        Response.Redirect("~/Payment.aspx?id=" + Filenumber.Text + "&mode=1" + "&subdomain=" + Subdomain);
                    }
                }
                else
                {
                    if (Subdomain == "nosubdomain")
                    {
                        Response.Redirect("~/msg.aspx?id=" + Filenumber.Text);
                    }
                    else
                    {  Response.Redirect("~/msg.aspx?id=" + Filenumber.Text +"&subdomain=" + Subdomain); }
                   
                }
            }
            else
            {
                if (Subdomain == "nosubdomain")
                {
                    Response.Redirect("~/msg.aspx?id=" + Filenumber.Text);
                }
                else
                { Response.Redirect("~/msg.aspx?id=" + Filenumber.Text + "&subdomain=" + Subdomain); }
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('unknown error! contact admin')</script>");
        }


    }
    #endregion
    #endregion



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















   











  
}