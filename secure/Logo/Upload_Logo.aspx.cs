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

public partial class secure_Logo_Upload_Logo : System.Web.UI.Page
{
    bool fileOK1 = false;
       bool fileOK2 = false;
    protected void page_init(object sender, EventArgs e)
    {

    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":               
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString() == "0")
            {
                lblresult.Text = "Logo Uploaded";
            }
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        lblresult.Text = ""; 
          string filename ="";
          if (drpclient.SelectedValue.ToString() != "0")
          {
              filename = drpclient.SelectedValue.ToString();
          }

          if (drpsubclient.SelectedValue.ToString() != "0")
          {
              filename = drpsubclient.SelectedValue.ToString();
          }

            if ((FileUpload1.HasFile))
            {

                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String allowedExtensions = ".png";

                if (fileExtension == allowedExtensions)
                {
                    fileOK1 = true;
                }
               
                if ((fileOK1))
                {
                    string imageFilePath = Server.MapPath("~/Assets/Logo/") + filename + ".png";

                    if (File.Exists(imageFilePath))
                    {                                        
                        System.IO.File.Delete("imageFilePath");
                    }
                    FileUpload1.SaveAs(Server.MapPath("~/Assets/Logo/") + filename + ".png");
                    Response.Redirect("~/secure/Logo/Upload_Logo.aspx?id=0");
                   
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
           MasterAdmin.Utility.dpsubclients(drpclient.SelectedValue.ToString(),drpsubclient);
        }
        else
        {
            drpsubclient.Items.Clear();
        }
    }
}

           
