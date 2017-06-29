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

public partial class Service_description : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["cus"].ToString());
        DataSet ds = Credentialpage.Utility.serviceDescription(id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ds.Tables[0].Rows[0]["Description"].ToString() != "")&&(ds.Tables[0].Rows[0]["Description"].ToString() != null))
            {
            infobox.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            }
             else
        {
            infobox.Text = "No Description Available";
        }
        }
       
    }

}
