using Foundation;
using System;
using UIKit;
using CoreGraphics;
using System.Collections;
using CoreAnimation;

namespace Teatime
{
    public partial class showUserData : UITableViewController
    {
		UITableView table;
		UILabel myLabel;
		public showUserData (IntPtr handle) : base (handle)
        {
        }
		public override bool PrefersStatusBarHidden()
		{
			return true;
		}

		partial void ResetButton_TouchUpInside(UIButton sender)
		{


		  //Create Alert
                var okCancelAlertController = UIAlertController.Create("Delete User Data", "Are you sure?", UIAlertControllerStyle.Alert);

			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("Okay", UIAlertActionStyle.Default, alert => deleteData()));
			okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

			//Present Alert
			PresentViewController(okCancelAlertController, true ,null);



		}
		public void deleteData()
		{
			foreach (var s in DatabaseMgmt.Database.GetAllItems
					 ())
			{
				DatabaseMgmt.Database.DeleteItem(s);
			}
						
			var myStoryboard = AppDelegate.Storyboard;

			showUserData showUserDataController = myStoryboard.InstantiateViewController("showUserData") as showUserData;

			PresentViewController(showUserDataController, true,null);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Configure the view.
			// Set Background Gradient and add to Layer
			var gradient = new CAGradientLayer();
			gradient.Frame = View.Bounds;
			gradient.NeedsDisplayOnBoundsChange = true;
			gradient.MasksToBounds = true;

			UIColor startColor = UIColor.FromRGB((int)78, (int)191, (int)216);
			UIColor endColor = UIColor.FromRGB((int)78, (int)91, (int)216);
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			View.Layer.InsertSublayer(gradient, 0);

		/*	myLabel = new UILabel(new CGRect(View.Frame.Width, 0, 50, 30));

			myLabel.Text = "Reset";
			myLabel.Font = UIFont.FromName("AppleSDGothicNeo-UltraLight", 20f);
			myLabel.TextColor = UIColor.FromHSB((nfloat)0, 0, 6f);
			View.AddSubview(myLabel);
			//myLabel2.ZPosition = 2;
			//myLabel2.Alpha = 0.9f;
			//this.AddChild(myLabel);
			//this.AddChild(myLabel2);
*/
			ArrayList dateList = new ArrayList(); 
		//	this.TableView.Source = ; (DatabaseMgmt.Database.GetAllItems());
			//table = new UITableView(new CGRect(0, 20, View.Bounds.Width, View.Bounds.Height - 20)); // defaults to Plain styl
			//table.AutoresizingMask = UIViewAutoresizing.All;
			table = userData;

			//string[] tableItems = new string[100] ; //= new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
				//Console.WriteLine("Start Output: --------------------------------");
			foreach (var s in DatabaseMgmt.Database.GetAllItems
					 ())
			{
				//tableItems[s.Id] = s.dateInserted.ToString();
				dateList.Add(s.Username.ToString() +":PT=" +s.PrototypeNr+";"+ s.dateInserted.ToString() + ";D1=" + s.Dim1.ToString() + ";D2=" + s.Dim2.ToString() + ";D3=" + s.Dim3.ToString()+"|"+s.Comment);
			//	Console.WriteLine("Username:" + s.Username + ", DateInserted:" + s.dateInserted + ", Dimension1:" + s.Dim1 + ", Dimension2:" + s.Dim2 + ", Dimension3:" + s.Dim3 + ", ProtypeNr:" + s.PrototypeNr + ", Comment:" + s.Comment);
				//AppDelegate.Database.DeleteItem(s);
			}
			Console.WriteLine("End Output: --------------------------------");
			//table.Source = new TableSource(tableItems);
			table.Source = new TableSource(dateList);
			//Add(table);



		}
    }
}