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

public partial class secure_Documents_Browse_Documents : System.Web.UI.Page
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


    protected void grid_Documents_DataBound(object sender, EventArgs e)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
               grid_Documents.Columns[5].Visible = false;
                break;
            case "ADMIN":
               grid_Documents.Columns[6].Visible = false;
               grid_Documents.Columns[7].Visible = true;
               foreach (GridViewRow row in grid_Documents.Rows)
               {
                   if (row.RowType == DataControlRowType.DataRow)
                   {
                       ImageButton delete = ((ImageButton)row.FindControl("del"));
                       HyperLink url = (HyperLink)row.FindControl("HyperLink1");

                       delete.Attributes.Add("onclick", "javascript:return " +
                       "confirm('Are you sure you want to delete this Document ?\\n \"" + url.Text + "\" ')");

                   }
               }
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }

        foreach (GridViewRow row in grid_Documents.Rows)
        {
            
            //url
            HyperLink url = (HyperLink)row.FindControl("HyperLink1");
            Label doc_id = (Label)row.FindControl("doc_id");
            Label role = (Label)row.FindControl("Role");
            url.NavigateUrl = "~/secure/Documents/Update_Documents.aspx?did=" + doc_id.Text.ToString() + "&role=" + role.Text.ToString() + "&search=" + searchbox.Text + "&t1=" + searchoption1.SelectedIndex.ToString();
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
            ImageButton btndocuments = (ImageButton)row.FindControl("btndocuments");
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
         
            Label lblrole = (Label)row.FindControl("Role");
            if (lblrole.Text != "Client")
            {
                btndocuments.Enabled = false;
                btndocuments.ImageUrl = "~/secure/Code/button/Docdisable.png";
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
                row.Cells[2].BackColor = System.Drawing.Color.White;
                row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#E3E1D2");
                btndescription.Enabled = false;
                btndescription.ImageUrl = "~/secure/Code/button/Desdisable.png";
            }
            ImageButton btn = (ImageButton)row.FindControl("btndownload");           
            string path = Server.MapPath("~/Assets/Documents/");
            if (!File.Exists(path+doc_id.Text+".pdf"))
            {
                btn.ImageUrl = "~/secure/Code/icons/pdf_icon_grey.png";
                btn.Enabled = false;
            }
            
        } 
    }
    protected void grid_Documents_Load(object sender, EventArgs e)
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
    protected void grid_Documents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grid_Documents.PageIndex = e.NewPageIndex;
        action();            

    }
    protected void promote(object sender, ImageClickEventArgs e)
    {
        bool result = false;
        ImageButton btn = (ImageButton)sender;
        switch (btn.ID)
        {
            case "btndocuments":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        HyperLink name = (HyperLink)grdRow.FindControl("HyperLink1");                       
                        Label lblcountryId = (Label)grdRow.FindControl("country_id");                     
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        Label id = (Label)grdRow.FindControl("doc_id");                      
                        result = MasterAdmin.Utility.Grid_DocumentsPromote(name.Text, lblcountryId.Text,clientdes.Text, id.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString());
                        break;
                }
                break;

            case "btndescription":
                switch (Session["Admin_Type"].ToString())
                {
                    case "ADMIN":
                        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
                        Label id = (Label)grdRow.FindControl("doc_id");
                        Label clientdes = (Label)grdRow.FindControl("lblCdes");
                        result = MasterAdmin.Utility.Grid_DocumentsDescPromote(clientdes.Text, id.Text, Session["Customer_id"].ToString());
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
                Label id = (Label)grdRow.FindControl("doc_id");
                bool result = ClientAdmin.Utility.Grid_DocumentsActiveDesc(isEnable.Text, id.Text, Session["Admin_Customer"].ToString());
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
                ClientAdmin.Utility.Grid_SearchDocuments(grid_Documents, searchbox.Text, Session["Admin_Customer"].ToString(), app.AdminId, searchoption1.SelectedItem.ToString());
                break;
            case "ADMIN":
                MasterAdmin.Utility.Grid_SearchDocuments(grid_Documents, searchbox.Text, Session["Customer_id"].ToString(), Session["Admin_Customer"].ToString(), searchoption1.SelectedItem.ToString());
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
        Label rowid = (Label)grdRow.FindControl("doc_id");
        bool result = MasterAdmin.Utility.del_Document(rowid.Text);
        if (result)
        {
             string path = Server.MapPath("~/Assets/Documents/");
             if (File.Exists(path + rowid.Text + ".pdf"))
             {
                 File.Delete(path + rowid.Text + ".pdf"); 
             }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deleted successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deletion failed.');", true);
        }
        action();

    }

    protected void btndownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        Label lblfilename = (Label)grdRow.FindControl("lblfilename");
        Label filename = (Label)grdRow.FindControl("doc_id");
        filename.Text = filename.Text + ".pdf";
        string apath = Server.MapPath("~/Assets/Documents/" + filename.Text);
        FileInfo file = new FileInfo(apath);
        if (file.Exists)
        {           
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", lblfilename.Text));
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = ReturnExtension(file.Extension.ToLower());
            Response.TransmitFile(file.FullName);
            Response.End();
        }


    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

}
