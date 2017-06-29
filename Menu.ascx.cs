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

public partial class Menu : System.Web.UI.UserControl
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
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");
                break;
            case 2:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "current");   
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");
                break;
            case 3:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "current");   
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");  
                break;
            case 4:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "current");   
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");  
                break;
            case 5:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "current");   
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");  
                break;
            case 6:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "current");   
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");  
                break;
            case 7:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");                
                Link7.Attributes.Add("class", "current");
                Link8.Attributes.Add("class", "last");
                break;
            case 8:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "current last");
                break;
            default:
                Link1.Attributes.Add("class", "");
                Link2.Attributes.Add("class", "");
                Link3.Attributes.Add("class", "");
                Link4.Attributes.Add("class", "");
                Link5.Attributes.Add("class", "");
                Link6.Attributes.Add("class", "");
                Link7.Attributes.Add("class", "");
                Link8.Attributes.Add("class", "last");
                break;

        }
        
    }
}
