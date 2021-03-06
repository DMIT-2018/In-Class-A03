﻿using eRestaurant.BLL;
using eRestaurant.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Staff_SplitBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SelectBill_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(GetBill);
    }
    private void GetBill()
    {
        var controller = new WaiterController();
        var data = controller.GetBill(int.Parse(ActiveBills.SelectedValue));
        BillToSplit.Value = data.BillID.ToString();

        // Set the original bill items
        OriginalBillItems.DataSource = data.Items;
        OriginalBillItems.DataBind();
        UpdateTotalBill(OriginalBillItems, OriginalTotal);

        // empty out other bill
        NewBillItems.DataSource = null;
        NewBillItems.DataBind();
    }
    protected void BillItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        e.Cancel = true; // to prevent any other processing in the GridView's default Select handling

        GridView sendingGridView = sender as GridView;
        GridView receivingGridView;
        Label sendingTotalLabel, receivingTotalLabel;
        if (sendingGridView == OriginalBillItems)
        {
            receivingGridView = NewBillItems;
            receivingTotalLabel = NewTotal;
            sendingTotalLabel = OriginalTotal;
        }
        else
        {
            receivingGridView = OriginalBillItems;
            receivingTotalLabel = OriginalTotal;
            sendingTotalLabel = NewTotal;
        }


        GridViewRow row = sendingGridView.Rows[e.NewSelectedIndex];

        // 1) get the info from the column
        OrderItem item = GetOrderItem(row);

        // 2) move it to the other gridview
        List<OrderItem> newItems = GetRowsFrom(receivingGridView);
        newItems.Add(item);
        receivingGridView.DataSource = newItems;
        receivingGridView.DataBind();
        
        // 3) take it out of this list
        List<OrderItem> myItems = GetRowsFrom(sendingGridView);
        myItems.RemoveAt(e.NewSelectedIndex);
        sendingGridView.DataSource = myItems;
        sendingGridView.DataBind();

        // 4) update the totals
        UpdateTotalBill(sendingGridView, sendingTotalLabel);
        UpdateTotalBill(receivingGridView, receivingTotalLabel);

        // 5) happy dance
    }

    private void UpdateTotalBill(GridView aGridView, Label aLabel)
    {
        var data = GetRowsFrom(aGridView);
        decimal total = 0.00m;
        foreach (var item in data)
            total += item.Price * item.Quantity;
        aLabel.Text = string.Format("{0:C}", total);
    }

    private List<OrderItem> GetRowsFrom(GridView theGridView)
    {
        List<OrderItem> result = new List<OrderItem>();
        foreach(GridViewRow row in theGridView.Rows)
        {
            var data = GetOrderItem(row);
            result.Add(data);
        }
        return result;
    }

    private OrderItem GetOrderItem(GridViewRow row)
    {
        var qtyLabel = row.FindControl("Quantity") as Label;
        var nameLabel = row.FindControl("ItemName") as Label;
        var priceLabel = row.FindControl("Price") as Label;
        var result = new OrderItem()
        {
            Quantity = int.Parse(qtyLabel.Text),
            ItemName = nameLabel.Text,
            Price = decimal.Parse(priceLabel.Text)
        };
        return result;
    }

    protected void SplitBill_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(SplitTheBill);
    }

    private void SplitTheBill()
    {
        var originalItems = GetRowsFrom(OriginalBillItems);
        //var newItems = GetRowsFrom(NewBillItems);
        // The long version of the line above....
        List<OrderItem> newItems = new List<OrderItem>();
        foreach (GridViewRow row in NewBillItems.Rows)
        {
            var qtyLabel = row.FindControl("Quantity") as Label;
            var nameLabel = row.FindControl("ItemName") as Label;
            var priceLabel = row.FindControl("Price") as Label;

            int qty;
            decimal price;

            // a place to put an if, should I need to get from a different control.
            if (qtyLabel != null)
                qty = int.Parse(qtyLabel.Text);
            else
                qty = 0; // or from another control

            price = decimal.Parse(priceLabel.Text);

            var data = new OrderItem()
            {
                Quantity = qty,
                ItemName = nameLabel.Text,
                Price = price
            };

            newItems.Add(data);
        }

        int billId = int.Parse(BillToSplit.Value);

        WaiterController controller = new WaiterController();
        controller.SplitBill(billId, originalItems, newItems);
    }
}