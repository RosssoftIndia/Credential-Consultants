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
public partial class secure_EducationProgram_Update_DegreePlan : System.Web.UI.Page
{ 
      protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["degree_id"] = Request.QueryString["Degid"];
                Session["degree_role"] = Request.QueryString["role"]; 
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }       

    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                switch (Session["degree_role"].ToString())
                {
                    case "Client":
                        msg.InnerText = "";
                        break;
                    default:
                        msg.InnerText = "* Authorization On Editing Master Data is Restricted.";
                        break;
                }
                break;
            case "ADMIN":
                switch (Session["degree_role"].ToString())
                {
                    default:
                        msg.InnerText = "";
                        break;
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }    
      
    } 
    protected void Countrydp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList countrydp = (DropDownList)DetailsView_degreeplan.FindControl("Countrydp");
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

    protected void Update_Click(object sender, EventArgs e)
    {
        TextBox name = (TextBox)DetailsView_degreeplan.FindControl("name");
        DropDownList country = (DropDownList)DetailsView_degreeplan.FindControl("Countrydp");
        DropDownList confirmed = (DropDownList)DetailsView_degreeplan.FindControl("confirmed");
        DropDownList type = (DropDownList)DetailsView_degreeplan.FindControl("type");
        DropDownList equivalency = (DropDownList)DetailsView_degreeplan.FindControl("equivalency");
        CKEditorControl des = (CKEditorControl)DetailsView_degreeplan.FindControl("destxt");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_DegreePlanUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), type.SelectedValue.ToString(), Convert.ToInt32(Session["degree_id"].ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()), Session["Admin_Customer"].ToString(),des.Text ,Session["degree_role"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_DegreePlanUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), type.SelectedValue.ToString(), Convert.ToInt32(Session["degree_id"].ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()), Session["Admin_Customer"].ToString(),des.Text, Session["degree_role"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }       
       
        if (result == true)
        {
            Response.Redirect("~/secure/EducationProgram/Browse_DegreePlan.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString() + "&t2=" + Request.QueryString["t2"].ToString());
        }

    }
    protected void DetailsView_degreeplan_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_DegreePlanSelect(DetailsView_degreeplan, Convert.ToInt32(Session["degree_id"].ToString()),Session["Admin_Customer"].ToString(), Session["degree_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_DegreePlanSelect(DetailsView_degreeplan, Convert.ToInt32(Session["degree_id"].ToString()), Session["degree_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_degreeplan.FindControl("extratab");
                    tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }          
           
            DropDownList country = (DropDownList)DetailsView_degreeplan.FindControl("Countrydp");
            Label temp = (Label)DetailsView_degreeplan.FindControl("temp");
            country.SelectedValue = temp.Text;
            DropDownList dp = (DropDownList)DetailsView_degreeplan.FindControl("equivalency");
            Label temp1 = (Label)DetailsView_degreeplan.FindControl("temp1");
            dp.SelectedValue = temp1.Text;
        }
    }
    protected void equivalency_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            DropDownList dp = (DropDownList)DetailsView_degreeplan.FindControl("equivalency");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.GetEquivalency(dp, Session["Admin_Customer"].ToString(), app.AdminId,false);
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.GetEquivalency(dp, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                         
        }
    }
    protected void DetailsView_degreeplan_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["degree_role"].ToString() != "Client")
                {
                    DetailsView_degreeplan.Rows[0].Enabled  = false;
                    DetailsView_degreeplan.Rows[1].Enabled = false;
                    DetailsView_degreeplan.Rows[2].Enabled = false;
                    DetailsView_degreeplan.Rows[3].Enabled = false;
                    DetailsView_degreeplan.Rows[4].Enabled = false;  
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
        CKEditorControl countrydes = (CKEditorControl)DetailsView_degreeplan.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_degreeplan.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }
}
