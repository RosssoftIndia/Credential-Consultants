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

public partial class secure_Edit_Application_Status : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            ClientAdmin.Utility.GetEmployees(dpemployee, Convert.ToInt32(Session["Admin_Customer"].ToString()));
            ClientAdmin.Utility.Select_Employee(txtamount, dpemployee, Session["Trackingcode"].ToString());

        }

      
        btnview.PostBackUrl = "~/secure/View_Application.aspx?tc=" + Session["Trackingcode"].ToString();
        btnedit.PostBackUrl = "~/secure/Edit_Application.aspx?tc=" + Session["Trackingcode"].ToString(); 
        btnreport.PostBackUrl = "~/secure/Report_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btneval.PostBackUrl = "~/secure/Evaluate.aspx?tc=" + Session["Trackingcode"].ToString();  
        btnattach.PostBackUrl = "~/secure/Attachments.aspx?tc=" + Session["Trackingcode"].ToString();  

        grid_Notes_Load();
        ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname, lblcompany, Session["Trackingcode"].ToString());
    }
 
    protected void Update_Click(object sender, EventArgs e)
    {
       //Button Updatebtn = (Button)sender;
        DropDownList chk1 = (DropDownList)DetailsView_ApplEdit.FindControl("chk1");
        DropDownList chk2 = (DropDownList)DetailsView_ApplEdit.FindControl("chk2");
        DropDownList chk3 = (DropDownList)DetailsView_ApplEdit.FindControl("chk3");
        DropDownList chk4 = (DropDownList)DetailsView_ApplEdit.FindControl("chk4");
        DropDownList chk5 = (DropDownList)DetailsView_ApplEdit.FindControl("chk5");
        DropDownList chk6 = (DropDownList)DetailsView_ApplEdit.FindControl("chk6");
        DropDownList chk7 = (DropDownList)DetailsView_ApplEdit.FindControl("chk7");
        DropDownList chk8 = (DropDownList)DetailsView_ApplEdit.FindControl("chk8");
        int parm1 = 0, parm2 = 0, parm3 = 0, parm4 = 0, parm5 = 0, parm6 = 0, parm7 = 0, parm8 = 0;
        if (chk1.SelectedValue.ToString() == "1"){parm1 = 1;}else{parm1 = 0;}
        if (chk2.SelectedValue.ToString() == "1") { parm2 = 1; } else { parm2 = 0; }
        if (chk3.SelectedValue.ToString() == "1") { parm3 = 1; } else { parm3 = 0; }
        if (chk4.SelectedValue.ToString() == "1") { parm4 = 1; } else { parm4 = 0; }
        if (chk5.SelectedValue.ToString() == "1") { parm5 = 1; } else { parm5 = 0; }
        if (chk6.SelectedValue.ToString() == "1") { parm6 = 1; } else { parm6 = 0; }
        if (chk7.SelectedValue.ToString() == "1") { parm7 = 1; } else { parm7 = 0; }
        if (chk8.SelectedValue.ToString() == "1") { parm8 = 1; } else { parm8 = 0; }
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Update_ApplEdit(parm1, parm2, parm3, parm4, parm5, parm6, parm7, parm8, Session["Trackingcode"].ToString(), Session["User"].ToString());
                if ((parm1 == 1) & (parm2 == 1) & (parm3 == 1) & (parm4 == 1) & (parm5 == 1) & (parm6 == 1) & (parm7 == 1) & (parm8 == 1))
                {
                  ClientAdmin.Utility.updateArchieve(Session["Trackingcode"].ToString(),Session["User"].ToString());
                }
                else if ((parm1 == 1) & (parm2 == 1) & (parm3 == 1))
                {
                    //string Requestid = ClientAdmin.Utility.GetRequestid(Session["Trackingcode"].ToString());
                    //if (Requestid != "")
                    //{
                    //    string call = "call_myservice(" + Requestid + ")";
                    //    //Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", call, true);
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "call_my_service", call, true);
                    //}
                }
                
                break;
            case "ADMIN":             
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
     
       if (result == true)
       {
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Status Updated'); window.location='/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString() + "';", true);              
          // Response.Redirect("~/secure/Edit_Application.aspx?tc=" + Session["Trackingcode"].ToString());
       }
        
    }
    protected void DetailsView_ApplEdit_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.DetailsView_ApplEdit(Session["Trackingcode"].ToString(), DetailsView_ApplEdit);   
                    break;
                case "ADMIN":             
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
            
        }
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        Label msg = (Label)DetailsView_status.FindControl("Result");
        Button btn = (Button)sender;
        switch (btn.ID)
        {
            case "internal_Add":
                TextBox internalNote = (TextBox)DetailsView_status.FindControl("internalNote");                
                ClientAdmin.Utility.Grid_internalNotesAdd(Session["Trackingcode"].ToString(), internalNote.Text);
                break;
            case "applicant_Add":
                TextBox applicantNote = (TextBox)DetailsView_applicant.FindControl("applicantNote");               
                ClientAdmin.Utility.Grid_applicantNotesAdd(Session["Trackingcode"].ToString(), applicantNote.Text,"Admin");               
                break;
        }

        Response.Redirect("~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString());
        
       
    }

    protected void grid_Notes_Load()
    {        
        ClientAdmin.Utility.Grid_internalNotesBrowse(Grid_internalNotes, Session["Trackingcode"].ToString());
        
        ClientAdmin.Utility.Grid_applicantNotesBrowse(Grid_applicantNotes, Session["Trackingcode"].ToString(),"Client");
        ClientAdmin.Utility.Grid_applicantNotesBrowse(Grid_status, Session["Trackingcode"].ToString(),"Admin");
    }
   
    protected void Update1_Click(object sender, EventArgs e)
    {

        TextBox fileno = (TextBox)DetailsView_service.FindControl("file");
        TextBox infno = (TextBox)DetailsView_service.FindControl("internal");
        bool result= false;
         switch (Session["Admin_Type"].ToString())
         {
             case "USER":
                 result = ClientAdmin.Utility.Grid_internalUpdate(infno.Text, fileno.Text);
                 break;
             case "ADMIN":              
                 break;
             default:
                 Response.Redirect("~/Fail.aspx");
                 break;
         }
       
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Internal Fileno Updated'); window.location='/secure/Edit_Application_Status.aspx?tc="+ Session["Trackingcode"].ToString()+ "';", true);              
           // Response.Redirect("~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString());
        }

    }
    protected void DetailsView_service_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_internalSelect(DetailsView_service, Session["Trackingcode"].ToString());
                    break;
                case "ADMIN":                  
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
           
        }

    }
   
    protected void status_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("msgid");
        ClientAdmin.Utility.Grid_applicantNotesdel(id_control.Text.ToString());
        grid_Notes_Load();
    }
    protected void internal_del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("msgid");
        ClientAdmin.Utility.Grid_internalNotesdel(id_control.Text.ToString());
        grid_Notes_Load();

    }
    protected void Grid_internalNotes_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grid_internalNotes.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("internal_del"));
                Label info = ((Label)row.FindControl("note"));

                delete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this Internal note ?\\n \"" + info.Text + "\" ')");

            }
        }
    }
    protected void Grid_status_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grid_status.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delete = ((ImageButton)row.FindControl("status_del"));
                Label info = ((Label)row.FindControl("note"));

                delete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete this Status note ?\\n \"" + info.Text + "\" ')");

            }
        }
    }
    protected void Update_Employeeinfo (object sender, EventArgs e)
    {
       bool result = ClientAdmin.Utility.Update_Employee(txtamount.Text, dpemployee.SelectedValue.ToString(), Session["Trackingcode"].ToString());
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Employee Linked'); window.location='/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString() + "';", true);
           
        }
    }
}
