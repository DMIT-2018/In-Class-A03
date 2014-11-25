using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageMenuItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void UpdateAllItems_Click(object sender, EventArgs e)
    {
        List<Item> updatedMenuItemInfo = new List<Item>();
        // Gather up all the data from my GridView
        foreach(GridViewRow row in MenuItemsGridView.Rows)
        {
            // Find controls in that row
            var itemIdCtrl = row.FindControl("ItemID") as HiddenField;
            var descriptionCtrl = row.FindControl("Description") as TextBox;
            var caloriesCtrl = row.FindControl("Calories") as TextBox;
            var costCtrl = row.FindControl("CurrentCost") as TextBox;
            var isActiveCtrl = row.FindControl("IsActive") as CheckBox;

            var item = new Item()
            {
                // TODO: put values here....
            };

            updatedMenuItemInfo.Add(item);
        }
        // TODO: Push to the BLL for processing

    }
}