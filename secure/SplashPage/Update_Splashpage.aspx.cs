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
using CKEditor.NET;
using System.IO;


public partial class secure_SplashPage_Splashpage : System.Web.UI.Page
{
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Client_id"] = Request.QueryString["clid"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }  

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }  

    protected void Update_Click(object sender, EventArgs e)
    {
        RadioButtonList rbtn = (RadioButtonList)DetailsView_Splash.FindControl("Rbt");
        FileUpload uploader = (FileUpload)DetailsView_Splash.FindControl("Appuploader");
        Label clientid = (Label)DetailsView_Splash.FindControl("lblclientid");
        if (uploader.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(uploader.FileName).ToLower();
            String[] allowedExtensions = { ".pdf" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    string folder = clientid.Text; 

                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Assets/OfflineApp/" + folder));
                    ArrayList list = new ArrayList();
                    if (dirInfo.Exists)
                    {
                      
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Assets/OfflineApp/" + folder));
                    }

                    uploader.SaveAs(Server.MapPath("~/Assets/OfflineApp/" + folder) + "/Application.pdf");
                    action();
                }
                else { ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Wrong File Extension!Pdf is the only supported File Format')</script>"); }
            }
        }
        else
        {
            action();
        }   
       
    }

    protected void DetailsView_Splash_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Splashpage_Browse(DetailsView_Splash, Session["Client_id"].ToString());
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }           
      

        }
    }

    public void action()
    {
        RadioButtonList rbtn = (RadioButtonList)DetailsView_Splash.FindControl("Rbt");
        CKEditorControl Appmsg = (CKEditorControl)DetailsView_Splash.FindControl("Appinst");
        CKEditorControl browsermsg = (CKEditorControl)DetailsView_Splash.FindControl("brinst");
        //CKEditorControl Clientmsg = (CKEditorControl)DetailsView_Splash.FindControl("clinst");
        CKEditorControl Offlineappmsg = (CKEditorControl)DetailsView_Splash.FindControl("offinst");
        Label clientid = (Label)DetailsView_Splash.FindControl("lblclientid");
        RadioButtonList onlineapp = (RadioButtonList)DetailsView_Splash.FindControl("onlineapp");
        int offlineapp = 0;
        if (rbtn.SelectedValue == "True") { offlineapp = 1; }
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Splashpage_Update(browsermsg.Text, Appmsg.Text, " ", offlineapp, Offlineappmsg.Text, clientid.Text, Convert.ToInt32(onlineapp.SelectedValue.ToString()));
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        if (result == true)
        {
            Response.Redirect("~/secure/SplashPage/Browse_Splashpage.aspx?clid="+clientid.Text);
        }
    }
    protected void DetailsView_Splash_DataBound(object sender, EventArgs e)
    {
        Label msg = (Label)DetailsView_Splash.FindControl("lblpdf");
        Panel pdfpanel = (Panel)DetailsView_Splash.FindControl("pdfpanel");
        HyperLink btn = (HyperLink)DetailsView_Splash.FindControl("Imgpdf");
        Label clientid = (Label)DetailsView_Splash.FindControl("lblclientid");

        if (File.Exists(Server.MapPath("~/Assets/OfflineApp/" + clientid.Text + "/Application.pdf")))
        {
            msg.Visible = false;
            pdfpanel.Visible = true;
            btn.NavigateUrl = "~/Assets/OfflineApp/" + clientid.Text + "/Application.pdf";
        }
        else { pdfpanel.Visible = false; msg.Visible = true; }     
        Label clienttop = (Label)DetailsView_Splash.FindControl("clienttop");
        Label clientbottom = (Label)DetailsView_Splash.FindControl("clientbottom");
        string client = ClientAdmin.Utility.GetclientName(Convert.ToInt32(Session["Client_id"].ToString()));
        clienttop.Text = client;
        clientbottom.Text = client;
    }
}
