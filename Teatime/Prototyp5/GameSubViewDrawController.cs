using System;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public partial class GameSubViewDrawController : UIViewController
	{
		private GameSceneDraw scene;

		public GameSubViewDrawController(IntPtr handle) : base(handle)
		{
		}

		public GameSubViewDrawController()
		{

		}
		public override void ViewDidLoad()
		{

			// Configure the view.
			// Would force horizontal mode after loading
			// UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)PreferredInterfaceOrientationForPresentation()), new NSString("orientation"));

			base.ViewDidLoad();
			StartPrototype();
			            
		}

		private void StartPrototype()
		{
			// Configure SKview, cast current view
			var skView = (SKView)View;
			skView.ShowsFPS = false;
			skView.ShowsNodeCount = false;
			skView.IgnoresSiblingOrder = true;
			skView.SizeToFit();

			// Create and configure the scene.
			scene = new GameSceneDraw();
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			scene.SuperViewController = this;
			skView.PresentScene(scene);

			base.ViewDidLoad();
		}

		// Save Dimensions
		partial void UnwindProto6_TouchUpInside(UIButton sender)
		{
			scene.SaveProto5Input();
		}
		/*
		bool ShouldAllowLandscape()
		{
			return false;
		}

		public override bool ShouldAutorotate()
		{
			return ShouldAllowLandscape(); 
		}

		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
		{
			//return base.PreferredInterfaceOrientationForPresentation();
			return UIInterfaceOrientation.LandscapeLeft;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.Landscape;
		}*/

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override bool PrefersStatusBarHidden()
		{
			return true;
		}
	}
}