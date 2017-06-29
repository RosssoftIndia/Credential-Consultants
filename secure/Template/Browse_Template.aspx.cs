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

public partial class secure_Template_Browse_Template : System.Web.UI.Page
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
            MasterAdmin.Utility.dpsubclients(drpclient.SelectedValue.ToString(), drpsubclient);
        }
        else 
        {
            drpsubclient.Items.Clear();  
        }
    }
    protected void searchbtn_Click(object sender, ImageClickEventArgs e)
    {

        action(); 
      
    }

    protected void PersistRowIndex(FileInfo[] FIlist, string client, string path, DataTable dtCollection)
    {
        if (FIlist.Length > 0)
        {

            foreach (FileInfo FI in FIlist)
            {
                DataRow drrow = dtCollection.NewRow();
                drrow[0] = client;
                drrow[1] = FI.Name;
                drrow[2] = path;
                dtCollection.Rows.Add(drrow);
            }
            grid_Template.DataSource = dtCollection;           
        }



    }
    protected void del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        Label path = (Label)grdRow.FindControl("path");
        Label tempname = (Label)grdRow.FindControl("lblfilename");
        File.Delete(path.Text + tempname.Text);
        action(); 
    }

    public void action()
    {
        string folder = "";
        string Client = "";
        if (drpclient.SelectedValue.ToString() != "0")
        {
            folder = drpclient.SelectedValue.ToString();
            Client = drpclient.SelectedItem.ToString();
        }

        if (drpsubclient.SelectedValue.ToString() != "0")
        {
            folder = drpsubclient.SelectedValue.ToString();
            Client = drpsubclient.SelectedItem.ToString();
        }
        if (folder != "")
        {
            DataTable dtCollection = new DataTable();
            dtCollection.Rows.Clear();
            dtCollection.Columns.Clear();
            dtCollection.Columns.Add("Client");
            dtCollection.Columns.Add("Template Name");
            dtCollection.Columns.Add("Path");

            string path = Server.MapPath("~/Assets/Template/" + folder + "/");
            if (Directory.Exists(path))
            {
                DirectoryInfo dirinfo = new DirectoryInfo(path);
                FileInfo[] folderlist = dirinfo.GetFiles();
                PersistRowIndex(folderlist, Client, path, dtCollection);

            }
            grid_Template.DataBind();  
        }  
    }
}
