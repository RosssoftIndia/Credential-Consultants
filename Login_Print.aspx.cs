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

public partial class  Login_Print : System.Web.UI.Page
{
    string Subdomain = "";
    string txt = "";
    Authentication.Utility.DomainAttributes dm = new Authentication.Utility.DomainAttributes();
    protected void Page_Load(object sender, EventArgs e)
    {
   
        bool ClientIsValid = false;
        if (Request.QueryString["subdomain"] != null)
        {
            Subdomain = Request.QueryString["subdomain"].ToString();
        }
        else
    {
            Subdomain = "nosubdomain";
        }
        ClientIsValid = Authentication.Utility.ClientIsValid(Request.Url, Subdomain);
        //Client Check
        if (ClientIsValid)
        {
            dm = Authentication.Utility.GetClient(Request.Url, Subdomain);
            int Clientid = 0;
            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Print Application for</static><br/>" + "<client>" + dm.SubDmName + "</client>";
                Clientid = dm.SubDmID;
                
            }
            else
            {
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName; 
                Clientid = dm.DmID;
                Subclient.InnerHtml = "<static>Print Application</static>";
             
            }
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
            HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
            msg.InnerHtml = "<span style='color:Red;font-weight:bold;'>Note:</span>&nbsp;To print the Application,please enter the corresponding application file number. ";
      
        }    
              
     
    }
   
    protected void statusButton_Click(object sender, EventArgs e)
    {
        int Clientid = 0;
        if (dm.IsMultidomain) { Clientid = dm.SubDmID; } else { Clientid = dm.DmID; }
        Session["file"] = "Empty";
        if (txtfile.Text != "")
        {
            string result = ClientAdmin.Utility.check_filenumber(txtfile.Text.ToString(), Clientid.ToString());
           if (result == "Access_Denied")
           {
               txterror.Visible = true;  
           }
           else
           {
               txterror.Visible = false;
               Session["file"] = txtfile.Text;
               Response.Redirect("~/Printapplication.aspx?id=" + Clientid.ToString() + "|" + txtfile.Text);
               //Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "ShowValue()", true);              
              
           }
        }
    }

}
