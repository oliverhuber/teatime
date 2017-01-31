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
		public int Proto2Dim1 { get; set; }
		public int Proto2Dim2 { get; set; }
		public int Proto2Dim3 { get; set; }
		// Class declarations of the sprite nodes
		SKSpriteNode oneSprite;
		SKSpriteNode secSprite;
		SKShapeNode yourline;
		SKLabelNode myLabel;
		SKEmitterNode fireEmitter;
		bool firstTouch;
		protected GameSceneSprite(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			Proto2Dim1 = 0;
			Proto2Dim2 = 0;
			Proto2Dim3 = 0;
			// Setup Sprite Scene

			// New Label placed in the middle of the Screen
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Guten Tag, wie gehts dir heute?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2)
			};
			myLabel.Alpha = 0.9f;
			myLabel.RunAction(SKAction.Sequence(SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
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
			// Add Contextposition and add them to the scene
			myLabel.ZPosition = 1;
			container.ZPosition = 0;
			//yourline.ZPosition = 2;

			AddChild(myLabel);
			AddChild(container);
			createEmitterNode();

		}

		public void saveProto2Input()
		{
			//throw new NotImplementedException();
			TeatimeItem item;
			item = new TeatimeItem();
		if (DatabaseMgmt.inputName != null)
			{
				item.Username = DatabaseMgmt.inputName;

			}
			else {
				item.Username = "Anonym";
			}			item.dateInserted = DateTime.Now.ToLocalTime();
			item.Dim1 = Proto2Dim1;
			item.Dim2 = Proto2Dim2;
			item.Dim3 = Proto2Dim3;
			item.PrototypeNr = 2;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
			// TeatimeItem returnItem =  DatabaseMgmt.Database.GetItem(2);
			// Console.WriteLine(returnItem.Username);
			Console.WriteLine("Start Output: --------------------------------");
			foreach (var s in DatabaseMgmt.Database.GetAllItems
					 ())
			{

				Console.WriteLine("Username:" + s.Username + ", DateInserted:" + s.dateInserted + ", Dimension1:" + s.Dim1 + ", Dimension2:" + s.Dim2 + ", Dimension3:" + s.Dim3 + ", ProtypeNr:" + s.PrototypeNr + ", Comment:" + s.Comment);
				//DatabaseMgmt.Database.DeleteItem(s);
			}
			Console.WriteLine("End Output: --------------------------------");

		}
		public void createEmitterNode()
		{
			//setup a fire emitter
			var location = new CGPoint();
			location.X = (((this.View.Frame.Width / 2)));
			location.Y = (((this.View.Frame.Height / 4)));

			// Define Spa
			fireEmitter = new SKEmitterNode();
			fireEmitter.Position = location;
			fireEmitter.NumParticlesToEmit = 0;
			fireEmitter.ZPosition = 2;
			fireEmitter.ParticleAlpha = 0.3f;
			//fireEmitter.EmissionAngle = 300f;
			fireEmitter.XAcceleration = 0;
			fireEmitter.YAcceleration = 1;
			fireEmitter.EmissionAngle = 10f;
			//	fireEmitter.ParticleLifetimeRange
			fireEmitter.TargetNode = this;
			UIImage image = UIImage.FromBundle("spark");
			fireEmitter.ParticleScale = 0.4f;
			fireEmitter.ParticleSpeedRange = 100f;
			fireEmitter.ParticleScaleRange = 0.3f;
			fireEmitter.ParticleScaleSpeed = -0.1f;
			fireEmitter.ParticleBirthRate = 500;
			fireEmitter.ParticlePositionRange = new CGVector(100f, 100f);
			fireEmitter.ParticleLifetimeRange = 10f;
			fireEmitter.ParticleRotationRange = 10f;
			//fireEmitter.ParticleLifetime = 10f;
			fireEmitter.EmissionAngleRange = 100f;
			fireEmitter.ParticleTexture = SKTexture.FromImageNamed(("spark"));
			//this.AddChild(fireEmitter);


		}
		public void updateEmitter(nfloat coordX,nfloat coordY) { 
			fireEmitter.ParticleScale = 0.6f * ((1/Frame.Width)*coordX) +0.2f ;
			fireEmitter.ParticleSpeedRange = 100f+coordX*3;
			fireEmitter.ParticleScaleRange = 0.3f*((1 / Frame.Width) * coordX*10) +0.2f;
			fireEmitter.ParticleScaleSpeed = -0.1f;
			fireEmitter.ParticleBirthRate = 600-coordX+20;
		
		}
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			
			base.TouchesMoved(touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;
			var location = ((UITouch)touch).LocationInNode(this);
			if (touch != null)
			{
				nfloat offsetX = (nfloat)(touch.LocationInNode(this).X);
				nfloat offsetY = (nfloat)(touch.LocationInNode(this).Y);
				updateEmitter(offsetX,offsetY);
				fireEmitter.Position = location;


			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//Release the Changed center

				// Check click
				var checkX = ((UITouch)touch).LocationInView(View).X;
				var checkY = ((UITouch)touch).LocationInView(View).Y;
				if (checkY >= 7 * (Frame.Height / 8))
				{
					Proto2Dim1 = 1;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else 
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}

					//myLabel.Text = "Glücklich";
				}
				else if (checkY < 7 * (Frame.Height / 8) && checkY >= 6 * (Frame.Height / 8)){
					Proto2Dim1 = 2;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Zufrieden";

				}
				else if (checkY < 6 * (Frame.Height / 8) && checkY >= 5 * (Frame.Height / 8))
				{
					Proto2Dim1 = 3;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else if (checkY < 5 * (Frame.Height / 8) && checkY >= 4 * (Frame.Height / 8))
				{
					Proto2Dim1 = 4;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else if (checkY < 4 * (Frame.Height / 8) && checkY >= 3 * (Frame.Height / 8))
				{
					Proto2Dim1 = 5;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else if (checkY < 3 * (Frame.Height / 8) && checkY >= 2 * (Frame.Height / 8))
				{
					Proto2Dim1 = 6;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else if (checkY < 2 * (Frame.Height / 8) && checkY >= 1 * (Frame.Height / 8))
				{
					Proto2Dim1 = 7;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else {
					Proto2Dim1 = 8;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 1;
						Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 3;
						Proto2Dim3 = 3;
					}
					//myLabel.Text = "Nervös";
					//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
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
				nfloat checkX = ((UITouch)touchc).LocationInNode(this).X;
				nfloat checkY = ((UITouch)touchc).LocationInNode(this).Y;
			//	oneSprite.RemoveAllActions();
			//	oneSprite.Position = new CGPoint(checkX, checkY);
			//	fireEmitter.Position = new CGPoint(checkX, checkY);
				fireEmitter.Position = locationc;
				updateEmitter(checkX,checkY);
				//*******
				if (firstTouch == false)
				{
					//AddChild(oneSprite);
					AddChild(fireEmitter);
					firstTouch = true;
				}
			
				/*
				UIColor coloring;
				double speed = 0;
				if (checkY > 3 * (Frame.Height / 4))
				{
					speed = 0.1;
					coloring = UIColor.Green;
					//myLabel.Text = "Glücklich";
					myLabel.Position = new CGPoint(Frame.Width / 2, (7 * (Frame.Height / 8)));
				}
				else if (checkY < 3 * (Frame.Height / 4) && checkY > Frame.Height / 2)
				{
					speed = 0.5;
					coloring = UIColor.Blue;
				//	myLabel.Text = "Zufrieden";
					myLabel.Position = new CGPoint(Frame.Width / 2, (5 * (Frame.Height / 8)));

				}
				else if (checkY < Frame.Height / 2 && checkY > Frame.Height / 4)
				{
					speed = 2;
					coloring = UIColor.Orange;
					//myLabel.Text = "Beunruhigt";
					myLabel.Position = new CGPoint(Frame.Width / 2, (3 * (Frame.Height / 8)));

				}
				else {
					speed = 4;
					coloring = UIColor.Red;
					//myLabel.Text = "Nervös";
					myLabel.Position = new CGPoint(Frame.Width / 2, (1 * (Frame.Height / 8)));
					//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
				}

				SKAction scaleUp = SKAction.ScaleTo(3f, 5 - speed);
				SKAction scaleDown = SKAction.ScaleTo(1f, 5 - speed);
				SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleDown);
			*/
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}
	}
}
