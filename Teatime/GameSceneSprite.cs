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
		SKSpriteNode infoSprite;
		SKShapeNode yourline;
		SKSpriteNode container;
		SKLabelNode myLabel;
		SKLabelNode myLabelIntro;
		SKLabelNode myLabelInfo;
		SKLabelNode myLabelIntro2;
		SKLabelNode myLabelInfo2;
		SKLabelNode myLabelIntro3;
		SKLabelNode myLabelInfo3;
		SKLabelNode myLabelInfo4;
		SKLabelNode mySave;
		SKEmitterNode fireEmitter;
		bool switchInfo;
		bool firstTouch;
		bool infoTouch;
		int blurFactor;
		protected GameSceneSprite(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			switchInfo = false;
			Proto2Dim1 = 0;
			Proto2Dim2 = 0;
			Proto2Dim3 = 1;
			blurFactor = 1;
			// Setup Sprite Scene
			infoTouch = false;
			// New Label placed in the middle of the Screen
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Hier kannst du deine Stimmung erfassen",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2),
				ZPosition = 30
			};
			myLabel.Alpha = 0.9f;
			myLabel.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));

			myLabelIntro = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Berühre den Bildschirm",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) +30),
				ZPosition = 30
			};
			myLabelIntro.Alpha = 0.0f;
			myLabelIntro.RunAction(SKAction.Sequence(SKAction.WaitForDuration(2.5), SKAction.FadeInWithDuration(1),SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));


			myLabelInfo = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "und navigiere die Punkte",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) - 30),
				ZPosition = 30
			};
			myLabelInfo.Alpha = 0.0f;
			myLabelInfo.RunAction(SKAction.Sequence(SKAction.WaitForDuration(2.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));


			myLabelIntro2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "so dass diese schlussendlich",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) + 30),
				ZPosition = 30
			};
			myLabelIntro2.Alpha = 0.0f;
			myLabelIntro2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(6), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));


			myLabelInfo2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "deiner aktuellen Gefühlslage entsprechen",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) - 30),
				ZPosition = 30
			};
			myLabelInfo2.Alpha = 0.0f;
			myLabelInfo2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(6), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));


			myLabelIntro3 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "wenn du die Punkte lange drückst",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) + 30),
				ZPosition = 30
			};
			myLabelIntro3.Alpha = 0.0f;
			myLabelIntro3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(9.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));


			myLabelInfo3 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "kannst du die Punkte verändern",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) - 30),
				ZPosition = 30
			};
			myLabelInfo3.Alpha = 0.0f;
			myLabelInfo3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(9.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));

			myLabelInfo4 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Informationen zu Farben >",
				FontSize = 20,
				Position = new CGPoint(Frame.Width -180, (Frame.Height) - 50),
				ZPosition = 30
			};
			myLabelInfo4.Alpha = 0.0f;
			myLabelInfo4.RunAction(SKAction.Sequence(SKAction.WaitForDuration(13), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1.5), SKAction.FadeOutWithDuration(1)));




			// Background gradient sprite node
			container = new SKSpriteNode("background");
			container.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			container.Size = new CGSize(Frame.Width, Frame.Height);
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

			infoSprite = new SKSpriteNode("info");
			infoSprite.Position = new CGPoint(Frame.Width -40, Frame.Height -40);
			infoSprite.ZPosition = 10;
			infoSprite.XScale = 0.8f;
			infoSprite.YScale = 0.8f;
			infoSprite.Alpha = 0.8f;
			infoSprite.Name = "info";
		//	infoSprite.ColorBlendFactor = 1f;
			//infoSprite.Color = UIColor.FromHSB(0, 0, 0);
			AddChild(infoSprite);
			mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "next >",
				FontSize = 18,
				Position = new CGPoint(Frame.Width - 60, (Frame.Height - 48))
			};
			mySave.FontColor = UIColor.FromHSB((nfloat)0, 0, 3f);
			mySave.Alpha = 0.0f;
			mySave.ZPosition = 2;
			this.AddChild(mySave);


			AddChild(myLabel);
			AddChild(myLabelIntro);
			AddChild(myLabelInfo); 
			AddChild(myLabelIntro2);
			AddChild(myLabelInfo2); 
			AddChild(myLabelIntro3);
			AddChild(myLabelInfo3);
			AddChild(myLabelInfo4);
			AddChild(container);
			createEmitterNode();

			var gestureLongRecognizer = new UILongPressGestureRecognizer(PressHandler);
			gestureLongRecognizer.MinimumPressDuration = 0.5;
			this.View.AddGestureRecognizer(gestureLongRecognizer);
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

		void PressHandler(UILongPressGestureRecognizer gestureRecognizer)
		{
	
			var image = gestureRecognizer.View;
			if (gestureRecognizer.State == UIGestureRecognizerState.Began)
			//	|| gestureRecognizer.State == UIGestureRecognizerState.Changed)
			//if (gestureRecognizer.State == UIGestureRecognizerState.Ended)
			{
			//	var location = gestureRecognizer.LocationInView(this.View);
				var locationTouched = gestureRecognizer.LocationInView(this.View);
				nfloat coordX = locationTouched.X;
				nfloat coordY = locationTouched.Y;
				setDimensions(coordX, coordY);
				if (blurFactor == 1)
				{
					blurFactor = 5;
					updateBlurNode(blurFactor);
				}
			/*	else if (blurFactor == 2)
				{
					updateBlurNode(blurFactor);
					blurFactor++;
				}
				else if (blurFactor == 3)
				{
					updateBlurNode(blurFactor);
					blurFactor++;
				}
				else if (blurFactor == 4)
				{
					updateBlurNode(blurFactor);
					blurFactor++;
				}*/
				else {
					blurFactor = 1;
					updateBlurNode(blurFactor);

				}
				if (gestureRecognizer.State == UIGestureRecognizerState.Ended)
				{

				}
			}
				//	var convertLocation = new CGPoint(location.X, this.View.Frame.Height - location.Y);         //var location = gestureRecognizer.LocationOfTouch(0, image);
																												//	var location = ((UITouch)touch).LocationInNode(this);
		}
		public void updateBlurNode(int blur)
		{

			Proto2Dim3 = blur;
			fireEmitter.ParticleScale = 0.6f * ((1 / Frame.Width) * blur*20) + 0.2f;
			fireEmitter.ParticleScaleRange = 0.3f * ((1 / Frame.Width) * blur*50 * 10) + 0.2f;

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
			fireEmitter.ParticleAlpha = 0.4f;
			//fireEmitter.EmissionAngle = 300f;
			fireEmitter.XAcceleration = 0;
			fireEmitter.YAcceleration = 1;
			fireEmitter.EmissionAngle = 100f;
			//	fireEmitter.ParticleLifetimeRange
			fireEmitter.TargetNode = this;
			UIImage image = UIImage.FromBundle("spark");
			fireEmitter.ParticleScale = 0.4f;
			fireEmitter.ParticleSpeedRange = 100f;
			fireEmitter.ParticleScaleRange = 0.5f;
			fireEmitter.ParticleScaleSpeed = -0.1f;
			fireEmitter.ParticleBirthRate = 500;
			fireEmitter.ParticlePositionRange = new CGVector(120f, 120f);
			fireEmitter.ParticleLifetimeRange = 10f;
			fireEmitter.ParticleRotationRange = 10f;
			//fireEmitter.ParticleLifetime = 10f;
			fireEmitter.EmissionAngleRange = 200f;
			fireEmitter.ParticleTexture = SKTexture.FromImageNamed(("spark"));
			//this.AddChild(fireEmitter);


		}
		public void updateEmitter(nfloat coordX,nfloat coordY) { 
			//fireEmitter.ParticleScale = 0.6f * ((1/Frame.Width)*coordX) +0.2f ;
			fireEmitter.ParticleSpeedRange = 10f+(coordX/5);
			//fireEmitter.ParticleScaleRange = 0.3f*((1 / Frame.Width) * coordX*10) +0.2f;
			fireEmitter.ParticleScaleSpeed = -0.02f;

			fireEmitter.ParticleBirthRate = 20f+(coordX/5);
		
		}
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			if (infoTouch == false)
			{
				base.TouchesMoved(touches, evt);
				// get the touch
				UITouch touch = touches.AnyObject as UITouch;
				var location = ((UITouch)touch).LocationInNode(this);
				if (touch != null)
				{
					nfloat offsetX = (nfloat)(touch.LocationInNode(this).X);
					nfloat offsetY = (nfloat)(touch.LocationInNode(this).Y);
					updateEmitter(offsetX, offsetY);
					fireEmitter.Position = location;


				}
			}
		}
		public void setDimensions(nfloat coordX, nfloat coordY)
		{
			nfloat checkX = coordX;
			nfloat checkY = coordY;
			if (checkY >= 4 * (Frame.Height / 5))
			{
				Proto2Dim1 = 1;
				if (checkX >= 2 * (Frame.Width / 3))
				{
					Proto2Dim2 = 3;
					//Proto2Dim3 = 3;
				}
				else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
				{
					Proto2Dim2 = 2;
					//	Proto2Dim3 = 2;
				}
				else
				{
					Proto2Dim2 = 1;
					//	Proto2Dim3 = 3;
				}

				//myLabel.Text = "Glücklich";
			}
			else if (checkY < 4 * (Frame.Height / 5) && checkY >= 3 * (Frame.Height / 5))
			{
				Proto2Dim1 = 2;
				if (checkX >= 2 * (Frame.Width / 3))
				{
					Proto2Dim2 = 3;
					//	Proto2Dim3 = 1;
				}
				else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
				{
					Proto2Dim2 = 2;
					//Proto2Dim3 = 2;
				}
				else
				{
					Proto2Dim2 = 1;
					//Proto2Dim3 = 3;
				}
				//myLabel.Text = "Zufrieden";

			}
			else if (checkY < 3 * (Frame.Height / 5) && checkY >= 2 * (Frame.Height / 5))
			{
				Proto2Dim1 = 3;
				if (checkX >= 2 * (Frame.Width / 3))
				{
					Proto2Dim2 = 3;
					//Proto2Dim3 = 1;
				}
				else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
				{
					Proto2Dim2 = 2;
					//Proto2Dim3 = 2;
				}
				else
				{
					Proto2Dim2 = 1;
					//Proto2Dim3 = 3;
				}
				//myLabel.Text = "Beunruhigt";

			}
			else if (checkY < 2 * (Frame.Height / 5) && checkY >= 1 * (Frame.Height / 5))
			{
				Proto2Dim1 = 4;
				if (checkX >= 2 * (Frame.Width / 3))
				{
					Proto2Dim2 = 3;
					//Proto2Dim3 = 1;
				}
				else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
				{
					Proto2Dim2 = 2;
					//Proto2Dim3 = 2;
				}
				else
				{
					Proto2Dim2 = 1;
					//Proto2Dim3 = 3;
				}
				//myLabel.Text = "Beunruhigt";

			}
			else {
				Proto2Dim1 = 5;
				if (checkX >= 2 * (Frame.Width / 3))
				{
					Proto2Dim2 = 3;
					//Proto2Dim3 = 1;
				}
				else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
				{
					Proto2Dim2 = 2;
					//Proto2Dim3 = 2;
				}
				else
				{
					Proto2Dim2 = 1;
					//Proto2Dim3 = 3;
				}
				//myLabel.Text = "Nervös";
				//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
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
				setDimensions(checkX, checkY);
				/*
				if (checkY >= 4 * (Frame.Height / 5))
				{
					Proto2Dim1 = 1;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 3;
						//Proto2Dim3 = 3;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
					//	Proto2Dim3 = 2;
					}
					else 
					{
						Proto2Dim2 = 1;
					//	Proto2Dim3 = 3;
					}

					//myLabel.Text = "Glücklich";
				}
				else if (checkY < 4 * (Frame.Height / 5) && checkY >= 3 * (Frame.Height / 5)){
					Proto2Dim1 = 2;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 3;
					//	Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						//Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 1;
						//Proto2Dim3 = 3;
					}
					//myLabel.Text = "Zufrieden";

				}
				else if (checkY < 3 * (Frame.Height / 5) && checkY >= 2 * (Frame.Height / 5))
				{
					Proto2Dim1 = 3;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 3;
						//Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						//Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 1;
						//Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else if (checkY < 2 * (Frame.Height / 5) && checkY >= 1 * (Frame.Height / 5))
				{
					Proto2Dim1 = 4;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 3;
						//Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						//Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 1;
						//Proto2Dim3 = 3;
					}
					//myLabel.Text = "Beunruhigt";

				}
				else {
					Proto2Dim1 = 5;
					if (checkX >= 2 * (Frame.Width / 3))
					{
						Proto2Dim2 = 3;
						//Proto2Dim3 = 1;
					}
					else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
					{
						Proto2Dim2 = 2;
						//Proto2Dim3 = 2;
					}
					else
					{
						Proto2Dim2 = 1;
						//Proto2Dim3 = 3;
					}
					//myLabel.Text = "Nervös";
					//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
				}*/
			}
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{

			myLabel.Alpha = 0f;
			// Called when a touch begins
			foreach (var touch in touches)
			{
				UITouch touchc = touches.AnyObject as UITouch;
				//SKNodeTouches_UITouch touch = touches.AnyObject as SKNodeTouches_UITouch;
				var locationc = ((UITouch)touchc).LocationInNode(this);
				var nodeType = GetNodeAtPoint(locationc);
				infoTouch = false;
				if (nodeType.Name == "info")
				{
					infoTouch = true;

					if (switchInfo == false)
					{
						switchInfo = true;
						SKAction seqTexture = SKAction.SetTexture(SKTexture.FromImageNamed(("background_n")));
						container.RunAction((seqTexture));
						SKAction seqTextureInfo = SKAction.SetTexture(SKTexture.FromImageNamed(("inforeverse")));
						infoSprite.RunAction((seqTextureInfo));
					}
					else {
						switchInfo = false;
						SKAction seqTextureNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("background")));
						container.RunAction((seqTextureNormal));
						SKAction seqTextureInfoNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("info")));
						infoSprite.RunAction((seqTextureInfoNormal));
					}
				}
				else {
					//**********
		

					// Check click
					nfloat checkX = ((UITouch)touchc).LocationInNode(this).X;
					nfloat checkY = ((UITouch)touchc).LocationInNode(this).Y;
					//	oneSprite.RemoveAllActions();
					//	oneSprite.Position = new CGPoint(checkX, checkY);
					//	fireEmitter.Position = new CGPoint(checkX, checkY);
					fireEmitter.Position = locationc;
					updateEmitter(checkX, checkY);
					//*******
					if (firstTouch == false)
					{
						//AddChild(oneSprite);
						AddChild(fireEmitter);
						firstTouch = true;
				}
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
