using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;



/// <summary>
/// Summary description for RecomEngine
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService()]
public class RecomEngine : System.Web.Services.WebService {

    //public RecomEngine () {

    //    //Uncomment the following line if using designed components 
    //    //InitializeComponent(); 
    //}

    [WebMethod]
    public string Generate_Recommendation(string input)
    {
        string result = "";
        
        RecomEngine_NoCBC.RecommendatoinEngine rme = new RecomEngine_NoCBC.RecommendatoinEngine();    
        rme.ProcessERI(input);
        //rme.ProcessPendingERIs();

        result = input;
       

        return result;
    }
    private static DataSet GetDataSet(string strSQL)
    {
        SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
        DataSet ds = new DataSet();
        sdpPrd.Fill(ds);
        return ds;
    }
   private static string DBConnectionString()
    {
        string strConn = ConfigurationManager.ConnectionStrings["CredentialConsultantsConnectionString"].ConnectionString;
        return strConn;
    }

}

