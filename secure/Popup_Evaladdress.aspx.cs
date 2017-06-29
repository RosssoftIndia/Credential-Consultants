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

public partial class secure_Popup_Evaladdress : System.Web.UI.Page
{
    #region Global variables

    protected RadioButtonList block;    
    int i = 0;   
    #endregion

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
    #region dynamic radiobutton control for delivery service
    public void InitializeComponent()
    {

    }
    override protected void OnInit(EventArgs e)
    {
        //control          
        if (Request.QueryString["cid"] != null)
        {
            DataSet ds = ClientAdmin.Utility.delivery_copy(Convert.ToInt32(Request.QueryString["cid"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Delivery_copy"].ToString()) != 0)
            {
                    for (int j = 1; j <= Convert.ToInt32(ds.Tables[0].Rows[0]["Delivery_copy"].ToString()) - 1; j++)
                {
                    HtmlGenericControl title = new HtmlGenericControl("p");
                    dynamic_official.Controls.Add(title);
                        title.InnerHtml = "<b>Official Hard Copy Delivery-" + (j + 1).ToString() + ":</b>";

                        RadioButtonList block = new RadioButtonList();
                        block.ID = "block" + j.ToString();
                        block.AutoPostBack = true;
                        block.Items.Add(new ListItem("Please send my Official Hard Copy to my primary mailing address", "False"));
                        block.Items.Add(new ListItem("Please send this Official Hard Copy to a separate address.", "True"));
                        block.SelectedIndexChanged += new EventHandler(this.frm5_evalradio_SelectedIndexChanged);
                        dynamic_official.Controls.Add(block);

                }

            }
            }
        }
        InitializeComponent();
        base.OnInit(e);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        frm5_overlap.Visible = false;
        frm5_evalgridblock.Visible = true;
        ClientAdmin.Utility.Evaluationdisplay(officialgrid, Session["Request_id"].ToString());
        //primary address
        ClientAdmin.Utility.displayprimary(frm5_primarygrid, Session["Request_id"].ToString());

        if (!Page.IsPostBack)
        {            
            ClientAdmin.Utility.Getcountry(frm5_countryeval);
            if (Request.QueryString["id"] != null)
            {
                    ArrayList list = ClientAdmin.Utility.populate_evaluationAddress(Session["Request_id"].ToString());
                   
                    if (Request.QueryString["cid"] != null)
                    {
                        ClientAdmin.Utility.Getdeliverytype(frm5_deliverytypeeval, Request.QueryString["cid"].ToString());
                        DataSet ds = ClientAdmin.Utility.delivery_copy(Convert.ToInt32(Request.QueryString["cid"].ToString()));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Delivery_copy"].ToString()) > 1)
                            {

                                for (int j = 1; j <= Convert.ToInt32(ds.Tables[0].Rows[0]["Delivery_copy"].ToString()) - 1; j++)
                                {
                                   
                                    for (int k = 0; k <= list.Count - 1; k++)
                                    {
                                        RadioButtonList rbt = (RadioButtonList)this.FindControl("ctl00$Content$block" + j);
                                        string[] temp = new string[2];
                                        temp = list[k].ToString().Split('|');
                                        if (temp[1].ToString() == "Copy" + (j + 1))
                                        {
                                            if (temp[0].ToString() == "Primary")
                                            {
                                                rbt.SelectedValue = "False";
                                            }
                                            else
                                            {
                                                rbt.SelectedValue = "True";
                                                
                                            }
                                        }
                                    }
                                }

                            }
                        }
                }

            }
        }




    }


   
    protected void frm5_evalradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        // frm5_evalradio.Visible = false;
        RadioButtonList blockcntrl = (RadioButtonList)sender;
        string evalcount = "Copy" + (Convert.ToInt32(blockcntrl.ID.ToString().Substring(5)) + 1).ToString();
        frm5_hiddenvalue.Text = evalcount;
        //db check on existing value
      ClientAdmin.Utility.Check_copy(Convert.ToInt32(Session["Request_id"].ToString()), evalcount);

        if (blockcntrl.SelectedValue.ToString() == "False")
        {    //transform

            int result = ClientAdmin.Utility.SaveSameAddress(Convert.ToInt32(Session["Request_id"].ToString()), Convert.ToInt32(Session["Customer_id"].ToString()), evalcount);
            if (result == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Fill In the primary mailing address from the Edit Application')</script>");
                blockcntrl.ClearSelection();
            }
            else
            {
                ClientAdmin.Utility.Evaluationdisplay(officialgrid, Session["Request_id"].ToString());
            }
        }
        else
        {
            dynamic_official.Visible = false;
            frm5_evalformheader.InnerText = "( Official Hard Copy Delivery - " + (Convert.ToInt32(blockcntrl.ID.ToString().Substring(5)) + 1).ToString() + " ) :";
            frm5_evalform.Visible = true;
            
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
            bool result = Credentialpage.Utility.create_Evaluation_Delivery(Convert.ToInt32(frm5_deliverytypeeval.SelectedValue.ToString()), Convert.ToInt32(Session["Request_id"].ToString()), frm5_Fnameeval.Text, frm5_add1eval.Text, frm5_add2eval.Text, frm5_cityeval.Text, frm5_stateeval.Text, frm5_zipeval.Text, Convert.ToInt32(frm5_countryeval.SelectedValue.ToString()), 1, "Evaluation", frm5_hiddenvalue.Text.ToString(),frm5_instname.Text);
            ClientAdmin.Utility.Evaluationdisplay(officialgrid, Session["Request_id"].ToString());
            frm5_evalform.Visible = false;
            dynamic_official.Visible = true;
            frm5_btn_cleareval_Click(this, EventArgs.Empty);
        }

    }
}


            
