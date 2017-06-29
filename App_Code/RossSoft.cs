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
namespace RossSoft

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

        #region AppSettings
        public struct AppConfig
        {
            public string MailId;
            public string BugId;
            public string Startyear;
            public string Endyear;
            public string Payment;
            public string startpage;
            public string TypeSwitcher;
            public string AdminId;
            public string deslimit;
            public string SessionTime;

        }

        public static AppConfig AppSettings()
        {
            AppConfig config = new AppConfig();
            string query = "Select * FROM cc_masterdataset.Appsettings";
            DataSet ds = new DataSet();


            ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // config.MailId = ds.Tables[0].Rows[0]["Email"].ToString();
                // config.BugId = ds.Tables[0].Rows[0]["Email"].ToString();
                config.Startyear = base64Decode(ds.Tables[0].Rows[0]["Startyear"].ToString());
                config.Endyear = base64Decode(ds.Tables[0].Rows[0]["Endyear"].ToString());
                config.Payment = base64Decode(ds.Tables[0].Rows[0]["Payment"].ToString());
                config.startpage = base64Decode(ds.Tables[0].Rows[0]["Startpage"].ToString());
                config.TypeSwitcher = base64Decode(ds.Tables[0].Rows[0]["TypeSwitcher"].ToString());
                config.AdminId = base64Decode(ds.Tables[0].Rows[0]["AdminId"].ToString());
                config.deslimit = base64Decode(ds.Tables[0].Rows[0]["Deslimit"].ToString());
                config.SessionTime = base64Decode(ds.Tables[0].Rows[0]["SessionTime"].ToString());

            }

            return config;
        }
        #endregion


        #region AppSettings admin
        public static void Browse_AppSettings(GridView commongrid)
        {
            string strSQL = "Select * from cc_masterdataset.customersettings";
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static void Select_AppSettings(DetailsView commongrid ,string Id)
        {
            string strSQL = "Select * from cc_masterdataset.customersettings where Id="+ Id;
            DataSet ds = GetDataSet(strSQL);
            commongrid.DataSource = ds;
            commongrid.DataBind();

        }
        public static bool Update_AppSettings(TextBox txtCustomerId, TextBox txtCustomers, TextBox txtStartpage, TextBox txtPayment, TextBox txtTypeSwitcher, TextBox txtAdminId, TextBox txtdeslimit, TextBox txtSessionTime, TextBox txtStartyear, TextBox txtEndyear, string Id)
        {
            bool result = false;
            string strSQL = "Update cc_masterdataset.customersettings set [CustomerId]='" + base64Encode(txtCustomerId.Text) + "',[Customers]='" + base64Encode(txtCustomers.Text) + "',[Startpage]='" + base64Encode(txtStartpage.Text) + "',[Payment]='" + base64Encode(txtPayment.Text) + "',[TypeSwitcher]='" + base64Encode(txtTypeSwitcher.Text) + "',[AdminId]='" + base64Encode(txtAdminId.Text) + "',[Deslimit]='" + base64Encode(txtdeslimit.Text) + "',[SessionTime]='" + base64Encode(txtSessionTime.Text) + "',[Startyear]='" + base64Encode(txtStartyear.Text) + "',[Endyear]='" + base64Encode(txtEndyear.Text) + "' WHERE Id="+Id;
            result = GetDataSet_withoutID(strSQL);  
            return result;
        }

        public static string singledecrypt(string value)
        {
            string result = "";
            result = base64Decode(value);  
            return result;
        }
        #endregion

        #region encryptor
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




    }
}
