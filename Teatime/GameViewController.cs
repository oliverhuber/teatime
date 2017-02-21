using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Teatime
{
	public partial class GameViewController : UIViewController
	{
		
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

			UIColor startColor = UIColor.FromRGB(78, 191, 216);
			UIColor endColor = UIColor.FromRGB(78, 91, 216);
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			View.Layer.InsertSublayer(gradient, 0);
		
			userName.Placeholder = "Dein Name";
			userName.ShouldReturn += (textField) =>
			{
				userName.ResignFirstResponder();
				DatabaseMgmt.inputName = userName.Text;
				return true;
			};

		}

		// Buttons on Main Storyboard, both are connected with a segue to another Controller
		partial void GotoNext5_TouchUpInside(UIButton sender)
		{
		}

		partial void GotoNext4_TouchUpInside(UIButton sender)
		{
		}

		partial void GotoNext2_TouchUpInside(UIButton sender)
		{
		}

		partial void GotoNext_TouchUpInside(UIButton sender)
		{
		}

		partial void GotoNext6_TouchUpInside(UIButton sender)
		{
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

		public override bool ShouldAutorotate()
		{
			return true;
		}

	}

}