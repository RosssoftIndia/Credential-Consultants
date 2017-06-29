using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class secure_viewpdf : System.Web.UI.Page
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
        //url="http://samplepdf.com/sample.pdf&embedded=true

        if ((Request.QueryString["id"] != null) & (Request.QueryString["type"] != null))
        {
            string path = ""; 
            switch (Request.QueryString["type"].ToString())
            {
                case"1":
                    path = "../Assets/Documents/";
                    break;
                case "2":
                    path = "../Assets/Attachments/" + Request.QueryString["aid"].ToString() + "/Documents/";
                    break;
                case "3":
                    path = "../Assets/Attachments/" + Request.QueryString["aid"].ToString() + "/Reports/";
                    break;
            }
            //String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            //String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            //strUrl = "http://sdr.credentialconnectiontest.com/";
            //pdf_file.Attributes.Add("src", "http://docs.google.com/gview?url=" + strUrl + "Assets/Documents/" + Request.QueryString["id"].ToString() + ".pdf&embedded=true");
            pdf_file.Attributes.Add("src", "http://docs.google.com/gview?url=http://www.dharman.net/email/test.pdf&embedded=true");   
            //pdf_file.Attributes.Add("src", path + Request.QueryString["id"].ToString());   

             
        }
    }
 
   
}
