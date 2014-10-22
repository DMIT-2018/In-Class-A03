<Query Kind="Statements">
  <Connection>
    <ID>90484823-35fd-487c-8da7-087800aafd13</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// Total Item Sales Report
var results = from info in BillItems
              orderby info.Item.MenuCategory.Description, info.Item.Description
              select new
              {
                    CategoryDescription = info.Item.MenuCategory.Description,
                    ItemDescription = info.Item.Description,
                    Quantity = info.Quantity,
                    Price = info.SalePrice * info.Quantity,
                    Cost = info.UnitCost * info.Quantity
              };
results.Count().Dump();              
results.Dump();
