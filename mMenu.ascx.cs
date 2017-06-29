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

public partial class mMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    public void PublicMethodInUsercontrol(int i)
    {        
        switch (i)
        {
            case 1:
                Link1.Attributes.Add("class", "current");               
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "last");               
                break;
            case 2:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "current");   
                Link3.Attributes.Add("class", "last");               
                break;
            case 3:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "current last");  
                break;          
           
            default:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "last");          
                break;

        }
        
    }
}
