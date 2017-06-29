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

public partial class secure_Evalpopup : System.Web.UI.Page
{
    protected void page_init(object sender, EventArgs e)
    {

    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["eduid"] = Request.QueryString["Tc"];
                Session["eduname"] =    ClientAdmin.Utility.Getinstitution(Request.QueryString["Insid"]);
                Session["Recordid"] = Request.QueryString["id"];
                Session["Cid"] = Request.QueryString["Cid"];
                Session["Lid"] = Request.QueryString["Lid"];
                Session["ClientId"] = Session["Admin_Customer"].ToString(); // ClientAdmin.Utility.clientidbyRequestid(Request.QueryString["Rid"].ToString());
                lbleducation.Text = Session["eduname"].ToString();
               lblid.Text = Session["eduid"].ToString();
               if (!Page.IsPostBack)
               {
                   txtissued.Text = ClientAdmin.Utility.DetailsView_Linkageselect(Session["Lid"].ToString(), "Issued_GPA");
                   txtconverted.Text = ClientAdmin.Utility.DetailsView_Linkageselect(Session["Lid"].ToString(), "Converted_GPA");
               }
               lblRid.Text = Session["Recordid"].ToString();  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }


    }
 
    protected void equivalency_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();

            DropDownList equidp = (DropDownList)sender;
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    Boolean result = getRecommendation(equidp);
                    ClientAdmin.Utility.GetEquivalency(equidp, Session["ClientId"].ToString() , app.AdminId, result);
                    if (Session["Lid"].ToString() != "0")
                    {
                        equidp.SelectedValue = ClientAdmin.Utility.DetailsView_Linkageselect(Session["Lid"].ToString(), "Equi");
                    }
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }

    }
    protected void gradescale_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            DropDownList gradedp = (DropDownList)sender;
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.GetGradescale(gradedp, Session["ClientId"].ToString(), app.AdminId, Session["Cid"].ToString());
                    if (Session["Lid"].ToString() != "0")
                    {
                        gradedp.SelectedValue = ClientAdmin.Utility.DetailsView_Linkageselect(Session["Lid"].ToString(), "grade");
                    }
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
        }

    }
    protected void btn_Click(object sender, EventArgs e)
    {        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
              result =  ClientAdmin.Utility.DetailsView_EvaluateAdd(lblid.Text, equivalency.SelectedValue.ToString(), gradescale.SelectedValue.ToString(),lnkname.Text,txtissued.Text,txtconverted.Text,lblRid.Text);    
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
        if (result)
        {
            btn.Visible = false;
        }
        else
        {
            Response.Redirect("~/Fail.aspx");
        }
        

    }


    public Boolean getRecommendation(DropDownList equidp)
    {
        Boolean result = false;
        if (!Page.IsPostBack)
        {       
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    result = ClientAdmin.Utility.GetRecommendation(Session["Recordid"].ToString(), equidp, Session["ClientId"].ToString(), app.AdminId);
                   break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
            
        }
        return result;
    }
}

           
