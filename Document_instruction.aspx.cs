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

public partial class Document_instruction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int customerid = Convert.ToInt32(Request.QueryString["cus"].ToString());
        DataSet ds = Credentialpage.Utility.delivery_copy(customerid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            infobox.Text = ds.Tables[0].Rows[0]["Document_Instructions"].ToString(); 
        }
    }

}
