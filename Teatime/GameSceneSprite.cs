using System;
using System.Drawing;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class GameSceneSprite : SKScene
	{
		// Class declarations of the sprite nodes
		SKSpriteNode oneSprite;
		SKSpriteNode secSprite;
		SKShapeNode yourline;
		SKLabelNode myLabel;
		bool firstTouch;
		protected GameSceneSprite(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			// Setup Sprite Scene

			// New Label placed in the middle of the Screen
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Guten Tag, wie gehts dir heute?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2)
			};
			myLabel.Alpha = 0.9f;

			// Background gradient sprite node
			var container = new SKSpriteNode("background");
			container.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			//this.Size = new CGSize(1200, 200);

			// Sprite Node
			secSprite = new SKSpriteNode("spark");
			secSprite.Position = new CGPoint(200, 300);
			secSprite.ZPosition = 1;

			// Sprite Node
			oneSprite = new SKSpriteNode("spark");
			oneSprite.Position = new CGPoint(200, 100);
			oneSprite.ZPosition = 1;
			oneSprite.XScale = 2f;
			oneSprite.YScale = 2f;
			oneSprite.Alpha = 0.7f;

			firstTouch = false;
			// Create Path for the line, between both sprites
			/*var path = new CGPath();
			path.AddLines(new CGPoint[]{
				new CGPoint (oneSprite.Position.X, oneSprite.Position.Y),
				new CGPoint (secSprite.Position.X, secSprite.Position.Y)
			
					});
			path.CloseSubpath();

			// Generate Line according to Path
			yourline = new SKShapeNode();
			yourline.Path = path;
			*/
			// Add Contextposition and add them to the scene
			myLabel.ZPosition = 1;
			container.ZPosition = 0;
			//yourline.ZPosition = 2;

			//AddChild(yourline);
			//AddChild(oneSprite);
			//AddChild(secSprite);
			AddChild(myLabel);
			AddChild(container);

		}
	
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			
			base.TouchesMoved(touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;
			var location = ((UITouch)touch).LocationInNode(this);
			if (touch != null)
			{
				

				if (oneSprite.Frame.Contains(((UITouch)touch).LocationInNode(this)))
				{
					float offsetX = (float)(touch.LocationInView(View).X);
					float offsetY = (float)(touch.LocationInView(View).Y);
				//	oneSprite.ScaleTo(new CGSize(oneSprite.Size.Width + (offsetX / 1000), oneSprite.Size.Height + (offsetY / 1000)));


					oneSprite.Position = location;
					//myLabel.Text = offsetX + " " + offsetY;


				/*	var path = new CGPath();
					path.AddLines(new CGPoint[]{
						new CGPoint (secSprite.Position.X, secSprite.Position.Y),
						new CGPoint (offsetX, this.View.Frame.Height-offsetY),
					});
					path.CloseSubpath();

					yourline.Path = path;
					*/
				}

			}
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				//**********
				UITouch touchc = touches.AnyObject as UITouch;
				//SKNodeTouches_UITouch touch = touches.AnyObject as SKNodeTouches_UITouch;
				var locationc = ((UITouch)touchc).LocationInNode(this);

				// Check click
				var checkX = ((UITouch)touchc).LocationInNode(this).X;
				var checkY = ((UITouch)touchc).LocationInNode(this).Y;
				oneSprite.RemoveAllActions();
				oneSprite.Position = new CGPoint(checkX, checkY);
				//*******
				if (firstTouch == false)
				{
					AddChild(oneSprite);
					firstTouch = true;
				}
			

				UIColor coloring;
				var speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 1;
					coloring = UIColor.Green;
					myLabel.Text = "Glücklich";
					myLabel.Position = new CGPoint(Frame.Width / 2, (7 * (Frame.Height / 8)));
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 2;
					coloring = UIColor.Blue;
					myLabel.Text = "Zufrieden";
					myLabel.Position = new CGPoint(Frame.Width / 2, (5 * (Frame.Height / 8)));

				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 3;
					coloring = UIColor.Orange;
					myLabel.Text = "Beunruhigt";
					myLabel.Position = new CGPoint(Frame.Width / 2, (3 * (Frame.Height / 8)));

				}
				else {
					speed = 4;
					coloring = UIColor.Red;
					myLabel.Text = "Nervös";
					myLabel.Position = new CGPoint(Frame.Width / 2, (1 * (Frame.Height / 8)));
					//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
				}

				SKAction scaleUp = SKAction.ScaleTo(3f, 5 - speed);
				SKAction scaleDown = SKAction.ScaleTo(1f, 5 - speed);
				SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleDown);
				oneSprite.RunAction(SKAction.RepeatActionForever(scaleSeq));

				for (int i = 1; i <= 100; i++)
				{

					var location = ((UITouch)touch).LocationInNode(this);

					location.X = location.X + (new Random().Next(-10, 10));
					location.Y = location.Y + (new Random().Next(-10, 10));

					Random rnd = new Random();
					int newX = rnd.Next(-1000, 1000);
					int newY = rnd.Next(-1000, 1000);
					var sprite = new SKSpriteNode("spark")
					{
						Position = location,
						//		XScale = 1.5f * (speed),
						XScale = 0.5f * (speed),
						YScale = 0.5f * (speed)
					};

					var action = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					sprite.RunAction(SKAction.ColorizeWithColor(coloring, 0.1f, 2));
					sprite.RunAction(SKAction.ScaleTo(1.8f, 5));

					sprite.RunAction(SKAction.MoveTo(new CGPoint(newX, newY), 10));

					sprite.RunAction(SKAction.FadeOutWithDuration(5));
					sprite.RunAction(SKAction.RepeatActionForever(action));
					sprite.ZPosition = 1;
					AddChild(sprite);
				}
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}
	}
}
