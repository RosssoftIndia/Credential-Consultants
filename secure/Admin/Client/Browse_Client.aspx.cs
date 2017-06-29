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

public partial class secure_Admin_Client_Browse_Client : System.Web.UI.Page
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

          string listcontent ="";
        DataSet ds = MasterAdmin.Utility.Getclientlist();

        if (ds.Tables[0].Rows.Count > 0)
        {
            HtmlGenericControl accordion = new HtmlGenericControl("div");
            accordion.Attributes.Add("id", "accordion");
            for(int i=0; i<=ds.Tables[0].Rows.Count-1;i++)
            {
           listcontent  +=
                  "<h3><a href='#'>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</a></h3><div>" +
                "<table class='accordion-content'><thead><tr><td>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</td><td><a class='link' href='Update_Client.aspx?clid=" + ds.Tables[0].Rows[i]["id"].ToString() + "'>Edit</a>|<a class='link' href='../Splash/Update_Splashpage.aspx?clid=" + ds.Tables[0].Rows[i]["id"].ToString() + "'>Splash</a></td></tr></thead><tfoot><tr><td colspan='2'><em></em></td></tr></tfoot><tbody>" + Subdomain(ds.Tables[0].Rows[i]["SubDomainName"].ToString()) +
          "</div>";
            }
            accordion.InnerHtml = listcontent;
            list.Controls.Add(accordion);  
        }
          	
    }

    public string Subdomain(string subdomain)
    {
        string subcontent = "";
        DataSet ds = MasterAdmin.Utility.Getsubclientlist(subdomain);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                subcontent += "<tr><td>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</td><td><a class='link' href='Update_Client.aspx?clid=" + ds.Tables[0].Rows[i]["id"].ToString() + "'>Edit</a>|<a class='link' href='../Splash/Update_Splashpage.aspx?clid=" + ds.Tables[0].Rows[i]["id"].ToString() + "'>Splash</a></td></td></tr>";
            }
            subcontent += "</tbody></table>";
        }
        else
        {
            subcontent += "<tr><td>No Sub clients Available</td><td></td></tr><tbody></table>";
        }

        return subcontent; 
    }
  
}
