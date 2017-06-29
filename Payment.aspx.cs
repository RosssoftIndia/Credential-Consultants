using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Collections;
using System.Data; 

public partial class Payment : System.Web.UI.Page
{
    string Subdomain = "";
    string fileno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bool ClientIsValid = false;
        if (Request.QueryString["subdomain"] != null)
        {
            Subdomain = Request.QueryString["subdomain"].ToString();
        }
        else
        {
            Subdomain = "nosubdomain";
        }
        ClientIsValid = Authentication.Utility.ClientIsValid(Request.Url, Subdomain);
        //Client Check

         if (ClientIsValid)
         {
            Authentication.Utility.DomainAttributes dm = Authentication.Utility.GetClient(Request.Url, Subdomain);   

             if (dm.IsMultidomain)
             {
                 Page.Title = dm.DmName;
                 OrgTitle.InnerHtml = dm.DmName;
                 Session["Customer_id"] = dm.SubDmID;
                 
             }
             else
             {
                 Page.Title = dm.DmName; 
                 OrgTitle.InnerHtml = dm.DmName;
                 Session["Customer_id"] =  dm.DmID;
               
             }
               Authentication.Utility.checklogo(dm.DmID, OrgTitle,logo);
             }

        if (!Page.IsPostBack)
        {
            //load cost
           Session["fno"] = Request.QueryString["id"].ToString();
           Session["mode"] = Request.QueryString["mode"].ToString();
           Credentialpage.Utility.get_Cost(AmountTextBox,Dropcardtype,Session["fno"].ToString() , Session["Customer_id"].ToString());
           //year dropdown
           Credentialpage.Utility.Getyear_Payment(YearDropDownList);
           btn_submit.Attributes.Add("onclick", "Loading(true);");

        }

    }


    private bool AuthorizePayment()
    {
        RossSoft.Utility.AppConfig app = RossSoft.Utility.AppSettings();
        if (Session["Customer_id"].ToString() != "0")
        { //transkey& loginID
            DataSet ds = Credentialpage.Utility.paymentcode(Session["Customer_id"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                string l1 = ds.Tables[0].Rows[0]["LoginId"].ToString();
                string l2 = ds.Tables[0].Rows[0]["Transkey"].ToString();
                string email = ds.Tables[0].Rows[0]["Email"].ToString();

                string AuthNetVersion = "3.1"; // Contains CCV support
                string AuthNetLoginID = Credentialpage.Utility.base64Decode(l1);//"2bVa5VgR3p"; //Set your AuthNetLoginID here 411111111111111
                string AuthNetTransKey = Credentialpage.Utility.base64Decode(l2);// "69fA3MXdxt8V7p7k";  // Get this from your authorize.net merchant interface

                //string AuthNetLoginID ="2bVa5VgR3p"; //Set your AuthNetLoginID here 411111111111111
                //string AuthNetTransKey = "69fA3MXdxt8V7p7k";  // Get this from your authorize.net merchant interface

                WebClient webClientRequest = new WebClient();
                System.Collections.Specialized.NameValueCollection InputObject = new System.Collections.Specialized.NameValueCollection(30);
                System.Collections.Specialized.NameValueCollection ReturnObject = new System.Collections.Specialized.NameValueCollection(30);

                byte[] ReturnBytes;
                string[] ReturnValues;
                string ErrorString;

                InputObject.Add("x_version", AuthNetVersion);
                InputObject.Add("x_delim_data", "True");
                InputObject.Add("x_login", AuthNetLoginID);
                InputObject.Add("x_tran_key", AuthNetTransKey);
                InputObject.Add("x_relay_response", "False");

                //----------------------Set to False to go Live--------------------
                //InputObject.Add("x_test_request", "True");//demo
               // InputObject.Add("x_test_request", "False");//live
                InputObject.Add("x_test_request", app.Payment);
                //---------------------------------------------------------------------
                InputObject.Add("x_delim_char", ",");
                InputObject.Add("x_encap_char", "|");

                //Billing Address
                InputObject.Add("x_first_name", FirstNameTextBox.Text);
                InputObject.Add("x_last_name", LastNameTextBox.Text);
                InputObject.Add("x_phone", PhoneTextBox.Text);
                InputObject.Add("x_address", AddressTextBox.Text);
                InputObject.Add("x_city", CityTextBox.Text);
                InputObject.Add("x_state", StateTextBox.Text);
                InputObject.Add("x_zip", ZipTextBox.Text);
                InputObject.Add("x_email", EmailTextBox.Text);
                InputObject.Add("x_email_customer", "TRUE");                     //Emails Customer
                InputObject.Add("x_merchant_email", email);  //Emails Merchant
                InputObject.Add("x_country", CountryTextBox.Text);
                InputObject.Add("x_customer_ip", Request.UserHostAddress);  //Store Customer IP Address

                //Amount
                InputObject.Add("x_description", "Transaction FileNo:" + Session["fno"].ToString()  + " " + string.Format("{0:c2}", AmountTextBox.Text));  //Description of Purchase

                //Card Details
                InputObject.Add("x_card_num", CreditCardTextBox.Text);
                InputObject.Add("x_exp_date", MonthDropDownList.Text+YearDropDownList.Text);
                InputObject.Add("x_card_code", CCVTextBox.Text);

                InputObject.Add("x_method", "CC");
                InputObject.Add("x_type", "AUTH_CAPTURE");
                InputObject.Add("x_amount", string.Format("{0:c2}", Convert.ToDouble(AmountTextBox.Text)));

                // Currency setting. Check the guide for other supported currencies
                InputObject.Add("x_currency_code", "USD");

                try
                {
                    
                    //Actual Server
                    //Set above Testmode=off to go live
                    webClientRequest.BaseAddress = "https://secure.authorize.net/gateway/transact.dll";
                   // webClientRequest.BaseAddress = "https://test.authorize.net/gateway/transact.dll";

                    ReturnBytes = webClientRequest.UploadValues(webClientRequest.BaseAddress, "POST", InputObject);
                    ReturnValues = System.Text.Encoding.ASCII.GetString(ReturnBytes).Split(",".ToCharArray());

                    if (ReturnValues[0].Trim(char.Parse("|")) == "1")
                    {
                        authorizecode.Text  = ReturnValues[4].Trim(char.Parse("|")); // Returned Authorisation Code
                        transcode.Text  = ReturnValues[6].Trim(char.Parse("|")); // Returned Transaction ID
                        return true;
                    }
                    else
                    {
                        // Error!
                        ErrorString = ReturnValues[3].Trim(char.Parse("|")) + " (" + ReturnValues[2].Trim(char.Parse("|")) + ")";

                        if (ReturnValues[2].Trim(char.Parse("|")) == "44")
                        {
                            // CCV transaction decline
                            ErrorString += "Credit Card Code Verification (CCV) returned the following error: ";

                            switch (ReturnValues[38].Trim(char.Parse("|")))
                            {
                                case "N":
                                    ErrorString += "Card Code does not match.";
                                    break;
                                case "P":
                                    ErrorString += "Card Code was not processed.";
                                    break;
                                case "S":
                                    ErrorString += "Card Code should be on card but was not indicated.";
                                    break;
                                case "U":
                                    ErrorString += "Issuer was not certified for Card Code.";
                                    break;
                            }
                        }

                        if (ReturnValues[2].Trim(char.Parse("|")) == "17")
                        {
                            // wrong credit card 
                            ErrorString += "The merchant accepts Visa, MasterCard, Discover, Diners Club and JCB ";                         
                        }

                        if (ReturnValues[2].Trim(char.Parse("|")) == "45")
                        {
                            if (ErrorString.Length > 1)
                                ErrorString += "<br />n";

                            // AVS transaction decline
                            ErrorString += "Address Verification System (AVS) " +
                              "returned the following error: ";

                            switch (ReturnValues[5].Trim(char.Parse("|")))
                            {
                                case "A":
                                    ErrorString += " the zip code entered does not match the billing address.";
                                    break;
                                case "B":
                                    ErrorString += " no information was provided for the AVS check.";
                                    break;
                                case "E":
                                    ErrorString += " a general error occurred in the AVS system.";
                                    break;
                                case "G":
                                    ErrorString += " the credit card was issued by a non-US bank.";
                                    break;
                                case "N":
                                    ErrorString += " neither the entered street address nor zip code matches the billing address.";
                                    break;
                                case "P":
                                    ErrorString += " AVS is not applicable for this transaction.";
                                    break;
                                case "R":
                                    ErrorString += " please retry the transaction; the AVS system was unavailable or timed out.";
                                    break;
                                case "S":
                                    ErrorString += " the AVS service is not supported by your credit card issuer.";
                                    break;
                                case "U":
                                    ErrorString += " address information is unavailable for the credit card.";
                                    break;
                                case "W":
                                    ErrorString += " the 9 digit zip code matches, but the street address does not.";
                                    break;
                                case "Z":
                                    ErrorString += " the zip code matches, but the address does not.";
                                    break;
                            }
                        }

                        // ErrorString contains the actual error
                        resultSpan.InnerHtml  = ErrorString;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    resultSpan.InnerHtml = ex.Message;
                    return false;
                }
            }
        }
        return false;
    }
    //protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = true;
    //    for (int i = 0; i <= 3000; i++)
    //    {

    //    }
    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "fancyBox", "Loading(false);", true);
    //    AuthorizePayment();
    //    if (CustomValidator1.ErrorMessage.Length > 0)
    //    {
    //        args.IsValid = false;
    //    }
    //    else
    //    {
    //        bool status = Credentialpage.Utility.update_Coststatus(Request.QueryString["id"].ToString(), Session["Customer_id"].ToString(), authorizecode.Text.ToString(), transcode.Text.ToString(), Dropcardtype.SelectedValue.ToString());
    //        //Processed so send the user to a Thank You Page
    //        string res = "";
    //        if (status) { res = "True"; } else { res = "False"; }

    //        if (Subdomain == "nosubdomain")
    //        {
    //            Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=" + status + "&mode=" + Session["mode"].ToString());
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=" + status + "&mode=" + Session["mode"].ToString() + "&subdomain=" + Subdomain);
    //        }
    //    }
    //}
 
    protected void SubmitButton_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate("Authorize");
        if (Page.IsValid)
        {            
           bool result = AuthorizePayment();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "fancyBox", "Loading(false);", true);
            if (result)
        {
            bool status = Credentialpage.Utility.update_Coststatus(Request.QueryString["id"].ToString(), Session["Customer_id"].ToString(),authorizecode.Text.ToString(),transcode.Text.ToString(),Dropcardtype.SelectedValue.ToString());
            //Processed so send the user to a Thank You Page
            if (Subdomain == "nosubdomain")
            {
            Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=" + status + "&mode=" + Session["mode"].ToString());
        }
            else
            {
                Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=" + status + "&mode=" + Session["mode"].ToString()+ "&subdomain=" + Subdomain);
            }
        }
    }
        else
    {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "fancyBox", "Loading(false);", true);
       
    }
       
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        if (Subdomain == "nosubdomain")
        {
        Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=post" + "&mode=" + Session["mode"].ToString());
    }
        else
        {
            Response.Redirect("~/Thanku.aspx?id=" + Session["fno"].ToString() + "&valid=post" + "&mode=" + Session["mode"].ToString() + "&subdomain=" + Subdomain);
        }
       
    }
}



