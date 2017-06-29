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
public partial class secure_Major_Add_Major : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            DropDownList country = (DropDownList)DetailsView_Major.FindControl("Countrydp");            
            DropDownList dp = (DropDownList)DetailsView_Major.FindControl("equivalency");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Getcountry(country, Session["Admin_Customer"].ToString());
                    ClientAdmin.Utility.GetEquivalency(dp, Session["Admin_Customer"].ToString(), app.AdminId, false);
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Getcountry(country, Session["Customer_id"].ToString());
                    MasterAdmin.Utility.GetEquivalency(dp, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                  

        }

    }  
   

 protected void Add_Click(object sender, EventArgs e)
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
                result = ClientAdmin.Utility.Grid_MajorAdd(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()),des.Text, Session["Admin_Customer"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_MajorAdd(name.Text, Convert.ToInt32(country.SelectedValue.ToString()), Convert.ToInt32(confirmed.SelectedValue.ToString()), Convert.ToInt32(equivalency.SelectedValue.ToString()),des.Text, Session["Admin_Customer"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }     
        
        if (result == true)
        {
            Response.Redirect("~/secure/Major/Browse_Major.aspx?search=&t1=0&t2=0");
        }   

    }
   
}
