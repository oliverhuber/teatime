using Foundation;
using System;
using UIKit;
using SpriteKit;
namespace Teatime
{
    public partial class GameSubViewSpriteController : UIViewController
    {
        public GameSubViewSpriteController (IntPtr handle) : base (handle)
        {
			// Only test for commit
		}
		public GameSubViewSpriteController()
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Configure the view.
			var skView = (SKView)View;
			skView.ShowsFPS = true;
			skView.ShowsNodeCount = true;

			/* Sprite Kit applies additional optimizations to improve rendering performance */
			skView.IgnoresSiblingOrder = true;

			// Create and configure the scene.
			var scene = SKNode.FromFile<GameSceneSprite>("GameSceneSprite");
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;

			// Present the scene.
			skView.PresentScene(scene);
		}
		partial void UnwindProtoSprite_TouchUpInside(UIButton sender)
		{
		}
	}
}