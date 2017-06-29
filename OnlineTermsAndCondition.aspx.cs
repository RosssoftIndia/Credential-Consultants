using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class OnlineTermsAndCondition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["cus"].ToString());
        DataSet ds = Credentialpage.Utility.toc(id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ds.Tables[0].Rows[0]["Terms_And_Condition"].ToString() != "") && (ds.Tables[0].Rows[0]["Terms_And_Condition"].ToString() != null))
            {
                info.InnerHtml = ds.Tables[0].Rows[0]["Terms_And_Condition"].ToString();
            }
            else
            {
                info.InnerHtml = "No Terms And Condition Available";
            }
        }
       
    }
}
