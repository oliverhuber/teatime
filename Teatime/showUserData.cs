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

		public showUserData (IntPtr handle) : base (handle)
        {
        }
		public override bool PrefersStatusBarHidden()
		{
			return true;
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
				dateList.Add(s.Username.ToString() +":PT=" +s.PrototypeNr+";"+ s.dateInserted.ToString() + ";D1=" + s.Dim1.ToString() + ";D2=" + s.Dim2.ToString() + ";D3=" + s.Dim3.ToString());
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