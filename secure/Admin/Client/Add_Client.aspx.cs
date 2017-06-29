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

public partial class secure_Admin_Client_Add_Client : System.Web.UI.Page
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
            DropDownList clientdrp = (DropDownList)DetailsView_Client.FindControl("dpclients");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                   break;
                case "ADMIN":
                    MasterAdmin.Utility.dpclient(clientdrp);
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                
        }

    }
    protected void Add_Click(object sender, EventArgs e)
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
                    result = MasterAdmin.Utility.Grid_clientAdd(Name.Text, Address.Text, City.Text, State.Text, Zipcode.Text,domainname.Text, clientdrp.SelectedValue.ToString());   
                      break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }   

           
            if (result)
            {
                Response.Redirect("~/secure/Admin/Client/Browse_Client.aspx");
            }
               
    }
   
   
}
