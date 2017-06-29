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

public partial class secure_Institution_Update_Institution : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["institution_id"] = Request.QueryString["instid"];
                Session["institution_role"] = Request.QueryString["role"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

    }
       


    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
 
    protected void DetailsView_institution_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_InstitutionSelect(DetailsView_institution, Convert.ToInt32(Session["institution_id"].ToString()), Session["Admin_Customer"].ToString(), Session["institution_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_InstitutionSelect(DetailsView_institution, Convert.ToInt32(Session["institution_id"].ToString()), Session["institution_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_institution.FindControl("extratab");
                    tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }          
            DropDownList country = (DropDownList)DetailsView_institution.FindControl("Countrydp");
            Label temp = (Label)DetailsView_institution.FindControl("temp");
            country.SelectedValue = temp.Text; 
        }
    }  
    protected void Countrydp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            DropDownList country = (DropDownList)DetailsView_institution.FindControl("Countrydp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Getcountry(country, Session["Admin_Customer"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Getcountry(country, Session["Customer_id"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }            
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
       
        TextBox name = (TextBox)DetailsView_institution.FindControl("name");
        DropDownList country = (DropDownList)DetailsView_institution.FindControl("Countrydp");
        DropDownList confirmed = (DropDownList)DetailsView_institution.FindControl("confirmed");
        DropDownList type = (DropDownList)DetailsView_institution.FindControl("type");
        CKEditorControl institutiondes = (CKEditorControl)DetailsView_institution.FindControl("destxt");
        DropDownList mill = (DropDownList)DetailsView_institution.FindControl("degmill");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_InstitutionUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), type.SelectedValue.ToString(), Convert.ToInt32(Session["institution_id"].ToString()), Session["Admin_Customer"].ToString(), institutiondes.Text, Session["institution_role"].ToString(), Convert.ToInt32(mill.SelectedValue.ToString()));
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_InstitutionUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), type.SelectedValue.ToString(), Convert.ToInt32(Session["institution_id"].ToString()), Session["Customer_id"].ToString(), institutiondes.Text, Session["institution_role"].ToString(), Convert.ToInt32(mill.SelectedValue.ToString()));
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }   
       
        if (result == true)
        {
            Response.Redirect("~/secure/Institution/Browse_Institution.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString() + "&t2=" + Request.QueryString["t2"].ToString() + "&t3=" + Request.QueryString["t3"].ToString() + "&t4=" + Request.QueryString["t4"].ToString());
        }

    }
    protected void DetailsView_institution_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["institution_role"].ToString() != "Client")
                {
                    DetailsView_institution.Rows[0].Enabled = false;
                    DetailsView_institution.Rows[1].Enabled = false;
                    DetailsView_institution.Rows[2].Enabled = false;
                    DetailsView_institution.Rows[3].Enabled = false;                               
                    DetailsView_institution.Rows[4].Enabled = false;           
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
        CKEditorControl countrydes = (CKEditorControl)DetailsView_institution.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_institution.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }
}
