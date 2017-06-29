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

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace Credentialpage
{
    public class Utility
    {
        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
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

        #region Set Focus javascript
        public static void SetFocus(Control control)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\r\n<script language='JavaScript'>\r\n");
            sb.Append("<!--\r\n");
            sb.Append("function SetFocus()\r\n");
            sb.Append("{\r\n");
            sb.Append("\tdocument.");

            Control p = control.Parent;
            while (!(p is System.Web.UI.HtmlControls.HtmlForm)) p = p.Parent;

            sb.Append(p.ClientID);
            sb.Append("['");
            sb.Append(control.UniqueID);
            sb.Append("'].focus();\r\n");
            sb.Append("}\r\n");
            sb.Append("window.onload = SetFocus;\r\n");
            sb.Append("// -->\r\n");
            sb.Append("</script>");

            control.Page.RegisterClientScriptBlock("SetFocus", sb.ToString());
        }
        #endregion

        #region Control initialization
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
        public static void Getyear(DropDownList year, int start_year, int end_year)
        {
            end_year = DateTime.Now.Year;
            for (int i = start_year; i <= end_year; i++)
            {
                if (i == start_year)
                {
                    year.Items.Add(" ");
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
        public static void Getoptional(RadioButtonList selection, HtmlTableRow row)
        {
            if (selection.SelectedValue == "True")
            {
                row.Visible = true;
            }
            else
            {
                row.Visible = false;
            }
        }
        public static void Getoptionalcell(RadioButtonList selection, HtmlTableCell cell)
        {
            if (selection.SelectedValue == "True")
            {
                cell.Visible = true;
            }
            else
            {
                cell.Visible = false;
            }
        }
        public static void Getpurpose(DropDownList options)
        {
            string strSQL = "select id,Evaluation_Name from cc_clientdataset.evaluation_purpose order by priority";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Evaluation_Name";
            options.DataValueField = "id";
            options.DataBind();

        }
        public static string Getpurposevalue(string id)
        {
            string result = "";
            string strSQL = "select id,Evaluation_Name from cc_clientdataset.evaluation_purpose where id="+id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["Evaluation_Name"].ToString();
            }
            else
            {
                result = "";
            }
            return result;

        }
        //public static void Getpaymentmode(DropDownList options)
        //{
        //    string list = "Select|cashiers check/money order|credit card|personal check";
        //    Getdropdownloader(list, options);
        //}
        public static void Getpaymentmode(DropDownList options, int morder, int creditcard, int personalcheck)
        {
            string list = "Select";
            if (morder == 1)
        {
                list += "|" + "cashiers check/money order";
            }
            if (creditcard == 1)
            {
                list += "|" + "credit card";
        }
            if (personalcheck == 1)
        {
                list += "|" + "personal check";
            }
            Getdropdownloader(list, options);
        }
        //public static void Getpaymentmode1(DropDownList options)
        //{
        //    string list = "Select|cashiers check/money order|personal check";
        //    Getdropdownloader(list, options);
        //}
        public static void Getdropdownloader(string list, DropDownList country)
        {
            ArrayList li = new ArrayList();
            li.AddRange(list.Split('|'));
            for (int i = 0; i <= li.Count - 1; i++)
            {
                country.Items.Add(li[i].ToString());
            }
        }
        //edu_dropdown
        //public static void GetInstitution_name(DropDownList options, string query)
        //{
        //    string strSQL = query;
        //    DataSet ds = GetDataSet(strSQL);
        //    options.DataSource = ds;
        //    options.DataTextField = "Name";
        //    options.DataValueField = "Id";
        //    options.Items.Add(new ListItem("Select", "0"));
        //    options.DataBind();
        //}
        //public static void Getinstitution(DropDownList institution, int type, int Country_id)
        //{
        //    string strSQL;
        //    if (type == 0)
        //    {
        //        strSQL = "SELECT Id, Name FROM cc_masterdataset.institution WHERE (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND (Confirmed = 1)";
        //    }
        //    else
        //    {
        //        strSQL = "SELECT Id, Name FROM cc_masterdataset.institution WHERE (Country_ID = " + Country_id + ") AND (Type = 'University') AND (Confirmed = 1)";
        //    }
        //    GetInstitution_name(institution, strSQL);
        //}
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
        public static void Getdegree(DropDownList degree, int type, int Country_id, string customer)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            string strSQL;
            if (type == 0)
            {

                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId   + ")) order by Name";
            }
            else
            {
                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + ")) order by Name";
            }
            GetEducationplan(degree, strSQL);
        }
        public static void Getmajor(DropDownList options, int Country_id, string customer)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            string strSQL = "select Id,Name from cc_masterdataset.major where (Country_ID = " + Country_id + ") AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + ")) order by Name";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Name";
            options.DataValueField = "Id";
            options.Items.Add(new ListItem("Select", "0"));
            options.DataBind();

        }
        public static void Getdeliverytype(DropDownList options, int Customer_id)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.delivery_type where (Customer_Id=" + Customer_id + ") AND ( Type='Mail') order by priority";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Name";
            options.DataValueField = "id";
            options.Items.Add(new ListItem("Select", "0"));
            options.DataBind();
        }
        public static void GetCount(DropDownList options,int count)
        {
            for (int i = 1; i <= count; i++)
            {
                //if (i == 0)
                //{
                //   options.Items.Add("Select");
                //}
                //else
                //{
                    options.Items.Add(i.ToString());
                //}
            }
        }

        //New values to the dropdown        
        public static void Add_New(DropDownList options)
        {
            options.Items.Add("Add New");
        }
        public static int AddNew_institution(string Name, int Country_ID, string category, string customer)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            int institution_id;
            string query = "SELECT Id FROM cc_masterdataset.institution WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "') AND (Type = '" + category + "') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
            int result = GetDataSet_reader(query);

            if (result == 0)
            {
                query = "INSERT INTO cc_masterdataset.institution(Name, Country_ID, Type,Confirmed,Customer_Id,Category) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + category + "',0,'" + customer.ToString() + "','Client')";
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
            string query = "SELECT Id FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "') AND (Type = '" + category + "') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
            int result = GetDataSet_reader(query);

            if (result == 0)
            {
                query = "INSERT INTO cc_masterdataset.degree(Name, Country_ID, Type,Confirmed,Customer_Id,Category,EquiDegree_id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + category + "',0,'" + customer.ToString() + "','Client',0)";
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

        public static DataSet GetHighschool(int Request_Id)
        {
            string strSQL = "SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate FROM ((cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id) INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id) WHERE ((cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.applicant_education_history.Evaluation_Request_Id <> 0 )) AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet GetUniversity(int Request_Id)
        {
            string strSQL = "SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate FROM ((cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id) INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id) WHERE ((cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.applicant_education_history.Evaluation_Request_Id <> 0 )) AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University')";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }

        #endregion
      
        #region Insertion and Upadtion of table
        //create records
        public static int create_Applicante(string FirstName, string MiddleName, string LastName, string Gender, string DateOfBirth, string Addressline1, string Addressline2, string City, int CountryId, string State_or_province, string Zip_or_PostalCode, string HomePhone, string WorkPhone, string MobilePhone, string Email, int Customer_Id, string otherFirstName, string otherMiddleName, string otherLastName, string PreviousCredential_id, int Countryofbirth, int Join_cc_db)
        {
            string query = "INSERT INTO cc_clientdataset.applicant (FirstName, MiddleName, LastName, Gender, DateOfBirth, Addressline1,Addressline2, City, CountryId, State_or_province, Zip_or_PostalCode, HomePhone, WorkPhone, MobilePhone, Email, Customer_Id,otherFirstName,otherMiddleName,otherLastName,PreviousCredential_id,Countryofbirth,Join_cc_db) VALUES ('" + FirstName.Replace("'", "''") + "','" + MiddleName.Replace("'", "''") + "','" + LastName.Replace("'", "''") + "','" + Gender.Replace("'", "''") + "','" + DateOfBirth + "','" + Addressline1.Replace("'", "''") + "','" + Addressline2.Replace("'", "''") + "','" + City.Replace("'", "''") + "'," + CountryId + ",'" + State_or_province.Replace("'", "''") + "','" + Zip_or_PostalCode.Replace("'", "''") + "','" + HomePhone.Replace("'", "''") + "','" + WorkPhone.Replace("'", "''") + "','" + MobilePhone.Replace("'", "''") + "','" + Email.Replace("'", "''") + "'," + Customer_Id + ",'" + otherFirstName.Replace("'", "''") + "','" + otherMiddleName.Replace("'", "''") + "','" + otherLastName.Replace("'", "''") + "','" + PreviousCredential_id.Replace("'", "''") + "'," + Countryofbirth + "," + Join_cc_db + ")";
            int applicant_id = GetDataSet_withID(query);
            return applicant_id;
        }
        public static int create_purpose(int Applicant_Id, int Purpose_Id, string Eval_institution, string Eval_organization, string Eval_Attorney, string Eval_Board, string Eval_State, string Eval_Military_Recruiter, string Eval_other)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_request (Applicant_Id, Purpose_Id, Eval_institution, Eval_organization, Eval_Attorney, Eval_Board, Eval_State, Eval_Military_Recruiter, Eval_other,Application_Recieved,Documents_Recieved,Payment_Recieved,Evaluation_Complete,Verification_Complete,Evaluation_Approved,Packaging_Complete,Delievery_Complete) VALUES (" + Applicant_Id + "," + Purpose_Id + ",'" + Eval_institution.Replace("'", "''") + "','" + Eval_organization.Replace("'", "''") + "','" + Eval_Attorney.Replace("'", "''") + "','" + Eval_Board.Replace("'", "''") + "','" + Eval_State.Replace("'", "''") + "','" + Eval_Military_Recruiter.Replace("'", "''") + "','" + Eval_other.Replace("'", "''") + "'," + "0,0,0,0,0,0,0,0)";
            int RequestID = GetDataSet_withID(query);
            return RequestID;

        }
        public static bool create_education(int Evaluation_Request_Id, int Country_Id, int Major_Id, int EducationInstitution_Id, string StartDate, string EndDate, int Degree_Id, int DegreeObtained, string DateDegreeAwarded, int US_Equivalency_Id, string City, string State)
        {
            string query = "INSERT INTO cc_clientdataset.applicant_education_history (Evaluation_Request_Id, Country_Id,Major_Id, EducationInstitution_Id, StartDate, EndDate, Degree_Id, DegreeObtained,DateDegreeAwarded,US_Equivalency_Id,City,State_or_province) VALUES  (" + Evaluation_Request_Id + "," + Country_Id + "," + Major_Id + "," + EducationInstitution_Id + ",'" + StartDate + "','" + EndDate + "'," + Degree_Id + "," + DegreeObtained + ",'" + DateDegreeAwarded + "'," + US_Equivalency_Id + ",'" + City.Replace("'", "''") + "','" + State.Replace("'", "''") + "')";
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static bool create_Service(int Evaluation_Request_Id, int Service_Id)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_services (Service_Id, Evaluation_Request_Id) VALUES (" + Service_Id + "," + Evaluation_Request_Id + ")";
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static bool create_Evaluation_Delivery(int Delivery_Type_Id, int Evaluation_Request_Id, string Name, string Address1, string Address2, string City, string State_or_Province, string Zip_or_PostalCode, int Country_Id, int Count, string type_copy, string CopyNo,string opional)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name, Addressline1,Addressline2, City, State_or_Province,Zip_or_PostalCode, Country_Id, Count,Type,CopyNo,Optional_InstitutionName) VALUES (" + Delivery_Type_Id + "," + Evaluation_Request_Id + ",'" + Name.Replace("'", "''") + "','" + Address1.Replace("'", "''") + "','" + Address2.Replace("'", "''") + "','" + City.Replace("'", "''") + "','" + State_or_Province.Replace("'", "''") + "','" + Zip_or_PostalCode.Replace("'", "''") + "'," + Country_Id + "," + Count + ",'" + type_copy.Replace("'", "''") + "','" + CopyNo.Replace("'", "''") + "','" + opional.Replace("'", "''") + "')";
            bool result = GetDataSet_withoutID(query);
            return result;



        }
        public static bool create_Evaluation_Delivery(int Delivery_Type_Id, int Evaluation_Request_Id, string FirstName, string Faxno, int Country_Id, int Count, string type_copy,string Customer_id)
        {
            bool result = false;
            string strquery = "SELECT * FROM [cc_clientdataset].[delivery_type] where Type='Fax' and Customer_Id="+Customer_id;
            DataSet ds = GetDataSet(strquery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name,Faxno,Country_Id, Count,Type,CopyNo) VALUES (" + ds.Tables[0].Rows[0]["id"].ToString() + "," + Evaluation_Request_Id + ",'" + FirstName.Replace("'", "''") + "','" + Faxno.Replace("'", "''") + "'," + Country_Id + "," + Count + ",'" + type_copy.Replace("'", "''") + "','Fax')";
                result = GetDataSet_withoutID(query);
            }
            return result;

        }
        public static bool create_Evaluation_Delivery(int Evaluation_Request_Id, string FirstName, string Email, int Country_Id, int Count,string Customer_id,string mode)
        {
            bool result = false;
            string strquery = "SELECT * FROM [cc_clientdataset].[delivery_type] where Type='Email' and Customer_Id=" + Customer_id;
            DataSet ds = GetDataSet(strquery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name,Email,Country_Id, Count,Type,CopyNo) VALUES (" + ds.Tables[0].Rows[0]["id"].ToString() + "," + Evaluation_Request_Id + ",'" + FirstName.Replace("'", "''") + "','" + Email.Replace("'", "''") + "'," + Country_Id + "," + Count + ",'" + mode.Replace("'", "''") + "','" + mode.Replace("'", "''") + "')";
                result = GetDataSet_withoutID(query);
            }
            return result;

        }


        //update records
        public static bool update_education(int Evaluation_Request_Id, int Country_Id, int Major_Id, int EducationInstitution_Id, string StartDate, string EndDate, int Degree_Id, int DegreeObtained, string DateDegreeAwarded, int US_Equivalency_Id, string City, string State, int id)
        {
            string query = "UPDATE cc_clientdataset.applicant_education_history SET Evaluation_Request_Id =" + Evaluation_Request_Id + ", Country_Id = " + Country_Id + ", Major_Id =" + Major_Id + ", EducationInstitution_Id =" + EducationInstitution_Id + ", StartDate ='" + StartDate + "', EndDate ='" + EndDate + "', Degree_Id =" + Degree_Id + ", DegreeObtained =" + DegreeObtained + ",DateDegreeAwarded ='" + DateDegreeAwarded + "', US_Equivalency_Id =" + US_Equivalency_Id + ", City ='" + City.Replace("'", "''") + "', State_or_province ='" + State.Replace("'", "''") + "' where(id=" + id + ")";
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static void update_service1grid(GridView commongrid, int Service_id)
        {
            string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.id=" + Service_id + ") ORDER BY cc_clientdataset.evaluation_services.id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool update_Applicante(string FirstName, string MiddleName, string LastName, string Gender, string DateOfBirth, string Addressline1, string Addressline2, string City, int CountryId, string State_or_province, string Zip_or_PostalCode, string HomePhone, string WorkPhone, string MobilePhone, string Email, int Customer_Id, string otherFirstName, string otherMiddleName, string otherLastName, string PreviousCredential_id, int Countryofbirth, int Join_cc_db, int id)
        {

            string query = "UPDATE cc_clientdataset.applicant SET FirstName = '" + FirstName.Replace("'", "''") + "', MiddleName ='" + MiddleName.Replace("'", "''") + "', LastName ='" + LastName.Replace("'", "''") + "', Gender ='" + Gender.Replace("'", "''") + "', otherFirstName ='" + otherFirstName.Replace("'", "''") + "', otherMiddleName ='" + otherMiddleName.Replace("'", "''") + "', otherLastName ='" + otherLastName.Replace("'", "''") + "', DateOfBirth ='" + DateOfBirth + "', Addressline1 = '" + Addressline1.Replace("'", "''") + "',Addressline2 = '" + Addressline2.Replace("'", "''") + "', City ='" + City.Replace("'", "''") + "', Countryofbirth ='" + Countryofbirth + "', CountryId =" + CountryId + ", State_or_province ='" + State_or_province.Replace("'", "''") + "', Zip_or_PostalCode = '" + Zip_or_PostalCode.Replace("'", "''") + "', HomePhone = '" + HomePhone.Replace("'", "''") + "', WorkPhone = '" + WorkPhone.Replace("'", "''") + "', MobilePhone = '" + MobilePhone.Replace("'", "''") + "', Email ='" + Email.Replace("'", "''") + "', Customer_Id =" + Customer_Id + ", PreviousCredential_id ='" + PreviousCredential_id.Replace("'", "''") + "', Join_cc_db =" + Join_cc_db + " Where id=" + id;
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static bool Release_ApplicantInfo(int nPublish, int nID)
        {

            string query = "UPDATE cc_clientdataset.applicant SET  Publish_Info =" + nPublish.ToString() + " Where id=" + nID.ToString();
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static bool update_purpose(int Applicant_Id, int Purpose_Id, string Eval_institution, string Eval_organization, string Eval_Attorney, string Eval_Board, string Eval_State, string Eval_Military_Recruiter, string Eval_other, int request_Id)
        {
            string query = "UPDATE cc_clientdataset.evaluation_request SET Applicant_Id =" + Applicant_Id + ", Purpose_Id =" + Purpose_Id + ", Eval_institution ='" + Eval_institution.Replace("'", "''") + "', Eval_organization = '" + Eval_organization.Replace("'", "''") + "', Eval_Attorney = '" + Eval_Attorney.Replace("'", "''") + "', Eval_Board ='" + Eval_Board.Replace("'", "''") + "', Eval_State ='" + Eval_State.Replace("'", "''") + "', Eval_Military_Recruiter = '" + Eval_Military_Recruiter.Replace("'", "''") + "', Eval_other = '" + Eval_other.Replace("'", "''") + "', Application_Recieved = 0, Documents_Recieved = 0, Payment_Recieved = 0, Evaluation_Complete = 0, Verification_Complete = 0, Evaluation_Approved = 0, Packaging_Complete = 0, Delievery_Complete = 0 where id=" + request_Id;
            bool result = GetDataSet_withoutID(query);
            return result;
        }
#endregion

        #region Clear values
        public static void clear_evaluation_services(int id)
        {
            string query = "DELETE FROM cc_clientdataset.evaluation_services WHERE Evaluation_Request_Id =" + id;
            GetDataSet_withoutID(query);
        }     
        #endregion


        public static string GetCopyHeader(string copyno,string clientId)
        {
            string header = "";
            string strSQL = "SELECT * FROM [cc_masterdataset].[copyHeaders] where [Customer_Id]='"+ clientId +"' AND [Copyno]='"+copyno+"'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                header = "<b>" + ds.Tables[0].Rows[0]["Header"].ToString() + "</b>";
            }
            else
            {
                header = "<b>Official Hard Copy Delivery-" + copyno + ":</b>";
            }

            return header;
        }

        //---------------------------------------------------- Display Fax & Additionalcopy Cost-------------------------------------------------------

        #region display cost
        public static void GetFaxcost(Label fax, int Customer_id)
        {
            string strSQL = "SELECT Cost FROM cc_clientdataset.delivery_type where (Customer_Id=" + Customer_id + ") AND (Type = 'Fax')";
            DataSet ds = GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                fax.Text = ds.Tables[0].Rows[0]["Cost"].ToString();
            }
            else
            {
                fax.Text = "0";
            }

        }
        public static void GetAdditionalcost(Label additional, int Customer_id)
        {
            string query = "SELECT Cost FROM cc_clientdataset.service WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Additional Copy'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                additional.Text = ds.Tables[0].Rows[0]["Cost"].ToString();
            }

        }
        public static void GetEmailcost(Label email, int Customer_id)
        {
            string strSQL = "SELECT Cost FROM cc_clientdataset.delivery_type where (Customer_Id=" + Customer_id + ") AND (Type = 'Email')";
            DataSet ds = GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                email.Text = ds.Tables[0].Rows[0]["Cost"].ToString();
            }
            else
            {
                email.Text = "0";
            }

        }
   
        #endregion

        #region Encode & Decode transaction key
        // encode
        public static string base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];

                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

                string encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //decode
        public static string base64Decode(string sData)
        {

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(sData);

            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

            char[] decoded_char = new char[charCount];

            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

            string result = new String(decoded_char);

            return result;

        }
        #endregion


        #region Instruction
        //Landing page text
        public static void GetLandingtext(Uri url,HtmlTableRow tr,HtmlTableCell tc)
            {
            //string host = url.Host;
            //if (host.Split('.').Length > 1)
            //{
            //    int Customer_id = 0;
            //    int index = host.IndexOf(".");
            //    string SubDomainName = host.Substring(0, index);

                int Customer_id = 0;
            string SubDomainName = "ravtronix"; 
                string query = "Select id from cc_masterdataset.customer where SubDomainName='" + SubDomainName + "'";
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

                if (Customer_id != 0)
                {
                    query = "SELECT [LandingText]  FROM [cc_masterdataset].[customersettings] where Customer_Id="+ Customer_id;
                    DataSet ds = GetDataSet(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LandingText"].ToString() != "")
                        {
                            tr.Visible = true;
                            tc.InnerHtml = ds.Tables[0].Rows[0]["LandingText"].ToString();
                        }
                        else
                        {
                            tr.Visible = false;
                    }
                }
            }
            //}

        }
      
        public static DataSet delivery_copy(int customer_Id)
        {
            string strSQL = "SELECT Delivery_copy, Delivery_Instructions,Education_Instructions,Document_Instructions FROM cc_masterdataset.customersettings WHERE (Customer_Id =" + customer_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet serviceDescription(int Id)
        {
            string strSQL = "SELECT id,Description FROM cc_clientdataset.service WHERE (id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet mailDescription(int Id)
        {
            string strSQL = "SELECT id,Description FROM cc_clientdataset.delivery_type WHERE (id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet toc(int Id)
        {
            string strSQL = "SELECT id,Terms_And_Condition FROM cc_masterdataset.customersettings WHERE (Customer_Id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        // check creditcard active or not
        public static bool check_creditoption(string customerId)
        {
            bool result = false;
            string query = "select CreditCard FROM cc_masterdataset.customersettings WHERE Customer_Id =" + customerId;
            DataSet ds = GetDataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string temp = ds.Tables[0].Rows[0]["CreditCard"].ToString();
                if (ds.Tables[0].Rows[0]["CreditCard"].ToString() == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;

        }
        public static string check_cardType(string customerId)
        {
            string card = "";
            string query = "select Credit_Type FROM cc_masterdataset.customersettings WHERE Customer_Id =" + customerId;
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                card = ds.Tables[0].Rows[0]["Credit_Type"].ToString();
            }
            return card;
        }

        #endregion

        #region Print Application
        public static DataSet check_print(string filenumber, string customerId)
        {
            string strSQL = "SELECT cc_clientdataset.evaluation_request.id,cc_clientdataset.applicant.id as applicantid FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + filenumber + "') AND (cc_clientdataset.applicant.Customer_Id =" + customerId + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;

        }
        #endregion

        #region Application ID Generator
        public static void create_Applicantid(int id)
        {
            string FileNumber = create_Randomid();
            string query = "UPDATE cc_clientdataset.applicant SET FileNumber='" + FileNumber + "' WHERE id=" + id;
            GetDataSet_withoutID(query);
        }
        public static void get_Applicantid(Label option, int id)
        {
            string query = "Select FileNumber from cc_clientdataset.applicant WHERE id=" + id;
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                option.Text = ds.Tables[0].Rows[0]["FileNumber"].ToString();
            }

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

       

        #endregion

        #region CreditCard
        //payment gateway
        public static DataSet paymentcode(string Id)
        {
            string query = "select LoginId,Transkey,Email from cc_masterdataset.customersettings where (Customer_Id='" + Id + "')";
            DataSet ds = GetDataSet(query);
            return ds;
        }
        //save payment status
        public static bool update_Coststatus(string fileno, string custid, string autho, string trans,string cardtype)
        {

            string query = "UPDATE cc_clientdataset.applicant SET Paymentstatus =1,Authorizecode='" + autho + "',Transactioncode='" + trans + "',Paymentmode='credit card',Card_Type='"+cardtype +"'  Where FileNumber='" + fileno + "' AND Customer_Id=" + custid;
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        //get Payment Amount
        public static void get_Cost(TextBox Amt,DropDownList lst,string fileno, string id)
        {           
            string query = "select Amount from cc_clientdataset.applicant where ((FileNumber='" + fileno + "') AND (Customer_Id ='" + id + "'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
               Amt.Text = String.Format("{0:.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"].ToString()));
                         
            }
            query = "select Credit_Type from cc_masterdataset.customersettings where (Customer_Id ='" + id + "')";
            DataSet ds1 = GetDataSet(query);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Getdropdownloader(ds1.Tables[0].Rows[0]["Credit_Type"].ToString(), lst);  
            }
        }

        #endregion

        #region Grid Load
        public static void Grid_Applicant(DetailsView commongrid, int Applicant_Id)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FirstName,cc_clientdataset.applicant.MiddleName,cc_clientdataset.applicant.LastName,cc_clientdataset.applicant.FirstName +' ' +cc_clientdataset.applicant.MiddleName+' '+cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth,cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City,cc_masterdataset.countries.Name AS CountryBirth, countries_1.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender,cc_clientdataset.applicant.Card_Type FROM (cc_clientdataset.applicant INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.Countryofbirth = cc_masterdataset.countries.ID LEFT JOIN cc_masterdataset.countries countries_1 ON cc_clientdataset.applicant.CountryId = countries_1.ID) WHERE (cc_clientdataset.applicant.id =" + Applicant_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_payment(int Applicant_Id, Label mode, Label cardtype, HtmlTableRow Tyblk)
        {
            string strSQL = "SELECT * from cc_clientdataset.applicant WHERE (cc_clientdataset.applicant.id =" + Applicant_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                mode.Text = ds.Tables[0].Rows[0]["Paymentmode"].ToString();
                if (mode.Text == "credit card")
                {
                    cardtype.Text = ds.Tables[0].Rows[0]["Card_Type"].ToString();
                    Tyblk.Visible = true;
                }
                else
                {
                    Tyblk.Visible = false; 
                }
            }

        }
        public static void Grid_hischoolgrid(GridView commongrid, int Request_Id)
        {           
            string strSQL = "SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_univgrid(GridView commongrid, int Request_Id)
        {
            string strSQL = " SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_purposegrid(DetailsView commongrid, int Applicant_Id)
        {
            string strSQL = " SELECT cc_clientdataset.evaluation_purpose.Evaluation_Name, cc_clientdataset.service.Name,cc_clientdataset.evaluation_request.Eval_institution FROM (((cc_clientdataset.evaluation_request INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id) INNER JOIN cc_clientdataset.evaluation_services ON cc_clientdataset.evaluation_request.id = cc_clientdataset.evaluation_services.Evaluation_Request_Id) INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE (cc_clientdataset.evaluation_request.Applicant_Id =" + Applicant_Id + ") AND (cc_clientdataset.service.Type = 'Evaluation')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void deliveryinfo(GridView commongrid, int request_Id)
        {
            string strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count)as Count,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1 , cc_clientdataset.evaluation_delivery.Addressline2, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =  " + request_Id + ") AND (Type <> 'Fax')) GROUP BY cc_masterdataset.countries.Name,cc_clientdataset.evaluation_delivery.Type,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_service1grid(GridView commongrid, int Request_Id)
        {
            string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") ORDER BY cc_clientdataset.evaluation_services.id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_service1gridprint(GridView commongrid, int Request_Id)
        {
            //string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") ORDER BY cc_clientdataset.evaluation_services.id";
            string strSQL = " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation')) " +
                            " union " +
                            " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional')) " +
                            " union " +
                            " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
                             " union " +
                            " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +                          
                            "order by cost desc";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_DeliveryGrid(GridView commongrid, int Request_Id)
        {
            //string strSQL = "SELECT SUM(cc_clientdataset.evaluation_delivery.Count) AS Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') GROUP BY cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type HAVING ((COUNT(cc_clientdataset.evaluation_delivery.Name) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.City) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Zip_or_PostalCode) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0))";          
            //string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional')";
            string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') AND (cc_clientdataset.evaluation_delivery.id not in(" +
                "SELECT ed2.id  FROM [cc_clientdataset].[evaluation_delivery] as ed1,[cc_clientdataset].[evaluation_delivery] as ed2 " +
                "where ed1.Addressline1= ed2.Addressline1 and ed1.Addressline2=ed2.Addressline2 and ed1.City=ed2.City and ed1.State_or_Province=ed2.State_or_Province and " +
                "ed1.Zip_or_PostalCode=ed2.Zip_or_PostalCode and ed1.Evaluation_Request_Id=ed2.Evaluation_Request_Id and ed1.Country_Id = ed2.Country_Id and ed1.CopyNo='primary' and ed2.CopyNo ='Additional' and ed1.Evaluation_Request_Id=" + Request_Id + "))";

            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_copycharger(GridView commongrid, int Request_Id, string category_type)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_faxgrid(GridView commongrid, int Request_Id)
        {           
            string strSQL = "SELECT cast(count(*)as char)as Count, Name,Faxno,Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Fax')) GROUP BY Type, cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Faxno HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Faxno) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_emailgrid(GridView commongrid, int Request_Id)
        {
            string strSQL = "SELECT cast(count(*)as char)as Count, Name,Email,Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Email')) GROUP BY Type, cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Email HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Email) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }

        #endregion

        #region Payment Tab
        //General service       
        public static void Grid_paymentgrid(GridView commongrid, int Request_Id)
        {
            //string strSQL = "SELECT cc_clientdataset.service.Name, cc_clientdataset.service.Cost AS Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ")";
            //string strSQL = "SELECT cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost order by Cost desc ";
            //string strSQL = " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation')) " +
            //                " union " +
            //                " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional')) " +
            //                " union " +
            //                " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
            //                "order by cost desc";
            string strSQL = " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation')) " +
                            " union " +
                            " SELECT  cc_clientdataset.service.Name,1 as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,cc_clientdataset.service.Cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional')) " +
                            " union " +
                            " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Additional Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
                             " union " +
                         " SELECT   cc_clientdataset.service.Name,COUNT(*) as Qty,cc_clientdataset.service.Cost as price,cc_clientdataset.service.Description,Sum(cc_clientdataset.service.Cost) as cost FROM (cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id) WHERE ((cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.service.Type ='Evaluation Multiplier')) group by cc_clientdataset.service.Name,cc_clientdataset.service.Cost,cc_clientdataset.service.Description " +
                            "order by cost desc";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        //Additional service       
        public static void Grid_addcopygrid(GridView commongrid, int Request_Id)
        {
            //Additional copy
            string strSQL = "SELECT Count, Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Additional'))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count) as Count,cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery where ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Additional'))GROUP BY cc_clientdataset.evaluation_delivery.Type HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Type) > 0 ))";
                DataSet ds1 = GetDataSet(strSQL);
                commongrid.DataSource = ds1;
            }
            else
            {
                commongrid.DataSource = ds;
            }

            commongrid.DataBind();
        }
        //Email service    
        public static void Grid_emailcopygrid(GridView commongrid, int Request_Id)
        {
            //Additional copy
            string strSQL = "SELECT Count, Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Email'))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count) as Count,cc_clientdataset.evaluation_delivery.Type,cc_clientdataset.evaluation_delivery.id FROM cc_clientdataset.evaluation_delivery where ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Email')) GROUP BY cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Email,cc_clientdataset.evaluation_delivery.Type, cc_clientdataset.evaluation_delivery.id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Email) > 0 ))";
                DataSet ds1 = GetDataSet(strSQL);
                commongrid.DataSource = ds1;
            }
            else
            {
                commongrid.DataSource = ds;
            }

            commongrid.DataBind();
        }
        //fax service    
        public static void Grid_faxcopygrid(GridView commongrid, int Request_Id)
        {
            //Additional copy
            string strSQL = "SELECT Count, Type FROM cc_clientdataset.evaluation_delivery WHERE ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Fax'))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count) as Count,cc_clientdataset.evaluation_delivery.Type,cc_clientdataset.evaluation_delivery.id FROM cc_clientdataset.evaluation_delivery where ((Evaluation_Request_Id =" + Request_Id + ") AND (Type = 'Fax')) GROUP BY cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Faxno,cc_clientdataset.evaluation_delivery.Type, cc_clientdataset.evaluation_delivery.id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Faxno) > 0 ))";
                DataSet ds1 = GetDataSet(strSQL);
                commongrid.DataSource = ds1;
            }
            else
            {
                commongrid.DataSource = ds;
            }

            commongrid.DataBind();
        }
        //save cost
        public static bool update_Cost(string mode, string amount, string id)
        {

            string query = "UPDATE cc_clientdataset.applicant SET Paymentmode ='" + mode + "', Amount ='" + amount + "' Where id=" + id;
            bool result = GetDataSet_withoutID(query);
            return result;
        }

        #endregion

        #region Delivery Instruction Tab
        public static int getAdditional(int Customer_id)
        {
            int cost = 0;
            string query = "SELECT Cost FROM cc_clientdataset.service WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Additional Copy'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            return cost;
        }
        public static int getFaxCost(int Customer_id)
        {
            int cost = 0;
            string query = "SELECT Cost FROM cc_clientdataset.delivery_type WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Fax'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            return cost;
        }
        public static int getEmailCost(int Customer_id)
        {
            int cost = 0;
            string query = "SELECT Cost FROM cc_clientdataset.delivery_type WHERE ((Customer_Id =" + Customer_id + ") AND (Type = 'Email'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
            }
            return cost;
        }
        public static void Check_copy(int Request_Id, string evalcount)
        {
            string strSQL = " SELECT Name,Addressline1, Addressline2, City, Country_Id, State_or_province, Zip_or_PostalCode FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='" + evalcount + "')";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "DELETE FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='" + evalcount + "')";
                GetDataSet_withoutID(query);
            }


        }
        public static void GetSameAddress(TextBox frm5_Fname, TextBox frm5_add1, TextBox frm5_add2, TextBox frm5_city, TextBox frm5_state, TextBox frm5_zip, DropDownList frm5_country, DropDownList frm5_deliverytype, int Request_Id)
        {
            string strSQL = " SELECT Name,Addressline1, Addressline2, City, Country_Id, State_or_province, Zip_or_PostalCode,Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frm5_Fname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                frm5_add1.Text = ds.Tables[0].Rows[0]["Addressline1"].ToString();
                frm5_add2.Text = ds.Tables[0].Rows[0]["Addressline2"].ToString();
                frm5_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                frm5_state.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                frm5_zip.Text = ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString();
                frm5_country.SelectedValue = ds.Tables[0].Rows[0]["Country_Id"].ToString();
                frm5_deliverytype.SelectedValue = ds.Tables[0].Rows[0]["Delivery_Type_Id"].ToString();
            }


        }
        public static void GetPrimaryAddress(TextBox frm5_pfname, TextBox frm5_padd1, TextBox frm5_padd2, TextBox frm5_pcity, TextBox frm5_pstate, TextBox frm5_pzip, DropDownList frm5_pcountry, int Request_Id)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FirstName, cc_clientdataset.applicant.LastName, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_clientdataset.applicant.CountryId, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Zip_or_PostalCode FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.evaluation_request.id =" + Request_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frm5_pfname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                frm5_padd1.Text = ds.Tables[0].Rows[0]["Addressline1"].ToString();
                frm5_padd2.Text = ds.Tables[0].Rows[0]["Addressline2"].ToString();
                frm5_pcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                frm5_pstate.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                frm5_pzip.Text = ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString();
                frm5_pcountry.SelectedValue = ds.Tables[0].Rows[0]["CountryId"].ToString();
            }


        }
        public static void displayprimary(DetailsView commongrid,int Request_Id)
        {
            string strSQL = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static int SaveSameAddress(GridView officialgrid, int Request_Id, int cust_id, string evalcount)
        {
            int result = 0;
            string strSQL = " SELECT Name,Addressline1, Addressline2, City, Country_Id, State_or_province, Zip_or_PostalCode,Delivery_Type_Id,Optional_InstitutionName FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation') AND (CopyNo='primary') ";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "SELECT id FROM cc_clientdataset.delivery_type where Name='Free copy' and Customer_Id=0";
                DataSet ds1 = GetDataSet(query);
                if (ds1.Tables[0].Rows.Count > 0)
                {                    
                    create_Evaluation_Delivery(Convert.ToInt32(ds1.Tables[0].Rows[0]["id"].ToString()), Request_Id, ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["Addressline1"].ToString(), ds.Tables[0].Rows[0]["Addressline2"].ToString(), ds.Tables[0].Rows[0]["City"].ToString(), ds.Tables[0].Rows[0]["State_or_province"].ToString(), ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[0]["Country_Id"].ToString()), 1, "Evaluation", evalcount.ToString(), ds.Tables[0].Rows[0]["Optional_InstitutionName"].ToString());
                }
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;

        }
        public static void Grid_Mailcost(GridView commongrid, int Customer_Id)
        {
            string strSQL = "SELECT id, Name ,Cost,priority FROM cc_clientdataset.delivery_type WHERE (Customer_Id = " + Customer_Id + ") AND (Type = 'Mail') order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_officialgrid(GridView commongrid, int Request_Id)
        {           
            string strSQL = "SELECT cast(count(*)as char)as Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.evaluation_delivery.Name AS Recipient FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation')) GROUP BY cc_clientdataset.delivery_type.Name,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Grid_addevalgrid(GridView commongrid, int Request_Id)
        {          
            string strSQL = "SELECT cast(count(*)as char)as sno,sum(Count)as Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.evaluation_delivery.Name AS Recipient FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Additional')) GROUP BY cc_clientdataset.delivery_type.Name,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        #endregion

        #region Service & fees Tab
        public static void Grid_servicegrid(GridView commongrid, int Customer_Id)
        {
            string strSQL = "SELECT id, Name, Cost, Description, Customer_Id,Type FROM cc_clientdataset.service WHERE (Customer_Id =" + Customer_Id + ") AND ((Type = 'Evaluation') OR (Type = 'Evaluation Multiplier')) Order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_addservicegrid(GridView commongrid, int Customer_Id)
        {
            string strSQL = "SELECT id, Name, Cost, Description, Customer_Id,Type FROM cc_clientdataset.service WHERE (Customer_Id =" + Customer_Id + ") AND ((Type = 'Additional') OR (Type = 'Additional Multiplier')) Order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
#endregion


        #region Review Tab
        public static void Grid_servicedp(DropDownList options, int Customer_Id)
        {
            string strSQL = "SELECT Name, id FROM cc_clientdataset.service WHERE (Type ='Evaluation') AND (Customer_Id =" + Customer_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Name";
            options.DataValueField = "id";
            options.DataBind();
        }
     
        public static void Applicantnote(string appid,string Note,string type)
        {
            string query = "select * from [cc_masterdataset].[applicantnotes] where Applicant_id=" + appid + " and Type='" + type + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                query = "UPDATE [cc_masterdataset].[applicantnotes] SET [Notes]='" + Note.Replace("'", "''") + "',[Timestamp]='"+ DateTime.Now +"' where Applicant_id=" + appid + " and Type='" + type + "'";
        
            }
            else
            {
                query = "INSERT INTO [cc_masterdataset].[applicantnotes]([Applicant_id],[Notes],[Type],[Timestamp])VALUES(" + appid + ",'" + Note.Replace("'", "''") + "','" + type + "','" + DateTime.Now + "')";
        
            }


              GetDataSet_withoutID(query);
        }
        public static void update_evaluation_services(int id, int Service_Id)
        {
            string query = "UPDATE cc_clientdataset.evaluation_services SET Service_Id =" + Service_Id + " where id=" + id;
            GetDataSet_withoutID(query);
        }
        public static void delete_Applicant_Education_History(int id)
        {
            string query = "DELETE FROM cc_clientdataset.applicant_education_history WHERE id =" + id;
            GetDataSet_withoutID(query);
        }
        public static void delete_Evaluation_Delivery(int id)
        {
            string query = "DELETE FROM cc_clientdataset.evaluation_delivery WHERE id =" + id;
            GetDataSet_withoutID(query);
        }
        public static void delete_evaluation_services(int id)
        {
            string query = "DELETE FROM cc_clientdataset.evaluation_services WHERE id =" + id;
            GetDataSet_withoutID(query);
        }

        public static void senderinfo(string appid,string Note,string type,string sendername,string contact,string institution)
        {
            Applicantnote(appid,Note, "Client");
            string query = "update [cc_clientdataset].[evaluation_request] set Senders_Name='" + sendername.Replace("'", "''") + "',Senders_Contact='"+ contact +"',Eval_institution='"+ institution.Replace("'", "''")  +"' WHERE Applicant_Id="+ appid;
            GetDataSet_withoutID(query);
        }

        #endregion

        #region Highschool tab
        public static void GetHighschooldata(DropDownList frma1_opt_country, DropDownList frma1_option_degree, TextBox frma1_city, TextBox frma1_state, DropDownList frma1_start_year, DropDownList frma1_end_year, DropDownList frma1_option_graduate, DropDownList frma1_month, DropDownList frma1_date, DropDownList frma1_year, TextBox frma1_institution, int Record_id)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();

            string strSQL = "SELECT cc_clientdataset.applicant_education_history.Country_Id,cc_clientdataset.applicant_education_history.City,cc_clientdataset.applicant_education_history.State_or_province,cc_clientdataset.applicant_education_history.Degree_Id, cc_masterdataset.institution.Name, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.DateDegreeAwarded, cc_clientdataset.applicant_education_history.DegreeObtained FROM (cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id) WHERE (cc_clientdataset.applicant_education_history.id =" + Record_id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frma1_institution.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                frma1_opt_country.SelectedValue = ds.Tables[0].Rows[0]["Country_Id"].ToString();
                frma1_option_degree.SelectedValue = ds.Tables[0].Rows[0]["Degree_Id"].ToString();
                frma1_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                frma1_state.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                frma1_start_year.SelectedValue = ds.Tables[0].Rows[0]["StartDate"].ToString();
                frma1_end_year.Items.Clear();  
                Getyear(frma1_end_year, Convert.ToInt32(ds.Tables[0].Rows[0]["StartDate"].ToString()), Convert.ToInt32(app.Endyear));
                frma1_end_year.SelectedValue = ds.Tables[0].Rows[0]["EndDate"].ToString();
                if (ds.Tables[0].Rows[0]["DegreeObtained"].ToString() == "1")
                {
                    frma1_option_graduate.SelectedValue = "True";
                }
                else
                {
                    frma1_option_graduate.SelectedValue = "False";
                }

                frma1_year.Items.Clear();
                Getyear(frma1_year, Convert.ToInt32(ds.Tables[0].Rows[0]["StartDate"].ToString()), Convert.ToInt32(app.Endyear));
                if (ds.Tables[0].Rows[0]["DateDegreeAwarded"].ToString() != "Null")
                {
                    string ndate = ds.Tables[0].Rows[0]["DateDegreeAwarded"].ToString();
                    string[] data = ndate.Split('/');
                    frma1_month.SelectedValue = data[1].ToString();
                    frma1_date.SelectedValue = data[0].ToString();
                    frma1_year.SelectedValue = data[2].ToString();

                }


            }
        }
        public static void Getdegree_update(DropDownList degree, int type, int Country_id, string customer)
        {
            string strSQL;
            if (type == 0)
            {

                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
            }
            else
            {
                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'University') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
            }
            GetEducationplan(degree, strSQL);
        }
        #endregion

        #region University Tab

        public static void GetUniversitydata(DropDownList frma2_opt_country, DropDownList frma2_option_degree, TextBox frma2_city, TextBox frma2_state, DropDownList frma2_start_year, DropDownList frma2_end_year, DropDownList frma2_option_graduate, DropDownList frma2_month, DropDownList frma2_date, DropDownList frma2_year, TextBox frma2_institution, DropDownList frma2_option_major, int Record_id)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            string strSQL = "SELECT cc_clientdataset.applicant_education_history.Country_Id,cc_clientdataset.applicant_education_history.Major_Id,cc_clientdataset.applicant_education_history.City,cc_clientdataset.applicant_education_history.State_or_province,cc_clientdataset.applicant_education_history.Degree_Id, cc_masterdataset.institution.Name, cc_clientdataset.applicant_education_history.StartDate,cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.DateDegreeAwarded, cc_clientdataset.applicant_education_history.DegreeObtained FROM (cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id) WHERE (cc_clientdataset.applicant_education_history.id =" + Record_id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frma2_institution.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                frma2_opt_country.SelectedValue = ds.Tables[0].Rows[0]["Country_Id"].ToString();
                frma2_option_degree.SelectedValue = ds.Tables[0].Rows[0]["Degree_Id"].ToString();
                frma2_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                frma2_state.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                frma2_start_year.SelectedValue = ds.Tables[0].Rows[0]["StartDate"].ToString();
                frma2_end_year.Items.Clear();
                Getyear(frma2_end_year, Convert.ToInt32(ds.Tables[0].Rows[0]["StartDate"].ToString()), Convert.ToInt32(app.Endyear));
                frma2_end_year.SelectedValue = ds.Tables[0].Rows[0]["EndDate"].ToString();
                if (ds.Tables[0].Rows[0]["DegreeObtained"].ToString() == "1")
                {
                    frma2_option_graduate.SelectedValue = "True";
                }
                else
                {
                    frma2_option_graduate.SelectedValue = "False";
                }             
                frma2_option_major.SelectedValue = ds.Tables[0].Rows[0]["Major_Id"].ToString();
                frma2_year.Items.Clear();
                Getyear(frma2_year, Convert.ToInt32(ds.Tables[0].Rows[0]["StartDate"].ToString()), Convert.ToInt32(app.Endyear));
           

                if (ds.Tables[0].Rows[0]["DateDegreeAwarded"].ToString() != "Null")
                {
                    string ndate = ds.Tables[0].Rows[0]["DateDegreeAwarded"].ToString();
                    string[] data = ndate.Split('/');
                    frma2_month.SelectedValue = data[1].ToString();
                    frma2_date.SelectedValue = data[0].ToString();
                    frma2_year.SelectedValue = data[2].ToString();

                }


            }
        }
        public static void Getmajor_update(DropDownList options, int Country_id, string customer)
        {
            string strSQL = "select Id,Name from cc_masterdataset.major where (id > 0) AND (Country_ID = " + Country_id + ") AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Name";
            options.DataValueField = "Id";
            options.Items.Add(new ListItem("Select", "0"));
            options.DataBind();

        }

        #endregion

              

   
    public static void Grid_addonsservice(GridView commongrid, int Request_Id)
        {

            string strSQL = " SELECT cc_clientdataset.evaluation_delivery.id, cc_clientdataset.delivery_type.Name, cc_clientdataset.evaluation_delivery.Type, cc_clientdataset.delivery_type.Cost, cc_clientdataset.evaluation_delivery.Count FROM (cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id) WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id ="+Request_Id+") order by cc_clientdataset.evaluation_delivery.id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();  
        }
    public static void thankublock(HtmlTableRow block,string id,HtmlAnchor btn)   
    {
        string strSQL = "SELECT * FROM cc_masterdataset.customersettings where Customer_Id=" + id;
        DataSet ds = GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ThkuPage"].ToString() == "1")
            {
                block.Visible = true;
            }
            if(ds.Tables[0].Rows[0]["SiteUrl"].ToString() != "")
            {
                btn.HRef ="http://" + ds.Tables[0].Rows[0]["SiteUrl"].ToString();
               
            }
        }
    }
     

    public static string AppidbyFilenember(string fileno)
    {
        string appid = "0";
        string query = "Select id from [cc_clientdataset].[applicant] where [FileNumber]='" + fileno + "'";
        DataSet ds = new DataSet();
        ds = GetDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            appid = ds.Tables[0].Rows[0]["id"].ToString();
        }
        return appid;
    }
   
     
    public static void Getyear_Payment(DropDownList year)
    {
        int start_year = DateTime.Now.Year;
        int end_year = DateTime.Now.Year + 10;
        for (int i = start_year; i <= end_year; i++)
        {
            //if (i == start_year)
            //{
            //    year.Items.Add(" ");
            //}
            //else
            //{
            DateTime expirationDate = new DateTime(i, 1, 31); // random date           
            year.Items.Add(new ListItem(i.ToString(), expirationDate.ToString("yy")));
            //}
        }
    }
     
      
     
     
    }
}