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
using System.IO; 

public partial class secure_SplashPage_Browse_Splashpage : System.Web.UI.Page
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

    protected void DetailsView_Splash_DataBound(object sender, EventArgs e)
    {
      
        Panel pdfpanel = (Panel)DetailsView_Splash.FindControl("pdfpanel");
        HyperLink btn = (HyperLink)DetailsView_Splash.FindControl("Imgpdf");
        Label clientid = (Label)DetailsView_Splash.FindControl("lblclientid");

        if (File.Exists(Server.MapPath("~/Assets/OfflineApp/" + clientid.Text + "/Application.pdf")))
        {
            btn.NavigateUrl = "~/Assets/OfflineApp/" + clientid.Text + "/Application.pdf";
        }
        else { pdfpanel.Visible = false; }       
       
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
                ClientAdmin.Utility.Splashpage_Browse(DetailsView_Splash, clientid);
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }        
    }
}
