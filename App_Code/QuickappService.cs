// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class QuickappService : WebService
{
    public QuickappService()
    {
    }

    #region Db Connections
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

    public static void Getmonth(DropDownList month)
    {
        for (int i = 0; i <= 12; i++)
        {
            if (i == 0)
            {
                month.Items.Add(" ");
            }
            else
            {
                month.Items.Add(i.ToString());
            }
        }
    }
    public static void Getdate(DropDownList date)
    {
        for (int i = 0; i <= 31; i++)
        {
            if (i == 0)
            {
                date.Items.Add(" ");
            }
            else
            {
                date.Items.Add(i.ToString());
            }
        }
    }
    public static void Getyear(DropDownList year, int start_year, int end_year,bool Isinclude)
    {
        year.Items.Clear(); 
        end_year = DateTime.Now.Year;
        for (int i = start_year; i <= end_year; i++)
        {            
            if (i == start_year)
            {
                if (Isinclude)
                {
                    year.Items.Add(" ");
                    year.Items.Add(i.ToString());
                }
                else { year.Items.Add(" "); 
                }
            }
            else
            {
                year.Items.Add(i.ToString());
            }
        }
    }
    public static void Getcountry(DropDownList country)
    {
        //string strSQL = "SELECT * FROM cc_masterdataset.countries";
        string strSQL = "SELECT * from cc_masterdataset.countries ORDER by (case when Name = 'United States' then 0 else 1 end) asc, Name asc";
        DataSet ds = GetDataSet(strSQL);
        country.DataSource = ds;
        country.DataTextField = "Name";
        country.DataValueField = "Id";
        country.Items.Add(new ListItem("Select", "0"));
        country.DataBind();

    }
    public static void GetPurposeList(DropDownList purpose)
    {      
        string strSQL = "select id,Evaluation_Name from cc_clientdataset.evaluation_purpose order by priority";
        DataSet ds = GetDataSet(strSQL);
        purpose.DataSource = ds;
        purpose.DataTextField = "Evaluation_Name";
        purpose.DataValueField = "id";
        purpose.Items.Add(new ListItem("Select", "0"));
        purpose.DataBind();
    }
    public static void GetSubclients(DropDownList Clients, int customer_Id, bool Isall)
    {
        string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where SubDomainName=" +
        "(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") OR Parent=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") order by SubDomainName";//id -> SubDomainName
        DataSet ds = GetDataSet(strSQL);
        Clients.DataSource = ds;
        Clients.DataTextField = "SubDomainName";
        Clients.DataValueField = "id";
        if (Isall)
        {
            Clients.Items.Add(new ListItem("All", "0"));
        }
        else { Clients.Items.Add(new ListItem("Select", "0")); }
        Clients.DataBind();

    }
    public static void Getdegree(DropDownList degree, int type, int Country_id, string customer)
    {       
        string strSQL;
        if (type == 0)
        {

            strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0)) order by Name";
        }
        else
        {
            strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0)) order by Name";
        }
        GetEducationplan(degree, strSQL);
    }
    public static void GetEducationplan(DropDownList options, string query)
    {
        string strSQL = query;
        DataSet ds = GetDataSet(strSQL);
        options.DataSource = ds;
        options.DataTextField = "Name";
        options.DataValueField = "Id";
        options.Items.Add(new ListItem("Select", "0"));
        options.DataBind();

    }
    public static void Add_New(DropDownList options)
    {
        options.Items.Add("Add New");
    }
    public static void Getmajor(DropDownList options, int Country_id, string customer)
    {      
        string strSQL = "select Id,Name from cc_masterdataset.major where (Country_ID = " + Country_id + ") AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0)) order by Name";
        DataSet ds = GetDataSet(strSQL);
        options.DataSource = ds;
        options.DataTextField = "Name";
        options.DataValueField = "Id";
        options.Items.Add(new ListItem("Select", "0"));
        options.DataBind();

    }

    public static int create_Quickapp(string FirstName, string MiddleName, string LastName, int Countryofbirth, string otherFirstName, string otherMiddleName, string otherLastName, string Addressline1, string Addressline2, string City, string State_or_province, string Zip_or_PostalCode, int CountryId, string HomePhone, string WorkPhone, string MobilePhone, string DateOfBirth, string Gender, string Email, int Join_cc_db, int Publish_Info, int Customer_Id)
    {
        string query = "INSERT INTO cc_clientdataset.applicant (FirstName, MiddleName, LastName, Gender, DateOfBirth, Addressline1,Addressline2, City, CountryId, State_or_province, Zip_or_PostalCode, HomePhone, WorkPhone, MobilePhone, Customer_Id,otherFirstName,otherMiddleName,otherLastName,Countryofbirth,Email,Join_cc_db,Publish_Info) VALUES ('" + FirstName.Replace("'", "''") + "','" + MiddleName.Replace("'", "''") + "','" + LastName.Replace("'", "''") + "','" + Gender + "','" + DateOfBirth + "','" + Addressline1.Replace("'", "''") + "','" + Addressline2.Replace("'", "''") + "','" + City.Replace("'", "''") + "'," + CountryId + ",'" + State_or_province.Replace("'", "''") + "','" + Zip_or_PostalCode + "','" + HomePhone + "','" + WorkPhone + "','" + MobilePhone + "'," + Customer_Id + ",'" + otherFirstName.Replace("'", "''") + "','" + otherMiddleName.Replace("'", "''") + "','" + otherLastName.Replace("'", "''") + "'," + Countryofbirth + ",'" + Email.Replace("'", "''") + "'," + Join_cc_db + "," + Publish_Info + ")";
        int applicant_id = GetDataSet_withID(query);
        return applicant_id;
    }

    public static string create_Applicantid(int id)
    {
        string FileNumber = create_Randomid();
        string query = "UPDATE cc_clientdataset.applicant SET FileNumber='" + FileNumber + "' WHERE id=" + id;
        GetDataSet_withoutID(query);
        return FileNumber;
    }
    public static int create_Quickapp_purpose(int Applicant_Id, int Purpose_Id, string Eval_institution, string Eval_organization, string Eval_Attorney, string Eval_Board, string Eval_State, string Eval_Military_Recruiter, string Eval_other)
    {
        string query = "INSERT INTO cc_clientdataset.evaluation_request (Applicant_Id, Purpose_Id, Eval_institution, Eval_organization, Eval_Attorney, Eval_Board, Eval_State, Eval_Military_Recruiter, Eval_other,Application_Recieved,Documents_Recieved,Payment_Recieved,Evaluation_Complete,Verification_Complete,Evaluation_Approved,Packaging_Complete,Delievery_Complete) VALUES (" + Applicant_Id + "," + Purpose_Id + ",'" + Eval_institution.Replace("'", "''") + "','" + Eval_organization.Replace("'", "''") + "','" + Eval_Attorney.Replace("'", "''") + "','" + Eval_Board.Replace("'", "''") + "','" + Eval_State.Replace("'", "''") + "','" + Eval_Military_Recruiter.Replace("'", "''") + "','" + Eval_other.Replace("'", "''") + "'," + "0,0,0,0,0,0,0,0)";
        int RequestID = GetDataSet_withID(query);
        return RequestID;

    }
    public static string create_Randomid()
    {
        string id = "";
        //option 1
        //int PasswordLength = 5;

        //string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
        //Random randNum = new Random();
        //char[] chars = new char[PasswordLength];
        //int allowedCharCount = _allowedChars.Length;
        //for (int i = 0; i < PasswordLength; i++)
        //{
        //    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        //    id = id + chars[i].ToString();
        //}
        //option 2:
        //bool result = false;

        //           do
        //           {

        //               string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
        //               Random randNum = new Random();
        //               char[] chars = new char[PasswordLength];
        //               int allowedCharCount = _allowedChars.Length;
        //               for (int i = 0; i < PasswordLength; i++)
        //               {
        //                   chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        //                   id = id + chars[i].ToString();
        //               }

        //               string FileNumber = id;
        //               string query = "Select * from cc_clientdataset.applicant Where FileNumber='" + FileNumber + "'";
        //               DataSet ds = GetDataSet(query);
        //               if (ds.Tables[0].Rows.Count > 0)
        //               {
        //                   result = true;
        //                   id = "";
        //               }

        //           } while (result);


        //  return id;

        string constring = DBConnectionString();
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("GetFilenumber", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@r", SqlDbType.VarChar, 5);
                cmd.Parameters["@r"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return id = cmd.Parameters["@r"].Value.ToString();
            }
        }
    }
    public static int AddNew_institution(string Name, int Country_ID, string category, string customer)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        int institution_id;
        string query = "SELECT Id FROM cc_masterdataset.institution WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "') AND (Type = '" + category.Replace("'", "''") + "') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
        int result = GetDataSet_reader(query);

        if (result == 0)
        {
            query = "INSERT INTO cc_masterdataset.institution(Name, Country_ID, Type,Confirmed,Customer_Id,Category) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + category.Replace("'", "''") + "',0,'" + customer.ToString() + "','Client')";
            institution_id = GetDataSet_withID(query);
        }
        else
        {
            institution_id = result;
        }
        return institution_id;
    }
    public static int AddNew_degree(string Name, int Country_ID, string category, string customer)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        int degree_id;
        string query = "SELECT Id FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "') AND (Type = '" + category.Replace("'", "''") + "') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
        int result = GetDataSet_reader(query);

        if (result == 0)
        {
            query = "INSERT INTO cc_masterdataset.degree(Name, Country_ID, Type,Confirmed,Customer_Id,Category,EquiDegree_id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + category.Replace("'", "''") + "',0,'" + customer.ToString() + "','Client',0)";
            degree_id = GetDataSet_withID(query);
        }
        else
        {
            degree_id = result;
        }
        return degree_id;
    }
    public static int AddNew_major(string Name, int Country_ID, string customer)
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        int major_id;
        string query = "SELECT Id FROM cc_masterdataset.major WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "')AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
        int result = GetDataSet_reader(query);

        if (result == 0)
        {
            query = "INSERT INTO cc_masterdataset.major(Name, Country_ID,Confirmed,Customer_Id,Category,EquiMajor_id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + ",0,'" + customer.ToString() + "','Client',0)";
            major_id = GetDataSet_withID(query);
        }
        else
        {
            major_id = result;
        }
        return major_id;
    }
    public static bool create_education(int Evaluation_Request_Id, int Country_Id, int Major_Id, int EducationInstitution_Id, string StartDate, string EndDate, int Degree_Id, int DegreeObtained, string DateDegreeAwarded, int US_Equivalency_Id, string City, string State)
    {
        string query = "INSERT INTO cc_clientdataset.applicant_education_history (Evaluation_Request_Id, Country_Id,Major_Id, EducationInstitution_Id, StartDate, EndDate, Degree_Id, DegreeObtained,DateDegreeAwarded,US_Equivalency_Id,City,State_or_province) VALUES  (" + Evaluation_Request_Id + "," + Country_Id + "," + Major_Id + "," + EducationInstitution_Id + ",'" + StartDate + "','" + EndDate + "'," + Degree_Id + "," + DegreeObtained + ",'" + DateDegreeAwarded + "'," + US_Equivalency_Id + ",'" + City.Replace("'", "''") + "','" + State.Replace("'", "''") + "')";
        bool result = GetDataSet_withoutID(query);
        return result;
    }

    [WebMethod]
    public static List<string> GetInstitutionList(string prefixText, string type, string country, string customer)
    {
          string strSQL;
          if (type == "0")
          {
              strSQL = "SELECT Name FROM cc_masterdataset.institution WHERE (Name like '" + prefixText + "%')AND (Country_ID = " + Convert.ToInt32(country.ToString()) + ") AND (Type = 'HighSchool') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
          }
          else
          {
              strSQL = "SELECT Name FROM cc_masterdataset.institution WHERE (Name like '" + prefixText + "%')AND (Country_ID = " + Convert.ToInt32(country.ToString()) + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
          }
        DataSet ds = GetDataSet(strSQL);
        List<string> items = new List<string>(ds.Tables[0].Rows.Count);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                items.Add(ds.Tables[0].Rows[i]["Name"].ToString());
            }

        }
        return items;

    }

    [WebMethod]
    public static List<string> GetdegreeList(string prefixText,string type, string Country_id, string customer)
    {
        string strSQL;
        if (type == "0")
        {

            strSQL = "SELECT Name FROM cc_masterdataset.degree WHERE (Name like '" + prefixText + "%') AND (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
        }
        else
        {
            strSQL = "SELECT Name FROM cc_masterdataset.degree WHERE (Name like '" + prefixText + "%') AND (Country_ID = " + Country_id + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
        }

        DataSet ds = GetDataSet(strSQL);
        List<string> items = new List<string>(ds.Tables[0].Rows.Count);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                items.Add(ds.Tables[0].Rows[i]["Name"].ToString());
            }

        }
        return items;
       
    }

    [WebMethod]
    public static List<string> GetmajorList(string prefixText, string Country_id, string customer)
    {
        string strSQL = "select Name from cc_masterdataset.major where (Name like '" + prefixText + "%') AND (Country_ID = " + Country_id + ") AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0)) order by Name";
        DataSet ds = GetDataSet(strSQL);
        List<string> items = new List<string>(ds.Tables[0].Rows.Count);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                items.Add(ds.Tables[0].Rows[i]["Name"].ToString());
            }

        }
        return items;

    }

    public static string SubmitApplication(DataTable dtinfo, DataTable dtupper, DataTable dtpost, string clientid)
    {
        string appresult = "";
        int App_id = 0;
        int Req_id = 0;
        string filenumber = "";

        string strSQL = "";
        strSQL = "SELECT [id],[SubDomainName] FROM [cc_masterdataset].[customer] where id=" + clientid;
        DataSet ds = GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)//client exists
        {
            int clientId = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());

            //application
            if (dtinfo.Rows.Count > 0)
            {
                int applicant_id = create_Quickapp(dtinfo.Rows[0]["FirstName"].ToString(), dtinfo.Rows[0]["MiddleName"].ToString(), dtinfo.Rows[0]["LastName"].ToString(), getCountryId(dtinfo.Rows[0]["Countryofbirth"].ToString()), dtinfo.Rows[0]["otherFirstName"].ToString(), dtinfo.Rows[0]["otherMiddleName"].ToString(), dtinfo.Rows[0]["otherLastName"].ToString(), dtinfo.Rows[0]["Addressline1"].ToString(), dtinfo.Rows[0]["Addressline2"].ToString(), dtinfo.Rows[0]["City"].ToString(), dtinfo.Rows[0]["State_or_province"].ToString(), dtinfo.Rows[0]["Zip_or_PostalCode"].ToString(), getCountryId(dtinfo.Rows[0]["Country"].ToString()), dtinfo.Rows[0]["HomePhone"].ToString(), dtinfo.Rows[0]["WorkPhone"].ToString(), dtinfo.Rows[0]["MobilePhone"].ToString(), dtinfo.Rows[0]["DateOfBirth"].ToString(), dtinfo.Rows[0]["Gender"].ToString(), dtinfo.Rows[0]["Email"].ToString(), 0, 0, clientId);
                if (applicant_id != 0)
                {
                    App_id = applicant_id;
                    filenumber = create_Applicantid(App_id);
                }
            }

            //purpose
            Req_id = create_Quickapp_purpose(App_id, getpurposeId(dtinfo.Rows[0]["Purpose"].ToString()), "", "", "", "", "", "", "");



            //education high school            
            if (dtupper.Rows.Count > 0)
            {
                for (int i = 0; i < dtupper.Rows.Count; i++)
                {
                    string DateDegreeAwarded = "";
                    int institution_id = 0;
                    int degree_id = 0;
                    int graduated = 0;
                    institution_id = AddNew_institution(dtupper.Rows[i]["Institution"].ToString(), getCountryId(dtupper.Rows[i]["Country"].ToString()), "HighSchool", clientId.ToString());
                    degree_id = AddNew_degree(dtupper.Rows[i]["Degree"].ToString(), getCountryId(dtupper.Rows[i]["Country"].ToString()), "HighSchool", clientId.ToString());

                    if (dtupper.Rows[i]["Graduated"].ToString() == "True")
                    {
                        graduated = 1;
                        DateDegreeAwarded = "" + "/" + "" + "/" + dtupper.Rows[i]["Graduation"].ToString();
                    }
                    else { graduated = 0; DateDegreeAwarded = "Null"; }



                    bool result = create_education(Req_id, getCountryId(dtupper.Rows[i]["Country"].ToString()), 1, institution_id, dtupper.Rows[i]["StartDate"].ToString(), dtupper.Rows[i]["EndDate"].ToString(), degree_id, graduated, DateDegreeAwarded, 1, dtupper.Rows[i]["City"].ToString(), dtupper.Rows[i]["State"].ToString());

                }
            }


            //  education univ
            if (dtpost.Rows.Count > 0)
            {
                for (int i = 0; i < dtpost.Rows.Count; i++)
                {
                    string DateDegreeAwarded = "";
                    int institution_id = 0;
                    int degree_id = 0;
                    int major_id = 0;
                    int graduated = 0;

                    institution_id = AddNew_institution(dtpost.Rows[i]["Institution"].ToString(), getCountryId(dtpost.Rows[i]["Country"].ToString()), "University", clientId.ToString());
                    degree_id = AddNew_degree(dtpost.Rows[i]["Degree"].ToString(), getCountryId(dtpost.Rows[i]["Country"].ToString()), "University", clientId.ToString());
                    if (dtpost.Rows[i]["major"].ToString() != "")
                    {
                        major_id = AddNew_major(dtpost.Rows[i]["major"].ToString(), getCountryId(dtpost.Rows[i]["Country"].ToString()), clientId.ToString());
                    }
                    else { major_id = 0; }

                    if (dtpost.Rows[i]["Graduated"].ToString() == "True")
                    {
                        graduated = 1;
                        DateDegreeAwarded = "" + "/" + "" + "/" + dtpost.Rows[i]["Graduation"].ToString();
                    }
                    else { graduated = 0; DateDegreeAwarded = "Null"; }


                    bool result = create_education(Req_id, getCountryId(dtpost.Rows[i]["Country"].ToString()), major_id, institution_id, dtpost.Rows[i]["StartDate"].ToString(), dtpost.Rows[i]["EndDate"].ToString(), degree_id, graduated, DateDegreeAwarded, 1, dtpost.Rows[i]["City"].ToString(), dtpost.Rows[i]["State"].ToString());

                }
            }


            if ((App_id != 0) && (Req_id != 0) && (filenumber != ""))
            {
                appresult = filenumber + "|" + "" + "|" + "Successful";
            }
            else
            {
                appresult = "" + "|" + App_id + "," + Req_id + "," + filenumber + "|" + "UnSuccessful";
            }
        }
        else
        {
            appresult = "" + "|" + "WrongDomain" + "|" + "UnSuccessful";

        }

        return appresult;
    }
          public static int getCountryId(string name)
    {
        int id = 0;
        string strSQL = "SELECT Id FROM [cc_masterdataset].[countries] where Name='" + name.Replace("'", "''") + "'";
        DataSet ds = GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
        }
        return id;
    }
    public static int getpurposeId(string name)
    {
        int id = 0;
        id = ClientAdmin.Utility.GetpurposeId(name);    
        return id;
    }

}