using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class secure_Quickapp_trail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void getCurrent(int current)
    {

        link1.Attributes.Remove("class");
        link2.Attributes.Remove("class");
        link3.Attributes.Remove("class");
        link4.Attributes.Remove("class");
        switch (current)
        {
            case 1:
                link1.Attributes.Add("class", "current");
                break;
            case 2:
                link2.Attributes.Add("class", "current");
                break;
            case 3:
                link3.Attributes.Add("class", "current");
                break;
            case 4:
                link4.Attributes.Add("class", "current");
                break;

        }
    }
}
