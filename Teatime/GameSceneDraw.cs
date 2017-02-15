using System;
using System.Diagnostics.Contracts;
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
	
	public class GameSceneDraw : SKScene
	{
		
		public int Proto5Dim1 { get; set; }
		public int Proto5Dim2 { get; set; }
		public int Proto5Dim3 { get; set; }
		// Class declarations of the sprite nodes
		PointNode oneSprite;
		PointNode secSprite;
		PointNode currentSprite;
		PointNode lastSprite;
		PointNode nextSprite;
		PointNode arrowLeftSprite;
		PointNode arrowRightSprite;
		bool longPressEnabled = false;
		LineNodeDraw baseLine;
		bool startDragInside = false;
		double xLast;
		double xNext;
		SKLabelNode timeLabel;
		bool newNode;
		bool delNode;
		//LineNode yourline;
		SKLabelNode myLabel;
		SKLabelNode myLabel2;
		SKLabelNode myLabel3;

		SKLabelNode myLabelPos;
		SKLabelNode myLabelNeg;
		SKSpriteNode navSprite;

		UITextField inputUserText;


		SKEmitterNode fireEmitter;
		FieldNode backGroundNode;
		int lineCounter = 0;
		int updateCounter = 0;
		bool upperSpeed;
		bool upperSize;
		SKShapeNode yourline;
		SKShapeNode lastLine;
		SKShapeNode currentLine;
		SKShapeNode nextLine;
		PointNode activeNode;
		bool firstTouch;
		PointNode spriteLeft;
		PointNode spriteRight;
		SKLabelNode mySave;
		int gameMode;

		SKSpriteNode categoryTempFilter;
		SKLabelNode categoryText;
		SKLabelNode categoryText1;
		SKLabelNode categoryText2;
		SKLabelNode categoryText3;
		SKLabelNode categoryText4;
		SKLabelNode categoryText5;
		SKLabelNode categoryText6;
		SKLabelNode categoryText7;
		SKLabelNode categoryTextRel;
		SKLabelNode categoryTextDel;
		SKLabelNode categoryTextExit;

		protected GameSceneDraw(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			// Setup Sprite Scene
			Proto5Dim1 = 0;
			Proto5Dim2 = 0;
			Proto5Dim3 = 0;
			// New Label placed in the middle of the Screen
			gameMode = 0;
			if (gameMode == 0)
			{
				mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
				{
					Name = "next",
					Text = "next >",
					FontSize = 18,
					Position = new CGPoint(Frame.Width - 50, (Frame.Height - 48))
				};
				mySave.FontColor = UIColor.FromHSB((nfloat)0, 0, 0f);
				mySave.Alpha = 0.9f;
				mySave.ZPosition = 2;
				this.AddChild(mySave);
			}
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Erfasse deine Schlaf- ",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height/2  + 100)
			};
			myLabel.Alpha = 0.0f;
			myLabel.ZPosition = 1;
			myLabel.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabel);
			myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1),SKAction.WaitForDuration(1),SKAction.FadeOutWithDuration(1)));


			myLabel2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "und Wachphase.",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 70)
			};
			myLabel2.Alpha = 0.0f;
			myLabel2.ZPosition = 1;
			myLabel2.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabel2);
			myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5),SKAction.FadeInWithDuration(1),SKAction.WaitForDuration(1), SKAction.FadeOutWithDuration(1)));

			myLabel3 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Bestätige mit next >",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 40)
			};
			myLabel3.Alpha = 0.0f;
			myLabel3.ZPosition = 1;
			myLabel3.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabel3);
			myLabel3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1), SKAction.FadeOutWithDuration(1)));


			myLabelPos = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "",
				FontSize = 16,
				Position = new CGPoint(Frame.Width / 2, Frame.Height/2 +3)
			};
			myLabelPos.Alpha = 0.3f;
			myLabelPos.ZPosition = 1;
			myLabelPos.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabelPos);
			myLabelNeg = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = " Zeitachse ",
				FontSize = 16,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 15)
			};
			myLabelNeg.Alpha = 0.3f;
			myLabelNeg.ZPosition = 1;
			myLabelNeg.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabelNeg);

			// New Label placed in the middle of the Screen
			timeLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Timelabel",
				FontSize = 16,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 100)
			};
			timeLabel.Alpha = 0.0f;
			timeLabel.ZPosition = 1;
			AddChild(timeLabel);

			var locationTop = new CGPoint();
			locationTop.X = Frame.Width;//(this.View.Frame.Width );
			locationTop.Y = Frame.Height;//(this.View.Frame.Height);

			//this.Size = new CGSize(1200, 200);
			this.BackgroundColor = UIColor.FromHSB(0, 0, 0.9f);

			firstTouch = false;
			// Create Path for the line, between both sprites


			// Sprite Node
			secSprite = new PointNode("sparkfilled");
			secSprite.Position = new CGPoint(0, Frame.Height/2);
			secSprite.ZPosition = 1;
			secSprite.XScale = 0.1f;
			secSprite.YScale = 0.1f;
			secSprite.Alpha = 0.8f;
			secSprite.ColorBlendFactor = 1f;
			secSprite.Color = UIColor.FromHSB(0, 0, 0);


			// Sprite Node
			oneSprite = new PointNode("sparkfilled");
			oneSprite.Position = new CGPoint(Frame.Width, Frame.Height/2);
			oneSprite.ZPosition = 1;
			oneSprite.XScale = 0.1f;
			oneSprite.YScale = 0.1f;
			oneSprite.ColorBlendFactor = 1f;
			oneSprite.Alpha = 0.8f;
			oneSprite.Color = UIColor.FromHSB(0, 0, 0);
			firstTouch = false;
			// Create Path for the line, between both sprites
			var path = new CGPath();
		   path.AddLines(new CGPoint[]{
				new CGPoint (oneSprite.Position.X, oneSprite.Position.Y),
				new CGPoint (secSprite.Position.X, secSprite.Position.Y)
			
					});
			path.CloseSubpath();

			var pathBase = new CGPath();
			pathBase.AddLines(new CGPoint[]{
				new CGPoint (oneSprite.Position.X, oneSprite.Position.Y),
				new CGPoint (secSprite.Position.X, secSprite.Position.Y)

					});
			pathBase.CloseSubpath();

			// Generate Line according to Path
			yourline = new SKShapeNode();
			yourline.Path = path;
			yourline.StrokeColor= UIColor.FromHSB(0, 0, 0);
			yourline.Alpha = 0.0f;
			//yourline.ZPosition = 2;
			baseLine = new LineNodeDraw();
			baseLine.Name = "baseline";
			UIBezierPath tempbezier = new UIBezierPath();
			tempbezier.MoveTo(new CGPoint(0,Frame.Height/2));
			tempbezier.AddQuadCurveToPoint(
				new CGPoint(Frame.Width/2, Frame.Height/2),
				new CGPoint(100,100)
				);
			tempbezier.MoveTo(new CGPoint(Frame.Width / 2, Frame.Height / 2));
			tempbezier.AddQuadCurveToPoint(
				new CGPoint(Frame.Width, Frame.Height / 2),
				new CGPoint(200, 100)
				);
			//tempbezier.a
