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
using System.Data.SqlClient;

public partial class _Recovery : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
      
    }       

    protected void Page_Load(object sender, EventArgs e)
    {      
      
    } 
  
    protected void DetailsView_personalinfo_Load(object sender, EventArgs e)
    {
      
      
    }
    protected void DetailsView_personalinfo_DataBound(object sender, EventArgs e)
    {
      
    }
    protected void hischoolgrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in hischoolgrid.Rows)
        {
            Label graduation = ((Label)row.FindControl("grad"));
            if (graduation.Text.ToString() == "Null")
            {
                graduation.Text = "-Nill-";
            }
        }

    }
    protected void univgrid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in univgrid.Rows)
        {
            Label graduation = ((Label)row.FindControl("grad"));
            if (graduation.Text.ToString() == "Null")
            {
                graduation.Text = "-Nill-";
            }
        }

    }   
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

    }

    protected void DetailsView_purpose_DataBound(object sender, EventArgs e)
    {
        Label evalname = ((Label)DetailsView_purpose.FindControl("Eval_Name"));

        switch (evalname.Text)
        {
            case "Admission to High School":
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[6].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
               
                break;
            case "Admission to College/University":
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[6].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
               
                break;
            case "Employment":
                DetailsView_purpose.Fields[1].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[6].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
                break;
            case "Immigration":
                DetailsView_purpose.Fields[1].Visible = false;
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[6].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
                break;
            case "Professional Licensing/Registration":
                DetailsView_purpose.Fields[1].Visible = false;
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;             
                DetailsView_purpose.Fields[6].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
                break;
            case "Military":
                DetailsView_purpose.Fields[1].Visible = false;
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;             
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[7].Visible = false;
                break;
            case "Other":
                DetailsView_purpose.Fields[1].Visible = false;
                DetailsView_purpose.Fields[2].Visible = false;
                DetailsView_purpose.Fields[3].Visible = false;
                DetailsView_purpose.Fields[4].Visible = false;
                DetailsView_purpose.Fields[5].Visible = false;
                DetailsView_purpose.Fields[6].Visible = false;
                break;
        }

        

    }
    protected void DetailsView_payment_DataBound(object sender, EventArgs e)
    {
        Label pymode = ((Label)DetailsView_payment.FindControl("pymode"));
        if (pymode.Text != "credit card")
        {
            DetailsView_payment.Fields[1].Visible = false;
            DetailsView_payment.Fields[2].Visible = false;
            DetailsView_payment.Fields[3].Visible = false;

        }
    }
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
    protected void deliverydetails_DataBound(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in deliverydetails.Rows)
        {
            Label lbltype = (Label)row.FindControl("lbltype");
           // Label lblcopy = (Label)row.FindControl("lblCount");

            if (lbltype.Text == "Free copy")
            {
                count = count + 1;
                row.Visible = false;
            }
        }

        foreach (GridViewRow row in deliverydetails.Rows)
        {
            Label lblcopy = (Label)row.FindControl("lblcopy");
            Label lblCount = (Label)row.FindControl("lblCount");

            if (lblcopy.Text == "primary")
            {
                if(count >0)
                {
                    lblCount.Text = (Convert.ToInt32(lblCount.Text) + count).ToString();
                }
            }
        }
        
    }
    public struct SectionAttributes
    {
        public bool AddSection;
        public bool FaxSection;
        public bool EmailSection;
        public int AppType;
    }
    public static SectionAttributes section(string Clientid)
    {

        SectionAttributes sa = new SectionAttributes();
        string Appsettingquery = "Select * from [cc_masterdataset].[customersettings] where [Customer_Id]=" + Clientid;
        DataSet ds = new DataSet();
        ds = GetDataSet(Appsettingquery);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sa.AddSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Additional_Section"].ToString());
            sa.FaxSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Fax_Section"].ToString());
            sa.EmailSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Email_Section"].ToString());
            sa.AppType = Convert.ToInt32(ds.Tables[0].Rows[0]["Application_Type"].ToString());
        }
        else
        {
            sa.AddSection = false;
            sa.FaxSection = false;
            sa.EmailSection = false;
            sa.AppType = 1;
        }
        return sa;
    }
    public static void Get_applicantinfo(Label fileno, Label name, Label company, string Filenumber)
    {
        string strSQL = "SELECT cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name,cc_clientdataset.applicant.InternalFileNumber,cc_clientdataset.applicant.Customer_Id FROM cc_clientdataset.applicant where FileNumber='" + Filenumber + "'";
        DataSet ds = GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            fileno.Text = ds.Tables[0].Rows[0]["FileNumber"].ToString();
            name.Text = ds.Tables[0].Rows[0]["Name"].ToString();

            strSQL = "SELECT [Name] FROM [cc_masterdataset].[customer] where id=" + ds.Tables[0].Rows[0]["Customer_Id"].ToString();
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                company.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
        }

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        Get_applicantinfo(lblfileno, lblname, lblcompany, txtFileno.Text);

        #region section
        SectionAttributes sa = section(clientidbyFilenumber(txtFileno.Text));

        if (!sa.AddSection)
        {
            revsec_Additional.Visible = false;
        }
        if (!sa.FaxSection)
        {
            revsec_Fax.Visible = false;
            fax_detail.Visible = false;
        }
        if (!sa.EmailSection)
        {
            revsec_Email.Visible = false;
            email_detail.Visible = false;
        }
        if (sa.AppType == 2)
        {
            senderblock.Visible = true;
        }
        else
        {
            senderblock.Visible = false;
        }
        #endregion

        DetailsView_Applview(DetailsView_personalinfo, DetailsView_purpose, DetailsView_payment, txtFileno.Text, hischoolgrid, univgrid, service1grid, addongrid, copychargergrid, Convert.ToInt32(ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString())), deliverydetails, fax_grid, fax_details, email_grid, email_details, DetailsView_Sender);
        Grid_applicantNotesBrowse(Grid_applicantNotes, txtFileno.Text, "Client");

        Review_total_Amount();
    }

    public static string clientidbyFilenumber(string fileno)
    {
        string clientid = "0";
        string query = "Select Customer_Id from [cc_clientdataset].[applicant] where [FileNumber]='" + fileno + "'";
        DataSet ds = new DataSet();
        ds = GetDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            clientid = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
        }
        return clientid;
    }
    public static void Grid_applicantNotesBrowse(GridView commongrid, string Applicant_id, string type)
    {

        string strSQL = "SELECT * from cc_masterdataset.applicantnotes where Applicant_id = (SELECT [id] FROM [cc_clientdataset].[applicant] where FileNumber='" + Applicant_id + "') AND Type='" + type + "' ORDER BY id desc";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

    }
    public static void DetailsView_Applview(DetailsView personalinfo, DetailsView purpose, DetailsView payment, string FileNumber, GridView hischool, GridView college, GridView service1grid, GridView addongrid, GridView copychargergrid, int Customer_Id, GridView deliverydetails, GridView fax_grid, GridView fax_details, GridView Email_grid, GridView email_details, DetailsView senderinfo)
    {
        string strSQL = "SELECT cc_clientdataset.applicant.FirstName, cc_clientdataset.applicant.MiddleName, cc_clientdataset.applicant.LastName,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.Gender, cc_clientdataset.applicant.otherFirstName+' '+cc_clientdataset.applicant.otherMiddleName+' '+cc_clientdataset.applicant.otherLastName AS othername, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City,cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.PreviousCredential_id, countries.Name AS country,countries_1.Name AS CountryBirth, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Zip_or_PostalCode,cc_clientdataset.applicant.Paymentmode,cc_clientdataset.applicant.Paymentstatus,cc_clientdataset.applicant.Authorizecode,cc_clientdataset.applicant.Transactioncode, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email,cc_clientdataset.evaluation_purpose.Evaluation_Name, cc_clientdataset.evaluation_request.[Eval_institution],cc_clientdataset.evaluation_request.[Eval_organization],cc_clientdataset.evaluation_request.[Eval_Attorney],cc_clientdataset.evaluation_request.[Eval_Board],cc_clientdataset.evaluation_request.[Eval_State],cc_clientdataset.evaluation_request.[Eval_Military_Recruiter],cc_clientdataset.evaluation_request.[Eval_other],cc_clientdataset.evaluation_request.[Senders_Name],cc_clientdataset.evaluation_request.[Senders_Contact],cc_clientdataset.evaluation_request.[Purpose_Notes],cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id LEFT JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id INNER JOIN cc_masterdataset.countries countries_1 ON cc_clientdataset.applicant.Countryofbirth = countries_1.ID where cc_clientdataset.applicant.FileNumber='" + FileNumber + "'";
        DataSet ds = GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            personalinfo.DataSource = ds;
            personalinfo.DataBind();
            purpose.DataSource = ds;
            purpose.DataBind();

            senderinfo.DataSource = ds;
            senderinfo.DataBind();

            payment.DataSource = ds;
            payment.DataBind();

            Grid_hischoolgrid(hischool, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
            Grid_univgrid(college, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));

            Grid_service1grid(service1grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
            Grid_copycharger(copychargergrid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Additional", Customer_Id);
            Grid_addonsservice(addongrid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
            Grid_Fax(fax_grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Fax", Customer_Id);
            Grid_Email(Email_grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Email", Customer_Id);
            Grid_deliveryinfo(deliverydetails, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
            Grid_faxgrid(fax_details, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
            Grid_emailgrid(email_details, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
        }
    }
    public static void Grid_hischoolgrid(GridView commongrid, int Request_Id)
    {
        string strSQL = "SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();
    }
    public static void Grid_univgrid(GridView commongrid, int Request_Id)
    {

        string strSQL = " SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University')";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();
    }
    public static void Grid_service1grid(GridView commongrid, int Request_Id)
    {
        // string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") ORDER BY cc_clientdataset.evaluation_services.id";
        string strSQL = " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation')) " +
                      " union " +
                      " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional')) " +
                      " union " +
                      " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
                            " union " +
                        " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
                      "order by cost desc";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

        //cost  
        double final = 0.00;
        foreach (GridViewRow row in commongrid.Rows)
        {
            Label total = ((Label)row.FindControl("Label1"));
            Label result = ((Label)commongrid.FooterRow.FindControl("Label7"));
            String str1 = total.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
        }


    }
    public static void Grid_copycharger(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
    {
        string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

        //cost
        double final = 0.00;
        int price = getAdditional(Customer_Id);
        foreach (GridViewRow row in commongrid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Hard Copy";
            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
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
    public static void Grid_addonsservice(GridView commongrid, int Request_Id)
    {
        // string strSQL = "SELECT SUM(cc_clientdataset.evaluation_delivery.Count) AS Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') GROUP BY cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type HAVING ((COUNT(cc_clientdataset.evaluation_delivery.Name) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.City) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Zip_or_PostalCode) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0))";
        // string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional')";
        string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') AND (cc_clientdataset.evaluation_delivery.id not in(" +
        "SELECT ed2.id  FROM [cc_clientdataset].[evaluation_delivery] as ed1,[cc_clientdataset].[evaluation_delivery] as ed2 " +
        "where ed1.Addressline1= ed2.Addressline1 and ed1.Addressline2=ed2.Addressline2 and ed1.City=ed2.City and ed1.State_or_Province=ed2.State_or_Province and " +
        "ed1.Zip_or_PostalCode=ed2.Zip_or_PostalCode and ed1.Evaluation_Request_Id=ed2.Evaluation_Request_Id and ed1.Country_Id = ed2.Country_Id and ed1.CopyNo='primary' and ed2.CopyNo ='Additional' and ed1.Evaluation_Request_Id=" + Request_Id + "))";

        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

        //cost
        double final = 0.00;
        foreach (GridViewRow row in commongrid.Rows)
        {
            //cost mulitiply
            Label type = ((Label)row.FindControl("lbldeliveryname"));
            Label count = ((Label)row.FindControl("Label4"));
            Label cost = ((Label)row.FindControl("Label2"));

            if (type.Text == "Fax")
            {
                int result = (Convert.ToInt32(count.Text) * Convert.ToInt32(cost.Text));
                cost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));

            }
            else
            {
                cost.Text = String.Format("{0:c}", Convert.ToDouble(cost.Text));
            }

            //total cost            
            Label footer = ((Label)commongrid.FooterRow.FindControl("Label7"));
            String str1 = cost.Text;
            String delim = "$";
            String str2 = str1.Trim(delim.ToCharArray());

            final = final + Convert.ToDouble(str2.ToString());

            footer.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));

        }
    }
    public static void Grid_Fax(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
    {
        string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

        double final = 0.00;
        int price = getFaxCost(Customer_Id);
        foreach (GridViewRow row in commongrid.Rows)
        {
            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
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
    public static void Grid_Email(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
    {
        string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();

        double final = 0.00;
        int price = getEmailCost(Customer_Id);
        foreach (GridViewRow row in commongrid.Rows)
        {
            Label type = ((Label)row.FindControl("Label3"));
            type.Text = "Official Electronic Copy";
            Label cost = ((Label)row.FindControl("Label9"));
            Label count = ((Label)row.FindControl("Label1"));
            Label totalcost = ((Label)row.FindControl("Label10"));
            Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
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
    public static int getFaxCost(int Customer_id)
    {
        int cost = 0;
        string query = "SELECT Cost FROM cc_clientdataset.delivery_type WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Fax'))";
        DataSet ds = GetDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
        }
        return cost;
    }
    public static int getEmailCost(int Customer_id)
    {
        int cost = 0;
        string query = "SELECT Cost FROM cc_clientdataset.delivery_type WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Email'))";
        DataSet ds = GetDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
        }
        return cost;
    }
    public static void Grid_deliveryinfo(GridView commongrid, int request_Id)
    {
        //string strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count)as Count,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1 , cc_clientdataset.evaluation_delivery.Addressline2, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =  " + request_Id + ")AND(Type <> 'Fax')) GROUP BY cc_masterdataset.countries.Name,cc_clientdataset.evaluation_delivery.Type,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
        string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Optional_InstitutionName,cc_clientdataset.evaluation_delivery.Count,cc_clientdataset.evaluation_delivery.CopyNo,cc_clientdataset.delivery_type.Name as deliveryservice,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1 , cc_clientdataset.evaluation_delivery.Addressline2, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.delivery_type on cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id  where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + request_Id + ")AND(cc_clientdataset.evaluation_delivery.Type <> 'Fax'))";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();
    }
    public static void Grid_faxgrid(GridView commongrid, int Request_Id)
    {
        string strSQL = "SELECT cast(count(*)as char)as Count, Name,Faxno,Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Fax')) GROUP BY Type, cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Faxno HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Faxno) > 0 ))";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();
    }
    public static void Grid_emailgrid(GridView commongrid, int Request_Id)
    {
        string strSQL = "SELECT cast(count(*)as char)as Count, Name,Email,Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Email')) GROUP BY Type, cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Email HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Email) > 0 ))";
        DataSet ds = GetDataSet(strSQL);
        commongrid.DataSource = ds;
        commongrid.DataBind();
    }
    public static int getAdditional(int Customer_id)
    {
        int cost = 0;
        string query = "SELECT Cost FROM cc_clientdataset.service WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Additional Copy'))";
        DataSet ds = GetDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
        }
        return cost;
    }

    public static string DBConnectionString()
    {
        string strConn = ConfigurationManager.ConnectionStrings["RecoveryConnectionString"].ConnectionString;
        return strConn;
    }
    private static DataSet GetDataSet(string strSQL)
    {

        SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
        DataSet ds = new DataSet();
        sdpPrd.Fill(ds);
        return ds;
    }

}
