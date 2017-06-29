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
using System.IO; 

public partial class secure_Logo_Browse_Logo : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                drpclient.SelectedValue = Request.QueryString["id"].ToString();
                action();
            }
        }

    }

    protected void drpclient_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.dpgetclient(drpclient);
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }
    }
    protected void drpclient_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpclient.SelectedValue != "0")
        {
            MasterAdmin.Utility.dpsubclients(drpclient.SelectedValue.ToString(), drpsubclient);
        }
        else 
        {
            drpsubclient.Items.Clear();  
        }
    }
    protected void searchbtn_Click(object sender, ImageClickEventArgs e)
    {

        action(); 
      
    }

   
    protected void del_Click(object sender, ImageClickEventArgs e)
    {

        string Client = "";
        if (drpclient.SelectedValue.ToString() != "0")
        {
            Client = drpclient.SelectedValue.ToString();
        }
        if (drpsubclient.SelectedValue.ToString() != "0")
        {
            Client = drpsubclient.SelectedValue.ToString();
        }

         string path ="~/Assets/Logo/"+ Client +".png";
         if (File.Exists(Server.MapPath(path)))
         {
             File.Delete(Server.MapPath(path));
         }
         Response.Redirect("~/secure/Logo/Browse_Logo.aspx?id=" + Client);
         //action();      
       
    }

    public void action()
    {
        holder.Visible = true;
        string Client = "";
        if (drpclient.SelectedValue.ToString() != "0")
        {
            Client = drpclient.SelectedValue.ToString();            
        }

        if (drpsubclient.SelectedValue.ToString() != "0")
        {
            Client = drpsubclient.SelectedValue.ToString();        
        }
      

            string path ="~/Assets/Logo/"+ Client +".png";
            if (File.Exists(Server.MapPath(path)))
            {
                logo.ImageUrl = path;
                del.Visible = true;
            }
            else
            {
                logo.ImageUrl = "~/images/nologo.png";
                del.Visible = false;  
            }
        
    }
}
