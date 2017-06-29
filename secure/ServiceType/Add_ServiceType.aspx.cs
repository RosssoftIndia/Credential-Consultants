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

public partial class secure_ServiceType_Add_ServiceType : System.Web.UI.Page
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
        TextBox name = (TextBox)DetailsView_service.FindControl("name");
        TextBox cost = (TextBox)DetailsView_service.FindControl("Cost");
        CKEditorControl des = (CKEditorControl)DetailsView_service.FindControl("desc"); 
        DropDownList type = (DropDownList)DetailsView_service.FindControl("type");
        DropDownList dpsubclients = (DropDownList)DetailsView_service.FindControl("dpsubclients");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_ServiceTypeAdd(name.Text, Convert.ToInt32(cost.Text), type.SelectedValue.ToString(), dpsubclients.SelectedValue.ToString(), des.Text);
                break;
            case "ADMIN":                
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }     
       
        if (result == true)
        {
            Response.Redirect("~/secure/ServiceType/Browse_ServiceType.aspx?clid=" + dpsubclients.SelectedValue.ToString());
        }
    }

    protected void dpsubclients_Load(object sender, EventArgs e)
    {
        DropDownList dpsubclients = (DropDownList)DetailsView_service.FindControl("dpsubclients");
        if (!Page.IsPostBack)
        {
            ClientAdmin.Utility.GetSubclients(dpsubclients, Convert.ToInt32(Session["Admin_Customer"].ToString()), false);
        }
        if (Request.QueryString["clid"] != null)
        {
            dpsubclients.Enabled = false;
            dpsubclients.SelectedValue = Request.QueryString["clid"].ToString();
        }
    }
   
}
