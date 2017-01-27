using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;
//using Xamarin.Forms;

namespace Teatime
{
	public partial class GameViewController : UIViewController
	{
		
		// Specify sub controllers
		UIViewController gameSubViewController;
		UIViewController gameSubViewSpriteController;
		UIViewController gameSubViewSceneController;

		protected GameViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		// Register unwind Segue to return to this controller
		[Action("UnwindToPrototypes:")]
		public void UnwindToPrototypes(UIStoryboardSegue segue)
		{

		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Set Background Gradient and add to Layer
			var gradient = new CAGradientLayer();
			gradient.Frame = View.Bounds;
			gradient.NeedsDisplayOnBoundsChange = true;
			gradient.MasksToBounds = true;

			UIColor startColor = UIColor.FromRGB((int)78, (int)191, (int)216);
			UIColor endColor = UIColor.FromRGB((int)78, (int)91, (int)216);
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			View.Layer.InsertSublayer(gradient, 0);
		
			// Coded Button to got to next view
			/*	UIButton gotoProto1 = new UIButton();
				gotoProto1.Frame = new CGRect(0f, 0f, 100f, 30f);
				gotoProto1.SetTitle("Prototyp1", UIControlState.Normal);
				gotoProto1.Font = UIFont.FromName("AppleSDGothicNeo-UltraLight",10);
				gotoProto1.TintColor = UIColor.White;
				//gotoProto1.BackgroundColor = UIColor.Green;
				skView.AddSubview(gotoProto1);
			*/

			//Instatiating View Controllers with Storyboard ID 'GameSubViewController' and 'GameSubViewSpriteController'
			var myStoryboard = AppDelegate.Storyboard;
			gameSubViewController = myStoryboard.InstantiateViewController("GameSubViewController") as GameSubViewController;
			gameSubViewSpriteController = myStoryboard.InstantiateViewController("GameSubViewSpriteController") as GameSubViewSpriteController;
			gameSubViewSceneController= myStoryboard.InstantiateViewController("GameSubViewSceneController") as GameSubViewSceneController;

			/*gotoProto1.TouchUpInside += (object sender, System.EventArgs e) =>
			{
				this.NavigationController.PushViewController(gameSubViewController, true);
			};	*/

			this.userName.Placeholder = "Dein Name";
			//this.userName.
			//this.userName.BackgroundColor = UIColor.FromRGB((int)78, (int)91, (int)216);
			//this.userName.TextColor = UIColor.White;
			    this.userName.ShouldReturn += (textField) =>
			{
				userName.ResignFirstResponder();
				DatabaseMgmt.inputName = this.userName.Text;
				return true;
			};


		}




		// Buttons on Main Storyboard, both are connected with a segue to another Controller
		partial void GotoNext2_TouchUpInside(UIButton sender)
		{
		}

		partial void GotoNext_TouchUpInside(UIButton sender)
		{
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		partial void GotoNext4_TouchUpInside(UIButton sender)
		{
		//	throw new NotImplementedException();
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? UIInterfaceOrientationMask.AllButUpsideDown : UIInterfaceOrientationMask.All;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override bool PrefersStatusBarHidden()
		{
			return true;
		}
	}

}