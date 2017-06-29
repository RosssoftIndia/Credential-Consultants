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
/// Summary description for MasterAdmin
/// </summary>
namespace MasterAdmin

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
        public static void GetEquivalency(DropDownList option, string ClientId, string AdminId)
        {

            string strSQL = "SELECT * FROM cc_clientdataset.Equivalency where (Customer_Id=" + AdminId + ") OR (Customer_Id=" + ClientId + ")order by Name";
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "id";
            option.AppendDataBoundItems = true;
            option.Items.Add(new ListItem("Select", "0"));
            option.DataBind();
        }
        public static void GetEquivalencyMajor(DropDownList option, int Customer_id)
        {

            string strSQL = "SELECT * FROM us_equivalent_major where (Customer_Id=0) OR (Customer_Id=" + Customer_id + ")";
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "id";
            option.AppendDataBoundItems = true;
            option.Items.Add(new ListItem("Select", "0"));
            option.DataBind();
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
        public static bool Grid_CountryAdd(string Country, string description, string AdminId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.countries(Name,Description,Category,Customer_Id) VALUES ('" + Country.Replace("'", "''") + "','" + description.Replace("'", "''") + "','Master'," + AdminId + ")";
            result = GetDataSet_withoutID(strSQL);          
            return result;

        }
        public static void Grid_CountrySelect(DetailsView commongrid, int country_id, string role)
        {
             string strSQL="";
            switch(role)
            {
                case "Client":
                    strSQL = "select Id,Name,Description,Description as masterdesc from cc_masterdataset.countries where Id =" + country_id;
                    break;
                default:
                    strSQL = "select Id,Name,Description,Description as masterdesc from cc_masterdataset.countries where Id =" + country_id;
                    break;
            }
        
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }
        }
        public static bool Grid_CountryUpdate(string country, int countryid, string des,string role,string ClientId)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.countries set Name='" + country.Replace("'", "''") + "',Description='" + des.Replace("'", "''") + "' where Id =" + countryid;
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.countries set Name='" + country.Replace("'", "''") + "',Description='" + des.Replace("'", "''") + "' where Id =" + countryid;
                    break;
            }            
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_CountryPromote(string Id,string Country, string description, string ClientId)
        {
            bool result = false;
            string strSQL = " INSERT INTO cc_masterdataset.countries(Name,Description,Category,Customer_Id) VALUES ('" + Country.Replace("'", "''") + "','" + description.Replace("'", "''") + "','Promote'," + ClientId + ");" +
                            " UPDATE cc_clientdataset.countries set IsEnabled=0 WHERE Id=" + Id + " AND Customer_Id="+ ClientId;
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_CountryDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.countries set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.countries set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        #endregion
        #region institution table
        public static void Grid_SearchInstitution(GridView commongrid, string data, string ClientId, string AdminId, string IsConfirm, string Type, string category, string Isdegreemill)
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
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution c  where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                        " order by a.Name ";
                    }
                    else
        {
                        query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution c where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                        " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.IsDegreeMill FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" + " AND a.Type='" + Type + "'" + " AND a.IsDegreeMill=" + Isdegreemill +
                 " order by a.Name ";       
                    }
                    break;

            }
            //data = data.Replace("'", "''");
            //string query = "";
            //switch (category)
            //{
            //    case "Name":
            //        if (IsConfirm != "2")
            //        {

            //            query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
            //        " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
            //        " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
            //        " order by a.Name ";
            //        }
            //        else
            //        {
            //            query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" +
            //       " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" +
            //       " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" + " AND a.Type='" + Type + "'" +
            //       " order by a.Name ";
            //        }
            //        break;
            //    case "Country":
            //        if (IsConfirm != "2")
            //        {
            //            query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c  where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
            //            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
            //            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm +
            //            " order by a.Name ";
            //        }
            //        else
            //        {
            //            query = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ") AND cc_masterdataset.countries.Name like '" + data + "%'" +
            //            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
            //            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%'" +
            //            " order by a.Name ";
            //        }
            //        break;

           // }
        //    if (IsConfirm != "2")
        //    {
        //        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
        // " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
        // " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Confirmed=" + IsConfirm + " AND a.Type='" + Type + "'" +
        // " order by a.Name ";
        //    }
        //    else
        //    {
        //        query = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" +
        //" union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" +
        //" union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND cc_masterdataset.countries.Name like '" + data + "%' AND a.Type='" + Type + "'" +
        //" order by a.Name ";
        //    }

        //    break;

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
            switch (role)
            {
                case "Client":                
                    strSQL = "UPDATE cc_masterdataset.institution SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType + "',Description='" + des.Replace("'", "''") + "',IsDegreeMill =" + mill + " WHERE id =" + id;
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.institution SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType + "',Description='" + des.Replace("'", "''") + "',IsDegreeMill =" + mill + " WHERE id =" + id;
                    break;
            }   
           
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static void Grid_InstitutionSelect(DetailsView commongrid, int institution_id,string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,a.Description,a.Description as masterdesc,a.IsDegreeMill from cc_masterdataset.institution a where a.id=" + institution_id;
                    break;
                default:
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Type,a.Confirmed,a.Description,a.Description as masterdesc,a.IsDegreeMill from cc_masterdataset.institution a where a.id=" + institution_id;
                    break;
            }      
          
                DataSet ds = GetDataSet(strSQL);
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }
        public static bool Grid_InstitutionAdd(string Name, int Country_ID, int Confirmed, string catType, string des, string AdminId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.institution(Name, Country_ID, Confirmed, Type,Description,Category,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + ",'" + catType.Replace("'", "''") + "','" + des.Replace("'", "''") + "','Master'," + AdminId + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }

        public static bool Grid_InstitutionPromote(string Name, string Country_ID, string Confirmed, string catType, string des, string Id, string ClientId, string AdminId,string mill)
        {
            bool result = false;
            string strSQL = " INSERT INTO cc_historydataset.institution(Institution_Id,Name,Country_ID,Confirmed,Type,Description,Category,Customer_Id,IsDegreeMill) VALUES (" + Id + ",'" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + ",'" + catType + "','" + des.Replace("'", "''") + "','Client'," + ClientId + ","+ mill +");" +
                            " UPDATE cc_masterdataset.institution set Customer_Id=" + AdminId + ",Category='Promote' WHERE Id=" + Id + "; INSERT INTO cc_relateddataset.institution(Associated_Id,Description,Customer_Id,IsEnabled) VALUES (" + Id + ",'" + des.Replace("'", "''")+ "'," + ClientId + "," + 0 + ")";           
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_InstitutionDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.institution set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.institution set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
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
        public static void Grid_DegreePlanSelect(DetailsView commongrid, int degree_id, string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "SELECT Customer_Id,Id,Name,Country_ID,Confirmed,Type,EquiDegree_id,Description,Description as masterdesc FROM cc_masterdataset.degree WHERE (Id =" + degree_id + ")";
                    break;
                default:
                    strSQL = "SELECT Customer_Id,Id,Name,Country_ID,Confirmed,Type,EquiDegree_id,Description,Description as masterdesc FROM cc_masterdataset.degree WHERE (Id =" + degree_id + ")";
                    break;
            }   
            
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_DegreePlanUpdate(string Name, int Country_ID, int Confirmed, string catType, int id, int EquiDegree_id, string ClientId, string des, string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.degree SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType + "',EquiDegree_id =" + EquiDegree_id + ",Description='" + des.Replace("'", "''") + "' WHERE (Id =" + id + ")";
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.degree SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ", Type ='" + catType + "',EquiDegree_id =" + EquiDegree_id + ",Description='" + des.Replace("'", "''") + "' WHERE (Id =" + id + ")";
                    break;
            }

            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DegreePlanAdd(string Name, int Country_ID, int Confirmed, string catType, int EquiDegree_id,string des, string AdminId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.degree(Name, Country_ID, Confirmed, Type,EquiDegree_id,Category,Description,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + ",'" + catType.Replace("'", "''") + "'," + EquiDegree_id + ",'Master','" + des.Replace("'", "''") + "'," + AdminId + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_DegreePlanPromote(string Id, string ClientId, string AdminId)
        {
            bool result = false;
            string strSQL = "";
            strSQL = "SELECT Customer_Id,Id,Name,Country_ID,Confirmed,Type,EquiDegree_id,Description FROM cc_masterdataset.degree WHERE (Id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = " INSERT INTO cc_historydataset.degree(Degree_Id,Name, Country_ID, Confirmed, Type,EquiDegree_id,Category,Description,Customer_Id) VALUES (" + ds.Tables[0].Rows[0]["Id"].ToString() + ",'" + ds.Tables[0].Rows[0]["Name"].ToString().Replace("'", "''") + "'," + ds.Tables[0].Rows[0]["Country_ID"].ToString() + "," + ds.Tables[0].Rows[0]["Confirmed"].ToString() + ",'" + ds.Tables[0].Rows[0]["Type"].ToString() + "'," + ds.Tables[0].Rows[0]["EquiDegree_id"].ToString() + ",'Client','" + ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''") + "'," + ClientId + ");" +
                            " UPDATE cc_masterdataset.degree set Category='Promote',Customer_Id=" + AdminId + " WHERE Id=" + Id + "; INSERT INTO cc_relateddataset.degree(Associated_Id,Description,Customer_Id,IsEnabled) VALUES (" + ds.Tables[0].Rows[0]["Id"].ToString() + ",'" + ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''") + "'," + ClientId + "," + 0 + ")";
            }
           
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_DegreeDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.degree set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.degree set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
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
        public static void Grid_MajorSelect(DetailsView commongrid, int Major_id, string role)
        {

            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "SELECT Id,Name,Country_ID, Confirmed,Equimajor_id, Category,Customer_Id,Description,Description as masterdesc FROM cc_masterdataset.major where Id =" + Major_id;
                    break;
                default:
                    strSQL = "SELECT Id,Name,Country_ID, Confirmed,Equimajor_id, Category,Customer_Id,Description,Description as masterdesc FROM cc_masterdataset.major where Id =" + Major_id;
                    break;
            }           
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static bool Grid_MajorUpdate(string Name, int Country_ID, int Confirmed, int id, int EquiMajor_id, string ClientId, string des, string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.major SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ",EquiMajor_id=" + EquiMajor_id + ",Description='" + des.Replace("'", "''") + "'  WHERE (Id =" + id + ")";
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.major SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country_ID + ", Confirmed =" + Confirmed + ",EquiMajor_id=" + EquiMajor_id + ",Description='" + des.Replace("'", "''") + "' WHERE (Id =" + id + ")";
                    break;
            }        
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool Grid_MajorAdd(string Name, int Country_ID, int Confirmed, int EquiMajor_id, string des, string AdminId)
        {
            string strSQL = "INSERT INTO cc_masterdataset.major(Name, Country_ID, Confirmed,EquiMajor_id,Category,Description,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country_ID + "," + Confirmed + "," + EquiMajor_id + ",'Master','" + des.Replace("'", "''") + "'," + AdminId + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }

        public static bool Grid_MajorPromote(string Id, string ClientId, string AdminId)
        {
            bool result = false;
            string strSQL = "";
            strSQL = "SELECT Customer_Id,Id,Name,Country_ID,Confirmed,Equimajor_id,Description FROM cc_masterdataset.major WHERE (Id =" + Id + ")";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strSQL = " INSERT INTO cc_historydataset.major(Major_Id,Name, Country_ID, Confirmed,Equimajor_id,Category,Description,Customer_Id) VALUES (" + ds.Tables[0].Rows[0]["Id"].ToString() + ",'" + ds.Tables[0].Rows[0]["Name"].ToString().Replace("'", "''") + "'," + ds.Tables[0].Rows[0]["Country_ID"].ToString() + "," + ds.Tables[0].Rows[0]["Confirmed"].ToString() + "," + ds.Tables[0].Rows[0]["Equimajor_id"].ToString() + ",'Client','" + ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''") + "'," + ClientId + ");" +
                            " UPDATE cc_masterdataset.major set Category='Promote',Customer_Id=" + AdminId + " WHERE Id=" + Id + "; INSERT INTO cc_relateddataset.major(Associated_Id,Description,Customer_Id,IsEnabled) VALUES (" + ds.Tables[0].Rows[0]["Id"].ToString() + ",'" + ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''") + "'," + ClientId + "," + 0 + ")";
            }

            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_MajorDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.major set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.major set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
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
        public static bool Grid_SourceAdd(string Name, string des, int Country, string AdminId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.Source(Name,Description,CountryId,Category,Customer_Id)VALUES('" + Name.Replace("'", "''") + "','" + des.Replace("'", "''") + "'," + Country + ",'Master'," + AdminId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_SourceSelect(DetailsView commongrid, int source_id)
        {
            string strSQL = "SELECT *,Description as masterdesc FROM cc_masterdataset.Source WHERE ID =" + source_id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_SourceUpdate(string Name, string des, int Country, string ClientId, int source_id)
        {
            string strSQL = "UPDATE cc_masterdataset.source SET Name ='" + Name.Replace("'", "''") + "', Description ='" + des.Replace("'", "''") + "', CountryId =" + Country + " WHERE  ID=" + source_id ;
            bool result = GetDataSet_withoutID(strSQL);
            return result;            
        }
        public static bool Grid_SourceDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.source set Description='" + des.Replace("'", "''") + "' WHERE ID=" + Id + "; UPDATE cc_relateddataset.source set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
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
                    query = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled,a.Attachment  FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.Documents as c where c.Customer_Id=" + ClientId + ")" + " AND a.Name like'" + data + "%'" +
                " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled,a.Attachment  FROM cc_masterdataset.Documents  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
                " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled,a.Attachment  FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId + " AND a.Name like'" + data + "%'" +
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
        public static bool Grid_DocumentsAdd(string Name, int Country, string des, string AdminId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.Documents(Name, Country_ID,Description,Category,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country + ",'" + des.Replace("'", "''") + "','Master'," + AdminId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_DocumentsSelect(DetailsView commongrid, int des_id, string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,a.Description as masterdesc,a.Attachment from cc_masterdataset.Documents a where a.id=" + des_id;
                    break;
                default:
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,a.Description as masterdesc,a.Attachment from cc_masterdataset.Documents a where a.id=" + des_id;
                    break;
            }      
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_DocumentsUpdate(string Name, int Country, int doc_id, string ClientId, string des, string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.Documents SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country + ",Description='" + des.Replace("'", "''") + "'  WHERE  Id=" + doc_id + " AND Customer_Id =" + ClientId;
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.Documents SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country + ",Description='" + des.Replace("'", "''") + "'  WHERE  Id=" + doc_id;
                    break;
            }

            bool result = GetDataSet_withoutID(strSQL);
            return result;           
        }

        public static bool Grid_DocumentsPromote(string Name, string Country_ID,string des, string Id, string ClientId, string AdminId)
        {
            bool result = false;
            string strSQL = " INSERT INTO cc_historydataset.Documents(Documents_Id,Name,Country_ID,Description,Category,Customer_Id) VALUES (" + Id + ",'" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + des.Replace("'", "''") + "','Client'," + ClientId + ");" +
                            " UPDATE cc_masterdataset.Documents set Customer_Id=" + AdminId + ",Category='Promote' WHERE Id=" + Id + "; INSERT INTO cc_relateddataset.Documents(Associated_Id,Description,Customer_Id,IsEnabled) VALUES (" + Id + ",'" + des.Replace("'", "''") +"',"+ ClientId +","+ 0 +")";
            result = GetDataSet_withoutID(strSQL);
            return result;
        }     
        public static bool Grid_DocumentsDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.Documents set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.Documents set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Attachment_Documents(string Name, string doc_id,string type)
        {
            string strSQL = "";
            switch (type)
            {
                case "Clear":
                    strSQL = "UPDATE cc_masterdataset.Documents SET Attachment ='" + null  + "' WHERE  Id=" + doc_id;
                    break;
                case "Add":
                    strSQL = "UPDATE cc_masterdataset.Documents SET Attachment ='" + Name.Replace("'", "''") + "' WHERE  Id=" + doc_id;
                    break;
            }
          
            bool result = GetDataSet_withoutID(strSQL);
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
        public static bool Grid_gradescaleAdd(string Name, int Country, string des, string AdminId)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.gradescales(Name, Country_ID, Description,Category,Customer_Id) VALUES ('" + Name.Replace("'", "''") + "'," + Country + ",'" + des.Replace("'", "''") + "','Master'," + AdminId + ")";
            result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Grid_gradescaleSelect(DetailsView commongrid, int grade_id,string role)
        {
            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,a.Description as masterdesc from cc_masterdataset.gradescales a where a.id=" + grade_id;
                    break;
                default:
                    strSQL = "select a.id,a.Customer_Id,a.Name,a.Country_ID,a.Description,a.Description as masterdesc from cc_masterdataset.gradescales a where a.id=" + grade_id;
                    break;
            }   
         
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Grid_gradescaleUpdate(string Name, int Country, string des, int grade_id, string ClientId,string role)
        {

            string strSQL = "";
            switch (role)
            {
                case "Client":
                    strSQL = "UPDATE cc_masterdataset.gradescales SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country + ",Description ='" + des.Replace("'", "''") + "' WHERE  ID=" + grade_id;
                    break;
                default:
                    strSQL = "UPDATE cc_masterdataset.gradescales SET Name ='" + Name.Replace("'", "''") + "', Country_ID =" + Country + ",Description ='" + des.Replace("'", "''") + "' WHERE  ID=" + grade_id;
                    break;
            }
           
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }

        public static bool Grid_gradescalePromote(string Name, string Country_ID,string des, string Id, string ClientId, string AdminId)
        {
            bool result = false;
            string strSQL = " INSERT INTO cc_historydataset.gradescales(Gradescale_Id,Name,Country_ID,Description,Category,Customer_Id) VALUES (" + Id + ",'" + Name.Replace("'", "''") + "'," + Country_ID + ",'" + des.Replace("'", "''") + "','Client'," + ClientId + ");" +
                            " UPDATE cc_masterdataset.gradescales set Customer_Id=" + AdminId + ",Category='Promote' WHERE Id=" + Id + "; INSERT INTO cc_relateddataset.gradescales(Associated_Id,Description,Customer_Id,IsEnabled) VALUES (" + Id +",'" + des.Replace("'", "''") +"'," + ClientId + ","+0+")";
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
        public static bool Grid_gradescaleDescPromote(string des, string Id, string ClientId)
        {
            bool result = false;
            string strSQL = "UPDATE cc_masterdataset.gradescales set Description='" + des.Replace("'", "''") + "' WHERE Id=" + Id + "; UPDATE cc_relateddataset.gradescales set IsEnabled=0 WHERE Associated_Id=" + Id + " AND Customer_Id=" + ClientId;
            result = GetDataSet_withoutID(strSQL);
            return result;
        }
      
        #endregion


        #endregion


        #region Client Information
        #region Client
        public static DataSet Getclientlist()
        {
            string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where Parent ='SELF' ";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static DataSet Getsubclientlist(string Parentdomain)
        {
            string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where Parent ='" + Parentdomain + "'  order by Name";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        public static void Grid_clientSelect(DetailsView commongrid, string Client_id)
        {
            string strSQL = "SELECT * FROM cc_masterdataset.customer where id="+Client_id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }

        }
        public static bool Grid_clientUpdate(string Name, string Address, string City,string State,string Zipcode,string DomainName,string parent ,int id)
        {
            string strSQL = "UPDATE [cc_masterdataset].[customer] SET Name='" + Name.Replace("'", "''") + "',Address='" + Address.Replace("'", "''") + "',City='" + City.Replace("'", "''") + "',State='" + State.Replace("'", "''") + "',ZipCode='" + Zipcode + "',SubDomainName='" + DomainName.Replace("'", "''") + "',Parent='"+ parent +"' where  id=" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void dpclient(DropDownList option)
        {
            string strSQL = "SELECT Name,SubDomainName FROM cc_masterdataset.customer where id <> 0 AND Parent ='SELF' ";
            DataSet ds = GetDataSet(strSQL);           
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "SubDomainName";
            option.AppendDataBoundItems = true; 
            option.Items.Add(new ListItem("Domain", "SELF"));
            option.DataBind();

        }
        public static bool Grid_clientAdd(string Name, string Address, string City, string State, string Zipcode, string Subdomain,string Parent)
        {
            bool result = false;
            string strSQL = "INSERT INTO cc_masterdataset.customer ([Name],[Address],[City],[State],[ZipCode],[SubDomainName],[Parent]) VALUES ('" + Name.Replace("'", "''") + "','" + Address + "','" + City + "','" + State + "','" + Zipcode + "','" + Subdomain  + "','"+ Parent  +"')";
            int id = GetDataSet_withID(strSQL);
            if (id != 0)
            {
                if (Parent == "SELF")
                {
                strSQL = "INSERT INTO [cc_masterdataset].[customersettings]([Customer_Id],[Delivery_copy],[Delivery_Instructions],[Education_Instructions],[Document_Instructions],[Terms_And_Condition],[CreditCard],[LoginId],[Transkey],[Email],[ThkuPage],[SiteUrl],[Credit_Type]) VALUES("+id+",0,'','','','',0,'','','',0,'','')";
                 result = GetDataSet_withoutID(strSQL);
                 if (result)
                 {
                     strSQL = "INSERT INTO [cc_masterdataset].[splashpagesettings]([Customer_Id],[BrowserInstructions],[AppInstructions])select " + id + " as [Customer_Id],[BrowserInstructions],[AppInstructions] from [cc_masterdataset].[splashpagesettings] where [Customer_Id]=0";
                     result = GetDataSet_withoutID(strSQL);

                 }
            }
                else
                {
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
            }


            return result;

        }
      
        #endregion
        #region Client-Login
        public static void Grid_clientLoginBrowse(GridView commongrid, int customer_Id)
        {
            string strSQL = "SELECT * FROM  cc_relateddataset.login WHERE Role='Client' order by Name";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_clientLoginSelect(DetailsView commongrid, string Employee_id)
        {
            string strSQL = "SELECT * FROM cc_relateddataset.login where id=" + Employee_id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                commongrid.DataSource = ds;
                commongrid.DataBind();
            }

        }
        public static string Grid_clientLoginAdd(int Customer_Id, string Name, string Password)
        {
            string result = "";
            string strSQL = "SELECT id FROM cc_relateddataset.login where Customer_Id=" + Customer_Id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = "Login exists for this Client";
            }
            else
            {
                strSQL = "INSERT INTO cc_relateddataset.login (Customer_Id, Name, Password,Role) VALUES (" + Customer_Id + ",'" + Name.Replace("'", "''") + "','" + Password + "','CLIENT')";
                bool check = GetDataSet_withoutID(strSQL);
                if (check) { result = "true"; } else { result = "false"; }
            }
            return result;

        }
        public static bool Grid_clientLoginUpdate(string Name, string Password, string id)
        {
            string strSQL = "UPDATE cc_relateddataset.login SET Name='" + Name + "',Password='" + Password + "',Role='CLIENT' WHERE (id =" + id + ")";
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static void Getclient(DropDownList option)
        {
            string strSQL = "SELECT Name,id FROM cc_masterdataset.customer where id <> 0";
            DataSet ds = GetDataSet(strSQL);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "id";
            option.Items.Add(new ListItem("Select", "0"));
            option.DataBind();

        }
        public static void Selectclient(string Employee_id, DropDownList dp)
        {
            string strSQL = "SELECT Customer_Id FROM cc_relateddataset.login where id=" + Employee_id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dp.SelectedValue = ds.Tables[0].Rows[0]["Customer_Id"].ToString();
            }

        }
        #endregion
        #region Login as Client
        public static bool Admin_Client(string Employee_id, TextBox name, TextBox password)
        {
            bool result = false;
            string strSQL = "SELECT Name, Password FROM cc_relateddataset.login where id=" + Employee_id;
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                password.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }
        public static DataSet Logininfo(string Name, string Password)
        {
            string strSQL = "SELECT * FROM  cc_relateddataset.login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
            return GetDataSet(strSQL);
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

        public static bool Splashpage_Update(string Browsermsg, string Appmsg, string clientmsg, int offlineapp, string offlineappmsg, string ClientId)
        {

            string strSQL = "UPDATE cc_masterdataset.splashpagesettings SET  BrowserInstructions ='" + Browsermsg.Replace("'", "''") + "',AppInstructions='" + Appmsg.Replace("'", "''") + "',ClientInstructions='" + clientmsg.Replace("'", "''") + "',OfflineApp=" + offlineapp + ",OfflineAppInstructions='" + offlineappmsg.Replace("'", "''") + "'  where Customer_Id=" + ClientId;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }

        #endregion
        #endregion


        #region delete function
        public static bool del_Country(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[countries] where Id="+ id +";delete  from [cc_relateddataset].[countries] where Associated_Id ="+id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Document(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[Documents] where Id=" + id + ";delete  from [cc_relateddataset].[Documents] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Educationprogram(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[degree] where Id=" + id + ";delete  from [cc_relateddataset].[degree] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Gradescales(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[gradescales] where Id=" + id + ";delete  from [cc_relateddataset].[gradescales] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_major(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[major] where Id=" + id + ";delete  from [cc_relateddataset].[major] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
        public static bool del_Institution(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[institution] where Id=" + id + ";delete  from [cc_relateddataset].[institution] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;
        }
        
            public static bool del_Source(string id)
        {
            string strSQL = "delete from [cc_masterdataset].[Source] where Id=" + id + ";delete  from [cc_relateddataset].[Source] where Associated_Id =" + id;
            bool result = GetDataSet_withoutID(strSQL);
            return result;

        }
     
        #endregion



            #region Templates
            public static void dpgetclient(DropDownList option)
            {
                string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where id <> 0 AND Parent ='SELF' ";
                DataSet ds = GetDataSet(strSQL);
                option.DataSource = ds;
                option.DataTextField = "Name";
                option.DataValueField = "id";
                option.AppendDataBoundItems = true;
                option.Items.Add(new ListItem("Select", "0"));
                option.DataBind();

            }
            public static void dpsubclients(string Parentdomain, DropDownList option)
            {
                string strSQL = "SELECT id,Name,SubDomainName FROM cc_masterdataset.customer where Parent =(Select SubDomainName FROM cc_masterdataset.customer where id=" + Parentdomain + ")";
                DataSet ds = GetDataSet(strSQL);
                option.Items.Clear();
                option.DataSource = ds;
                option.DataTextField = "Name";
                option.DataValueField = "id";
                option.Items.Add(new ListItem("Select", "0"));
                option.DataBind();

            }
           
            #endregion









     

        #region public applicant view(client based)
        public static string check_filenumber(string filenumber, string customerId)
        {
            string result = "Access_Denied";
            string strSQL = "SELECT * FROM applicant WHERE FileNumber ='" + filenumber.Replace("'", "''") + "'and Customer_Id=" + customerId;
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
            string strSQL = "SELECT evaluation_request.Application_Recieved, evaluation_request.Documents_Recieved, evaluation_request.Payment_Recieved, evaluation_request.Evaluation_Complete, evaluation_request.Verification_Complete, evaluation_request.Evaluation_Approved, evaluation_request.Packaging_Complete, evaluation_request.Delievery_Complete, evaluation_purpose.Evaluation_Name,applicant.FirstName,applicant.LastName,applicant.DateOfBirth,applicant.FileNumber FROM (((applicant INNER JOIN countries ON applicant.CountryId = countries.CountryId) INNER JOIN evaluation_request ON applicant.id = evaluation_request.Applicant_Id) INNER JOIN evaluation_purpose ON evaluation_request.Purpose_Id = evaluation_purpose.id) WHERE applicant.FileNumber ='" + FileNumber + "'";
            DataSet ds = GetDataSet(strSQL);
            return ds;
        }
        #endregion

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
            string strSQL = "select Card_Type from Card_Type";
            DataSet ds = GetDataSet(strSQL);
            options.DataSource = ds;
            options.DataTextField = "Card_Type";
            options.DataValueField = "Card_Type";
            options.DataBind();

        }
        #endregion


        #region unwanted
        public static void Grid_CountryBrowse(GridView commongrid, string ClientId)
        {
            string strSQL = " SELECT a.Id,a.Category as Type,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.countries as a where a.Id not in (SELECT c.Associated_Id from cc_relateddataset.countries as c where c.Customer_Id=" + ClientId + ")" +
              " union" + " SELECT a.Id,a.Category as Type,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.countries as a,cc_relateddataset.countries as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
              " order by a.Name";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Grid_DegreePlanBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.degree c where c.Customer_Id=" + ClientId + ")" +
            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.degree  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.degree as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
            " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,a.type,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.degree as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
            " order by a.Name ";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_DocumentsBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.Documents as c where c.Customer_Id=" + ClientId + ")" +
               " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.Documents  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.Documents as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
               " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.Documents as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
               " order by a.Name ";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Grid_gradescaleBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.gradescales as c where c.Customer_Id=" + ClientId + ")" +
                " union " + "  SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.gradescales  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.gradescales as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
                " union " + " SELECT a.Id,a.Country_ID,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.gradescales as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
                " order by a.Name ";

            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Grid_InstitutionBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.institution as c where c.Customer_Id=" + ClientId + ")" +
                 " union " + " SELECT a.Id,a.Country_ID,a.Category,c.Customer_Id,a.Name,a.Type,a.Confirmed,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.institution as a,cc_relateddataset.institution as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
                 " union " + " SELECT a.Id,a.Country_ID,a.Category,a.Customer_Id,a.Name,a.Type,a.Confirmed,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.institution as a where a.Customer_Id=" + ClientId +
                 " order by a.Name ";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_MajorBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.Id not in (SELECT c.Associated_Id from cc_relateddataset.major c where c.Customer_Id=" + ClientId + ")" +
               " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,c.Customer_Id,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.major  as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id,cc_relateddataset.major as c where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
               " union " + " SELECT a.Id,a.Country_ID,a.Confirmed,cc_masterdataset.countries.Name as country,a.Category,a.Customer_Id,a.Name,'--' as Admindesc,IsNull(a.Description,'--') as Clientdesc,0 as IsEnabled FROM cc_masterdataset.major as a INNER JOIN  cc_masterdataset.countries ON a.Country_ID  = cc_masterdataset.countries.Id where a.Customer_Id=" + ClientId +
               " order by a.Name ";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();
        }
        public static void Grid_SourceBrowse(GridView commongrid, string ClientId, string AdminId)
        {
            string strSQL = " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,'--' as Clientdesc,0 as IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id where a.Customer_Id=" + AdminId + "  AND  a.ID not in (SELECT c.Associated_Id from cc_relateddataset.source as c where c.Customer_Id=" + ClientId + ")" +
               " union " + " SELECT a.ID,cc_masterdataset.countries.Name as CountryId,a.Customer_Id,a.Category,a.Name,IsNull(a.Description,'--')as Admindesc,IsNull(c.Description,'--')as clientdes,c.IsEnabled FROM cc_masterdataset.source as a INNER JOIN  cc_masterdataset.countries ON a.CountryId = cc_masterdataset.countries.Id,cc_relateddataset.source as c  where a.Id = c.Associated_Id AND c.Customer_Id=" + ClientId +
               " order by a.Name ";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }

        #region login
        public static string GetSubDomain(string Name, string Password, Uri url, string type)
        {
            RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();    

            bool block = Convert.ToBoolean(app.TypeSwitcher);
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
            string query = "Select id from customer where SubDomainName='" + customer_code + "'";
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
            string query = "Select Name from customer where SubDomainName='" + customer_code + "'";
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
            string strSQL = "SELECT Customer_Id FROM login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "'))";
            DataSet ds = GetDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "SELECT * FROM customer where SubDomainName='" + subdomain.ToString() + "' and id=" + ds.Tables[0].Rows[0]["Customer_Id"];
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
                strSQL = "SELECT * FROM login WHERE ((Name ='" + Name + "') AND (Password='" + Password + "') AND (Role='ADMIN'))";
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
      
        #endregion
        #endregion

    }
}
