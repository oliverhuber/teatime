using System;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public partial class GameSubViewController : UIViewController
	{
		private GameScene scene;

		protected GameSubViewController(IntPtr handle) : base(handle)
		{
		}

		public GameSubViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Configure the view.
			StartPrototype();
		}

		private void StartPrototype()
		{
			// Configure SKview, cast current view
			var skView = (SKView)View;
			skView.ShowsFPS = false;
			skView.ShowsNodeCount = false;

			// Sprite Kit applies additional optimizations to improve rendering performance 
			skView.IgnoresSiblingOrder = true;
			skView.SizeToFit();

			// Create and configure the scene
			scene = new GameScene();
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			skView.PresentScene(scene);
		}

		partial void UnwindProto_TouchUpInside(UIButton sender)
		{
			// If back button is clicked save prototype
			scene.SaveProto1Input();
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
		}

		public override bool PrefersStatusBarHidden()
		{
			return true;
		}
	}
}