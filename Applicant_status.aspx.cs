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

public partial class Applicant_status : System.Web.UI.Page
{
    protected void page_init(object sender, EventArgs e)
    {

    }
    int count = 8;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["file"].ToString() != "Empty")
        {
            string filenumber = Session["file"].ToString();           
            DataSet ds = ClientAdmin.Utility.Status_View(filenumber.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                int[] holder = new int[8];

                if (ds.Tables[0].Rows[0]["Application_Recieved"].ToString() == "1")
                {
                    img1.Visible = true;
                    blk1.Visible = false;
                    holder[0] = 1;
                }
                else
                {
                    blk1.Visible = true; 
                    img1.Visible = false;
                    holder[0] = 0;
                }

                if (ds.Tables[0].Rows[0]["Documents_Recieved"].ToString() == "1")
                {
                    blk2.Visible = false;
                    img2.Visible = true;
                    holder[1] = 1;
                }
                else
                {
                    blk2.Visible = true; 
                    img2.Visible = false;
                    holder[1] = 0;
                }

                if (ds.Tables[0].Rows[0]["Payment_Recieved"].ToString() == "1")
                {
                    blk3.Visible = false;
                    img3.Visible = true;
                    holder[2] = 1;
                }
                else
                {
                    blk3.Visible = true; 
                    img3.Visible = false;
                    holder[2] = 0;
                }

                if (ds.Tables[0].Rows[0]["Evaluation_Complete"].ToString() == "1")
                {
                    blk4.Visible = false;
                    img4.Visible = true;
                    holder[3] = 1;
                }
                else
                {
                    blk4.Visible = true; 
                    img4.Visible = false;
                    holder[3] = 0;
                }

                if (ds.Tables[0].Rows[0]["Verification_Complete"].ToString() == "1")
                {
                    blk5.Visible = false;
                    img5.Visible = true;
                    holder[4] = 1;
                }
                else
                {
                    blk5.Visible = true; 
                    img5.Visible = false;
                    holder[4] = 0;
                }

                if (ds.Tables[0].Rows[0]["Evaluation_Approved"].ToString() == "1")
                {
                    blk6.Visible = false;
                    img6.Visible = true;
                    holder[5] = 1;
                }
                else
                {
                    blk6.Visible = true; 
                    img6.Visible = false;
                    holder[5] = 0;
                }

                    if (ds.Tables[0].Rows[0]["Packaging_Complete"].ToString() == "1")
                    {
                        blk7.Visible = false;
                        img7.Visible = true;
                        holder[6] = 1;
                    }
                    else
                    {
                        blk7.Visible = true; 
                        img7.Visible = false;
                        holder[6] = 0;
                    }

                    if (ds.Tables[0].Rows[0]["Delievery_Complete"].ToString() == "1")
                    {
                        blk8.Visible = false;
                        img8.Visible = true;
                        holder[7] = 1;
                    }
                    else
                    {
                        blk8.Visible = true; 
                        img8.Visible = false;
                        holder[7] = 0;
                    }

                appl_name.InnerHtml = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                appl_number.InnerHtml = ds.Tables[0].Rows[0]["FileNumber"].ToString();
                appl_purpose.InnerHtml = ds.Tables[0].Rows[0]["Evaluation_Name"].ToString();

            
                //ClientAdmin.Utility.Grid_applicantNotesBrowse(Grid_applicantNotes, filenumber.ToString(), "Client");
                ClientAdmin.Utility.Grid_applicantNotesBrowse(Grid_status, filenumber.ToString(), "Admin");
               
            }
        }
        else
        {
            
            //Response.Write("<script>alert('Session expired! try after some time.')</script>");
        }
    }
}

           
