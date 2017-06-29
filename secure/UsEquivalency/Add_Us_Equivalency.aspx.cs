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

public partial class secure_UsEquivalency_Add_Us_Equivalency : System.Web.UI.Page
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
      

    }
    protected void Add_Click(object sender, EventArgs e)
    {
        TextBox name = (TextBox)DetailsView_Equivalency.FindControl("name");
        CKEditorControl institutiondes = (CKEditorControl)DetailsView_Equivalency.FindControl("destxt");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_EquivalencyAdd(name.Text, institutiondes.Text, Session["Admin_Customer"].ToString());
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        } 
  
        if (result == true)
        {
            Response.Redirect("~/secure/UsEquivalency/Browse_Us_Equivalency.aspx?search=");
        }
    }  
}
