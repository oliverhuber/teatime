using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;
//using Xamarin.Forms;

namespace Teatime
{
	public partial class GameSubViewController : UIViewController
	{
		
		GameViewController gameViewController;
		protected GameSubViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public GameSubViewController()
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
			skView.ShowsFPS = true;
			skView.ShowsNodeCount = true;

			/* Sprite Kit applies additional optimizations to improve rendering performance */
			skView.IgnoresSiblingOrder = true;
			skView.SizeToFit();

			// Create and configure the scene.
			var scene = SKNode.FromFile<GameScene>("GameScene");
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			skView.PresentScene(scene);

		}

		partial void UnwindProto_TouchUpInside(UIButton sender)
		{
			//throw new NotImplementedException();
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