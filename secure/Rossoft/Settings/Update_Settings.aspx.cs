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

public partial class _secure_Rossoft_Settings_Update_Settings : System.Web.UI.Page
{
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
              
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        bool result = false;
        TextBox txtCustomerId = (TextBox)DetailsView_Appsettings.FindControl("txtCustomerId");     
        TextBox txtCustomers = (TextBox)DetailsView_Appsettings.FindControl("txtCustomers");      
        TextBox txtStartpage = (TextBox)DetailsView_Appsettings.FindControl("txtStartpage");   
        TextBox txtPayment = (TextBox)DetailsView_Appsettings.FindControl("txtPayment");      
        TextBox txtTypeSwitcher = (TextBox)DetailsView_Appsettings.FindControl("txtTypeSwitcher");     
        TextBox txtAdminId = (TextBox)DetailsView_Appsettings.FindControl("txtAdminId");      
        TextBox txtdeslimit = (TextBox)DetailsView_Appsettings.FindControl("txtdeslimit");      
        TextBox txtSessionTime = (TextBox)DetailsView_Appsettings.FindControl("txtSessionTime");       
        TextBox txtStartyear = (TextBox)DetailsView_Appsettings.FindControl("txtStartyear");      
        TextBox txtEndyear = (TextBox)DetailsView_Appsettings.FindControl("txtEndyear");

        if (Request.QueryString["id"] != null)
        {
            result = RossSoft.Utility.Update_AppSettings(txtCustomerId, txtCustomers, txtStartpage, txtPayment, txtTypeSwitcher, txtAdminId, txtdeslimit, txtSessionTime, txtStartyear, txtEndyear, Request.QueryString["id"].ToString());


            if (result == true)
            {
                Response.Redirect("~/secure/Rossoft/Settings/Browse_Settings.aspx");
            }
        }

    }
    protected void DetailsView_Appsettings_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null) 
            {
                RossSoft.Utility.Select_AppSettings(DetailsView_Appsettings, Request.QueryString["id"].ToString());    
            }
        }
    }

    protected void DetailsView_Appsettings_DataBound(object sender, EventArgs e)
    {
        TextBox txtCustomerId = (TextBox)DetailsView_Appsettings.FindControl("txtCustomerId");
        txtCustomerId.Text = RossSoft.Utility.singledecrypt(txtCustomerId.Text);
        TextBox txtCustomers = (TextBox)DetailsView_Appsettings.FindControl("txtCustomers");
        txtCustomers.Text = RossSoft.Utility.singledecrypt(txtCustomers.Text);  
         TextBox txtStartpage = (TextBox)DetailsView_Appsettings.FindControl("txtStartpage");
        txtStartpage.Text = RossSoft.Utility.singledecrypt(txtStartpage.Text);  
         TextBox txtPayment = (TextBox)DetailsView_Appsettings.FindControl("txtPayment");
        txtPayment.Text = RossSoft.Utility.singledecrypt(txtPayment.Text);  
          TextBox txtTypeSwitcher = (TextBox)DetailsView_Appsettings.FindControl("txtTypeSwitcher");
        txtTypeSwitcher.Text = RossSoft.Utility.singledecrypt(txtTypeSwitcher.Text);  
         TextBox txtAdminId = (TextBox)DetailsView_Appsettings.FindControl("txtAdminId");
        txtAdminId.Text = RossSoft.Utility.singledecrypt(txtAdminId.Text);  
        TextBox txtdeslimit = (TextBox)DetailsView_Appsettings.FindControl("txtdeslimit");
        txtdeslimit.Text = RossSoft.Utility.singledecrypt(txtdeslimit.Text);  
         TextBox txtSessionTime = (TextBox)DetailsView_Appsettings.FindControl("txtSessionTime");
       txtSessionTime.Text = RossSoft.Utility.singledecrypt(txtSessionTime.Text);  
        TextBox txtStartyear = (TextBox)DetailsView_Appsettings.FindControl("txtStartyear");
        txtStartyear.Text = RossSoft.Utility.singledecrypt(txtStartyear.Text);  
         TextBox txtEndyear = (TextBox)DetailsView_Appsettings.FindControl("txtEndyear");
       txtEndyear.Text = RossSoft.Utility.singledecrypt(txtEndyear.Text);  

    }
}
