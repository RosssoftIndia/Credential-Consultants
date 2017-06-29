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

public partial class secure_ClientAdmin_Client_Add_Client : System.Web.UI.Page
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
                    ClientAdmin.Utility.dpclient(clientdrp, Session["Admin_Customer"].ToString(), false);
                   break;
                case "ADMIN":                  
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                
           
        }

    }
    protected void Add_Click(object sender, EventArgs e)
    {
        Label errormsg = (Label)DetailsView_Client.FindControl("errormsg");
        if (errormsg.Text == "The DomainName is valid.")
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
                    result = ClientAdmin.Utility.Grid_clientAdd(Name.Text, Address.Text, City.Text, State.Text, Zipcode.Text, domainname.Text, clientdrp.SelectedValue.ToString());   
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }   

           
            if (result)
            {
                Response.Redirect("~/secure/ClientAdmin/Client/Browse_Client.aspx");
            }
               
    }
    }

   
    protected void DomainName_TextChanged(object sender, EventArgs e)
    {
        Label errormsg = (Label)DetailsView_Client.FindControl("errormsg");
        TextBox txt = (TextBox)sender;
        errormsg.Text = "";
        if (txt.Text != "")
        {
            bool result = ClientAdmin.Utility.DomainName_Exist(txt.Text);
            if (result)
            {
                errormsg.ForeColor = System.Drawing.Color.Red;
                errormsg.Text = "The DomainName that you requested,has already been registered.";               
            }
            else
            {
                errormsg.ForeColor = System.Drawing.Color.Green;
                errormsg.Text = "The DomainName is valid.";
                
            }
         
        }
        
    }
   
}
