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

public partial class secure_View_Application : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
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

    protected void Page_Load(object sender, EventArgs e)
    {
        btnedit.PostBackUrl="~/secure/Edit_Application.aspx?tc=" +Session["Trackingcode"].ToString();  
        btnstatus.PostBackUrl = "~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString();      
        btnreport.PostBackUrl="~/secure/Report_Status.aspx?tc=" +Session["Trackingcode"].ToString();  
        btneval.PostBackUrl="~/secure/Evaluate.aspx?tc=" +Session["Trackingcode"].ToString();  
        btnattach.PostBackUrl = "~/secure/Attachments.aspx?tc=" + Session["Trackingcode"].ToString();  

        ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname,lblcompany, Session["Trackingcode"].ToString());

        #region section
       ClientAdmin.Utility.SectionAttributes sa = ClientAdmin.Utility.section(ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString()));
       
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
        else {
            senderblock.Visible = false;
        }
        #endregion
    } 
  
    protected void DetailsView_personalinfo_Load(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.DetailsView_Applview(DetailsView_personalinfo, DetailsView_purpose, DetailsView_payment, Session["Trackingcode"].ToString(), hischoolgrid, univgrid, service1grid, addongrid, copychargergrid, Convert.ToInt32(ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString())), deliverydetails, fax_grid, fax_details, email_grid, email_details,DetailsView_Sender);
                ClientAdmin.Utility.Grid_applicantNotesBrowse(Grid_applicantNotes, Session["Trackingcode"].ToString(), "Client");
                break;
            case "ADMIN":             
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }        
        Review_total_Amount();
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
}
