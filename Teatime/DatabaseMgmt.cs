using System;
using System.IO;
using SQLite;

namespace Teatime
{
	// Table specificaition
	[Table("Items")]
	public class Stock
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		[MaxLength(8)]
		public string Symbol { get; set; }
	}

	public class DatabaseMgmt
	{
		public DatabaseMgmt()
		{

			// DB Path and Connection
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"teatime.db3");
			var db = new SQLiteConnection(dbPath);

			// DB Setup	and Initialisation
			db.CreateTable<Stock>();
			if (db.Table<Stock>().Count() == 3)
			{
				// only insert the data if it doesn't already exist
				var newStock = new Stock();
				newStock.Symbol = "AAPL";
				db.Insert(newStock);

				newStock = new Stock();
				newStock.Symbol = "GOOG";
				db.Insert(newStock);

				newStock = new Stock();
				newStock.Symbol = "MSFT";
				db.Insert(newStock);

				newStock = new Stock();
				newStock.Symbol = "gaga";
				db.Insert(newStock);
			}

			DoSomeDataAccess();

		}

		public static void DoSomeDataAccess()
		{
			// DB Path and Connection
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "teatime.db3");
			var db = new SQLiteConnection(dbPath);
		//	var stock = db.Get<Stock>(5); // primary key id of 5
		//	var stockList = db.Table<Stock>();
			Console.WriteLine("Reading data");
			var table = db.Table<Stock>();
			foreach (var s in table)
			{
				Console.WriteLine(s.Id + " " + s.Symbol);
			}
		}
		
	}
}
