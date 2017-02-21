using System;
using System.Linq;
using AudioToolbox;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{

	public class GameSceneDraw : SKScene
	{
		// Prototype Dimension Mapping 
		public int Proto5Dim1 { get; set; }
		public int Proto5Dim2 { get; set; }
		public int Proto5Dim3 { get; set; }

		// Definition of the sprite nodes and vars

		private double xLast;
		private double xNext;
		private bool newNode;
		private bool delNode;
		private int gameMode;

		private PointNode lastPoint;
		private PointNode firstPoint;
		private PointNode currentPoint;
		private PointNode previousPoint;
		private PointNode nextPoint;
		private PointNode arrowLeftSprite;
		private PointNode arrowRightSprite;

		private LineNodeDraw baseLine;

		private SKLabelNode myLabel;
		private SKLabelNode myLabel2;
		private SKLabelNode myLabel3;
		private SKLabelNode timeLabel;

		private SKLabelNode myLabelPos;
		private SKLabelNode myLabelNeg;
		private SKSpriteNode navSprite;

		private SKShapeNode currentLine;
		private SKShapeNode nextLine;

		private PointNode spriteLeft;
		private PointNode spriteRight;
		private SKLabelNode mySave;

		private SKSpriteNode categoryTempFilter;
		private SKLabelNode categoryText1;
		private SKLabelNode categoryText2;
		private SKLabelNode categoryText3;
		private SKLabelNode categoryText4;
		private SKLabelNode categoryText5;
		private SKLabelNode categoryText6;
		private SKLabelNode categoryText7;
		private SKLabelNode categoryTextRel;
		private SKLabelNode categoryTextDel;
		private SKLabelNode categoryTextExit;

		private UITextField inputUserText;

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

			// Navigation Label and Sprite to switch game modes
			mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Name = "next",
				Text = "next >",
				FontSize = 18,
				Position = new CGPoint(Frame.Width - 50, (Frame.Height - 48))
			};
			mySave.FontColor = UIColor.FromHSB(0, 0, 0f);
			mySave.Alpha = 0.9f;
			mySave.ZPosition = 2;
			AddChild(mySave);

			// Navigation Sprite behind next label
			navSprite = new SKSpriteNode();
			navSprite.Name = "navSprite";
			navSprite.Alpha = 0.0000001f;
			navSprite.ZPosition = 10;
			navSprite.Color = UIColor.FromHSB(0, 1, 0.0f);
			navSprite.Size = new CGSize(140, 70);
			navSprite.Position = new CGPoint((View.Frame.Width - (70)), (View.Frame.Height - (35)));
			AddChild(navSprite);

			// Add the top label
			myLabelPos = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "",
				FontSize = 16,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 3)
			};
			myLabelPos.Alpha = 0.3f;
			myLabelPos.ZPosition = 1;
			myLabelPos.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabelPos);

			// Add the bottom Label
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

			// Add the time label to the scene
			timeLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Timelabel",
				FontSize = 16,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 100)
			};
			timeLabel.Alpha = 0.0f;
			timeLabel.ZPosition = 1;
			AddChild(timeLabel);

			// Set the background color of this scene
			BackgroundColor = UIColor.FromHSB(0, 0, 0.9f);

			// Setup Scene for game mode 1 with baseline, initial 2 sprites
			// First Point Node
			firstPoint = new PointNode("sparkfilled");
			firstPoint.Position = new CGPoint(0-6, Frame.Height / 2);
			firstPoint.ZPosition = 1;
			firstPoint.XScale = 0.1f;
			firstPoint.YScale = 0.1f;
			firstPoint.Alpha = 0.8f;
			firstPoint.ColorBlendFactor = 1f;
			firstPoint.Color = UIColor.FromHSB(0, 0, 0);
			AddChild(firstPoint);

			// Last Point  Node
			lastPoint = new PointNode("sparkfilled");
			lastPoint.Position = new CGPoint(Frame.Width+6, Frame.Height / 2);
			lastPoint.ZPosition = 1;
			lastPoint.XScale = 0.1f;
			lastPoint.YScale = 0.1f;
			lastPoint.ColorBlendFactor = 1f;
			lastPoint.Alpha = 0.8f;
			lastPoint.Color = UIColor.FromHSB(0, 0, 0);
			AddChild(lastPoint);


			// Create Path for the line, between both sprites
			/*var path = new CGPath();
			path.AddLines(new CGPoint[]{
				new CGPoint (lastPoint.Position.X, lastPoint.Position.Y),
				new CGPoint (firstPoint.Position.X, firstPoint.Position.Y)

					});
			path.CloseSubpath();
*/


			// Generate Line according to Path
			//yourline = new SKShapeNode();
			//yourline.Path = path;
			//yourline.StrokeColor = UIColor.FromHSB(0, 0, 0);
			//yourline.Alpha = 0.0f;
			//yourline.ZPosition = 2;

			/*UIBezierPath tempbezier = new UIBezierPath();
			tempbezier.MoveTo(new CGPoint(0, Frame.Height / 2));
			tempbezier.AddQuadCurveToPoint(
				new CGPoint(Frame.Width / 2, Frame.Height / 2),
				new CGPoint(100, 100)
				);
			tempbezier.MoveTo(new CGPoint(Frame.Width / 2, Frame.Height / 2));
			tempbezier.AddQuadCurveToPoint(
				new CGPoint(Frame.Width, Frame.Height / 2),
				new CGPoint(200, 100)
				);
			*/
	

			 
			// Set up game scene for game mode 2
			// Night Nodes 
			var locationLeft = new CGPoint();
			locationLeft.X = (0 - View.Frame.Width / 4 - 20);
			locationLeft.Y = (View.Frame.Height / 2);
		
			var locationRight = new CGPoint();
			locationRight.X = (View.Frame.Width + View.Frame.Width / 4 + 20);
			locationRight.Y = (View.Frame.Height / 2);

			spriteLeft = new PointNode()
			{
				Position = locationLeft,
				Name = "Left",
				XScale = 1,
				YScale = 1,
				Alpha = 0.8f,
				Color = UIColor.FromHSB(0, 0, 0.2f)

			};

			spriteRight = new PointNode()
			{
				Position = locationRight,
				Name = "Right",
				XScale = 1,
				YScale = 1,
				Alpha = 0.8f,
				Color = UIColor.FromHSB(0, 0, 0.2f)

			};

			// ZPosition in comparsion to other SpriteNodes
			spriteLeft.ZPosition = -1;
			spriteRight.ZPosition = -1;
			spriteLeft.Size = new CGSize(Frame.Width, Frame.Height);
			spriteRight.Size = new CGSize(Frame.Width, Frame.Height);
			AddChild(spriteLeft);
			AddChild(spriteRight);

			// Create Baseline
			var pathBase = new CGPath();
			pathBase.AddLines(new CGPoint[]{
				new CGPoint (lastPoint.Position.X, lastPoint.Position.Y),
				new CGPoint (firstPoint.Position.X, firstPoint.Position.Y)
			});
			pathBase.CloseSubpath();
			baseLine = new LineNodeDraw();
			baseLine.Name = "baseline";
			baseLine.Path = pathBase;
			baseLine.StrokeColor = UIColor.FromHSB(0, 0, 0);
			baseLine.Alpha = 0.1f;
			baseLine.ZPosition = 3;
			AddChild(baseLine);

			// Arrow Sprite Node
			arrowLeftSprite = new PointNode("arrow");
			arrowLeftSprite.Name = "arrowLeft";
			arrowLeftSprite.Position = new CGPoint(Frame.Width / 2 - 135, Frame.Height / 2);
			arrowLeftSprite.ZPosition = 1;
			arrowLeftSprite.XScale = 1f;
			arrowLeftSprite.YScale = 1f;
			arrowLeftSprite.ZRotation = ((nfloat)(Math.PI / 2));
			arrowLeftSprite.Alpha = 0.8f;
			AddChild(arrowLeftSprite);

			// Arrow Sprite Node
			arrowRightSprite = new PointNode("arrow");
			arrowRightSprite.Name = "arrowRight";
			arrowRightSprite.Position = new CGPoint(Frame.Width / 2 + 135, Frame.Height / 2);
			arrowRightSprite.ZPosition = 1;
			arrowRightSprite.ZRotation = -((nfloat)(Math.PI / 2));
			arrowRightSprite.XScale = 1f;
			arrowRightSprite.YScale = 1f;
			arrowRightSprite.Alpha = 0.8f;
			AddChild(arrowRightSprite);

			// Set the first navigation points
			previousPoint = firstPoint;
			nextPoint = lastPoint;

			delNode = false;
			SetInfoText();

			// Add long press gesture recognizer and handler
			var gestureLongRecognizer = new UILongPressGestureRecognizer(PressHandler);
			gestureLongRecognizer.MinimumPressDuration = 0.5;
			gestureLongRecognizer.AllowableMovement = 5f;
			View.AddGestureRecognizer(gestureLongRecognizer);
		}
		private void SetInfoText()
		{ 
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
			Text = "Erfasse deine Schlaf- ",
			FontSize = 18,
			Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 100)
			};
			myLabel.Alpha = 0.0f;
			myLabel.ZPosition = 1;
			myLabel.FontColor = UIColor.FromHSB(0, 0, 0);
			AddChild(myLabel);
			myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1), SKAction.FadeOutWithDuration(1)));

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
			myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(1), SKAction.FadeOutWithDuration(1)));

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
		}

		private void SetUpCategoryView()
		{
			SystemSound.Vibrate.PlaySystemSound();
			gameMode = 2;
			CheckSprites();
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

			categoryText1 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Wähle eine Kategorie / Aktion",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2 - 120, Frame.Height / 2 + 200)
			};
			categoryText1.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
			categoryText1.Alpha = 0.9f;
			categoryText1.ZPosition = 101;
			categoryText1.FontColor = UIColor.FromHSB(0, 0, 1f);
			AddChild(categoryText1);

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

			inputUserText = new UITextField();
			inputUserText.Frame = new CGRect(View.Bounds.Width / 2 - 127, View.Bounds.Height / 2 + 3, 225, 28);
			inputUserText.BackgroundColor = UIColor.FromHSB(0, 0, 0.2f);
			inputUserText.TextColor = UIColor.FromHSB(0.9f, 0.5f, 1.0f);
			View.AddSubview(inputUserText);
			inputUserText.ClearButtonMode = UITextFieldViewMode.WhileEditing;

			inputUserText.ShouldReturn += (textField) =>
			{
				inputUserText.ResignFirstResponder();
				SetPointCategory(inputUserText.Text, inputUserText.TextColor);
				return true;
			};
			inputUserText.ShouldChangeCharacters = (textField, range, replacementString) =>
			{
				var newLength = textField.Text.Length + replacementString.Length - range.Length;
				return newLength <= 20;
			};
			inputUserText.BorderStyle = UITextBorderStyle.RoundedRect;
			inputUserText.Font = UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f);

			if (currentPoint.Category == "" || currentPoint.Category == "Kollegium" || currentPoint.Category == "Einzelne Schüler" || currentPoint.Category == "Schülergruppen" || currentPoint.Category == "Eltern" || currentPoint.Category == "Schlaf" || currentPoint.Category == "Privater Grund")
			{
				inputUserText.AttributedPlaceholder = new NSAttributedString(
					"> Eigene Kategorie",
					font: UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f),
					foregroundColor: UIColor.FromHSB(0.9f, 0.5f, 1.0f)
				);
			}
			else {
				inputUserText.AttributedPlaceholder = new NSAttributedString(
					"> " + currentPoint.Category,
					font: UIFont.FromName("AppleSDGothicNeo-UltraLight", 18.0f),
					foregroundColor: UIColor.FromHSB(0.9f, 0.5f, 1.0f)
				);
				inputUserText.Text = currentPoint.Category;
			}
		}
		private void PressHandler(UILongPressGestureRecognizer gestureRecognizer)
		{
			if (gameMode == 1)
			{
				var image = gestureRecognizer.View;
				if (gestureRecognizer.State == UIGestureRecognizerState.Began || gestureRecognizer.State == UIGestureRecognizerState.Recognized || gestureRecognizer.State == UIGestureRecognizerState.Changed)
				{
					var location = gestureRecognizer.LocationInView(View);
					var convertLocation = new CGPoint(location.X, View.Frame.Height - location.Y);
					if (currentPoint != null && currentPoint.Frame.Contains(convertLocation) && currentPoint.Name != "Left" && currentPoint.Name != "Right" && currentPoint.Name != "navSprite" && currentPoint.Name != "arrowLeft" && currentPoint.Name != "arrowRight")
					{
						SetUpCategoryView();
					}


					if (currentPoint != null && currentPoint.Frame.Contains(convertLocation) && currentPoint.Name != "Left" && currentPoint.Name != "Right" && currentPoint.Name != "navSprite" && currentPoint.Name != "arrowLeft" && currentPoint.Name != "arrowRight")
					{
						timeLabel.Text = "";

						SKAction actMove = SKAction.ScaleTo(1f, 1f, 0.2);
						SKAction actMoveBack = SKAction.ScaleTo(0.4f, 0.4f, 0.2);
						SKAction seq = SKAction.Sequence(actMove, actMoveBack);

						currentPoint.RunAction(SKAction.RepeatAction(seq, 2));

					}
				}
			}
		}

		// Check which Sprite node is the one before and the next on the X axis and save them
		private void CheckSprites()
		{
			double diffXLast;
			double diffXNext;
			bool firstRun = true;

			foreach (var spriteNode in Children.OfType<PointNode>())
			{

				if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight")
				{
					diffXLast = currentPoint.Position.X - spriteNode.Position.X;
					diffXNext = spriteNode.Position.X - currentPoint.Position.X;
					if (firstRun == true)
					{
						xLast = currentPoint.Position.X - firstPoint.Position.X;
						xNext = lastPoint.Position.X - currentPoint.Position.X;
						firstRun = false;
					}
					if (diffXLast > 0 && xLast >= diffXLast)
					{
						previousPoint = spriteNode;
						xLast = diffXLast;
					}
					if (diffXNext > 0 && xNext >= diffXNext)
					{
						nextPoint = spriteNode;
						xNext = diffXNext;
					}

				}
			}
		}

		// Set the category for a dedicated point node and remove view, go back to active game mode
		private void SetPointCategory(string categoryCustomText, UIColor categoryColor)
		{
			newNode = false;
			currentPoint.Category = categoryCustomText;
			timeLabel.Text = categoryCustomText;
			currentPoint.Color = categoryColor;
			RemoveCategoryView();
		}

		// Remove Sprites and Labels, whole category view
		private void RemoveCategoryView()
		{
			categoryTempFilter.RemoveFromParent();
			categoryText1.RemoveFromParent();
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

		// Calculate Time (between 00:00-24:00) in format 00:00 depending on the X Value, the views vertical axis acts as 100%
		private string TimeCalculation(nfloat posX) { 
			double selectedHour = (24 / Frame.Width * posX);
			string restMin = "";
			string stringHour = selectedHour.ToString();
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
			string selectedMinString = selectedMin.ToString();
			if (selectedMinString.Length == 1)
			{
				selectedMinString = "0" + selectedMinString;
			}
			return splitHour[0] + ":" + selectedMinString;
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			newNode = true;

			// Called when a touch begins
			foreach (var touch in touches)
			{
				var touchc = touches.AnyObject as UITouch;
				var locationc = touchc.LocationInNode(this);

				// Check click
				nfloat checkX = touchc.LocationInNode(this).X;
				nfloat checkY = touchc.LocationInNode(this).Y;

				nfloat offsetY = (touchc.LocationInNode(this).Y);

				var nodeAtPoint = GetNodeAtPoint(locationc);
				if (gameMode == 2 && (nodeAtPoint is PointNode || nodeAtPoint.Name == "ExitLabel"))
				{
					// New node to add
					newNode = false;
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "DeleteLabel")
				{
					// Delete node, no one to add
					delNode = true;
					newNode = false;
					RemoveCategoryView();

					// Check before and after sprite nodes
					CheckSprites();

					// Remove sprite node
					currentPoint.RemoveFromParent();

					// Get pathes for removal
					var pathCheckRem = new CGPath();
					pathCheckRem.AddLines(new CGPoint[]{
						new CGPoint (previousPoint.Position.X, previousPoint.Position.Y),
						new CGPoint (currentPoint.Position.X, currentPoint.Position.Y)
					});
					var pathCheckRemNext = new CGPath();
					pathCheckRemNext.AddLines(new CGPoint[]{
						new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
						new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
					});

					timeLabel.Text = "Zeitpunkt gelöscht";

					// Remove matching line nodes from view
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
					}

					// Set the new path to connect before and after sprite nodes
					var pathReset = new CGPath();
					pathReset.AddLines(new CGPoint[]{
						new CGPoint (previousPoint.Position.X, previousPoint.Position.Y),
						new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
					});
					var newlineNext = new SKShapeNode();
					newlineNext.Path = pathReset;
					newlineNext.StrokeColor = UIColor.FromHSB(0, 0, 0);
					newlineNext.LineWidth = 2f;
					newlineNext.Alpha = 0.5f;
					AddChild(newlineNext);
					nextLine = newlineNext;
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat1")
				{
					newNode = false;
					timeLabel.Text = "Kollegium";
					currentPoint.Category = "Kollegium";
					currentPoint.Color = UIColor.FromHSB(0.3f, 0.5f, 0.8f);
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat2")
				{
					newNode = false;
					timeLabel.Text = "Einzelne Schüler";
					currentPoint.Category = "Einzelne Schüler";
					currentPoint.Color = UIColor.FromHSB(0.4f, 0.5f, 0.8f);
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat3")
				{
					newNode = false;
					timeLabel.Text = "Schülergruppen";
					currentPoint.Category = "Schülergruppen";
					currentPoint.Color = UIColor.FromHSB(0.5f, 0.5f, 0.8f);
					RemoveCategoryView();
									}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat4")
				{
					newNode = false;
					timeLabel.Text = "Eltern";
					currentPoint.Category = "Eltern";
					currentPoint.Color = UIColor.FromHSB(0.6f, 0.5f, 0.8f);
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat5")
				{
					newNode = false;
					timeLabel.Text = "Schlaf";
					currentPoint.Category = "Schlaf";
					currentPoint.Color = UIColor.FromHSB(0.7f, 0.5f, 0.8f);
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "Cat6")
				{
					newNode = false;
					timeLabel.Text = "Privater Grund";
					currentPoint.Category = "Privater Grund";
					currentPoint.Color = UIColor.FromHSB(0.8f, 0.5f, 0.8f);
					RemoveCategoryView();
				}
				else if (gameMode == 2 && nodeAtPoint.Name == "CatRelLabel")
				{
					newNode = false;
					currentPoint.Category = "";
					timeLabel.Text = "Keine Kategorie";
					currentPoint.Color = UIColor.FromHSB(0.0f, 0.0f, 0.2f);
					RemoveCategoryView();
				}

				// Move to game mode 1 from game mode 0
				if (((nodeAtPoint is SKLabelNode && nodeAtPoint.Name == "next") || nodeAtPoint.Name == "navSprite") && gameMode == 0)
				{
					gameMode = 1;

					myLabel.RemoveAllActions();
					myLabel2.RemoveAllActions();
					myLabel3.RemoveAllActions();

					myLabel.Alpha = 0f;
					myLabel2.Alpha = 0f;
					myLabel3.Alpha = 0f;

					myLabel.Text = "Bewerte nun deine Stimmung rückblickend";
					myLabel2.Text = "Drücke lange auf einen Punkt um diesem";
					myLabel3.Text = "eine Kategorie zuzuordnen";

					myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));

					// Set sleep mesure sprites as inactive
					spriteLeft.Alpha = 0.8f;
					spriteLeft.Color = UIColor.FromHSB(0, 0, 0.8f);
					spriteRight.Alpha = 0.8f;
					spriteRight.Color = UIColor.FromHSB(0, 0, 0.8f);

					timeLabel.Text = "";
					myLabelPos.Text = "+";
					myLabelNeg.Text = "_";

					// Set for all sprite points an active alpha, arrows disappear
					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						if (spriteNode.Name == "arrowLeft" || spriteNode.Name == "arrowRight")
						{
							spriteNode.Alpha = 0.0f;
						}
						else if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite")
						{
							spriteNode.Alpha = 1f;
						}
					}

					// Set for all line nodes an acive alpha
					foreach (var lineNode in Children.OfType<SKShapeNode>())
					{
						if (lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0.5f;
						}
					}
				}

				// Move to game mode 0 from game mode 1
				else if (((nodeAtPoint is SKLabelNode && nodeAtPoint.Name == "next") || nodeAtPoint.Name == "navSprite") && gameMode == 1)
				{
					gameMode = 0;

					myLabel.RemoveAllActions();
					myLabel2.RemoveAllActions();
					myLabel3.RemoveAllActions();

					myLabel.Alpha = 0f;
					myLabel2.Alpha = 0f;
					myLabel3.Alpha = 0f;

					myLabel.Text = "Erfasse deine Schlaf-";
					myLabel2.Text = "und Wachphase";
					myLabel3.Text = "Bestätige mit next >";

					myLabel.RunAction(SKAction.Sequence(SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel2.RunAction(SKAction.Sequence(SKAction.WaitForDuration(0.5), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));
					myLabel3.RunAction(SKAction.Sequence(SKAction.WaitForDuration(1), SKAction.FadeInWithDuration(1), SKAction.WaitForDuration(2), SKAction.FadeOutWithDuration(1)));

					spriteLeft.ZPosition = -2;
					spriteRight.ZPosition = -2;

					spriteLeft.Alpha = 0.8f;
					spriteLeft.Color = UIColor.FromHSB(0, 0, 0.2f);
					spriteRight.Alpha = 0.8f;
					spriteRight.Color = UIColor.FromHSB(0, 0, 0.2f);

					timeLabel.Text = "";
					myLabelPos.Text = "";
					myLabelNeg.Text = " Zeitachse ";

					// Set an active alpha for arrows and an inactive for all sprite points
					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						if (spriteNode.Name == "arrowLeft" || spriteNode.Name == "arrowRight")
						{
							spriteNode.Alpha = 0.8f;
						}
						else if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite")
						{
							spriteNode.Alpha = 0.2f;
						}
					}

					// Set for all lines an inactive alpha
					foreach (var lineNode in Children.OfType<SKShapeNode>())
					{
						if (lineNode.Name != "baseline")
						{
							lineNode.Alpha = 0.02f;
						}
					}

				}

				// if game mode is 1
				else if (gameMode == 1)
				{
					// Do for all sprite points
					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						// if it is not the navigation sprite
						if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight")
						{
							// Check if one of them contains the touch location
							if (spriteNode.Frame.Contains(((UITouch)touch).LocationInNode(this)) || ((spriteNode.Position.X == checkX) && spriteNode.Position.X > 0))
							{
								// Time label calculation Start
								timeLabel.Position = new CGPoint(spriteNode.Position.X, spriteNode.Position.Y + 50);

								if (delNode == false)
								{
									timeLabel.Text = TimeCalculation(spriteNode.Position.X);
								}
								timeLabel.ZPosition = 3;
								timeLabel.FontColor = UIColor.Black;
								timeLabel.Alpha = 0.8f;
								// Time label end

								// If a node is touched do not create new one and set the current one
								newNode = false;
								currentPoint = spriteNode;
								CheckSprites();

								var pathCheck = new CGPath();
								pathCheck.AddLines(new CGPoint[]{
									new CGPoint (previousPoint.Position.X, previousPoint.Position.Y),
									new CGPoint (currentPoint.Position.X, currentPoint.Position.Y)
								});
								var pathCheckNext = new CGPath();
								pathCheckNext.AddLines(new CGPoint[]{
									new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
									new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
								});

								// Set current and next line
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
								}
							}
						}
					}

					// If the node is not existing already create a new one
					if (newNode == true && delNode == false && (offsetY < Frame.Height - 70))
					{
		
						currentPoint = new PointNode("sparkfilled");
						currentPoint.Position = new CGPoint(checkX, checkY);
						currentPoint.Category = "";
						currentPoint.ZPosition = 1;
						currentPoint.XScale = 0.4f;
						currentPoint.YScale = 0.4f;
						currentPoint.ColorBlendFactor = 1f;
						currentPoint.Alpha = 1f;
						currentPoint.Color = UIColor.FromHSB(0, 0, 0.2f);
						AddChild(currentPoint);
						CheckSprites();

						var path = new CGPath();
						path.AddLines(new CGPoint[]{
							new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
							new CGPoint (previousPoint.Position.X, previousPoint.Position.Y)
						});
						path.CloseSubpath();
						var newline = new SKShapeNode();
						newline.Path = path;
						newline.StrokeColor = UIColor.FromHSB(0, 0, 0);
						newline.Alpha = 0.5f;
						newline.LineWidth = 2f;


						AddChild(newline);
						currentLine = newline;

						var pathNext = new CGPath();
						pathNext.AddLines(new CGPoint[]{
							new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
							new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
						});
						pathNext.CloseSubpath();
						var newlineNext = new SKShapeNode();
						newlineNext.Path = pathNext;
						newlineNext.StrokeColor = UIColor.FromHSB(0, 0, 0);
						newlineNext.LineWidth = 2f;
						newlineNext.Alpha = 0.5f;
						AddChild(newlineNext);
						nextLine = newlineNext;


						var pathCheck = new CGPath();
						pathCheck.AddLines(new CGPoint[]{
							new CGPoint (previousPoint.Position.X, previousPoint.Position.Y),
							new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
						});

						// Time label Start
						timeLabel.Position = new CGPoint(currentPoint.Position.X, currentPoint.Position.Y + 50);
						timeLabel.Text = TimeCalculation(currentPoint.Position.X);
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;

						foreach (var lineNode in Children.OfType<SKShapeNode>())
						{
							if (lineNode.Path.PathBoundingBox == pathCheck.PathBoundingBox && lineNode.Name != "baseline")
							{
								lineNode.Alpha = 0f;
								lineNode.RemoveFromParent();

							}
						}
					}
				}
			}
		}
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			// get the touch
			var touch = touches.AnyObject as UITouch;
			var location = touch.LocationInNode(this);
			if (touch != null)
			{
				nfloat offsetX = touch.LocationInNode(this).X;
				nfloat offsetY = touch.LocationInNode(this).Y;

				if (gameMode == 0)
				{
					if (offsetX < Frame.Width / 2 && (offsetY < Frame.Height - 70))
					{
						spriteLeft.Position = new CGPoint(0 - Frame.Width / 2 + offsetX, Frame.Height / 2);
						arrowLeftSprite.Position = new CGPoint(offsetX - 20, Frame.Height / 2);
						timeLabel.Position = new CGPoint(offsetX + 30, Frame.Height / 2 + 15);
						timeLabel.Text = TimeCalculation(offsetX);
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
					}

					if (offsetX > Frame.Width / 2 && offsetY < Frame.Height - 70)
					{
						spriteRight.Position = new CGPoint(Frame.Width - Frame.Width / 2 + offsetX, Frame.Height / 2);
						arrowRightSprite.Position = new CGPoint(offsetX + 20, Frame.Height / 2);
						timeLabel.Position = new CGPoint(offsetX - 30, Frame.Height / 2 + 15);
						timeLabel.Text = TimeCalculation(offsetX); 
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
					}
				}

				if (gameMode == 1 && currentPoint != null)
				{
					if (currentPoint.Frame.Contains(touch.LocationInNode(this)) && delNode == false && currentPoint.Name != "Left" && currentPoint.Name != "Right")
					{
						timeLabel.Position = new CGPoint(currentPoint.Position.X, currentPoint.Position.Y + 50);
						timeLabel.Text = TimeCalculation(currentPoint.Position.X); 
						timeLabel.ZPosition = 3;
						timeLabel.FontColor = UIColor.Black;
						timeLabel.Alpha = 0.8f;
						if (newNode == true)
						{
							var pathCheck = new CGPath();
							pathCheck.AddLines(new CGPoint[]{
								new CGPoint (previousPoint.Position.X, previousPoint.Position.Y),
								new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
							});

							// Remove current line between before and after sprite
							foreach (var lineNode in Children.OfType<SKShapeNode>())
							{
								if (lineNode.Path.PathBoundingBox == pathCheck.PathBoundingBox && lineNode.Name != "baseline")
								{
									lineNode.Alpha = 0f;
									lineNode.RemoveFromParent();

								}
							}
						}
						if ((offsetX < nextPoint.Position.X - 8) && (offsetX - 8 > previousPoint.Position.X))
						{
							currentPoint.Position = location;
						}
						else if (offsetX > nextPoint.Position.X - 8)
						{
							currentPoint.Position = new CGPoint(currentPoint.Position.X, offsetY);
						}
						else if (offsetX < previousPoint.Position.X + 8)
						{
							currentPoint.Position = new CGPoint(currentPoint.Position.X, offsetY);
						}
						CheckSprites();

						// Update pathes during move
						var path = new CGPath();
						path.AddLines(new CGPoint[]{
							new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
							new CGPoint (previousPoint.Position.X, previousPoint.Position.Y)
						});
						path.CloseSubpath();
						currentLine.Path = path;

						// Update pathes during move
						var pathNext = new CGPath();
						pathNext.AddLines(new CGPoint[]{
							new CGPoint (currentPoint.Position.X, currentPoint.Position.Y),
							new CGPoint (nextPoint.Position.X, nextPoint.Position.Y)
						});
						pathNext.CloseSubpath();
						nextLine.Path = pathNext;

						// Scale dragged point node 
						SKAction actMove = SKAction.ScaleTo(0.8f, 0.8f, 0.2);
						currentPoint.RunAction(SKAction.RepeatAction(actMove, 1));
					}
				}
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			var touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				var location = touch.LocationInNode(this);
				var nodeAtPoint = GetNodeAtPoint(location);
				var spriteNodeChecked = nodeAtPoint;

				// Scale down dragged sprite point after release the drag
				SKAction actMoveBack = SKAction.ScaleTo(0.4f, 0.4f, 0.2);
				if (gameMode == 1 && (spriteNodeChecked.Name != "Left" && spriteNodeChecked.Name != "Right" && spriteNodeChecked.Name != "navSprite" && spriteNodeChecked.Name != "arrowLeft" && spriteNodeChecked.Name != "arrowRight"))
				{
					foreach (var spriteNode in Children.OfType<PointNode>())
					{
						if (spriteNode.Name != "Left" && spriteNode.Name != "Right" && spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight")
						{
							currentPoint.RunAction(SKAction.RepeatAction(actMoveBack, 1));
						}
					}
				}
			}
			delNode = false;
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}

		public void SaveProto5Input()
		{
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
					item.Comment = TimeCalculation(offsetX);
					item.Dim2 = 1;
				}
				else if (spriteNode.Name == "Right")
				{
					nfloat offsetX = spriteRight.Position.X - (Frame.Width - Frame.Width / 2);
					item.Comment = TimeCalculation(offsetX);
					item.Dim3 = 1;
				}
				else if (spriteNode.Name == "navSprite" || spriteNode.Name == "arrowLeft" || spriteNode.Name == "arrowRight")
				{
					// Ignore
				}
				else {

					string catText = "";

					if (spriteNode is PointNode)
					{
						catText = spriteNode.Category;
						if (catText == "")
						{
							catText = "No Categorie";
						}
					}

					item.Comment = TimeCalculation(spriteNode.Position.X) + ";" + catText;
					item.Dim1 = (int)(Frame.Height / 2 - spriteNode.Position.Y);
				}

				if (spriteNode.Name != "navSprite" && spriteNode.Name != "arrowLeft" && spriteNode.Name != "arrowRight")
				{
					DatabaseMgmt.Database.SaveItem(item);
				}
			}
		}
	}
}
