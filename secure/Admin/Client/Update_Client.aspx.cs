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

public partial class secure_Admin_Client_Update_Client : System.Web.UI.Page
{
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Client_id"] = Request.QueryString["clid"];  
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
        bool result = false;
        TextBox Name = (TextBox)DetailsView_Client.FindControl("txtName");
        TextBox Address = (TextBox)DetailsView_Client.FindControl("txtAddress");
        TextBox City = (TextBox)DetailsView_Client.FindControl("txtCity");
        TextBox State = (TextBox)DetailsView_Client.FindControl("txtState");
        TextBox Zipcode = (TextBox)DetailsView_Client.FindControl("txtZipcode");
        TextBox domainname = (TextBox)DetailsView_Client.FindControl("txtDomainName");
        DropDownList clientdrp = (DropDownList)DetailsView_Client.FindControl("dpclients");  
      
      
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_clientUpdate(Name.Text, Address.Text, City.Text, State.Text, Zipcode.Text, domainname.Text, clientdrp.SelectedValue.ToString(), Convert.ToInt32(Session["Client_id"].ToString()));
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }   
        
        if (result == true)
        {
            Response.Redirect("~/secure/Admin/Client/Browse_Client.aspx");
        }

    }
    protected void DetailsView_Client_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":                  
                  MasterAdmin.Utility.Grid_clientSelect(DetailsView_Client, Session["Client_id"].ToString());                  
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }         
        }
    }

    protected void DetailsView_Client_DataBound(object sender, EventArgs e)
    {
        
       
        
    }
    protected void dpclients_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TextBox type = (TextBox)DetailsView_Client.FindControl("txtType");
            DropDownList clientdrp = (DropDownList)DetailsView_Client.FindControl("dpclients");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.dpclient(clientdrp);
                    clientdrp.SelectedValue = type.Text; 
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }
    }
}
