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
using System.Net;
using System.Web.Script.Serialization;

public partial class Default : System.Web.UI.Page
{
    string Subdomain = "";
    string RedirectUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        bool ClientIsValid= false;
        if (Request.QueryString["subdomain"] != null)
        {
            Subdomain= Request.QueryString["subdomain"].ToString();
        }
        else
        {
            Subdomain="nosubdomain";
        }
         ClientIsValid = Authentication.Utility.ClientIsValid(Request.Url,Subdomain);
        //Client Check
       
        if (ClientIsValid)
        {
         int clientid=0;  
            Authentication.Utility.DomainAttributes dm = Authentication.Utility.GetClient(Request.Url,Subdomain);
            Authentication.Utility.SessionVariable sv = new Authentication.Utility.SessionVariable();  
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
       

            if (dm.IsMultidomain)
            {
                Page.Title = dm.DmName;
            OrgTitle.InnerHtml = dm.DmName;
                Subclient.InnerHtml = "<static>Online Application for</static><br/>" + "<client>" + dm.SubDmName + "</client>";
              sv.Customer_id = dm.DmID;
              sv.SubClient_id = dm.SubDmID;
              clientid = dm.SubDmID;
                
            } 
            else
            { 
                Page.Title = dm.DmName; OrgTitle.InnerHtml = dm.DmName; sv.Customer_id = dm.DmID;
             clientid = dm.DmID;
                Subclient.InnerHtml = "<static>Online Application</static>";
              
            }
              Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
           

            Session["Appsettings"] = app;
            Session["Clientsettings"] = dm;
            Session["SV"] = sv;


          // string listcontent = "";
          //  HtmlGenericControl listcontent = new HtmlGenericControl("div");

            DataSet ds = Authentication.Utility.Splashpage(clientid);  
            if (ds.Tables[0].Rows.Count > 0)
            {

                HtmlGenericControl accordion = new HtmlGenericControl("div");
                accordion.Attributes.Add("id", "accordion");
               
             
                //AppInstructions
                HtmlGenericControl title1 = new HtmlGenericControl("h3");
                title1.InnerHtml = "<a href='#'>Instructions</a>";
                accordion.Controls.Add(title1);

                  HtmlGenericControl Instruction = new HtmlGenericControl("div");
                  Instruction.InnerHtml = ds.Tables[0].Rows[0]["AppInstructions"].ToString();
                  accordion.Controls.Add(Instruction);
                  

                                //OfflineApp Instructions
              
                                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["OfflineApp"].ToString()))
                                {
                                    HtmlTable tb = new HtmlTable();
                                    HtmlTableRow tr1 = new HtmlTableRow();
                                    HtmlTableRow tr2 = new HtmlTableRow();
                                    HtmlTableCell c1 = new HtmlTableCell();
                                    HtmlTableCell c2 = new HtmlTableCell();
                                    ImageButton imgbtn = new ImageButton();
                                    imgbtn.Click += new ImageClickEventHandler(Downloadbtn_Click);
                                    imgbtn.ImageUrl = "~/Code/menu/pdf_icon.png";
                                    imgbtn.Width = 42;
                                    imgbtn.Height = 42;
                                    imgbtn.AlternateText = clientid.ToString();
                                    c1.Controls.Add(imgbtn);
                                    c2.InnerHtml = "Application.pdf";
                                    tr1.Controls.Add(c1);
                                    c1.Align = "Center";
                                    tr2.Controls.Add(c2);
                                    tb.Controls.Add(tr1);
                                    tb.Controls.Add(tr2);
                                    HtmlGenericControl Instruction2 = new HtmlGenericControl("span"); 
                                    Instruction2.InnerHtml +=
                                            "<b>Application</b><br/>" +
                                             ds.Tables[0].Rows[0]["OfflineAppInstructions"].ToString();
                                            
                                             //"<table><tr><td style='text-align:center;' >" +
                                             //"<input type='image' name='ctl00$Content$imgbtn' alt='"+clientid+"' width='42px' Height='42px' id='ctl00_Content_imgbtn' src='Code/menu/pdf_icon.png' onclick='javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;ctl00$Content$imgbtn&quot;, &quot;&quot;, true, &quot;&quot;, &quot;&quot;, false, false))' style='border-width:0px;'><a href='Assets/OfflineApp/" + clientid + "/Application.pdf'><img src='Code/menu/pdf_icon.png' width='42px' Height='42px' /></a></td></tr> " +
                                             //"<tr><td>Application.pdf</td></tr></table>";  
                                    Instruction.Controls.Add(Instruction2);
                                    Instruction.Controls.Add(tb);

                                }

                   
               //BrowserInstructions
                                HtmlGenericControl title2 = new HtmlGenericControl("h3");
                                title2.InnerHtml = "<a href='#'>Browser Instructions</a>";
                                accordion.Controls.Add(title2);
                                HtmlGenericControl BrowserInstruction = new HtmlGenericControl("div");
                                BrowserInstruction.InnerHtml = ds.Tables[0].Rows[0]["BrowserInstructions"].ToString();  


                    accordion.Controls.Add(BrowserInstruction);
                    list.Controls.Add(accordion);

                    //Online app
                    if (ds.Tables[0].Rows[0]["OnlineApp"].ToString() != "1")
                    {                        
                        recaptchablk.Visible = false;
                       
                    }
            }


            //Application Type
            switch (dm.App_Type)
            {
                case 1:
                    RedirectUrl = "~/Index.aspx";
                    break;
                case 2:
                    RedirectUrl = "~/mIndex.aspx";
                    break;
                case 3:
                    RedirectUrl = "~/nIndex.aspx";
                    break;
            }     

            if (Subdomain != "nosubdomain")
            {
                 RedirectUrl += "?subdomain=" + Subdomain; 
            }
           
        }   

    }

    protected void Btn_continue_Click(object sender, EventArgs e)
    {
      //  HtmlGenericControl msg = (HtmlGenericControl)Master.FindControl("Msgbox");
       if(Page.IsValid)
        {
           Session.Add("Session_name", "loggedinsuccessfully");
           Response.Redirect(RedirectUrl);
        }
       else
       {
           lblmsg.Text = "There was a Capatcha mismatch Try Again!";  
       }
      
    }
    protected void Downloadbtn_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;

        string filename = "~/Assets/OfflineApp/" + btn.AlternateText + "/Application.pdf";
        if(File.Exists(Server.MapPath(filename)))
        {
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + "Application.pdf");     
        Response.TransmitFile(filename);
        Response.End();
        }
    }
    protected void frm1_Btn_continue_Click(object sender, ImageClickEventArgs e)
    {
        if (Validate())
        {
            Session.Add("Session_name", "loggedinsuccessfully");
            Response.Redirect(RedirectUrl);
        }
        else
        {
            //lblmsg.Text = "There was a Capatcha mismatch Try Again!";
            lblmsg.Text = "Not Valid reCAPTCHA!";
        }

    }

    public bool Validate()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        bool Valid = false;
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=6Lc2tg4TAAAAAGxSWcEg5LEQXsn0h7_xGcdsrOCO&response=" + Response);
        try
        {
            //Google recaptcha Response
            using (WebResponse wResponse = req.GetResponse())
            {

                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                    Valid = Convert.ToBoolean(data.success);
                }
        }

            return Valid;
        }
        catch (WebException ex)
        {
            throw ex;
        }
    }
    public class MyObject
    {
        public string success { get; set; }
    }
}

