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
using CKEditor.NET;  

public partial class secure_Country_Update_Country : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["country_id"] = Request.QueryString["ctrid"];
                Session["country_role"] = Request.QueryString["role"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }    
      

    }  

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                opt.Visible = false;
                break;
            case "ADMIN":
                opt.Visible = true;
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }     
    }  

    protected void Update_Click(object sender, EventArgs e)
    {
        bool result = false;
        TextBox countrytxt = (TextBox)DetailsView_Country.FindControl("Countrytxt");   
        CKEditorControl countrydes = (CKEditorControl)DetailsView_Country.FindControl("destxt");
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_CountryUpdate(countrytxt.Text, Convert.ToInt32(Session["country_id"].ToString()), countrydes.Text, Session["Admin_Customer"].ToString(), Session["country_role"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_CountryUpdate(countrytxt.Text, Convert.ToInt32(Session["country_id"].ToString()), countrydes.Text, Session["country_role"].ToString(), Session["Customer_id"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }           
      
        if (result == true)
        {
            Response.Redirect("~/secure/Country/Browse_Country.aspx?search=" + Request.QueryString["search"].ToString());
        }
    }
    protected void DetailsView_Country_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_CountrySelect(DetailsView_Country, Convert.ToInt32(Session["country_id"].ToString()), Session["Admin_Customer"].ToString(), Session["country_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_CountrySelect(DetailsView_Country, Convert.ToInt32(Session["country_id"].ToString()), Session["country_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_Country.FindControl("extratab");
                    tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }      
            
        }
    }
    protected void DetailsView_Country_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["country_role"].ToString() != "Client")
                {
                    DetailsView_Country.Rows[0].Enabled = false;       
                }
                break;
            case "ADMIN":                
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        } 
    }

    protected void Swap_Click(object sender, EventArgs e)
    {       
        CKEditorControl countrydes = (CKEditorControl)DetailsView_Country.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_Country.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;   
    }
}
