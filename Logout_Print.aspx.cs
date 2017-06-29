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

public partial class Logout_Print : System.Web.UI.Page
{
    string Subdomain = "";
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
            Authentication.Utility.DomainAttributes dm = Authentication.Utility.GetClient(Request.Url, Subdomain);            
            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Print Application for</static><br/>" + "<client>" + dm.SubDmName + "</client>";              
                
            }
            else
            {
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName;           
                Subclient.InnerHtml = "<static>Print Application</static>";
            }
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
            }        
        } 
   
}
