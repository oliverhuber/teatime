using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public partial class GameSubViewController : UIViewController
	{
		


		GameViewController gameViewController;
		GameScene scene;
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
			scene = SKNode.FromFile<GameScene>("GameScene");
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;
			scene.Size = View.Bounds.Size;
			skView.PresentScene(scene);
		}

		partial void UnwindProto_TouchUpInside(UIButton sender)
		{/*
			//throw new NotImplementedException();
			TeatimeItem item;
			item = new TeatimeItem();
			item.Username = "Oliver";
			item.dateInserted = DateTime.Now.ToLocalTime();
			item.Dim1 = Proto1Dim1;
			item.Dim2 = Proto1Dim1;
			item.Dim3 = Proto1Dim3;
			item.PrototypeNr = 1;
			item.Comment = "test";
			AppDelegate.Database.SaveItem(item);
			// TeatimeItem returnItem =  AppDelegate.Database.GetItem(2);
			// Console.WriteLine(returnItem.Username);

			foreach (var s in AppDelegate.Database.GetItemsUserOliver
			         ())
			{
				
				Console.WriteLine("Username:" + s.Username + ", DateInserted:" + s.dateInserted + ", Dimension1:" + s.Dim1 + ", Dimension2:" + s.Dim2 + ", Dimension3:" + s.Dim3 + ", ProtypeNr:" + s.PrototypeNr + ", Comment:" + s.Comment);
				//AppDelegate.Database.DeleteItem(s);
			}*/
			scene.saveProto1Input();
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