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
			base.ViewDidLoad();
			// Configure the view.

			startPrototype();



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


		}




		partial void UnwindProto6_TouchUpInside(UIButton sender)
		{
		//	throw new NotImplementedException();
			scene.saveProto5Input();

		}

		public override bool ShouldAutorotate()
		{
			return true;
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