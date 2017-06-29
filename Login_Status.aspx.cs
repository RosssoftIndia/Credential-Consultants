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

public partial class  Login_Status : System.Web.UI.Page
{
    Authentication.Utility.DomainAttributes dm = new Authentication.Utility.DomainAttributes();
    Authentication.Utility.SessionVariable sv = new Authentication.Utility.SessionVariable();
   
       
    string txt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string Subdomain = "";
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
            sv = new Authentication.Utility.SessionVariable();
         

            int Clientid = 0;
            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
                OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Online Status for</static><br/>" + "<client>" + dm.SubDmName + "</client>";
                sv.Customer_id = dm.DmID;
                sv.SubClient_id = dm.SubDmID;
                Clientid = dm.SubDmID;
                
            }
            else
            {
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName; sv.Customer_id = dm.DmID;
                Clientid = dm.DmID;
                Subclient.InnerHtml = "<static>Online Status</static>";
             
            }
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
            HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
            msg.InnerHtml = "<span style='color:Red;font-weight:bold;'>Note:</span>&nbsp;To Check the Application status,please enter the corresponding application file number. ";
       
        }      
       

              
    }
   
    protected void statusButton_Click(object sender, EventArgs e)
    {
        int Clientid = 0;
        Session["file"] = "Empty";
        if (txtfile.Text  != "")
        {
            string result = "";
            if (dm.IsMultidomain) 
            { 
                Clientid = dm.SubDmID;
                result = ClientAdmin.Utility.check_filenumber(txtfile.Text.ToString(), Clientid.ToString());
            } else
            {
                Clientid = dm.DmID;
                result = ClientAdmin.Utility.check_filenumberdomain(txtfile.Text.ToString(), dm.DmID.ToString());
            }
            
           if (result == "Access_Denied")
           {
               txterror.Visible = true;  
           }
           else
           {
               txterror.Visible = false;
               Session["file"] = txtfile.Text;    
               Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "ShowValue()", true);              
              
           }
        }
    }

}
