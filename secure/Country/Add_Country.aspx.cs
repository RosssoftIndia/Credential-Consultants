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

public partial class secure_Country_Add_Country : System.Web.UI.Page
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
         bool result = false;
       TextBox countrytxt = (TextBox)DetailsView_Country.FindControl("Countrytxt");
           CKEditorControl des = (CKEditorControl)DetailsView_Country.FindControl("countrydesc");
       switch (Session["Admin_Type"].ToString())
       {
           case "USER":            
               break;
           case "ADMIN":
             result = MasterAdmin.Utility.Grid_CountryAdd(countrytxt.Text, des.Text, Session["Admin_Customer"].ToString());
               break;
           default:
               Response.Redirect("~/Fail.aspx");
               break;
       }      
     
      
       if (result == true)
       {
           Response.Redirect("~/secure/Country/Browse_Country.aspx?search=");
       }
    }
}
