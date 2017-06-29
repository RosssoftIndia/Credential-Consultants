using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
  {
    this.Load += new System.EventHandler(this.Page_Load);
  }

  private void Page_Load(object sender, System.EventArgs e)
  {

    if(Session["Session_name"]==null) 
    {
        Response.Redirect("~/Timeout.aspx");
    }
    InjectSessionExpireScript();    
  }

  // For  demo purpose the timeout is set to a smaller value. 
  //Remember The Javascript setTimeout works in milliseconds. 
  protected void InjectSessionExpireScript( )
  {
      RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();    

      int sessiontime = Convert.ToInt32(app.SessionTime);
      string script = "<script> \n" +
      "function expireSession(){ \n" +
      " window.location = '" + "Timeout.aspx" + "'}\n" +
      "setTimeout('expireSession()', " + sessiontime * 60 * 1000 + " ); \n" +
      "</script>";
    this.Page.RegisterClientScriptBlock("expirescript",script);
  }  
}




