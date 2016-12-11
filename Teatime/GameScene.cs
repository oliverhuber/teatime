using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class GameScene : SKScene
	{
		SKSpriteNode backgroundSprite;
		SKLabelNode myLabel;
		SKLabelNode myLabel2;
		SKLabelNode myLabel3;
		SKLabelNode myLabel4;

		protected GameScene(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	
		public override void DidMoveToView(SKView view)
		{
			// Setup Scene with SKNodes and call the sparks generator
			// Set inital bgcolor
			this.BackgroundColor = UIColor.FromRGBA(100, 200, 200, 155);

			// Define and add Label 1
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Guten Tag, wie gehts dir heute?",
				FontSize = 28,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2)
			};
			myLabel.Alpha = 0.9f;
			myLabel.ZPosition = 1;
			AddChild(myLabel);

			// Actions and Sequence for Label 2
			SKAction labelWait = SKAction.WaitForDuration(2);
			SKAction labelFade = SKAction.FadeOutWithDuration(3);
			SKAction labelSeq = SKAction.Sequence(labelWait, labelFade);
			myLabel.RunAction(labelSeq);

			// Define and add Label 3
			myLabel2 = new SKLabelNode("AppleSDGothicNeo-Regular")
			{
				Text = "Bewege deinen Finger auf dem Screen",
				FontSize = 22,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 +140)
			};
			myLabel2.Alpha = 0.9f;
			myLabel2.ZPosition = 1;
			AddChild(myLabel2);

			// Actions and Sequence for Label 2
			SKAction action1 = SKAction.WaitForDuration(2);
			SKAction action2 = SKAction.FadeOutWithDuration(3);
			SKAction sequence = SKAction.Sequence(action1, action2);
			myLabel2.RunAction(sequence);

			// Define and add Label 3
			myLabel3 = new SKLabelNode("AppleSDGothicNeo-Regular")
			{
				Text = "Welche Farbe und Bewegung",
				FontSize = 24,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 140),
			};
			myLabel3.Alpha = 0.0f;
			myLabel3.ZPosition = 1;
			AddChild(myLabel3);

			// Define and add Label 4
			myLabel4 = new SKLabelNode("AppleSDGothicNeo-Regular")
			{
				Text = "passt zu deinem Gemütszustand?",
				FontSize = 24,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 110),
			};
			myLabel4.Alpha = 0.0f;
			myLabel4.ZPosition = 1;
			AddChild(myLabel4);


			// Actions and Sequence for label 3 / 4
			SKAction action11 = SKAction.WaitForDuration(6);
			SKAction action22 = SKAction.FadeInWithDuration(2);
			SKAction action33 = SKAction.WaitForDuration(3);
			SKAction action44 = SKAction.FadeOutWithDuration(2);
			SKAction sequence11 = SKAction.Sequence(action11, action22,action33,action44);
			myLabel3.RunAction(sequence11);
			myLabel4.RunAction(sequence11);

			// Background Sprite with gradient
			backgroundSprite = new SKSpriteNode("spark");
			backgroundSprite.ScaleTo(this.Frame.Size);
			backgroundSprite.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			backgroundSprite.ZPosition = 0;
			backgroundSprite.Alpha = 0.3f;
			AddChild(backgroundSprite);

			// Generate Sparks
			generateSparks();

		}
		public override void Update(double currentTime )
		{
			//UPDATE FAST
		}


		public void changeTexture()
		{
			// Change Texture for all SkarkNodes
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.changeTexture();
			}
		}
		public void updateSparks(double speed, bool random, bool disturb, int disturbFactor, bool vibrate)
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.moveAnimation ( speed,  random, disturb, disturbFactor, vibrate);

			}
		}


		//*****************************************************************************
		/// <summary>
		/// Generate the sparks particles
		/// </summary>
		//*****************************************************************************

		public void generateSparks() { 

			// Inital speed 
			var speed = 0;

			// Generate 5 nodes per 10 rows
			for (int i = 1; i <= 5; i++)
			{
				for (int y = 1; y <= 10; y++) 
				{
					// Calculate location of each Spark (5 Sparks per row, for 10 rows)
					var location = new CGPoint();
					location.X = (((this.View.Frame.Width/10) * (2 * i))-(this.View.Frame.Width / 10));
					location.Y = (((this.View.Frame.Height/20) * (2 * y)) - (this.View.Frame.Height / 20));

					// Define Spark with location and alpha
					var sprite = new SparkNode("spark")
					{
						Position = location,
						XScale = 0.5f ,
						YScale = 0.5f ,
						Alpha = 0.3f
						          
					};

					// Set the Center for the node
					sprite.centerOfNode = location;

					// Define Rotation, is zero because of the inital speed
					var action = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					sprite.RunAction(SKAction.RepeatActionForever(action));

					// Define inital load actions, scale
					sprite.RunAction(SKAction.ScaleTo(1.6f, 2));

					// Position in comparsion to other SpriteNodes
					sprite.ZPosition = 1;

					// Define ParentNode, each SparkNode gets a ParentNode
					var parent = new ParentNode("spark")
					{
						Position = location,
						XScale = 0.5f,
						YScale = 0.5f,
						Alpha = 0.3f

					};

					// Set the Center for the node
					parent.centerOfNode = location;

					// Define Rotation, is zero because of the inital speed
					var paction = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					parent.RunAction(SKAction.RepeatActionForever(paction));

					// Define inital load actions, scale
					parent.RunAction(SKAction.ScaleTo(1.6f, 2));

					// Position in comparsion to other SpriteNodes
					parent.ZPosition = 1;

					// Add the SparkNode to the scene
					AddChild(sprite);

					// Add the ParentNode to the SparkNode
					sprite.parentNode = parent;

					// Add the ParentNode to the scene
					AddChild(parent);
				}
			}

			// Do first update of the all sparks, that the will have a base movement
			updateSparks(1, true,false,0,false);
		}



		// If finger moved on screen
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null)
			{

				// Get the latest X and Y coordinates
				float offsetX = (float)(touch.LocationInView(View).X);
				float offsetY = (float)(touch.LocationInView(View).Y);

				// other coordinate system
				//var checkX = ((UITouch)touchc).LocationInNode(this).X;
				//var checkY = ((UITouch)touchc).LocationInNode(this).Y;

				float checkX = offsetX;
				float checkY = offsetY;

				// Background Calculating
				this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height),0.5f,(nfloat)  (((checkX / Frame.Width)  / 3)*2+((0.3333333f))));

				// Check to which part the finger is moved to and update the sparks
				var speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 0;
					updateSparks(speed,true,true,6,true);
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 1;
					updateSparks(speed, true,true,4,true);
				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 3;
					updateSparks(speed, true,true,2,false);
				}
				else {
					speed = 5;
					updateSparks(speed, false,false,0,false);
				}

				// Use to show calculations
				//nfloat calc = (nfloat) (((checkX / Frame.Width) / 3) * 2 + (0.3333333f)) ;
				//myLabel.Text = calc.ToString();

				/*
				if (oneSprite.Frame.Contains(((UITouch)touch).LocationInNode(this)))
				{
					oneSprite.Position = location;

					// Update Path of Line
					var path = new CGPath();
					path.AddLines(new CGPoint[]{
						new CGPoint (secSprite.Position.X, secSprite.Position.Y),
						new CGPoint (offsetX, this.View.Frame.Height-offsetY),
					});
					path.CloseSubpath();
					yourline.Path = path;
				}

				// check to see if the touch started in the drag me image
				if (touchStartedInside)
				{
					// move the shape
					float offsetX = (float)( touch.LocationInView(View).X);
					float offsetY = (float)( touch.LocationInView(View).Y);
					oneSprite.Position = new CGPoint((offsetX), (float)(offsetY));

				}
				*/
			}
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{

				//**********
				UITouch touchc = touches.AnyObject as UITouch;

				// Check click
				var checkX = ((UITouch)touchc).LocationInView(View).X;
				var checkY = ((UITouch)touchc).LocationInView(View).Y;

				// other coordinate system
				//var checkX = ((UITouch)touchc).LocationInNode(this).X;
				//var checkY = ((UITouch)touchc).LocationInNode(this).Y;

				// Background Calculating
				this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

				// Check to which part the finger is moved to and update the sparks
				var speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 0;
					updateSparks(speed, true, true, 6, true);
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 1;
					updateSparks(speed, true, true, 4, true);
				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 3;
					updateSparks(speed, true, true, 2, false);
				}
				else {
					speed = 5;
					updateSparks(speed, false, false, 0, false);
				}


				// Double tapped
				if (touchc.TapCount == 2)
				{
					//	changeTexture();
					// do something with the double touch.
				}

			}
		}
	}
}
