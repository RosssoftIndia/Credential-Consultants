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

public partial class secure_Gradescale_Update_Gradescale : System.Web.UI.Page
{
     protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["grade_id"] = Request.QueryString["gid"];
                Session["grade_role"] = Request.QueryString["role"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }          
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
      

    } 
   
    protected void Update_Click(object sender, EventArgs e)
    {
        TextBox name = (TextBox)DetailsView_Gradescale.FindControl("name");
        CKEditorControl institutiondes = (CKEditorControl)DetailsView_Gradescale.FindControl("destxt");
        DropDownList countrydp = (DropDownList)DetailsView_Gradescale.FindControl("countrydp");    
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_gradescaleUpdate(name.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), institutiondes.Text, Convert.ToInt32(Session["grade_id"].ToString()), Session["Admin_Customer"].ToString(), Session["grade_role"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_gradescaleUpdate(name.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), institutiondes.Text, Convert.ToInt32(Session["grade_id"].ToString()), Session["Customer_id"].ToString(), Session["grade_role"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }   
      
        if (result == true)
        {
            Response.Redirect("~/secure/Gradescale/Browse_Gradescale.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString());
        }
    }

    protected void DetailsView_Gradescale_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_gradescaleSelect(DetailsView_Gradescale, Convert.ToInt32(Session["grade_id"].ToString()),Session["Admin_Customer"].ToString(), Session["grade_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_gradescaleSelect(DetailsView_Gradescale, Convert.ToInt32(Session["grade_id"].ToString()), Session["grade_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_Gradescale.FindControl("extratab");
                    tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }   
        
            DropDownList country = (DropDownList)DetailsView_Gradescale.FindControl("Countrydp");
            Label temp = (Label)DetailsView_Gradescale.FindControl("temp");
            country.SelectedValue = temp.Text; 
        }
    }
    protected void Countrydp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList countrydp = (DropDownList)DetailsView_Gradescale.FindControl("countrydp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Getcountry(countrydp, Session["Admin_Customer"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Getcountry(countrydp, Session["Customer_id"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }     
            
        }
    }
    protected void DetailsView_Gradescale_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["grade_role"].ToString() != "Client")
                {
                    DetailsView_Gradescale.Rows[0].Enabled = false;
                    DetailsView_Gradescale.Rows[1].Enabled = false;                  
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
        CKEditorControl countrydes = (CKEditorControl)DetailsView_Gradescale.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_Gradescale.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }
}
