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

public partial class secure_Description : System.Web.UI.Page
{
    protected void page_init(object sender, EventArgs e)
    {

    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        lbleducation.Text = Request.QueryString["content"].ToString();
    }
 
  
}

           
