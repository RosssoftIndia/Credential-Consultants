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

public partial class secure_Popup_Primaryaddress : System.Web.UI.Page
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
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["cid"] != null)
            {
                ClientAdmin.Utility.Getdeliverytype(frm5_pdelivery, Request.QueryString["cid"].ToString());
            }
            ClientAdmin.Utility.Getcountry(frm5_pcountry);

            
            if (Request.QueryString["id"] != null)
            {
                //primary address update
                if (Request.QueryString["id"].ToString() == "1")
                {
                    //button
                    primarysubmit.Visible = false;
                    primaryupdate.Visible = true;
                    primaryeditmsg.Visible = true;
                    primaryeditgrid.Visible = true;

                    //display grid
                    ClientAdmin.Utility.deliverydisplay(deliverydisplaygrid, Session["Request_id"].ToString()); 

                    DataSet ds = ClientAdmin.Utility.GetprimaryAddress(Session["Request_Id"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        frm5_pname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        frm5_padd1.Text = ds.Tables[0].Rows[0]["Addressline1"].ToString();
                        frm5_padd2.Text = ds.Tables[0].Rows[0]["Addressline2"].ToString();
                        frm5_pcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        frm5_pstate.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                        frm5_pzip.Text = ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString();
                        frm5_pcountry.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                        frm5_pdelivery.SelectedValue = ds.Tables[0].Rows[0]["Delivery_Type_Id"].ToString();
                        frm5_pinst.Text = ds.Tables[0].Rows[0]["Optional_InstitutionName"].ToString();
                    }
                }
                if (Request.QueryString["id"].ToString() == "0")
                {
                    //button
                    primarysubmit.Visible = true;
                    primaryupdate.Visible = false;
                    primaryeditmsg.Visible = false;
                    primaryeditgrid.Visible = false; 
                }
                

            }
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        bool result = false;
        switch (btn.ID)
        {
            case "primarysubmit":
                result = ClientAdmin.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_pdelivery.SelectedValue.ToString()), Convert.ToInt32(Session["Request_id"].ToString()), frm5_pname.Text, frm5_padd1.Text, frm5_padd2.Text, frm5_pcity.Text, frm5_pstate.Text, frm5_pzip.Text, Convert.ToInt32(frm5_pcountry.SelectedValue.ToString()), 1, "Evaluation", "primary",frm5_pinst.Text);
                if (result)
                {
                    Response.Redirect("~/secure/Request_complete.aspx?id=9");
                }
                else { Response.Redirect("~/secure/Request_complete.aspx?id=8"); }
                break;

            case "primaryupdate":
                result = ClientAdmin.Utility.update_primaryaddress(Convert.ToInt32(frm5_pdelivery.SelectedValue.ToString()), Convert.ToInt32(Session["Request_id"].ToString()), frm5_pname.Text, frm5_padd1.Text, frm5_padd2.Text, frm5_pcity.Text, frm5_pstate.Text, frm5_pzip.Text, Convert.ToInt32(frm5_pcountry.SelectedValue.ToString()), 1, "Evaluation", "primary", true, frm5_pinst.Text);
                if (result)
                {
                    Response.Redirect("~/secure/Request_complete.aspx?id=7");
                }
                else { Response.Redirect("~/secure/Request_complete.aspx?id=6"); }
                break;



        }
    }
    protected void primaryclear_Click(object sender, EventArgs e)
    {
        frm5_padd1.Text = "";
        frm5_padd2.Text = "";
        frm5_pstate.Text = "";
        frm5_pcity.Text = "";
        frm5_pname.Text = "";
        frm5_pdelivery.SelectedIndex = 0;
        frm5_pcountry.SelectedIndex = 0;
        frm5_pzip.Text = "";
    }
}


            
