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

public partial class Edit_Service : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["service_id"] = Convert.ToInt32(Request.QueryString["id"].ToString());
        Session["Cus_id"] = Convert.ToInt32(Request.QueryString["cid"].ToString());
    }

    protected void service1grid_Load(object sender, EventArgs e)
    {
        Credentialpage.Utility.update_service1grid(service1grid, Convert.ToInt32(Session["service_id"].ToString()));

    }
    protected void dp_val_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList dp = (DropDownList)sender;
            GridViewRow grdRow = (GridViewRow)dp.Parent.Parent as GridViewRow;
            Label id_control = (Label)grdRow.FindControl("temp");
            int id = Convert.ToInt32(id_control.Text.ToString());
            Credentialpage.Utility.Grid_servicedp(dp, Convert.ToInt32(Session["Cus_id"].ToString()));
            dp.SelectedValue = id.ToString();
        }

    }
    protected void Update_Click(object sender, EventArgs e)
    {
         Button updatebtn = (Button)sender;
            GridViewRow grdRow = (GridViewRow)updatebtn.Parent.Parent as GridViewRow;
            DropDownList dp = (DropDownList)grdRow.FindControl("dp_val");       
          Credentialpage.Utility.update_evaluation_services(Convert.ToInt32(Session["service_id"].ToString()),Convert.ToInt32(dp.SelectedValue.ToString()));     
        Response.Redirect("~/Request_complete.aspx");  

    }
}