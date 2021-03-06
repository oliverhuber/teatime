﻿using System;
using System.Linq;
using System.Timers;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
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

		private bool nextTouch;
		private bool waitWithInteraction;
		private int lineCounter;
		private int lineCounterUpdate;

		private Timer aTimer;
		private Timer bTimer;
		private int aTimerCounter;
		private int bTimerCounter;
		private LineNode[] lineNodes = new LineNode[19];

		private nfloat speedVal;
		private nfloat sizeVal;

		protected GameSceneLine(IntPtr handle) : base(handle)
		{
		}

		public GameSceneLine()
		{
		}

		public override void DidMoveToView(SKView view)
		{
			// Setup Sprite Scene
			Proto4Dim1 = 0;
			Proto4Dim2 = 0;
			Proto4Dim3 = 0;

			waitWithInteraction = true;
			nextTouch = false;
			lineCounter = 0;
			lineCounterUpdate = 0;

			// Define four labels and sprite nodes which act as buttons
			// Left Upper Sprite and Label
			leftUpperSprite = new SKSpriteNode();
			leftUpperSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			leftUpperSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			leftUpperSprite.Position = new CGPoint(Frame.Width / 4, (Frame.Height / 4) * 3);
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

			aTimer = new Timer();
			aTimer.Elapsed += OnTimedEvent;
			aTimer.Interval = 140;
			aTimer.Enabled = true;

			aTimerCounter = 0;
			bTimerCounter = 0;

			// Default Value
			sizeVal = 1.5f;
			speedVal = 0.8f;

		}
		// Initial timer generates lines
		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			aTimerCounter++;

			// Generate new line
			GenerateLine();

		}
		private void OnTimedEventUpdate(object source, ElapsedEventArgs e)
		{
			bTimerCounter++;

			// Update Lines
			UpdateLine();
		}

		private void GenerateLine()
		{
			lineCounter++;

			// Create LineNodes
			var yourline = new LineNode("line");
			yourline.Position = new CGPoint(Frame.Width / 20 * lineCounter, Frame.Height / 2 + 20);
			yourline.ZPosition = 12;
			yourline.XScale = 1.3f;
			yourline.ID = lineCounter;

			// Call Move Method of LineNode
			//yourline.EnableMove(temp);
			yourline.SetExactUpdatedValues(speedVal, sizeVal);
			yourline.YScale = 0.5f;
			//yourline.YScale = 1 + lineCounter / 20;
			lineNodes[lineCounter] = yourline;
			AddChild(yourline);

			// Spot Generate Line Timer and Start Update Timer
			if (lineCounter == 18)
			{
				waitWithInteraction = true;
				aTimer.Stop();
				aTimer.Dispose();

				// New Update timer
				bTimer = new Timer();
				bTimer.Elapsed += new ElapsedEventHandler(OnTimedEventUpdate);
				bTimer.Interval = 140;
				bTimer.Enabled = true;

				Proto4Dim1 = (int)(speedVal * 10);
				Proto4Dim2 = (int)(sizeVal * 10);
				Proto4Dim3 = Proto4Dim1 - Proto4Dim2;
			}
		}
		private void UpdateLine()
		{
			lineCounterUpdate++;

			if (lineCounterUpdate == 19)
			{
				Proto4Dim1 = (int)(speedVal * 10);
				Proto4Dim2 = (int)(sizeVal * 10);
				Proto4Dim3 = Proto4Dim1 - Proto4Dim2;
			}

			if (lineCounterUpdate >= 10000000)
			{
				lineCounterUpdate = 0;
			}

			// Do for all LineNodes
			foreach (var lineNode in Children.OfType<LineNode>())
			{
				if (lineCounterUpdate == lineNode.ID && lineCounterUpdate < 19)
				{
					// Update LindeNode
					lineNode.SetExactUpdatedValues(speedVal, sizeVal);
				}
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
				if (nextTouch == false)
				{
					nextTouch = true;
				}

				// Wait with interaction until first cycle is done
				if (waitWithInteraction)
				{

					// Update Lines depending on the location of click, animate button
					if (checkY > (Frame.Height / 2))
					{
						if (checkX > (Frame.Width / 2))
						{
							speedVal = speedVal - 0.1f;
							SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
							SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
							SKAction seq = SKAction.Sequence(act1, act2);
							rightUpperSprite.RunAction(seq);
						}
						else
						{
							sizeVal = sizeVal + 0.1f;
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
							speedVal = speedVal + 0.1f;
							SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
							SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
							SKAction seq = SKAction.Sequence(act1, act2);
							rightLowerSprite.RunAction(seq);
						}
						else
						{
							sizeVal = sizeVal - 0.1f;
							SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
							SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 1);
							SKAction seq = SKAction.Sequence(act1, act2);
							leftLowerSprite.RunAction(seq);
						}
					}
					// Min Speed
					if (speedVal < 0.1f)
					{
						speedVal = 0.1f;
					}
					// Max Speed 
					if (speedVal > 2.0f)
					{
						speedVal = 2.0f;
					}

					// Max Size
					if (sizeVal > 8.0f)
					{
						sizeVal = 8.0f;
					}
					// Min Size
					if (sizeVal < 0.1f)
					{
						sizeVal = 0.1f;
					}

					lineCounterUpdate = 0;
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