using System;
using UIKit;
using SpriteKit;
namespace Teatime
{
	public partial class GameSubViewSpriteController : UIViewController
	{
		private GameSceneSprite scene;

		public GameSubViewSpriteController(IntPtr handle) : base(handle)
		{
		}

		public GameSubViewSpriteController()
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
			StartPrototype();
		}

		private void StartPrototype()
		{
			// Configure the view
			var skView = (SKView)View;
			skView.ShowsFPS = false;
			skView.ShowsNodeCount = false;
			skView.IgnoresSiblingOrder = true;

			// Create and configure the scene.
			scene = new GameSceneSprite();
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;

			// Present the scene.
			skView.PresentScene(scene);
		}
		partial void UnwindProtoSprite_TouchUpInside(UIButton sender)
		{
			// If back button is clicked save prototype
			scene.SaveProto2Input();
		}
	}
}