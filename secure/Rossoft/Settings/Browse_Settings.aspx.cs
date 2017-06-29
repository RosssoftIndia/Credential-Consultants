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

public partial class _secure_Rossoft_Settings_Browse_Settings : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_Appsetting_Load(object sender, EventArgs e)
    {       
     RossSoft.Utility.Browse_AppSettings(grid_Appsetting);  
    }
    protected void grid_Appsetting_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Appsetting.PageIndex = e.NewPageIndex;
        RossSoft.Utility.Browse_AppSettings(grid_Appsetting);         
    }

    protected void grid_Appsetting_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_Appsetting.Rows)
        {
            HyperLink customer = (HyperLink)row.FindControl("linkCustomers");
            customer.Text = RossSoft.Utility.singledecrypt(customer.Text);     
        }
    }
}
