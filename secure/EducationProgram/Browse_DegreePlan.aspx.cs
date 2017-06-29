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

public partial class secure_EducationProgram_Browse_DegreePlan : System.Web.UI.Page
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

    }    
    protected void grid_Degree_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Convert.ToInt32(Request.QueryString["t1"].ToString()))
            {
                case 0:
                     searchoption1.SelectedIndex = 0; 
                    break;
                case 1:
                    searchoption1.SelectedIndex = 1;
                    break;
            }
            switch (Convert.ToInt32(Request.QueryString["t2"].ToString()))
        {
                case 0:
                    searchoption2.SelectedIndex = 0;
                break;
                case 1:
                    searchoption2.SelectedIndex = 1;
                break;
                case 2:
                    searchoption2.SelectedIndex = 2;
                break;
        }             
     

            searchbox.Text = Request.QueryString["search"].ToString();
            action();
        }
     
    }
    protected void grid_Degree_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Degree.PageIndex = e.NewPageIndex;
        action();     
     
    }
    protected void grid_Degree_DataBound(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                grid_Degree.Columns[6].Visible = false;
                break;
            case "ADMIN":
                grid_Degree.Columns[7].Visible = false;
                grid_Degree.Columns[8].Visible = true;
                foreach (GridViewRow row in grid_Degree.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        ImageButton delete = ((ImageButton)row.FindControl("del"));
                        HyperLink url = (HyperLink)row.FindControl("HyperLink1");

                        delete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to delete this Education Program ?\\n \"" + url.Text + "\" ')");

                    }
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        foreach (GridViewRow row in grid_Degree.Rows)
        {
            //url
            HyperLink url = (HyperLink)row.FindControl("HyperLink1");
            Label degree_id = (Label)row.FindControl("lbldegreeId");
            Label role = (Label)row.FindControl("Role");
            url.NavigateUrl = "~/secure/EducationProgram/Update_DegreePlan.aspx?Degid=" + degree_id.Text.ToString() + "&role=" + role.Text.ToString() + "&search=" + searchbox.Text + "&t1=" + searchoption1.SelectedIndex.ToString() + "&t2=" + searchoption2.SelectedIndex.ToString(); 

            //tooltip
            int limit = Convert.ToInt32(app.deslimit);
            Label lblcdes = (Label)row.FindControl("lblclientdes");
            string clientdes = (System.Text.RegularExpressions.Regex.Replace(Server.HtmlDecode(lblcdes.Text), @"<[^>]*>", string.Empty)).Replace("&nbsp;", "");
            if (clientdes.Length > limit) { lblcdes.Text = clientdes.Substring(0, limit) + "..."; } else { lblcdes.Text = clientdes; }
            lblcdes.ToolTip = clientdes;
            Label lblades = (Label)row.FindControl("lbladmindes");
            string admindes = (System.Text.RegularExpressions.Regex.Replace(Server.HtmlDecode(lblades.Text), @"<[^>]*>", string.Empty)).Replace("&nbsp;", "");
            if (admindes.Length > limit) { lblades.Text = admindes.Substring(0, limit) + "..."; } else { lblades.Text = admindes; }
            lblades.ToolTip = admindes;
            Label confirm = ((Label)row.FindControl("lblconfirmed"));
            if (confirm.Text == "1") { confirm.Text = "True"; } else { confirm.Text = "False"; }

            //disable promote and promote Description
            ImageButton btndescription = (ImageButton)row.FindControl("btndescription");
            ImageButton btndegree = (ImageButton)row.FindControl("btndegree");
            ImageButton btnEnable = (ImageButton)row.FindControl("btn");
            Label isEnable = (Label)row.FindControl("IsEnable");

            if (isEnable.Text == "1")
            {
                btnEnable.ImageUrl = "~/secure/Code/button/Disable.png";
                row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }
            else
            {
                row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }     

            if (lblcdes.Text == "--")
            {
                btnEnable.Visible = false;
                btndescription.Enabled = false;
                btndescription.ImageUrl = "~/secure/Code/button/Desdisable.png";
            }              
            Label lblrole = (Label)row.FindControl("Role");
            if (lblrole.Text != "Client")
            {
                btndegree.Enabled = false;
                btndegree.ImageUrl = "~/secure/Code/button/Degdisabled.png";

            }
            else
            {
                if (Session["Admin_Type"].ToString() == "ADMIN")
                {
                    HyperLink linkname = (HyperLink)row.FindControl("HyperLink1");
                    linkname.NavigateUrl = "";
                }

                //only client data
                btnEnable.Visible = false;
                row.Cells[4].BackColor = System.Drawing.Color.White;
                row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
                btndescription.Enabled = false;
                btndescription.ImageUrl = "~/secure/Code/button/Desdisable.png";
            }
        }      
    }

    protected void promote(object sender, ImageClickEventArgs e)
    {
        bool result = false;
        ImageButton btn = (ImageButton)sender;
        switch (btn.ID)
        {
            case "btndegree":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        ImageButton promote = (ImageButton)sender;
                        GridViewRow grdRow = (GridViewRow)promote.Parent.Parent as GridViewRow;
                        Label id = (Label)grdRow.FindControl("lbldegreeId");
                        result = MasterAdmin.Utility.Grid_DegreePlanPromote(id.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString());
                        break;        
            
                }
                break;

            case "btndescription":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        Label id = (Label)grdRow.FindControl("lbldegreeId");
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        result = MasterAdmin.Utility.Grid_DegreeDescPromote(clientdes.Text, id.Text, Session["Customer_id"].ToString());
                        break;
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        if (result)
        {
            action(); // Response.Redirect("~/secure/EducationProgram/Browse_DegreePlan.aspx");
        }
        else
        {
            Response.Redirect("~/Fail.aspx");
        }

    }
   
    protected void btn_Click(object sender, ImageClickEventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ImageButton btn = (ImageButton)sender;
                GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                Label isEnable = (Label)btn.FindControl("IsEnable");
                Label id = (Label)grdRow.FindControl("lbldegreeId");
                bool result = ClientAdmin.Utility.Grid_DegreeActiveDesc(isEnable.Text, id.Text, Session["Admin_Customer"].ToString());
                if (result)
                {
                    action();   // Response.Redirect("~/secure/EducationProgram/Browse_DegreePlan.aspx");
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }

    protected void searchbtn_Click(object sender, ImageClickEventArgs e)
    {        
        action();
                }
    public void action()
    {
       
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_SearchDegree(grid_Degree, searchbox.Text, Session["Admin_Customer"].ToString(), app.AdminId, searchoption2.SelectedValue.ToString(), searchoption1.SelectedItem.ToString());
                break;
            case "ADMIN":
                MasterAdmin.Utility.Grid_SearchDegree(grid_Degree, searchbox.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString(), searchoption2.SelectedValue.ToString(), searchoption1.SelectedItem.ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

    }

    protected void del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label rowid = (Label)grdRow.FindControl("lbldegreeId");
        bool result = MasterAdmin.Utility.del_Educationprogram(rowid.Text);
        if (result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deleted successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deletion failed.');", true);
        }
        action();

    }

}
