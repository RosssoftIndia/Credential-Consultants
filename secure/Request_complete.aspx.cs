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

public partial class secure_Request_complete : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            switch (Convert.ToInt32(Request.QueryString["id"].ToString()))
            {
                case 0:
                    bodycontent.InnerHtml = "Your Changes Failed to Update. Close this window to return to Edit Application.";
                    break;
                case 1:
                    bodycontent.InnerHtml = "Your Changes are Updated successfully. Close this window to return to Edit Application.";
                    break;
                case 2:
                    bodycontent.InnerHtml = "New Education Record Failed to Insert. Close this window to return to Edit Application.";
                    break;
                case 3:
                    bodycontent.InnerHtml = "New Education Record Inserted successfully. Close this window to return to Edit Application.";
                    break;
                case 4:
                    bodycontent.InnerHtml = "New service Inserted successfully. Close this window to return to Edit Application.";
                    break;
                case 5:
                    bodycontent.InnerHtml = "Your Changes are Updated successfully. Close this window to return to Edit Application.";
                    break;
                case 6:
                    bodycontent.InnerHtml = "Your Changes to Primary Address Failed to Update. Close this window to return to Edit Application.";
                    break;
                case 7:
                    bodycontent.InnerHtml = "Your Changes to Primary Address Updated successfully. Close this window to return to Edit Application.";
                    break;
                case 8:
                    bodycontent.InnerHtml = "Primary Mailing Address Failed to Insert. Close this window to return to Edit Application.";
                    break;
                case 9:
                    bodycontent.InnerHtml = "Primary Mailing Address Inserted successfully. Close this window to return to Edit Application.";
                    break;
                case 10:
                    bodycontent.InnerHtml = "Fax Service Failed to Insert. Close this window to return to Edit Application.";
                    break;
                case 11:
                    bodycontent.InnerHtml = "Fax Service Inserted successfully. Close this window to return to Edit Application.";
                    break;
                case 12:
                    bodycontent.InnerHtml = "Additional Official Hard Copy Service Failed to Insert. Close this window to return to Edit Application.";
                    break;
                case 13:
                    bodycontent.InnerHtml = "Additional Official Hard Copy Service Inserted successfully. Close this window to return to Edit Application.";
                    break;
                case 14:
                    bodycontent.InnerHtml = "Official Electronic Service Failed to Insert. Close this window to return to Edit Application.";
                    break;
                case 15:
                    bodycontent.InnerHtml = "Official Electronic Service Inserted successfully. Close this window to return to Edit Application.";
                    break;
            }
            
        }
    }
}
