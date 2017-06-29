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

public partial class secure_UsEquivalency_Browse_Us_Equivalency : System.Web.UI.Page
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

    }    
  

    protected void grid_Equivalency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grid_Equivalency.PageIndex = e.NewPageIndex;
        action();   
       
    }
    protected void grid_Equivalency_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            searchbox.Text = Request.QueryString["search"].ToString();
            action();
        }     
      
    }
    protected void grid_Equivalency_DataBound(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        foreach (GridViewRow row in grid_Equivalency.Rows)
        {
            //url          
            HyperLink url = (HyperLink)row.FindControl("HyperLink1");
            Label equi_id = (Label)row.FindControl("equi_id");
            url.NavigateUrl = "~/secure/UsEquivalency/Update_Us_Equivalency.aspx?eqlid=" + equi_id.Text.ToString() + "&search=" + searchbox.Text; 

            //tooltip
            int limit = Convert.ToInt32(app.deslimit);
            Label lblcdes = (Label)row.FindControl("lbldes");
            string clientdes = (System.Text.RegularExpressions.Regex.Replace(Server.HtmlDecode(lblcdes.Text), @"<[^>]*>", string.Empty)).Replace("&nbsp;", "");
            if (clientdes.Length > limit) { lblcdes.Text = clientdes.Substring(0, limit) + "..."; } else { lblcdes.Text = clientdes; }
            lblcdes.ToolTip = clientdes;                   

        } 
    }

    protected void searchbtn_Click(object sender, ImageClickEventArgs e)
    {
        action();
    }
    private void action()
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_SearchEquivalency(grid_Equivalency, searchbox.Text, Session["Admin_Customer"].ToString());
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }
}
