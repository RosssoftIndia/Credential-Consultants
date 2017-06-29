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
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using System.Security;

public partial class secure_Evaluate : System.Web.UI.Page
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
        btnreport.PostBackUrl = "~/secure/Report_Status.aspx?tc=" + Session["Trackingcode"].ToString();
        btnattach.PostBackUrl = "~/secure/Attachments.aspx?tc=" + Session["Trackingcode"].ToString();
       

        ClientAdmin.Utility.Get_applicantinfo(lblfileno, lblname, lblcompany, Session["Trackingcode"].ToString());
        ClientAdmin.Utility.Get_Internal(txtinternal, Session["Trackingcode"].ToString());
        txtfileno.Text = Session["Trackingcode"].ToString();
        if (!Page.IsPostBack)
        {

            string folder = ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString());   //Session["Admin_Customer"].ToString();
       
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Assets/Template/" + folder));

            if (dirInfo.Exists)
            {
                drpTemplate();
            }
            else
            {
                Directory.CreateDirectory(Server.MapPath("~/Assets/Template/" + folder));
                drpTemplate();
            }
          
        }
    }
    protected void grid_Evaluate_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_Evaluate.Rows)
        {
            Label lblmajor = (Label)row.FindControl("major");
            if (lblmajor.Text == "0")
            {
                lblmajor.Text = "";
            }

            Label lblequi = (Label)row.FindControl("lblequi");
            Label lblsep = (Label)row.FindControl("seperator");
            Label lblgrade = (Label)row.FindControl("lblgrad");
            Label linkageId = (Label)row.FindControl("lbllnk");
            if (linkageId.Text != "")
            {
                DataSet ds = ClientAdmin.Utility.DetailsView_Linkageinfo(linkageId.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                
                    lblequi.Text = ds.Tables[0].Rows[0]["equiname"].ToString();
                    lblequi.ToolTip = (System.Text.RegularExpressions.Regex.Replace(Server.HtmlDecode(ds.Tables[0].Rows[0]["equides"].ToString()), @"<[^>]*>", string.Empty)).Replace("&nbsp;", "");
                    lblgrade.Text = ds.Tables[0].Rows[0]["gradename"].ToString();
                    lblgrade.ToolTip = (System.Text.RegularExpressions.Regex.Replace(Server.HtmlDecode(ds.Tables[0].Rows[0]["gradedes"].ToString()), @"<[^>]*>", string.Empty)).Replace("&nbsp;", ""); 
                  
                }
                else{ lblequi.Visible = false;lblgrade.Visible = false;lblsep.Text = "-";}
            }
            else { lblequi.Visible = false; lblgrade.Visible = false; lblsep.Text = "-"; }           

        }
          
    }
   
    protected void grid_Evaluate_Load(object sender, EventArgs e)
    {
            switch (Session["Admin_Type"].ToString())
            {
                case "USER":
                    ClientAdmin.Utility.DetailsView_EvaluateBrowse(Session["Trackingcode"].ToString(), grid_Evaluate);                            
                    break;
                case "ADMIN":
                    break;
                default:
                    Response.Redirect("~/Fail.aspx");
                    break;
            }
    }
    
 

    #region Generate Template Dropdown
    public void drpTemplate()
    {
        string folder = ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString()); //Session["Admin_Customer"].ToString();
        if (folder == Session["Admin_Customer"].ToString())
        {
            templatelist(folder, true);  
        }
        else
        {
            templatelist(Session["Admin_Customer"].ToString(), true); 
            templatelist(folder, false);
        }
 
    }

    public void templatelist(string folder,bool type)
    {

        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Assets/Template/" + folder));
        ArrayList list = new ArrayList();
        list.AddRange(dirInfo.GetFiles("*.docx"));
        loadtemplates(list,type,folder);
    }
    public void loadtemplates(ArrayList list,bool type,string folder)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.LoadTemplate(Drptemplate, list,type,folder);
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }

    #endregion

    #region Onclick
    protected void btn_Click(object sender, EventArgs e)
    {       
        lblerror.Text = "";
        string folder = Drptemplate.SelectedValue.ToString();     //// Session["Admin_Customer"].ToString();

        string clientid = ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString());
        string check = ClientAdmin.Utility.check_filenumber(txtfileno.Text, clientid);
        if (check == "Access")
        {
            int count = ClientAdmin.Utility.EvaluationCount(txtfileno.Text);
            //if (count > 0)
            //{
                SelectTemplateType(folder.Split('|')[1] , count);
            //}
            //else { lblerror.Text = "* The application file number entered has no Educational record."; }
        }
        else { lblerror.Text = "* The application file number entered is invalid. Please check your file number and try again."; }




    }


    #endregion

    #region Select Template
    public void SelectTemplateType(string folder, int educount)
    {

        string xmltemplate = "", dotxtemplate = "";
        string ReportType = Drptemplate.SelectedItem.ToString();
             
        xmltemplate = Server.MapPath("~/Assets/Template/Template.xml");
        dotxtemplate = Server.MapPath("~/Assets/Template/" + folder + "/" + ReportType +".docx");

        GenerateReport(xmltemplate, dotxtemplate, educount);
    }

    #endregion

    #region Generate Report

    public void GenerateReport(string xmltemplate, string dotxtemplate, int educount)
    {

        string strTemp = Server.MapPath("~/App_Data/"); //Environment.GetEnvironmentVariable("temp");
        string strFileName = String.Format("{0}\\{1}.docx", strTemp, Guid.NewGuid().ToString());
        File.Copy(dotxtemplate, strFileName);

        GetData(xmltemplate, educount);
        string customXml = File.ReadAllText(Server.MapPath("~/App_Data/datatemp.xml"));
        ReplaceCustomXML(strFileName, customXml);

        //return it to the client - we know strFile is updated, so return it
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("content-disposition", "attachment; filename=" + lblname.Text.Replace(' ','_')   +".docx");
       // Response.ContentType = "application/msword";
        Response.ContentType = "application/vnd.ms-word.document.12";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.TransmitFile(strFileName);
        Response.Flush();
        Response.End();        

        //Delete the temp file
        File.Delete(strFileName);
        File.Delete(Server.MapPath("~/App_Data/datatemp.xml"));
    }


    protected void ReplaceCustomXML(string fileName, string customXML)
    {
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
        {
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;

            mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

            //Add a new customXML part and then add content
            CustomXmlPart customXmlPart = mainPart.AddNewPart<CustomXmlPart>();

            //copy the XML into the new part...
            using (StreamWriter ts = new StreamWriter(customXmlPart.GetStream()))
                ts.Write(customXML);
        }

    }

    protected void GetData(string xmlpath, int educount)
    {
        File.Copy(xmlpath, Server.MapPath("~/App_Data/datatemp.xml"), true);

        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~/App_Data/datatemp.xml"));
        DateTime dt = DateTime.Now;
        int count = doc.DocumentElement.ChildNodes.Count;
        int xmldata = 4;
        int xmlgeneral = 21;
        int xmledu = 71;
        int xmldelivery = 95;
        //generate data
        DataSet Generalinfo = ClientAdmin.Utility.Generaldata(txtfileno.Text);
        DataSet Eduinfo = ClientAdmin.Utility.Edudata(txtfileno.Text);
        DataSet Deliveryinfo = ClientAdmin.Utility.deliverydata(txtfileno.Text);
        DataSet domaininfo = ClientAdmin.Utility.Clientinfodata(ClientAdmin.Utility.clientidbyFilenumber(Session["Trackingcode"].ToString()));
        string[] temp;
        for (int i = 0; i <= xmldata; i++)
        {
            switch (doc.DocumentElement.ChildNodes[i].Name)
            {
                case "currentmonth":
                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(dt.ToString("MMMM"));
                    break;
                case "currentdate":
                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(dt.ToString("dd"));
                    break;
                case "currentyear":
                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(dt.Year.ToString().Split('/')[0]);
                    break;
                case "fileno":
                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(txtinternal.Text);
                    break;
                case "evalpurpose":
                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.Getpurpose(txtfileno.Text));
                    break;
            }
        }
        for (int i = xmldata+1; i <= xmlgeneral; i++)
        {
                    //general info
                    if (Generalinfo.Tables[0].Rows.Count > 0)
                    {
                        temp = Generalinfo.Tables[0].Rows[0]["DateOfBirth"].ToString().Split('/');
                //DataTable dts = (DataTable)Generalinfo.Tables[0];
                foreach (DataColumn col in Generalinfo.Tables[0].Columns)
                        {
                            if (col.ColumnName == doc.DocumentElement.ChildNodes[i].Name)
                            {
                                doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Generalinfo.Tables[0].Rows[0][doc.DocumentElement.ChildNodes[i].Name].ToString());
                            }
                        }
                        if (doc.DocumentElement.ChildNodes[i].Name == "birthdate")
                        {
                            doc.DocumentElement.ChildNodes[i].InnerText = SafeString(temp[1].ToString());
                        }
                        if (doc.DocumentElement.ChildNodes[i].Name == "birthmonth")
                        {
                            doc.DocumentElement.ChildNodes[i].InnerText = SafeString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(temp[0].ToString())));
                        }
                        if (doc.DocumentElement.ChildNodes[i].Name == "birthyear")
                        {
                            doc.DocumentElement.ChildNodes[i].InnerText = SafeString(temp[2].ToString());
                        }

                    }
        }
        for (int i = xmlgeneral+1; i <= xmledu; i++)
        {
                    //Edu
                    if (educount > 5) { educount = 5; }
                    if (Eduinfo.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 1; j <= educount; j++)
                        {
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_documents")
                            {
                                doc.DocumentElement.ChildNodes[i].InnerText = "";
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_program")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("degree", Eduinfo.Tables[0].Rows[j - 1]["eduid"].ToString(), Session["Admin_Customer"].ToString()));
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_country")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Eduinfo.Tables[0].Rows[j - 1]["Country"].ToString());
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_institution")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Eduinfo.Tables[0].Rows[j - 1]["Name"].ToString());
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_recognition")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("Institution", Eduinfo.Tables[0].Rows[j - 1]["InsId"].ToString(), Session["Admin_Customer"].ToString()));
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_us")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("equi", Eduinfo.Tables[0].Rows[j - 1]["Linkage"].ToString(), Session["Admin_Customer"].ToString()));
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_grade")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("grade", Eduinfo.Tables[0].Rows[j - 1]["Linkage"].ToString(), Session["Admin_Customer"].ToString()));
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_degree")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Eduinfo.Tables[0].Rows[j - 1]["degree"].ToString());
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_dates")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Eduinfo.Tables[0].Rows[j - 1]["StartDate"].ToString() + '-' + Eduinfo.Tables[0].Rows[j - 1]["EndDate"].ToString());
                            }
                            if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_graddate")
                            {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Eduinfo.Tables[0].Rows[j - 1]["DateDegreeAwarded"].ToString());
                                }
                                if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_issued_gpa")
                                {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("Issued_GPA", Eduinfo.Tables[0].Rows[j - 1]["Linkage"].ToString(), Session["Admin_Customer"].ToString()));
                                }
                                if (doc.DocumentElement.ChildNodes[i].Name == "credential_" + j + "_converted_gpa")
                                {
                                    doc.DocumentElement.ChildNodes[i].InnerText = SafeString(ClientAdmin.Utility.GetDescription("Converted_GPA", Eduinfo.Tables[0].Rows[j - 1]["Linkage"].ToString(), Session["Admin_Customer"].ToString()));
                            }
                        }
                    }
                    }
        for (int i = xmledu+1; i <=xmldelivery; i++)
        {
            //Edu
            int deliverycount = Deliveryinfo.Tables[0].Rows.Count;
            if (deliverycount > 5) { deliverycount = 5; }
            if (deliverycount > 0)
            {
                for (int j = 1; j <= deliverycount; j++)
                {
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_Name")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["Name"].ToString());
                    }
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_StreetName")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["StreetName"].ToString());
                    }
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_City")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["City"].ToString());
                    }
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_State_Province")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["State_or_Province"].ToString());
                    }
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_Country")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["Country"].ToString());
                    }
                    if (doc.DocumentElement.ChildNodes[i].Name == "Additional_Address" + j + "_Zip")
                    {
                        doc.DocumentElement.ChildNodes[i].InnerText = SafeString(Deliveryinfo.Tables[0].Rows[j - 1]["Zip_or_PostalCode"].ToString());
                    }
                }
            }
        }
        for (int i = xmldelivery + 1; i <= count-1; i++)
        {            
            foreach (DataColumn col in domaininfo.Tables[0].Columns)
            {
                if (col.ColumnName == doc.DocumentElement.ChildNodes[i].Name)
                {
                    doc.DocumentElement.ChildNodes[i].InnerText = (domaininfo.Tables[0].Rows[0][doc.DocumentElement.ChildNodes[i].Name].ToString());
                }
            }
        }


        doc.Save(Server.MapPath("~/App_Data/datatemp.xml"));

    }


        



    public void Edu()
    {

    }
    #endregion


 
    public static string SafeString(string Input)
    {
        string result = "";
       // result = SecurityElement.Escape(Input);
        result = Input;

        return result;

    }



 
}
