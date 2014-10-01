<Query Kind="Expression">
  <Connection>
    <ID>90484823-35fd-487c-8da7-087800aafd13</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from cat in MenuCategories
orderby cat.Description
select new
{
  Description = cat.Description,
  MenuItems = from item in cat.Items
              where item.Active
              orderby item.Description
              select new
              {
                Description = item.Description,
                Price = item.CurrentPrice,
                Calories = item.Calories,
                Comment = item.Comment
              }
}