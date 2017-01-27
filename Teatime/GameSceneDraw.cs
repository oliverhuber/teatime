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
		SKSpriteNode oneSprite;
		SKSpriteNode secSprite;
		SKSpriteNode currentSprite;
		SKSpriteNode lastSprite;
		SKSpriteNode nextSprite;
		bool longPressEnabled = false;
		LineNodeDraw baseLine;
		bool startDragInside = false;
		double xLast;
		double xNext;
		SKLabelNode timeLabel;
		bool newNode;
		//LineNode yourline;
		SKLabelNode myLabel;
		SKLabelNode myLabelPos;
		SKLabelNode myLabelNeg;

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
		SKSpriteNode activeNode;
		bool firstTouch;

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
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Bewerte deine Stimmung durch den Tag",
				FontSize = 15,
				Position = new CGPoint(Frame.Width / 2, Frame.Height  - 80)
			};
			myLabel.Alpha = 0.9f;
			myLabel.ZPosition = 1;
			myLabel.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabel);


			myLabelPos = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "positiv",
				FontSize = 10,
				Position = new CGPoint(Frame.Width / 2, Frame.Height/2 +3)
			};
			myLabelPos.Alpha = 0.9f;
			myLabelPos.ZPosition = 1;
			myLabelPos.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabelPos);
			myLabelNeg = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "negativ",
				FontSize = 10,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 12)
			};
			myLabelNeg.Alpha = 0.9f;
			myLabelNeg.ZPosition = 1;
			myLabelNeg.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabelNeg);

			// New Label placed in the middle of the Screen
			timeLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Timelabel",
				FontSize = 15,
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
			secSprite = new SKSpriteNode("spark3");
			secSprite.Position = new CGPoint(0, Frame.Height/2);
			secSprite.ZPosition = 1;
			secSprite.XScale = 0.1f;
			secSprite.YScale = 0.1f;
			secSprite.Alpha = 0.8f;
			secSprite.ColorBlendFactor = 1f;
			secSprite.Color = UIColor.FromHSB(0, 0, 0);
		
			// Sprite Node
			oneSprite = new SKSpriteNode("spark3");
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
			yourline.Alpha = 0.2f;
			//yourline.ZPosition = 2;
			baseLine = new LineNodeDraw();
			baseLine.Name = "baseline";
			baseLine.Path = pathBase;
			baseLine.StrokeColor = UIColor.FromHSB(0, 0, 0);
			baseLine.Alpha = 0.5f;
			baseLine.ZPosition = 3;
			AddChild(baseLine);
			AddChild(yourline);
			AddChild(secSprite);
			AddChild(oneSprite);

			lastSprite = secSprite;
		//	xLast = secSprite.Position.X;
		//	xNext = oneSprite.Position.X;
			nextSprite = oneSprite;


			var gestureLongRecognizer = new UILongPressGestureRecognizer(PressHandler);
			gestureLongRecognizer.MinimumPressDuration = 1;
			this.View.AddGestureRecognizer(gestureLongRecognizer);
		}
		public void saveProto5Input()
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
			}
			item.dateInserted = DateTime.Now.ToLocalTime();
			item.Dim1 = Proto5Dim1;
			item.Dim2 = Proto5Dim2;
			item.Dim3 = Proto5Dim3;
			item.PrototypeNr = 5;
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
			if (gestureRecognizer.State == UIGestureRecognizerState.Began
				|| gestureRecognizer.State == UIGestureRecognizerState.Changed)
			{
				SKLabelNode gesLabel;
				// New Label placed in the middle of the Screen
				gesLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
				{
					Text = "ges",
					FontSize = 15,
					Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 100)
				};
				gesLabel.Alpha = 0.0f;
				gesLabel.ZPosition = 1;
				//AddChild(gesLabel);
				longPressEnabled = true;
				gesLabel.Text = "LongPressed: " + gestureRecognizer.LocationOfTouch(0, image).ToString();
				gesLabel.Alpha = 1.0f;
				foreach (var spriteNode in Children.OfType<SKSpriteNode>())
				{
					SKAction actMove = SKAction.MoveToX(spriteNode.Position.X-2, 0.1);
					SKAction actMoveBack = SKAction.MoveToX(spriteNode.Position.X+2, 0.1);
					SKAction seq = SKAction.Sequence(actMove, actMoveBack);
					spriteNode.RunAction(SKAction.RepeatActionForever(seq));
				}
			}
		}


		void checkSprites()
		{
			double diffXLast;
			double diffXNext;
			bool firstRun = true;

			foreach (var spriteNode in Children.OfType<SKSpriteNode>())
			{


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





				// Time label end

				if (currentSprite.Frame.Contains(((UITouch)touch).LocationInNode(this)))
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

					nfloat offsetX = (float)(touch.LocationInView(View).X);
					nfloat offsetY = (float)(touch.LocationInView(View).Y);
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
							}
							/*if (lineNode.Path. == pathCheckReverse.CurrentPoint)
							{
								lineNode.Alpha = 0f;
							}*/
						}
					}
					if (offsetX < nextSprite.Position.X-5 && offsetX-5 > lastSprite.Position.X)
					{

						currentSprite.Position = location;
						//= new CGPoint(currentSprite.Position.X, location.Y);
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
			}
			startDragInside = false;

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

				foreach (var spriteNode in Children.OfType<SKSpriteNode>())
				{
					if (spriteNode.Frame.Contains(((UITouch)touch).LocationInNode(this)))
					{


					// Time label Start
						timeLabel.Position = new CGPoint(spriteNode.Position.X, spriteNode.Position.Y +50);
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
						timeLabel.Text = splitHour[0] + ":" + selectedMinString;
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
				if (newNode == true)
				{
					//**********





					currentSprite = new SKSpriteNode("spark3");
					currentSprite.Position = new CGPoint(checkX, checkY);
					currentSprite.ZPosition = 1;
					currentSprite.XScale = 0.4f;
					currentSprite.YScale = 0.4f;
					currentSprite.ColorBlendFactor = 1f;
					currentSprite.Alpha = 0.9f;
					currentSprite.Color = UIColor.FromHSB(0, 0, 0);
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

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}
	}
}
