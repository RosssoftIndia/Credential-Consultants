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
using System.Text.RegularExpressions;  
 
public partial class secure_Attachments : System.Web.UI.Page
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
        if (Request.QueryString["Tc"] != null)
        {
            Session["Appid"] = ClientAdmin.Utility.AppidbyFilenember(Session["Trackingcode"].ToString());
        }
        btnview.PostBackUrl = "~/secure/View_Application.aspx?tc=" + Session["Trackingcode"].ToString();    
        btnedit.PostBackUrl = "~/secure/Edit_Application.aspx?tc=" + Session["Trackingcode"].ToString();
        btnstatus.PostBackUrl = "~/secure/Edit_Application_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btnreport.PostBackUrl = "~/secure/Report_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btneval.PostBackUrl = "~/secure/Evaluate.aspx?tc=" + Session["Trackingcode"].ToString();

        ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname, lblcompany, Session["Trackingcode"].ToString());
        load_grid();
    }

    #region Documents
    protected void Load_documents()
    {
        DataTable dtCollection = new DataTable();
        dtCollection.Rows.Clear();
        dtCollection.Columns.Clear();
        dtCollection.Columns.Add("url");
        dtCollection.Columns.Add("Path");
        dtCollection.Columns.Add("File");

        string path = Server.MapPath("~/Assets/Attachments/" + Session["Appid"].ToString());
        if (checkfolder(path))
    {
            string docpath = path + "/Documents/";
            //document
            if (Directory.Exists(docpath))
        {
                DirectoryInfo docpathinfo = new DirectoryInfo(docpath);
                FileInfo[] docList = docpathinfo.GetFiles();
                docPersistRowIndex(docList, Session["Appid"].ToString(), dtCollection);
           
            }
           
        }
        Doc_grid.DataBind();
    }
    protected void docPersistRowIndex(FileInfo[] FIlist, string appid, DataTable dtCollection)
    {
        if (FIlist.Length > 0)
           {                

            foreach (FileInfo FI in FIlist)
               {
                DataRow drrow = dtCollection.NewRow();
                drrow[0] = "~/Assets/Attachments/" + appid + "/Documents/" + FI.Name;
                drrow[1] = "~/secure/viewpdf.aspx?id=" + FI.Name + "&type=2&aid=" + appid;
                drrow[2] = FI.Name;
                dtCollection.Rows.Add(drrow);
               }

           }
        dtCollection.DefaultView.Sort = "File asc";
        Doc_grid.DataSource = dtCollection;


        }
    protected void doc_delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        Label vpath = (Label)grdRow.FindControl("lblpath");
        string apath = Server.MapPath(vpath.Text);
        if (File.Exists(apath))
        {
            File.Delete(apath);
            if (!File.Exists(apath))
        {
                Load_documents();
            }
        }
        }    
    protected void Doc_grid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Doc_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label vpath = (Label)row.FindControl("lblpath");
                string apath = Server.MapPath(vpath.Text);
                FileInfo file = new FileInfo(apath);
                ImageButton btndownload = ((ImageButton)row.FindControl("btndownload"));

                switch (file.Extension.ToLower())
                {
                    case ".png":
                        btndownload.ImageUrl = "~/Code/icons/png.png";
                        break;
                    case ".jpg":
                        btndownload.ImageUrl = "~/Code/icons/jpg.png";
                        break;
                    case ".doc":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".docx":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".xls":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".xlsx":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".pdf":
                        btndownload.ImageUrl = "~/Code/icons/pdf.png";
                        break;
                }
            }
        }
    }
    protected void Doc_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Doc_grid.PageIndex = e.NewPageIndex;
        Load_documents();
    }
    protected void doc_upload_Click(object sender, EventArgs e)
    {
        if (doc_uploader.HasFile)
        {
            bool result = false;
            Button btn = (Button)sender;


            if (Session["Appid"] != null)
            {
                string appid = Session["Appid"].ToString();
                string path = Server.MapPath("~/Assets/Attachments/" + appid);
                if (checkfolder(path))
                {
                    txtdocname.Text = Path.GetFileNameWithoutExtension(doc_uploader.FileName.ToString());
                    FileUpload(path, "Documents", txtdocname.Text, 1);
                    Load_documents();
                }
            }

        }
        else
        {
            docmsg.Text = "You have not specified a file.";
        }  


    }
    #endregion

    #region Reports
    protected void Load_reports()
    {
        DataTable dtCollection = new DataTable();
        dtCollection.Rows.Clear();
        dtCollection.Columns.Clear();
        dtCollection.Columns.Add("url");
        dtCollection.Columns.Add("Path");
        dtCollection.Columns.Add("File");

        string path = Server.MapPath("~/Assets/Attachments/" + Session["Appid"].ToString());
        if (checkfolder(path))
            {
            string docpath = path + "/Reports/";
            //document
            if (Directory.Exists(docpath))
            {
                DirectoryInfo docpathinfo = new DirectoryInfo(docpath);
                FileInfo[] docList = docpathinfo.GetFiles();
                repoPersistRowIndex(docList, Session["Appid"].ToString(), dtCollection);

    }

        }
        report_grid.DataBind();
        }
    protected void repoPersistRowIndex(FileInfo[] FIlist, string appid, DataTable dtCollection)
        {
        if (FIlist.Length > 0)
            {

            foreach (FileInfo FI in FIlist)
                {
                DataRow drrow = dtCollection.NewRow();
                drrow[0] = "~/Assets/Attachments/" + appid + "/Reports/" + FI.Name;
                drrow[1] = "~/secure/viewpdf.aspx?id=" + FI.Name + "&type=3&aid=" + appid;
                drrow[2] = FI.Name;
                dtCollection.Rows.Add(drrow);
                        }

            }
        dtCollection.DefaultView.Sort = "File asc";
        report_grid.DataSource = dtCollection;
           

    }
    protected void report_delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        Label vpath = (Label)grdRow.FindControl("lblpath");
        string apath = Server.MapPath(vpath.Text);
        if (File.Exists(apath))
    {
            File.Delete(apath);
            if (!File.Exists(apath))
        {
                Load_reports();
            }
        }
        }
    protected void report_grid_DataBound(object sender, EventArgs e)
        {

        foreach (GridViewRow row in report_grid.Rows)
            {
            if (row.RowType == DataControlRowType.DataRow)
                {
                Label vpath = (Label)row.FindControl("lblpath");
                string apath = Server.MapPath(vpath.Text);
                FileInfo file = new FileInfo(apath);
                ImageButton btndownload = ((ImageButton)row.FindControl("btndownload"));

                switch (file.Extension.ToLower())
                    {
                    case ".png":
                        btndownload.ImageUrl = "~/Code/icons/png.png";
                        break;
                    case ".jpg":
                        btndownload.ImageUrl = "~/Code/icons/jpg.png";
                        break;
                    case ".doc":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".docx":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".xls":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".xlsx":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".pdf":
                        btndownload.ImageUrl = "~/Code/icons/pdf.png";
                        break;
                        }
                    }
                    }
                }
    protected void report_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
        report_grid.PageIndex = e.NewPageIndex;
        Load_reports();
        }
    protected void report_upload_Click(object sender, EventArgs e)
        {
        if (report_uploader.HasFile)
    {
            bool result = false;
            if (Session["Appid"] != null)
        {
                string appid = Session["Appid"].ToString();
                string path = Server.MapPath("~/Assets/Attachments/" + appid);
                if (checkfolder(path))
            {
                    txtreportname.Text = Path.GetFileNameWithoutExtension(report_uploader.FileName.ToString());
                    FileUpload(path, "Reports", txtreportname.Text, 2);
                    Load_reports();
            }
        }

    }
        else
        {
            reportmsg.Text = "You have not specified a file.";
        }
    }
    #endregion
  

    #region Other
    protected void Load_Otherdocuments()
    {
        DataTable dtCollection = new DataTable();
        dtCollection.Rows.Clear();
        dtCollection.Columns.Clear();
        dtCollection.Columns.Add("url");
        dtCollection.Columns.Add("Path");
        dtCollection.Columns.Add("File");

        string path = Server.MapPath("~/Assets/Attachments/" + Session["Appid"].ToString());
        if (checkfolder(path))
        {
            string docpath = path + "/Others/";
            //document
            if (Directory.Exists(docpath))
            {
                DirectoryInfo docpathinfo = new DirectoryInfo(docpath);
                FileInfo[] docList = docpathinfo.GetFiles();
                otherPersistRowIndex(docList, Session["Appid"].ToString(), dtCollection);

            }          

        }
        Other_grid.DataBind();
    }
    protected void otherPersistRowIndex(FileInfo[] FIlist, string appid, DataTable dtCollection)
    {
        if (FIlist.Length > 0)
        {

            foreach (FileInfo FI in FIlist)
            {
                DataRow drrow = dtCollection.NewRow();
                drrow[0] = "~/Assets/Attachments/" + appid + "/Others/" + FI.Name;
                drrow[1] = "~/secure/viewpdf.aspx?id=" + FI.Name + "&type=2&aid=" + appid; 
                drrow[2] = FI.Name;               
                dtCollection.Rows.Add(drrow);
            }
                    
        }
        dtCollection.DefaultView.Sort = "File asc"; 
        Other_grid.DataSource = dtCollection;


    }
    protected void other_delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        Label vpath = (Label)grdRow.FindControl("lblpath");
        string apath = Server.MapPath(vpath.Text);
        if (File.Exists(apath))
        {
            File.Delete(apath);
            if (!File.Exists(apath))
            {
                Load_Otherdocuments();
            }
        }
    }
    protected void Other_grid_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Other_grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label vpath = (Label)row.FindControl("lblpath");
                string apath = Server.MapPath(vpath.Text);
                FileInfo file = new FileInfo(apath);
                ImageButton btndownload = ((ImageButton)row.FindControl("btndownload"));

                switch (file.Extension.ToLower())
                {
                    case ".png":
                        btndownload.ImageUrl = "~/Code/icons/png.png";
                        break;
                    case ".jpg":
                        btndownload.ImageUrl = "~/Code/icons/jpg.png";
                        break;
                    case ".doc":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".docx":
                        btndownload.ImageUrl = "~/Code/icons/doc.png";
                        break;
                    case ".xls":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".xlsx":
                        btndownload.ImageUrl = "~/Code/icons/xls.png";
                        break;
                    case ".pdf":
                        btndownload.ImageUrl = "~/Code/icons/pdf.png";
                        break;
                }
            }
        }
    }
    protected void Other_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Other_grid.PageIndex = e.NewPageIndex;
        Load_Otherdocuments();
    }
    protected void other_upload_Click(object sender, EventArgs e)
    {
        if (Other_uploader.HasFile)
        {
            bool result = false;
            Button btn = (Button)sender;


            if (Session["Appid"] != null)
    {
                string appid = Session["Appid"].ToString();
                string path = Server.MapPath("~/Assets/Attachments/" + appid);
        if (checkfolder(path))
        {
                    txtothername.Text = Path.GetFileNameWithoutExtension(Other_uploader.FileName.ToString());
                    FileUpload(path, "Others", txtothername.Text, 3);
                    Load_Otherdocuments();
                }
            }

        }
        else
            {
            docmsg.Text = "You have not specified a file.";
        }


            }
    #endregion


    #region Common
    protected void load_grid()
    {
        Load_documents();
        Load_reports();
        Load_Otherdocuments();
        }
    protected bool checkfolder(string root)
    {
        bool result = false;

        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
    }
        else { result = true; }

        if (!Directory.Exists(root + "/Documents"))
    {
            Directory.CreateDirectory(root + "/Documents");
        }
        else { result = true; }

        if (!Directory.Exists(root + "/Reports"))
        {
            Directory.CreateDirectory(root + "/Reports");
        }
        else { result = true; }

        if (!Directory.Exists(root + "/Others"))
            {
            Directory.CreateDirectory(root + "/Others");
            }
        else { result = true; }

          

        return result;

    }
    protected bool Isvalid(string ext, string[] validFileTypes)
    {
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        return isValidFile;

    }
    protected bool Isexist(string path)
    {
        bool result = false;

        if (File.Exists(path))
        {
            result = true;
    }

        return result;
    }
    public bool GetValidFileName(string fileName)
    {

        bool result = true;


        string x = @"^[\w\-. ]+$";
        Regex RgxUrl = new Regex(x);
        if (RgxUrl.IsMatch(fileName))
        {
            result = false; 
        }
        return result;

    }
    public  void ShowChars(Label msg)
    {       
        char[] invalidFileChars = Path.GetInvalidFileNameChars();

        foreach (char someChar in invalidFileChars)
        {
            msg.Text = msg.Text +someChar;
           
        }
    }
    protected void btndownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)btn.Parent.Parent as GridViewRow;
        Label vpath = (Label)grdRow.FindControl("lblpath");
        string apath = Server.MapPath(vpath.Text);
        if (File.Exists(apath))
        {
            FileInfo file = new FileInfo(apath);
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", file.Name));
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
    protected bool FileUpload(string path, string folder, string filename, int Type)
    {
        //Type
        //1. Document
        //2. Report
        //3. Other

        bool result = false;      
        FileUpload docuploader;
        Label msg;
        var validFileTypes = new string []{};
        switch(Type)
        {
            case 1:
            docuploader = (FileUpload)doc_uploader;
            msg = (Label)docmsg;
            validFileTypes = new string [] { "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
            break;
        
            case 2:
            docuploader = (FileUpload)report_uploader;
            msg = (Label)reportmsg;
            validFileTypes = new string[] { "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
            break;

            case 3:
            docuploader = (FileUpload)Other_uploader;
            msg = (Label)othermsg;
            validFileTypes = new string[] { "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
            break;

            default :
            docuploader = (FileUpload)doc_uploader;
            msg = (Label)docmsg;
            validFileTypes = new string[] { "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
            break;

        }


        if (!GetValidFileName(filename))
        {
            if (docuploader.PostedFile.ContentLength < 11534336)
            {
                string fileExt = System.IO.Path.GetExtension(docuploader.FileName).ToLower();
           
                if (Isvalid(fileExt, validFileTypes))
                {
                    try
                    {
                        if (!Isexist(path + "/" + folder + "/" + filename + fileExt))
                        {
                            double fileinmb = Convert.ToDouble(docuploader.PostedFile.ContentLength) / 1048576;
                            docuploader.SaveAs(path + "/" + folder + "/" + filename + fileExt);
                            msg.Text = "File Uploaded. Repeat to upload another File.";
                            
                        }
                        else { msg.Text = "File Name you specified Exists"; }
                    }
                    catch (Exception ex)
                    {
                        msg.Text = "ERROR: " + ex.Message.ToString();
                    }
                }
                else { msg.Text = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes); }
            }
            else
            {
    
                msg.Text = "File size of " + Convert.ToString((docuploader.PostedFile.ContentLength / 1024) / 1024) + " MB is exceeding the uploading limit.";
    
            }
        }
        else
    {

            msg.Text = "filename specified contains spl characters";
            ShowChars(msg);
        }

        return result;
    }
    #endregion 




    #region unwanted
    //protected bool Upload(string path, string folder, string filename, bool Type)
    //{

    //    bool result = false;
    //    FileUpload docuploader;
    //    Label msg;
    //    if (Type)
    //    {
    //        docuploader = (FileUpload)doc_uploader;
    //        msg = (Label)docmsg;
    //    }
    //    else
    //    {
    //        docuploader = (FileUpload)report_uploader;
    //        msg = (Label)reportmsg;
    //    }
    //    if (!GetValidFileName(filename))
    //    {
    //        if (docuploader.PostedFile.ContentLength < 11534336)
    //        {
    //            string fileExt = System.IO.Path.GetExtension(docuploader.FileName).ToLower();
    //            if (fileExt == ".pdf")
    //            {
    //                try
    //                {
    //                    if (!Isexist(path + "/" + folder + "/" + filename + fileExt))
    //                    {
    //                        double fileinmb = Convert.ToDouble(docuploader.PostedFile.ContentLength) / 1048576;
    //                        docuploader.SaveAs(path + "/" + folder + "/" + filename + fileExt);
    //                        msg.Text = "File Uploaded. Repeat to upload another PDF.";
    //                        //msg.Text = "File name: " +
    //                        //   docuploader.PostedFile.FileName + "<br>" + "( " +
    //                        //    Convert.ToString(fileinmb.ToString(".00")) + " MB /" +
    //                        //     Convert.ToString(docuploader.PostedFile.ContentLength / 1024) + " kb)<br>" +
    //                        //    "Content type: " +
    //                        //    docuploader.PostedFile.ContentType + "<br>" +
    //                        //    "Uploded Successfully";
    //                    }
    //                    else { msg.Text = "File Name you specified Exists"; }
    //                }
    //                catch (Exception ex)
    //                {
    //                    msg.Text = "ERROR: " + ex.Message.ToString();
    //                }
    //            }
    //            else { msg.Text = "You can only specify a pdf file."; }
    //        }
    //        else
    //        {

    //            msg.Text = "File size of " + Convert.ToString((docuploader.PostedFile.ContentLength / 1024) / 1024) + " MB is exceeding the uploading limit.";

    //        }
    //    }
    //    else
    //    {

    //        msg.Text = "filename specified contains spl characters";
    //        ShowChars(msg);
    //    }

    //    return result;
    //}
    #endregion

 
}
