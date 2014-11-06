using eRestaurant.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Staff_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(SetMockedTimeToLastBill);
    }

    private void SetMockedTimeToLastBill()
    {
        var controller = new AdHocController();
        var info = controller.GetLastBillDateTime();
        // formatting date for use in an <input type="date"> HTML5 control
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        
        // formatting time for use in an <input type="time"> HTML5 control
        SearchTime.Text = info.ToString("HH:mm:ss"); // HH is 24 hour clock, hh is 12 hour clock
    }

    protected void SeatingGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        // Seat walk-in customers
        MessageUserControl.TryRun(() =>
        {
            // TODO: There are a lot of assumptions in parsing the input, and it would be better
            //       to break this into chunks an display appropriate "usage" messages to the end-user.
            // Get the controls
            GridViewRow row = SeatingGridView.Rows[e.NewSelectedIndex];
            var tableControl = row.FindControl("TableNumber") as Label;
            var numberInPartyControl = row.FindControl("NumberInParty") as TextBox;
            var waiterListControl = row.FindControl("WaiterList") as DropDownList;
            var when = DateTime.Parse(SearchDate.Text).Add(TimeSpan.Parse(SearchTime.Text));
            // Seat the customer
            var controller = new SeatingController();
            controller.SeatCustomer(when, byte.Parse(tableControl.Text), int.Parse(numberInPartyControl.Text), int.Parse(waiterListControl.SelectedValue));
            // Refresh the gridview
            SeatingGridView.DataBind();
        }, "Customer Seated", "New walk-in customer has been seated");
    }
}