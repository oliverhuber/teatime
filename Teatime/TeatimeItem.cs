using System;
using SQLite;

namespace Teatime
{
	public class TeatimeItem
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
		public DateTime dateInserted { get; set; }
	}
}