using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
    public partial class GameSubViewDrawController : UIViewController
    {
        public GameSubViewDrawController (IntPtr handle) : base (handle)
        {
        }
		GameViewController gameViewController;
		GameSceneDraw scene;
		public GameSubViewDrawController()
		{

		}
		public override void ViewDidLoad()
		{

			// Configure the view.
		//	UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)PreferredInterfaceOrientationForPresentation()), new NSString("orientation"));
		//	UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)PreferredInterfaceOrientationForPresentation()), new NSString("orientation"));

			base.ViewDidLoad();		
			startPrototype();


		}
	

	/*	public override bool ShouldAutorotate()

		{

			return true;

		}
*/


		/*public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()

		{

			return UIInterfaceOrientationMask.Landscape;

		}*/
		 bool ShouldAllowLandscape ()
    {
			return true;//TopViewController is xController; // implement this to return true when u want it
		}
		 public override bool ShouldAutorotate()
		{
			return ShouldAllowLandscape(); // implemet this method to return true only when u want it to
		}
	

	/*	 (NSUInteger)supportedInterfaceOrientations {
    if ([[UIDevice currentDevice]
		userInterfaceIdiom] == UIUserInterfaceIdiomPad) {
        // iPad: Allow all orientations
        return UIInterfaceOrientationMaskAll;
    } else {
        // iPhone: Allow only landscape
        return UIInterfaceOrientationMaskLandscape;
    }
}*/
		
		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
		{
			//return base.PreferredInterfaceOrientationForPresentation();
			return UIInterfaceOrientation.LandscapeLeft;
		}


		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
    {
        //var shouldAllowOtherOrientation = ShouldAllowLandscape (); // same here
       // if (shouldAllowOtherOrientation) 
        {
				return UIInterfaceOrientationMask.Landscape;
        } 
			    //return UIInterfaceOrientationMask.Portrait;
    }
		public void startPrototype()
		{
			// Configure SKview, cast current view
			var skView = (SKView)View;
			skView.ShowsFPS = false;
			skView.ShowsNodeCount = false;

			/* Sprite Kit applies additional optimizations to improve rendering performance */
			skView.IgnoresSiblingOrder = true;
			skView.SizeToFit();

			// Create and configure the scene.
			scene = SKNode.FromFile<GameSceneDraw>("GameSceneDraw");
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			skView.PresentScene(scene);

			base.ViewDidLoad();
		}




		partial void UnwindProto6_TouchUpInside(UIButton sender)
		{
		//	throw new NotImplementedException();
			scene.saveProto5Input();

		}

		/*public override bool ShouldAutorotate()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? UIInterfaceOrientationMask.AllButUpsideDown : UIInterfaceOrientationMask.All;
		}
*/
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