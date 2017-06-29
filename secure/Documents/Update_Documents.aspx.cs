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
using System.Net;

public partial class secure_Documents_Update_Documents : System.Web.UI.Page
{
     protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["document_id"] = Request.QueryString["did"];
                Session["des_role"] = Request.QueryString["role"];  
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
            switch (Session["Admin_Type"].ToString())
            {
                case "ADMIN":
                    Doc_grid.Visible = true;  
                    uploadblk.Visible = true;                   
                    break;
                default:
                    Doc_grid.Visible = false;
                    uploadblk.Visible = false; 
                    break; 
            }
      
        }
    } 
   
    protected void Update_Click(object sender, EventArgs e)
    {
        TextBox name = (TextBox)DetailsView_Documents.FindControl("name");
        DropDownList countrydp = (DropDownList)DetailsView_Documents.FindControl("countrydp");
        CKEditorControl des = (CKEditorControl)DetailsView_Documents.FindControl("destxt");
        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_DocumentsUpdate(name.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), Session["Admin_Customer"].ToString(), Convert.ToInt32(Session["document_id"].ToString()), des.Text, Session["des_role"].ToString());
                break;
            case "ADMIN":
                result = MasterAdmin.Utility.Grid_DocumentsUpdate(name.Text, Convert.ToInt32(countrydp.SelectedValue.ToString()), Convert.ToInt32(Session["document_id"].ToString()), Session["Customer_id"].ToString(), des.Text, Session["des_role"].ToString());
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }        
      
        if (result == true)
        {
            Response.Redirect("~/secure/Documents/Browse_Documents.aspx?search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString());
        }
    }
    protected void DetailsView_Documents_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Grid_DocumentsSelect(DetailsView_Documents, Convert.ToInt32(Session["document_id"].ToString()),Session["Admin_Customer"].ToString(), Session["des_role"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Grid_DocumentsSelect(DetailsView_Documents, Convert.ToInt32(Session["document_id"].ToString()), Session["des_role"].ToString());
                    HtmlGenericControl tab = (HtmlGenericControl)DetailsView_Documents.FindControl("extratab");
                    tab.Visible = false; 
                    load_grid();
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }                    
            DropDownList country = (DropDownList)DetailsView_Documents.FindControl("Countrydp");
            Label temp = (Label)DetailsView_Documents.FindControl("temp");
            country.SelectedValue = temp.Text; 
            
        }
    }
    protected void Countrydp_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList countrydp = (DropDownList)DetailsView_Documents.FindControl("countrydp");
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.Getcountry(countrydp, Session["Admin_Customer"].ToString());
                    break;
                case "ADMIN":
                    MasterAdmin.Utility.Getcountry(countrydp, Session["Customer_id"].ToString());
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }     
           
        }
    }
    protected void DetailsView_Documents_DataBound(object sender, EventArgs e)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                if (Session["des_role"].ToString() != "Client")
                {
                    DetailsView_Documents.Rows[0].Enabled = false;
                    DetailsView_Documents.Rows[1].Enabled = false;
                  
                }
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }       
    }
    protected void Swap_Click(object sender, EventArgs e)
    {
        CKEditorControl countrydes = (CKEditorControl)DetailsView_Documents.FindControl("destxt");
        HtmlGenericControl masterdesc = (HtmlGenericControl)DetailsView_Documents.FindControl("masterdesc");
        countrydes.Text = masterdesc.InnerHtml;
    }





    protected void upload_Click(object sender, EventArgs e)
    {
        bool result = false;
        string path = Server.MapPath("~/Assets/Documents/");
        if (Request.QueryString["did"] != null)
        {
            if (docuploader.HasFile)
            {

                if (docuploader.PostedFile.ContentLength < 11534336)
                {
                    string fileExt = System.IO.Path.GetExtension(docuploader.FileName).ToLower();
                    if (fileExt == ".pdf")
                    {
                        Upload(path, fileExt);
                    }
                    else { msg.Text = "You can only specify a pdf file."; }
                }
                else
                {

                    msg.Text = "File size of " + Convert.ToString((docuploader.PostedFile.ContentLength / 1024) / 1024) + " MB is exceeding the uploading limit.";

                }
            }
            else
            {
                msg.Text = "You have not specified a file.";
            }

        }
        load_grid();
    }
       protected bool Upload(string path, string fileExt)
       {          
           bool result = false;

           try
           {
               double fileinmb = Convert.ToDouble(docuploader.PostedFile.ContentLength) / 1048576;
               docuploader.SaveAs(path + Session["document_id"].ToString() +fileExt);
               msg.Text = "File Uploaded. Repeat to upload another PDF.";
               //msg.Text = "File name: " +
               //    docuploader.PostedFile.FileName + "<br>" + "( " +
               //    Convert.ToString(fileinmb.ToString(".00")) + " MB /" +
               //     Convert.ToString(docuploader.PostedFile.ContentLength / 1024) + " kb)<br>" +
               //    "Content type: " +
               //    docuploader.PostedFile.ContentType + "<br>" +
               //    "Uploded Successfully";
               MasterAdmin.Utility.Attachment_Documents(docuploader.PostedFile.FileName.ToString(), Session["document_id"].ToString(),"Add");
               AdminLoad();
              // Response.Redirect("~/secure/Documents/Update_Documents.aspx?did=" + Request.QueryString["did"].ToString() + "&role=" + Request.QueryString["role"].ToString() + "&search=" + Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString());
           }
           catch (Exception ex)
           {
               msg.Text = "ERROR: " + ex.Message.ToString();
           }

           return result;
       }
       protected void PersistRowIndex(string filename,string virtualfilename, string path, DataTable dtCollection)
       {

           DataRow drrow = dtCollection.NewRow();
           drrow[0] = virtualfilename; 
           drrow[1] = path;
           drrow[2] = filename;
           dtCollection.Rows.Add(drrow);

           Doc_grid.DataSource = dtCollection;
           //if (Session["Urole"].ToString() == "LV")
           //{
           //    Doc_grid.Columns[2].Visible = false;
           //}




       }
       protected void delete_Click(object sender, ImageClickEventArgs e)
       {
           ImageButton btn = (ImageButton)sender;
           GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
           Label filename = (Label)grdRow.FindControl("lblfilename");
           string apath = Server.MapPath("~/Assets/Documents/" + filename.Text);         
           if (File.Exists(apath))
           {
               File.Delete(apath);
               if (!File.Exists(apath))
               {
                   MasterAdmin.Utility.Attachment_Documents("", Session["document_id"].ToString(), "Clear");
                   AdminLoad();
                   //Response.Redirect("~/secure/Documents/Update_Documents.aspx?did=" +   Request.QueryString["did"].ToString() +"&role=" +Request.QueryString["role"].ToString()+"&search="+ Request.QueryString["search"].ToString() + "&t1=" + Request.QueryString["t1"].ToString());
               }
           }
       }
       protected void Doc_grid_DataBound(object sender, EventArgs e)
       {
           //foreach (GridViewRow row in Doc_grid.Rows)
           //{
           //    HyperLink btn = (HyperLink)row.FindControl("download");
           //    Label filename = (Label)row.FindControl("lblfilename");
           //    Label path = (Label)row.FindControl("lblpath");
           //    btn.NavigateUrl = path.Text + filename.Text;
           //}
       }
       protected void load_grid()
       {
           if (Request.QueryString["did"] != null)
           {
               DataTable dtCollection = new DataTable();
               dtCollection.Rows.Clear();
               dtCollection.Columns.Clear();
               dtCollection.Columns.Add("Vfile");
               dtCollection.Columns.Add("Path");
               dtCollection.Columns.Add("File");

               string filename = Request.QueryString["did"].ToString() + ".pdf";
               string path = "~/Assets/Documents/" + filename;
               if (File.Exists(Server.MapPath(path)))
               {
                   Label lblfilename = (Label)DetailsView_Documents.FindControl("lblfilename");
                  string virtualfilename = lblfilename.Text; 
                   PersistRowIndex(filename,virtualfilename, Request.QueryString["did"].ToString(), dtCollection);
               }


               Doc_grid.DataBind();
           }
       }
       protected void btndownload_Click(object sender, ImageClickEventArgs e)
       {
           ImageButton btn = (ImageButton)sender;
           GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
           Label filename= (Label)grdRow.FindControl("lblvirtualfilename");
           string apath = Server.MapPath("~/Assets/Documents/" + Request.QueryString["did"].ToString() + ".pdf");
           FileInfo file = new FileInfo(apath);
           if (file.Exists)
           {
               //try
               //{
               //    string strURL = apath;
               //    WebClient req = new WebClient();
               //    HttpResponse response = HttpContext.Current.Response;
               //    response.Clear();
               //    response.ClearContent();
               //    response.ClearHeaders();
               //    response.Buffer = true;
               //    response.AddHeader("Content-Disposition", "attachment;filename=\"" + apath  + "\"");
               //    byte[] data = req.DownloadData(Server.MapPath(strURL));
               //    response.BinaryWrite(data);
               //    response.End();
               //}
               //catch (Exception ex)
               //{
               //}
               Response.ClearContent();
               Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", filename.Text));
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

       protected void AdminLoad()
       {
           MasterAdmin.Utility.Grid_DocumentsSelect(DetailsView_Documents, Convert.ToInt32(Session["document_id"].ToString()), Session["des_role"].ToString());
           HtmlGenericControl tab = (HtmlGenericControl)DetailsView_Documents.FindControl("extratab");
           tab.Visible = false;
           load_grid();
           DropDownList country = (DropDownList)DetailsView_Documents.FindControl("Countrydp");
           MasterAdmin.Utility.Getcountry(country, Session["Customer_id"].ToString());           
           Label temp = (Label)DetailsView_Documents.FindControl("temp");
           country.SelectedValue = temp.Text;
       }

}
