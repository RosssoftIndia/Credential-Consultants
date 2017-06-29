using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class secure_Quickapp_dots : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void getCurrent(int current,int dotcount)
    {

        for (int i = 1; i <= dotcount; i++)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");  
           
            if (i == current)
            {
                li.Attributes.Add("class", "current"); 
            }
            HtmlAnchor aTag = new HtmlAnchor();
            aTag.HRef = "#";
            li.Controls.Add(aTag);    

            controlholder.Controls.Add(li);    

        }
    }
}
