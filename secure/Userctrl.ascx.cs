using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class secure_Userctrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void setTitle(string title,int id)
    {
        OrgTitle.InnerHtml = title;        
        Authentication.Utility.checklogo(id, OrgTitle, logo);
    }
}
