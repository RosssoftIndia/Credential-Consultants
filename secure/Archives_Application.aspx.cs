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

public partial class secure_Archives_Application : System.Web.UI.Page
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
            ClientAdmin.Utility.GetSubclients(dpsubclients, Convert.ToInt32(Session["Admin_Customer"].ToString()),true);
            ClientAdmin.Utility.Search_GetEmployees(dpemployee, Convert.ToInt32(Session["Admin_Customer"].ToString()));
            action();
            grid_Archives_Application.Columns[6].Visible = false;
        }
    }   
    protected void grid_Archives_Application_Load(object sender, EventArgs e)
    {
      
        
    }
    protected void grid_Archives_Application_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Archives_Application.PageIndex = e.NewPageIndex;
        action();       
        
    }
    protected void grid_Archives_Application_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_Archives_Application.Rows)
        {
            Label clientid = (Label)row.FindControl("lblclientid");
            Label client = (Label)row.FindControl("lblclient");
            client.Text = ClientAdmin.Utility.GetclientName(Convert.ToInt32(clientid.Text));
     
            if (row.RowType == DataControlRowType.DataRow)
            {

                ImageButton lnremove = (ImageButton)row.FindControl("btndelete");   
                if (Convert.ToInt32(row.RowIndex) % 2 == 1)
                {
                    lnremove.Attributes.Add("onclick", " this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.backgroundColor='#eeff00'; if (confirm('Are you sure you want to delete this Application?')) return true; else {this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.backgroundColor='#FCFAE9'; return false;}");

                }
                else { lnremove.Attributes.Add("onclick", " this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.backgroundColor='#eeff00'; if (confirm('Are you sure you want to delete this Application?')) return true; else {this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.backgroundColor='#ffffff'; return false;}"); }
             
            }
           
        }
    }
    protected void application_del_Click(object sender, EventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label id_control = (Label)grdRow.FindControl("lblfileno");
        Label clientid = (Label)grdRow.FindControl("lblclientid");
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Remove_Application(id_control.Text, clientid.Text);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Application deleted successfully.');", true);
                break;
            case "ADMIN":               
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }            
         
        action();
      //  grid_Archives_Application_Load(this, EventArgs.Empty);
        

       // ClientScript.RegisterStartupScript(GetType(), "dateSrpt", "<script>alert('Application deleted successfully.')</script>");
    }
    protected void searchbtn_Click(object sender, ImageClickEventArgs e)
    {
        action();
    }

    public void action()
    {
        string searchdata = "";
        string clients = "";
        if (dpsubclients.SelectedItem.ToString() == "All")
        {
            clients = ClientAdmin.Utility.GetAllSubclients(Convert.ToInt32(Session["Admin_Customer"].ToString()));
        }
        else
        {
            clients = dpsubclients.SelectedValue.ToString();
        }
        if (searchbox.Enabled == false)
        {
            if (searchbox.Text == "All")
            {
                searchdata = searchbox.Text;
            }
            else
            {
                searchdata = dpemployee.SelectedValue.ToString();
            }
        }
        else
        {
            searchdata = searchbox.Text;
        }


        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_ArchivesApplicant(grid_Archives_Application, clients, searchdata, searchoption1.SelectedItem.ToString());
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

    }

    protected void dpemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dpemployee.SelectedIndex != 0)
        {
            searchbox.Text = ClientAdmin.Utility.Search_GetEmployeesbyId(dpemployee.SelectedValue.ToString());
        }
        else
        {
            searchbox.Text = dpemployee.SelectedItem.ToString();
        }
    }
    protected void searchoption1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (searchoption1.SelectedValue == "Employee")
        {
            searchbox.Text = dpemployee.SelectedItem.ToString();
            searchbox.Enabled = false;
            dpemployee.Visible = true;
            grid_Archives_Application.Columns[6].Visible = true;

        }
        else
        {
            searchbox.Text = "";
            searchbox.Enabled = true;
            dpemployee.Visible = false;
            grid_Archives_Application.Columns[6].Visible = false;
        }
    }
}
