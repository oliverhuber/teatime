using System;
using System.IO;
using SQLite;

namespace Teatime
{
	/*// Table specificaition
	[Table("UserInput")]
	public class UserInput
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public int PrototypeNr { get; set; }
		[MaxLength(10)]
		public string Username { get; set; }
		public int Dim1 { get; set; }
		public int Dim2 { get; set; }
		public int Dim3 { get; set; }
		[MaxLength(255)]
		public string Comment { get; set; }

	}
*/

	public static class DatabaseMgmt
	{
		static TeatimeDatabase database;

		public static TeatimeDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new TeatimeDatabase(new FileHelper().GetLocalFilePath("TeatimeSQLite.db3"));
				}
				return database;
			}
		}
		//public DatabaseMgmt()
		//{
		/*

			// DB Path and Connection
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"teatime.db3");
			var db = new SQLiteConnection(dbPath);

			// DB Setup	and Initialisation

			db.CreateTable<UserInput>();/*
			if (db.Table<UserInput>().Count() == 0)
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
			var table = db.Table<UserInput>();
			foreach (var s in table)
			{
				Console.WriteLine(s.Id + " " + s.Username);
			}
		}
		*/
	}
}
