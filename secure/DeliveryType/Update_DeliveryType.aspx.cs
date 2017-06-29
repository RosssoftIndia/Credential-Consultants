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


public partial class secure_DeliveryType_Update_DeliveryType : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Delivery_id"] = Request.QueryString["delid"];  
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

        TextBox name = (TextBox)DetailsView_Delivery.FindControl("name");
        CKEditorControl des = (CKEditorControl)DetailsView_Delivery.FindControl("desc"); 
        TextBox cost = (TextBox)DetailsView_Delivery.FindControl("Cost");
        DropDownList type = (DropDownList)DetailsView_Delivery.FindControl("type");
        Label clientid = (Label)DetailsView_Delivery.FindControl("lblclientid");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_DeliveryTypeUpdate(name.Text, Convert.ToInt32(cost.Text), type.SelectedValue.ToString(), Convert.ToInt32(Session["Delivery_id"].ToString()),des.Text);
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }      
        
        if (result == true)
        {
            Response.Redirect("~/secure/DeliveryType/Browse_DeliveryType.aspx?clid=" + clientid.Text);
        }

    }   
    protected void DetailsView_Delivery_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_DeliveryTypeSelect(DetailsView_Delivery, Convert.ToInt32(Session["Delivery_id"].ToString()));
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }    
            
        }
    }
    protected void DetailsView_Delivery_DataBound(object sender, EventArgs e)
    {

        Label lblclientid = (Label)DetailsView_Delivery.FindControl("lblclientid");
        Label clientbottom = (Label)DetailsView_Delivery.FindControl("clientbottom");
        clientbottom.Text = ClientAdmin.Utility.GetclientName(Convert.ToInt32(lblclientid.Text));       
      
    }
}
