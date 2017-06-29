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
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Net;


public partial class Printapplication : System.Web.UI.Page
{
    int clientid = 0;
    string Subdomain = "";
    protected void Page_Load(object sender, EventArgs e)
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
 
        Session["Applicant_id"] = 0;
        Session["Request_id"] = 0;

            Authentication.Utility.DomainAttributes dm = Authentication.Utility.GetClient(Request.Url, Subdomain);

            if (dm.IsMultidomain)
            {
                Page.Title = dm.SubDmName; OrgTitle.InnerHtml = dm.SubDmName;
                clientid = dm.SubDmID;
            }
            else
            {
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName;
                clientid = dm.DmID;
            }


        string identifier = Request.QueryString["id"].ToString();

        if (identifier.Contains("|"))
        {
            string[] Query_spliter;
            Query_spliter = identifier.Split('|');

                string result = ClientAdmin.Utility.check_filenumber(Query_spliter[1].ToString(), clientid.ToString());
             if (result == "Access_Denied")
             {
                 Response.Redirect("~/Logout_Print.aspx");
             }
             else
             {
                 DataSet ds = Credentialpage.Utility.check_print(Query_spliter[1].ToString(), Query_spliter[0].ToString());
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     Session["Applicant_id"] = ds.Tables[0].Rows[0]["applicantid"].ToString();
                     Session["Request_id"] = ds.Tables[0].Rows[0]["id"].ToString();
                 }
             }

        }        
    
            if ((dm.IsCreditcard.ToString() == "0") && (dm.Onlinecc.ToString() == "0"))
            {
                creditblog.Visible = false;
    }
            else
    {
                creditblog.Visible = true;
            }
       
            #region section
            if (!dm.AddSection)
        {
                Revsec_Additionalheader.Visible = false;
                Revsec_Additional.Visible = false;
            }
            if (!dm.FaxSection)
            {
                Revsec_Faxheader.Visible = false;
                Revsec_Fax.Visible = false;
        }
            if (!dm.EmailSection)
        {
                Revsec_Emailheader.Visible = false;
                Revsec_Email.Visible = false;
        }
            #endregion
            
        refresh_Click(this,EventArgs.Empty);  

            switch (dm.App_Type)
            {
               
                case 3:                 
                    edu.Visible = false;
                    break;
                default :
                    edu.Visible = true;
                    break;
            }     
    }

       
       
     
    }
          //Applicant
    protected void Applicant_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_Applicant(Applicant,Convert.ToInt32(Session["Applicant_id"].ToString()));
        Credentialpage.Utility.Grid_payment(Convert.ToInt32(Session["Applicant_id"].ToString()), pymode, Ctype,Tyblk);
    }
    protected void Applicant_DataBound(object sender, EventArgs e)
    {
        if(Applicant.Rows.Count !=0)
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
    protected void hischoolgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_hischoolgrid(hischoolgrid, Convert.ToInt32(Session["Request_id"].ToString()));
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
    //univgrid
    protected void univgrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_univgrid(univgrid, Convert.ToInt32(Session["Request_id"].ToString()));
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
  
    //purposegrid
    protected void purposegrid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_purposegrid(purposegrid,Convert.ToInt32(Session["Applicant_id"].ToString()));             
    }
    protected void purposegrid_DataBound(object sender, EventArgs e)
    {
        //Label evalname = ((Label)purposegrid.FindControl("Eval_Name"));
        //    if (evalname.Text != "Education")
        //    {
        //        purposegrid.Fields[2].Visible = false;
        //    }           
  
    }    

    //delivery report
    protected void deliverydetails_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.deliveryinfo(deliverydetails, Convert.ToInt32(Session["Request_id"].ToString()));      
    }
       


    //general service
    protected void service1grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_service1gridprint(service1grid, Convert.ToInt32(Session["Request_id"].ToString()));
    }
    protected void service1grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        foreach (GridViewRow row in service1grid.Rows)
        {

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
        Credentialpage.Utility.Grid_DeliveryGrid(Delivery_Grid, Convert.ToInt32(Session["Request_id"].ToString()));
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
        Credentialpage.Utility.Grid_copycharger(copychargergrid, Convert.ToInt32(Session["Request_id"].ToString()), "Additional");
    }
    protected void copychargergrid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = Credentialpage.Utility.getAdditional(Convert.ToInt32(clientid.ToString()));
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

    //email grid
    protected void email_grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_copycharger(email_grid, Convert.ToInt32(Session["Request_id"].ToString()), "Email");
    }
    protected void email_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = Credentialpage.Utility.getEmailCost(Convert.ToInt32(clientid.ToString()));
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

    //fax grid
    protected void fax_grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_copycharger(fax_grid, Convert.ToInt32(Session["Request_id"].ToString()), "Fax");
    }
    protected void fax_grid_DataBound(object sender, EventArgs e)
    {
        double final = 0.00;
        int price = Credentialpage.Utility.getFaxCost(Convert.ToInt32(clientid.ToString()));
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

    //review refresh
    protected void refresh_Click(object sender, EventArgs e)
    {
        hischoolgrid_Load(this, EventArgs.Empty);
        univgrid_Load(this, EventArgs.Empty);
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
    protected void fax_details_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.Grid_faxgrid(fax_details, Convert.ToInt32(Session["Request_id"].ToString()));
    }

}

