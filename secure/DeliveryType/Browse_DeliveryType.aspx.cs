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

public partial class secure_DeliveryType_Browse_DeliveryType : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        switch (Session["Authenticate"].ToString())
        {
            case "Approved":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
    {      
            ClientAdmin.Utility.GetSubclients(dpsubclients, Convert.ToInt32(Session["Admin_Customer"].ToString()), false);
            if (Request.QueryString["clid"] != null)
        {
                dpsubclients.SelectedValue = Request.QueryString["clid"].ToString();
        }    
       
    }
        Addlink.HRef = "Add_DeliveryType.aspx?clid=" + dpsubclients.SelectedValue.ToString();
        Action(dpsubclients.SelectedValue.ToString()); 
    }   
  
   
    protected void grid_Delivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Delivery.PageIndex = e.NewPageIndex;       
        Action(dpsubclients.SelectedValue.ToString());     
        
    }
    protected void up_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton thisbutton = (ImageButton)sender;
        GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
        HiddenField hiddenorder = (HiddenField)(thisrow.FindControl("txtorder"));
        HiddenField hiddenid = (HiddenField)(thisrow.FindControl("txtid"));
        string thisorder = hiddenorder.Value;
        string thispart = hiddenid.Value;

        GridView thisgrid = grid_Delivery; 
        int maxrows = thisgrid.Rows.Count;
        int row = thisrow.RowIndex;
        int previousrownumber = row - 1;
        if (previousrownumber != -1)
        {
            GridViewRow previousrow = thisgrid.Rows[previousrownumber];
            hiddenorder = (HiddenField)(previousrow.FindControl("txtorder"));
            hiddenid = (HiddenField)(previousrow.FindControl("txtid"));
            string nextorder = hiddenorder.Value;
            string nextpart = hiddenid.Value;
            if (thisorder != "")
            {
                ClientAdmin.Utility.Grid_DeliveryTypeOrderUpdate(thispart, nextorder);
                ClientAdmin.Utility.Grid_DeliveryTypeOrderUpdate(nextpart, thisorder);

            }
            Action(dpsubclients.SelectedValue.ToString());         
        }

    }
    protected void down_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton thisbutton = (ImageButton)sender;
        GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
        HiddenField hiddenorder = (HiddenField)(thisrow.FindControl("txtorder"));
        HiddenField hiddenid = (HiddenField)(thisrow.FindControl("txtid"));
        string thisorder = hiddenorder.Value;
        string thispart = hiddenid.Value;
        GridView thisgrid = grid_Delivery;
        int maxrows = thisgrid.Rows.Count;
        int row = thisrow.RowIndex;
        int nextrownumber = row + 1;
        if (nextrownumber != maxrows)
        {
            GridViewRow nextrow = thisgrid.Rows[nextrownumber];
            hiddenorder = (HiddenField)(nextrow.FindControl("txtorder"));
            hiddenid = (HiddenField)(nextrow.FindControl("txtid"));
            string nextorder = hiddenorder.Value;
            string nextpart = hiddenid.Value;
            if (thisorder != "")
            {
                ClientAdmin.Utility.Grid_DeliveryTypeOrderUpdate(thispart, nextorder);
                ClientAdmin.Utility.Grid_DeliveryTypeOrderUpdate(nextpart, thisorder);
   
}
            Action(dpsubclients.SelectedValue.ToString());
         
        }

    }

    protected void dpsubclients_SelectedIndexChanged(object sender, EventArgs e)
    {
        Addlink.HRef = "Add_DeliveryType.aspx?clid=" + dpsubclients.SelectedValue.ToString();
        UpdatePanel2.Update();  
        Action(dpsubclients.SelectedValue.ToString());
      
    }
    public void Action(string clientid)
    {
        switch (Session["Admin_Type"].ToString())
        {
            case "USER":
                ClientAdmin.Utility.Grid_DeliveryTypeBrowse(grid_Delivery, clientid.ToString());
                break;
            case "ADMIN":
                break;
            default:
                Response.Redirect("~/Fail.aspx");
                break;
        }    
    }
   
    protected void del_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton deletebtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)deletebtn.Parent.Parent as GridViewRow;
        HiddenField rowid = (HiddenField)grdRow.FindControl("txtid");
        HiddenField hiddenorder = (HiddenField)grdRow.FindControl("txtorder");
        bool result = ClientAdmin.Utility.del_Delivery(rowid.Value);


        if (result)
        {
            int nextrowno = grdRow.RowIndex + 1;
            int order = Convert.ToInt32(hiddenorder.Value);
            for (int i = nextrowno; i <= grid_Delivery.Rows.Count - 1; i++)
            {
                GridViewRow nextrow = grid_Delivery.Rows[i];
                HiddenField nrowid = (HiddenField)nextrow.FindControl("txtid");
                ClientAdmin.Utility.Grid_DeliveryTypeOrderUpdate(nrowid.Value, order.ToString());
                order++;
            }


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deleted successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Record deletion failed.');", true);
        }
        Action(dpsubclients.SelectedValue.ToString());
    }
}
