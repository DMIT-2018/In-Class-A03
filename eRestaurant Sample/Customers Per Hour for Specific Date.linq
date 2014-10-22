<Query Kind="Expression">
  <Connection>
    <ID>90484823-35fd-487c-8da7-087800aafd13</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// How many customers per hour on Sept. 15, 2014.
from info in Bills
where info.BillDate.Year == 2014
   && info.BillDate.Month == 9
   && info.BillDate.Day == 15
group info by info.BillDate.Hour into infoGroup
select new 
{
   Hour = infoGroup.Key,
   CustomerBillCount = infoGroup.Count(),
   CustomersCount = infoGroup.Sum(x=>x.NumberInParty)
}
