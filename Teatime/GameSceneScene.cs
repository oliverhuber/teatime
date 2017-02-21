using System;
using CoreGraphics;
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
		private nfloat hueTop;
		private nfloat satTop;
		private nfloat briTop;
		private nfloat hueBel;
		private nfloat satBel;
		private nfloat briBel;

		private double manipulate;
		private int gameMode;
		private bool startDragMenu;
		private bool actionStarted;
		private nfloat lastY;

		private SKLabelNode navLabelTop;
		private SKLabelNode navLabelBottom;
		private SKSpriteNode navSpriteTop;
		private SKSpriteNode navSpriteBottom;

		private FieldNode spriteTop;
		private FieldNode spriteTopBg;
		private FieldNode spriteBelow;
		private FieldNode spriteBelowBg;

		private UIColor lastColor;

		private SKShapeNode followDragNode;

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
			hueTop = 0;
			hueBel = 0;
			satTop = 0;
			satBel = 0;
			briTop = 0.3f;
			briBel = 0.6f;

			GenerateSprites();

			// Generate Label 1
			navLabelTop = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Wie fühlst du dich?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) + 20)
			};
			navLabelTop.FontColor = UIColor.FromHSB(0, 0, 3f);
			navLabelTop.Alpha = 0.9f;
			navLabelTop.ZPosition = 2;
			AddChild(navLabelTop);

			// Generate Label 2
			navLabelBottom = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Wie möchtest du dich fühlen?",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, (Frame.Height / 2) - 40)
			};
			navLabelBottom.FontColor = UIColor.FromHSB(0, 0, 6f);
			navLabelBottom.ZPosition = 2;
			navLabelBottom.Alpha = 0.9f;
			AddChild(navLabelBottom);

			// Cover the labels with a sprite node for better navigation covering of touch
			navSpriteTop = new SKSpriteNode();
			navSpriteTop.Name = "navSpriteTop";
			navSpriteTop.Alpha = 0.0000001f;
			navSpriteTop.ZPosition = 1.1f;
			navSpriteTop.Color = UIColor.FromHSB(0, 1, 0.0f);
			navSpriteTop.Size = new CGSize(Frame.Width, 70);
			navSpriteTop.Position = new CGPoint((View.Frame.Width / 2), (View.Frame.Height / 2 + (35)));
			AddChild(navSpriteTop);

			// Cover the labels with a sprite node for better navigation covering of touch
			navSpriteBottom = new SKSpriteNode();
			navSpriteBottom.Name = "navSpriteBottom";
			navSpriteBottom.Alpha = 0.0000001f;
			navSpriteBottom.ZPosition = 1.1f;
			navSpriteBottom.Color = UIColor.FromHSB(1, 0, 0.0f);
			navSpriteBottom.Size = new CGSize(Frame.Width, 70);
			navSpriteBottom.Position = new CGPoint((View.Frame.Width / 2), (View.Frame.Height / 2 - (35)));
			AddChild(navSpriteBottom);

			// No Drag, no Action, used to block different actions at the same time
			// Start used is active until touch from menu is released
			startDragMenu = false;
			actionStarted = false;
		}

		// Generate Sprite fields 
		private void GenerateSprites()
		{
			var locationTop = new CGPoint();
			locationTop.X = (View.Frame.Width / 2);
			locationTop.Y = (View.Frame.Height);

			var locationBelow = new CGPoint();
			locationBelow.X = (View.Frame.Width / 2);
			locationBelow.Y = 0;

			// Define Spark with location and alpha
			spriteTop = new FieldNode()
			{
				Position = locationTop,
				Name = "TopBg",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB(0.8f, 0.5f, 0.5f)

			};

			spriteTopBg = new FieldNode()
			{
				Position = locationTop,
				Name = "Top",
				XScale = 1,
				YScale = 1,
				Alpha = 0f,
				Texture = SKTexture.FromImageNamed(("background"))
			};

			spriteBelow = new FieldNode()
			{
				Position = locationBelow,
				Name = "Below",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB(0.5f, 0.5f, 0.5f)
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

			// Create Circle
			var tempPath = new UIBezierPath();
			tempPath.MoveTo(new CGPoint(0, 0));
			tempPath.AddArc(new CGPoint(0, 0), 60, 0, 2.0f * (float)Math.PI, true);

			CGPath convertedPath = tempPath.CGPath;
			followDragNode = new SKShapeNode()
			{
				Position = locationTop,
				Path = convertedPath,
				Name = "follow",
				XScale = 1,
				YScale = 1,
				Alpha = 0f,
				FillColor = UIColor.FromHSB(0, 0, 1f),
			};

			// Position in comparsion to other SpriteNodes
			spriteTop.ZPosition = 1;
			spriteBelow.ZPosition = 1;
			spriteTopBg.ZPosition = 0;
			spriteBelowBg.ZPosition = 0;
			followDragNode.ZPosition = 0.5f;

			// Update Sizes
			spriteTopBg.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteBelowBg.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteTop.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			spriteBelow.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);

			// Add nodes
			AddChild(spriteTopBg);
			AddChild(spriteBelowBg);
			AddChild(spriteTop);
			AddChild(spriteBelow);
			AddChild(followDragNode);
		}

		private void MoveToUpFromCenterClear()
		{
			spriteTopBg.RemoveAllActions();
			spriteTop.RemoveAllActions();

			spriteBelowBg.RemoveAllActions();
			spriteBelow.RemoveAllActions();

			navSpriteTop.RemoveAllActions();
			navLabelTop.RemoveAllActions();

			navSpriteBottom.RemoveAllActions();
			navLabelBottom.RemoveAllActions();

			SKAction actionTop = SKAction.MoveToY((View.Frame.Height * 1.5f) - 60, 0.2);
			SKAction actionBelow = SKAction.MoveToY((View.Frame.Height / 2) - 60, 0.2);
			SKAction actionBelowLabel = SKAction.MoveToY(View.Frame.Height - 100, 0.2);
			SKAction actionTopLabel = SKAction.MoveToY(View.Frame.Height - 40, 0.2);

			spriteTopBg.RunAction(actionTop);
			spriteTop.RunAction(actionTop);

			spriteBelowBg.RunAction(actionBelow);
			spriteBelow.RunAction(actionBelow);

			navSpriteTop.RunAction(actionTopLabel);
			navLabelTop.RunAction(actionTopLabel);

			navSpriteBottom.RunAction(actionBelowLabel);
			navLabelBottom.RunAction(actionBelowLabel);

			gameMode = 1;
		}

		private void MoveToDownFromCenterClear()
		{
			spriteTopBg.RemoveAllActions();
			spriteTop.RemoveAllActions();

			spriteBelowBg.RemoveAllActions();
			spriteBelow.RemoveAllActions();

			navSpriteTop.RemoveAllActions();
			navLabelTop.RemoveAllActions();

			navSpriteBottom.RemoveAllActions();
			navLabelBottom.RemoveAllActions();

			SKAction actionTop = SKAction.MoveToY((View.Frame.Height / 2) + 60, 0.2);
			SKAction actionBelow = SKAction.MoveToY((0 - View.Frame.Height / 2) + 60, 0.2);
			SKAction actionBelowLabel = SKAction.MoveToY(0 + 20, 0.2);
			SKAction actionTopLabel = SKAction.MoveToY(0 + 80, 0.2);

			spriteTopBg.RunAction(actionTop);
			spriteTop.RunAction(actionTop);

			spriteBelowBg.RunAction(actionBelow);
			spriteBelow.RunAction(actionBelow);

			navSpriteTop.RunAction(actionTopLabel);
			navLabelTop.RunAction(actionTopLabel);

			navSpriteBottom.RunAction(actionBelowLabel);
			navLabelBottom.RunAction(actionBelowLabel);

			gameMode = 2;
		}

		private void MoveToUpFromCenter()
		{
			SKAction actionTopA = SKAction.MoveToY((View.Frame.Height * 1.5f) - 55, 0.1);
			SKAction actionTopB = SKAction.MoveToY((View.Frame.Height * 1.5f) - 65, 0.1);
			SKAction actionTopC = SKAction.MoveToY((View.Frame.Height * 1.5f) - 60, 0.2);

			SKAction actionBelowA = SKAction.MoveToY((View.Frame.Height / 2) - 55, 0.1);
			SKAction actionBelowB = SKAction.MoveToY((View.Frame.Height / 2) - 65, 0.1);
			SKAction actionBelowC = SKAction.MoveToY((View.Frame.Height / 2) - 60, 0.2);

			SKAction actionBelowLabelA = SKAction.MoveToY(View.Frame.Height - 95, 0.1);
			SKAction actionBelowLabelB = SKAction.MoveToY(View.Frame.Height - 105, 0.1);
			SKAction actionBelowLabelC = SKAction.MoveToY(View.Frame.Height - 100, 0.2);

			SKAction actionTopLabelA = SKAction.MoveToY(View.Frame.Height - 35, 0.1);
			SKAction actionTopLabelB = SKAction.MoveToY(View.Frame.Height - 45, 0.1);
			SKAction actionTopLabelC = SKAction.MoveToY(View.Frame.Height - 40, 0.2);

			SKAction seqTop = SKAction.Sequence(actionTopA, actionTopB, actionTopC);
			SKAction seqBelow = SKAction.Sequence(actionBelowA, actionBelowB, actionBelowC);
			SKAction seqTopLabel = SKAction.Sequence(actionTopLabelA, actionTopLabelB, actionTopLabelC);
			SKAction seqBelowLabel = SKAction.Sequence(actionBelowLabelA, actionBelowLabelB, actionBelowLabelC);

			spriteTop.RunAction(seqTop);
			spriteTopBg.RunAction(seqTop);

			spriteBelow.RunAction(seqBelow);
			spriteBelowBg.RunAction(seqBelow);

			navSpriteBottom.RunAction(seqBelowLabel);
			navLabelBottom.RunAction(seqBelowLabel);

			navLabelTop.RunAction(seqTopLabel);
			navSpriteTop.RunAction(seqTopLabel);

			gameMode = 1;
			actionStarted = false;	
		}

		private void MoveToDownFromCenter()
		{
			SKAction actionTopA = SKAction.MoveToY((View.Frame.Height / 2) + 55, 0.1);
			SKAction actionTopB = SKAction.MoveToY((View.Frame.Height / 2) + 65, 0.1);
			SKAction actionTopC = SKAction.MoveToY((View.Frame.Height / 2) + 60, 0.2);

			SKAction actionBelowA = SKAction.MoveToY((0 - View.Frame.Height / 2) + 55, 0.1);
			SKAction actionBelowB = SKAction.MoveToY((0 - View.Frame.Height / 2) + 65, 0.1);
			SKAction actionBelowC = SKAction.MoveToY((0 - View.Frame.Height / 2) + 60, 0.2);

			SKAction actionBelowLabelA = SKAction.MoveToY(0 + 15, 0.1);
			SKAction actionBelowLabelB = SKAction.MoveToY(0 + 25, 0.1);
			SKAction actionBelowLabelC = SKAction.MoveToY(0 + 20, 0.2);

			SKAction actionTopLabelA = SKAction.MoveToY(0 + 75, 0.1);
			SKAction actionTopLabelB = SKAction.MoveToY(0 + 85, 0.1);
			SKAction actionTopLabelC = SKAction.MoveToY(0 + 80, 0.2);

			SKAction seqTop = SKAction.Sequence(actionTopA, actionTopB, actionTopC);
			SKAction seqBelow = SKAction.Sequence(actionBelowA, actionBelowB, actionBelowC);
			SKAction seqTopLabel = SKAction.Sequence(actionTopLabelA, actionTopLabelB, actionTopLabelC);
			SKAction seqBelowLabel = SKAction.Sequence(actionBelowLabelA, actionBelowLabelB, actionBelowLabelC);

			spriteTop.RunAction(seqTop);
			spriteTopBg.RunAction(seqTop);

			spriteBelow.RunAction(seqBelow);
			spriteBelowBg.RunAction(seqBelow);

			navSpriteBottom.RunAction(seqBelowLabel);
			navLabelBottom.RunAction(seqBelowLabel);

			navLabelTop.RunAction(seqTopLabel);
			navSpriteTop.RunAction(seqTopLabel);

			gameMode = 2;
			actionStarted = false;
		}

		public void MoveToCenterFromDown()
		{
			SKAction actionTopA = SKAction.MoveToY(View.Frame.Height + 5, 0.1);
			SKAction actionTopB = SKAction.MoveToY(View.Frame.Height - 5, 0.1);
			SKAction actionTopC = SKAction.MoveToY(View.Frame.Height, 0.2);

			SKAction actionBelowA = SKAction.MoveToY(0 + 5, 0.1);
			SKAction actionBelowB = SKAction.MoveToY(0 - 5, 0.1);
			SKAction actionBelowC = SKAction.MoveToY(0, 0.2);

			SKAction actionTopLabelA = SKAction.MoveToY(View.Frame.Height / 2 + 25, 0.1);
			SKAction actionTopLabelB = SKAction.MoveToY(View.Frame.Height / 2 + 15, 0.1);
			SKAction actionTopLabelC = SKAction.MoveToY(View.Frame.Height / 2 + 20, 0.2);

			SKAction actionBelowLabelA = SKAction.MoveToY(View.Frame.Height / 2 - 35, 0.1);
			SKAction actionBelowLabelB = SKAction.MoveToY(View.Frame.Height / 2 - 45, 0.1);
			SKAction actionBelowLabelC = SKAction.MoveToY(View.Frame.Height / 2 - 40, 0.2);

			SKAction seqTop = SKAction.Sequence(actionTopA, actionTopB, actionTopC);
			SKAction seqBelow = SKAction.Sequence(actionBelowA, actionBelowB, actionBelowC);
			SKAction seqTopLabel = SKAction.Sequence(actionTopLabelA, actionTopLabelB, actionTopLabelC);
			SKAction seqBelowLabel = SKAction.Sequence(actionBelowLabelA, actionBelowLabelB, actionBelowLabelC);

			spriteTop.RunAction(seqTop);
			spriteTopBg.RunAction(seqTop);

			spriteBelow.RunAction(seqBelow);
			spriteBelowBg.RunAction(seqBelow);

			navSpriteBottom.RunAction(seqBelowLabel);
			navLabelBottom.RunAction(seqBelowLabel);

			navLabelTop.RunAction(seqTopLabel);
			navSpriteTop.RunAction(seqTopLabel);

			gameMode = 0;
			actionStarted = false;
		}

		private void MoveToCenterFromUp()
		{
			SKAction actionTopA = SKAction.MoveToY(View.Frame.Height - 5, 0.1);
			SKAction actionTopB = SKAction.MoveToY(View.Frame.Height + 5, 0.1);
			SKAction actionTopC = SKAction.MoveToY(View.Frame.Height, 0.2);

			SKAction actionBelowA = SKAction.MoveToY(0 - 5, 0.1);
			SKAction actionBelowB = SKAction.MoveToY(0 + 5, 0.1);
			SKAction actionBelowC = SKAction.MoveToY(0, 0.2);

			SKAction actionTopLabelA = SKAction.MoveToY(View.Frame.Height / 2 + 15, 0.1);
			SKAction actionTopLabelB = SKAction.MoveToY(View.Frame.Height / 2 + 25, 0.1);
			SKAction actionTopLabelC = SKAction.MoveToY(View.Frame.Height / 2 + 20, 0.2);

			SKAction actionBelowLabelA = SKAction.MoveToY(View.Frame.Height / 2 - 45, 0.1);
			SKAction actionBelowLabelB = SKAction.MoveToY(View.Frame.Height / 2 - 35, 0.1);
			SKAction actionBelowLabelC = SKAction.MoveToY(View.Frame.Height / 2 - 40, 0.2);

			SKAction seqTop = SKAction.Sequence(actionTopA, actionTopB, actionTopC);
			SKAction seqBelow = SKAction.Sequence(actionBelowA, actionBelowB, actionBelowC);
			SKAction seqTopLabel = SKAction.Sequence(actionTopLabelA, actionTopLabelB, actionTopLabelC);
			SKAction seqBelowLabel = SKAction.Sequence(actionBelowLabelA, actionBelowLabelB, actionBelowLabelC);

			spriteTop.RunAction(seqTop);
			spriteTopBg.RunAction(seqTop);

			spriteBelow.RunAction(seqBelow);
			spriteBelowBg.RunAction(seqBelow);

			navSpriteBottom.RunAction(seqBelowLabel);
			navLabelBottom.RunAction(seqBelowLabel);

			navLabelTop.RunAction(seqTopLabel);
			navSpriteTop.RunAction(seqTopLabel);

			actionStarted = false;
			gameMode = 0;
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				
				var touchc = touches.AnyObject as UITouch;
				var locationc = touchc.LocationInNode(this);

				// Check touch location
				nfloat checkX = touchc.LocationInNode(this).X;
				nfloat checkY = touchc.LocationInNode(this).Y;

				// Get node at location
				var nodeAtLocation = GetNodeAtPoint(locationc);

				// Checkings
				// Set actionStarted and startDragMenu that no change to active edit mode is possible
				// Game mode in Start Mode and touch in Menu
				if (gameMode == 0 && checkY >= (Frame.Height / 2) - 70 && checkY <= (Frame.Height / 2) + 70)
				{
					startDragMenu = true;
					actionStarted = true;
				}

				// Game in Edit Mode and touch in Menu
				else if (gameMode == 1 && checkY > Frame.Height - 120)
				{
					startDragMenu = true;
					actionStarted = true;
				}

				// Game in Edit Mode and touch in Menu
				else if (gameMode == 2 && checkY < 0 + 120)
				{
					startDragMenu = true;
					actionStarted = true;
				}


				// Actions
				// Game Mode in Start Mode and touch in Field Node
				if (gameMode == 0)
				{
					// Move Sprites and Labels up and go set Edit Mode
					if ((checkY) < Frame.Height / 2 - 70)
					{
						MoveToUpFromCenterClear();
					}

					// Move Sprites and Labels down and set Edit Mode
					else if ((checkY) > Frame.Height / 2 + 70)
					{
						MoveToDownFromCenterClear();
					}
				}

				// Check if touch is not in menu and Touched Mode is field node and in edit mode
				// Do only if sprite top and spite below has no actions, otherwise would lead in a locked mode
				else if (startDragMenu == false && nodeAtLocation is FieldNode && (gameMode == 1 || gameMode == 2) && !spriteTop.HasActions && !spriteBelow.HasActions)
				{
					var touchedNode = (FieldNode)GetNodeAtPoint(locationc);

					if (touchedNode == spriteTop)
					{
						spriteTop.Alpha = 0f;
						spriteTopBg.Alpha = 1f;
					}
					else 
					{
						spriteBelow.Alpha = 0f;
						spriteBelowBg.Alpha = 1f;
					}
				}
			}
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			var touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				var locationc = touch.LocationInNode(this);

				// Check Location
				nfloat checkX = touch.LocationInNode(this).X;
				nfloat checkY = touch.LocationInNode(this).Y;
				lastY = checkY;

				// Game Mode in Edit Mode and no Navigation Drag and do only if sprite is not moving to another game mode
				if (gameMode != 0 && startDragMenu == false && !spriteTop.HasActions && !spriteBelow.HasActions)
				{
					// Game Mode 2=EditTop, and click in field not navigation, do Draw
					if (gameMode == 2 && checkY > 0 + 70)
					{
						// Match Background to color range background
						manipulate = (checkY / Frame.Height) + 0.2;
						if (manipulate > 1)
						{
							manipulate = manipulate - 1;
						}

						// Show Color Range
						spriteTop.Alpha = 0f;
						spriteTopBg.Alpha = 1f;

						// Set Color values to read each separate
						hueTop = (nfloat)(manipulate);
						satTop = 0.5f;
						briTop = (((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));

						// Set Color as Background
						lastColor = UIColor.FromHSB((nfloat)(manipulate), 0.5f, (((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
						spriteTop.Color = lastColor;

						// Show Circle as color lense
						followDragNode.FillColor = lastColor;
						followDragNode.StrokeColor = lastColor;
						followDragNode.Alpha = 0.8f;
						followDragNode.Position = locationc;
					}
					else if (gameMode == 1 && checkY < Frame.Height - 70)
					{
						// Match Background to color range background
						manipulate = (checkY / Frame.Height) + 0.37;
						if (manipulate > 1)
						{
							manipulate = manipulate - 1;
						}

						// Show Color Range
						spriteBelow.Alpha = 0f;
						spriteBelowBg.Alpha = 1f;

						// Set Color values to read each separate
						hueBel = (nfloat)(manipulate);
						satBel = 0.5f;
						briBel = (((checkX / Frame.Width) / 3) * 2 + ((0.3333333f)));

						// Set Color as Background
						lastColor = UIColor.FromHSB((nfloat)(manipulate), 0.5f, (((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
						spriteBelow.Color = lastColor;

						// Show Circle as color lense
						followDragNode.FillColor = lastColor;
						followDragNode.StrokeColor = lastColor;
						followDragNode.Alpha = 0.8f;
						followDragNode.Position = locationc;
					}
				}

				// Drag Menu Bar Below 
				if (gameMode == 2 && checkY <= 0 + 100 && startDragMenu == true && actionStarted == true)
				{
					spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, ((View.Frame.Height * 0.5f) + 60) + checkY);
					spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X, -View.Frame.Height / 2 + 60 + checkY);
					spriteTop.Position = new CGPoint(spriteTop.Position.X, ((View.Frame.Height * 0.5f) + 60) + checkY);
					spriteBelow.Position = new CGPoint(spriteBelowBg.Position.X, -View.Frame.Height / 2 + 60 + checkY);
					navLabelTop.Position = new CGPoint(navLabelBottom.Position.X, checkY + 80);
					navLabelBottom.Position = new CGPoint(navLabelTop.Position.X, checkY + 20);
					navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY + 80);
					navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY + 20);
				}

				// Activate move when menu is dragged over threashold
				else if (gameMode == 2 && checkY > 0 + 100 && startDragMenu == true && actionStarted == true)
				{
					MoveToCenterFromDown();

				}

				// Drag Menu Bar Top
				else if (gameMode == 1 && checkY >= Frame.Height - 100 && startDragMenu == true && actionStarted == true)
				{
					spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, ((View.Frame.Height * 1.5f) - Frame.Height - 60) + checkY);
					spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X, View.Frame.Height / 2 - Frame.Height - 60 + checkY);
					spriteTop.Position = new CGPoint(spriteTop.Position.X, ((View.Frame.Height * 1.5f) - Frame.Height - 60) + checkY);
					spriteBelow.Position = new CGPoint(spriteBelowBg.Position.X, View.Frame.Height / 2 - Frame.Height - 60 + checkY);
					navLabelTop.Position = new CGPoint(navLabelBottom.Position.X, checkY - 40);
					navLabelBottom.Position = new CGPoint(navLabelTop.Position.X, checkY - 100);
					navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY - 40);
					navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY - 100);
				}

				// Activate move when menu is dragged over threashold
				else if (gameMode == 1 && checkY < Frame.Height - 100 && startDragMenu == true && actionStarted == true)
				{
					MoveToCenterFromUp();
				}

				if (gameMode == 0 && startDragMenu == true && actionStarted == true)
				{
					// Drag Menu Bar in Center in Start Game above
					if (checkY <= (Frame.Height / 2) + 70 && checkY >= (Frame.Height / 2) - 70)
					{
						spriteTopBg.Position = new CGPoint(spriteTopBg.Position.X, View.Frame.Height / 2 + checkY);
						spriteBelowBg.Position = new CGPoint(spriteBelowBg.Position.X, checkY - View.Frame.Height / 2);
						spriteTop.Position = new CGPoint(spriteTop.Position.X, View.Frame.Height / 2 + checkY);
						spriteBelow.Position = new CGPoint(spriteBelow.Position.X, checkY - View.Frame.Height / 2);
						navLabelTop.Position = new CGPoint(navLabelBottom.Position.X, checkY + 20);
						navLabelBottom.Position = new CGPoint(navLabelTop.Position.X, checkY - 40);
						navSpriteTop.Position = new CGPoint(navSpriteTop.Position.X, checkY + 20);
						navSpriteBottom.Position = new CGPoint(navSpriteBottom.Position.X, checkY - 40);
					}
					else if (checkY > (Frame.Height / 2 + 70))
					{
						MoveToUpFromCenter();
					}
					else if (checkY < Frame.Height / 2 - 70)
					{
						MoveToDownFromCenter();
					}
				}
			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			var touchc = touches.AnyObject as UITouch;
			if (touchc != null)
			{
				// Check if Drag is released in Menu Rect, then Menu move back to start position
				if (actionStarted == true && startDragMenu == true && (lastY > Frame.Height / 2 - 70) && lastY < Frame.Height / 2)
				{
					MoveToCenterFromDown();
				}
				else if (actionStarted == true && startDragMenu == true && (lastY < Frame.Height / 2 + 70) && lastY > Frame.Height / 2)
				{
					MoveToCenterFromUp();
				}

				else if (gameMode == 1 && lastY > Frame.Height - 100 && startDragMenu == true && actionStarted == true)
				{
					MoveToUpFromCenter();
				}
				else if (gameMode == 2 && lastY < 0 + 100 && startDragMenu == true && actionStarted == true)
				{
					MoveToDownFromCenter();
				}

				startDragMenu = false;
				actionStarted = false;

				// Set the dimension values and revert background color range to full color view
				if (gameMode != 0)
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

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}

		// Save Dimensions
		public void SaveProto3Input()
		{
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
			item.Dim1 = Proto3Dim1;
			item.Dim2 = Proto3Dim2;
			item.Dim3 = Proto3Dim3;
			item.PrototypeNr = 3;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
		}
	}
}