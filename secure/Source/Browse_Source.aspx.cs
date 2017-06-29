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

public partial class secure_Source_Browse_Source : System.Web.UI.Page
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
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                opt.Visible = false;
                break;
            case "ADMIN":
                opt.Visible = true;
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }      
    }    
  

    protected void grid_souce_DataBound(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                grid_source.Columns[4].Visible = false;
                break;
            case "ADMIN":
                grid_source.Columns[5].Visible = false;
                grid_source.Columns[6].Visible = true;
                foreach (GridViewRow row in grid_source.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        ImageButton delete = ((ImageButton)row.FindControl("del"));
                        HyperLink url = (HyperLink)row.FindControl("HyperLink1");

                        delete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to delete this Source ?\\n \"" + url.Text + "\" ')");

                    }
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        foreach (GridViewRow row in grid_source.Rows)
        {
            //url
            HyperLink url = (HyperLink)row.FindControl("HyperLink1");
            Label source_id = (Label)row.FindControl("source_id");
            Label role = (Label)row.FindControl("Role");
            url.NavigateUrl = "~/secure/Source/Update_Source.aspx?sid=" + source_id.Text.ToString() + "&role=" + role.Text.ToString() + "&search=" + searchbox.Text + "&t1=" + searchoption1.SelectedIndex.ToString();
        
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

            //disable promote and promote Description           
            ImageButton btndescription = (ImageButton)row.FindControl("btndescription");           
            ImageButton btnEnable = (ImageButton)row.FindControl("btn");
            Label isEnable = (Label)row.FindControl("IsEnable");

            if (isEnable.Text == "1")
            {
                btnEnable.ImageUrl = "~/secure/Code/button/Disable.png";
                row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }
            else
            {
                row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }
           
            if (lblcdes.Text == "--")
            {
                btnEnable.Visible = false;
                btndescription.Enabled = false;
                btndescription.ImageUrl = "~/secure/Code/button/Desdisable.png";
            }
           
        } 
    }
    protected void grid_souce_Load(object sender, EventArgs e)
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
            searchbox.Text = Request.QueryString["search"].ToString();
            action();
        }
    }
    protected void grid_souce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grid_source.PageIndex = e.NewPageIndex;
        action();    
    }
    protected void promote(object sender, ImageClickEventArgs e)
    {
        bool result = false;
        ImageButton btn = (ImageButton)sender;
        switch (btn.ID)
        {
            case "btndescription":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        Label id = (Label)grdRow.FindControl("source_id");
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        result = MasterAdmin.Utility.Grid_SourceDescPromote(clientdes.Text, id.Text, Session["Customer_id"].ToString());
                        break;
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        if (result)
        {
            action();
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
                Label id = (Label)grdRow.FindControl("source_id");
                bool result = ClientAdmin.Utility.Grid_SourceActiveDesc(isEnable.Text, id.Text, Session["Admin_Customer"].ToString());
                if (result)
                {
                    action();
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
                 ClientAdmin.Utility.Grid_SearchSource(grid_source, searchbox.Text, Session["Admin_Customer"].ToString(), app.AdminId, searchoption1.SelectedItem.ToString());
                 break;
             case "ADMIN":
                 MasterAdmin.Utility.Grid_SearchSource(grid_source, searchbox.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString(), searchoption1.SelectedItem.ToString());
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
         Label rowid = (Label)grdRow.FindControl("source_id");
         bool result = MasterAdmin.Utility.del_Source(rowid.Text);
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

