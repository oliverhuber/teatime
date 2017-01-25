using System;
using System.Drawing;
using System.Linq;
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

		int gameMode;
		SKLabelNode myLabel;
		SKLabelNode mySave;
		SKLabelNode myLabel2;

		FieldNode spriteTop;
		FieldNode spriteBelow;
		SKEmitterNode fireEmitter;
		Random rnd = new Random();
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

			myLabel2.ZPosition = 2;
			myLabel2.Alpha = 0.9f;
			this.AddChild(myLabel);
			this.AddChild(myLabel2);
		}
		public void saveProto3Input()
		{
			//throw new NotImplementedException();
			TeatimeItem item;
			item = new TeatimeItem();
			item.Username = "Oliver";
			item.dateInserted = DateTime.Now.ToLocalTime();
			item.Dim1 = Proto3Dim1;
			item.Dim2 = Proto3Dim2;
			item.Dim3 = Proto3Dim3;
			item.PrototypeNr = 3;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
			// TeatimeItem returnItem =  DatabaseMgmt.Database.GetItem(2);
			// Console.WriteLine(returnItem.Username);
			Console.WriteLine("Start Output: --------------------------------");
			foreach (var s in DatabaseMgmt.Database.GetItemsUserOliver
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
			spriteTop = new FieldNode()
			{
				Position = locationTop,
				Name = "Top",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB((nfloat)0, 0, 0.3f)

			};

			spriteBelow = new FieldNode()
			{
				Position = locationBelow,
				Name = "Below",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB((nfloat)0, 0, 0.6f)

			};
			// Position in comparsion to other SpriteNodes
			spriteTop.ZPosition = 1;
			spriteBelow.ZPosition = 1;

		
			spriteTop.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteBelow.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			this.AddChild(spriteTop);
			this.AddChild(spriteBelow);
//	}
		//	}
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
				if (nodeType is SKLabelNode && gameMode !=0)
				{

					SKAction action1 = SKAction.MoveToY(this.View.Frame.Height , 0.2);
					SKAction action2 = SKAction.MoveToY(0, 0.2);
					SKAction action3 = SKAction.MoveToY(this.View.Frame.Height/2 +20, 0.2);
					SKAction action4 = SKAction.MoveToY(this.View.Frame.Height/2 - 40, 0.2);

					spriteTop.RunAction(action1);
					spriteBelow.RunAction(action2);
					myLabel.RunAction(action3);
					myLabel2.RunAction(action4);

					/*var locationTop = new CGPoint();
					locationTop.X = (this.View.Frame.Width / 2);
					locationTop.Y = (this.View.Frame.Height);
					var locationBelow = new CGPoint();
					locationBelow.X = (this.View.Frame.Width / 2);
					locationBelow.Y = 0;
					spriteTop.Position = locationTop;
					spriteBelow.Position = locationBelow;
					myLabel.Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) + 20);
					myLabel2.Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) - 40);*/
					gameMode = 0;

				}
				else if (nodeType is FieldNode)
				{
					FieldNode touchedNode = (Teatime.FieldNode)GetNodeAtPoint(locationc);
					//	touchedNode.Color = UIColor.FromHSB((nfloat)0, 0, 1);
					// Define Spark with location and alpha
					if (gameMode == 1 || gameMode == 2)
					{

						var sprite = new SKSpriteNode("spark3")
						{
							Position = locationc,
							XScale = 1.6f,
							YScale = 1.6f,
							Alpha = 0.7f,
							ZPosition = 10         

						};
						//this.AddChild(sprite);
						touchedNode.Color = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
						if (touchedNode == spriteTop)
						{
							hueTop = (nfloat)(checkY / Frame.Height);
							satTop = 0.5f;
							briTop = (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));
						}
						else {
							hueBel= (nfloat)(checkY / Frame.Height);
							satBel= 0.5f;
							briBel = (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));
						}
					}
					if (gameMode == 0)
					{
						mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Text = "next >",
							FontSize = 18,
							Position = new CGPoint(Frame.Width - 50, (Frame.Height -40))
						};
						mySave.FontColor = UIColor.FromHSB((nfloat)0, 0, 3f);
						mySave.Alpha = 0.9f;
						mySave.ZPosition = 2;
						this.AddChild(mySave);
					}
					//UIColor topColor = spriteTop.Color;

					//UIColor belColor = spriteBelow.Color;

					Proto3Dim1 = (int)((hueTop - hueBel) * 30) ;
					Proto3Dim2 = (int)((satTop - satBel) * 30);
					Proto3Dim3 = (int)((briTop - briBel) * 30);

					if (gameMode == 0)
					{
						if (checkYBig > Frame.Height / 2)
						{
							SKAction action1 = SKAction.MoveToY(this.View.Frame.Height * 2, 0.2);
							SKAction action2 = SKAction.MoveToY(this.View.Frame.Height / 2, 0.2);
							SKAction action3 = SKAction.MoveToY(this.View.Frame.Height - 30, 0.2);
							SKAction action4 = SKAction.MoveToY(this.View.Frame.Height + 30, 0.2);

							spriteTop.RunAction(action1);
							spriteBelow.RunAction(action2);
							myLabel2.RunAction(action3);
							myLabel.RunAction(action4);
							gameMode = 1;
						}
						else {
							SKAction action1 = SKAction.MoveToY(this.View.Frame.Height / 2, 0.2);
							SKAction action2 = SKAction.MoveToY(0 - this.View.Frame.Height, 0.2);
							SKAction action3 = SKAction.MoveToY(0 - 30, 0.2);
							SKAction action4 = SKAction.MoveToY(0 + 30, 0.2);

							spriteTop.RunAction(action1);
							spriteBelow.RunAction(action2);
							myLabel2.RunAction(action3);
							myLabel.RunAction(action4);
							gameMode = 2;
						}
					}
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
