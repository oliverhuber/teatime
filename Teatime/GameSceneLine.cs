using System;
using System.Linq;
using System.Threading;

using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class TimerState
	{
		public int Counter;
		public Timer Tmr;
	}
	public class GameSceneLine : SKScene
	{       
		// Prototype Dimension Mapping 
		public int Proto4Dim1 { get; set; }
		public int Proto4Dim2 { get; set; }
		public int Proto4Dim3 { get; set; }

		// Class declarations of the sprite nodes
		private SKSpriteNode leftUpperSprite;
		private SKSpriteNode rightUpperSprite;
		private SKSpriteNode leftLowerSprite;
		private SKSpriteNode rightLowerSprite;

		private SKLabelNode myLabelSpeedPlus;
		private SKLabelNode myLabelSpeedMinus;
		private SKLabelNode myLabelSizePlus;
		private SKLabelNode myLabelSizeMinus;

		private bool upperSpeed;
		private bool upperSize;
		private bool lowerSpeed;
		private bool lowerSize;

		private int lineCounter;
		private bool firstTouch;

		protected GameSceneLine(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void DidMoveToView(SKView view)
		{
			// Setup Sprite Scene
			Proto4Dim1 = 0;
			Proto4Dim2 = 0;
			Proto4Dim3 = 0;

			firstTouch = false;
			lineCounter = 0;

			// Define four labels and sprite nodes which act as buttons
			// Left Upper Sprite and Label
			leftUpperSprite = new SKSpriteNode();
			leftUpperSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			leftUpperSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			leftUpperSprite.Position = new CGPoint(Frame.Width/4,(Frame.Height/4)*3);
			leftUpperSprite.ZPosition = 3;
			AddChild(leftUpperSprite);

			myLabelSizePlus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "grösser",
				FontSize = 20,
				Position = leftUpperSprite.Position,
				ZPosition = 5
			};
			myLabelSizePlus.Alpha = 0.9f;
			myLabelSizePlus.FontColor = UIColor.FromHSB(0, 0, 0.0f);
			AddChild(myLabelSizePlus);

			// Right Upper Sprite and Label
			rightUpperSprite = new SKSpriteNode();
			rightUpperSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			rightUpperSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			rightUpperSprite.Position = new CGPoint(((Frame.Width / 4) * 3), ((Frame.Height / 4) * 3));
			rightUpperSprite.ZPosition = 3;
			AddChild(rightUpperSprite);

			myLabelSpeedPlus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "schneller",
				FontSize = 20,
				Position = rightUpperSprite.Position,
				ZPosition = 5
			};
			myLabelSpeedPlus.Alpha = 0.9f;
			myLabelSpeedPlus.FontColor = UIColor.FromHSB(0, 0, 0.0f);
			AddChild(myLabelSpeedPlus);

			// Left Lower Sprite and Label
			leftLowerSprite = new SKSpriteNode();
			leftLowerSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			leftLowerSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			leftLowerSprite.Position = new CGPoint((Frame.Width / 4), (Frame.Height / 4));
			leftLowerSprite.ZPosition = 3;
			AddChild(leftLowerSprite);

			myLabelSizeMinus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "kleiner",
				FontSize = 20,
				Position = leftLowerSprite.Position,
				ZPosition = 5
			};
			myLabelSizeMinus.Alpha = 0.9f;
			myLabelSizeMinus.FontColor = UIColor.FromHSB(0, 0, 0.0f);
			AddChild(myLabelSizeMinus);

			// Right Lower Sprite and Label
			rightLowerSprite = new SKSpriteNode();
			rightLowerSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			rightLowerSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			rightLowerSprite.Position = new CGPoint(((Frame.Width / 4) * 3), (Frame.Height / 4));
			rightLowerSprite.ZPosition = 3;
			AddChild(rightLowerSprite);

			myLabelSpeedMinus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "langsamer",
				FontSize = 20,
				Position = rightLowerSprite.Position,
				ZPosition = 5
			};
			myLabelSpeedMinus.Alpha = 0.9f;
			myLabelSpeedMinus.FontColor = UIColor.FromHSB(0, 0, 0.0f);
			AddChild(myLabelSpeedMinus);

			// Set Background of Scene to black
			BackgroundColor = UIColor.FromHSB(0, 0, 0);

			// Timer
			var s = new TimerState();

			// Create the delegate that invokes methods for the timer.
			var timerDelegate = new TimerCallback(CheckStatus);

			// Create a timer that waits 200ms , then invokes 100ms
			var timerDelecate = new Timer(timerDelegate, s, 200, 100);

			// Keep a handle to the timer, so it can be disposed
			s.Tmr = timerDelecate;
		}

		private void GenerateLine()
		{
			lineCounter++;

			// Create LineNodes
			var yourline = new LineNode("line");
			double temp = 0.6 + ((double)1 / 5);
			yourline.Position = new CGPoint(Frame.Width / 20 * lineCounter, Frame.Height / 2 + 20);
			yourline.ZPosition = 5;
			yourline.XScale = 1.5f;

			// Call Move Method of LineNode
			yourline.EnableMove(temp);
			yourline.YScale = 1 + lineCounter / 20;
			AddChild(yourline);

		}

		// Update Method for for the Lines (speed, size)
		private void UpdateLines(bool upperSpeedTemp, bool upperSizeTemp, bool lowerSpeedTemp, bool lowerSizeTemp)
		{

			// Generate new Timer
			var s = new TimerState();

			// Create the delegate that invokes methods for the timer.
			var timerDelegate = new TimerCallback(CheckStatusUpdate);

			// Create a timer that waits 100ms, then invokes 100ms.
			var timerDelecate = new Timer(timerDelegate, s, 100, 100);

			// Keep a handle to the timer, so it can be disposed.
			s.Tmr = timerDelecate;

			// Update size and speed
			upperSize = upperSizeTemp;
			upperSpeed = upperSpeedTemp;
			lowerSize = lowerSizeTemp;
			lowerSpeed = lowerSpeedTemp;
		}

		// Initial timer generates lines
		private void CheckStatus(object state)
		{
			var s = (TimerState)state;
			s.Counter++;

			// Generate new line
			GenerateLine();
			(s.Tmr).Change(100, 100);
			// Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);

			// Dispose timer
			if (s.Counter == 19)
			{
				s.Tmr.Dispose();
				s.Tmr = null;
			}
		}

		// Timer line checking method
		private void CheckStatusUpdate(object state)
		{
			var s = (TimerState)state;
			s.Counter++;
			int a = 0;

			// Do for all LineNodes
			foreach (var lineNode in Children.OfType<LineNode>())
			{
				a++;
				if (s.Counter == a)
				{
					// Update LindeNode
					lineNode.SetUpdateSpeed(upperSpeed, upperSize, lowerSpeed, lowerSize);

					// If last node update dimensions
					if (a == 19)
					{
						Proto4Dim1 = Convert.ToInt32(lineNode.SpeedLocal * 10);
						Proto4Dim2 = Convert.ToInt32(lineNode.SizeLocal * 10);
						Proto4Dim3 = Proto4Dim1 - Proto4Dim2;
					}
				}
			}
			// Dispose timer
			if (s.Counter == 19)
			{
				s.Tmr.Dispose();
				s.Tmr = null;
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				var touchc = touches.AnyObject as UITouch;

				// Check location
				nfloat checkX = touchc.LocationInNode(this).X;
				nfloat checkY = touchc.LocationInNode(this).Y;

				// Get the first touch
				if (firstTouch == false)
				{
					firstTouch = true;
				}

				// Update Lines depending on the location of click, animate button
				if (checkY > (Frame.Height / 2))
				{
					if (checkX > (Frame.Width / 2))
					{
						UpdateLines(true, false, false, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
						SKAction seq = SKAction.Sequence(act1, act2);
						rightUpperSprite.RunAction(seq);
					}
					else 
					{
						UpdateLines(false, true, false, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
						SKAction seq = SKAction.Sequence(act1, act2);
						leftUpperSprite.RunAction(seq);
					}
				}
				else 
				{
					if (checkX > (Frame.Width / 2))
					{
						UpdateLines(false, false, true, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
						SKAction seq = SKAction.Sequence(act1, act2);
						rightLowerSprite.RunAction(seq);
					}
					else 
					{
						UpdateLines(false, false, false, true);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
						SKAction seq = SKAction.Sequence(act1, act2);
						leftLowerSprite.RunAction(seq);
					}
				}
			}
		}
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			// Get the touch
			var touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				nfloat offsetX = (touch.LocationInNode(this).X);
				nfloat offsetY = (touch.LocationInNode(this).Y);
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}

		// Save Dimensions
		public void SaveProto4Input()
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
			item.Dim1 = Proto4Dim1;
			item.Dim2 = Proto4Dim2;
			item.Dim3 = Proto4Dim3;
			item.PrototypeNr = 4;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
		}
	}
}