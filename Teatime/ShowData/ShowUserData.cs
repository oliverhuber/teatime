using System;
using System.Collections;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace Teatime
{
	public partial class ShowUserData : UITableViewController
	{
		UITableView table;
		public ShowUserData(IntPtr handle) : base(handle)
		{
		}

		public override bool PrefersStatusBarHidden()
		{
			return true;
		}

		partial void ResetButton_TouchUpInside(UIButton sender)
		{
			// Create Alert
			var okCancelAlertController = UIAlertController.Create("Delete User Data", "Are you sure?", UIAlertControllerStyle.Alert);

			// Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("Okay", UIAlertActionStyle.Default, alert => deleteData()));
			okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

			// Present Alert
			PresentViewController(okCancelAlertController, true, null);
		}

		// Delete Data from Table
		public void deleteData()
		{
			foreach (var s in DatabaseMgmt.Database.GetAllItems())
			{
				DatabaseMgmt.Database.DeleteItem(s);
			}

			var myStoryboard = AppDelegate.Storyboard;

			ShowUserData showUserDataController = myStoryboard.InstantiateViewController("ShowUserData") as ShowUserData;

			PresentViewController(showUserDataController, true, null);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Set Background Gradient and add to Layer
			var gradient = new CAGradientLayer();
			gradient.Frame = View.Bounds;
			gradient.NeedsDisplayOnBoundsChange = true;
			gradient.MasksToBounds = true;

			UIColor startColor = UIColor.FromRGB(78, 191, 216);
			UIColor endColor = UIColor.FromRGB(78, 91, 216);
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			View.Layer.InsertSublayer(gradient, 0);

			ArrayList dateList = new ArrayList();
			table = userData;

			foreach (var s in DatabaseMgmt.Database.GetAllItems())
			{
				dateList.Add(s.Username.ToString() + ":PT=" + s.PrototypeNr + ";" + s.dateInserted + ";D1=" + s.Dim1 + ";D2=" + s.Dim2 + ";D3=" + s.Dim3 + "|" + s.Comment);
			}
			table.Source = new TableSource(dateList);
		}
	}
}