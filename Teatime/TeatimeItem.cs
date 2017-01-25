using System;
using SQLite;

namespace Teatime
{
	public class TeatimeItem
	{
		/*
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
		*/
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
		public DateTime dateInserted { get; set; }

	}
}
