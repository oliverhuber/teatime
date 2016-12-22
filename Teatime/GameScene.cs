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
		bool pressAndFollow;
		CGPoint globalCenter;
		SKFieldNode fieldNode;
		SKSpriteNode backgroundSprite;
		SKLabelNode myLabel;
		SKLabelNode myLabel2;
		SKLabelNode myLabel3;
		SKLabelNode myLabel4;
		nfloat LastX;
		nfloat LastY;
		int changeColor;
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
			myLabel = new SKLabelNode("AppleSDGothicNeo-Regular")
			{
				Text = "Guten Tag, wie gehts dir heute?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2+140)
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
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 +110)
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
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 140),
			};
			myLabel3.Alpha = 0.0f;
			myLabel3.ZPosition = 1;
			AddChild(myLabel3);

			// Define and add Label 4
			myLabel4 = new SKLabelNode("AppleSDGothicNeo-Regular")
			{
				Text = "passt zu deinem Gemütszustand?",
				FontSize = 20,
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

			// Add Gravity Field
			fieldNode = SKFieldNode.CreateSpringField();
			fieldNode.Enabled = false;
			fieldNode.Position = new CGPoint(Frame.Size.Width / 2, Frame.Size.Height / 2);
			fieldNode.Strength = 0.5f;
			fieldNode.Region = new SKRegion(Frame.Size);
			AddChild(fieldNode);

			// Set Color Flag
			changeColor = 0;
			// Generate Sparks
			generateSparks();
			//generateCoordinateSystem();
		}
		public override void Update(double currentTime )
		{
			//UPDATE FAST
			if (pressAndFollow == true)
			{
				updateCenter();
			}
		}


		public void changeTexture()
		{
			// Change Texture for all SkarkNodes
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.changeTexture();
			}
		}
		public void updateCenter()
		{
			Random rnd = new Random();
			CGPoint center = globalCenter;
			// Change Texture for all SkarkNodes
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.centerOfNode = center;
				spark.parentNode.centerOfNode = center;
				spark.RemoveAllActions();
				spark.parentNode.RemoveAllActions();
				double scaleSpeed= (rnd.NextDouble() * (1 - 0.5) + 0.5);
				SKAction action1 = SKAction.ScaleTo(3, scaleSpeed);
				SKAction action2 = SKAction.ScaleTo(1, scaleSpeed);
				var sequence = SKAction.Sequence(action1, action2);
				spark.RunAction(SKAction.RepeatActionForever(sequence));

				/*if (spark.Position.X > center.X && spark.Position.Y > center.Y)
				{
					spark.PhysicsBody.Velocity = new CGVector(-400, -400);
					spark.parentNode.PhysicsBody.Velocity = new CGVector(-400, -400);
				}
				else if (spark.Position.X > center.X && spark.Position.Y < center.Y)
				{
					spark.PhysicsBody.Velocity = new CGVector(-400, 400);
					spark.parentNode.PhysicsBody.Velocity = new CGVector(-400, 400);
				}
				else if (spark.Position.X < center.X && spark.Position.Y > center.Y)
				{
					spark.PhysicsBody.Velocity = new CGVector(400, -400);
					spark.parentNode.PhysicsBody.Velocity = new CGVector(400, -400);
				}
				else {
					spark.PhysicsBody.Velocity = new CGVector(400, 400);
					spark.parentNode.PhysicsBody.Velocity = new CGVector(400, 400);
				}*/

			}
		}
		public void revertCenter()
		{
			// Change Texture for all SkarkNodes
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.centerOfNode = spark.centerOfNodeFixed;
				spark.parentNode.centerOfNode = spark.parentNode.centerOfNodeFixed;
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
		public void followDrag(double speed, bool random, bool disturb, int disturbFactor, bool vibrate)
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var spark in Children.OfType<SparkNode>())
			{
				//spark.followDrag();

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
			for (int i = 1; i <= 3; i++)
			{
				for (int y = 1; y <= 6; y++) 
				{
					// Calculate location of each Spark (5 Sparks per row, for 10 rows)
					var location = new CGPoint();
					location.X = (((this.View.Frame.Width/6) * (2 * i))-(this.View.Frame.Width / 6));
					location.Y = (((this.View.Frame.Height/12) * (2 * y)) - (this.View.Frame.Height / 12));

					// Define Spark with location and alpha
					var sprite = new SparkNode("spark")
					{
						Position = location,
						XScale = 1.6f ,
						YScale = 1.6f ,
						Alpha = 0.3f
						          
					};

					// Set the Center for the node
					sprite.centerOfNode = location;
					sprite.centerOfNodeFixed = location;

					// Define Rotation, is zero because of the inital speed
					var action = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					sprite.RunAction(SKAction.RepeatActionForever(action));

					// Define inital load actions, scale
					//sprite.RunAction(SKAction.ScaleTo(1.6f, 2));

					// Position in comparsion to other SpriteNodes
					sprite.ZPosition = 1;

					// Define ParentNode, each SparkNode gets a ParentNode
					var parent = new ParentNode("spark")
					{
						Position = location,
						XScale = 0.9f,
						YScale = 0.9f,
						Alpha = 0.3f

					};

					// Set the Center for the node
					parent.centerOfNode = location;
					parent.centerOfNodeFixed = location;
					// Define Rotation, is zero because of the inital speed
					var paction = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					parent.RunAction(SKAction.RepeatActionForever(paction));

					// Define inital load actions, scale
				//	parent.RunAction(SKAction.ScaleTo(1.6f, 2));

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
				pressAndFollow = true;
				fieldNode.Enabled = true;

				// Get the latest X and Y coordinates
				 // 0 of X i on top
				float offsetX = (float)(touch.LocationInView(View).X);
				float offsetY = (float)(touch.LocationInView(View).Y);

				// other coordinate system
				// 0 of Y is bottom
				var checkX_Location = ((UITouch)touch).LocationInNode(this).X;
				var checkY_Location = ((UITouch)touch).LocationInNode(this).Y;

				float checkX = offsetX;
				float checkY = offsetY;


				fieldNode.Position= new CGPoint(checkX_Location, checkY_Location);
				if (changeColor % 3 == 0)
				{
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

				}
				else if (changeColor % 3 == 1)
				{
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 1f, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
				}
				else if (changeColor % 3 == 2)
				{
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)0, 0, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
				}
				else {
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
				}
				// Check to which part the finger is moved to and update the sparks

				var speed = 0;
				//if (Math.Abs((int)checkX_Location) % 1 == 0)
				//{
					globalCenter = new CGPoint(checkX_Location, checkY_Location);
					this.updateCenter();

			
			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//Release the Changed center
				this.revertCenter();
				fieldNode.Enabled = false;
				// Check click
				var checkX = ((UITouch)touch).LocationInView(View).X;
				var checkY = ((UITouch)touch).LocationInView(View).Y;
				double speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 0.5;
					updateSparks(speed, true, true, 8, true);
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 1;
					updateSparks(speed, true, true, 4, true);
				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 2;
					updateSparks(speed, true, true, 2, false);
				}
				else {
					speed = 3;
					updateSparks(speed, false, false, 0, false);
				}
			}
			pressAndFollow = false;

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

				if (changeColor % 2 == 0)
				{
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

				}
				else if (changeColor % 2 == 1)
				{
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)0, 0, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
				}
				else {
					// Background Calculating
					this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
				}

				// Check to which part the finger is moved to and update the sparks
				double speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 0.5;
					updateSparks(speed, true, true, 8, true);
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 1;
					updateSparks(speed, true, true, 4, true);
				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 2;
					updateSparks(speed, true, true, 2, false);
				}
				else {
					speed = 3;
					updateSparks(speed, false, false, 0, false);
				}

				LastX = checkX;
				LastY = checkY;
				// Double tapped
				if (touchc.TapCount == 2)
				{
					// Change Color Theme
					changeColor++;
					if (changeColor % 3 == 0)
					{
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

					}
					else if (changeColor % 3 == 1)
					{
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 1f, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
					}
					else if (changeColor % 3 == 2)
					{
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 1f, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
					}
					else {
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
					}
					//	changeTexture();
					// do something with the double touch.
				}

			}
		}
	}
}
