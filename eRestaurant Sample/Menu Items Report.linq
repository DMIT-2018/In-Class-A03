<Query Kind="Expression">
  <Connection>
    <ID>50e535df-8b6d-4a21-b5d3-dfde364563ec</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// This query is for pulling out data to be used in a
// Details report. The query gets all the menu items
// and their categories, sorting them by the category
// description and then by the menu item description.
from cat in Items
orderby cat.MenuCategory.Description, cat.Description
select new
{
    CategoryDescription = cat.MenuCategory.Description,
    ItemDescription = cat.Description,
    Price = cat.CurrentPrice,
    Calories = cat.Calories,
    Comment = cat.Comment
}