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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO; 


namespace Authentication
{
    public class Utility
    {
        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Dbconnection
        public static string DBConnectionString()
        {
            string strConn = ConfigurationManager.ConnectionStrings["CredentialConsultantsConnectionString"].ConnectionString;
            return strConn;
        }
        private static DataSet GetDataSet(string strSQL)
        {

            SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
            DataSet ds = new DataSet();
            sdpPrd.Fill(ds);
            return ds;
        }
        private static bool GetDataSet_withoutID(string strSQL)
        {
            bool result;
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        private static int GetDataSet_withID(string strSQL)
        {
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("SELECT @@IDENTITY", conn);
            string appId = cmd.ExecuteScalar().ToString();
            int nId = Convert.ToInt32(appId);
            conn.Close();
            return nId;
        }
        private static int GetDataSet_reader(string strSQL)
        {
            int id = 0;
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();

            if (rd.HasRows)
            {
                id = Convert.ToInt32(rd["id"].ToString());
            }
            else
            {
                id = 0;
            }
            conn.Close();
            return id;
        }
        #endregion

        #region Admin  
        public static bool AdminClientIsValid(Uri url)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

            url = GetUrl(url, IsLive, true);
            bool result = false;
            string host = url.Host;
            int dmLength = host.Split('.').Length;

            if (host.Split('.')[0].ToString() != "www")
            {
            string query = "Select id,Name from cc_masterdataset.customer where SubDomainName='";


            switch (dmLength)
            {
                case 3:
                    query += host.Split('.')[0] + "' AND Parent='SELF'";
                    break;
                default:
                    myredirect("Invalid Url Contact Administration.");
                    break;

            }

            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                result = true;
            }
            else
            {
                myredirect("Invalid Url Contact Administration.");
                result = false;
            }
            conn.Close();
            }
            else
            {
                HttpContext.Current.Response.Redirect("http://" + host.Replace("www.", "").ToString()+"/Login.aspx");
            }

            return result;
        }
        public struct AdminDomainAttributes
        {
            public int Length;           
            public int DmID;
            public string DmName;
            public string Subdm;
           
        }
        public static AdminDomainAttributes AdminSplitUrl(Uri url)
        {           
            AdminDomainAttributes Dm = new AdminDomainAttributes();
            ClientAttributes client = new ClientAttributes();
            string host = url.Host;
            Dm.Length = host.Split('.').Length;
          
            switch (Dm.Length)
            {
                case 3:
                    client = GetClientInfo(host.Split('.')[0]);
                    Dm.DmID = client.ID;
                    Dm.DmName = client.Name;
                    Dm.Subdm = client.Subdm;     
                    break;
                default:
                    myredirect("Invalid Url Contact Administration.");
                    break;
            }
            
            return Dm;
        }
     
        public static AdminDomainAttributes AdminGetClient(Uri url)
        {
            AdminDomainAttributes Dm = new AdminDomainAttributes();
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

            url = GetUrl(url, IsLive, true);   

            Dm = AdminSplitUrl(url);

            return Dm;
        }



        public static string GetSubDomain(string Name, string Password, Uri url, string type)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            
            bool block = false; //Convert.ToBoolean(app.TypeSwitcher);
            string SubDomainName = "";
            string title = "";
            if (block)
            {
                //---main block--------------------------------------------
                string host = url.Host;
                if (host.Split('.').Length > 1)
                {
                    int index = host.IndexOf(".");
                    SubDomainName = host.Substring(0, index);
                    switch (type)
                    {
                        case "title":
                            title = Check_Title(SubDomainName);
                            bool Isvalid = Authentication.Utility.Getcompanylist(title);   
                        
                          if (!Isvalid)
                          {
                                    HttpContext.Current.Response.Redirect("http://www.credentialconsultants.com/");
                            }                         
                                           
                            break;

                        case "check_customer":
                            int cstid = Check_Customer(SubDomainName);
                            title = cstid.ToString();
                            break;

                        case "login":
                            string result = Login(Name, Password, SubDomainName);
                            title = result;
                            break;
                    }
                    return title;
                }
                //---main block--------------------------------------------
            }
            else
            {

                //---demo block--------------------------------------------
                SubDomainName = "ravtronix";
                switch (type)
                {
                    case "title":
                        title = Check_Title(SubDomainName);
                        break;

                    case "check_customer":
                        int cstid = Check_Customer(SubDomainName);
                        title = cstid.ToString();
                        break;

                    case "login":
                        string result = Login(Name, Password, SubDomainName);
                        title = result;
                        break;
                }
                return title;


                //---demo block--------------------------------------------
            }

