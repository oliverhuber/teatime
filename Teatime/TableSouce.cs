using System;
using System.Collections;
using Foundation;
using UIKit;
namespace Teatime
{
	public class TableSource : UITableViewSource
	{
		protected string[] tableItems;
		protected ArrayList tableItemsList;
		protected string cellIdentifier = "TableCell";

		public TableSource(string[] items)//, HomeScreen owner)
		{
			tableItems = items;
		}

		public TableSource(ArrayList items)//, HomeScreen owner)
		{
			tableItemsList = items;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItemsList.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			UIAlertController okAlertController = UIAlertController.Create("Row Selected", tableItemsList[indexPath.Row].ToString(), UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			tableView.DeselectRow(indexPath, true);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(cellIdentifier) as CustomCell;
			if (cell == null)
				cell = new CustomCell((NSString)cellIdentifier);
				cell.UpdateCell("Log", tableItemsList[indexPath.Row].ToString());
			return cell;
		}
	}
}
