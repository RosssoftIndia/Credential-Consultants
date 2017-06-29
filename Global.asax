<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
       
        // Code that runs when an unhandled error occurs
             //string strUserName = HttpContext.Current.User.Identity.Name;
        
        // string err = " Generated Application Error in " + Request.Url.ToString() + "Stack trace: " + Server.GetLastError().ToString();
        
        //    string from = ConfigurationSettings.AppSettings["MailId"];
        //    string to = ConfigurationSettings.AppSettings["BugId"];
        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //    mail.To.Add(to);
        //    mail.From = new System.Net.Mail.MailAddress(from, "CredentialConsultants", System.Text.Encoding.UTF8);
        //    mail.Subject = "error on CredentialConsultants site";
        //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mail.Body = err;         
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    mail.IsBodyHtml = true;
        //    mail.Priority = System.Net.Mail.MailPriority.High;

        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential(from, "falconoidtest");

        //    client.Port = 587; 
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true; 
        //    try
        //    {
        //        client.Send(mail);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ex2 = ex;
        //        string errorMessage = string.Empty;
        //        while (ex2 != null)
        //        {
        //            errorMessage += ex2.ToString();
        //            ex2 = ex2.InnerException;
        //        }                              
        //    }

    }

    void Session_Start(object sender, EventArgs e) 
    {
        Session["Appsettings"] = "Empty";
        Session["Clientsettings"] = "Empty";
        Session["SV"] = "Empty";
        
        // Code that runs when a new session is started
        //global
        Session["user_status"] = "Accepted";
        
        //public         
        //Session["Applicant_id"] = 0;
        //Session["Request_id"] = 0;
        //Session["SubClient_id"] = 0;
        //Session["Customer_id"] = 0;
        //Session["page1"] = 0;
        //Session["page2"] = 0;
        //Session["page3"] = 0;
        //Session["page4"] = 0;
        //admin
        Session["User"] = 0;
        Session["Trackingcode"] = 0;
        Session["Admin_Type"] = 0;
        Session["Admin_Customer"] = 0;
        Session["Authenticate"] = 0;
        Session["App_Status"] = 0;
        Session["fno"] = 0;
        Session["Applicant_id"] = 0;
        Session["Request_id"] = 0;
       

        

    }

    void Session_End(object sender, EventArgs e) 
    {      
        Session["Appsettings"] = "Empty";
        Session["Clientsettings"] = "Empty";
        Session["SV"] = "Empty";
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session["user_status"] = "Denied";
        //Session["Applicant_id"] = 0;
        //Session["Request_id"] = 0;
        //Session["SubClient_id"] = 0;
        //Session["Customer_id"] = 0;
        //Session["page1"] = 0;
        //Session["page2"] = 0;
        //Session["page3"] = 0;
        //Session["page4"] = 0;
        //admin
        Session["Trackingcode"] = 0;
        Session["Admin_Customer"] = 0;
        Session["Authenticate"] = 0;
        Session["App_Status"] = 0;
        Session["fno"] = 0;      
        Session["Applicant_id"] = 0;
        Session["Request_id"] = 0;
       // Response.Redirect("~/Timeout.aspx");  

    }
       
</script>
