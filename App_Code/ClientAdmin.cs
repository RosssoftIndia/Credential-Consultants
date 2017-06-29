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



/// <summary>
/// Summary description for clientAdmin
/// </summary>
namespace ClientAdmin
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

        #region Menu
        #region Application

        #region Active
        public static string GetclientName(int customer_Id)
        {
            string clients = "Nodata";
            string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where id=" + customer_Id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clients = ds.Tables[0].Rows[0]["SubDomainName"].ToString();
            }
            return clients;
        }
     
        public static void GetSubclients(DropDownList Clients, int customer_Id,bool Isall)
        {            
            //string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where SubDomainName=" +
            //"(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") OR Parent=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") order by SubDomainName";
            string client = GetclientName(customer_Id);

            //string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where SubDomainName=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") OR Parent=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") ORDER by (case when SubDomainName = '" + client + "' then 0 else 1 end) asc, SubDomainName asc";
            string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where SubDomainName=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") OR Parent=(SELECT SubDomainName FROM cc_masterdataset.customer where id =" + customer_Id + ") ORDER by SubDomainName asc";
            

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
        public static string GetAllSubclients(int customer_Id)
        {
            string clients = "Nodata";
            string strSQL = "SELECT id,SubDomainName FROM cc_masterdataset.customer where id=" + customer_Id + " OR Parent=(SELECT SubDomainName FROM cc_masterdataset.customer where id=" + customer_Id + ") order by id";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
               string temp = "";
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    temp += ds.Tables[0].Rows[i]["id"].ToString();
                    temp += ",";
                }
              
                clients = temp.TrimEnd(',');
            }
            return clients;
        }
        public static void GetEmployees(DropDownList Clients, int customer_Id)
        {
            string strSQL = "SELECT * FROM [cc_relateddataset].[login] where Customer_Id=" + customer_Id + " and Role in('EMPLOYEE','CLIENT') order by Name";

            DataSet ds = GetDataSet(strSQL);
            Clients.DataSource = ds;
            Clients.DataTextField = "Name";
            Clients.DataValueField = "id";
            Clients.AppendDataBoundItems = true;
            Clients.Items.Add(new ListItem("Select", "0")); 
            Clients.DataBind();

        }
        public static void Search_GetEmployees(DropDownList Clients, int customer_Id)
        {
            string strSQL = "SELECT * FROM [cc_relateddataset].[login] where Customer_Id=" + customer_Id + " and Role in('EMPLOYEE','CLIENT') order by Name";

            DataSet ds = GetDataSet(strSQL);
            Clients.DataSource = ds;
            Clients.DataTextField = "Name";
            Clients.DataValueField = "id";
            Clients.AppendDataBoundItems = true;
            Clients.Items.Add(new ListItem("All", "0"));
            Clients.DataBind();

        }
        public static string Search_GetEmployeesbyId(string Id)
        {
            string result = "";
            string strSQL = "SELECT * FROM [cc_relateddataset].[login] where Id="+Id+" order by Name";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            return result;

        }
        public static bool Select_Employee(TextBox amount, DropDownList employeeID, string FileNumber)
        {
            bool result = false;
             string query = "SELECT id FROM cc_clientdataset.applicant WHERE FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int applicant_id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());

                string strSQL = "select Employee,convert(DOUBLE PRECISION, Amount) as Amount from [cc_clientdataset].[evaluation_request] where Applicant_Id=" + applicant_id;
                DataSet ds1 = GetDataSet(strSQL);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    amount.Text = ds1.Tables[0].Rows[0]["Amount"].ToString();
                    employeeID.SelectedValue = ds1.Tables[0].Rows[0]["Employee"].ToString();
                }
            }
            return result;
        }
        public static bool Update_Employee(string amount, string employeeID, string FileNumber)
        {
            bool result = false;
            string query = "SELECT id FROM cc_clientdataset.applicant WHERE FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int applicant_id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());

                string strSQL = "UPDATE [cc_clientdataset].[evaluation_request] SET [Amount]='" + amount + "' ,[Employee]='" + employeeID + "' where Applicant_Id=" + applicant_id;
                result = GetDataSet_withoutID(strSQL);

            }
            return result;
        }

        public static void Grid_ActiveApplicant(GridView commongrid, string customer_Id, string type, string data, string rd)
        {
            string strSQL = "", strSQL1 = "", strSQL2 = "", strSQL3 = "";
            strSQL1 = "SELECT cc_clientdataset.applicant.Customer_Id,convert(DOUBLE PRECISION, cc_clientdataset.evaluation_request.Amount) as Amount,cc_clientdataset.evaluation_request.Employee,cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_clientdataset.applicant INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.Countryofbirth = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
            switch (type)
            {
                case "ALL":
                    strSQL2 = "(not((cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Complete = 1) AND (cc_clientdataset.evaluation_request.Verification_Complete = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Approved = 1) AND (cc_clientdataset.evaluation_request.Packaging_Complete = 1) AND (cc_clientdataset.evaluation_request.Delievery_Complete = 1))AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0))";
               break;
                case "Step 1":
                    strSQL2 = " not((cc_clientdataset.evaluation_request.Application_Recieved =1) AND (cc_clientdataset.evaluation_request.Documents_Recieved =1) AND (cc_clientdataset.evaluation_request.Payment_Recieved =1)) AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0)";
               break;
                case "Step 2":
                    strSQL2 = " (not((cc_clientdataset.evaluation_request.Evaluation_Complete = 1)AND (cc_clientdataset.evaluation_request.Verification_Complete = 1)) AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0) AND (cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1))";
               break;
                case "Step 3":
                    strSQL2 = " ((cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Complete = 1) AND (cc_clientdataset.evaluation_request.Verification_Complete = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Approved = 0)) AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0)";
               break;
                case "Step 4":
                    strSQL2 = " (not((cc_clientdataset.evaluation_request.Packaging_Complete = 1) AND (cc_clientdataset.evaluation_request.Delievery_Complete = 1)) AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0) AND (cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Complete = 1) AND (cc_clientdataset.evaluation_request.Verification_Complete = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Approved = 1))";
               break;
                default:
                    strSQL2 = " (not((cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Complete = 1) AND (cc_clientdataset.evaluation_request.Verification_Complete = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Approved = 1) AND (cc_clientdataset.evaluation_request.Packaging_Complete = 1) AND (cc_clientdataset.evaluation_request.Delievery_Complete = 1))AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0))";
                   break;
            }             

            switch (rd)
            {
                case "Name":
                    strSQL3 = " AND ((cc_clientdataset.applicant.FirstName like '" + data + "%') OR (cc_clientdataset.applicant.LastName like '" + data + "%'))  order by cc_clientdataset.applicant.id desc ";
                    break;
                case "DOB (yyyy-mm-dd)":
                    strSQL3 = " AND (cc_clientdataset.applicant.DateOfBirth = '" + data + "') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Country (Country Of Birth)":
                    strSQL3 = " AND (cc_masterdataset.countries.Name like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Email":
                    strSQL3 = " AND (cc_clientdataset.applicant.Email like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Filenumber":
                    strSQL3 = " AND (cc_clientdataset.applicant.FileNumber like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Highschool":
                    strSQL1 = "SELECT cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
                    strSQL3 = " AND ((cc_masterdataset.institution.Name like '" + data + "%') AND (cc_masterdataset.institution.Type = 'HighSchool')) order by cc_clientdataset.applicant.id desc ";
                    break;
                case "University":
                    strSQL1 = "SELECT cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
                    strSQL3 = " AND ((cc_masterdataset.institution.Name like '" + data + "%') AND (cc_masterdataset.institution.Type = 'University')) order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Employee":
                    if (data != "All")
                    {
                        strSQL3 = " AND (cc_clientdataset.evaluation_request.Employee='" + data + "') order by cc_clientdataset.applicant.id desc ";
                    }
                    else
                    {
                        strSQL3 = " AND ((cc_clientdataset.applicant.FirstName like '%') OR (cc_clientdataset.applicant.LastName like '" + data + "%'))  order by cc_clientdataset.applicant.id desc ";               
                    }
                    break;

            }

            strSQL = strSQL1 + strSQL2 + strSQL3;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        #endregion

        #region Archive
        public static void Grid_ArchivesApplicant(GridView commongrid, string customer_Id, string data, string rd)
        {
            string strSQL = "", strSQL1 = "", strSQL2 = "", strSQL3 = "";
            strSQL1 = "SELECT cc_clientdataset.applicant.Customer_Id,convert(DOUBLE PRECISION, cc_clientdataset.evaluation_request.Amount) as Amount,cc_clientdataset.evaluation_request.Employee,cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber,cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_clientdataset.applicant INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.Countryofbirth = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
            strSQL2 = " (cc_clientdataset.evaluation_request.Application_Recieved = 1) AND (cc_clientdataset.evaluation_request.Documents_Recieved = 1) AND (cc_clientdataset.evaluation_request.Payment_Recieved = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Complete = 1) AND (cc_clientdataset.evaluation_request.Verification_Complete = 1) AND (cc_clientdataset.evaluation_request.Evaluation_Approved = 1) AND (cc_clientdataset.evaluation_request.Packaging_Complete = 1) AND (cc_clientdataset.evaluation_request.Delievery_Complete = 1) AND (cc_clientdataset.applicant.Customer_Id in (" + customer_Id + ")) AND (len(cc_clientdataset.applicant.FileNumber)<>0)"; 

            switch (rd)
            {
                case "Name":
                    strSQL3 = " AND ((cc_clientdataset.applicant.FirstName like '" + data + "%') OR (cc_clientdataset.applicant.LastName like '" + data + "%'))  order by cc_clientdataset.applicant.id desc ";
                    break;
                case "DOB (yyyy-mm-dd)":
                    strSQL3 = " AND (cc_clientdataset.applicant.DateOfBirth = '" + data + "') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Country (Country Of Birth)":
                    strSQL3 = " AND (cc_masterdataset.countries.Name like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Email":
                    strSQL3 = " AND (cc_clientdataset.applicant.Email like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Filenumber":
                    strSQL3 = " AND (cc_clientdataset.applicant.FileNumber like '" + data + "%') order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Highschool":
                    strSQL1 = "SELECT cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
                    strSQL3 = " AND ((cc_masterdataset.institution.Name like '" + data + "%') AND (cc_masterdataset.institution.Type = 'HighSchool')) order by cc_clientdataset.applicant.id desc ";
                    break;
                case "University":
                    strSQL1 = "SELECT cc_clientdataset.applicant.Createdon,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.InternalFileNumber, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.applicant.Zip_or_PostalCode, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Gender, cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id WHERE ";
                    strSQL3 = " AND ((cc_masterdataset.institution.Name like '" + data + "%') AND (cc_masterdataset.institution.Type = 'University')) order by cc_clientdataset.applicant.id desc ";
                    break;
                case "Employee":
                    if (data != "All")
                    {
                        strSQL3 = " AND (cc_clientdataset.evaluation_request.Employee='" + data + "') order by cc_clientdataset.applicant.id desc ";
                    }
                    else
                    {
                        strSQL3 = " AND ((cc_clientdataset.applicant.FirstName like '%') OR (cc_clientdataset.applicant.LastName like '" + data + "%'))  order by cc_clientdataset.applicant.id desc ";
                    }
                    break;

            }


            strSQL = strSQL1 + strSQL2 + strSQL3;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        #endregion

        #region Evaluate
        public static void DetailsView_EvaluateBrowse(string FileNumber, GridView commongrid)
        {
              string strSQL="";
             DataSet ds = new DataSet();
             strSQL = " SELECT cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Request_Id = ds.Tables[0].Rows[0]["id"].ToString();
                strSQL = " SELECT cc_clientdataset.applicant_education_history.Evaluation_Request_Id,cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Id as instituteId,cc_masterdataset.institution.Name,'0' as major, cc_masterdataset.degree.Id as eduid, cc_masterdataset.degree.Name AS Expr1,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage ,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')" +
             " union " + " SELECT cc_clientdataset.applicant_education_history.Evaluation_Request_Id,cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Id as instituteId,cc_masterdataset.institution.Name,cc_masterdataset.major.Name as major, cc_masterdataset.degree.Id  as eduid, cc_masterdataset.degree.Name AS Expr1,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_masterdataset.major  ON cc_clientdataset.applicant_education_history.Major_Id  = cc_masterdataset.major.Id WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University') order by cc_clientdataset.applicant_education_history.StartDate";
                ds = GetDataSet(strSQL);
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }
            //else
            //{
            //   // strSQL = " SELECT '' as Name,'' as eduid, '' as Expr1, '' as StartDate,'' as EndDate, '' as id, '' as Country, '' as DateDegreeAwarded ";
            //}
             
          
         
        }
        public static bool DetailsView_EvaluateAdd(string eduid,string equivalencyid,string gradeid,string name,string issued,string converted,string Recordid)
        {
             bool result = false;                 
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT * FROM cc_masterdataset.Linkage WHERE Education_Id=" + eduid + " AND Equivalency_Id=" + equivalencyid + " AND Gradescale_Id=" + gradeid;
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = ds.Tables[0].Rows[0]["Id"].ToString(); 
                strSQL = "UPDATE cc_clientdataset.applicant_education_history SET Linkage=" + id + " WHERE id=" + Recordid;
                result = GetDataSet_withoutID(strSQL);
            }
            else
            {
                strSQL = "INSERT INTO cc_masterdataset.Linkage(Education_Id,Equivalency_Id,Gradescale_Id,Linkage_name,Issued_GPA,Converted_GPA) VALUES(" + eduid + "," + equivalencyid + "," + gradeid + ",'" + name.Replace("'", "''") + "','" + issued.Replace("'", "''") + "','" + converted.Replace("'", "''") + "')";
               int id  = GetDataSet_withID(strSQL);
               strSQL = "UPDATE cc_clientdataset.applicant_education_history SET Linkage="+ id +" WHERE id=" + Recordid;
              result  = GetDataSet_withoutID(strSQL);
            
            }
            return result;

        }
        public static DataSet DetailsView_Linkageinfo(string linkId)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = " SELECT cc_masterdataset.gradescales.Name AS gradename, cc_masterdataset.gradescales.Description AS gradedes, cc_clientdataset.Equivalency.Name AS equiname,cc_clientdataset.Equivalency.Description AS equides FROM cc_masterdataset.Linkage left outer join " +
                  " cc_masterdataset.gradescales ON cc_masterdataset.Linkage.Gradescale_Id = cc_masterdataset.gradescales.ID left outer join " +
                  " cc_clientdataset.Equivalency ON cc_masterdataset.Linkage.Equivalency_Id = cc_clientdataset.Equivalency.ID " + "WHERE  (cc_masterdataset.Linkage.Id ="+ linkId +")";
            ds = GetDataSet(strSQL);
            return ds;
          
         }
        public static string DetailsView_Linkageselect(string linkId,string type)
        {
            string result="";
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT * FROM cc_masterdataset.Linkage WHERE Id=" + linkId;
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                switch (type)
                {
                    case "Equi":
                        result = ds.Tables[0].Rows[0]["Equivalency_Id"].ToString();
                        break;

                    case "grade":
                        result = ds.Tables[0].Rows[0]["Gradescale_Id"].ToString();
                        break;

                    case "Issued_GPA":
                        result = ds.Tables[0].Rows[0]["Issued_GPA"].ToString();
                        break;

                    case "Converted_GPA":
                        result = ds.Tables[0].Rows[0]["Converted_GPA"].ToString();
                        break;
                }
            }
            return result;

        }
        #endregion

        #region Edit status
        public static bool Update_ApplEdit(int Application_Recieved, int Documents_Recieved, int Payment_Recieved, int Evaluation_Complete, int Verification_Complete, int Evaluation_Approved, int Packaging_Complete, int Delievery_Complete, string FileNumber,string UpdatedBy)
        {
            bool result = false;
            string query = "SELECT id FROM cc_clientdataset.applicant WHERE FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strSQL = "";
                int applicant_id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                string qs = "SELECT * FROM cc_clientdataset.evaluation_request WHERE Applicant_Id="+ applicant_id;
                DataSet dscheck = GetDataSet(qs);
                if (
                    (dscheck.Tables[0].Rows[0]["Application_Recieved"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Documents_Recieved"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Payment_Recieved"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Evaluation_Complete"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Verification_Complete"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Evaluation_Approved"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Packaging_Complete"].ToString() == "0") &
                    (dscheck.Tables[0].Rows[0]["Delievery_Complete"].ToString() == "0")
                    )
                {
                    strSQL = "UPDATE cc_clientdataset.evaluation_request SET Application_Recieved =" + Application_Recieved + ", Documents_Recieved =" + Documents_Recieved + ", Payment_Recieved =" + Payment_Recieved + ",Evaluation_Complete =" + Evaluation_Complete + ", Verification_Complete =" + Verification_Complete + ", Evaluation_Approved =" + Evaluation_Approved + ",Packaging_Complete =" + Packaging_Complete + ", Delievery_Complete =" + Delievery_Complete + ",FirstUpdatedTime='" + DateTime.Now + "',LastUpdatedTime='" + DateTime.Now + "',UpdatedBy='" + UpdatedBy + "' WHERE Applicant_Id =" + applicant_id;
                }
                else
                {
                    strSQL = "UPDATE cc_clientdataset.evaluation_request SET Application_Recieved =" + Application_Recieved + ", Documents_Recieved =" + Documents_Recieved + ", Payment_Recieved =" + Payment_Recieved + ",Evaluation_Complete =" + Evaluation_Complete + ", Verification_Complete =" + Verification_Complete + ", Evaluation_Approved =" + Evaluation_Approved + ",Packaging_Complete =" + Packaging_Complete + ", Delievery_Complete =" + Delievery_Complete + ",LastUpdatedTime='" + DateTime.Now + "',UpdatedBy='" + UpdatedBy + "' WHERE Applicant_Id =" + applicant_id;
                }
  
              
                result = GetDataSet_withoutID(strSQL);

            }

            return result;

        }
        public static void DetailsView_ApplEdit(string FileNumber, DetailsView commongrid)
        {
            //string strSQL = "SELECT cc_clientdataset.evaluation_request.LastUpdatedTime as Timestamp,cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name FROM (((cc_clientdataset.applicant INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id) INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id) INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id) WHERE cc_clientdataset.applicant.FileNumber ='" + FileNumber + "'";
            string strSQL = "SELECT cc_clientdataset.evaluation_request.UpdatedBy,cc_clientdataset.evaluation_request.LastUpdatedTime as Timestamp,cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete, cc_clientdataset.evaluation_purpose.Evaluation_Name from cc_clientdataset.evaluation_request INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id where cc_clientdataset.evaluation_request.Applicant_Id =(select id from [cc_clientdataset].[applicant] where cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void updateArchieve(string FileNumber,string UpdatedBy)
        {
             string query = "SELECT id FROM cc_clientdataset.applicant WHERE FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int applicant_id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                string strSQL = "UPDATE cc_clientdataset.evaluation_request SET archived=1,UpdatedBy='"+UpdatedBy+"', LastUpdatedTime='" + DateTime.Now + "' WHERE Applicant_Id =" + applicant_id;
                GetDataSet_withoutID(strSQL);
            }
        }

        #region File notes
        public static bool Grid_internalNotesAdd(string Applicant_id , string Notes)
        {
            bool result = false;
            string clientid = clientidbyFilenumber(Applicant_id);
            if (clientid != "0")
            {
                string strSQL = "INSERT INTO cc_masterdataset.filenotes(Applicant_id, Customer_id, Notes,Timestamp) VALUES ('" + Applicant_id + "'," + clientid + ",'" + Notes.Replace("'", "''") + "','" + DateTime.Now + "')";
                result = GetDataSet_withoutID(strSQL);
            }
            return result;

        }
        public static void Grid_internalNotesBrowse(GridView commongrid, String Applicant_id)
        {
            string clientid = clientidbyFilenumber(Applicant_id);
            if (clientid != "0")
            {
                string strSQL = "SELECT id,Notes, Timestamp FROM cc_masterdataset.filenotes WHERE (Applicant_id ='" + Applicant_id + "') AND (Customer_id=" + clientid + ") ORDER BY id desc";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        }
        public static bool Grid_applicantNotesAdd(string appid, string Note, string type)
        {
            bool result = false;
            string applicationid = AppidbyFilenember(appid);
            if (applicationid != "0")
        {
                string strSQL = "INSERT INTO [cc_masterdataset].[applicantnotes]([Applicant_id],[Notes],[Type],[Timestamp])VALUES(" + applicationid + ",'" + Note.Replace("'", "''") + "','" + type.Replace("'", "''") + "','" + DateTime.Now + "')";
                result = GetDataSet_withoutID(strSQL);
            }
            return result;

        }
        public static void Grid_applicantNotesBrowse(GridView commongrid, string Applicant_id,string type)
        {

            string strSQL = "SELECT * from cc_masterdataset.applicantnotes where Applicant_id = (SELECT [id] FROM [cc_clientdataset].[applicant] where FileNumber='" + Applicant_id + "') AND Type='" + type + "' ORDER BY id desc";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_applicantNotesdel(string record)
        {
            string strSQL = "DELETE FROM cc_masterdataset.applicantnotes WHERE id=" + record;
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_internalNotesdel(string  record)
        {
            string strSQL = "DELETE FROM cc_masterdataset.filenotes WHERE id=" + record;
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }


        #endregion

        #region internalfilename
        public static void Grid_internalSelect(DetailsView commongrid, string Filenumber)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name,cc_clientdataset.applicant.InternalFileNumber FROM cc_clientdataset.applicant where FileNumber='" + Filenumber + "'";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_internalUpdate(string inf, string filenumber)
        {
            string strSQL = "UPDATE cc_clientdataset.applicant SET InternalFileNumber ='" + inf + "' WHERE FileNumber='" + filenumber + "'";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }

        #endregion

        #region applicantinfo
        public static void Get_applicantinfo(Label fileno,Label name,Label company,string Filenumber)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name,cc_clientdataset.applicant.InternalFileNumber,cc_clientdataset.applicant.Customer_Id FROM cc_clientdataset.applicant where FileNumber='" + Filenumber + "'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fileno.Text = ds.Tables[0].Rows[0]["FileNumber"].ToString();
                name.Text = ds.Tables[0].Rows[0]["Name"].ToString();

                strSQL = "SELECT [Name] FROM [cc_masterdataset].[customer] where id=" + ds.Tables[0].Rows[0]["Customer_Id"].ToString();
               ds = GetDataSet(strSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    company.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
        }

        }
          #endregion
        #endregion

        #region Edit application

        #region common
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
            string strSQL = "SELECT * FROM cc_masterdataset.countries";
            DataSet ds = GetDataSet(strSQL);
            country.DataSource = ds;
            country.DataTextField = "Name";
            country.DataValueField = "Id";
            country.Items.Add(new ListItem("Select", "0"));
            country.DataBind();

        }
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


        #region personalinfo
        public static DataSet GetAppinfo(string trackingcode)
        {
            string strSQL = "SELECT [id],[Applicant_Id],[Purpose_Id] FROM [cc_clientdataset].[evaluation_request] where Applicant_Id = (SELECT id  FROM [cc_clientdataset].[applicant] where FileNumber='" + trackingcode + "')";
            DataSet ds = GetDataSet(strSQL);
            return ds;

        }
        public static DataSet Edit_application(string ApplicationId)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.applicant where cc_clientdataset.applicant.id=" + ApplicationId;
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static bool update_Applicante(string FirstName, string MiddleName, string LastName, string Gender, string DateOfBirth, string Addressline1, string Addressline2, string City, int CountryId, string State_or_province, string Zip_or_PostalCode, string HomePhone, string WorkPhone, string MobilePhone, string Email, int Customer_Id, string otherFirstName, string otherMiddleName, string otherLastName, string PreviousCredential_id, int Countryofbirth, int Join_cc_db, string trackingid)
        {
            bool result = false;
            string query = "Select id from cc_clientdataset.applicant where FileNumber='" + trackingid + "'";
            DataSet ds = GetDataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                query = "UPDATE cc_clientdataset.applicant SET FirstName = '" + FirstName.Replace("'", "''") + "', MiddleName ='" + MiddleName.Replace("'", "''") + "', LastName ='" + LastName.Replace("'", "''") + "', Gender ='" + Gender.Replace("'", "''") + "', otherFirstName ='" + otherFirstName.Replace("'", "''") + "', otherMiddleName ='" + otherMiddleName.Replace("'", "''") + "', otherLastName ='" + otherLastName.Replace("'", "''") + "', DateOfBirth ='" + DateOfBirth + "', Addressline1 = '" + Addressline1.Replace("'", "''") + "',Addressline2 = '" + Addressline2.Replace("'", "''") + "', City ='" + City.Replace("'", "''") + "', Countryofbirth ='" + Countryofbirth + "', CountryId =" + CountryId + ", State_or_province ='" + State_or_province.Replace("'", "''") + "', Zip_or_PostalCode = '" + Zip_or_PostalCode.Replace("'", "''") + "', HomePhone = '" + HomePhone.Replace("'", "''") + "', WorkPhone = '" + WorkPhone.Replace("'", "''") + "', MobilePhone = '" + MobilePhone.Replace("'", "''") + "', Email ='" + Email.Replace("'", "''") + "', PreviousCredential_id ='" + PreviousCredential_id + "' Where id=" + ds.Tables[0].Rows[0]["id"].ToString();
                result = GetDataSet_withoutID(query);
            }

            return result;
        }
        #endregion

        #region purpose
        public static void Getpurpose(DropDownList options)
        {
            string strSQL = "select id,Evaluation_Name from cc_clientdataset.evaluation_purpose order by priority";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Evaluation_Name";
            options.DataValueField = "id";
            options.DataBind();

        }
        public static DataSet Purpose_application(string applicationId)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.evaluation_request where Applicant_Id=" + applicationId;
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static bool update_purpose(int Applicant_Id, int Purpose_Id, string Eval_institution, string Eval_organization, string Eval_Attorney, string Eval_Board, string Eval_State, string Eval_Military_Recruiter, string Eval_other, int request_Id)
        {
            string query = "UPDATE cc_clientdataset.evaluation_request SET Applicant_Id =" + Applicant_Id + ", Purpose_Id =" + Purpose_Id + ", Eval_institution ='" + Eval_institution.Replace("'", "''") + "', Eval_organization = '" + Eval_organization.Replace("'", "''") + "', Eval_Attorney = '" + Eval_Attorney.Replace("'", "''") + "', Eval_Board ='" + Eval_Board.Replace("'", "''") + "', Eval_State ='" + Eval_State.Replace("'", "''") + "', Eval_Military_Recruiter = '" + Eval_Military_Recruiter.Replace("'", "''") + "', Eval_other = '" + Eval_other.Replace("'", "''") + "', Application_Recieved = 0, Documents_Recieved = 0, Payment_Recieved = 0, Evaluation_Complete = 0, Verification_Complete = 0, Evaluation_Approved = 0, Packaging_Complete = 0, Delievery_Complete = 0 where id=" + request_Id;
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static string GetpurposeName(string PurposeId)
        {
            string Purpose="";
            string strSQL = "select Evaluation_Name from cc_clientdataset.evaluation_purpose Where id="+PurposeId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Purpose = ds.Tables[0].Rows[0]["Evaluation_Name"].ToString();
            }
            else { Purpose = ""; } 
            return Purpose; 
        }
        #endregion
       
        #region education
        public static void Grid_hischoolgrid(GridView commongrid, string Request_Id)
        {
            string strSQL = "SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool update_education(int Evaluation_Request_Id, int Country_Id, int Major_Id, int EducationInstitution_Id, string StartDate, string EndDate, int Degree_Id, int DegreeObtained, string DateDegreeAwarded, int US_Equivalency_Id, string City, string State, int id)
        {
            string query = "UPDATE cc_clientdataset.applicant_education_history SET Evaluation_Request_Id =" + Evaluation_Request_Id + ", Country_Id = " + Country_Id + ", Major_Id =" + Major_Id + ", EducationInstitution_Id =" + EducationInstitution_Id + ", StartDate ='" + StartDate + "', EndDate ='" + EndDate + "', Degree_Id =" + Degree_Id + ", DegreeObtained =" + DegreeObtained + ",DateDegreeAwarded ='" + DateDegreeAwarded + "', US_Equivalency_Id =" + US_Equivalency_Id + ", City ='" + City.Replace("'", "''") + "', State_or_province ='" + State.Replace("'", "''") + "' where(id=" + id + ")";
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        public static void Grid_univgrid(GridView commongrid, string Request_Id)
        {
            string strSQL = " SELECT cc_masterdataset.institution.Name, cc_masterdataset.degree.Name AS Expr1, cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }

        #region Educationpopup
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
            string query = "SELECT Id FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_ID + ") AND (Name = '" + Name.Replace("'", "''") + "') AND (Type = '" + category + "') AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + "))";
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
        public static void Getdegree(DropDownList degree, int type, int Country_id, string customer)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
            string strSQL;
            if (type == 0)
            {

                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'HighSchool') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + ")) order by Name";
            }
            else
            {
                strSQL = "SELECT Id, Name FROM cc_masterdataset.degree WHERE (Country_ID = " + Country_id + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=" + app.AdminId + ")) order by Name";
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
        public static void delete_Applicant_Education_History(int id)
        {
            string query = "DELETE FROM cc_clientdataset.applicant_education_history WHERE id =" + id;
            GetDataSet_withoutID(query);
        }

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

        public static bool create_education(int Evaluation_Request_Id, int Country_Id, int Major_Id, int EducationInstitution_Id, string StartDate, string EndDate, int Degree_Id, int DegreeObtained, string DateDegreeAwarded, int US_Equivalency_Id, string City, string State)
        {
            string query = "INSERT INTO cc_clientdataset.applicant_education_history (Evaluation_Request_Id, Country_Id,Major_Id, EducationInstitution_Id, StartDate, EndDate, Degree_Id, DegreeObtained,DateDegreeAwarded,US_Equivalency_Id,City,State_or_province) VALUES  (" + Evaluation_Request_Id + "," + Country_Id + "," + Major_Id + "," + EducationInstitution_Id + ",'" + StartDate + "','" + EndDate + "'," + Degree_Id + "," + DegreeObtained + ",'" + DateDegreeAwarded + "'," + US_Equivalency_Id + ",'" + City.Replace("'", "''") + "','" + State.Replace("'", "''") + "')";
            bool result = GetDataSet_withoutID(query);
            return result;
        }
        #endregion
        #endregion

        #region service
        //service
        public static void GetCount(DropDownList options, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                options.Items.Add(i.ToString());
               
            }
        }
        public static DataSet Grid_Evaluationservice(string Request_Id)
        {
            string strSQL = "SELECT COUNT(cc_clientdataset.service.Name) as Countno,  cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND ((cc_clientdataset.service.Type ='Evaluation') OR(cc_clientdataset.service.Type ='Evaluation Multiplier')) GROUP BY cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet Grid_Additionalservice(string Request_Id)
        {
            string strSQL = "SELECT COUNT(cc_clientdataset.service.Name) as Countno,  cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") AND ((cc_clientdataset.service.Type ='Additional') OR(cc_clientdataset.service.Type ='Additional Multiplier')) GROUP BY cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static void clear_evaluation_services(int id)
        {
            string query = "DELETE FROM cc_clientdataset.evaluation_services WHERE Evaluation_Request_Id =" + id;
            GetDataSet_withoutID(query);
        }
        public static DataSet serviceDescription(int Id)
        {
            string strSQL = "SELECT id,Description FROM cc_clientdataset.service WHERE (id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static void Grid_servicegrid(GridView commongrid, string Request_Id)
        {
           // string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") ORDER BY cc_clientdataset.evaluation_services.id";
            string strSQL = "SELECT COUNT(cc_clientdataset.service.Name) as Countno,  cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost as price,(COUNT(cc_clientdataset.service.Name)* cc_clientdataset.service.Cost) as Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") GROUP BY cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.Service_Id"; 
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_service(GridView commongrid,string Customer_Id)
        {
            string strSQL = "SELECT id, Name, Cost, Description, Customer_Id,Type FROM cc_clientdataset.service WHERE (Customer_Id =" + Customer_Id + ") AND ((Type = 'Evaluation') OR (Type = 'Evaluation Multiplier')) Order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_addservice(GridView commongrid, string Customer_Id)
        {
            string strSQL = "SELECT id, Name, Cost, Description, Customer_Id,Type FROM cc_clientdataset.service WHERE (Customer_Id =" + Customer_Id + ") AND ((Type = 'Additional') OR (Type = 'Additional Multiplier')) Order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool create_Service(int Evaluation_Request_Id, int Service_Id)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_services (Service_Id, Evaluation_Request_Id) VALUES (" + Service_Id + "," + Evaluation_Request_Id + ")";
            bool result = GetDataSet_withoutID(query);
            return result;
        }

        //primary
        public static string delivery_copycount(string customer_Id)
        {
            string count="";
            string strSQL = "SELECT Delivery_copy FROM cc_masterdataset.customersettings WHERE (Customer_Id =" + customer_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                count = ds.Tables[0].Rows[0]["Delivery_copy"].ToString();
            }
            else { count = "0"; }
            return count;
        }
        public static void displayprimary(DetailsView commongrid, string Request_Id)
        {
            string strSQL = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Getdeliverytype(DropDownList options, string Customer_id)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.delivery_type where (Customer_Id=" + Customer_id + ") AND ( Type='Mail') order by priority";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Name";
            options.DataValueField = "id";
            options.Items.Add(new ListItem("Select", "0"));
            options.DataBind();
        }
        public static DataSet GetprimaryAddress(string Request_Id)
        {
            string strSQL = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Id as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.Optional_InstitutionName FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet ds = GetDataSet(strSQL);
            return ds;

        }
        public static DataSet GetAddress(string Record_Id)
        {
            string strSQL = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Id as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (a.id="+Record_Id +")";
            DataSet ds = GetDataSet(strSQL);
            return ds;

        }
        public static void deliverydisplay(GridView commongrid, string Request_Id)
        {
            DataTable table = new DataTable();
            table.Columns.Add("rowid", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Addressline1", typeof(string));
            table.Columns.Add("Addressline2", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("State_or_province", typeof(string));
            table.Columns.Add("Zip_or_PostalCode", typeof(string));
            table.Columns.Add("Delivery_Type_Id", typeof(int));
            table.Columns.Add("CopyNo", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Sentto", typeof(string));

            string strSQL1 = " SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ")";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {

                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                         dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                        dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Primary"
                            );
                    }
                    else
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                        dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                         dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Other"
                            );
                    }
                }

                commongrid.DataSource = table;
                commongrid.DataBind();
            }
            else { commongrid.DataBind(); }

            //commongrid.DataSource = ds;
           // commongrid.DataBind();            
        }
        public static void evaluationAddress(GridView commongrid, string Request_Id)
        {
            DataTable table = new DataTable();
            table.Columns.Add("rowid", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Addressline1", typeof(string));
            table.Columns.Add("Addressline2", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("State_or_province", typeof(string));
            table.Columns.Add("Zip_or_PostalCode", typeof(string));
            table.Columns.Add("Delivery_Type_Id", typeof(int));
            table.Columns.Add("CopyNo", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Sentto", typeof(string));

            string strSQL1 = " SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ")  AND (Type='Evaluation') AND (CopyNo<>'primary') order by CopyNo";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {

                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                         dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                        dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Primary"
                            );
                    }
                    else
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                        dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                         dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Other"
                            );
                    }
                }

                commongrid.DataSource = table;
                commongrid.DataBind();
            }
            else { commongrid.DataBind(); }

            //commongrid.DataSource = ds;
            // commongrid.DataBind();            
        }
        public static void deliveryAddress(GridView commongrid, string Request_Id)
        {
            DataTable table = new DataTable();
            table.Columns.Add("rowid", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Addressline1", typeof(string));
            table.Columns.Add("Addressline2", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("State_or_province", typeof(string));
            table.Columns.Add("Zip_or_PostalCode", typeof(string));
            table.Columns.Add("Delivery_Type_Id", typeof(int));
            table.Columns.Add("CopyNo", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Sentto", typeof(string));

            string strSQL1 = " SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ")";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {

                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                         dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                        dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Primary"
                            );
                    }
                    else
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                        dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                         dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Other"
                            );
                    }
                }

                commongrid.DataSource = table;
                commongrid.DataBind();
            }
            else { commongrid.DataBind(); }

            //commongrid.DataSource = ds;
            // commongrid.DataBind();            
        }
        public static bool update_primaryaddress(int Delivery_Type_Id, int Request_Id, string Name, string Address1, string Address2, string City, string State_or_Province, string Zip_or_PostalCode, int Country_Id, int Count, string type_copy, string CopyNo,bool Isupdate,string optional)
        {
            //list of primary address
            ArrayList addresslist = new ArrayList();

            string strSQL1 = " SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (CopyNo<>'primary')";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {
                int j=0;
                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                        addresslist.Add(dsall.Tables[0].Rows[i]["id"].ToString()); 
                        j++;
                    }                    
                }

            }

           //update primary address
            string query = "Update cc_clientdataset.evaluation_delivery set Delivery_Type_Id=" + Delivery_Type_Id + ",Name='" + Name.Replace("'", "''") + "', Addressline1='" + Address1.Replace("'", "''") + "',Addressline2='" + Address2.Replace("'", "''") + "', City='" + City.Replace("'", "''") + "', State_or_Province='" + State_or_Province.Replace("'", "''") + "',Zip_or_PostalCode='" + Zip_or_PostalCode.Replace("'", "''") + "', Country_Id=" + Country_Id + ", Count=" + Count + ",Optional_InstitutionName='" + optional.Replace("'", "''") + "' Where (Evaluation_Request_Id =" + Request_Id + ") AND (Type='" + type_copy.Replace("'", "''") + "')AND (CopyNo='" + CopyNo.Replace("'", "''") + "')";
            bool result = GetDataSet_withoutID(query);

            if (result)
            {
                // update other primary address
                if (Isupdate)
                {
                    for (int i = 0; i <= addresslist.Count-1; i++)
                    {
                        query = "Update cc_clientdataset.evaluation_delivery set Delivery_Type_Id=" + Delivery_Type_Id + ",Name='" + Name.Replace("'", "''") + "', Addressline1='" + Address1.Replace("'", "''") + "',Addressline2='" + Address2.Replace("'", "''") + "', City='" + City.Replace("'", "''") + "', State_or_Province='" + State_or_Province.Replace("'", "''") + "',Zip_or_PostalCode='" + Zip_or_PostalCode.Replace("'", "''") + "', Country_Id=" + Country_Id + "  Where (id=" + addresslist[i].ToString() + ")";
                        GetDataSet_withoutID(query);
                    }
                }

            }


            return result;



        }

        //Evaluation
        public static void Evaluationdisplay(GridView commongrid, string Request_Id)
        {
            string strSQL = "SELECT cast(count(*)as char)as Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.evaluation_delivery.Name AS Recipient FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation')) GROUP BY cc_clientdataset.delivery_type.Name,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static DataSet delivery_copy(int customer_Id)
        {
            string strSQL = "SELECT Delivery_copy, Delivery_Instructions,Education_Instructions,Document_Instructions FROM cc_masterdataset.customersettings WHERE (Customer_Id =" + customer_Id + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
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
        public static int SaveSameAddress(int Request_Id, int cust_id, string evalcount)
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
        public static bool create_Evaluation_Delivery(int Delivery_Type_Id, int Evaluation_Request_Id, string Name, string Address1, string Address2, string City, string State_or_Province, string Zip_or_PostalCode, int Country_Id, int Count, string type_copy, string CopyNo,string optional)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name, Addressline1,Addressline2, City, State_or_Province,Zip_or_PostalCode, Country_Id, Count,Type,CopyNo,Optional_InstitutionName) VALUES (" + Delivery_Type_Id + "," + Evaluation_Request_Id + ",'" + Name.Replace("'", "''") + "','" + Address1.Replace("'", "''") + "','" + Address2.Replace("'", "''") + "','" + City.Replace("'", "''") + "','" + State_or_Province.Replace("'", "''") + "','" + Zip_or_PostalCode.Replace("'", "''") + "'," + Country_Id + "," + Count + ",'" + type_copy.Replace("'", "''") + "','" + CopyNo.Replace("'", "''") + "','" + optional.Replace("'", "''") + "')";
            bool result = GetDataSet_withoutID(query);
            return result;



        }
        public static ArrayList populate_evaluationAddress(string Request_Id)
        {
            ArrayList list = new ArrayList();
            DataTable table = new DataTable();
            table.Columns.Add("rowid", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Addressline1", typeof(string));
            table.Columns.Add("Addressline2", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("State_or_province", typeof(string));
            table.Columns.Add("Zip_or_PostalCode", typeof(string));
            table.Columns.Add("Delivery_Type_Id", typeof(int));
            table.Columns.Add("CopyNo", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Sentto", typeof(string));

            string strSQL1 = " SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ")  AND (Type='Evaluation') AND (CopyNo <>'primary') order by CopyNo";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {

                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                         dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                        dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Primary"
                            );
                    }
                    else
                    {
                        table.Rows.Add(
                        dsall.Tables[0].Rows[i]["id"].ToString(),
                        dsall.Tables[0].Rows[i]["Name"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline1"].ToString(),
                        dsall.Tables[0].Rows[i]["Addressline2"].ToString(),
                        dsall.Tables[0].Rows[i]["City"].ToString(),
                        dsall.Tables[0].Rows[i]["Country"].ToString(),
                        dsall.Tables[0].Rows[i]["State_or_province"].ToString(),
                        dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString(),
                        dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString(),
                        dsall.Tables[0].Rows[i]["CopyNo"].ToString(),
                         dsall.Tables[0].Rows[i]["Type"].ToString(),
                        "Other"
                            );
                    }
                }
            }

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    list.Add(table.Rows[i]["Sentto"].ToString() +"|"+ table.Rows[i]["CopyNo"].ToString());
                }
                
            }

            return list;            
                      
        }

        //additional
        public static void Grid_copycharger(GridView commongrid, int Request_Id, string category_type)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
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
        public static void Grid_addevalgrid(GridView commongrid, int Request_Id)
        {
            string strSQL = "SELECT cast(count(*)as char)as sno,sum(Count)as Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.evaluation_delivery.Name AS Recipient FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Additional')) GROUP BY cc_clientdataset.delivery_type.Name,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
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
        public static string CheckPrimary(string rowid, string Request_Id)
        {        
            string result="";
            string strSQL1 = "SELECT a.id,a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id,a.CopyNo,a.Type FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (a.id ="+rowid +")";
            DataSet dsall = GetDataSet(strSQL1);

            string strSQL2 = " SELECT a.Name,a.Addressline1, a.Addressline2, a.City, b.Name as Country, a.State_or_province, a.Zip_or_PostalCode,a.Delivery_Type_Id FROM cc_clientdataset.evaluation_delivery a inner join cc_masterdataset.countries b on a.Country_Id = b.Id WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type='Evaluation')AND (CopyNo='primary')";
            DataSet dsprimary = GetDataSet(strSQL2);

            if ((dsall.Tables[0].Rows.Count > 0) && (dsprimary.Tables[0].Rows.Count > 0))
            {

                for (int i = 0; i <= dsall.Tables[0].Rows.Count - 1; i++)
                {
                    if ((dsall.Tables[0].Rows[i]["Name"].ToString() == dsprimary.Tables[0].Rows[0]["Name"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline1"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline1"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Addressline2"].ToString() == dsprimary.Tables[0].Rows[0]["Addressline2"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["City"].ToString() == dsprimary.Tables[0].Rows[0]["City"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Country"].ToString() == dsprimary.Tables[0].Rows[0]["Country"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["State_or_province"].ToString() == dsprimary.Tables[0].Rows[0]["State_or_province"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Zip_or_PostalCode"].ToString() == dsprimary.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString()) &&
                        (dsall.Tables[0].Rows[i]["Delivery_Type_Id"].ToString() == dsprimary.Tables[0].Rows[0]["Delivery_Type_Id"].ToString()))
                    {
                       
                      result =  "Primary";
                           
                    }
                    else
                    {
                        result = "Other";
                           
                    }
                }

                
            }
            return result;
                      
        }
        public static void populateAddress(TextBox frm5_Fname, TextBox frm5_add1, TextBox frm5_add2, TextBox frm5_city, TextBox frm5_state, TextBox frm5_zip, DropDownList frm5_country, DropDownList frm5_deliverytype, DropDownList frm5_copies_addl, string rowid, TextBox frm5_addlinstname)
        {
            string strSQL = " SELECT Count,Name,Addressline1, Addressline2, City, Country_Id, State_or_province, Zip_or_PostalCode,Delivery_Type_Id,Optional_InstitutionName FROM cc_clientdataset.evaluation_delivery WHERE (id=" + rowid + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frm5_copies_addl.SelectedValue = ds.Tables[0].Rows[0]["Count"].ToString();
                frm5_Fname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                frm5_add1.Text = ds.Tables[0].Rows[0]["Addressline1"].ToString();
                frm5_add2.Text = ds.Tables[0].Rows[0]["Addressline2"].ToString();
                frm5_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                frm5_state.Text = ds.Tables[0].Rows[0]["State_or_province"].ToString();
                frm5_zip.Text = ds.Tables[0].Rows[0]["Zip_or_PostalCode"].ToString();
                frm5_country.SelectedValue = ds.Tables[0].Rows[0]["Country_Id"].ToString();
                frm5_deliverytype.SelectedValue = ds.Tables[0].Rows[0]["Delivery_Type_Id"].ToString();
                frm5_addlinstname.Text = ds.Tables[0].Rows[0]["Optional_InstitutionName"].ToString();
            }


        }
        public static bool UpdateAdditionalCopy(int Delivery_Type_Id, int Evaluation_Request_Id, string Name, string Address1, string Address2, string City, string State_or_Province, string Zip_or_PostalCode, int Country_Id, int Count, string type_copy, string CopyNo,string rowid,string optional)
        {
            string query = "Update cc_clientdataset.evaluation_delivery set Delivery_Type_Id=" + Delivery_Type_Id + ", Evaluation_Request_Id=" + Evaluation_Request_Id + ", Name='" + Name.Replace("'", "''") + "', Addressline1='" + Address1.Replace("'", "''") + "',Addressline2='" + Address2.Replace("'", "''") + "', City='" + City + "', State_or_Province='" + State_or_Province + "',Zip_or_PostalCode='" + Zip_or_PostalCode + "', Country_Id=" + Country_Id + ", Count=" + Count + ",Type='" + type_copy + "',CopyNo='" + CopyNo + "',Optional_InstitutionName='"+ optional +"' where id="+rowid;                                                                                                                                                                                                                                                                                                                                                                       
            bool result = GetDataSet_withoutID(query);
            return result;



        }

        //fax
        public static void faxgrid_display(GridView commongrid, int Request_Id, string category_type)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void delete_Evaluation_Delivery(int id)
        {
            string query = "DELETE FROM cc_clientdataset.evaluation_delivery WHERE id =" + id;
            GetDataSet_withoutID(query);
        }
        public static bool create_Evaluation_Delivery(int Delivery_Type_Id, int Evaluation_Request_Id, string FirstName, string Faxno, int Country_Id, int Count, string type_copy, string Customer_id)
        {
            bool result = false;
            string strquery = "SELECT * FROM [cc_clientdataset].[delivery_type] where Type='Fax' and Customer_Id=" + Customer_id;
            DataSet ds = GetDataSet(strquery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name,Faxno,Country_Id, Count,Type,CopyNo) VALUES (" + ds.Tables[0].Rows[0]["id"].ToString() + "," + Evaluation_Request_Id + ",'" + FirstName.Replace("'", "''") + "','" + Faxno.Replace("'", "''") + "'," + Country_Id + "," + Count + ",'" + type_copy.Replace("'", "''") + "','Fax')";
                result = GetDataSet_withoutID(query);
            }
            return result;

        }
        public static DataSet getfax_byid(string rowid)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.evaluation_delivery WHERE (id="+ rowid+")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static bool updatefax(string FirstName, string Faxno,string rowid)
        {
            bool result = false;

            string query = "Update cc_clientdataset.evaluation_delivery set Name='" + FirstName.Replace("'", "''") + "',Faxno='" + Faxno.Replace("'", "''") + "' where id=" + rowid;
            result = GetDataSet_withoutID(query);


            return result;
        }

        //Email
        public static void emailgrid_display(GridView commongrid, int Request_Id, string category_type)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool create_Email_Delivery(int Delivery_Type_Id, int Evaluation_Request_Id, string FirstName, string Email, int Country_Id, int Count, string type_copy, string Customer_id)
        {
            bool result = false;
            string strquery = "SELECT * FROM [cc_clientdataset].[delivery_type] where Type='Email' and Customer_Id=" + Customer_id;
            DataSet ds = GetDataSet(strquery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "INSERT INTO cc_clientdataset.evaluation_delivery (Delivery_Type_Id, Evaluation_Request_Id, Name,Email,Country_Id, Count,Type,CopyNo) VALUES (" + ds.Tables[0].Rows[0]["id"].ToString() + "," + Evaluation_Request_Id + ",'" + FirstName.Replace("'", "''") + "','" + Email  + "'," + Country_Id + "," + Count + ",'" + type_copy + "','Email')";
                result = GetDataSet_withoutID(query);
            }
            return result;

        }
        public static DataSet getemail_byid(string rowid)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.evaluation_delivery WHERE (id=" + rowid + ")";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static bool updateemail(string FirstName, string Email, string rowid)
        {
            bool result = false;

            string query = "Update cc_clientdataset.evaluation_delivery set Name='" + FirstName.Replace("'", "''") + "',Email='" + Email.Replace("'", "''") + "' where id=" + rowid;
            result = GetDataSet_withoutID(query);


            return result;
        }


        //cost
        public static bool update_Totalcost(string applicationid,string cost)
        {
            bool result = false;

             string query = "UPDATE cc_clientdataset.applicant SET Amount = '" +cost + "' Where id=" + applicationid;
                result = GetDataSet_withoutID(query);
            

            return result;
        }

        //Quick app Insertion        
        public static string create_Applicantid(int id)
        {
            string FileNumber = create_Randomid();
            string query = "UPDATE cc_clientdataset.applicant SET FileNumber='" + FileNumber + "' WHERE id=" + id;
            GetDataSet_withoutID(query);
            return FileNumber;
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
        public static int create_Quickapp(string FirstName, string MiddleName, string LastName, int Countryofbirth, string otherFirstName, string otherMiddleName, string otherLastName, string Addressline1, string Addressline2, string City, string State_or_province, string Zip_or_PostalCode, int CountryId, string HomePhone, string WorkPhone, string MobilePhone, string DateOfBirth,string Gender,string Email,int Join_cc_db,int Publish_Info, int Customer_Id)
        {
            string query = "INSERT INTO cc_clientdataset.applicant (FirstName, MiddleName, LastName, Gender, DateOfBirth, Addressline1,Addressline2, City, CountryId, State_or_province, Zip_or_PostalCode, HomePhone, WorkPhone, MobilePhone, Customer_Id,otherFirstName,otherMiddleName,otherLastName,Countryofbirth,Email,Join_cc_db,Publish_Info) VALUES ('" + FirstName.Replace("'", "''") + "','" + MiddleName.Replace("'", "''") + "','" + LastName.Replace("'", "''") + "','" + Gender + "','" + DateOfBirth + "','" + Addressline1.Replace("'", "''") + "','" + Addressline2.Replace("'", "''") + "','" + City.Replace("'", "''") + "'," + CountryId + ",'" + State_or_province.Replace("'", "''") + "','" + Zip_or_PostalCode + "','" + HomePhone + "','" + WorkPhone + "','" + MobilePhone + "'," + Customer_Id + ",'" + otherFirstName.Replace("'", "''") + "','" + otherMiddleName.Replace("'", "''") + "','" + otherLastName.Replace("'", "''") + "'," + Countryofbirth +",'"+ Email + "'," + Join_cc_db + "," + Publish_Info + ")";
            int applicant_id = GetDataSet_withID(query);
            return applicant_id;
        }
        public static int GetpurposeId(string PurposeName)
        {
            int purposeId = 0;
            string strSQL = "select id from cc_clientdataset.evaluation_purpose Where Evaluation_Name='" + PurposeName +"'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                purposeId = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }

            return purposeId;
        }
        public static int create_Quickapp_purpose(int Applicant_Id, int Purpose_Id, string Eval_institution, string Eval_organization, string Eval_Attorney, string Eval_Board, string Eval_State, string Eval_Military_Recruiter, string Eval_other)
        {
            string query = "INSERT INTO cc_clientdataset.evaluation_request (Applicant_Id, Purpose_Id, Eval_institution, Eval_organization, Eval_Attorney, Eval_Board, Eval_State, Eval_Military_Recruiter, Eval_other,Application_Recieved,Documents_Recieved,Payment_Recieved,Evaluation_Complete,Verification_Complete,Evaluation_Approved,Packaging_Complete,Delievery_Complete) VALUES (" + Applicant_Id + "," + Purpose_Id + ",'" + Eval_institution.Replace("'", "''") + "','" + Eval_organization.Replace("'", "''") + "','" + Eval_Attorney.Replace("'", "''") + "','" + Eval_Board.Replace("'", "''") + "','" + Eval_State.Replace("'", "''") + "','" + Eval_Military_Recruiter.Replace("'", "''") + "','" + Eval_other.Replace("'", "''") + "'," + "0,0,0,0,0,0,0,0)";
            int RequestID = GetDataSet_withID(query);
            return RequestID;

        }
        #endregion


      
        
        
        #endregion

        #region View
        public struct SectionAttributes
        {
            public bool AddSection;
            public bool FaxSection;
            public bool EmailSection;
            public int AppType;
        }
        public static SectionAttributes section(string Clientid)
        {

            SectionAttributes sa = new SectionAttributes();
            string Appsettingquery = "Select * from [cc_masterdataset].[customersettings] where [Customer_Id]=" + Clientid;
            DataSet ds = new DataSet();
            ds = GetDataSet(Appsettingquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sa.AddSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Additional_Section"].ToString());
                sa.FaxSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Fax_Section"].ToString());
                sa.EmailSection = Convert.ToBoolean(ds.Tables[0].Rows[0]["Email_Section"].ToString());
                sa.AppType = Convert.ToInt32(ds.Tables[0].Rows[0]["Application_Type"].ToString());
            }
            else
            {
                sa.AddSection = false;
                sa.FaxSection = false;
                sa.EmailSection = false;
                sa.AppType = 1;
            }
            return sa;
        }
        public static void DetailsView_Applview(DetailsView personalinfo, DetailsView purpose, DetailsView payment, string FileNumber, GridView hischool, GridView college, GridView service1grid, GridView addongrid, GridView copychargergrid, int Customer_Id, GridView deliverydetails, GridView fax_grid, GridView fax_details, GridView Email_grid, GridView email_details,DetailsView senderinfo)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FirstName, cc_clientdataset.applicant.MiddleName, cc_clientdataset.applicant.LastName,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.Gender, cc_clientdataset.applicant.otherFirstName+' '+cc_clientdataset.applicant.otherMiddleName+' '+cc_clientdataset.applicant.otherLastName AS othername, cc_clientdataset.applicant.DateOfBirth, cc_clientdataset.applicant.Addressline1,cc_clientdataset.applicant.Addressline2, cc_clientdataset.applicant.City,cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.PreviousCredential_id, countries.Name AS country,countries_1.Name AS CountryBirth, cc_clientdataset.applicant.State_or_province, cc_clientdataset.applicant.Zip_or_PostalCode,cc_clientdataset.applicant.Paymentmode,cc_clientdataset.applicant.Paymentstatus,cc_clientdataset.applicant.Authorizecode,cc_clientdataset.applicant.Transactioncode, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email,cc_clientdataset.evaluation_purpose.Evaluation_Name, cc_clientdataset.evaluation_request.[Eval_institution],cc_clientdataset.evaluation_request.[Eval_organization],cc_clientdataset.evaluation_request.[Eval_Attorney],cc_clientdataset.evaluation_request.[Eval_Board],cc_clientdataset.evaluation_request.[Eval_State],cc_clientdataset.evaluation_request.[Eval_Military_Recruiter],cc_clientdataset.evaluation_request.[Eval_other],cc_clientdataset.evaluation_request.[Senders_Name],cc_clientdataset.evaluation_request.[Senders_Contact],cc_clientdataset.evaluation_request.[Purpose_Notes],cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id LEFT JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id INNER JOIN cc_masterdataset.countries countries_1 ON cc_clientdataset.applicant.Countryofbirth = countries_1.ID where cc_clientdataset.applicant.FileNumber='" + FileNumber + "'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                personalinfo.DataSource = ds;
                personalinfo.DataBind();
                purpose.DataSource = ds;
                purpose.DataBind();

                senderinfo.DataSource = ds;
                senderinfo.DataBind();

                payment.DataSource = ds;
                payment.DataBind();

                Grid_hischoolgrid(hischool, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                Grid_univgrid(college, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));

                Grid_service1grid(service1grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                Grid_copycharger(copychargergrid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Additional", Customer_Id);
                Grid_addonsservice(addongrid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                Grid_Fax(fax_grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Fax", Customer_Id);
                Grid_Email(Email_grid, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), "Email", Customer_Id);
                Grid_deliveryinfo(deliverydetails, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                Grid_faxgrid(fax_details, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                Grid_emailgrid(email_details, Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
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
        public static void Grid_service1grid(GridView commongrid, int Request_Id)
        {
           // string strSQL = "SELECT cc_clientdataset.evaluation_services.Evaluation_Request_Id, cc_clientdataset.service.Name, cc_clientdataset.service.Cost, cc_clientdataset.service.Description, cc_clientdataset.service.Type, cc_clientdataset.evaluation_services.id, cc_clientdataset.evaluation_services.Service_Id FROM cc_clientdataset.evaluation_services INNER JOIN cc_clientdataset.service ON cc_clientdataset.evaluation_services.Service_Id = cc_clientdataset.service.id WHERE (cc_clientdataset.evaluation_services.Evaluation_Request_Id =" + Request_Id + ") ORDER BY cc_clientdataset.evaluation_services.id";
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

            //cost  
            double final = 0.00;
            foreach (GridViewRow row in commongrid.Rows)
            {
                Label total = ((Label)row.FindControl("Label1"));
                Label result = ((Label)commongrid.FooterRow.FindControl("Label7"));
                String str1 = total.Text;
                String delim = "$";
                String str2 = str1.Trim(delim.ToCharArray());

                final = final + Convert.ToDouble(str2.ToString());

                result.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
            }


        }
        public static void Grid_copycharger(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

            //cost
            double final = 0.00;
            int price = getAdditional(Customer_Id);
            foreach (GridViewRow row in commongrid.Rows)
            {
                Label type = ((Label)row.FindControl("Label3"));
                type.Text = "Official Hard Copy";
                Label cost = ((Label)row.FindControl("Label9"));
                Label count = ((Label)row.FindControl("Label1"));
                Label totalcost = ((Label)row.FindControl("Label10"));
                Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
                cost.Text = price.ToString();
                double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
                totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
                cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

                String str1 = totalcost.Text;
                String delim = "$";
                String str2 = str1.Trim(delim.ToCharArray());

                final = final + Convert.ToDouble(str2.ToString());
                total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
            }

        }
        public static void Grid_addonsservice(GridView commongrid, int Request_Id)
        {
           // string strSQL = "SELECT SUM(cc_clientdataset.evaluation_delivery.Count) AS Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') GROUP BY cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type HAVING ((COUNT(cc_clientdataset.evaluation_delivery.Name) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.City) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Zip_or_PostalCode) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0) AND (COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0))";
           // string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional')";
            string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Count, cc_clientdataset.delivery_type.Name, cc_clientdataset.delivery_type.Cost, cc_clientdataset.delivery_type.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_clientdataset.delivery_type ON cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id WHERE (cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =" + Request_Id + ") AND (cc_clientdataset.evaluation_delivery.Type = 'Evaluation' OR cc_clientdataset.evaluation_delivery.Type = 'Additional') AND (cc_clientdataset.evaluation_delivery.id not in(" +
            "SELECT ed2.id  FROM [cc_clientdataset].[evaluation_delivery] as ed1,[cc_clientdataset].[evaluation_delivery] as ed2 " +
            "where ed1.Addressline1= ed2.Addressline1 and ed1.Addressline2=ed2.Addressline2 and ed1.City=ed2.City and ed1.State_or_Province=ed2.State_or_Province and " +
            "ed1.Zip_or_PostalCode=ed2.Zip_or_PostalCode and ed1.Evaluation_Request_Id=ed2.Evaluation_Request_Id and ed1.Country_Id = ed2.Country_Id and ed1.CopyNo='primary' and ed2.CopyNo ='Additional' and ed1.Evaluation_Request_Id=" + Request_Id + "))";

            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

            //cost
            double final = 0.00;
            foreach (GridViewRow row in commongrid.Rows)
            {
                //cost mulitiply
                Label type = ((Label)row.FindControl("lbldeliveryname"));
                Label count = ((Label)row.FindControl("Label4"));
                Label cost = ((Label)row.FindControl("Label2"));

                if (type.Text == "Fax")
                {
                    int result = (Convert.ToInt32(count.Text) * Convert.ToInt32(cost.Text));
                    cost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));

                }
                else
                {
                    cost.Text = String.Format("{0:c}", Convert.ToDouble(cost.Text));
                }

                //total cost            
                Label footer = ((Label)commongrid.FooterRow.FindControl("Label7"));
                String str1 = cost.Text;
                String delim = "$";
                String str2 = str1.Trim(delim.ToCharArray());

                final = final + Convert.ToDouble(str2.ToString());

                footer.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));

            }
        }
        public static void Grid_Fax(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

            double final = 0.00;
            int price = getFaxCost(Customer_Id);
            foreach (GridViewRow row in commongrid.Rows)
            {
                Label cost = ((Label)row.FindControl("Label9"));
                Label count = ((Label)row.FindControl("Label1"));
                Label totalcost = ((Label)row.FindControl("Label10"));
                Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
                cost.Text = price.ToString();
                double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
                totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
                cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

                String str1 = totalcost.Text;
                String delim = "$";
                String str2 = str1.Trim(delim.ToCharArray());

                final = final + Convert.ToDouble(str2.ToString());
                total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
            }



        }
        public static void Grid_Email(GridView commongrid, int Request_Id, string category_type, int Customer_Id)
        {
            string strSQL = "SELECT Count, Type, id,Name FROM cc_clientdataset.evaluation_delivery WHERE (Evaluation_Request_Id =" + Request_Id + ") AND (Type ='" + category_type + "')order by id";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

            double final = 0.00;
            int price = getEmailCost(Customer_Id);
            foreach (GridViewRow row in commongrid.Rows)
            {
                Label type = ((Label)row.FindControl("Label3"));
                type.Text = "Official Electronic Copy";
                Label cost = ((Label)row.FindControl("Label9"));
                Label count = ((Label)row.FindControl("Label1"));
                Label totalcost = ((Label)row.FindControl("Label10"));
                Label total = ((Label)commongrid.FooterRow.FindControl("Label11"));
                cost.Text = price.ToString();
                double result = (Convert.ToDouble(cost.Text) * Convert.ToDouble(count.Text));
                totalcost.Text = String.Format("{0:c}", Convert.ToDouble(result.ToString()));
                cost.Text = String.Format("{0:c}", Convert.ToDouble(price.ToString()));

                String str1 = totalcost.Text;
                String delim = "$";
                String str2 = str1.Trim(delim.ToCharArray());

                final = final + Convert.ToDouble(str2.ToString());
                total.Text = String.Format("{0:c}", Convert.ToDouble(final.ToString()));
            }



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
        public static void Grid_deliveryinfo(GridView commongrid, int request_Id)
        {
            //string strSQL = "SELECT sum(cc_clientdataset.evaluation_delivery.Count)as Count,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1 , cc_clientdataset.evaluation_delivery.Addressline2, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id =  " + request_Id + ")AND(Type <> 'Fax')) GROUP BY cc_masterdataset.countries.Name,cc_clientdataset.evaluation_delivery.Type,cc_clientdataset.evaluation_delivery.Name,cc_clientdataset.evaluation_delivery.Addressline1,cc_clientdataset.evaluation_delivery.Addressline2,cc_clientdataset.evaluation_delivery.City,cc_clientdataset.evaluation_delivery.State_or_Province,cc_clientdataset.evaluation_delivery.zip_or_PostalCode,cc_clientdataset.evaluation_delivery.Country_Id,cc_clientdataset.evaluation_delivery.Delivery_Type_Id HAVING (( COUNT(cc_clientdataset.evaluation_delivery.Name) > 0 ) AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline1) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Addressline2) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.City) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.State_or_Province) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.zip_or_PostalCode) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Country_Id) > 0 )AND ( COUNT(cc_clientdataset.evaluation_delivery.Delivery_Type_Id) > 0 ))";
            string strSQL = "SELECT cc_clientdataset.evaluation_delivery.Optional_InstitutionName,cc_clientdataset.evaluation_delivery.Count,cc_clientdataset.evaluation_delivery.CopyNo,cc_clientdataset.delivery_type.Name as deliveryservice,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1 , cc_clientdataset.evaluation_delivery.Addressline2, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.delivery_type on cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id  where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + request_Id + ")AND(cc_clientdataset.evaluation_delivery.Type <> 'Fax'))";
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

        #region Remove
        public static void Remove_Application(string fileno, string customer)
        {
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand("Remove_Application", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@fileno", fileno));
            cmd.Parameters.Add(new SqlParameter("@customer", customer));
            int k = cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion


      

        #endregion

        #region Common
        public static void Getcountry(DropDownList country, string ClientId)
        {
            string strSQL = " Select a.Id, a.Name  from cc_masterdataset.countries a order by Name ";
            DataSet ds = GetDataSet(strSQL);
            country.DataSource = ds;
            country.DataTextField = "Name";
            country.DataValueField = "Id";
            country.AppendDataBoundItems = true;
            country.Items.Add(new ListItem("Select", "0"));
            country.DataBind();

        }
        public static void GetEquivalency(DropDownList option, string ClientId,string AdminId,Boolean result)
        {

            string strSQL = "SELECT * FROM cc_clientdataset.Equivalency where (Customer_Id="+ AdminId +") OR (Customer_Id=" + ClientId + ") order by Name";
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "id";
            option.AppendDataBoundItems = true;
            if (!result){option.Items.Add(new ListItem("Select", "0"));}
            option.DataBind();
        }
        public static void GetGradescale(DropDownList option, string ClientId, string AdminId,string CountryID)
        {

            string strSQL = "SELECT * FROM cc_masterdataset.gradescales where ((Customer_Id=" + AdminId + ") OR (Customer_Id=" + ClientId + ")) AND (Country_ID="+ CountryID +")";
            DataSet ds = GetDataSet(strSQL); 
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "ID";
            option.AppendDataBoundItems = true;
            option.Items.Add(new ListItem("Select", "0"));
            option.DataBind();
        }
        public static string Getinstitution(string InstitutionId)
        {
            string result = "";
            string strSQL = " Select a.Name  from cc_masterdataset.institution a where a.Id="+ InstitutionId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            return result;

        }
        public static string GetRequestid(string FileNumber)
        {
            string result = "";
            DataSet ds = new DataSet();
            string strSQL = "SELECT cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["id"].ToString();
            }
            return result;
        }
        public static Boolean GetRecommendation(string Recordid, DropDownList option, string ClientId, string AdminId)
        {
            Boolean result = false;
            DataSet ds = new DataSet();
            string strSQL = "SELECT * FROM cc_clientdataset.recom_selection WHERE (application_education_history_id =" + Recordid + ")order by recom_order"; 
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //strSQL = "SELECT * FROM cc_clientdataset.Equivalency where ID IN(SELECT Equivalency_Id FROM cc_masterdataset.Linkage WHERE Id IN(SELECT us_equivalency_id FROM cc_clientdataset.recom_selection WHERE (application_education_history_id =" + Recordid + ")))";
                strSQL = "SELECT * FROM cc_clientdataset.Equivalency where ID IN(SELECT us_equivalency_id FROM cc_clientdataset.recom_selection WHERE (application_education_history_id =" + Recordid + "))";
                ds = GetDataSet(strSQL);
                option.DataSource = ds;
                option.DataTextField = "Name";
                option.DataValueField = "id";
                option.AppendDataBoundItems = true;
                option.Items.Add(new ListItem("Select", "0"));                
                option.DataBind();
                option.Items.Add(new ListItem("------------------------------------------------------", "0"));
                result = true;
            }
            return result;
        }

        #endregion

        #region Maintenance
        #region Country
        public static void Grid_SearchCountry(GridView commongrid, string data, string ClientId)
        {
            data = data.Replace("'", "''");
            //string strSQL = "SELECT CountryId,Name FROM countries where Name LIKE '" + data + "%' order by Name";
            string strSQL = " SELECT a.Id,a.Category as Type,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.countries as a where a.Id not in (SELECT c.Associated_Id from cc_relateddataset.countries as c where c.Customer_Id=" + ClientId + ") AND a.Name LIKE '" + data + "%'" +
                " union " + " SELECT a.Id,a.Category as Type,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.countries as a,cc_relateddataset.countries as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name LIKE '" + data + "%'" +
               " order by a.Name";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_CountrySelect(DetailsView commongrid, int country_id, string ClientId, string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select Id,Name,Description,'Description unavailable' as masterdesc from cc_masterdataset.countries where Id =" + country_id + " AND Customer_Id=" + ClientId;
                    break;
                default:
                    strSQL = " SELECT a.Id,a.Name,c.Description,a.Description as masterdesc FROM cc_masterdataset.countries as a,cc_relateddataset.countries as c where a.Id = c.Associated_Id AND a.Id=" + country_id + " AND c.Customer_Id=" + ClientId;
                   ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       
                    }
                    else
                    {
                        strSQL = "select a.Id,a.Name,'' as Description,a.Description as masterdesc  from cc_masterdataset.countries a where a.Id =" + country_id;                      
                    }
                    break;                  
            }
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
           

        }
        public static bool Grid_CountryUpdate(string country,int countryid, string des, string ClientId,string role)
        {

            string strSQL = "";
             DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.countries set Name='" + country.Replace("'", "''") + "',Description='" + des.Replace("'", "''") + "' where Id =" + countryid + " AND Customer_Id=" + ClientId;
                    break;
                default:                   
                    strSQL = "Select * from cc_relateddataset.countries where Associated_Id=" + countryid + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.countries SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + countryid + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.countries(Description,Associated_Id,IsEnabled,Customer_Id)VALUES('" + des.Replace("'", "''") + "'," + countryid + ",1," + ClientId + ")";
                    }
                    break;
            }       
         
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_CountryActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.countries set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.countries set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
        #endregion
        #region institution table
        public static void Grid_SearchInstitution(GridView commongrid, string data, string ClientId, string AdminId, string IsConfirm, string Type, string category,string Isdegreemill)
        {
            data = data.Replace("'", "''");
            string query = "";
            switch (category)
            {
                case "Name":
                    if (IsConfirm != "2")
                    {

                        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                    " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                    " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                    " order by a.Name ";
                    }
                    else
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                   " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                   " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                   " order by a.Name ";
                    }
                    break;
                case "Country":
                    if (IsConfirm != "2")
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                 " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                 " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                 " order by a.Name ";
                    }
                    else
        {
                        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                " order by a.Name ";
                    }

                    break;
            }

            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
        }     


        }
        public static bool Grid_InstitutionUpdate(string Name, int Country_ID, int Confirmed, string catType, int id, string ClientId,string des,string role,int mill)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.institution SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType.Replace("'", "''") + "',Description='" + des.Replace("'", "''") + "',IsDegreeMill =" + mill + "  WHERE (id =" + id + ")";
                    break;
                default:
                    strSQL = "Select * from cc_relateddataset.institution where Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.institution SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.institution (Description,Associated_Id,Customer_Id,IsEnabled)VALUES('" + des.Replace("'", "''") + "'," + id + "," + ClientId + ",1)";

                    }
                    break;
            }       
           
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_InstitutionSelect(DetailsView commongrid, int institution_id, string ClientId,string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,a.Description,'Description unavailable' as masterdesc,a.IsDegreeMill  from cc_masterdataset.institution a where a.id=" + institution_id;
                    break;
                default:
                    strSQL = "SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(c.Description,'')as Description,a.Description as masterdesc,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND a.Id =" + institution_id + " AND c.Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,'' as Description,a.Description as masterdesc,a.IsDegreeMill  from cc_masterdataset.institution a where a.id=" + institution_id;
                    }
                    break;
            }

            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_InstitutionAdd(string Name, int Country_ID, int Confirmed, string catType,string des,string ClientId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.institution(Name, Country_ID, Confirmed, Type,Description,Category,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + ",'" + catType.Replace("'", "''") + "','" + des.Replace("'", "''") + "','Client'," + ClientId + ")";          
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_InstitutionActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
             string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.institution set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.institution set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }
          
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_DegreeMill(string status, string Id)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_masterdataset.institution set IsDegreeMill=1 WHERE Id=" + Id;
                    break;

                case "1":
                    strSQL = "UPDATE cc_masterdataset.institution set IsDegreeMill=0 WHERE Id=" + Id;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
        #endregion
        #region degree
        public static void Grid_SearchDegree(GridView commongrid, string data, string ClientId, string AdminId, string IsConfirm, string Type)
        {
            data = data.Replace("'", "''");
            string query = "";

            switch (Type)
            {
                case "Name":
                    if (IsConfirm != "2")
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ") AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                        " order by a.Name ";
                    }
                    else
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ") AND a.Name like'" + data + "%'" +
                       " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                       " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                       " order by a.Name ";
                    }
                    break;
                case "Country":

                    if (IsConfirm != "2")
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c  where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
                        " order by a.Name ";
                    }
                    else
        {          
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%'" +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
             " order by a.Name ";
                    }

                    break;
            }
            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
        }

        }
        public static void Grid_DegreePlanSelect(DetailsView commongrid, int degree_id,string ClientId, string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,a.EquiDegree_id,a.Description,'Description unavailable' as masterdesc  from cc_masterdataset.degree a where a.id=" + degree_id;
                    break;
                default:
                    strSQL = "SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,a.EquiDegree_id,IsNull(c.Description,'')as Description,a.Description as masterdesc,c.IsEnabled FROM cc_masterdataset.degree as a,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND a.Id =" + degree_id + " AND c.Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,'' as Description,a.Description as masterdesc ,a.EquiDegree_id  from cc_masterdataset.degree a where a.id=" + degree_id;
                    }
                    break;
            }          
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_DegreePlanUpdate(string Name, int Country_ID, int Confirmed, string catType, int id, int EquiDegree_id,string ClientId,string des,string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.degree SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType + "',Description='" + des.Replace("'", "''") + "'  WHERE (id =" + id + ")";
                    break;
                default:
                    strSQL = "Select * from cc_relateddataset.degree where Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.degree SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.degree (Description,Associated_Id,Customer_Id,IsEnabled)VALUES('" + des.Replace("'", "''") + "'," + id + "," + ClientId + ",1)";

                    }
                    break;
            }                      
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DegreePlanAdd(string Name, int Country_ID, int Confirmed, string catType, int EquiDegree_id, string des, string ClientId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.degree(Name, Country_ID, Confirmed, Type,EquiDegree_id,Category,Description,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + ",'" + catType.Replace("'", "''") + "'," + EquiDegree_id + ",'Client','" + des.Replace("'", "''") + "'," + ClientId + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DegreeActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.degree set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.degree set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
       #endregion
        #region major
        public static void Grid_SearchMajor(GridView commongrid, string data, string ClientId, string AdminId, string IsConfirm, string Type)
        {
            data = data.Replace("'", "''");
            string query = "";
            switch (Type)
            {
                case "Name":
                    if (IsConfirm != "2")
                    {

                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                  " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                  " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm +
                  " order by a.Name ";
                    }
                    else
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" +
                " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                " order by a.Name ";
                    }
                    break;
                case "Country":
                    if (IsConfirm != "2")
                    {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
              " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
              " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
              " order by a.Name ";
                    }
                    else
        {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
             " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
             " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
              " order by a.Name ";
                    }


                    break;
            }

            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
        }
        }
        public static void Grid_MajorSelect(DetailsView commongrid, int Major_id, string ClientId, string role)
        {
              string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Confirmed,a.Equimajor_id,a.Description,'Description unavailable' as masterdesc  from cc_masterdataset.major  a where a.id=" + Major_id;
                    break;
                default:
                    strSQL = "SELECT a.Id,a.Name,a.Country_ID,a.Equimajor_id,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,IsNull(c.Description,'')as Description,a.Description as masterdesc,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND a.Id =" + Major_id + " AND c.Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Confirmed,a.Equimajor_id,'' as Description,a.Description as masterdesc  from cc_masterdataset.major  a where a.id=" + Major_id;
                    }
                    break;
            }            
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_MajorUpdate(string Name, int Country_ID, int Confirmed, int id, int EquiMajor_id, string ClientId, string des, string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.major SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ",Description='" + des.Replace("'", "''") + "'  WHERE (id =" + id + ")";
                    break;
                default:
                    strSQL = "Select * from cc_relateddataset.major where Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.major SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + id + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.major (Description,Associated_Id,Customer_Id,IsEnabled)VALUES('" + des.Replace("'", "''") + "'," + id + "," + ClientId + ",1)";

                    }
                    break;
            }      
           
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_MajorAdd(string Name, int Country_ID, int Confirmed, int EquiMajor_id,string des, string ClientId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.major(Name, Country_ID, Confirmed,EquiMajor_id,Category,Description,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + "," + EquiMajor_id + ",'Client','" + des.Replace("'", "''") + "'," + ClientId + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_MajorActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.major set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.major set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
        #endregion
        #region Documents Table
        public static void Grid_SearchDocuments(GridView commongrid, string data, string ClientId, string AdminId, string Type)
        {
            data = data.Replace("'", "''");
            string query = "";
            switch (Type)
        {
                case "Name":
                    query = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.Attachment FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.Documents as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" +
                " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.Attachment  FROM cc_masterdataset.Documents  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.Attachment   FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                " order by a.Name ";
                    break;
                case "Country":
                    query = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.Attachment  FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.Documents as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.Attachment  FROM cc_masterdataset.Documents  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.Attachment  FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                " order by a.Name ";

                    break;
            }

            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
            }

        }
        public static bool Grid_DocumentsAdd(string Name, int Country, string des, string ClientId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.Documents(Name, Country_ID,Description,Category, Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country + ",'" + des.Replace("'", "''") + "','Client'," + ClientId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_DocumentsSelect(DetailsView commongrid,int doc_id, string ClientId,string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,'Description unavailable' as masterdesc,a.Attachment  from cc_masterdataset.Documents a where a.id=" + doc_id;
                    break;
                default:
                    strSQL = "SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,IsNull(c.Description,'')as Description,a.Description as masterdesc,c.IsEnabled,a.Attachment FROM cc_masterdataset.Documents as a,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND a.Id =" + doc_id + " AND c.Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,'' as Description,a.Description as masterdesc,a.Attachment  from cc_masterdataset.Documents a where a.id=" + doc_id;
                    }
                    break;
            }        
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_DocumentsUpdate(string Name, int Country, string ClientId, int doc_id, string des, string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.Documents SET Name ='" + Name.Replace("'", "''") + "',Description='"+ des +"',Country_ID =" + Country + " WHERE  ID=" + doc_id;
                    break;
                default:
                    strSQL = "Select * from cc_relateddataset.Documents where Associated_Id=" + doc_id  + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.Documents SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + doc_id  + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.Documents (Description,Associated_Id,Customer_Id,IsEnabled)VALUES('" + des.Replace("'", "''") + "'," +doc_id  + "," + ClientId + ",1)";

                    }
                    break;
            }                  
          
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_DocumentsActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.Documents set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.Documents set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
     
        #endregion
        #region gradescale Table
        public static void Grid_SearchGradescale(GridView commongrid, string data, string ClientId, string AdminId, string Type)
        {
            data = data.Replace("'", "''");
            string query = "";
            switch (Type)
        {
                case "Name":
                    query = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.gradescales as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" +
                 " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.gradescales  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.gradescales as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                 " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                 " order by a.Name ";
                    break;
                case "Country":
                    query = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.gradescales as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                  " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.gradescales  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.gradescales as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                  " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like'" + data + "%'" +
                " order by a.Name ";
                    break;
            }
            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
            }

        }
        public static bool Grid_gradescaleAdd(string Name, int Country,string des, string ClientId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.gradescales(Name, Country_ID, Description,Category,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country + ",'" + des.Replace("'", "''") + "','Client'," + ClientId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_gradescaleSelect(DetailsView commongrid, int grade_id, string ClientId, string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,'Description unavailable' as masterdesc  from cc_masterdataset.gradescales a where a.id=" + grade_id;
                    break;
                default:
                    strSQL = "SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,IsNull(c.Description,'--')as Description,a.Description as masterdesc,c.IsEnabled FROM cc_masterdataset.gradescales as a ,cc_relateddataset.gradescales as c  where a.Id = c.Associated_Id AND a.Id=" + grade_id + " AND c.Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        strSQL = "select  a.id,a.Customer_Id,a.Name,a.Country_ID,'' as Description,a.Description as masterdesc  from cc_masterdataset.gradescales a where a.id=" + grade_id;
                    }
                    break;
            }        
          
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_gradescaleUpdate(string Name, int Country,string des,int grade_id, string ClientId,string role)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            switch (role)
            {
                case "Client":
                    strSQL ="UPDATE cc_masterdataset.gradescales SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country + ", Description ='" + des.Replace("'", "''") + "' WHERE  ID=" + grade_id;
                    break;
                default:
                    strSQL = "Select * from cc_relateddataset.gradescales where Associated_Id=" + grade_id + " AND Customer_Id=" + ClientId;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE cc_relateddataset.gradescales SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + grade_id + " AND Customer_Id=" + ClientId;
                    }
                    else
                    {
                        strSQL = "INSERT INTO cc_relateddataset.gradescales (Description,Associated_Id,Customer_Id,IsEnabled)VALUES('" + des.Replace("'", "''") + "'," + grade_id + "," + ClientId + ",1)";

                    }
                    break;
            }                 
           
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }

        public static bool Grid_gradescaleActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.gradescales set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.gradescales set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
        #endregion
        #region Source Table
        public static void Grid_SearchSource(GridView commongrid, string data, string ClientId, string AdminId, string Type)
        {
            data = data.Replace("'", "''");
            string query = "";
            switch (Type)
        {
                case "Name":
                    query = " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.ID not in (SELECT c.Associated_Id from cc_relateddataset.source as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" +
              " union " + " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id,cc_relateddataset.source as c  where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
              " order by a.Name ";
                    break;
                case "Country":
                    query = " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.ID not in (SELECT c.Associated_Id from cc_relateddataset.source as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
              " union " + " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id,cc_relateddataset.source as c  where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
              " order by a.Name ";

                    break;
            }

            if (query != "")
            {
                DataSet ds1 = GetDataSet(query);
                commongrid.DataSource = ds1;
            commongrid.DataBind();
            }

        }
        public static void Grid_SourceSelect(DetailsView commongrid, int source_id, string ClientId)
        {
            string strSQL = "";
            DataSet ds = new DataSet();

            strSQL = " SELECT a.ID,a.CountryId,a.Customer_Id,a.Name,c.Description,c.IsEnabled,a.Description as masterdesc FROM cc_masterdataset.source as a,cc_relateddataset.source as c where a.Id = c.Associated_Id AND a.Id=" + source_id + " AND c.Customer_Id=" + ClientId;
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                strSQL = "select a.Id,a.CountryId,a.Customer_Id,a.Name,'' as Description,a.Description as masterdesc from cc_masterdataset.source a where a.Id =" + source_id;
            }                 
            
            ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();           
           

        }
        public static bool Grid_SourceUpdate(string Name, string des, int Country, string ClientId, int source_id)
        {
            string strSQL = "";
            DataSet ds = new DataSet();              
              
            strSQL = "Select * from cc_relateddataset.source where Associated_Id=" + source_id  + " AND Customer_Id=" + ClientId;
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = "UPDATE cc_relateddataset.source SET Description='" + des.Replace("'", "''") + "' WHERE Associated_Id=" + source_id + " AND Customer_Id=" + ClientId;
            }
            else
            {
                strSQL = "INSERT INTO cc_relateddataset.source (Description,Associated_Id,IsEnabled,Customer_Id)VALUES('" + des.Replace("'", "''") + "'," +source_id + ",1," + ClientId + ")";
            }
            bool result = GetDataSet_withoutID(strSQL);
            return result;           
        }
        public static bool Grid_SourceActiveDesc(string status, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "";
            switch (status)
            {
                case "0":
                    strSQL = "UPDATE cc_relateddataset.source set IsEnabled=1 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

                case "1":
                    strSQL = "UPDATE cc_relateddataset.source set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
                    break;

            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
   
        #endregion
        #region Equivalency Table
        public static void Grid_SearchEquivalency(GridView commongrid, string data, string ClientId)
        {
            data = data.Replace("'", "''");
            string query = "SELECT ID, Name, Description, Customer_Id FROM cc_clientdataset.Equivalency WHERE ((Name like'" + data + "%') AND (Customer_Id=" + ClientId + ")) order by Name";
            DataSet ds = GetDataSet(query);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_EquivalencyAdd(string Name, string des, string ClientId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_clientdataset.Equivalency(Name, Description, Customer_Id) VALUES ('" + Name.Replace("'", "''") + "','" + des.Replace("'", "''") + "'," + ClientId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_EquivalencySelect(DetailsView commongrid, int source_id)
        {
            string strSQL = "SELECT * FROM cc_clientdataset.Equivalency WHERE ID =" + source_id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_EquivalencyUpdate(string Name, string des, string ClientId, int source_id)
        {
            string strSQL = "UPDATE cc_clientdataset.Equivalency SET Name ='" + Name.Replace("'", "''") + "', Description ='" + des.Replace("'", "''") + "' WHERE  ID=" + source_id + " AND Customer_Id =" + ClientId;
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        #endregion
        #endregion

        #region Settings
        #region CustomerAdons Table
        public static void DetailsView_CustomerAdonsBrowse(DetailsView commongrid, string ClientId)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.customersettings where Customer_Id=" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_CustomerAdonsAdd(int dlcount, string msg1, string msg2, string ClientId, string url, string toc, int creditcard, string login, string transkey, string mailid, string thk, string credittype, string siteurl)
        {
            bool result = false;
            string strSQL = "SELECT * FROM cc_masterdataset.customersettings where Customer_Id=" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = "UPDATE cc_masterdataset.customersettings SET  Delivery_copy =" + dlcount + ",Delivery_Instructions='" + msg1.Replace("'", "''") + "',Education_Instructions='" + msg2.Replace("'", "''") + "',Document_Instructions='" + url.Replace("'", "''") + "',Terms_And_Condition='" + toc.Replace("'", "''") + "',CreditCard=" + creditcard + ",LoginId='" + login + "',Transkey='" + transkey + "',Email='" + mailid + "',ThkuPage=" + thk + ",SiteUrl='" + siteurl + "',Credit_Type='" + credittype + "'  where Customer_Id=" + ClientId;
                result = GetDataSet_withoutID(strSQL);
            }
            else
            {

                strSQL = "INSERT INTO cc_masterdataset.customersettings(Customer_Id,Delivery_copy, Delivery_Instructions, Education_Instructions,Document_Instructions,Terms_And_Condition,CreditCard,LoginId,Transkey,Email,ThkuPage,SiteUrl,Credit_Type) VALUES (" + ClientId + "," + dlcount + ",'" + msg1.Replace("'", "''") + "','" + msg2.Replace("'", "''") + "','" + url.Replace("'", "''") + "','" + toc.Replace("'", "''") + "'," + creditcard + ",'" + login + "','" + transkey + "','" + mailid + "," + thk + ",'" + siteurl + "','" + credittype + "')";
                result = GetDataSet_withoutID(strSQL);
            }
            return result;

        }
        public static DataSet Grid_CustomerAdonsSelect(DetailsView commongrid, string ClientId)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.customersettings where Customer_Id=" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
            return ds;

        }

        public static DataSet Grid_Creditcard(string ClientId)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.customersettings where Customer_Id=" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            return ds;

        }
        public static bool Grid_CustomerAdonsUpdate(int dlcount, string msg1, string msg2, string ClientId, string url, string toc, int creditcard, string login, string transkey, string mailid, string thk, string credittype, string siteurl, string Additional, string fax, string email, string tdb, string Purposesection, string purposeid, string targetname, string state, string splinst, string complinst, string upload, string app_type, string Targetsection, int Onlinecc, int moneyorder, int personalcheck, string creditcardinst, string moneyorderinst, string personalcheckinst)
        {
            string strSQL = "UPDATE cc_masterdataset.customersettings SET  Delivery_copy =" + dlcount + " , Delivery_Instructions='" + msg1.Replace("'", "''") + "',Education_Instructions='" + msg2.Replace("'", "''") + "',Document_Instructions='" + url.Replace("'", "''") + "',Terms_And_Condition='" + toc.Replace("'", "''") + "',CreditCard=" + creditcard + ",LoginId='" + login.Replace("'", "''") + "',Transkey='" + transkey.Replace("'", "''") + "',Email='" + mailid.Replace("'", "''") + "',ThkuPage=" + thk.Replace("'", "''") + ",SiteUrl='" + siteurl.Replace("'", "''") + "',Credit_Type='" + credittype.Replace("'", "''") + "',Additional_Section='" + Additional.Replace("'", "''") + "',Fax_Section='" + fax.Replace("'", "''") + "',Email_Section='" + email.Replace("'", "''") + "',Talent_Database='" + tdb.Replace("'", "''") + "',Purpose_Section='" + Purposesection.Replace("'", "''") + "',Lock_PurposeId=" + purposeid.Replace("'", "''") + ",Lock_TargetName='" + targetname.Replace("'", "''") + "',Lock_State='" + state.Replace("'", "''") + "',Spl_Instruction='" + splinst.Replace("'", "''") + "',Completed_Instruction='" + complinst.Replace("'", "''") + "',Applicant_Upload='" + upload.Replace("'", "''") + "',Application_Type=" + app_type.Replace("'", "''") + ",Target_Section='" + Targetsection.Replace("'", "''") + "',Creditcard_Instructions='" + creditcardinst.Replace("'", "''") + "',Onlinecc=" + Onlinecc + ",Moneyorder=" + moneyorder + ",Moneyorder_Instructions='" + moneyorderinst.Replace("'", "''") + "',Personalcheck=" + personalcheck + ",Personalcheck_Instructions='" + personalcheckinst.Replace("'", "''") + "' where Customer_Id=" + ClientId;          
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }

        #region payment encryptor
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

        #region Creditcard Type
        public static void Getcard_type(CheckBoxList options)
        {
            string strSQL = "select Card_Type from cc_clientdataset.Card_Type";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Card_Type";
            options.DataValueField = "Card_Type";
            options.DataBind();

        }
        #endregion
        #endregion
        #region Splash-page
        public static void Splashpage_Browse(DetailsView commongrid, string ClientId)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.splashpagesettings where Customer_Id=" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }

        public static bool Splashpage_Update(string Browsermsg, string Appmsg, string clientmsg, int offlineapp, string offlineappmsg, string ClientId,int onlineapp)
        {

            string strSQL = "UPDATE cc_masterdataset.splashpagesettings SET  BrowserInstructions ='" + Browsermsg.Replace("'", "''") + "',AppInstructions='" + Appmsg.Replace("'", "''") + "',ClientInstructions='" + clientmsg.Replace("'", "''") + "',OfflineApp=" + offlineapp + ",OfflineAppInstructions='" + offlineappmsg.Replace("'", "''") + "',OnlineApp="+ onlineapp +"  where Customer_Id=" + ClientId;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }

        #endregion


        #region Delivery type
        public static void Grid_DeliveryTypeBrowse(GridView commongrid, string ClientId)
        {
            string strSQL = "SELECT id, Name, Cost,priority FROM cc_clientdataset.delivery_type WHERE Customer_Id =" + ClientId + " order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_DeliveryTypeSelect(DetailsView commongrid, int id)
        {
            string strSQL = "SELECT id, Name, Cost,Type,Description,Customer_Id FROM cc_clientdataset.delivery_type where id=" + id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_DeliveryTypeUpdate(string Name, int Cost, string cattype, int id, string des)
        {
            string strSQL = "UPDATE cc_clientdataset.delivery_type SET Name ='" + Name.Replace("'", "''") + "', Cost =" + Cost + ", Type='" + cattype.Replace("'", "''") + "',Description ='" + des.Replace("'", "''") + "' WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DeliveryTypeAdd(string Name, int Cost, string type, string ClientId, string des)
        {
            string strSQL = "SELECT max(priority)as priority FROM cc_clientdataset.delivery_type WHERE Customer_Id =" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["priority"].ToString() != "")
                {
                    int priority = Convert.ToInt32(ds.Tables[0].Rows[0]["priority"].ToString());
                    priority = priority + 1;
                    strSQL = "INSERT INTO cc_clientdataset.delivery_type(Name, Cost,Type,customer_Id,priority,Description) VALUES ('" + Name.Replace("'", "''") + "'," + Cost + ",'" + type.Replace("'", "''") + "'," + ClientId + "," + priority + ",'" + des.Replace("'", "''") + "')";
                }
                else
                {
                    strSQL = "INSERT INTO cc_clientdataset.delivery_type(Name, Cost,Type,customer_Id,priority,Description) VALUES ('" + Name.Replace("'", "''") + "'," + Cost + ",'" + type.Replace("'", "''") + "'," + ClientId + ",1,'" + des.Replace("'", "''") + "')";
                }
            }
            //else
            //{
            //    strSQL = "INSERT INTO cc_clientdataset.delivery_type(Name, Cost,Type,customer_Id,priority) VALUES ('" + Name.Replace("'", "''") + "'," + Cost + ",'" + type + "'," + ClientId + ",1)";
            //}           
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DeliveryTypeOrderUpdate(string id, string order)
        {
            string strSQL = "UPDATE  cc_clientdataset.delivery_type SET priority =" + order + " WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Delivery(string id)
        {
            string strSQL = "delete from cc_clientdataset.delivery_type where Id=" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        #endregion
        #region service type
        public static void Grid_ServiceTypeBrowse(GridView commongrid, string ClientId)
        {
            string strSQL = "SELECT id, Name,Description, Cost,priority FROM cc_clientdataset.service WHERE Customer_Id =" + ClientId + " order by priority";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_ServiceTypeSelect(DetailsView commongrid, int service_id)
        {
            string strSQL = "SELECT id, Name,Description, Cost,Type,Customer_Id FROM cc_clientdataset.service where id=" + service_id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_ServiceTypeUpdate(string Name, int Cost, string cattype, int id, string desc)
        {
            string strSQL = "UPDATE cc_clientdataset.service SET Name ='" + Name.Replace("'", "''") + "', Cost =" + Cost + ", Type='" + cattype.Replace("'", "''") + "',Description='" + desc.Replace("'", "''") + "' WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_ServiceOrderUpdate(string id, string order)
        {
            string strSQL = "UPDATE cc_clientdataset.service SET priority =" + order + " WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_ServiceTypeAdd(string Name, int Cost, string type, string ClientId, string desc)
        {
            string strSQL = "SELECT max(priority)as priority FROM cc_clientdataset.service WHERE Customer_Id =" + ClientId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["priority"].ToString() != "")
                {
                    int priority = Convert.ToInt32(ds.Tables[0].Rows[0]["priority"].ToString());
                    priority = priority + 1;
                    strSQL = "INSERT INTO cc_clientdataset.service(Name, Cost,Type,customer_Id,Description,priority) VALUES ('" + Name.Replace("'", "''") + "'," + Cost + ",'" + type.Replace("'", "''") + "'," + ClientId + ",'" + desc.Replace("'", "''") + "'," + priority + ")";
                }
                else
                {
                    strSQL = "INSERT INTO cc_clientdataset.service(Name, Cost,Type,customer_Id,Description,priority) VALUES ('" + Name.Replace("'", "''") + "'," + Cost + ",'" + type.Replace("'", "''") + "'," + ClientId + ",'" + desc.Replace("'", "''") + "',1)";
                }
            }
            //else
            //{
            //    strSQL = "INSERT INTO cc_clientdataset.service(Name, Cost,Type,customer_Id,Description,priority) VALUES ('" + Name + "'," + Cost + ",'" + type + "'," + ClientId + ",'" + desc + "',1)";
            //}


            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Service(string id)
        {
            string strSQL = "delete from [cc_clientdataset].[service] where Id=" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        #endregion
#endregion

        #region Client Accounts
        
        #region clients      
        public static DataSet Getclientlist(string clientId)
        {
            string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where id="+ clientId +" AND Parent ='SELF' ";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet Getsubclientlist(string Parentdomain)
        {
            string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where Parent ='" + Parentdomain + "'";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static void Grid_clientSelect(DetailsView commongrid, string Client_id)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.customer where id=" + Client_id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }

        }
        public static bool Grid_clientUpdate(string Name, string Address, string City, string State, string Zipcode, string DomainName, string parent, int id)
        {
            string strSQL = "UPDATE [cc_masterdataset].[customer] SET Name='" + Name.Replace("'", "''") + "',Address='" + Address.Replace("'", "''") + "',City='" + City.Replace("'", "''") + "',State='" + State.Replace("'", "''") + "',ZipCode='" + Zipcode + "',SubDomainName='" + DomainName.Replace("'", "''") + "',Parent='" + parent + "' where  id=" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void dpclient(DropDownList option, string clientId,bool append)
        {
            string strSQL = "SELECT Name,SubDomainName FROM cc_masterdataset.customer where id=" + clientId;
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "SubDomainName";
            option.AppendDataBoundItems = append;
            option.Items.Add(new ListItem("Domain", "SELF"));
            option.DataBind();

        }
        public static bool Grid_clientAdd(string Name, string Address, string City, string State, string Zipcode, string Subdomain, string Parent)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.customer ([Name],[Address],[City],[State],[ZipCode],[SubDomainName],[Parent]) VALUES ('" + Name.Replace("'", "''") + "','" + Address + "','" + City + "','" + State + "','" + Zipcode + "','" + Subdomain + "','" + Parent + "')";
            int id = GetDataSet_withID(strSQL);
            if (id != 0)
            {
                //strSQL = "INSERT INTO [cc_masterdataset].[customersettings]([Customer_Id],[Delivery_copy],[Delivery_Instructions],[Education_Instructions],[Document_Instructions],[Terms_And_Condition],[CreditCard],[LoginId],[Transkey],[Email],[ThkuPage],[SiteUrl],[Credit_Type]) VALUES(" + id + ",0,'','','','',0,'','','',0,'','')";
                //result = GetDataSet_withoutID(strSQL);
                //if (result)
                //{
                //    strSQL = "INSERT INTO [cc_masterdataset].[splashpagesettings]([Customer_Id],[BrowserInstructions],[AppInstructions])select " + id + " as [Customer_Id],[BrowserInstructions],[AppInstructions] from [cc_masterdataset].[splashpagesettings] where [Customer_Id]=0";
                //    result = GetDataSet_withoutID(strSQL);

                //}

                SqlConnection con = new SqlConnection(DBConnectionString());
                con.Open();
                SqlCommand cmdd = new SqlCommand("CreateClient", con);
                cmdd.CommandType = CommandType.StoredProcedure;
                cmdd.CommandTimeout = 0;

                cmdd.Parameters.Add("@Subdomain", SqlDbType.VarChar, 500).Value = Parent; 
                cmdd.Parameters.Add("@ClientId", SqlDbType.Int).Value = id;

                SqlParameter spresult = new SqlParameter("@output", SqlDbType.VarChar, 50);
                spresult.Direction = ParameterDirection.Output;
                cmdd.Parameters.Add(spresult);

                SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, 50000);
                errormsg.Direction = ParameterDirection.Output;
                cmdd.Parameters.Add(errormsg);

                cmdd.ExecuteNonQuery();
                con.Close();

                if (spresult.Value.ToString() == "Successful")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }


            return result;

        }
        public static bool DomainName_Exist(string domainname)
        {
            bool result = false;
            string strSQL = "SELECT Name,SubDomainName FROM cc_masterdataset.customer where SubDomainName='" + domainname +"'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = true;
            }

            return result;
         
        }
        #endregion

        #region Employee                  
        public static void Getclient(DropDownList option, string clientid)
        {
            string strSQL = "SELECT Name,id FROM cc_masterdataset.customer where id=" + clientid;
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "id";          
            option.DataBind();

        }
        public static bool Grid_LoginAdd(int Customer_Id, string Name, string Password)
        {
            string strSQL = "INSERT INTO cc_relateddataset.login(Customer_Id, Name, Password,Role) VALUES (" + Customer_Id + ",'" + Name.Replace("'", "''") + "','" + Password.Replace("'", "''") + "','EMPLOYEE')";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_LoginBrowse(GridView commongrid, int customer_Id)
        {
            string strSQL = "SELECT * FROM  cc_relateddataset.login WHERE Customer_Id =" + customer_Id + " AND Role='EMPLOYEE' order by Name";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_LoginSelect(DetailsView commongrid, int Employee_id)
        {
            string strSQL = "SELECT * FROM cc_relateddataset.login where id=" + Employee_id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_LoginUpdate(int Customer_Id, string Name, string Password, int id)
        {
            string strSQL = "UPDATE cc_relateddataset.login SET Customer_Id=" + Customer_Id + ", Name='" + Name.Replace("'", "''") + "',Password='" + Password.Replace("'", "''") + "',Role='EMPLOYEE' WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        #endregion
        #endregion      

     
        #endregion      

        #region public applicant view(client based)
        public static string check_filenumber(string filenumber, string customerId)
        {
            string result = "Access_Denied";
            string strSQL = "SELECT * FROM cc_clientdataset.applicant WHERE FileNumber ='" + filenumber.Replace("'", "''") + "'and Customer_Id=" + customerId;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = "Access";
            }
            else
            {
                result = "Access_Denied";
            }

            return result;
        }
        public static string check_filenumberdomain(string filenumber, string customerId)
        {
            string result = "Access_Denied";
            string strSQL = "SELECT * FROM cc_clientdataset.applicant WHERE FileNumber ='" + filenumber.Replace("'", "''") + "' and Customer_Id in(select id from [cc_masterdataset].[customer] where SubDomainName=(Select SubDomainName from [cc_masterdataset].[customer] where id =" + customerId + ") or Parent=(Select SubDomainName from [cc_masterdataset].[customer] where id =" + customerId + "))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = "Access";
            }
            else
            {
                result = "Access_Denied";
            }

            return result;
        }
        public static DataSet Status_View(string FileNumber)
        {
            string strSQL = "SELECT cc_clientdataset.evaluation_request.Application_Recieved, cc_clientdataset.evaluation_request.Documents_Recieved, cc_clientdataset.evaluation_request.Payment_Recieved, cc_clientdataset.evaluation_request.Evaluation_Complete, cc_clientdataset.evaluation_request.Verification_Complete, cc_clientdataset.evaluation_request.Evaluation_Approved, cc_clientdataset.evaluation_request.Packaging_Complete, cc_clientdataset.evaluation_request.Delievery_Complete,cc_clientdataset.evaluation_request.Applicant_Note, cc_clientdataset.evaluation_purpose.Evaluation_Name,cc_clientdataset.applicant.FirstName,cc_clientdataset.applicant.LastName,cc_clientdataset.applicant.DateOfBirth,cc_clientdataset.applicant.FileNumber FROM (((cc_clientdataset.applicant INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.Id) INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id) INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id) WHERE cc_clientdataset.applicant.FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        #endregion

        #region Template
        public static void Get_Internal(TextBox fileno,string Filenumber)
        {
            string strSQL = "SELECT cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name,cc_clientdataset.applicant.InternalFileNumber FROM cc_clientdataset.applicant where FileNumber='" + Filenumber + "'";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fileno.Text = ds.Tables[0].Rows[0]["InternalFileNumber"].ToString();            

            }
        }
        public static int EvaluationCount(string FileNumber)
        {
            int count = -1;
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Request_Id = ds.Tables[0].Rows[0]["id"].ToString();
                strSQL = " SELECT cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Name,'0' as major, cc_masterdataset.degree.Id as eduid, cc_masterdataset.degree.Name AS Expr1,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage ,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')" +
             " union " + " SELECT cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Name,cc_masterdataset.major.Name as major, cc_masterdataset.degree.Id  as eduid, cc_masterdataset.degree.Name AS Expr1,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, cc_clientdataset.applicant_education_history.DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_masterdataset.major  ON cc_clientdataset.applicant_education_history.Major_Id  = cc_masterdataset.major.Id WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University') order by cc_clientdataset.applicant_education_history.StartDate";
                ds = GetDataSet(strSQL);
                count = ds.Tables[0].Rows.Count; 
            }
            else
            {
                count = 0;
            }

            return count;
          
        }
        public static void LoadTemplate(DropDownList loader,ArrayList list,bool type,string folder)
        {
            if (type)
        {
           loader.Items.Add(new ListItem("Select", "0"));
            }
           if (list.Count > 0)
           {
               for (int i = 0; i <= list.Count - 1; i++)
               {
                   string[] value = list[i].ToString().Split('.');
                   loader.Items.Add(new ListItem(value[0].ToString() , value[0].ToString()+"|"+ folder));
               }
           }
        }
        public static bool TemplateName(string name, ArrayList list)
        {
            bool result = false;
            if (list.Count > 0)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    string[] temp = list[i].ToString().Split('.');
                    string check = temp[0].ToString();
                   if (check.ToLower()  == name.ToLower())
                   {
                       result = true;
                       break;
                   }
                   else
                   {
                       result = false;
                      
                   }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        public static DataSet Generaldata(string FileNumber)
        {
            //string strSQL = "SELECT cc_clientdataset.applicant.FirstName as fname, cc_clientdataset.applicant.MiddleName as mname, cc_clientdataset.applicant.LastName as lname,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.Gender, cc_clientdataset.applicant.otherFirstName as ofname,cc_clientdataset.applicant.otherMiddleName as omname,cc_clientdataset.applicant.otherLastName AS olname, CONVERT( varchar, cc_clientdataset.applicant.DateOfBirth , 101) as DateOfBirth,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Primary_Address_Name, cc_clientdataset.applicant.Addressline1+' '+ cc_clientdataset.applicant.Addressline2 AS Primary_Address_StreetName, cc_clientdataset.applicant.City AS Primary_Address_City,cc_clientdataset.applicant.State_or_province AS Primary_Address_State_Province,countries.Name AS country,countries_1.Name AS Primary_Address_Country,cc_clientdataset.applicant.Zip_or_PostalCode AS Primary_Address_Zip,cc_clientdataset.applicant.FileNumber as filenumber, countries.Name AS country,countries_1.Name AS birthcountry, cc_clientdataset.applicant.Paymentmode,cc_clientdataset.applicant.Paymentstatus,cc_clientdataset.applicant.Authorizecode,cc_clientdataset.applicant.Transactioncode, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.evaluation_purpose.Evaluation_Name,cc_clientdataset.evaluation_request.Eval_institution,cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id INNER JOIN cc_masterdataset.countries countries_1 ON cc_clientdataset.applicant.Countryofbirth = countries_1.ID where cc_clientdataset.applicant.FileNumber='" + FileNumber + "'";
            string strSQL = "SELECT cc_clientdataset.applicant.FirstName as fname, cc_clientdataset.applicant.MiddleName as mname, cc_clientdataset.applicant.LastName as lname,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.Gender, cc_clientdataset.applicant.otherFirstName as ofname,cc_clientdataset.applicant.otherMiddleName as omname,cc_clientdataset.applicant.otherLastName AS olname, CONVERT( varchar, cc_clientdataset.applicant.DateOfBirth , 101) as DateOfBirth,cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Primary_Address_Name, cc_clientdataset.applicant.Addressline1+' '+ cc_clientdataset.applicant.Addressline2 AS Primary_Address_StreetName, cc_clientdataset.applicant.City AS Primary_Address_City,cc_clientdataset.applicant.State_or_province AS Primary_Address_State_Province,countries.Name AS Primary_Address_Country,cc_clientdataset.applicant.Zip_or_PostalCode AS Primary_Address_Zip,cc_clientdataset.applicant.FileNumber as filenumber, countries.Name AS country,countries_1.Name AS birthcountry, cc_clientdataset.applicant.Paymentmode,cc_clientdataset.applicant.Paymentstatus,cc_clientdataset.applicant.Authorizecode,cc_clientdataset.applicant.Transactioncode, cc_clientdataset.applicant.HomePhone, cc_clientdataset.applicant.WorkPhone, cc_clientdataset.applicant.MobilePhone, cc_clientdataset.applicant.Email, cc_clientdataset.evaluation_purpose.Evaluation_Name,cc_clientdataset.evaluation_request.Eval_institution,cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant.CountryId = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.evaluation_purpose ON cc_clientdataset.evaluation_request.Purpose_Id = cc_clientdataset.evaluation_purpose.id INNER JOIN cc_masterdataset.countries countries_1 ON cc_clientdataset.applicant.Countryofbirth = countries_1.ID where cc_clientdataset.applicant.FileNumber='" + FileNumber + "'";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet Edudata(string FileNumber)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Request_Id = ds.Tables[0].Rows[0]["id"].ToString();
                strSQL = " SELECT cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Id as InsId,cc_masterdataset.institution.Name,'0' as major, cc_masterdataset.degree.Id as eduid, cc_masterdataset.degree.Name AS degree,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage ,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, right(cc_clientdataset.applicant_education_history.DateDegreeAwarded,4) as DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'Highschool') AND (cc_masterdataset.degree.Type = 'Highschool')" +
             " union " + " SELECT cc_clientdataset.applicant_education_history.id,cc_masterdataset.institution.Id as InsId,cc_masterdataset.institution.Name,cc_masterdataset.major.Name as major, cc_masterdataset.degree.Id  as eduid, cc_masterdataset.degree.Name AS degree,IsNull(cc_clientdataset.applicant_education_history.Linkage,0) as Linkage,cc_clientdataset.applicant_education_history.StartDate, cc_clientdataset.applicant_education_history.EndDate, cc_clientdataset.applicant_education_history.id,cc_masterdataset.countries.Id as Cid, cc_masterdataset.countries.Name AS Country, right(cc_clientdataset.applicant_education_history.DateDegreeAwarded,4) as DateDegreeAwarded FROM cc_clientdataset.applicant_education_history INNER JOIN cc_masterdataset.institution ON cc_clientdataset.applicant_education_history.EducationInstitution_Id = cc_masterdataset.institution.id INNER JOIN cc_masterdataset.degree ON cc_clientdataset.applicant_education_history.Degree_Id = cc_masterdataset.degree.id INNER JOIN cc_masterdataset.countries ON cc_clientdataset.applicant_education_history.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_masterdataset.major  ON cc_clientdataset.applicant_education_history.Major_Id  = cc_masterdataset.major.Id WHERE (cc_clientdataset.applicant_education_history.Evaluation_Request_Id =" + Request_Id + ") AND (cc_masterdataset.institution.Type = 'University') AND (cc_masterdataset.degree.Type = 'University') order by cc_clientdataset.applicant_education_history.StartDate";
            }
            else
            {
                strSQL = " SELECT '' as Name,'' as eduid, '' as degree, '' as StartDate,'' as EndDate, '' as id, '' as Country, '' as DateDegreeAwarded ";
            }

            ds = GetDataSet(strSQL);

            return ds;
        }
        public static DataSet deliverydata(string FileNumber)
        {
             string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT cc_clientdataset.evaluation_request.id FROM cc_clientdataset.applicant INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant.id = cc_clientdataset.evaluation_request.Applicant_Id WHERE (cc_clientdataset.applicant.FileNumber ='" + FileNumber + "')";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Request_Id = ds.Tables[0].Rows[0]["id"].ToString();
                strSQL = "SELECT cc_clientdataset.evaluation_delivery.Optional_InstitutionName,cc_clientdataset.evaluation_delivery.Count,cc_clientdataset.evaluation_delivery.CopyNo,cc_clientdataset.delivery_type.Name as deliveryservice,cc_clientdataset.evaluation_delivery.Name, cc_clientdataset.evaluation_delivery.Addressline1+' '+cc_clientdataset.evaluation_delivery.Addressline2 AS StreetName, cc_clientdataset.evaluation_delivery.City, cc_clientdataset.evaluation_delivery.State_or_Province, cc_clientdataset.evaluation_delivery.Zip_or_PostalCode, cc_masterdataset.countries.Name AS Country, cc_clientdataset.evaluation_delivery.Type FROM cc_clientdataset.evaluation_delivery INNER JOIN cc_masterdataset.countries ON cc_clientdataset.evaluation_delivery.Country_Id = cc_masterdataset.countries.ID INNER JOIN cc_clientdataset.delivery_type on cc_clientdataset.evaluation_delivery.Delivery_Type_Id = cc_clientdataset.delivery_type.id  where ((cc_clientdataset.evaluation_delivery.Evaluation_Request_Id = " + Request_Id + ")AND(cc_clientdataset.evaluation_delivery.Type <> 'Fax'))";
                ds = GetDataSet(strSQL);
            }
            return ds;
        }

        public static DataSet Clientinfodata(string clientId)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "SELECT Name AS Domain_Account_Address_Name,Address AS Domain_Account_Address_StreetName,City AS Domain_Account_Address_City,State AS Domain_Account_Address_State_Province,ZipCode AS Domain_Account_Address_Zip FROM [cc_masterdataset].[customer] where id ='"+clientId+"'";
            ds = GetDataSet(strSQL);
            
            return ds;
        }

        public static string GetDescription(string type,string Id,string client)
        {
            string strSQL = "";
            string des="";
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            switch (type)
            {
                case "degree":
                 
                   
                    strSQL = "SELECT * FROM cc_masterdataset.degree where Id =" + Id;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Category"].ToString() != "Client")
                        {
                            strSQL = "SELECT * FROM cc_relateddataset.degree where Associated_Id =" + Id + "AND Customer_Id=" + client + "AND IsEnabled=1";
                            ds1 = GetDataSet(strSQL);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                des = ds1.Tables[0].Rows[0]["Description"].ToString();
                            }
                            else { des = ds.Tables[0].Rows[0]["Description"].ToString(); }
                        }
                        else { des = ds.Tables[0].Rows[0]["Description"].ToString(); }

                    }
                    break;
                case "Institution":
                    strSQL = "SELECT * FROM cc_masterdataset.institution where Id =" + Id;
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Category"].ToString() != "Client")
                        {
                            strSQL = "SELECT * FROM cc_relateddataset.institution where Associated_Id =" + Id + "AND Customer_Id=" + client + "AND IsEnabled=1";
                            ds1 = GetDataSet(strSQL);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                des = ds1.Tables[0].Rows[0]["Description"].ToString();
                            }
                            else { des = ds.Tables[0].Rows[0]["Description"].ToString(); }
                        }
                        else { des = ds.Tables[0].Rows[0]["Description"].ToString(); }

                    }
                    break;
                case "equi":
                    strSQL = " SELECT cc_masterdataset.gradescales.Name AS gradename, cc_masterdataset.gradescales.Description AS gradedes, cc_clientdataset.Equivalency.Name AS equiname,cc_clientdataset.Equivalency.Description AS equides FROM cc_masterdataset.Linkage left outer join " +
                          " cc_masterdataset.gradescales ON cc_masterdataset.Linkage.Gradescale_Id = cc_masterdataset.gradescales.ID left outer join " +
                          " cc_clientdataset.Equivalency ON cc_masterdataset.Linkage.Equivalency_Id = cc_clientdataset.Equivalency.ID " + "WHERE  (cc_masterdataset.Linkage.Id =" + Id + ")";
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        des = ds.Tables[0].Rows[0]["equides"].ToString(); 
                    }
                    break;
                case "grade":
                    strSQL = " SELECT cc_masterdataset.gradescales.Name AS gradename, cc_masterdataset.gradescales.Description AS gradedes, cc_clientdataset.Equivalency.Name AS equiname,cc_clientdataset.Equivalency.Description AS equides FROM cc_masterdataset.Linkage left outer join " +
                        " cc_masterdataset.gradescales ON cc_masterdataset.Linkage.Gradescale_Id = cc_masterdataset.gradescales.ID left outer join " +
                        " cc_clientdataset.Equivalency ON cc_masterdataset.Linkage.Equivalency_Id = cc_clientdataset.Equivalency.ID " + "WHERE  (cc_masterdataset.Linkage.Id =" + Id + ")";
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        des = ds.Tables[0].Rows[0]["gradedes"].ToString();
                    }
                    break;
                case "Issued_GPA":
                    strSQL = " SELECT [Issued_GPA] FROM [cc_masterdataset].[Linkage] WHERE  (cc_masterdataset.Linkage.Id =" + Id + ")";
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        des = ds.Tables[0].Rows[0]["Issued_GPA"].ToString();
                    }
                    break;
                case "Converted_GPA":
                    strSQL = " SELECT [Converted_GPA] FROM [cc_masterdataset].[Linkage] WHERE  (cc_masterdataset.Linkage.Id =" + Id + ")";
                    ds = GetDataSet(strSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        des = ds.Tables[0].Rows[0]["Converted_GPA"].ToString();
                    }
                    break;

            }
            
            des = System.Text.RegularExpressions.Regex.Replace(HttpContext.Current.Server.HtmlDecode(des), @"<[^>]*>", string.Empty).Replace("&nbsp;", "");
            return des;
        }
          public static string Getpurpose(string FileNumber)
        {
            string purpose = "";
            string strSQL = "";
            DataSet ds = new DataSet();
            strSQL = "  Select a.Evaluation_Name from cc_clientdataset.evaluation_purpose a where a.id =(select b.Purpose_Id from cc_clientdataset.evaluation_request b where b.Applicant_Id =(select c.id from cc_clientdataset.applicant c where c.FileNumber ='" + FileNumber + "'))";
            ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                purpose = ds.Tables[0].Rows[0]["Evaluation_Name"].ToString();
            }
            return purpose;
        }
     
        #endregion


        #region get things from filenumber
        public static string clientidbyFilenumber(string fileno)
        {
            string clientid = "0";
            string query = "Select Customer_Id from [cc_clientdataset].[applicant] where [FileNumber]='" + fileno + "'";
            DataSet ds = new DataSet();
            ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clientid = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
            }
            return clientid;
        }
        public static string domainclientsfolder(string fileno)
        {
            string clientid = "0";
            clientid = clientidbyFilenumber(fileno);
            if(clientid != "0")
            {
              string  query = "Select Parent from [cc_masterdataset].[customer] where [id]=" + clientid;
              DataSet ds = new DataSet();
                ds = GetDataSet(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Parent"].ToString() != "SELF")
                    {

                    }
                }

            }
            return clientid;
        }
        public static string clientidbyRequestid(string Requestid)
        {
            string clientid = "0";
            string query = "Select Customer_Id from [cc_clientdataset].[applicant] where id = (select Applicant_Id from cc_clientdataset.evaluation_request where id =" + Requestid + ")";
            DataSet ds = new DataSet();
            ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clientid = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
            }
            return clientid;
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

#endregion



        #region unwantedblock
        //#region US Equivalent Degree
        //public static void Grid_EquivalentDegreeBrowse(GridView commongrid, int customer_Id)
        //{
        //    //string strSQL = "SELECT id,Name, Description FROM us_equivalent_degree where id>1 AND customer_Id=" + customer_Id + " order by Name";
        //    string strSQL = "SELECT id,customer_Id,Name, Description FROM us_equivalent_degree where ((customer_Id=" + customer_Id + ") OR (customer_Id=0)) order by Name";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static void Grid_EquivalentDegreeSelect(DetailsView commongrid, int eql_id)
        //{
        //    string strSQL = "SELECT id,Name, Description FROM us_equivalent_degree where id=" + eql_id;
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static bool Grid_EquivalentDegreeUpdate(string Name, string Description, int id)
        //{
        //    string strSQL = "UPDATE us_equivalent_degree SET Name ='" + Name + "', Description ='" + Description + "' WHERE (id =" + id + ")";
        //    bool result = GetDataSet_withoutID(strSQL);
        //    return result;

        //}
        //public static bool Grid_EquivalentDegreeAdd(string Name, string Description, int customer_Id)
        //{
        //    string strSQL = "INSERT INTO us_equivalent_degree(Name, Description,customer_Id) VALUES ('" + Name + "','" + Description + "'," + customer_Id + ")";
        //    bool result = GetDataSet_withoutID(strSQL);
        //    return result;

        //}


        //public static void Grid_CountryBrowse(GridView commongrid, string ClientId)
        //{
        //    string strSQL = " SELECT a.Id,a.Category as Type,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.countries as a where a.Id not in (SELECT c.Associated_Id from cc_relateddataset.countries as c where c.Customer_Id=" + ClientId + ")" +
        //       " union" + " SELECT a.Id,a.Category as Type,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.countries as a,cc_relateddataset.countries as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //       " order by a.Name";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();

        //}
        //#endregion
        //#region US Equivalent Major
        //public static void Grid_EquivalentMajorBrowse(GridView commongrid, int customer_Id)
        //{
        //    string strSQL = "SELECT id,customer_Id,Name, Description FROM us_equivalent_major where ((customer_Id=" + customer_Id + ") OR (customer_Id=0)) order by Name";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static void Grid_EquivalentMajorSelect(DetailsView commongrid, int eql_id)
        //{
        //    string strSQL = "SELECT id,Name, Description FROM us_equivalent_major where id=" + eql_id;
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static bool Grid_EquivalentMajorUpdate(string Name, string Description, int id)
        //{
        //    string strSQL = "UPDATE us_equivalent_major SET Name ='" + Name + "', Description ='" + Description + "' WHERE (id =" + id + ")";
        //    bool result = GetDataSet_withoutID(strSQL);
        //    return result;

        //}
        //public static bool Grid_EquivalentMajorAdd(string Name, string Description, int customer_Id)
        //{
        //    string strSQL = "INSERT INTO us_equivalent_major(Name, Description,customer_Id) VALUES ('" + Name + "','" + Description + "'," + customer_Id + ")";
        //    bool result = GetDataSet_withoutID(strSQL);
        //    return result;

        //}
        //#endregion  
        //public static void Grid_DegreePlanBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    string strSQL = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ")" +
        //     " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //     " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
        //     " order by a.Name ";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static void Grid_InstitutionBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    string strSQL = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" +
        //        " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //        " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId +
        //        " order by a.Name ";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static void Grid_MajorBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    string strSQL = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" +
        //      " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //      " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
        //      " order by a.Name ";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();
        //}
        //public static void Grid_DocumentsBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    string strSQL = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.Documents as c where c.Customer_Id=" + ClientId + ")" +
        //        " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.Documents  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //        " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
        //        " order by a.Name ";

        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();

        //}
        //public static void Grid_gradescaleBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    string strSQL = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.gradescales as c where c.Customer_Id=" + ClientId + ")" +
        //        " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.gradescales  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.gradescales as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
        //        " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
        //        " order by a.Name ";

        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();

        //}
        //public static void Grid_SourceBrowse(GridView commongrid, string ClientId, string AdminId)
        //{
        //    //string strSQL =" SELECT a.ID,a.CountryId,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.source as a where a.Customer_Id="+ AdminId +"  AND  a.ID not in (SELECT c.Associated_Id from cc_relateddataset.source as c where c.Customer_Id="+ ClientId +")" +
        //    //   " union " + " SELECT a.ID,a.CountryId,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.source as a,cc_relateddataset.source as c where a.Id = c.Associated_Id AND c.Customer_Id=" +  ClientId + 
        //    //   "order by a.Name ";
        //    string strSQL = " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.ID not in (SELECT c.Associated_Id from cc_relateddataset.source as c where c.Customer_Id=" + ClientId + ")" +
        //      " union " + " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id,cc_relateddataset.source as c  where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
            //   "order by a.Name ";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();

        //}
        //public static void Grid_EquivalencyBrowse(GridView commongrid, string ClientId)
        //{
        //    string strSQL = "SELECT ID, Name,Description, Customer_Id FROM cc_clientdataset.Equivalency WHERE (Customer_Id =" + ClientId + " ) order by Name";
        //    DataSet ds = GetDataSet(strSQL);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();

        //}
        
        //public static void Grid_SearchApplicant(GridView commongrid, string data, string customer, string rd)
        //{
        //    string query = "";

        //    switch (rd)
        //    {
        //        case "Name (First Name|Last Name)":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_clientdataset.applicant  WHERE ((cc_clientdataset.applicant.FirstName ='" + data + "') OR (cc_clientdataset.applicant.LastName ='" + data + "')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "DOB (yyyy-mm-dd)":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_clientdataset.applicant WHERE ((cc_clientdataset.applicant.DateOfBirth ='" + data + "')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "Country (Country Of Birth)":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber,cc_clientdataset.applicant.Email FROM cc_masterdataset.countries INNER JOIN cc_clientdataset.applicant ON cc_masterdataset.countries.ID = cc_clientdataset.applicant.Countryofbirth WHERE ((cc_clientdataset.applicant.FileNumber <> '') AND (cc_clientdataset.applicant.Customer_Id = " + customer.ToString() + ") AND (cc_masterdataset.countries.Name = '" + data + "')) order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "Email":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_clientdataset.applicant WHERE ((cc_clientdataset.applicant.Email ='" + data + "')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "Filenumber":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_clientdataset.applicant WHERE ((cc_clientdataset.applicant.FileNumber ='" + data + "')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "Highschool":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id WHERE ((cc_masterdataset.institution.Name ='" + data + "') AND (cc_masterdataset.institution.Type = 'HighSchool')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;
        //        case "University":
        //            query = "SELECT cc_clientdataset.applicant.FirstName+' '+ cc_clientdataset.applicant.MiddleName+' '+ cc_clientdataset.applicant.LastName AS Name, cc_clientdataset.applicant.FileNumber, cc_clientdataset.applicant.Email FROM cc_masterdataset.institution INNER JOIN cc_clientdataset.applicant_education_history ON cc_masterdataset.institution.id = cc_clientdataset.applicant_education_history.EducationInstitution_Id INNER JOIN cc_clientdataset.evaluation_request ON cc_clientdataset.applicant_education_history.Evaluation_Request_Id = cc_clientdataset.evaluation_request.id INNER JOIN cc_clientdataset.applicant ON cc_clientdataset.evaluation_request.Applicant_Id = cc_clientdataset.applicant.id WHERE ((cc_masterdataset.institution.Name ='" + data + "') AND (cc_masterdataset.institution.Type = 'University')) AND (cc_clientdataset.applicant.FileNumber <>'') AND (cc_clientdataset.applicant.Customer_Id=" + customer.ToString() + ") order by cc_clientdataset.applicant.id desc ";
        //            break;

        //    }
        //    DataSet ds = GetDataSet(query);
        //    commongrid.DataSource = ds;
        //    commongrid.DataBind();


        //}

        //#region login
        //public static string GetSubDomain(string Name, string Password, Uri url, string type)
        //{
        //    bool block = Convert.ToBoolean(ConfigurationSettings.AppSettings["TypeSwitcher"].ToString());
        //    string SubDomainName = "";
        //    string title = "";
        //    if (block)
        //    {
        //        //---main block--------------------------------------------
        //        string host = url.Host;
        //        if (host.Split('.').Length > 1)
        //        {
        //            int index = host.IndexOf(".");
        //            SubDomainName = host.Substring(0, index);
        //            switch (type)
        //            {
        //                case "title":
        //                    title = Check_Title(SubDomainName);
        //                    break;

        //                case "check_customer":
        //                    int cstid = Check_Customer(SubDomainName);
        //                    title = cstid.ToString();
        //                    break;

        //                case "login":
        //                    string result = Login(Name, Password, SubDomainName);
        //                    title = result;
        //                    break;
        //            }
        //            return title;
        //        }
        //        //---main block--------------------------------------------
        //    }
        //    else
        //    {

        //        //---demo block--------------------------------------------
        //        SubDomainName = "ravtronix";
        //        switch (type)
        //        {
        //            case "title":
        //                title = Check_Title(SubDomainName);
        //                break;

        //            case "check_customer":
        //                int cstid = Check_Customer(SubDomainName);
        //                title = cstid.ToString();
        //                break;

        //            case "login":
        //                string result = Login(Name, Password, SubDomainName);
        //                title = result;
        //                break;
        //        }
        //        return title;


        //        //---demo block--------------------------------------------
        //    }

        //    return null;
        //}
        //public static int Check_Customer(string customer_code)
        //{
        //    int Customer_id = 0;
        //    string query = "Select id from customer where SubDomainName='" + customer_code + "'";
        //    SqlConnection conn = new SqlConnection(DBConnectionString());
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = new SqlCommand(query, conn);
        //    SqlDataReader rd = cmd.ExecuteReader();
        //    rd.Read();
        //    if (rd.HasRows)
        //    {
        //        Customer_id = Convert.ToInt32(rd["id"].ToString());
        //    }
        //    else
        //    {
        //        Customer_id = 0;
        //    }
        //    conn.Close();
        //    return Customer_id;

        //}
        //public static string Check_Title(string customer_code)
        //{
        //    string Customer = "";
        //    string query = "Select Name from customer where SubDomainName='" + customer_code + "'";
        //    SqlConnection conn = new SqlConnection(DBConnectionString());
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = new SqlCommand(query, conn);
        //    SqlDataReader rd = cmd.ExecuteReader();
        //    rd.Read();
        //    if (rd.HasRows)
        //    {
        //        Customer = rd["Name"].ToString();
        //    }
        //    else
        //    {
        //        Customer = "Wrong Domain";
        //    }
        //    conn.Close();
        //    return Customer;

        //}
        //public static string Login(string Name, string Password, string subdomain)
        //{
        //    string result = "";
        //    string strSQL = "SELECT Customer_Id FROM login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
        //    DataSet ds = GetDataSet(strSQL);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string query = "SELECT * FROM customer where SubDomainName='" + subdomain.ToString() + "' and id=" + ds.Tables[0].Rows[0]["Customer_Id"];
        //        DataSet ds1 = GetDataSet(query);
        //        if (ds1.Tables[0].Rows.Count > 0)
        //        {
        //            result = "USER";
        //        }
        //        else
        //        {
        //            result = "Access_Denied";
        //        }
        //    }


        //    //check Admin
        //    if (result == "Access_Denied")
        //    {
        //        strSQL = "SELECT * FROM login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "') AND (Role='ADMIN'))";
        //        DataSet ds2 = GetDataSet(strSQL);
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {
        //            result = "ADMIN";
        //        }
        //        else
        //        {
        //            result = "Access_Denied";
        //        }
        //    }

        //    return result;
        //}
        //public static DataSet Logininfo(string Name, string Password)
        //{
        //    string strSQL = "SELECT * FROM  login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
        //    return GetDataSet(strSQL);
        //}

        //#endregion

      #endregion

    }
}