            return null;
        }
        public static int Check_Customer(string customer_code)
        {
            int Customer_id = 0;
            string query = "Select id from cc_masterdataset.customer where SubDomainName='" + customer_code + "'";
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                Customer_id = Convert.ToInt32(rd["id"].ToString());
            }
            else
            {
                Customer_id = 0;
            }
            conn.Close();
            return Customer_id;

        }
        public static string Check_Title(string customer_code)
        {
            string Customer = "";
            string query = "Select Name from cc_masterdataset.customer where SubDomainName='" + customer_code + "'";
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                Customer = rd["Name"].ToString();
            }
            else
            {
                Customer = "Wrong Domain";
            }
            conn.Close();
            return Customer;

        }

        public static string Login(string Name, string Password, string subdomain)
        {
            string result = "";
            string strSQL = "SELECT Customer_Id FROM cc_relateddataset.login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "SELECT * FROM cc_masterdataset.customer where SubDomainName='" + subdomain.ToString() + "' and id=" + ds.Tables[0].Rows[0]["Customer_Id"];
                DataSet ds1 = GetDataSet(query);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    result = "USER";
                }
                else
                {
                    result = "Access_Denied";
                }
            }


            //check Admin
            if (result == "Access_Denied")
            {
                strSQL = "SELECT * FROM cc_relateddataset.login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "') AND (Role='ADMIN'))";
                DataSet ds2 = GetDataSet(strSQL);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    result = "ADMIN";
                }
                else
                {
                    result = "Access_Denied";
                }
            }

            return result;
        }
        public static DataSet Logininfo(string Name, string Password)
        {
            string strSQL = "SELECT * FROM  cc_relateddataset.login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
            return GetDataSet(strSQL);
        }
        public static bool Getcompanylist(string CompanyName)
        {
            bool result = false;
            string strSQL = "SELECT * FROM cc_masterdataset.customer";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (CompanyName == ds.Tables[0].Rows[i]["Name"].ToString())
                    {
                        result = true;
                        break;
                    }
                    else { result = false; }
                }

            }

            return result;

        }

        #endregion        

    


        #region Application
        public struct DomainAttributes
        {
            public int Length;
            public bool IsMultidomain;
            public int DmID;
            public string DmName;
            public int SubDmID;
            public string SubDmName;         

            #region Application settings
            public string EducationalInstruction;
            public string DeliveryInstruction;
            public string DocumentInstruction;
            public int IncludedCopies;
            public int IsRedirect;
            public string RedirectUrl;
            public int IsCreditcard;
            public string SupportedCards;
            public bool AddSection;
            public bool FaxSection;
            public bool EmailSection;
            public bool Talentdb;
            public bool PurposeSection;
            public int Lock_PurposeId;
            public string Lock_TargetName;
            public string Lock_State;
            public string Spl_Instruction;
            public string Completed_Instruction;
            public bool Appl_Upload;
            public int App_Type;
            public int Target_Section;
            public int Onlinecc;
            public string Creditcard_Instructions;
            public int Moneyorder;
            public string Moneyorder_Instructions;
            public int Personalcheck;
            public string Personalcheck_Instructions;
            #endregion

            #region Cost Settings
            public int FaxCopy;
            public int AdditionalCopy;
            public int EmailCopy;
            #endregion



        }
        public static DomainAttributes SplitUrl(Uri url, string subdomain)
        {
            string Appsettingquery = "Select * from [cc_masterdataset].[customersettings] where [Customer_Id]=";
            string Faxcostquery = "SELECT Cost FROM cc_clientdataset.delivery_type where (Type = 'Fax') AND Customer_Id=";
            string Emailcostquery = "SELECT Cost FROM cc_clientdataset.delivery_type where (Type = 'Email') AND Customer_Id=";
            string AdditionalCopyquery = "SELECT Cost FROM cc_clientdataset.service WHERE (Type = 'Additional Copy') AND Customer_Id =";

            DomainAttributes Dm = new DomainAttributes();
            ClientAttributes client = new ClientAttributes();
            string host = url.Host;
            Dm.Length = host.Split('.').Length;

            if (subdomain != "nosubdomain")
            {
                Dm.IsMultidomain = true;
                client = GetClientInfo(host.Split('.')[0]);
                Dm.DmID = client.ID;
                Dm.DmName = client.Name;
                client = GetClientInfo(subdomain);
                Dm.SubDmID = client.ID;
                Dm.SubDmName = client.Name;
                Appsettingquery += Dm.SubDmID;
                Faxcostquery += Dm.SubDmID;
                AdditionalCopyquery += Dm.SubDmID;
                Emailcostquery += Dm.SubDmID;
            }
            else
            {
                    client = GetClientInfo(host.Split('.')[0]);
                    Dm.IsMultidomain = false;
                    Dm.DmID = client.ID;
                    Dm.DmName = client.Name;
                    Dm.SubDmID = 0;
                    Dm.SubDmName = "";
                    Appsettingquery += Dm.DmID;
                    Faxcostquery += Dm.DmID;
                    AdditionalCopyquery += Dm.DmID;
                    Emailcostquery += Dm.DmID;
            }

            //switch (Dm.Length)
            //{
            //    case 3:
            //        client = GetClientInfo(host.Split('.')[0]);
            //        Dm.IsMultidomain = false;
            //        Dm.DmID = client.ID;
            //        Dm.DmName = client.Name;
            //        Dm.SubDmID = 0;
            //        Dm.SubDmName = "";
            //        Appsettingquery += Dm.DmID;
            //        Faxcostquery += Dm.DmID;
            //        AdditionalCopyquery += Dm.DmID;
            //        Emailcostquery += Dm.DmID;
            //        break;
            //    case 4:
            //        Dm.IsMultidomain = true;
            //        client = GetClientInfo(host.Split('.')[1]);
            //        Dm.DmID = client.ID;
            //        Dm.DmName = client.Name;
            //        client = GetClientInfo(host.Split('.')[0]);
            //        Dm.SubDmID = client.ID;
            //        Dm.SubDmName = client.Name;
            //        Appsettingquery += Dm.SubDmID;
            //        Faxcostquery += Dm.SubDmID;
            //        AdditionalCopyquery += Dm.SubDmID;
            //        Emailcostquery += Dm.SubDmID;
            //        break;
            //}





            //Application settings
            DataSet ds = new DataSet();
            ds = GetDataSet(Appsettingquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Dm.EducationalInstruction = ds.Tables[0].Rows[0]["Education_Instructions"].ToString();
                Dm.DeliveryInstruction = ds.Tables[0].Rows[0]["Delivery_Instructions"].ToString();
                Dm.DocumentInstruction = ds.Tables[0].Rows[0]["Document_Instructions"].ToString();
                Dm.IncludedCopies = Convert.ToInt32(ds.Tables[0].Rows[0]["Delivery_copy"].ToString());
                Dm.IsRedirect = Convert.ToInt32(ds.Tables[0].Rows[0]["ThkuPage"].ToString());
                Dm.RedirectUrl = ds.Tables[0].Rows[0]["SiteUrl"].ToString();
                Dm.IsCreditcard = Convert.ToInt32(ds.Tables[0].Rows[0]["CreditCard"].ToString());
                Dm.SupportedCards = ds.Tables[0].Rows[0]["Credit_Type"].ToString();
                Dm.AddSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Additional_Section"].ToString());
                Dm.FaxSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Fax_Section"].ToString());
                Dm.EmailSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Email_Section"].ToString());
                Dm.Talentdb = Convert.ToBoolean(ds.Tables[0].Rows[0]["Talent_Database"].ToString());
                Dm.PurposeSection  = Convert.ToBoolean(ds.Tables[0].Rows[0]["Purpose_Section"].ToString());
                Dm.Lock_PurposeId  = Convert.ToInt32(ds.Tables[0].Rows[0]["Lock_PurposeId"].ToString());
                Dm.Lock_TargetName  = ds.Tables[0].Rows[0]["Lock_TargetName"].ToString();
                Dm.Lock_State  = ds.Tables[0].Rows[0]["Lock_State"].ToString();
                Dm.Spl_Instruction = ds.Tables[0].Rows[0]["Spl_Instruction"].ToString();
                Dm.Completed_Instruction = ds.Tables[0].Rows[0]["Completed_Instruction"].ToString();
                Dm.Appl_Upload = Convert.ToBoolean(ds.Tables[0].Rows[0]["Applicant_Upload"].ToString());
                Dm.App_Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Application_Type"].ToString());
                Dm.Target_Section = Convert.ToInt32(ds.Tables[0].Rows[0]["Target_Section"].ToString());
                Dm.Onlinecc = Convert.ToInt32(ds.Tables[0].Rows[0]["Onlinecc"].ToString());
                Dm.Creditcard_Instructions = ds.Tables[0].Rows[0]["Creditcard_Instructions"].ToString();
                Dm.Moneyorder = Convert.ToInt32(ds.Tables[0].Rows[0]["Moneyorder"].ToString());
                Dm.Moneyorder_Instructions = ds.Tables[0].Rows[0]["Moneyorder_Instructions"].ToString();
                Dm.Personalcheck = Convert.ToInt32(ds.Tables[0].Rows[0]["Personalcheck"].ToString());
                Dm.Personalcheck_Instructions = ds.Tables[0].Rows[0]["Personalcheck_Instructions"].ToString();
            }
            //Cost settings
            ds.Clear();
            ds = GetDataSet(Faxcostquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Dm.FaxCopy = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            else { Dm.FaxCopy = 0; }

            ds.Clear();
            ds = GetDataSet(AdditionalCopyquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Dm.AdditionalCopy = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            else { Dm.AdditionalCopy = 0; }
            ds.Clear();
            ds = GetDataSet(Emailcostquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Dm.EmailCopy = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            else { Dm.EmailCopy = 0; }


            return Dm;
        }
        public static DomainAttributes GetClient(Uri url, string subdomain)
        {
            DomainAttributes Dm = new DomainAttributes();
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

            url = GetUrl(url, IsLive, false);

            Dm = SplitUrl(url,subdomain);

            return Dm;
        }

        public struct SessionVariable
        {
            public int Applicant_id;
            public int Request_id;
            public int SubClient_id;
            public int Customer_id;
            public int page1;
            public int page2;
            public int page3;
            public int page4;
        }

        public static DataSet Splashpage(int clientid)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.splashpagesettings WHERE Customer_Id=" + clientid; 
            return GetDataSet(strSQL);
        }
        #endregion

        #region CommonFunction
         public struct ClientAttributes
         {
             public int ID;
             public string Name;
             public string Subdm;
             public bool Isvalid;            
         }
         public static ClientAttributes GetClientInfo(string customer_code)
         {
             ClientAttributes Client = new ClientAttributes();
             string query = "Select id,Name,SubDomainName from cc_masterdataset.customer where SubDomainName='" + customer_code + "'";
             SqlConnection conn = new SqlConnection(DBConnectionString());
             conn.Open();
             SqlCommand cmd = new SqlCommand();
             cmd = new SqlCommand(query, conn);
             SqlDataReader rd = cmd.ExecuteReader();
             rd.Read();
             if (rd.HasRows)
             {
                Client.ID  = Convert.ToInt32(rd["id"].ToString());
                Client.Name = rd["Name"].ToString();
                Client.Isvalid = true;
                Client.Subdm = rd["SubDomainName"].ToString();
             }
             else
             {
                 Client.ID = 0;
                 Client.Name = "Wrong Domain";
                 Client.Isvalid = false;
                 Client.Subdm = "Wrong Sub Domain";
             }
             conn.Close();
             return Client; 

         }
         public static bool ClientIsValid1(Uri url)
         {
             RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
             bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

             url = GetUrl(url,IsLive, false);   
             bool result = false;
             string host = url.Host;
             int dmLength = host.Split('.').Length;
            
             string query = "Select id,Name from cc_masterdataset.customer where SubDomainName='";

             switch (dmLength)
             {
                 case 3:
                     query += host.Split('.')[0] + "' AND Parent='SELF'";
                     break;
                 case 4:
                     query += host.Split('.')[0] + "' AND Parent='" + host.Split('.')[1] + "'";
                     break;
                 default :
                     myredirect("Not in Use");
                     break;
             }


             SqlConnection conn = new SqlConnection(DBConnectionString());
             conn.Open();
             SqlCommand cmd = new SqlCommand();
             cmd = new SqlCommand(query, conn);
             SqlDataReader rd = cmd.ExecuteReader();
             rd.Read();
             if (rd.HasRows)
             {
                 result = true;
             }
             else
             {
                 myredirect("Not in Use");
                 result = false;
             }
             conn.Close();

             return result;
         }
         public static bool ClientIsValid2(Uri url, string subdomain)
         {
             RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
             bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

             url = GetUrl(url, IsLive, false);
             bool result = false;
             string host = url.Host;
             int dmLength = host.Split('.').Length;

             if (host.Split('.')[0].ToString() != "www")
             {
             string query = "Select id,Name from cc_masterdataset.customer where";

             if (subdomain == "nosubdomain")
             {
                 query += " SubDomainName='" + host.Split('.')[0] + "' AND Parent='SELF'";
             }
             else
             {
                 query += " SubDomainName='" + subdomain  + "' AND Parent='" + host.Split('.')[0] + "'";
             }

           

             //switch (dmLength)
             //{
             //    case 3:
             //        query += host.Split('.')[0] + "' AND Parent='SELF'";
             //        break;
             //    case 4:
             //        query += host.Split('.')[0] + "' AND Parent='" + host.Split('.')[1] + "'";
             //        break;
             //    default:
             //        myredirect();
             //        break;
             //}


             SqlConnection conn = new SqlConnection(DBConnectionString());
             conn.Open();
             SqlCommand cmd = new SqlCommand();
             cmd = new SqlCommand(query, conn);
             SqlDataReader rd = cmd.ExecuteReader();
             rd.Read();
             if (rd.HasRows)
             {
                 result = true;
             }
             else
             {
                 myredirect("Not in Use");
                 result = false;
             }
             conn.Close();
             }
             else
             {
                // HttpContext.Current.Response.Redirect("http://" + host.Replace("www.", "").ToString());  
                // HttpContext.Current.Response.Redirect("~/Urlcheck.aspx");
                 HttpContext.Current.Response.Redirect("http://www.credentialconsultants.com/");
             }
             return result;
         }
         public static bool ClientIsValid(Uri url, string subdomain)
         {
             RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
             bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

             url = GetUrl(url, IsLive, false);
             bool result = false;
             string host = url.Host;
             int dmLength = host.Split('.').Length;
             string sQuery = url.Query;
             if (sQuery.Contains("subdomain="))
             {

                 subdomain = HttpUtility.ParseQueryString(sQuery).Get("subdomain");
             }
             else
             {
                 subdomain = "";
             }

           

             if (System.Configuration.ConfigurationSettings.AppSettings["Test"].ToString() == "1")
             {
                 HttpContext.Current.Response.Clear();
                 HttpContext.Current.Response.Write(url + "-Invalid Url");
                 HttpContext.Current.Response.End();
             }
             if (!subdomain.Contains("www"))
             {
               string query = "Select id,Name from cc_masterdataset.customer where";

                 if (subdomain == "")
                 {
                     query += " SubDomainName='" + host.Split('.')[0] + "' AND Parent='SELF'";
                 }
                 else
                 {
                     query += " SubDomainName='" + subdomain + "' AND Parent='" + host.Split('.')[0] + "'";
                 }

                 SqlConnection conn = new SqlConnection(DBConnectionString());
                 conn.Open();
                 SqlCommand cmd = new SqlCommand();
                 cmd = new SqlCommand(query, conn);
                 SqlDataReader rd = cmd.ExecuteReader();
                 rd.Read();
                 if (rd.HasRows)
                 {
                     result = true;
                 }
                 else
                 {
                     myredirect(url.ToString());
                     result = false;
                 }
                 conn.Close();
             }
             else
             {

                 string domain = host.Split('.')[0];
                 if ((!subdomain.Contains("www.")) || (subdomain==""))
                 {
                     HttpContext.Current.Response.Redirect("https://" + domain + ".credentialconnection.com/Default.aspx");
                 }
                 else
                 {

                     // HttpContext.Current.Response.Redirect("http://" + host.Replace("www.", "").ToString());  
                     // HttpContext.Current.Response.Redirect("~/Urlcheck.aspx");
                     HttpContext.Current.Response.Redirect("https://" + domain + ".credentialconnection.com/Default.aspx?subdomain=" + subdomain.Replace("www.", "").ToString());
                 }
             }
             return result;
         }
         public static void rewriteurl(Uri url)
         {
             RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
             bool IsLive = Convert.ToBoolean(app.TypeSwitcher);

             bool mode = false ;
             url = GetUrl(url, IsLive, false);
             bool result = false;
             string host = url.Host;
             int dmLength = host.Split('.').Length;

             if (host.Split('.')[0].ToString() != "www")
             {
             string query = "Select id,Name from cc_masterdataset.customer where SubDomainName='";

             switch (dmLength)
             {
                 case 3:
                     query += host.Split('.')[0] + "' AND Parent='SELF'";
                    mode = false;
                     break;
                 case 4:
                     query += host.Split('.')[0] + "' AND Parent='" + host.Split('.')[1] + "'";
                    mode = true;
                     break;
                 default:
                     myredirect("Not in Use");
                     break;
             }


             SqlConnection conn = new SqlConnection(DBConnectionString());
             conn.Open();
             SqlCommand cmd = new SqlCommand();
             cmd = new SqlCommand(query, conn);
             SqlDataReader rd = cmd.ExecuteReader();
             rd.Read();
             if (rd.HasRows)
             {
                 result = true;
             }
             else
             {
                 myredirect("Not in Use");
                 result = false;
             }
             conn.Close();

             if (result)
             {
                if(mode)
                {
                  reseturl(IsLive,host.Split('.')[1],host.Split('.')[0],mode);
                }
                     else
                     {
                    reseturl(IsLive,host.Split('.')[0], "", mode);
                }
             }
             }
             else {                
                 //HttpContext.Current.Response.Redirect("http://" + host.Replace("www.","").ToString());  
                 //HttpContext.Current.Response.Redirect("~/Urlcheck.aspx");
                 HttpContext.Current.Response.Redirect("http://www.credentialconsultants.com/");
             }
            
         }
         public static void checklogo(int CompanyId,HtmlGenericControl title,HtmlImage logo)
         {         
          string url = "~/Assets/logo/" + CompanyId + ".png";
          

             if (File.Exists(HttpContext.Current.Server.MapPath(url)))
             {
                 System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(url));
                 int ogheight = image.Height;
                 image.Dispose();
                 if (ogheight  < 150)
                 {
                     logo.Src = url;
                     logo.Visible = true;
                     title.Visible = false; 
      
                     //Image img = new Image();
                     //img.ImageUrl = url;
                     //title.Controls.Add(img);
                     //title.Attributes.Add("style", "text-indent: -999px;");
                     //title.Attributes.Add("style", "background:url(" + url + ") 25px 5px no-repeat;text-indent: -999px;height:" + image.Height + "px;");
                 }
                 else
                 {                   
                     logo.Visible = false;                   
                     title.Visible = true;                    
                     //title.Attributes.Remove("style");
                     //title.Attributes.Add("class", "clientTitle");
                     
                 }
                 
             }
             else
             {
                 logo.Visible = false;                
                 title.Visible = true;
               
                 //title.Attributes.Remove("style");
                 //title.Attributes.Add("class", "clientTitle");
             }
         

         }
        #endregion



        //demo
         public static Uri GetUrl(Uri url,bool Islive,bool IsAdmin)
         {
             
             if (!Islive)
             {
                 if (IsAdmin)
                 {
                     url = new Uri("https://sdr.credentialconnection.com");
                 }
                 else
                 {
                     url = new Uri("https://sdr.credentialconnection.com");
                 }
             }
             return url;

         }
        public static void myredirect(string url)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(url + "-Invalid Url");
            HttpContext.Current.Response.End();  
           // HttpContext.Current.Response.Redirect("http://www.credentialconsultants.com/");
        }
        public static void reseturl(bool Isdemo,string domain,string subdomain,bool mode)
        {
           
            string url = "";
            if (Isdemo)
            {
                if (mode)
                {
                    url = "https://" + domain + ".credentialconnection.com/Default.aspx?subdomain=" + subdomain;
                }
                else
                {
                    url = "https://" + domain + ".credentialconnection.com/Default.aspx";
                }
            }
            else
            {
                url = "~/Default.aspx";
            }
            HttpContext.Current.Response.Redirect(url);
        }

    }

    public class secureurl
    {
        public secureurl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //Function to encode the string
		static public string TamperProofStringEncode(string value, string key)
		{
			System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
			return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
		}

		//Function to decode the string
		//Throws an exception if the data is corrupt
		static public string TamperProofStringDecode(string value, string key)
		{
			String dataValue = "";
			String calcHash = "";
			String storedHash = "";

			System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

			try
			{
				dataValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[0]));
				storedHash = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[1]));
				calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

				if (storedHash != calcHash)
				{
					//Data was corrupted
					throw new ArgumentException("Hash value does not match");
					//This error is immediately caught below
				}

			}
			catch (System.Exception)
			{
                HttpContext.Current.Response.Clear(); 
                HttpContext.Current.Response.Write("Invalid Url");
                HttpContext.Current.Response.End();  
			}
			return dataValue;
		}

		static public string QueryStringEncode(string value)
		{
            return System.Web.HttpUtility.UrlEncode(TamperProofStringEncode(value, System.Configuration.ConfigurationSettings.AppSettings["TamperProofKey"]));
		}

		static public string QueryStringDecode(string value)
		{
			return TamperProofStringDecode(value, System.Configuration.ConfigurationSettings.AppSettings["TamperProofKey"]);
		}
     
    }
}