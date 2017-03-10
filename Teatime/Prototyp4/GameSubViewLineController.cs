using System;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public partial class GameSubViewLineController : UIViewController
	{
		private GameSceneLine scene;

		public GameSubViewLineController(IntPtr handle) : base(handle)
		{
		}

		public GameSubViewLineController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Configure the view
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

			// Create and configure the scene
			scene = new GameSceneLine();
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			skView.PresentScene(scene);
		}

		partial void UnwindProto5_TouchUpInside(UIButton sender)
		{
			// If back button is clicked save prototype
			scene.SaveProto4Input();
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