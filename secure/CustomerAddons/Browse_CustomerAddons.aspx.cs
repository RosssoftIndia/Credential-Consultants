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

public partial class secure_CustomerAddons_Browse_CustomerAddons : System.Web.UI.Page
{
    public string[] str1;
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
            ClientAdmin.Utility.GetSubclients(dpsubclients, Convert.ToInt32(Session["Admin_Customer"].ToString()), false);
            if (Request.QueryString["clid"] != null)
            {
                dpsubclients.SelectedValue = Request.QueryString["clid"].ToString();
            }
        }
       
        Action(dpsubclients.SelectedValue.ToString()); 
    }
   

    protected void dpsubclients_SelectedIndexChanged(object sender, EventArgs e)
    {
        Action(dpsubclients.SelectedValue.ToString());
    }

    public void Action(string clientid)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.DetailsView_CustomerAdonsBrowse(DetailsView_Customer, clientid.ToString());
                DataSet ds = ClientAdmin.Utility.Grid_Creditcard(clientid.ToString());
                CheckBoxList CheckBoxList1 = (CheckBoxList)DetailsView_Customer.FindControl("CheckBoxList1");
                ClientAdmin.Utility.Getcard_type(CheckBoxList1);
                str1 = ds.Tables[0].Rows[0]["Credit_Type"].ToString().Split('|');
                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    for (int j = 0; j <= str1.Length - 1; j++)
                    {
                        if (CheckBoxList1.Items[i].Value == str1[j].ToString())
                        {
                            CheckBoxList1.Items[i].Selected = true;
                        }
                    }
                }       
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }             
    }
}
