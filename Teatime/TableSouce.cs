
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using Foundation;
using UIKit;
namespace Teatime
{
	public class TableSource : UITableViewSource
	{
		protected string[] tableItems;
		protected ArrayList tableItemsList;
		protected string cellIdentifier = "TableCell";
		//HomeScreen owner;

		public TableSource(string[] items)//, HomeScreen owner)
		{
			tableItems = items;
		//	this.owner = owner;
		}
		public TableSource(ArrayList items)//, HomeScreen owner)
		{
			tableItemsList = items;
			//	this.owner = owner;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			//return tableItems.Length;
			return tableItemsList.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			UIAlertController okAlertController = UIAlertController.Create("Row Selected", tableItemsList[indexPath.Row].ToString(), UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			//owner.PresentViewController(okAlertController, true, null);

			tableView.DeselectRow(indexPath, true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		/*public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			Contract.Ensures(Contract.Result<UITableViewCell>() != null);
			//Contract.Ensures(Contract.Result<UITableViewCell>() != null);
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
			
			//cell.TextLabel.Text = tableItems[indexPath.Row];
			//string[] itemss = 
			cell.TextLabel.Text = tableItemsList[indexPath.Row].ToString();

			return cell;
		}
*/
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(cellIdentifier) as CustomCell;
			if (cell == null)
			/*	cell = new CustomCell(cellIdentifier);
			cell.UpdateCell(tableItems[indexPath.Row].Heading
					, tableItems[indexPath.Row].SubHeading
					));
					*/
				cell = new CustomCell((NSString)cellIdentifier);
			    cell.UpdateCell("Log"
			                    , tableItemsList[indexPath.Row].ToString()
					);
			return cell;
		}
	}


}
