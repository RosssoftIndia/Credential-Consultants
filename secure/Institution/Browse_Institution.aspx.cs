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

public partial class secure_Institution_Browse_Institution : System.Web.UI.Page
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
 
    protected void grid_institution_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Convert.ToInt32(Request.QueryString["t1"].ToString()))
            {
                case 0:
                    searchoption0.SelectedIndex = 0;
                    break;
                case 1:
                    searchoption0.SelectedIndex = 1;
                    break;
            }
            switch (Convert.ToInt32(Request.QueryString["t2"].ToString()))
            {
                case 0:
                    searchoption1.SelectedIndex = 0;
                    break;
                case 1:
                    searchoption1.SelectedIndex = 1;
                    break;
            }
            switch (Convert.ToInt32(Request.QueryString["t4"].ToString()))
            {
                case 0:
                    searchoption3.SelectedIndex = 0;
                    break;
                case 1:
                    searchoption3.SelectedIndex = 1;
                    break;
            }
            switch (Convert.ToInt32(Request.QueryString["t3"].ToString()))
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

    protected void grid_institution_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_institution.PageIndex = e.NewPageIndex;
        action();   
       
    }

    protected void grid_institution_DataBound(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":               
                grid_institution.Columns[5].Visible = false;                     
                break;
            case "ADMIN":
                grid_institution.Columns[6].Visible = false;     
                grid_institution.Columns[7].Visible = true;
                foreach (GridViewRow row in grid_institution.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        ImageButton delete = ((ImageButton)row.FindControl("del"));
                        HyperLink url = (HyperLink)row.FindControl("HyperLink1");

                        delete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to delete this Institution ?\\n \"" + url.Text + "\" ')");

                    }
                }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        foreach (GridViewRow row in grid_institution.Rows)
        {
            //url
            HyperLink url = (HyperLink)row.FindControl("HyperLink1");
            Label institution_id = (Label)row.FindControl("institution_id");
            Label role = (Label)row.FindControl("Role");
            url.NavigateUrl = "~/secure/Institution/update_Institution.aspx?instid=" + institution_id.Text.ToString() + "&role=" + role.Text.ToString() + "&search=" + searchbox.Text + "&t1=" + searchoption0.SelectedIndex.ToString() + "&t2=" + searchoption1.SelectedIndex.ToString() + "&t3=" + searchoption2.SelectedIndex.ToString() + "&t4=" + searchoption3.SelectedIndex.ToString(); 

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
        
            //Confirmation state true or false
            Label confirm = ((Label)row.FindControl("lblconfirmed"));          
            if (confirm.Text == "1") { confirm.Text = "True"; } else { confirm.Text = "False"; }

            //disable promote and promote Description
            ImageButton btndescription = (ImageButton)row.FindControl("btndescription");
            ImageButton btninstitution = (ImageButton)row.FindControl("btninstitution");         
            ImageButton btnEnable = (ImageButton)row.FindControl("btn");
            Label isEnable = (Label)row.FindControl("IsEnable");

            if (isEnable.Text == "1")
            {
                btnEnable.ImageUrl = "~/secure/Code/button/Disable.png";
                row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }
            else
            {
                row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
            }     

            //ImageButton millbtn = (ImageButton)row.FindControl("millbtn");
            //Label Isdegreemill = (Label)row.FindControl("Isdegreemill");
            //if (Isdegreemill.Text == "1")
            //{
            //    millbtn.ImageUrl = "~/secure/Code/button/Disable.png";
               
            //}
            

            if (lblcdes.Text == "--")
            {
                btnEnable.Visible = false;
                btndescription.Enabled = false;
                btndescription.ImageUrl = "~/secure/Code/button/Desdisable.png";
            }
           
            Label lblrole = (Label)row.FindControl("Role");
            if (lblrole.Text != "Client")
            {
                btninstitution.Enabled  = false;
                 btninstitution.ImageUrl = "~/secure/Code/button/Insdisable.png";
               
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
                row.Cells[3].BackColor = System.Drawing.Color.White;
                row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
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
            case "btninstitution":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        HyperLink institutionname = (HyperLink)grdRow.FindControl("HyperLink1");
                        string cnf = "";
                        Label lblcountryId = (Label)grdRow.FindControl("country_id");
                        Label confirm = ((Label)grdRow.FindControl("lblconfirmed"));
                        Label mill = ((Label)grdRow.FindControl("lblmill"));
                        Label type = ((Label)grdRow.FindControl("lblType"));
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        Label id = (Label)grdRow.FindControl("institution_id");
                        if (confirm.Text == "True") { cnf = "1"; } else { cnf = "0"; }
                        result = MasterAdmin.Utility.Grid_InstitutionPromote(institutionname.Text, lblcountryId.Text, cnf, type.Text, clientdes.Text, id.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString(),mill.Text);
                        break;
                }
                break;

            case "btndescription":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        Label id = (Label)grdRow.FindControl("institution_id");
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        result = MasterAdmin.Utility.Grid_InstitutionDescPromote(clientdes.Text, id.Text, Session["Customer_id"].ToString());
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
                Label id = (Label)grdRow.FindControl("institution_id");
                bool result = ClientAdmin.Utility.Grid_InstitutionActiveDesc(isEnable.Text, id.Text, Session["Admin_Customer"].ToString());
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
    protected void millbtn_Click(object sender, ImageClickEventArgs e)
    {

        //ImageButton btn = (ImageButton)sender;
        //GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        //Label isEnable = (Label)grdRow.FindControl("Isdegreemill");        
        //Label id = (Label)grdRow.FindControl("institution_id");
        //bool result = ClientAdmin.Utility.Grid_DegreeMill(isEnable.Text, id.Text);
        //if (result)
        //{
        //    action();
        //}

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
                ClientAdmin.Utility.Grid_SearchInstitution(grid_institution, searchbox.Text, Session["Admin_Customer"].ToString(), app.AdminId, searchoption2.SelectedValue.ToString(), searchoption1.SelectedItem.ToString(), searchoption0.SelectedItem.ToString(), searchoption3.SelectedValue.ToString());
                break;
            case "ADMIN":
                MasterAdmin.Utility.Grid_SearchInstitution(grid_institution, searchbox.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString(), searchoption2.SelectedValue.ToString(), searchoption1.SelectedItem.ToString(), searchoption0.SelectedItem.ToString(), searchoption3.SelectedValue.ToString());
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
        Label rowid = (Label)grdRow.FindControl("institution_id");
        bool result = MasterAdmin.Utility.del_Institution(rowid.Text);
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
