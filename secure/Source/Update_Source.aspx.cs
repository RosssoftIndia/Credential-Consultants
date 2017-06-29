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

public partial class secure_Source_Update_Source : System.Web.UI.Page
{
     protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["source_id"] = Request.QueryString["sid"];
                Session["source_role"] = Request.QueryString["role"];
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
        TextBox name = (TextBox)DetailsView_source.FindControl("name");
        CKEditorControl institutiondes = (CKEditorControl)DetailsView_source.FindControl("destxt");
        DropDownList countrydp = (DropDownList)DetailsView_source.FindControl("countrydp");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_SourceUpdate(name.Text, institutiondes.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), Session["Admin_Customer"].ToString(), Convert.ToInt32(Session["source_id"].ToString()));
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_SourceUpdate(name.Text, institutiondes.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), Session["Admin_Customer"].ToString(), Convert.ToInt32(Session["source_id"].ToString()));
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
       
        if (result == true)
        {
            Response.Redirect("~/secure/Source/Browse_Source.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString());
        }
    }
   
    protected void DetailsView_source_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_SourceSelect(DetailsView_source, Convert.ToInt32(Session["source_id"].ToString()), Session["Admin_Customer"].ToString());
                    break;
                case "ADMIN":
                   MasterAdmin.Utility.Grid_SourceSelect(DetailsView_source, Convert.ToInt32(Session["source_id"].ToString()));
                   HtmlGenericControl tab = (HtmlGenericControl)DetailsView_source.FindControl("extratab");
                   tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
          
            DropDownList country = (DropDownList)DetailsView_source.FindControl("Countrydp");
            Label temp = (Label)DetailsView_source.FindControl("temp");
            country.SelectedValue = temp.Text; 
        }
    }
    protected void Countrydp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList countrydp = (DropDownList)DetailsView_source.FindControl("countrydp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Getcountry(countrydp, Session["Admin_Customer"].ToString());
                    break;
                case "ADMIN":
                   MasterAdmin.Utility.Getcountry(countrydp, Session["Admin_Customer"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }         
          
        }
    }
    protected void DetailsView_source_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["source_role"].ToString() != "Client")
                {
                    DetailsView_source.Rows[0].Enabled = false;
                    DetailsView_source.Rows[1].Enabled = false;     
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
        CKEditorControl countrydes = (CKEditorControl)DetailsView_source.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_source.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }
}