//			tempbezier.ClosePath();
		//	baseLine.Path = tempbezier.CGPath;
			baseLine.Path = pathBase;
			baseLine.StrokeColor = UIColor.FromHSB(0, 0, 0);
			baseLine.Alpha = 0.1f;
			baseLine.ZPosition = 3;
			AddChild(baseLine);
			AddChild(yourline);
			AddChild(secSprite);
			AddChild(oneSprite);

			lastSprite = secSprite;
		//	xLast = secSprite.Position.X;
		//	xNext = oneSprite.Position.X;
			nextSprite = oneSprite;

			delNode = false;
			var gestureLongRecognizer = new UILongPressGestureRecognizer(PressHandler);
			gestureLongRecognizer.MinimumPressDuration = 0.5;
			this.View.AddGestureRecognizer(gestureLongRecognizer);

// Night Nodes
			var locationLeft = new CGPoint();
			locationLeft.X = (0 - this.View.Frame.Width / 4 -20);
			locationLeft.Y = (this.View.Frame.Height / 2);
			var locationRight = new CGPoint();
			locationRight.X = (this.View.Frame.Width + this.View.Frame.Width / 4 +20);
			locationRight.Y = (this.View.Frame.Height / 2);
			spriteLeft = new PointNode()
			{
				Position = locationLeft,
				Name = "Left",
				XScale = 1,
				YScale = 1,
				Alpha = 0.8f,
				Color = UIColor.FromHSB((nfloat)0, 0, 0.2f)

			};

			spriteRight = new PointNode()
			{
				Position = locationRight,
				Name = "Right",
				XScale = 1,
				YScale = 1,
				Alpha = 0.8f,
				Color = UIColor.FromHSB((nfloat)0, 0, 0.2f)

			};
			// Position in comparsion to other SpriteNodes
			spriteLeft.ZPosition = -1;
			spriteRight.ZPosition = -1;


			// Sprite Node
			arrowLeftSprite = new PointNode("arrow");
			arrowLeftSprite.Name = "arrowLeft";
			arrowLeftSprite.Position = new CGPoint(Frame.Width / 2 - 135, Frame.Height / 2);
			arrowLeftSprite.ZPosition = 1;
			arrowLeftSprite.XScale = 1f;
			arrowLeftSprite.YScale = 1f;
			arrowLeftSprite.ZRotation = ((System.nfloat)(Math.PI / 2));
			arrowLeftSprite.Alpha = 0.8f;
			//arrowLeftSprite.ColorBlendFactor = 1f;
			//arrowLeftSprite.Color = UIColor.FromHSB(0, 0, 0);
			// Sprite Node
			arrowRightSprite = new PointNode("arrow");
			arrowRightSprite.Name = "arrowRight";
			arrowRightSprite.Position = new CGPoint(Frame.Width/2 +135, Frame.Height / 2);
			arrowRightSprite.ZPosition = 1;
			arrowRightSprite.ZRotation =  -((System.nfloat)(Math.PI / 2));
			arrowRightSprite.XScale =1f;
			arrowRightSprite.YScale =1f;
			arrowRightSprite.Alpha = 0.8f;
			//arrowLeftSprite.ColorBlendFactor = 1f;
			//arrowLeftSprite.Color = UIColor.FromHSB(0, 0, 0);


			spriteLeft.Size = new CGSize(Frame.Width , Frame.Height );
			spriteRight.Size = new CGSize(Frame.Width , Frame.Height );
			this.AddChild(spriteLeft);
			this.AddChild(spriteRight);

			navSprite = new SKSpriteNode();
			navSprite.Name = "navSprite";
			navSprite.Alpha = 0.0000001f;

			navSprite.ZPosition = 10;
			navSprite.Color = UIColor.FromHSB((nfloat)0, 1, 0.0f);
			navSprite.Size = new CGSize(140, 70);
			//navSprite.AnchorPoint = new CGPoint(1, 1);
			navSprite.Position = new CGPoint((this.View.Frame.Width-(70)), (this.View.Frame.Height-(35) ));
			AddChild(navSprite);
			AddChild(arrowLeftSprite);
			AddChild(arrowRightSprite);

		}
		public void saveProto5Input()
		{
			//throw new NotImplementedException();
			TeatimeItem item;


			foreach (var spriteNode in Children.OfType<PointNode>())
			{

				item = new TeatimeItem();
				if (DatabaseMgmt.inputName != null)
				{
					item.Username = DatabaseMgmt.inputName;

				}
				else {
					item.Username = "Anonym";
				}

				item.dateInserted = DateTime.Now.ToLocalTime();

				item.PrototypeNr = 5;
				item.Comment = "N/A";
				item.Dim1 = Proto5Dim1;
				item.Dim2 = Proto5Dim2;
				item.Dim3 = Proto5Dim3;

				if (spriteNode.Name == "Left")
				{
					nfloat offsetX = spriteLeft.Position.X - (0 - Frame.Width / 2);

					double selectedHour = (24 / Frame.Width * offsetX);
					//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
					//	double selectedMin = Math.Round(fullHourRest/100*60,0);
					String restMin = "";
					String stringHour = selectedHour.ToString();
					var splitHour = stringHour.Split('.');
					if (splitHour[0].Length == 1)
					{
						splitHour[0] = "0" + splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}

					else {
						splitHour[0] = splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}
					double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
					String selectedMinString = selectedMin.ToString();
					if (selectedMinString.Length == 1)
					{
						selectedMinString = "0" + selectedMinString;
					}
					// Time calculated
					item.Comment = splitHour[0] + ":" + selectedMinString;
					item.Dim2 = 1;

				}
				else if (spriteNode.Name == "Right")
				{
					nfloat offsetX = spriteRight.Position.X - (Frame.Width - Frame.Width / 2);
					double selectedHour = (24 / Frame.Width * offsetX);
					//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
					//	double selectedMin = Math.Round(fullHourRest/100*60,0);
					String restMin = "";
					String stringHour = selectedHour.ToString();
					var splitHour = stringHour.Split('.');
					if (splitHour[0].Length == 1)
					{
						splitHour[0] = "0" + splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}
					else {
						splitHour[0] = splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}
					double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
					String selectedMinString = selectedMin.ToString();
					if (selectedMinString.Length == 1)
					{
						selectedMinString = "0" + selectedMinString;
					}
					// Time calculated
					item.Comment = splitHour[0] + ":" + selectedMinString;
					item.Dim3 = 1;

				}
				else if (spriteNode.Name == "navSprite" || spriteNode.Name == "arrowLeft" ||spriteNode.Name=="arrowRight")
				{ 
				
				}
				else {
					double selectedHour = (24 / Frame.Width * spriteNode.Position.X);
					//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
					//	double selectedMin = Math.Round(fullHourRest/100*60,0);
					String restMin = "";
					String stringHour = selectedHour.ToString();
					var splitHour = stringHour.Split('.');
					if (splitHour[0].Length == 1)
					{
						splitHour[0] = "0" + splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}
					else {
						splitHour[0] = splitHour[0];
						if (splitHour.Length > 1)
						{

							if (splitHour[1].Length >= 2)
							{
								restMin = splitHour[1].Substring(0, 2);
							}
							else if (splitHour[1].Length == 1)
							{
								restMin = splitHour[1].Substring(0, 1) + "0";
							}
						}
						else {
							restMin = "00";
						}
					}
					double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
					String selectedMinString = selectedMin.ToString();
					if (selectedMinString.Length == 1)
					{
						selectedMinString = "0" + selectedMinString;
					}
					String catText="";
					if (spriteNode is PointNode)
					{
						catText = ((PointNode)spriteNode).Category;
						if (catText == "")
						{
							catText = "No Categorie";
						}
					}


					// Time calculated
					item.Comment = splitHour[0] + ":" + selectedMinString + ";" +catText;
					item.Dim1 = (int) (Frame.Height / 2 - spriteNode.Position.Y);
				}
			

			    if (spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight")
				{
					DatabaseMgmt.Database.SaveItem(item);
				}

			}
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
			if (gameMode == 1)
			{
				
				var image = gestureRecognizer.View;
				if (gestureRecognizer.State == UIGestureRecognizerState.Began || gestureRecognizer.State ==  UIGestureRecognizerState.Recognized
					|| gestureRecognizer.State == UIGestureRecognizerState.Changed)
				{

					var location = gestureRecognizer.LocationInView(this.View);
					var convertLocation = new CGPoint(location.X, this.View.Frame.Height - location.Y);         //var location = gestureRecognizer.LocationOfTouch(0, image);
																												//	var location = ((UITouch)touch).LocationInNode(this);
					if (currentSprite != null && currentSprite.Frame.Contains(convertLocation) && currentSprite.Name != "Left" && currentSprite.Name != "Right" && currentSprite.Name != "navSprite" && currentSprite.Name != "arrowLeft" && currentSprite.Name != "arrowRight")

					{
						gameMode = 2;

						checkSprites();                                                                                         // Sprite Node
						categoryTempFilter = new SKSpriteNode();
						categoryTempFilter.Size = new CGSize(Frame.Width, Frame.Height);
						categoryTempFilter.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
						categoryTempFilter.ZPosition = 100;
						categoryTempFilter.XScale = 1f;
						categoryTempFilter.YScale = 1f;
						categoryTempFilter.ColorBlendFactor = 1f;
						categoryTempFilter.Alpha = 0.8f;
						categoryTempFilter.Color = UIColor.FromHSB(0, 0, 0);
						AddChild(categoryTempFilter);
						// Time label end



						categoryText = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Text = "Wähle eine Kategorie / Aktion",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 200)
						};
						categoryText.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText.Alpha = 0.9f;
						categoryText.ZPosition = 101;
						categoryText.FontColor = UIColor.FromHSB(0, 0, 1f);
						AddChild(categoryText);

						categoryText2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat1",
							Text = "> Kollegium",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 160)
						};
						categoryText2.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText2.Alpha = 0.9f;
						categoryText2.ZPosition = 101;
						categoryText2.FontColor = UIColor.FromHSB(0.3f, 0.5f, 1f);
						AddChild(categoryText2);

						categoryText3 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat2",
							Text = "> Einzelne Schüler",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 130)
						};
						categoryText3.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText3.Alpha = 0.9f;
						categoryText3.ZPosition = 101;
						categoryText3.FontColor = UIColor.FromHSB(0.4f, 0.5f, 1f);
						AddChild(categoryText3);

						categoryText4 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat3",
							Text = "> Schülergruppen",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 100)
						};
						categoryText4.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText4.Alpha = 0.9f;
						categoryText4.ZPosition = 101;
						categoryText4.FontColor = UIColor.FromHSB(0.5f, 0.5f, 1f);
						AddChild(categoryText4);

						categoryText5 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat4",
							Text = "> Eltern",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 70)
						};
						categoryText5.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText5.Alpha = 0.9f;
						categoryText5.ZPosition = 101;
						categoryText5.FontColor = UIColor.FromHSB(0.6f, 0.5f, 1f);
						AddChild(categoryText5);

						categoryText6 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat5",
							Text = "> Schlaf",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 40)
						};
						categoryText6.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText6.Alpha = 0.9f;
						categoryText6.ZPosition = 101;
						categoryText6.FontColor = UIColor.FromHSB(0.7f, 0.5f, 1f);
						AddChild(categoryText6);

						categoryText7 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "Cat6",
							Text = "> Privater Grund",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 10)
						};
						categoryText7.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryText7.Alpha = 0.9f;
						categoryText7.ZPosition = 101;
						categoryText7.FontColor = UIColor.FromHSB(0.8f, 0.5f, 1f);
						AddChild(categoryText7);

						categoryTextRel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "CatRelLabel",
							Text = "> Kategorie zurücksetzen?",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 - 110)
						};
						categoryTextRel.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryTextRel.Alpha = 0.9f;
						categoryTextRel.ZPosition = 101;
						categoryTextRel.FontColor = UIColor.FromHSB(0, 0, 1f);
						AddChild(categoryTextRel);

						categoryTextDel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "DeleteLabel",
							Text = "> Zeitpunkt löschen?",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 - 140)
						};
						categoryTextDel.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryTextDel.Alpha = 0.9f;
						categoryTextDel.ZPosition = 101;
						categoryTextDel.FontColor = UIColor.FromHSB(0, 0, 1f);
						AddChild(categoryTextDel);


						categoryTextExit = new SKLabelNode("AppleSDGothicNeo-UltraLight")
						{
							Name = "ExitLabel",
							Text = "> Menu verlassen",
							FontSize = 18,
							Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 - 170)
						};
						categoryTextExit.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
						categoryTextExit.Alpha = 0.9f;
						categoryTextExit.ZPosition = 101;
						categoryTextExit.FontColor = UIColor.FromHSB(0, 0, 1f);
						AddChild(categoryTextExit);
						//	myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1), SKAction.FadeOutWithDuration(1)));

						inputUserText = new UITextField();
						inputUserText.Frame = new CGRect(View.Bounds.Width / 2 -127 , View.Bounds.Height / 2+3, 225, 28);
						inputUserText.BackgroundColor = UIColor.FromHSB(0, 0, 0.2f);
						inputUserText.TextColor = UIColor.FromHSB(0.9f, 0.5f, 1.0f);
					

						View.AddSubview(inputUserText);
						inputUserText.ClearButtonMode = UITextFieldViewMode.WhileEditing; 

						inputUserText.ShouldReturn += (textField) =>
						{
							inputUserText.ResignFirstResponder();
							//DatabaseMgmt.inputName = this.userName.Text;
							setPointCategory(inputUserText.Text, inputUserText.TextColor);
							return true;
						};
						inputUserText.ShouldChangeCharacters = (textField, range, replacementString) =>
						{
							var newLength = textField.Text.Length + replacementString.Length - range.Length;
							return newLength <= 20;
						};
						inputUserText.BorderStyle = UITextBorderStyle.RoundedRect;
						inputUserText.Font = UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f);
						if (currentSprite.Category == "" || currentSprite.Category == "Kollegium" || currentSprite.Category == "Einzelne Schüler" || currentSprite.Category == "Schülergruppen" || currentSprite.Category == "Eltern" || currentSprite.Category == "Schlaf" || currentSprite.Category == "Privater Grund")
						{
							inputUserText.AttributedPlaceholder = new NSAttributedString(
							"> Eigene Kategorie",
							font: UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f),
							foregroundColor: UIColor.FromHSB(0.9f, 0.5f, 1.0f)

							);
						}
						else  {
							inputUserText.AttributedPlaceholder = new NSAttributedString(
								"> " + currentSprite.Category,
							font: UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f),
							foregroundColor: UIColor.FromHSB(0.9f, 0.5f, 1.0f)

							);
							inputUserText.Text = currentSprite.Category;
						}




					}


					//if (currentSprite.Frame.Contains(location))
					if (currentSprite != null && currentSprite.Frame.Contains(convertLocation) && currentSprite.Name != "Left" && currentSprite.Name != "Right"&& currentSprite.Name != "navSprite"&&  currentSprite.Name != "arrowLeft" && currentSprite.Name != "arrowRight")

					{

					
						//longPressEnabled = true;
						//gesLabel.Text = "LongPressed: " + gestureRecognizer.LocationOfTouch(0, image).ToString();


						timeLabel.Text = "";

						//	SKAction actMove = SKAction.MoveToX(currentSprite.Position.X - 2, 0.1);
						//	SKAction actMoveBack = SKAction.MoveToX(currentSprite.Position.X + 2, 0.1); 
						SKAction actMove = SKAction.ScaleTo(1f, 1f, 0.2);
						SKAction actMoveBack = SKAction.ScaleTo(0.4f, 0.4f, 0.2);
						SKAction seq = SKAction.Sequence(actMove, actMoveBack);
						SKAction seqTexture = SKAction.SetTexture(SKTexture.FromImageNamed(("sparkx")));
						SKAction seqTextureNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("sparkfilled")));
						SKAction seqAll = SKAction.Sequence(seqTexture, seq, seqTextureNormal);
						//currentSprite.Texture = SKTexture.FromImageNamed(("sparkx"));
						currentSprite.RunAction(SKAction.RepeatAction(seq,2));

				}

				}
			}
		}


		void checkSprites()
		{
			double diffXLast;
			double diffXNext;
			bool firstRun = true;

			foreach (var spriteNode in Children.OfType<PointNode>())
			{

				if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name!="navSprite"&& spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight"){
					diffXLast = currentSprite.Position.X - spriteNode.Position.X;
					diffXNext = spriteNode.Position.X - currentSprite.Position.X;
					if (firstRun == true)
					{
						xLast = currentSprite.Position.X - secSprite.Position.X;
						xNext = oneSprite.Position.X - currentSprite.Position.X;
						firstRun = false;
					}
					if (diffXLast > 0 && xLast >= diffXLast)
					{
						lastSprite = spriteNode;
						xLast = diffXLast;
					}
					if (diffXNext > 0 && xNext >= diffXNext)
					{
						nextSprite = spriteNode;
						xNext = diffXNext;
					}

				}
			}

		}
		public void updateLines(bool upperSpeedTemp,bool upperSizeTemp)
		{

					
		}
	
	
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			
			base.TouchesMoved(touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;
			var location = ((UITouch)touch).LocationInNode(this);
			if (touch != null)
			{
				//	nfloat offsetX = (nfloat)(touch.LocationInNode(this).X);
				//	nfloat offsetY = (nfloat)(touch.LocationInNode(this).Y);
				nfloat offsetX = (nfloat)(touch.LocationInNode(this).X);
				nfloat offsetY = (nfloat)(touch.LocationInNode(this).Y);


				if (gameMode == 0)
				{
			
					if (offsetX < Frame.Width / 2 && (offsetY < Frame.Height-70 ))
					{
						
						spriteLeft.Position = new CGPoint(0-Frame.Width/2  + offsetX, Frame.Height / 2);
						//= new CGPoint(currentSprite.Position.X, location.Y);
						arrowLeftSprite.Position= new CGPoint( offsetX-20, Frame.Height / 2);
						timeLabel.Position = new CGPoint(offsetX + 30, Frame.Height / 2 + 15);
						double selectedHour = (24 / Frame.Width * offsetX);
						//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
						//	double selectedMin = Math.Round(fullHourRest/100*60,0);
						String restMin = "";
						String stringHour = selectedHour.ToString();
						var splitHour = stringHour.Split('.');
						if (splitHour[0].Length == 1)
						{
							splitHour[0] = "0" + splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						else {
							splitHour[0] = splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
						String selectedMinString = selectedMin.ToString();
						if (selectedMinString.Length == 1)
						{
							selectedMinString = "0" + selectedMinString;
						}
						// Time calculated
						timeLabel.Text = splitHour[0] + ":" + selectedMinString;
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
						// Time label end
					}

					if (offsetX > Frame.Width / 2 && offsetY < Frame.Height - 70  )
					{

						spriteRight.Position = new CGPoint(Frame.Width - Frame.Width / 2 + offsetX, Frame.Height / 2);
						//= new CGPoint(currentSprite.Position.X, location.Y);
						arrowRightSprite.Position = new CGPoint(offsetX + 20, Frame.Height / 2);

						timeLabel.Position = new CGPoint(offsetX - 30, Frame.Height / 2 + 15);
						double selectedHour = (24 / Frame.Width * offsetX);
						//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
						//	double selectedMin = Math.Round(fullHourRest/100*60,0);
						String restMin = "";
						String stringHour = selectedHour.ToString();
						var splitHour = stringHour.Split('.');
						if (splitHour[0].Length == 1)
						{
							splitHour[0] = "0" + splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						else {
							splitHour[0] = splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
						String selectedMinString = selectedMin.ToString();
						if (selectedMinString.Length == 1)
						{
							selectedMinString = "0" + selectedMinString;
						}
						// Time calculated
						timeLabel.Text = splitHour[0] + ":" + selectedMinString;
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
						// Time label end
					}


				}

				// Time label end
				if (gameMode == 1 && currentSprite!=null)
				{

					if (currentSprite.Frame.Contains(((UITouch)touch).LocationInNode(this)) && delNode == false && currentSprite.Name != "Left" && currentSprite.Name != "Right" )
					{


						// Time label Start
						timeLabel.Position = new CGPoint(currentSprite.Position.X, currentSprite.Position.Y + 50);
						double selectedHour = (24 / Frame.Width * currentSprite.Position.X);
						//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
						//	double selectedMin = Math.Round(fullHourRest/100*60,0);
						String restMin = "";
						String stringHour = selectedHour.ToString();
						var splitHour = stringHour.Split('.');
						if (splitHour[0].Length == 1)
						{
							splitHour[0] = "0" + splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						else {
							splitHour[0] = splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
						String selectedMinString = selectedMin.ToString();
						if (selectedMinString.Length == 1)
						{
							selectedMinString = "0" + selectedMinString;
						}
						// Time calculated
						timeLabel.Text = splitHour[0] + ":" + selectedMinString;
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
						// Time label end

					//	nfloat offsetX = (float)(touch.LocationInView(View).X);
					//	nfloat offsetY = (float)(touch.LocationInView(View).Y);
						//oneSprite.ScaleTo(new CGSize(oneSprite.Size.Width + (offsetX / 1000), oneSprite.Size.Height + (offsetY / 1000)));


						if (newNode == true)
						{
							var pathCheck = new CGPath();
							pathCheck.AddLines(new CGPoint[]{
						new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
						new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),

					});


							foreach (var lineNode in Children.OfType<SKShapeNode>())
							{
								if (lineNode.Path.PathBoundingBox == pathCheck.PathBoundingBox && lineNode.Name != "baseline")
								{
									lineNode.Alpha = 0f;
									lineNode.RemoveFromParent();

								}
								/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
								{
									lineNode.Alpha = 0f;
								}*/
							}
						}
						if (offsetX < nextSprite.Position.X - 8 && offsetX - 8 > lastSprite.Position.X)
						{

							currentSprite.Position = location;
							//= new CGPoint(currentSprite.Position.X, location.Y);
						} 
						else if (offsetX > nextSprite.Position.X - 8)
						{
						currentSprite.Position = new CGPoint(currentSprite.Position.X , offsetY);
						}
						else if (offsetX  < lastSprite.Position.X + 8)
						{
							currentSprite.Position = new CGPoint(currentSprite.Position.X,offsetY);
						}
						//myLabel.Text = offsetX + " " + offsetY;
						checkSprites();

						var path = new CGPath();
						path.AddLines(new CGPoint[]{
						new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
						new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
						});
						path.CloseSubpath();

						currentLine.Path = path;



						var pathNext = new CGPath();
						pathNext.AddLines(new CGPoint[]{
						new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
						new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),
						});
						pathNext.CloseSubpath();
							
						nextLine.Path = pathNext;
						SKAction actMove = SKAction.ScaleTo(0.8f, 0.8f, 0.2);
						//SKAction actMoveBack = SKAction.ScaleTo(0.4f, 0.4f, 0.2);
						//SKAction seq = SKAction.Sequence(actMove, actMoveBack);
						//SKAction seqTexture = SKAction.SetTexture(SKTexture.FromImageNamed(("sparkx")));
						//SKAction seqTextureNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("sparkfilled")));
						//SKAction seqAll = SKAction.Sequence(seqTexture, seq, seqTextureNormal);
						//currentSprite.Texture = SKTexture.FromImageNamed(("sparkx"));
						currentSprite.RunAction(SKAction.RepeatAction(actMove, 1));

				}

				}
			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//lastSprite = currentSprite;
				//lastLine = currentLine;
				var location = ((UITouch)touch).LocationInNode(this);
				var nodeType = GetNodeAtPoint(location);
				var spriteNode = nodeType;
				SKAction actMoveBack = SKAction.ScaleTo(0.4f, 0.4f, 0.2);
				if (gameMode == 1 && (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight"))
				{
					currentSprite.RunAction(SKAction.RepeatAction(actMoveBack, 1));
				}
			}

			startDragInside = false;
			delNode = false;
		}

		public void setPointCategory(String categoryCustomText, UIColor categoryColor) { 
			newNode = false;
			currentSprite.Category = categoryCustomText;
			timeLabel.Text = categoryCustomText;
			currentSprite.Color = categoryColor;
			categoryTempFilter.RemoveFromParent();
			categoryText.RemoveFromParent();
			categoryText2.RemoveFromParent();
			categoryText3.RemoveFromParent();
			categoryText4.RemoveFromParent();
			categoryText5.RemoveFromParent();
			categoryText6.RemoveFromParent();
			categoryText7.RemoveFromParent();
			categoryTextRel.RemoveFromParent();
			categoryTextDel.RemoveFromParent();
			categoryTextExit.RemoveFromParent();
			inputUserText.RemoveFromSuperview();

			gameMode = 1;
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{


			newNode = true;

			// Called when a touch begins
			foreach (var touch in touches)
			{
				UITouch touchc = touches.AnyObject as UITouch;
				//SKNodeTouches_UITouch touch = touches.AnyObject as SKNodeTouches_UITouch;
				var locationc = ((UITouch)touchc).LocationInNode(this);

				// Check click
				nfloat checkX = ((UITouch)touchc).LocationInNode(this).X;
				nfloat checkY = ((UITouch)touchc).LocationInNode(this).Y;

				nfloat offsetY = (nfloat)(touchc.LocationInNode(this).Y);

				var nodeType = GetNodeAtPoint(locationc);
				if (gameMode == 2 && (nodeType is PointNode || nodeType.Name == "ExitLabel"))
				{
					newNode = false;
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();
					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "DeleteLabel")
				{
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					checkSprites();
					delNode = true;
					//REMOVAL CODE
					currentSprite.RemoveFromParent();
					//spriteNode.Alpha = 0f;
					var pathCheckRem = new CGPath();
					pathCheckRem.AddLines(new CGPoint[]{
									new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),

									});
					var pathCheckRemNext = new CGPath();
					pathCheckRemNext.AddLines(new CGPoint[]{
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
									new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),

									});
					timeLabel.Text = "Zeitpunkt gelöscht";
					foreach (var lineNode in Children.OfType<SKShapeNode>())
					{
						if (lineNode.Path.PathBoundingBox == pathCheckRem.PathBoundingBox && lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0f;
							lineNode.RemoveFromParent();
						}
						if (lineNode.Path.PathBoundingBox == pathCheckRemNext.PathBoundingBox && lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0f;
							lineNode.RemoveFromParent();

						}
						/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
						{
							lineNode.Alpha = 0f;
						}*/
					}
					var pathReset = new CGPath();
					pathReset.AddLines(new CGPoint[]{
									new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),
									});
					SKShapeNode newlineNext = new SKShapeNode();
					newlineNext.Path = pathReset;
					newlineNext.StrokeColor = UIColor.FromHSB(0, 0, 0);
					newlineNext.LineWidth = 2f;
					newlineNext.Alpha = 0.5f;
					AddChild(newlineNext);
					nextLine = newlineNext;

					newNode = false;


					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat1")
				{
					newNode = false;
					timeLabel.Text = "Kollegium";
					currentSprite.Category = "Kollegium";
					currentSprite.Color = UIColor.FromHSB(0.3f, 0.5f, 0.8f);
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat2")
				{
					newNode = false;
					timeLabel.Text = "Einzelne Schüler";
					currentSprite.Category = "Einzelne Schüler";
					categoryTempFilter.RemoveFromParent();
					currentSprite.Color = UIColor.FromHSB(0.4f, 0.5f, 0.8f);
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();


					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat3")
				{
					newNode = false;
					timeLabel.Text = "Schülergruppen";
					currentSprite.Category = "Schülergruppen";
					categoryTempFilter.RemoveFromParent();
					currentSprite.Color = UIColor.FromHSB(0.5f, 0.5f, 0.8f);
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat4")
				{
					newNode = false;
					timeLabel.Text = "Eltern";
					currentSprite.Category = "Eltern";
					currentSprite.Color = UIColor.FromHSB(0.6f, 0.5f, 0.8f);
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat5")
				{
					newNode = false;
					timeLabel.Text = "Schlaf";
					currentSprite.Category = "Schlaf";
					currentSprite.Color = UIColor.FromHSB(0.7f, 0.5f, 0.8f);
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();


					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "Cat6")
				{
					newNode = false;
					timeLabel.Text = "Privater Grund";
					currentSprite.Category = "Privater Grund";
					currentSprite.Color = UIColor.FromHSB(0.8f, 0.5f, 0.8f);
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					gameMode = 1;
				}
				else if (gameMode == 2 && nodeType.Name == "CatRelLabel")
				{
					newNode = false;
					currentSprite.Category = "";
					timeLabel.Text = "Keine Kategorie";
					currentSprite.Color = UIColor.FromHSB(0.0f, 0.0f, 0.2f);
					categoryTempFilter.RemoveFromParent();
					categoryText.RemoveFromParent();
					categoryText2.RemoveFromParent();
					categoryText3.RemoveFromParent();
					categoryText4.RemoveFromParent();
					categoryText5.RemoveFromParent();
					categoryText6.RemoveFromParent();
					categoryText7.RemoveFromParent();
					categoryTextRel.RemoveFromParent();
					categoryTextDel.RemoveFromParent();
					categoryTextExit.RemoveFromParent();
					inputUserText.RemoveFromSuperview();

					gameMode = 1;
				}
				if (((nodeType is SKLabelNode && nodeType.Name == "next" )|| nodeType.Name=="navSprite") && gameMode == 0 )
				{
					myLabel.RemoveAllActions();
					myLabel2.RemoveAllActions();
					myLabel3.RemoveAllActions();

					myLabel.Alpha = 0f;
					myLabel2.Alpha = 0f;
					myLabel3.Alpha = 0f;

					gameMode = 1;
					myLabel.Text = "Bewerte nun deine Stimmung rückblickend";
					myLabel2.Text = "Drücke lange auf einen Punkt um diesem";
					myLabel3.Text = "eine Kategorie zuzuordnen";

					spriteLeft.Alpha = 0.8f;
					spriteLeft.Color = UIColor.FromHSB((nfloat)0, 0, 0.8f);
					spriteRight.Alpha = 0.8f;
					spriteRight.Color = UIColor.FromHSB((nfloat)0, 0, 0.8f);
					timeLabel.Text = "";
					myLabelPos.Text = "+";
					myLabelNeg.Text = "_";
					myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5),SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1),SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						if (spriteNode.Name == "arrowLeft" || spriteNode.Name == "arrowRight")
						{
							spriteNode.Alpha = 0.0f;
						}
						else if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite")
						{
							spriteNode.Alpha = 1f;
							//spriteNode.Color = UIColor.FromHSB(0, 0, 0.2f);		
						}
					}
					foreach (var lineNode in Children.OfType<SKShapeNode>())
					{
						if (lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0.5f;
						//	lineNode.StrokeColor = UIColor.FromHSB(0, 0, 0f);
							//lineNode.RemoveFromParent();
						}
					}
	
				}
				else if (((nodeType is SKLabelNode && nodeType.Name == "next") || nodeType.Name == "navSprite") && gameMode == 1)
				{
					myLabel.RemoveAllActions();
					myLabel2.RemoveAllActions();
					myLabel3.RemoveAllActions();

					myLabel.Alpha = 0f;
					myLabel2.Alpha = 0f;
					myLabel3.Alpha = 0f;

					spriteLeft.ZPosition = -2;
					spriteRight.ZPosition = -2;
					gameMode = 0;
					myLabel.Text = "Erfasse deine Schlaf-";
					myLabel2.Text = "und Wachphase";
					myLabel3.Text = "Bestätige mit next >";
					spriteLeft.Alpha = 0.8f;
					spriteLeft.Color = UIColor.FromHSB((nfloat)0, 0, 0.2f);
					spriteRight.Alpha = 0.8f;
					spriteRight.Color = UIColor.FromHSB((nfloat)0, 0, 0.2f);

					timeLabel.Text = "";
					myLabelPos.Text = "";
					myLabelNeg.Text = " Zeitachse ";
					myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5),SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1),SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					foreach (var spriteNode in Children.OfType<PointNode>())
					{

						if (spriteNode.Name == "arrowLeft" || spriteNode.Name == "arrowRight")
						{
							spriteNode.Alpha = 0.8f;
						}
						else if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite")
						{
							spriteNode.Alpha = 0.2f;
							//spriteNode.Color = UIColor.FromHSB(0, 0, 0.6f);

						}
					}
					foreach (var lineNode in Children.OfType<SKShapeNode>())
					{
						if (lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0.02f;
							//lineNode.RemoveFromParent();
							//lineNode.StrokeColor = UIColor.FromHSB(0, 0, 0.4f);

						}
					}

				}
				else if (gameMode == 1)
				{

					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						/*	if (spriteNode.HasActions)
							{
								newNode = false;
								delNode = true;
							}*/
						if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite" && spriteNode.Name!="arrowLeft" && spriteNode.Name!="arrowRight")
						{

							if (spriteNode.Frame.Contains(((UITouch)touch).LocationInNode(this)) || ((spriteNode.Position.X==checkX) && spriteNode.Position.X>0))//|| ((checkX-spriteNode.Position.X)<1&& spriteNode.Position.X > 0))
							{
								/*if (spriteNode.HasActions == true)
								{
									currentSprite = spriteNode;
									checkSprites();
									delNode = true;
									//REMOVAL CODE
									spriteNode.RemoveFromParent();
									spriteNode.Alpha = 0f;
									var pathCheckRem = new CGPath();
									pathCheckRem.AddLines(new CGPoint[]{
									new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),

									});
									var pathCheckRemNext = new CGPath();
									pathCheckRemNext.AddLines(new CGPoint[]{
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
									new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),

									});
									timeLabel.Text = "Zeitpunkt gelöscht";
									foreach (var lineNode in Children.OfType<SKShapeNode>())
									{
										if (lineNode.Path.PathBoundingBox == pathCheckRem.PathBoundingBox && lineNode.Name != "baseline")
										{
											lineNode.Alpha = 0f;
											lineNode.RemoveFromParent();
										}
										if (lineNode.Path.PathBoundingBox == pathCheckRemNext.PathBoundingBox && lineNode.Name != "baseline")
										{
											lineNode.Alpha = 0f;
											lineNode.RemoveFromParent();

										}
										/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
										{
											lineNode.Alpha = 0f;
										}*/
									/*}
									var pathReset = new CGPath();
									pathReset.AddLines(new CGPoint[]{
									new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),
									});
									SKShapeNode newlineNext = new SKShapeNode();
									newlineNext.Path = pathReset;
									newlineNext.StrokeColor = UIColor.FromHSB(0, 0, 0);
									newlineNext.LineWidth = 2f;
									newlineNext.Alpha = 0.5f;
									AddChild(newlineNext);
									nextLine = newlineNext;

									newNode = false;

								}*/

								// Time label Start
								timeLabel.Position = new CGPoint(spriteNode.Position.X, spriteNode.Position.Y + 50);
								double selectedHour = (24 / Frame.Width * spriteNode.Position.X);
								//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
								//	double selectedMin = Math.Round(fullHourRest/100*60,0);
								String restMin = "";
								String stringHour = selectedHour.ToString();
								var splitHour = stringHour.Split('.');
								if (splitHour[0].Length == 1)
								{
									splitHour[0] = "0" + splitHour[0];
									if (splitHour.Length > 1)
									{

										if (splitHour[1].Length >= 2)
										{
											restMin = splitHour[1].Substring(0, 2);
										}
										else if (splitHour[1].Length == 1)
										{
											restMin = splitHour[1].Substring(0, 1) + "0";
										}
									}
									else {
										restMin = "00";
									}
								}
								else {
									splitHour[0] = splitHour[0];
									if (splitHour.Length > 1)
									{

										if (splitHour[1].Length >= 2)
										{
											restMin = splitHour[1].Substring(0, 2);
										}
										else if (splitHour[1].Length == 1)
										{
											restMin = splitHour[1].Substring(0, 1) + "0";
										}
									}
									else {
										restMin = "00";
									}

								}
								double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
								String selectedMinString = selectedMin.ToString();
								if (selectedMinString.Length == 1)
								{
									selectedMinString = "0" + selectedMinString;
								}
								// Time calculated
								if (delNode == false)
								{
									timeLabel.Text = splitHour[0] + ":" + selectedMinString;
								}
								timeLabel.ZPosition = 3;
								timeLabel.FontColor = UIColor.Black;
								timeLabel.Alpha = 0.8f;
								// Time label end


								newNode = false;
								activeNode = spriteNode;
								currentSprite = spriteNode;
								checkSprites();

								var pathCheck = new CGPath();
								pathCheck.AddLines(new CGPoint[]{
									new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),

								});
								var pathCheckNext = new CGPath();
								pathCheckNext.AddLines(new CGPoint[]{
									new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
									new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),

								});

								foreach (var lineNode in Children.OfType<SKShapeNode>())
								{
									if (lineNode.Path.PathBoundingBox == pathCheck.PathBoundingBox)
									{
										currentLine = lineNode;
									}
									if (lineNode.Path.PathBoundingBox == pathCheckNext.PathBoundingBox)
									{
										nextLine = lineNode;
									}

									/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
									{
										lineNode.Alpha = 0f;
									}*/
								}

								
							}
						}

					}
					if (newNode == true && delNode == false && (offsetY < Frame.Height - 70))
					{
						//**********





						currentSprite = new PointNode("sparkfilled");
						currentSprite.Position = new CGPoint(checkX, checkY);
						currentSprite.Category = "";
						//currentSprite.Size=
						currentSprite.ZPosition = 1;
						currentSprite.XScale = 0.4f;
						currentSprite.YScale = 0.4f;
						currentSprite.ColorBlendFactor = 1f;
						currentSprite.Alpha = 1f;
						currentSprite.Color = UIColor.FromHSB(0, 0, 0.2f);
						AddChild(currentSprite);
						checkSprites();

						var path = new CGPath();
						path.AddLines(new CGPoint[]{
							new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
							new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
						});
						path.CloseSubpath();
						SKShapeNode newline = new SKShapeNode();
						newline.Path = path;
						newline.StrokeColor = UIColor.FromHSB(0, 0, 0);
						newline.Alpha = 0.5f;
						newline.LineWidth = 2f;


						AddChild(newline);
						currentLine = newline;

						var pathNext = new CGPath();
						pathNext.AddLines(new CGPoint[]{
							new CGPoint (currentSprite.Position.X, currentSprite.Position.Y),
						new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),
						});
						pathNext.CloseSubpath();
						SKShapeNode newlineNext = new SKShapeNode();
						newlineNext.Path = pathNext;
						newlineNext.StrokeColor = UIColor.FromHSB(0, 0, 0);
						newlineNext.LineWidth = 2f;
						newlineNext.Alpha = 0.5f;
						AddChild(newlineNext);
						nextLine = newlineNext;


						var pathCheck = new CGPath();
						pathCheck.AddLines(new CGPoint[]{
						new CGPoint (lastSprite.Position.X, lastSprite.Position.Y),
						new CGPoint (nextSprite.Position.X, nextSprite.Position.Y),

					});

						// Time label Start
						timeLabel.Position = new CGPoint(currentSprite.Position.X, currentSprite.Position.Y + 50);
						double selectedHour = (24 / Frame.Width * currentSprite.Position.X);
						//	double fullHourRest = (24 / Frame.Width * location.X)- Math.Round(24 / Frame.Width * location.X, 0);
						//	double selectedMin = Math.Round(fullHourRest/100*60,0);
						String restMin = "";
						String stringHour = selectedHour.ToString();
						var splitHour = stringHour.Split('.');
						if (splitHour[0].Length == 1)
						{
							splitHour[0] = "0" + splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}
						}
						else {
							splitHour[0] = splitHour[0];
							if (splitHour.Length > 1)
							{

								if (splitHour[1].Length >= 2)
								{
									restMin = splitHour[1].Substring(0, 2);
								}
								else if (splitHour[1].Length == 1)
								{
									restMin = splitHour[1].Substring(0, 1) + "0";
								}
							}
							else {
								restMin = "00";
							}

						}
						double selectedMin = Math.Round(Convert.ToDouble(restMin) / 100 * 60, 0);
						String selectedMinString = selectedMin.ToString();
						if (selectedMinString.Length == 1)
						{
							selectedMinString = "0" + selectedMinString;
						}
						// Time calculated
						timeLabel.Text = splitHour[0] + ":" + selectedMinString;
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
						// Time label end
						foreach (var lineNode in Children.OfType<SKShapeNode>())
						{
							if (lineNode.Path.PathBoundingBox == pathCheck.PathBoundingBox && lineNode.Name != "baseline")
							{
								lineNode.Alpha = 0f;
								lineNode.RemoveFromParent();

							}
							/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
							{
								lineNode.Alpha = 0f;
							}*/
						}
						activeNode = currentSprite;
					}
					UIColor coloring;
					var speed = 0;
					if (checkY > (Frame.Height / 2))
					{
						speed = 1;
						coloring = UIColor.Green;
						//myLabel.Text = "Glücklich";
						//myLabel.Position = new CGPoint(Frame.Width / 2, (7 * (Frame.Height / 8)));
						updateLines(true, true);
					}


					else {
						speed = 4;
						coloring = UIColor.Red;
					//myLabel.Text = "Nervös";
					//myLabel.Position = new CGPoint(Frame.Width / 2, (1 * (Frame.Height / 8)));
					//myLabel.RunAction(SKAction.RotateByAngle(NMath.PI * speed, 10.0));
					updateLines(false, false);
				}

			
				}
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}
	}
}
