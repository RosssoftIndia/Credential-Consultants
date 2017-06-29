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
 

 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class University : WebService
{
    public University()
    {
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
       
        int ncount = 0;
        string country = "0", customer = "0";
        string[] QuerySpliter;
        ncount = contextKey.IndexOf("|");
        if (ncount > 0)
        {
            QuerySpliter = contextKey.Split('|');
            country = QuerySpliter[0].ToString();
            customer = QuerySpliter[1].ToString();
        }
        string strSQL = "SELECT Name FROM cc_masterdataset.institution WHERE (Name like '" + prefixText + "%')AND (Country_ID = " + Convert.ToInt32(country.ToString()) + ") AND (Type = 'University') AND (Confirmed = 1) AND ((Customer_Id=" + Convert.ToInt32(customer.ToString()) + ") or (Customer_Id=0))";
            DataSet ds = GetDataSet(strSQL);
             List<string> items = new List<string>(ds.Tables[0].Rows.Count);
            if (ds.Tables[0].Rows.Count > 0)
            {
                items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(ds.Tables[0].Rows[i]["Name"].ToString());
                }

            }
            return items.ToArray();       
    }

    private static DataSet GetDataSet(string strSQL)
    {
        SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
        DataSet ds = new DataSet();
        sdpPrd.Fill(ds);
        return ds;
    }
    public static string DBConnectionString()
    {
        string strConn = ConfigurationManager.ConnectionStrings["CredentialConsultantsConnectionString"].ConnectionString;
        return strConn;
    }
}