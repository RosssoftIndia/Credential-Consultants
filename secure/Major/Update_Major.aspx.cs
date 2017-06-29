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

public partial class secure_Major_Update_Major : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Major_id"] = Request.QueryString["Majid"];  
                Session["major_role"] = Request.QueryString["role"];
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
                switch (Session["major_role"].ToString())
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
                switch (Session["major_role"].ToString())
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
            DropDownList country = (DropDownList)DetailsView_Major.FindControl("Countrydp");
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
        TextBox name = (TextBox)DetailsView_Major.FindControl("name");
        DropDownList country = (DropDownList)DetailsView_Major.FindControl("Countrydp");
        DropDownList confirmed = (DropDownList)DetailsView_Major.FindControl("confirmed");
        DropDownList equivalency = (DropDownList)DetailsView_Major.FindControl("equivalency");
        CKEditorControl des = (CKEditorControl)DetailsView_Major.FindControl("destxt");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_MajorUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), Convert.ToInt32(Session["Major_id"].ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()), Session["Admin_Customer"].ToString(),des.Text  , Session["major_role"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_MajorUpdate(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), Convert.ToInt32(Session["Major_id"].ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()), Session["Customer_id"].ToString(),des.Text, Session["major_role"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }       
      
        if (result == true)
        {
            Response.Redirect("~/secure/Major/Browse_Major.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString() + "&t2=" + Request.QueryString["t2"].ToString());
        }

    }
    protected void DetailsView_Major_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_MajorSelect(DetailsView_Major, Convert.ToInt32(Session["Major_id"].ToString()),Session["Admin_Customer"].ToString(), Session["major_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_MajorSelect(DetailsView_Major, Convert.ToInt32(Session["Major_id"].ToString()), Session["major_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_Major.FindControl("extratab");
                    tab.Visible = false; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }     
         
            DropDownList country = (DropDownList)DetailsView_Major.FindControl("Countrydp");
            Label temp = (Label)DetailsView_Major.FindControl("temp");
            country.SelectedValue = temp.Text;
            DropDownList dp = (DropDownList)DetailsView_Major.FindControl("equivalency");
            Label temp1 = (Label)DetailsView_Major.FindControl("temp1");
            dp.SelectedValue = temp1.Text;
        }
    }
    protected void equivalency_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            DropDownList dp = (DropDownList)DetailsView_Major.FindControl("equivalency");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.GetEquivalency(dp, Session["Admin_Customer"].ToString(), app.AdminId,false );
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
    protected void DetailsView_Major_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["major_role"].ToString() != "Client")
                {
                    DetailsView_Major.Rows[0].Enabled = false;
                    DetailsView_Major.Rows[1].Enabled = false;
                    DetailsView_Major.Rows[2].Enabled = false;
                    DetailsView_Major.Rows[3].Enabled = false;
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
        CKEditorControl countrydes = (CKEditorControl)DetailsView_Major.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_Major.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }
}
