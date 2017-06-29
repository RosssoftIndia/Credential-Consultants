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

public partial class secure_Edit_Application : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        validate(); 
    }       
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Customer_id"] = ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString());  

        btnview.PostBackUrl = "~/secure/View_Application.aspx?tc=" + Session["Trackingcode"].ToString();      
        btnstatus.PostBackUrl = "~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btnreport.PostBackUrl = "~/secure/Report_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btneval.PostBackUrl = "~/secure/Evaluate.aspx?tc=" + Session["Trackingcode"].ToString();  
        btnattach.PostBackUrl = "~/secure/Attachments.aspx?tc=" + Session["Trackingcode"].ToString();  


        if (!Page.IsPostBack)
        {
            ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname, lblcompany, Session["Trackingcode"].ToString());      
            Page_Control_Initialization();
            Tabrefresh("All");
            
        }

        #region section
        ClientAdmin.Utility.SectionAttributes sa = ClientAdmin.Utility.section(ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString()));

        if (!sa.AddSection)
        {
            Editsecadditional.Visible = false;
        }
        if (!sa.FaxSection)
        {
            Editsecfax.Visible = false;            
        }
        if (!sa.EmailSection)
        {
            Editsecemail.Visible = false;  
        }
        #endregion
        //links

        // Service tab
        //service
        servicegridEdit.HRef = "Popup_Service.aspx?id=1&cid=" + Session["Customer_id"].ToString();
        servicegridAdd.HRef = "Popup_Service.aspx?id=0&cid=" + Session["Customer_id"].ToString();
        //primary address
        primaryAdd.HRef = "Popup_Primaryaddress.aspx?id=0&cid=" + Session["Customer_id"].ToString();
        primaryEdit.HRef = "Popup_Primaryaddress.aspx?id=1&cid=" + Session["Customer_id"].ToString(); 
        //evaluation
        EvaluationAdd.HRef = "Popup_Evaladdress.aspx?id=0&cid=" + Session["Customer_id"].ToString();
        EvaluationEdit.HRef = "Popup_Evaladdress.aspx?id=1&cid=" + Session["Customer_id"].ToString(); 
        //aditional
        AdditionalAdd.HRef = "Popup_Additionalcopy.aspx?id=0&cid=" + Session["Customer_id"].ToString();
        //fax
        FaxAdd.HRef = "Popup_Fax.aspx?id=0&cid=" + Session["Customer_id"].ToString();
        //email
        EmailAdd.HRef = "Popup_Email.aspx?id=0&cid=" + Session["Customer_id"].ToString(); 
    }
 



    #region Tabs
    private void Page_Control_Initialization()
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        #region personalinfo
        ClientAdmin.Utility.Getmonth(frm1_option_month);
        ClientAdmin.Utility.Getdate(frm1_option_date);
        ClientAdmin.Utility.Getyear(frm1_option_year, Convert.ToInt32(app.Startyear), Convert.ToInt32(app.Endyear));
        ClientAdmin.Utility.Getcountry(frm1_option_country);
        ClientAdmin.Utility.Getcountry(frm1_Country_birth);
        #endregion       

        #region DefaultTabLoad
        nav1holder.Attributes.Add("class", "current");
        personalinfotab.Visible = true;       
        #endregion

    }
    private bool GetappInfo(string Filenumber)
    {
        bool result = false;
        DataSet ds = ClientAdmin.Utility.GetAppinfo(Filenumber);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["Applicant_id"] = ds.Tables[0].Rows[0]["Applicant_Id"].ToString();
            Session["Request_id"] = ds.Tables[0].Rows[0]["id"].ToString();
            result = true;
        }
        return result;
    }
    private void LoadPersonalinfoTab(string ApplicationId)
    {
        DataSet ds = ClientAdmin.Utility.Edit_application(ApplicationId);
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
    private void LoadPurposeTab(string ApplicationId)
                {
        DataSet ds = ClientAdmin.Utility.Purpose_application(ApplicationId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblstatecaption.Visible = false;
           txtstate.Visible = false;
           txtpurpose.Text = ClientAdmin.Utility.GetpurposeName(ds.Tables[0].Rows[0]["Purpose_Id"].ToString());
           switch (txtpurpose.Text)
                    {
                case "Admission to High School":
                    lblcaption.Text = "Which educational institution referred you to us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_institution"].ToString();
                    break;
                case "Admission to College/University":
                    lblcaption.Text = "Which educational institution referred you to us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_institution"].ToString();
                    break;
                case "Employment":
                    lblcaption.Text = "Which employer referred you to us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_organization"].ToString();
                    break;
                case "Immigration":
                    lblcaption.Text = "Which Attorney or Law firm referred you to us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_Attorney"].ToString();
                    break;
                case "Professional Licensing/Registration":
                    lblcaption.Text = "Name of Board from which you seek licensing:";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_Board"].ToString();
                    txtstate.Text = ds.Tables[0].Rows[0]["Eval_State"].ToString();
                    lblstatecaption.Visible = true;
                    txtstate.Visible = true;
                    break;
                case "Military":
                    lblcaption.Text = "Military Recruiter referred you to us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_Military_Recruiter"].ToString();
                    break;
                case "Other":
                    lblcaption.Text = "How did you hear about us?";
                    txtcontent.Text = ds.Tables[0].Rows[0]["Eval_other"].ToString();
                    break;
                    }

        }

    }
    private void LoadHighschooltab(string RequestId)
    { ClientAdmin.Utility.Grid_hischoolgrid(hischoolgrid, RequestId); }
    private void LoadUniversitytab(string RequestId)
    { ClientAdmin.Utility.Grid_univgrid(univgrid, RequestId); }
    private void LoadServicetab(string RequestId)
    {
        //service
        ClientAdmin.Utility.Grid_servicegrid(servicegrid, RequestId); if (servicegrid.Rows.Count > 0) { servicegridEdit.Visible = true; servicegridAdd.Visible = false; } else { servicegridEdit.Visible = false; servicegridAdd.Visible = true; } 
    
        //primary address
        ClientAdmin.Utility.displayprimary(frm5_primarygrid, Session["Request_id"].ToString()); if (frm5_primarygrid.DataItemCount > 0) { primaryEdit.Visible = true; primaryAdd.Visible = false; service_EvaluationEditmenu.Visible = true; service_EvaluationEditmsg.Visible = false; service_additionlmenu.Visible = true; service_additionlmsg.Visible = false; } else { primaryEdit.Visible = false; primaryAdd.Visible = true; service_EvaluationEditmenu.Visible = false; service_EvaluationEditmsg.Visible = true; service_additionlmenu.Visible = false; service_additionlmsg.Visible = true; }
        //Official Hard Copy Delivery
        ClientAdmin.Utility.evaluationAddress(deliveryaddressgrid, Session["Request_id"].ToString()); if (deliveryaddressgrid.Rows.Count > 0) { EvaluationEdit.Visible = true; EvaluationAdd.Visible = false; } else { EvaluationEdit.Visible = false; EvaluationAdd.Visible = true; }
        //additional
        ClientAdmin.Utility.Grid_copycharger(copychargergrid, Convert.ToInt32(Session["Request_id"].ToString()), "Additional"); 

       //fax
        ClientAdmin.Utility.faxgrid_display(fax_grid, Convert.ToInt32(Session["Request_id"].ToString()), "Fax");

        //email
        ClientAdmin.Utility.emailgrid_display(email_grid, Convert.ToInt32(Session["Request_id"].ToString()), "Email");

        //totalcost
        ClientAdmin.Utility.Grid_addonsservice(addongrid, Convert.ToInt32(RequestId));
        Review_total_Amount();

                }
    ////private void LoadReviewtab(string RequestId)
    ////{
    ////    //ClientAdmin.Utility.Grid_service1grid(service1grid, Convert.ToInt32(RequestId));
    ////    //ClientAdmin.Utility.Grid_copycharger(copychargergrid_display, Convert.ToInt32(RequestId), "Additional",Convert.ToInt32(Session["Admin_Customer"].ToString()));
                
    ////    //ClientAdmin.Utility.Grid_Fax(fax_grid_display, Convert.ToInt32(RequestId), "Fax", Convert.ToInt32(Session["Admin_Customer"].ToString()));
       
    ////}



    #endregion

    #region Trigger

    #region TabRefresh
    protected void Tabrefresh(string mode)
    {
        bool result = GetappInfo(Session["Trackingcode"].ToString());
        if (result)
        {
            switch(mode)
            {
                case "All":
                    LoadUrl(Session["Applicant_id"].ToString());
                    LoadPersonalinfoTab(Session["Applicant_id"].ToString());
                    LoadPurposeTab(Session["Applicant_id"].ToString());
                    LoadHighschooltab(Session["Request_id"].ToString());
                    LoadUniversitytab(Session["Request_id"].ToString());
                    LoadServicetab(Session["Request_id"].ToString());
                   // LoadReviewtab(Session["Request_id"].ToString());
                break;
                case "Personalinfo":
                    LoadPersonalinfoTab(Session["Applicant_id"].ToString());
                    break;
                case "Purpose":
                    LoadPurposeTab(Session["Applicant_id"].ToString());
                break;
                case "Education":
                    LoadHighschooltab(Session["Request_id"].ToString());
                    LoadUniversitytab(Session["Request_id"].ToString());
                    break;
                case "Service":
                    LoadServicetab(Session["Request_id"].ToString());
                break;
                ////case "Review":
                ////    LoadReviewtab(Session["Request_id"].ToString());
                ////    break;


            }
          
        }
    }
    #endregion

    #region Refresh
    protected void refresh_Click(object sender, EventArgs e)
    {
        Tabrefresh("All");
    }
    #endregion

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
    #endregion   

    #region Education tab
    #region Highschool tab
    protected void hischoolgrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in hischoolgrid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = ((Label)row.FindControl("Label5"));
                link.NavigateUrl = "~/secure/Popup_Editschool.aspx?id=" + id.Text;
                link.ToolTip = "Credential Consultant :: Edit Block.";
                link.CssClass = "iframe";
               // link.Attributes.Add("rel", "iframe");
                //link.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");

                ImageButton l = (ImageButton)row.FindControl("hischoolgrid_del");
                l.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this HighSchool record?')");

            }
       }
        
    }
    protected void hischoolgrid_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label5");
        int id = Convert.ToInt32(id_control.Text.ToString());
        ClientAdmin.Utility.delete_Applicant_Education_History(id);
        refresh_Click(this, EventArgs.Empty);
    }
    #endregion

    #region univ tab
    protected void univgrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in univgrid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
        {
                HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = ((Label)row.FindControl("Label1"));
                link.NavigateUrl = "~/secure/Popup_Edituniversity.aspx?id=" + id.Text;
                link.ToolTip = "Credential Consultant :: Edit Block.";
                link.CssClass = "iframe";
               // link.Attributes.Add("rel", "iframe");
                //link.Attributes.Add("onClick", "ShowProcessMessage('ProcessingWindow')");

                ImageButton l = (ImageButton)row.FindControl("univgrid_del");
                l.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this University record?')");

            }
        }



    }
    protected void univgrid_del_Click(object sender, ImageClickEventArgs e)
            {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("Label1");
        int id = Convert.ToInt32(id_control.Text.ToString());
        ClientAdmin.Utility.delete_Applicant_Education_History(id);
        refresh_Click(this, EventArgs.Empty);
            }
    #endregion
    #endregion

    #region service
       //service
    protected void servicegrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        foreach (GridViewRow row in servicegrid.Rows)
        {         

            Label total = ((Label)row.FindControl("Label1"));
            Label result = ((Label)servicegrid.FooterRow.FindControl("Label7"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
            
        }


    }

    //additional    
    protected void copychargergrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = ClientAdmin.Utility.getAdditional(Convert.ToInt32(Session["Customer_id"].ToString()));
        foreach (GridViewRow row in copychargergrid.Rows)
    {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = (Label)row.FindControl("Label2");
                if (frm5_primarygrid.DataItemCount > 0)
        {
                    link.NavigateUrl = "~/secure/Popup_Additionalcopy.aspx?id=1&rowid=" + id.Text + "&cid=" + Session["Customer_id"].ToString();
        }
                else { link.NavigateUrl = "#"; }
                link.ToolTip = "Credential Consultant :: Edit Block.";
                link.CssClass = "iframe";
               // link.Attributes.Add("rel", "iframe");

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
        ClientAdmin.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
    }

   //fax
    protected void fax_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = Credentialpage.Utility.getFaxCost(Convert.ToInt32(Session["Customer_id"].ToString()));
        foreach (GridViewRow row in fax_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
    {        
                HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = (Label)row.FindControl("Label2");
                link.NavigateUrl = "~/secure/Popup_Fax.aspx?id=1&rowid=" + id.Text;
                link.ToolTip = "Credential Consultant :: Edit Block.";
                link.CssClass = "iframe";
                //link.Attributes.Add("rel", "iframe");

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
        ClientAdmin.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
    }


    //email
    protected void email_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = Credentialpage.Utility.getEmailCost(Convert.ToInt32(Session["Customer_id"].ToString()));
        foreach (GridViewRow row in email_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = ((HyperLink)row.FindControl("HyperLink1"));
                Label id = (Label)row.FindControl("Label2");
                link.NavigateUrl = "~/secure/Popup_Email.aspx?id=1&rowid=" + id.Text;
                link.ToolTip = "Credential Consultant :: Edit Block.";
                link.CssClass = "iframe";
                //link.Attributes.Add("rel", "iframe");

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
        ClientAdmin.Utility.delete_Evaluation_Delivery(id);
        refresh_Click(this, EventArgs.Empty);
       
    }
    #endregion


   

    #endregion

    #region Navigation function
    protected void nav_Click(object sender, EventArgs e)
    {
          validate(); 
          pagereset();
            LinkButton link = (LinkButton)sender;
            switch (link.ID)
         {
                case "nav1":
                    Tabrefresh("Personalinfo");
                    nav1holder.Attributes.Add("class", "current");
                    personalinfotab.Visible = true; 
                    break;
                case "nav2":
                    Tabrefresh("Purpose");
                    nav2holder.Attributes.Add("class", "current");
                    purposetab.Visible = true; 
                 break;
                case "nav3":
                    Tabrefresh("Education");
                    nav3holder.Attributes.Add("class", "current");
                    secondarytab.Visible = true; 
                 break;
                case "nav4":
                    Tabrefresh("Service");
                    nav4holder.Attributes.Add("class", "current");
                    servicetab.Visible = true;
                 break;
                //case "nav5":
                //    Tabrefresh("Review");
                //    nav5holder.Attributes.Add("class", "current");
                //    ReviewTab.Visible = true;
                //    break;
         }
       
       
    }     
    protected void pagereset()
        {
        //tab visibility
        personalinfotab.Visible = false;
        purposetab.Visible = false;
        secondarytab.Visible = false;
        servicetab.Visible = false;
       // ReviewTab.Visible = false;      

        //navigation reset
        nav1holder.Attributes.Remove("class");
        nav2holder.Attributes.Remove("class");
        nav3holder.Attributes.Remove("class");
        nav4holder.Attributes.Remove("class");
//nav5holder.Attributes.Remove("class");

    }
    protected void LoadUrl(string ApplicationId)
    {
        personalinfoedit.HRef = "Popup_Editpersonalinfo.aspx?id=" + ApplicationId.ToString(); 
        purposeedit.HRef = "Popup_Editpurpose.aspx?id=" + ApplicationId.ToString();
    }
    protected void validate()
        {
        switch (Session["Authenticate"].ToString())
            {
            case "Approved":
                Session["Trackingcode"] = Request.QueryString["Tc"];
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
    }
    public void Review_total_Amount()
    {
        double sum1 = 0.00, sum2 = 0.00, sum3 = 0.00, sum4 = 0.00, sum5 = 0.00;
        String delim = "$";
        if (servicegrid.Rows.Count != 0)
        {
            Label result1 = ((Label)servicegrid.FooterRow.FindControl("Label7"));
            String str1 = result1.Text;
            sum1 = Convert.ToDouble(str1.Trim(delim.ToCharArray()));
        }
        else
        {
            sum1 = 0.00;
        }
        if (addongrid.Rows.Count != 0)
        {
            Label result2 = ((Label)addongrid.FooterRow.FindControl("Label7"));
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
        updateCost(final.ToString());
    }
    public void updateCost(string cost)
    {
        ClientAdmin.Utility.update_Totalcost(Session["Applicant_id"].ToString(),cost);
    }
    #endregion



   
    protected void addongrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in addongrid.Rows)
        {
            Label lbldeliveryname = (Label)row.FindControl("lbldeliveryname");
            if (lbldeliveryname.Text == "Free copy")
            {
                row.Visible = false;
            }
        }
    }
}
