using Foundation;
using System;
using UIKit;
using SpriteKit;
namespace Teatime
{
    public partial class GameSubViewSpriteController : UIViewController
    {
		GameSceneSprite scene;
        public GameSubViewSpriteController (IntPtr handle) : base (handle)
        {
			// Only test for commit
			// Only test for local branch change in Xamarin
			// Only test direct push from Xamarin
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
			var skView = (SKView)View;
			skView.ShowsFPS = true;
			skView.ShowsNodeCount = true;

			/* Sprite Kit applies additional optimizations to improve rendering performance */
			skView.IgnoresSiblingOrder = true;

			// Create and configure the scene.
			scene = SKNode.FromFile<GameSceneSprite>("GameSceneSprite");
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;

			// Present the scene.
			skView.PresentScene(scene);
		}
		partial void UnwindProtoSprite_TouchUpInside(UIButton sender)
		{
			scene.saveProto2Input();
		}
	}
}