<Query Kind="Program">
  <Connection>
    <ID>90484823-35fd-487c-8da7-087800aafd13</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	var data = from cat in MenuCategories
				orderby cat.Description
				select new Category()
				{
				Description = cat.Description,
				MenuItems = from item in cat.Items
							where item.Active
							orderby item.Description
							select new MenuItem()
							{
								Description = item.Description,
								Price = item.CurrentPrice,
								Calories = item.Calories,
								Comment = item.Comment
							}
			};
	data.Dump();
}

// Define other methods and classes here
public class Category
{
    public string Description { get;set;}
	public IEnumerable MenuItems { get;set;}
}
public class MenuItem
{
	public string Description { get;set;}
	public decimal Price { get;set;}
	public int? Calories {get;set;}
	public string Comment { get;set;}
}





