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

public partial class  Fail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool ClientIsValid = Authentication.Utility.AdminClientIsValid(Request.Url);
        if (ClientIsValid)
        {
            Authentication.Utility.AdminDomainAttributes dm = Authentication.Utility.AdminGetClient(Request.Url);

            Page.Title = dm.DmName;
            OrgTitle.InnerHtml = dm.DmName;           
          Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
        }
    }  
   
}
