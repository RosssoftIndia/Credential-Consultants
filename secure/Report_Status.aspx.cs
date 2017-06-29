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

public partial class secure_Report_Status : System.Web.UI.Page
{

   protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Trackingcode"] = Request.QueryString["Tc"]; 
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }        

    }              
   

    protected void Page_Load(object sender, EventArgs e)
    {
        btnview.PostBackUrl = "~/secure/View_Application.aspx?tc=" + Session["Trackingcode"].ToString();
        btnedit.PostBackUrl = "~/secure/Edit_Application.aspx?tc=" + Session["Trackingcode"].ToString();     
        btnstatus.PostBackUrl = "~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btneval.PostBackUrl = "~/secure/Evaluate.aspx?tc=" + Session["Trackingcode"].ToString();  
        btnattach.PostBackUrl = "~/secure/Attachments.aspx?tc=" + Session["Trackingcode"].ToString();  

        ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname, lblcompany, Session["Trackingcode"].ToString());
    }  
    protected void grid_Notes_Load(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                 ClientAdmin.Utility.Grid_internalNotesBrowse(grid_Notes, Session["Trackingcode"].ToString());
                break;
            case "ADMIN":              
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }      
     
    }
    protected void grid_Notes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Notes.PageIndex = e.NewPageIndex;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_internalNotesBrowse(grid_Notes, Session["Trackingcode"].ToString());
                break;
            case "ADMIN":               
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }    
        
    }
    protected void rec_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("lblid");
        bool result = ClientAdmin.Utility.Grid_internalNotesdel(id_control.Text);
        if (result)
        {
            Response.Redirect("~/secure/Report_Status.aspx?tc=" + Session["Trackingcode"].ToString());
        }
    }
    protected void grid_Notes_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_Notes.Rows)
        {
            ImageButton l = (ImageButton)row.FindControl("btndelete");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this Internal Note')");
        }
    }
}
