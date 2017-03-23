
namespace Teatime
{
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
		public static string inputName;
	}
}
