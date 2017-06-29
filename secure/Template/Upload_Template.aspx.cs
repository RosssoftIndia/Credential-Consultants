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

public partial class secure_Template_Upload_Template : System.Web.UI.Page
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
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        lblresult.Text = ""; 
          string folder ="";
          if (drpclient.SelectedValue.ToString() != "0")
          {
              folder = drpclient.SelectedValue.ToString();
          }

          if (drpsubclient.SelectedValue.ToString() != "0")
          {
              folder = drpsubclient.SelectedValue.ToString();
          }
       
        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Assets/Template/" + folder));
        ArrayList list = new ArrayList();
        if (dirInfo.Exists)
        {
            list.AddRange(dirInfo.GetFiles("*.docx"));
        }
        else
        {
            Directory.CreateDirectory(Server.MapPath("~/Assets/Template/" + folder));
        }
       
        bool result = ClientAdmin.Utility.TemplateName(txtName.Text,list);
        if (result != true)
        {
           // ValidationSummary1.Visible = true; 
            if ((FileUpload1.HasFile))
            {

                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String allowedExtensions = ".docx";

                if (fileExtension == allowedExtensions)
                {
                    fileOK1 = true;
                }
               
                if ((fileOK1))
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Assets/Template/") + folder+"/"+ txtName.Text +".docx");
                    lblresult.Text = "Template Uploaded";
                }

            }
        }
        else
        {
            lblresult.Text = "Template Name Exist";
          //  ValidationSummary1.Visible = false;
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

           
