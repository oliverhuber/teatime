using System.Collections.Generic;
using SQLite;

namespace Teatime
{
	public class TeatimeDatabase
	{
		readonly SQLiteConnection database;

		public TeatimeDatabase(string dbPath)
		{
			database = new SQLiteConnection(dbPath);
			database.CreateTable<TeatimeItem>();
		}

		public TableQuery<TeatimeItem> GetItemsAsync()
		{
			return database.Table<TeatimeItem>();
		}

		public List<TeatimeItem> GetItemsUserOliver()
		{
			return database.Query<TeatimeItem>("SELECT * FROM [TeatimeItem] WHERE [Username] = 'Oliver'");
		}
		public List<TeatimeItem> GetAllItems()
		{
			return database.Query<TeatimeItem>("SELECT * FROM [TeatimeItem]");
		}

		public TeatimeItem GetItem(int id)
		{
			return database.Table<TeatimeItem>().Where(i => i.Id == id).First();
		}

		public int SaveItem(TeatimeItem item)
		{
			if (item.Id != 0)
			{
				return database.Update(item);
			}
			else {
				return database.Insert(item);
			}
		}

		public int DeleteItem(TeatimeItem item)
		{
			return database.Delete(item);
		}
	}
}
