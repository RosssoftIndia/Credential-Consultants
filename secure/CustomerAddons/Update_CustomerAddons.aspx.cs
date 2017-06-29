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
using CKEditor.NET;


public partial class secure_CustomerAddons_Update_CustomerAddons : System.Web.UI.Page
{
    public string[] str1;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                Session["Client_id"] = Request.QueryString["clid"];  
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }  

    protected void Page_Load(object sender, EventArgs e)
    {

    }  

    protected void Update_Click(object sender, EventArgs e)
    {
        
        TextBox dlcount = (TextBox)DetailsView_customer.FindControl("dlcopy");
        CKEditorControl msg1 = (CKEditorControl)DetailsView_customer.FindControl("dlinst");
        CKEditorControl msg2 = (CKEditorControl)DetailsView_customer.FindControl("edinst");
        CKEditorControl url = (CKEditorControl)DetailsView_customer.FindControl("url");
        CKEditorControl toc = (CKEditorControl)DetailsView_customer.FindControl("txt_toc");       
        RadioButtonList option1 = (RadioButtonList)DetailsView_customer.FindControl("thankuoption");
        TextBox logintxt = (TextBox)DetailsView_customer.FindControl("logintextbox");
        TextBox weburl = (TextBox)DetailsView_customer.FindControl("WebUrl");
        TextBox transkeytxt = (TextBox)DetailsView_customer.FindControl("transkeybox");
        TextBox mailid = (TextBox)DetailsView_customer.FindControl("mailid");
        CheckBoxList CheckBoxList1 = (CheckBoxList)DetailsView_customer.FindControl("CheckBoxList1");
        Label clientid = (Label)DetailsView_customer.FindControl("lblclientid");

        //payment section
        RadioButtonList option = (RadioButtonList)DetailsView_customer.FindControl("searchoption");
        RadioButtonList Onlinecc = (RadioButtonList)DetailsView_customer.FindControl("Onlinecc");
        RadioButtonList morder = (RadioButtonList)DetailsView_customer.FindControl("morder");
        RadioButtonList pcheck = (RadioButtonList)DetailsView_customer.FindControl("pcheck");
        CKEditorControl CreditCardinst = (CKEditorControl)DetailsView_customer.FindControl("CreditCardinst");
        CKEditorControl morderinst = (CKEditorControl)DetailsView_customer.FindControl("morderinst");
        CKEditorControl pcheckinst = (CKEditorControl)DetailsView_customer.FindControl("pcheckinst");

        //service section
        RadioButtonList Apptypeoption = (RadioButtonList)DetailsView_customer.FindControl("List_App_type");
        CheckBox chk_Mail = (CheckBox)DetailsView_customer.FindControl("chk_Mail");
        CheckBox chk_Fax = (CheckBox)DetailsView_customer.FindControl("chk_Fax");
        CheckBox chk_Email = (CheckBox)DetailsView_customer.FindControl("chk_Email");
        //purpose section
        RadioButtonList Purpose_Section = (RadioButtonList)DetailsView_customer.FindControl("Purposeoption");
        TextBox frm4_institution = (TextBox)DetailsView_customer.FindControl("frm4_institution");
        TextBox frm4_lawfirm = (TextBox)DetailsView_customer.FindControl("frm4_lawfirm");
        TextBox frm4_military = (TextBox)DetailsView_customer.FindControl("frm4_military");
        TextBox frm4_organization = (TextBox)DetailsView_customer.FindControl("frm4_organization");
        TextBox frm4_state = (TextBox)DetailsView_customer.FindControl("frm4_state");
        TextBox frm4_board = (TextBox)DetailsView_customer.FindControl("frm4_board");
        TextBox frm4_evaluation = (TextBox)DetailsView_customer.FindControl("frm4_evaluation");
        //talent db
        RadioButtonList tdb = (RadioButtonList)DetailsView_customer.FindControl("tdbuoption");

        //review section
        CKEditorControl splinst = (CKEditorControl)DetailsView_customer.FindControl("splinst");
        CKEditorControl complinst = (CKEditorControl)DetailsView_customer.FindControl("complinst");
        RadioButtonList Uploadoption = (RadioButtonList)DetailsView_customer.FindControl("Uploadoption");

        int credit = 0;

        string hashlogin = "";
        string hashtranskey = "";
        string temp = "";
        string siteurl = "";
        string vartype = "";    
        if (option.SelectedIndex == 0)
        {
            temp = logintxt.Text.Trim();
            hashlogin = ClientAdmin.Utility.base64Encode(temp);
            credit = 1;
            temp = "";
            temp = transkeytxt.Text.Trim();
            hashtranskey = ClientAdmin.Utility.base64Encode(temp);        
        }
        else
        {
            credit = 0;
            hashlogin = "";
            hashtranskey = "";
            mailid.Text = "";
        }
        if (option1.SelectedIndex == 0)
        {
            siteurl = weburl.Text;
        }
        else
        {
            siteurl = "";
        }

        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            if (CheckBoxList1.Items[i].Selected)
            {

                vartype += CheckBoxList1.Items[i].Value + "|";
            }
        }
        vartype = vartype.TrimEnd('|');


        //purposelock
         
     string Lock_TargetName="";
     string Lock_State = "";
     if (Purpose_Section.SelectedIndex == 0)
     {
         switch (frm4_option_purpose.SelectedItem.ToString())
         {
             case "Admission to High School":
                 Lock_TargetName = frm4_institution.Text;
                 break;
             case "Admission to College/University":
                 Lock_TargetName = frm4_institution.Text;
                 break;
             case "Employment":
                 Lock_TargetName = frm4_organization.Text;
                 break;
             case "Immigration":
                 Lock_TargetName = frm4_lawfirm.Text;
                 break;
             case "Professional Licensing/Registration":
                 Lock_TargetName = frm4_board.Text;
                 Lock_State = frm4_state.Text;
                 break;
             case "Military":
                 Lock_TargetName = frm4_military.Text;
                 break;
             case "Other":
                 Lock_TargetName = frm4_evaluation.Text;
                 break;
         }
     }
     else
     {
         frm4_option_purpose.SelectedIndex = 0; 
         Lock_TargetName = "";
         Lock_State = "";
     }
      


        bool result = false;
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                result = ClientAdmin.Utility.Grid_CustomerAdonsUpdate(Convert.ToInt32(dlcount.Text),msg1.Text, msg2.Text, clientid.Text, url.Text, toc.Text.ToString(), credit, hashlogin, hashtranskey, mailid.Text, option1.SelectedValue.ToString(), vartype, siteurl, chk_Mail.Checked.ToString(), chk_Fax.Checked.ToString(), chk_Email.Checked.ToString(), tdb.SelectedValue.ToString(), Purpose_Section.SelectedValue.ToString(), frm4_option_purpose.SelectedValue.ToString(), Lock_TargetName, Lock_State, splinst.Text, complinst.Text, Uploadoption.SelectedValue.ToString(), Apptypeoption.SelectedValue.ToString(), Targetoption.SelectedValue.ToString(), Convert.ToInt32(Onlinecc.SelectedValue.ToString()), Convert.ToInt32(morder.SelectedValue.ToString()), Convert.ToInt32(pcheck.SelectedValue.ToString()), CreditCardinst.Text, morderinst.Text, pcheckinst.Text);      
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }           
      
        if (result == true)
        {
            Response.Redirect("~/secure/CustomerAddons/Browse_CustomerAddons.aspx?clid=" + clientid.Text);
        }
    }
  
    protected void DetailsView_customer_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = ClientAdmin.Utility.Grid_CustomerAdonsSelect(DetailsView_customer, Session["Client_id"].ToString());
            TextBox logintxt = (TextBox)DetailsView_customer.FindControl("logintextbox"); 
            TextBox transkeytxt = (TextBox)DetailsView_customer.FindControl("transkeybox");
            RadioButtonList option = (RadioButtonList)DetailsView_customer.FindControl("searchoption");
            RadioButtonList option1 = (RadioButtonList)DetailsView_customer.FindControl("thankuoption");          
            Label l1 = (Label)DetailsView_customer.FindControl("tempchk1");
            Label l2 = (Label)DetailsView_customer.FindControl("tempchk2");
            logintxt.Text = ClientAdmin.Utility.base64Decode(l1.Text);
            transkeytxt.Text = ClientAdmin.Utility.base64Decode(l2.Text);
            HtmlGenericControl block1 = (HtmlGenericControl)DetailsView_customer.FindControl("creditinfoblock");
            HtmlGenericControl block2 = (HtmlGenericControl)DetailsView_customer.FindControl("Thankublock");
            TextBox weburl = (TextBox)DetailsView_customer.FindControl("WebUrl");
            weburl.Text = ds.Tables[0].Rows[0]["SiteUrl"].ToString();
            CheckBoxList CheckBoxList1 = (CheckBoxList)DetailsView_customer.FindControl("CheckBoxList1");

            //purpose section
            HtmlTableRow frm4_optional = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional");
            HtmlTableRow frm4_optional1 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional1");
            HtmlTableRow frm4_optional2 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional2");
            HtmlTableRow frm4_optional3 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional3");
            HtmlTableRow frm4_optional4 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional4");
            HtmlTableRow frm4_optional5 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional5");
            RadioButtonList Purpose_Section = (RadioButtonList)DetailsView_customer.FindControl("Purposeoption");
            TextBox frm4_institution = (TextBox)DetailsView_customer.FindControl("frm4_institution");
            TextBox frm4_lawfirm = (TextBox)DetailsView_customer.FindControl("frm4_lawfirm");
            TextBox frm4_military = (TextBox)DetailsView_customer.FindControl("frm4_military");
            TextBox frm4_organization = (TextBox)DetailsView_customer.FindControl("frm4_organization");
            TextBox frm4_state = (TextBox)DetailsView_customer.FindControl("frm4_state");
            TextBox frm4_board = (TextBox)DetailsView_customer.FindControl("frm4_board");
            TextBox frm4_evaluation = (TextBox)DetailsView_customer.FindControl("frm4_evaluation");
            TextBox Lock_TargetName = (TextBox)DetailsView_customer.FindControl("Lock_TargetName");
            TextBox Lock_State = (TextBox)DetailsView_customer.FindControl("Lock_State");
            TextBox Lock_PurposeId = (TextBox)DetailsView_customer.FindControl("Lock_PurposeId");
            HtmlTable pruposeblock = (HtmlTable)DetailsView_customer.FindControl("purposeblock");

            ClientAdmin.Utility.Getcard_type(CheckBoxList1);
            str1 = ds.Tables[0].Rows[0]["Credit_Type"].ToString().Split('|');
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                for (int j = 0; j <= str1.Length - 1; j++)
                {
                    if (CheckBoxList1.Items[i].Value == str1[j].ToString())
                    {
                        CheckBoxList1.Items[i].Selected = true;
                    }
                }
            }       
            if (option.SelectedIndex == 0)
            {
                block1.Visible = true;                         
            }
            else
            {
                block1.Visible = false;
            }
            if (option1.SelectedIndex == 0)
            {
                block2.Visible = true;
            }
            else
            {
                block2.Visible = false;
            }
            DropDownList frm4_option_purpose =(DropDownList)DetailsView_customer.FindControl("frm4_option_purpose");
            Credentialpage.Utility.Getpurpose(frm4_option_purpose);
                
            frm4_option_purpose.SelectedValue = Lock_PurposeId.Text; 
            //purpose
            frm4_optional.Visible = false;
            frm4_optional1.Visible = false;
            frm4_optional2.Visible = false;
            frm4_optional3.Visible = false;
            frm4_optional4.Visible = false;
            frm4_optional5.Visible = false;
            if (Purpose_Section.SelectedIndex == 0)
            {
                pruposeblock.Visible = true;
                targetblock.Visible = true;
                switch (frm4_option_purpose.SelectedItem.ToString())
                {
                    case "Admission to High School":
                        frm4_optional.Visible = true;
                        frm4_institution.Text = Lock_TargetName.Text;
                        break;
                    case "Admission to College/University":
                        frm4_optional.Visible = true;
                        frm4_institution.Text = Lock_TargetName.Text;
                        break;
                    case "Employment":
                        frm4_optional1.Visible = true;
                        frm4_organization.Text = Lock_TargetName.Text;
                        break;
                    case "Immigration":
                        frm4_optional2.Visible = true;
                        frm4_lawfirm.Text = Lock_TargetName.Text;
                        break;
                    case "Professional Licensing/Registration":
                        frm4_optional3.Visible = true;
                        frm4_board.Text = Lock_TargetName.Text;
                        frm4_state.Text = Lock_State.Text;
                        break;
                    case "Military":
                        frm4_optional4.Visible = true;
                        frm4_military.Text = Lock_TargetName.Text;
                        break;
                    case "Other":
                        frm4_optional5.Visible = true;
                        frm4_evaluation.Text = Lock_TargetName.Text;
                        break;
                }
            }
            else
            {
                purposeblock.Visible = false; 
                frm4_institution.Text = "";
                frm4_lawfirm.Text = "";
                frm4_military.Text = "";
                frm4_organization.Text = "";
                frm4_state.Text = "";
                frm4_board.Text = "";
                frm4_evaluation.Text = "";
            }
        }
    }
    protected void searchoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList r = (RadioButtonList)sender;
        HtmlGenericControl block = (HtmlGenericControl)DetailsView_customer.FindControl("creditinfoblock");
        if (r.SelectedIndex == 0)
        {
            block.Visible = true;
        }
        else
        {
            block.Visible = false;
        }
    }
    protected void thankuoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList r = (RadioButtonList)sender;
        HtmlGenericControl block1 = (HtmlGenericControl)DetailsView_customer.FindControl("Thankublock");
        if (r.SelectedIndex == 0)
        {
            block1.Visible = true;
        }
        else
        {
            block1.Visible = false;
        }
    }
    protected void frm4_option_purpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList frm4_option_purpose = (DropDownList)sender;

        HtmlTableRow frm4_optional = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional");
        HtmlTableRow frm4_optional1 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional1");
        HtmlTableRow frm4_optional2 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional2");
        HtmlTableRow frm4_optional3 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional3");
        HtmlTableRow frm4_optional4 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional4");
        HtmlTableRow frm4_optional5 = (HtmlTableRow)DetailsView_customer.FindControl("frm4_optional5");
        TextBox frm4_institution = (TextBox)DetailsView_customer.FindControl("frm4_institution");
        TextBox frm4_lawfirm = (TextBox)DetailsView_customer.FindControl("frm4_lawfirm");
        TextBox frm4_military = (TextBox)DetailsView_customer.FindControl("frm4_military");
        TextBox frm4_organization = (TextBox)DetailsView_customer.FindControl("frm4_organization");
        TextBox frm4_state = (TextBox)DetailsView_customer.FindControl("frm4_state");
        TextBox frm4_board = (TextBox)DetailsView_customer.FindControl("frm4_board");
        TextBox frm4_evaluation = (TextBox)DetailsView_customer.FindControl("frm4_evaluation");
        frm4_institution.Text = "";
        frm4_lawfirm.Text = "";
        frm4_military.Text = "";
        frm4_organization.Text = "";
        frm4_state.Text = "";
        frm4_board.Text = "";
        frm4_evaluation.Text = "";
        frm4_optional.Visible = false;
        frm4_optional1.Visible = false;
        frm4_optional2.Visible = false;
        frm4_optional3.Visible = false;
        frm4_optional4.Visible = false;
        frm4_optional5.Visible = false;
        targetblock.Visible = false; 
        switch (frm4_option_purpose.SelectedItem.ToString())
        {
            case "Admission to High School":
                targetblock.Visible = true;
                frm4_optional.Visible = true;              
                break;
            case "Admission to College/University":
                targetblock.Visible = true;
                frm4_optional.Visible = true;              
                break;
            case "Employment":               
                targetblock.Visible = true;
                frm4_optional1.Visible = true;               
                break;
            case "Immigration":               
                targetblock.Visible = true;
                frm4_optional2.Visible = true;               
                break;
            case "Professional Licensing/Registration":                
                targetblock.Visible = true;
                frm4_optional3.Visible = true;                
                break;
            case "Military":              
                targetblock.Visible = true;
                frm4_optional4.Visible = true;         
                break;
            case "Other":             
                targetblock.Visible = true;
                frm4_optional5.Visible = true;
                break;          
        }

    }
    protected void Purposeoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList purpose = (RadioButtonList)sender;

        HtmlTable block = (HtmlTable)DetailsView_customer.FindControl("purposeblock");
        if (purpose.SelectedIndex == 0)
        {
            block.Visible = true;
        }
        else
        {
            block.Visible = false;
        }
    }
    protected void DetailsView_customer_DataBound(object sender, EventArgs e)
    {
        Label clienttop = (Label)DetailsView_customer.FindControl("clienttop");
        Label clientbottom = (Label)DetailsView_customer.FindControl("clientbottom");
        string client = ClientAdmin.Utility.GetclientName(Convert.ToInt32(Session["Client_id"].ToString()));
        clienttop.Text = client;
        clientbottom.Text = client;
    }
}
