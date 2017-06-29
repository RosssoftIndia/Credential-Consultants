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

public partial class secure_ServiceType_Update_ServiceType : System.Web.UI.Page
{
   protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["service_id"] = Request.QueryString["serid"]; 
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
        
        TextBox name = (TextBox)DetailsView_service.FindControl("name");
        TextBox cost = (TextBox)DetailsView_service.FindControl("Cost");
        CKEditorControl des = (CKEditorControl)DetailsView_service.FindControl("desc"); 
        DropDownList type = (DropDownList)DetailsView_service.FindControl("type");
        Label clientid = (Label)DetailsView_service.FindControl("lblclientid");
         bool result = false;
      switch (Session["Admin_Type"].ToString())
        {
            case "USER":
              result =  ClientAdmin.Utility.Grid_ServiceTypeUpdate(name.Text, Convert.ToInt32(cost.Text), type.SelectedValue.ToString(), Convert.ToInt32(Session["service_id"].ToString()), des.Text);
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }     
      
        
        if (result == true)
        {
            Response.Redirect("~/secure/ServiceType/Browse_ServiceType.aspx?clid=" + clientid.Text);
        }

    }
    protected void DetailsView_service_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                     ClientAdmin.Utility.Grid_ServiceTypeSelect(DetailsView_service, Convert.ToInt32(Session["service_id"].ToString()));    
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }           
           
        }

    }
    protected void DetailsView_service_DataBound(object sender, EventArgs e)
    {

        Label lblclientid = (Label)DetailsView_service.FindControl("lblclientid");
        Label clientbottom = (Label)DetailsView_service.FindControl("clientbottom");
        clientbottom.Text = ClientAdmin.Utility.GetclientName(Convert.ToInt32(lblclientid.Text));       
    }
}
