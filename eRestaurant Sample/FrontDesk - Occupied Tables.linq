<Query Kind="Statements">
  <Connection>
    <ID>50e535df-8b6d-4a21-b5d3-dfde364563ec</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// Find out occupancy information on the tables in the restaurant at a specific date/time

// Create a date and time object to use for sample input data
var date = Bills.Max(b => b.BillDate).Date;
var time = Bills.Max(b => b.BillDate).TimeOfDay.Add(new TimeSpan(0, 30, 0));
date.Add(time).Dump("The test date/time I am using");

// Step 1 - Get the table info along with any walk-in bills and reservation bills for the specific time slot
var step1 = from data in Tables
			select new
			{
				Table = data.TableNumber,
				Seating = data.Capacity,
				// This sub-query gets the bills for walk-in customers
				Bills = from billing in data.Bills
						where 
							   billing.BillDate.Year == date.Year
							&& billing.BillDate.Month == date.Month
							&& billing.BillDate.Day == date.Day
							&& billing.BillDate.TimeOfDay <= time
							&& (!billing.OrderPaid.HasValue || billing.OrderPaid >= time)
//							&& (!billing.PaidStatus || billing.OrderPaid >= time)
						select billing,
				// This sub-query gets the bills for reservations
				Reservations = from booking in data.ReservationTables // drill to the first collection
						from billing in booking.Reservation.Bills // drill to the next collection
						where 
							   billing.BillDate.Year == date.Year
							&& billing.BillDate.Month == date.Month
							&& billing.BillDate.Day == date.Day
							&& billing.BillDate.TimeOfDay <= time
							&& (!billing.OrderPaid.HasValue || billing.OrderPaid >= time)
//							&& (!billing.PaidStatus || billing.OrderPaid >= time)
						select billing
			};
step1.Dump("Step 1 of my queries");

// Step 2 - Union the walk-in bills and the reservation bills while extracting the relevant bill info
// .ToList() helps to resolve the "Types in Union of Concat are constructed incompatibly" error
var step2 = from data in step1.ToList() // .ToList() forces the first result set to be in-memory
			select new
			{
				Table = data.Table,
				Seating = data.Seating,
				CommonBilling = from info in data.Bills.Union(data.Reservations)
								select info
			};
step2.Dump("Step 2 of my queries - unioning the result");

// Step 3 - Get just the first CommonBilling item
//         (presumes no overlaps can occur - i.e., two groups at the same table at the same time)
var step3 = from data in step2
            select new
			{
				Table = data.Table,
				Seating = data.Seating,
				Taken = data.CommonBilling.Count() > 0,
				// .FirstOrDefault() is effectively "flattening" my collection of 1 item into a
				// single object whose properties I can get in step 4 by using the dot (.) operator
				CommonBilling = data.CommonBilling.FirstOrDefault()
			};
step3.Dump("Step 3 in my query - pull out the first (only) item from the common billing list");

// Step 4 - Build our intended seating summary info
var step4 = from data in step3
            select new // SeatingSummary() // my DTO
			{
				Table = data.Table,
				Seating = data.Seating,
				Taken = data.Taken,
				// use a ternary expression to conditionally get the bill id (if it exists)
				BillID = data.Taken ?			    // if(data.Taken)
				         data.CommonBilling.BillID  // value to use if true						 
					   : (int?) null				// value to use if false
			};
step4.Dump("Step 4 - my final results that I need for the form");


























