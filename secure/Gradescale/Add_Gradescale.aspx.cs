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

public partial class secure_Gradescale_Add_Gradescale : System.Web.UI.Page
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
        TextBox name = (TextBox)DetailsView_Gradescale.FindControl("name");
        CKEditorControl institutiondes = (CKEditorControl)DetailsView_Gradescale.FindControl("destxt");
        DropDownList countrydp = (DropDownList)DetailsView_Gradescale.FindControl("countrydp");  
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
              result = ClientAdmin.Utility.Grid_gradescaleAdd(name.Text,Convert.ToInt32(countrydp.SelectedValue.ToString()),institutiondes.Text,Session["Admin_Customer"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_gradescaleAdd(name.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), institutiondes.Text, Session["Admin_Customer"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }       
  
        if (result == true)
        {
            Response.Redirect("~/secure/Gradescale/Browse_Gradescale.aspx?search=&t1=0");
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
}
