<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageMenuItems.aspx.cs" Inherits="Admin_ManageMenuItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Manage Menu Items</h1>

        <asp:LinkButton ID="UpdateAllItems" runat="server" CssClass="btn btn-primary" OnClick="UpdateAllItems_Click">Update all Menu Items</asp:LinkButton>
        <asp:GridView ID="MenuItemsGridView" runat="server" AutoGenerateColumns="False" DataSourceID="MenuItemDataSource"
            ItemType="eRestaurant.Entities.Item">
            <Columns>
                <asp:TemplateField HeaderText="CurrentPrice" SortExpression="CurrentPrice">
                    <ItemTemplate>
                        <asp:HiddenField ID="ItemID" runat="server" Value="<%# Item.ItemID %>" />

                        Description:
                        <asp:TextBox ID="Description" runat="server" Text='<%# Item.Description %>' />
                        Calories: 
                        <asp:TextBox ID="Calories" runat="server" Text='<%# Item.Calories %>' />
                        Current Cost ($): 
                        <asp:TextBox ID="CurrentCost" runat="server" Text='<%# Item.CurrentCost %>' />
                        <asp:CheckBox ID="IsActive" runat="server" Checked="<%# Item.Active %>" Text="Is Active" />
<%--                        <br />
                        Comment: 
                        <asp:TextBox ID="Comment" runat="server" Text='<%# Item.Comment %>' TextMode="MultiLine" Width="100%" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="MenuItemDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllItemsAlphabetical" TypeName="eRestaurant.BLL.RestaurantAdminController"></asp:ObjectDataSource>
    </div>
</asp:Content>

