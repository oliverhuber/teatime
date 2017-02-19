using System;
using System.Drawing;
using System.Linq;
using AudioToolbox;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;
namespace Teatime
{
	public class GameSceneScene : SKScene
	{
		public int Proto3Dim1 { get; set; }
		public int Proto3Dim2 { get; set; }
		public int Proto3Dim3 { get; set; }
		nfloat hueTop, satTop, briTop, alpTop, hueBel, satBel, briBel, alpBel;

		double manipulate;
		int gameMode;
		SKLabelNode myLabel;
		SKLabelNode mySave;
		SKLabelNode myLabel2;
		SKSpriteNode navSprite;
		SKSpriteNode navSpriteTop;
		SKSpriteNode navSpriteBottom;
		UIColor lastColor;
		SKShapeNode followDragNode;
		FieldNode spriteTop;
		FieldNode spriteTopBg;
		FieldNode spriteBelow;
		FieldNode spriteBelowBg;
		SKEmitterNode fireEmitter;
		Random rnd = new Random();
		bool startDragMenu;
		bool actionDone;
		nfloat lastY;
		protected GameSceneScene(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			Proto3Dim1 = 0;
			Proto3Dim2 = 0;
			Proto3Dim3 = 0;
			gameMode = 0;
			generateSparks();
			createEmitterNode();
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Wie fühlst du dich?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) + 20)
			};
			myLabel.FontColor = UIColor.FromHSB((nfloat)0, 0, 3f);
			hueTop = 0;
			hueBel = 0;
			satTop = 0;
			satBel = 0;
			alpTop = 0;
			alpBel = 0;
			briTop = 0.3f;
			briBel = 0.6f;

			myLabel.Alpha = 0.9f;
			myLabel.ZPosition = 2;
			myLabel2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Wie möchtest du dich fühlen?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) -40)
			};
			myLabel2.FontColor = UIColor.FromHSB((nfloat)0, 0, 6f);
			//myLabel2.Frame.Size.Width = new CGSize(myLabel2.Frame.Width + 100, myLabel2.Frame.Height + 40);
			myLabel2.ZPosition = 2;
			myLabel2.Alpha = 0.9f;
			this.AddChild(myLabel);
			this.AddChild(myLabel2);

			navSprite = new SKSpriteNode();
			navSprite.Name = "navSprite";
			navSprite.Alpha = 0.0000001f;
			navSprite.ZPosition = 10;
			navSprite.Color = UIColor.FromHSB((nfloat)0, 1, 0.0f);
			navSprite.Size = new CGSize(140, 70);
			navSprite.Position = new CGPoint((this.View.Frame.Width - (70)), (this.View.Frame.Height - (35)));
		//	AddChild(navSprite);

			navSpriteTop = new SKSpriteNode();
			navSpriteTop.Name = "navSpriteTop";
			navSpriteTop.Alpha = 0.0000001f;
			navSpriteTop.ZPosition = 1.1f;
			navSpriteTop.Color = UIColor.FromHSB((nfloat)0, 1, 0.0f);
			navSpriteTop.Size = new CGSize(Frame.Width, 70);
			navSpriteTop.Position = new CGPoint((this.View.Frame.Width/2 ), (this.View.Frame.Height/2 + (35)));
			AddChild(navSpriteTop);

			navSpriteBottom = new SKSpriteNode();
			navSpriteBottom.Name = "navSpriteBottom";
			navSpriteBottom.Alpha = 0.0000001f;
			navSpriteBottom.ZPosition = 1.1f;
			navSpriteBottom.Color = UIColor.FromHSB((nfloat)1, 0, 0.0f);
			navSpriteBottom.Size = new CGSize(Frame.Width, 70);
			navSpriteBottom.Position = new CGPoint((this.View.Frame.Width / 2), (this.View.Frame.Height / 2 - (35)));
			AddChild(navSpriteBottom);


			mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "next >",
				FontSize = 18,
				Position = new CGPoint(Frame.Width - 60, (Frame.Height - 48))
			};
			mySave.FontColor = UIColor.FromHSB((nfloat)0, 0, 3f);
			mySave.Alpha = 0.0f;
			mySave.ZPosition = 2;
			//this.AddChild(mySave);
			startDragMenu = false;
			actionDone = false;
		}
		public void saveProto3Input()
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
			item.Dim1 = Proto3Dim1;
			item.Dim2 = Proto3Dim2;
			item.Dim3 = Proto3Dim3;
			item.PrototypeNr = 3;
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
		public void followDrag()
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var spark in Children.OfType<SKSpriteNode>())
			{
				//spark.followDrag();

			}
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
			fireEmitter.ParticlePositionRange = new CGVector(100f,100f);
			fireEmitter.ParticleLifetimeRange = 10f;
			fireEmitter.ParticleRotationRange = 10f;
			//fireEmitter.ParticleLifetime = 10f;
			fireEmitter.EmissionAngleRange = 100f;
			fireEmitter.ParticleTexture=SKTexture.FromImageNamed(("spark"));
			//this.AddChild(fireEmitter);
	
    
		}

		public void generateSparks()
		{


			bool blackOrWhite = false;
			// Generate 5 nodes per 10 rows
			//for (int i = 1; i <= 1; i++)
			//{
			//	for (int y = 1; y <= 2; y++)
			//	{
			// Calculate location of each Spark (5 Sparks per row, for 10 rows)
			//	var location = new CGPoint();
			//	location.X = (((this.View.Frame.Width / 2) * (2 * i)) - (this.View.Frame.Width / 2));
			//	location.Y = (((this.View.Frame.Height / 4) * (2 * y)) - (this.View.Frame.Height / 4));
			var locationTop = new CGPoint();
			locationTop.X = (this.View.Frame.Width / 2);
			locationTop.Y = (this.View.Frame.Height);
			var locationBelow = new CGPoint();
			locationBelow.X = (this.View.Frame.Width / 2);
			locationBelow.Y = 0;
			// Define Spark with location and alpha
			spriteTopBg = new FieldNode()
			{
				Position = locationTop,
				Name = "Top",
				XScale = 1,
				YScale = 1,
				Alpha = 0f,
				Texture = SKTexture.FromImageNamed(("background"))
			

			};
			spriteTop = new FieldNode()
			{
				Position = locationTop,
				Name = "TopBg",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB((nfloat)0.8f, 0.5f, 0.5f)

			};
			spriteBelow = new FieldNode()
			{
				Position = locationBelow,
				Name = "Below",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB((nfloat)0.5f, 0.5f, 0.5f)

			};

			spriteBelowBg = new FieldNode()
			{
				Position = locationBelow,
				Name = "BelowBg",
				XScale = 1,
				YScale = 1,
				Alpha = 0f,
				Texture = SKTexture.FromImageNamed(("background"))

			};

			UIBezierPath tempPath = new UIBezierPath();
			tempPath.MoveTo(new CGPoint(0, 0));
			tempPath.AddArc( new CGPoint(0,0),60,0, 2.0f * (float)Math.PI,true);
			//tempPath.st
			//tempPath.Fill();
			//tempPath.ClosePath();
			//tempPath.Stroke();
			                               
			CGPath convertedPath = tempPath.CGPath;
			followDragNode = new SKShapeNode()
			{
				Position = locationTop,
				Path = convertedPath,
				Name = "follow",
				XScale = 1,
				YScale = 1,
				Alpha = 0f,
				FillColor = UIColor.FromHSB((nfloat)0, 0, 1f),
			//	StrokeColor=UIColor.FromHSB((nfloat)0, 0, 1f),

				                   
			};
			// Position in comparsion to other SpriteNodes
			spriteTop.ZPosition = 1;
			spriteBelow.ZPosition = 1;
			spriteTopBg.ZPosition = 0;
			spriteBelowBg.ZPosition = 0;
			followDragNode.ZPosition = 0.5f;
			spriteTopBg.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteBelowBg.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);		
			spriteTop.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteBelow.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			this.AddChild(spriteTopBg);
			this.AddChild(spriteBelowBg); 
			this.AddChild(spriteTop);
			this.AddChild(spriteBelow);
			this.AddChild(followDragNode);
			    //	}
		//	}
		}
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				var locationc = ((UITouch)touch).LocationInNode(this);
				// Check click
				nfloat checkX = ((UITouch)touch).LocationInNode(this).X;
				nfloat checkY = ((UITouch)touch).LocationInNode(this).Y;
				nfloat checkYBig = ((UITouch)touch).LocationInView(View).Y;
				lastY = checkY;
				if (gameMode!=0  && startDragMenu == false) {
					


					//hueTop = (nfloat)(checkY / Frame.Height);
					//satTop = 0.5f;
					//briTop = (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));
					//lastColor =  UIColor.FromHSB((nfloat)(manipulate), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
				


					if (gameMode == 2  && checkY > 0 + 70 )
					{
						manipulate = (checkY / Frame.Height) + 0.2;
						if (manipulate > 1)
						{
							manipulate = manipulate - 1;
						}
						spriteTop.Alpha = 0f;
						spriteTopBg.Alpha = 1f;
						hueTop = (nfloat)(manipulate);
						satTop = 0.5f;
						briTop = (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));
						lastColor = UIColor.FromHSB((nfloat)(manipulate), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
						spriteTop.Color = lastColor;
						followDragNode.FillColor = lastColor;
						followDragNode.StrokeColor = lastColor;
						followDragNode.Alpha = 0.8f;
						followDragNode.Position = locationc;
					}
					else if (gameMode == 1 && checkY < Frame.Height - 70)
					{
						manipulate = (checkY / Frame.Height) + 0.37;
						if (manipulate > 1)
						{
							manipulate = manipulate - 1;
						}
						spriteBelow.Alpha = 0f;
						spriteBelowBg.Alpha = 1f;
						hueBel = (nfloat)(manipulate);
						satBel = 0.5f;
						briBel = (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));
						lastColor = UIColor.FromHSB((nfloat)(manipulate), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

						spriteBelow.Color = lastColor;
						followDragNode.FillColor = lastColor;
						followDragNode.StrokeColor = lastColor;
						followDragNode.Alpha = 0.8f;
						followDragNode.Position = locationc;
					}



				}
				if (gameMode == 2 && checkY <= 0 + 100 && startDragMenu == true && actionDone == true)
				{
					spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, ((this.View.Frame.Height * 0.5f)  + 60) + checkY);
					spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X, -this.View.Frame.Height / 2  + 60 + checkY);
					spriteTop.Position = new CGPoint(spriteTop.Position.X, ((this.View.Frame.Height * 0.5f)  + 60) + checkY);
					spriteBelow.Position = new CGPoint(spriteBelowBg.Position.X, -this.View.Frame.Height / 2  + 60 + checkY);
					myLabel.Position = new CGPoint(myLabel2.Position.X, checkY + 80);
					myLabel2.Position = new CGPoint(myLabel.Position.X, checkY + 20);
					navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY + 80);
					navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY + 20);
				}
				else if (gameMode == 2 && checkY > 0 + 100 && startDragMenu == true && actionDone==true)
				{
					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height, 0.2);
					SKAction action1_1 = SKAction.MoveToY(this.View.Frame.Height + 5, 0.1);
					SKAction action1_2 = SKAction.MoveToY(this.View.Frame.Height - 5, 0.1);

					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action2_1 = SKAction.MoveToY(0 + 5, 0.1);
					SKAction action2_2 = SKAction.MoveToY(0 - 5, 0.1);

					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height / 2 + 20, 0.2);
					SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height / 2 + 25, 0.1);
					SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height / 2 + 15, 0.1);

					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height / 2 - 40, 0.2);
					SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height / 2 - 35, 0.1);
					SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height / 2 - 45, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
					SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
					SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
					SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel.RunAction(seq3);

					myLabel2.RunAction(seq4);
					navSpriteTop.RunAction(seq4);
					gameMode = 0;
					//startDragMenu = false;
					actionDone = false;
					//SystemSound.Vibrate.PlayAlertSound();
				}

				else if (gameMode == 1 && checkY >= Frame.Height - 100 && startDragMenu == true && actionDone == true)
				{



					//Move
					spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, ((this.View.Frame.Height * 1.5f) - Frame.Height - 60) + checkY);
					spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X,  this.View.Frame.Height / 2 -Frame.Height -60 +checkY);
					spriteTop.Position = new CGPoint(spriteTop.Position.X, ((this.View.Frame.Height * 1.5f) - Frame.Height  - 60) + checkY);
					spriteBelow.Position = new CGPoint(spriteBelowBg.Position.X, this.View.Frame.Height / 2 -Frame.Height- 60 + checkY);
					myLabel.Position = new CGPoint(myLabel2.Position.X, checkY  - 40);
					myLabel2.Position = new CGPoint(myLabel.Position.X, checkY  - 100);
					navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY - 40);
					navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY - 100);


				} else if (gameMode == 1 && checkY < Frame.Height - 100 && startDragMenu == true && actionDone == true)
				{
					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height, 0.2);
					SKAction action1_1 = SKAction.MoveToY(this.View.Frame.Height + 5, 0.1);
					SKAction action1_2 = SKAction.MoveToY(this.View.Frame.Height - 5, 0.1);

					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action2_1 = SKAction.MoveToY(0 + 5, 0.1);
					SKAction action2_2 = SKAction.MoveToY(0 - 5, 0.1);

					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height / 2 + 20, 0.2);
					SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height / 2 + 25, 0.1);
					SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height / 2 + 15, 0.1);

					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height / 2 - 40, 0.2);
					SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height / 2 - 35, 0.1);
					SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height / 2 - 45, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_2, action1_1, action1);
					SKAction seq2 = SKAction.Sequence(action2_2, action2_1, action2);
					SKAction seq3 = SKAction.Sequence(action3_2, action3_1, action3);
					SKAction seq4 = SKAction.Sequence(action4_2, action4_1, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel.RunAction(seq3);

					myLabel2.RunAction(seq4);
					navSpriteTop.RunAction(seq4);
					//startDragMenu = false;
					actionDone = false;
					//SystemSound.Vibrate.PlaySystemSound();

					gameMode = 0;
				}

				if (gameMode == 0  && startDragMenu == true && actionDone == true)
				{
					if (checkY <= (Frame.Height / 2) + 70 && checkY  > Frame.Height / 2)
					{


						//Move
						spriteTopBg.Position=new CGPoint(spriteTopBg.Position.X,this.View.Frame.Height/2+checkY);
						spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X, checkY-this.View.Frame.Height / 2);
						spriteTop.Position = new CGPoint(spriteTop.Position.X, this.View.Frame.Height/2+checkY);
						spriteBelow.Position = new CGPoint(spriteBelow.Position.X, checkY - this.View.Frame.Height / 2);
						myLabel.Position = new CGPoint(myLabel2.Position.X, checkY + 20);
						myLabel2.Position = new CGPoint(myLabel.Position.X, checkY - 40);
						navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY + 20);
						navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY - 40);
					}
					else if (checkY >= (Frame.Height / 2) - 70 && checkY < Frame.Height / 2)
					{
						//Move
						//Move
						spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, this.View.Frame.Height/2 + checkY);
						spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X,checkY - this.View.Frame.Height / 2);
						spriteTop.Position = new CGPoint(spriteTop.Position.X, this.View.Frame.Height/2 + checkY);
						spriteBelow.Position = new CGPoint(spriteBelow.Position.X, checkY - this.View.Frame.Height / 2);
						myLabel.Position = new CGPoint(myLabel2.Position.X, checkY + 20);
						myLabel2.Position = new CGPoint(myLabel.Position.X, checkY - 40);
						navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY + 20);
						navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY - 40);
					}
					else if (checkY > (Frame.Height / 2+70))
					{
						SKAction action1 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 60, 0.2);
						SKAction action1_1 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 55, 0.1);
						SKAction action1_2 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 65, 0.1);


						SKAction action2 = SKAction.MoveToY((this.View.Frame.Height / 2) - 60, 0.2);
						SKAction action2_1 = SKAction.MoveToY((this.View.Frame.Height / 2) - 55, 0.1);
						SKAction action2_2 = SKAction.MoveToY((this.View.Frame.Height / 2) - 65, 0.1);


						SKAction action3 = SKAction.MoveToY(this.View.Frame.Height - 100, 0.2);
						SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height - 95, 0.1);
						SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height - 105, 0.1);


						SKAction action4 = SKAction.MoveToY(this.View.Frame.Height - 40, 0.2);
						SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height - 35, 0.1);
						SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height - 45, 0.1);

						SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
						SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
						SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
						SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

						spriteTop.RunAction(seq1);
						spriteTopBg.RunAction(seq1);

						spriteBelow.RunAction(seq2);
						spriteBelowBg.RunAction(seq2);

						navSpriteBottom.RunAction(seq3);
						myLabel2.RunAction(seq3);

						myLabel.RunAction(seq4);
						navSpriteTop.RunAction(seq4);
						gameMode = 1;
						//startDragMenu = false;
						actionDone = false;
					}
					else if (checkY < Frame.Height / 2 -70) {
						SKAction action1 = SKAction.MoveToY((this.View.Frame.Height / 2) + 60, 0.2);
						SKAction action1_1 = SKAction.MoveToY((this.View.Frame.Height / 2) + 55, 0.1);
						SKAction action1_2 = SKAction.MoveToY((this.View.Frame.Height / 2) + 65, 0.1);

						SKAction action2 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 60, 0.2);
						SKAction action2_1 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 55, 0.1);
						SKAction action2_2 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 65, 0.1);

						SKAction action3 = SKAction.MoveToY(0 + 20, 0.2);
						SKAction action3_1 = SKAction.MoveToY(0 + 15, 0.1);
						SKAction action3_2 = SKAction.MoveToY(0 + 25, 0.1);

						SKAction action4 = SKAction.MoveToY(0 + 80, 0.2);
						SKAction action4_1 = SKAction.MoveToY(0 + 75, 0.1);
						SKAction action4_2 = SKAction.MoveToY(0 + 85, 0.1);

						SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
						SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
						SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
						SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

						spriteTop.RunAction(seq1);
						spriteTopBg.RunAction(seq1);

						spriteBelow.RunAction(seq2);
						spriteBelowBg.RunAction(seq2);

						navSpriteBottom.RunAction(seq3);
						myLabel2.RunAction(seq3);

						myLabel.RunAction(seq4);
						navSpriteTop.RunAction(seq4);
						gameMode = 2;
					//	startDragMenu = false;
						actionDone = false;


					}
				}

			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			UITouch touchc = touches.AnyObject as UITouch;
			//SKNodeTouches_UITouch touch = touches.AnyObject as SKNodeTouches_UITouch;



			if (touchc != null)
			{
				var locationc = ((UITouch)touchc).LocationInNode(this);
				var nodeType = GetNodeAtPoint(locationc);
				//FieldNode touchedNode = (Teatime.FieldNode)GetNodeAtPoint(locationc);
				if (actionDone == true && startDragMenu == true && (lastY > Frame.Height / 2 - 70) && lastY < Frame.Height/2 )
				{
					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height, 0.2);
					SKAction action1_1 = SKAction.MoveToY(this.View.Frame.Height+5, 0.1);
					SKAction action1_2 = SKAction.MoveToY(this.View.Frame.Height-5, 0.1);

					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action2_1 = SKAction.MoveToY(0+5, 0.1);
					SKAction action2_2 = SKAction.MoveToY(0-5, 0.1);

					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height / 2 + 20, 0.2);
					SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height / 2 + 25, 0.1);
					SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height / 2 + 15, 0.1);

					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height / 2 - 40, 0.2);
					SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height / 2 - 35, 0.1);
					SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height / 2 - 45, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
					SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
					SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
					SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel.RunAction(seq3);

					myLabel2.RunAction(seq4);
					navSpriteTop.RunAction(seq4);

				}
				else if (actionDone == true && startDragMenu == true && (lastY < Frame.Height / 2 + 70) && lastY > Frame.Height / 2)
				{
					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height, 0.2);
					SKAction action1_1 = SKAction.MoveToY(this.View.Frame.Height + 5, 0.1);
					SKAction action1_2 = SKAction.MoveToY(this.View.Frame.Height - 5, 0.1);

					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action2_1 = SKAction.MoveToY(0 + 5, 0.1);
					SKAction action2_2 = SKAction.MoveToY(0 - 5, 0.1);

					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height / 2 + 20, 0.2);
					SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height / 2 + 25, 0.1);
					SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height / 2 + 15, 0.1);

					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height / 2 - 40, 0.2);
					SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height / 2 - 35, 0.1);
					SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height / 2 - 45, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_2, action1_1, action1);
					SKAction seq2 = SKAction.Sequence(action2_2, action2_1, action2);
					SKAction seq3 = SKAction.Sequence(action3_2, action3_1, action3);
					SKAction seq4 = SKAction.Sequence(action4_2, action4_1, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel.RunAction(seq3);

					myLabel2.RunAction(seq4);
					navSpriteTop.RunAction(seq4);
				}
			
				else if (gameMode == 1 && lastY > Frame.Height - 100 && startDragMenu == true && actionDone == true)
				{
					SKAction action1 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 60, 0.2);
					SKAction action1_1 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 55, 0.1);
					SKAction action1_2 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 65, 0.1);


					SKAction action2 = SKAction.MoveToY((this.View.Frame.Height / 2) - 60, 0.2);
					SKAction action2_1 = SKAction.MoveToY((this.View.Frame.Height / 2) - 55, 0.1);
					SKAction action2_2 = SKAction.MoveToY((this.View.Frame.Height / 2) - 65, 0.1);


					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height - 100, 0.2);
					SKAction action3_1 = SKAction.MoveToY(this.View.Frame.Height - 95, 0.1);
					SKAction action3_2 = SKAction.MoveToY(this.View.Frame.Height - 105, 0.1);


					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height - 40, 0.2);
					SKAction action4_1 = SKAction.MoveToY(this.View.Frame.Height - 35, 0.1);
					SKAction action4_2 = SKAction.MoveToY(this.View.Frame.Height - 45, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
					SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
					SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
					SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel2.RunAction(seq3);

					myLabel.RunAction(seq4);
					navSpriteTop.RunAction(seq4);
				}
				else if (gameMode == 2 && lastY < 0 + 100 && startDragMenu == true && actionDone == true)
				{

					SKAction action1 = SKAction.MoveToY((this.View.Frame.Height / 2) + 60, 0.2);
					SKAction action1_1 = SKAction.MoveToY((this.View.Frame.Height / 2) + 55, 0.1);
					SKAction action1_2 = SKAction.MoveToY((this.View.Frame.Height / 2) + 65, 0.1);

					SKAction action2 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 60, 0.2);
					SKAction action2_1 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 55, 0.1);
					SKAction action2_2 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 65, 0.1);

					SKAction action3 = SKAction.MoveToY(0 + 20, 0.2);
					SKAction action3_1 = SKAction.MoveToY(0 + 15, 0.1);
					SKAction action3_2 = SKAction.MoveToY(0 + 25, 0.1);

					SKAction action4 = SKAction.MoveToY(0 + 80, 0.2);
					SKAction action4_1 = SKAction.MoveToY(0 + 75, 0.1);
					SKAction action4_2 = SKAction.MoveToY(0 + 85, 0.1);

					SKAction seq1 = SKAction.Sequence(action1_1, action1_2, action1);
					SKAction seq2 = SKAction.Sequence(action2_1, action2_2, action2);
					SKAction seq3 = SKAction.Sequence(action3_1, action3_2, action3);
					SKAction seq4 = SKAction.Sequence(action4_1, action4_2, action4);

					spriteTop.RunAction(seq1);
					spriteTopBg.RunAction(seq1);

					spriteBelow.RunAction(seq2);
					spriteBelowBg.RunAction(seq2);

					navSpriteBottom.RunAction(seq3);
					myLabel2.RunAction(seq3);

					myLabel.RunAction(seq4);
					navSpriteTop.RunAction(seq4);
				}
			
		 	    startDragMenu = false;
				actionDone = false;
				if (gameMode != 0) 
				//if (nodeType is FieldNode || (nodeType.Name == "navSpriteTop" && gameMode == 0) || (nodeType.Name == "navSpriteBottom" && gameMode == 0) || (nodeType is SKLabelNode && gameMode == 0))
				{
					spriteTop.Alpha = 1f;
					spriteTopBg.Alpha = 0f;
					spriteBelow.Alpha = 1f;
					spriteBelowBg.Alpha = 0f;
					followDragNode.Alpha = 0f;
					Proto3Dim1 = (int)((hueTop - hueBel) * 30);
					Proto3Dim2 = (int)((satTop - satBel) * 30);
					Proto3Dim3 = (int)((briTop - briBel) * 30);

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

				nfloat checkYBig = ((UITouch)touchc).LocationInView(View).Y;


				var nodeType = GetNodeAtPoint(locationc);

				if (gameMode == 0 && checkY >= (Frame.Height / 2) - 70 && checkY <= (Frame.Height / 2) + 70)
				{
					startDragMenu = true;
					actionDone = true;
				}
			    else if (gameMode == 1 && checkY > Frame.Height - 120)
				{
					startDragMenu = true;
					actionDone = true;

				}
				else if (gameMode == 2 && checkY < 0 + 120)
				{
					startDragMenu = true;
					actionDone = true;

				}
				/*if ((nodeType is SKLabelNode && gameMode != 0) || (nodeType.Name == "navSprite" && gameMode != 0) || (nodeType.Name == "navSpriteTop" && gameMode != 0) || (nodeType.Name == "navSpriteBottom" && gameMode != 0))
				{
					mySave.Alpha = 0.0f;

					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height, 0.2);
					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height / 2 + 20, 0.2);
					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height / 2 - 40, 0.2);

					spriteTop.RunAction(action1);
					spriteTopBg.RunAction(action1);
					spriteBelow.RunAction(action2);
					spriteBelowBg.RunAction(action2);
					myLabel.RunAction(action3);
					myLabel2.RunAction(action4);
					navSpriteTop.RunAction(action4);
					navSpriteBottom.RunAction(action3);

					gameMode = 0;

				}*/
				 if (gameMode == 0)
				{
					 if ((checkY) < Frame.Height / 2 - 70)
						{
						SKAction action1 = SKAction.MoveToY((this.View.Frame.Height * 1.5f) - 60, 0.2);
						SKAction action2 = SKAction.MoveToY((this.View.Frame.Height / 2) - 60, 0.2);
						SKAction action3 = SKAction.MoveToY(this.View.Frame.Height - 100, 0.2);
						SKAction action4 = SKAction.MoveToY(this.View.Frame.Height - 40, 0.2);

						spriteTopBg.RunAction(action1);
						spriteBelowBg.RunAction(action2);
						spriteTop.RunAction(action1);
						spriteBelow.RunAction(action2);
						myLabel2.RunAction(action3);
						myLabel.RunAction(action4);
						navSpriteTop.RunAction(action4);
						navSpriteBottom.RunAction(action3);
						gameMode = 1;
					}
					else if ((checkY) > Frame.Height / 2 + 70)
					{
						SKAction action1 = SKAction.MoveToY((this.View.Frame.Height / 2) + 60, 0.2);
						SKAction action2 = SKAction.MoveToY((0 - this.View.Frame.Height / 2) + 60, 0.2);
						SKAction action3 = SKAction.MoveToY(0 + 20, 0.2);
						SKAction action4 = SKAction.MoveToY(0 + 80, 0.2);

						spriteTopBg.RunAction(action1);
						spriteBelowBg.RunAction(action2);
						spriteTop.RunAction(action1);
						spriteBelow.RunAction(action2);
						myLabel2.RunAction(action3);
						myLabel.RunAction(action4);
						navSpriteTop.RunAction(action4);
						navSpriteBottom.RunAction(action3);
						gameMode = 2;
					}
				}
				else if (startDragMenu==false && nodeType is FieldNode || (nodeType.Name == "navSpriteTop" && gameMode == 0) || (nodeType.Name == "navSpriteBottom" && gameMode == 0) || (nodeType is SKLabelNode && gameMode == 0) )
				{
					//	touchedNode.Color = UIColor.FromHSB((nfloat)0, 0, 1);
					// Define Spark with location and alpha
					if (gameMode == 1 || gameMode == 2)
					{
						if (nodeType is FieldNode)
						{
							FieldNode touchedNode = (Teatime.FieldNode)GetNodeAtPoint(locationc);
							
							var sprite = new SKSpriteNode("spark3")
							{
								Position = locationc,
								XScale = 1.6f,
								YScale = 1.6f,
								Alpha = 0.7f,
								ZPosition = 10         

							};
					


							if (touchedNode == spriteTop)
							{

								spriteTop.Alpha = 0f;
								spriteTopBg.Alpha = 1f;
							}
							else {


								spriteBelow.Alpha = 0f;
								spriteBelowBg.Alpha = 1f;
							}
						}
					}

					//UIColor topColor = spriteTop.Color;

					//UIColor belColor = spriteBelow.Color;

				

				}

			}
		}
		public void updateFieldNode(UITouch touch)
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var node in Children.OfType<FieldNode>())
			{


			}
		}
		public override void Update(double currentTime)
		{
			//fireEmitter.YAcceleration=rnd.Next(-500, 500);
			//fireEmitter.XAcceleration=rnd.Next(-500, 500);

			// Called before each frame is rendered
		}
	}
}
