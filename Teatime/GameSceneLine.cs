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
	public class TimerState
	{
		public int counter = 0;
		public Timer tmr;
	}
	public class GameSceneLine : SKScene
	{
		public int Proto4Dim1 { get; set; }
		public int Proto4Dim2 { get; set; }
		public int Proto4Dim3 { get; set; }
		// Class declarations of the sprite nodes
		SKSpriteNode leftUpperSprite;
		SKSpriteNode rightUpperSprite;
		SKSpriteNode leftLowerSprite;
		SKSpriteNode rightLowerSprite;
		SKLabelNode myLabelSpeedPlus;
		SKLabelNode myLabelSpeedMinus;
		SKLabelNode myLabelSizePlus;
		SKLabelNode myLabelSizeMinus;


		//LineNode yourline;
		SKLabelNode myLabel;
		SKEmitterNode fireEmitter;
		FieldNode backGroundNode;
		int lineCounter = 0;
		int updateCounter = 0;
		bool upperSpeed;
		bool upperSize;
		bool lowerSpeed;
		bool lowerSize;
		Timer timer;
		bool firstTouch;
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
			// New Label placed in the middle of the Screen
			myLabel = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Guten Tag, wie gehts dir heute??",
				FontSize = 20,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 100)
			};
			myLabel.Alpha = 0.9f;
			//this.Size = View.Bounds.Size;

			// Background gradient sprite node
			//	var container = new SKSpriteNode("background");
			//	container.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);

			var locationTop = new CGPoint();
			locationTop.X = Frame.Width;//(this.View.Frame.Width );
			locationTop.Y = Frame.Height;//(this.View.Frame.Height);
			backGroundNode = new FieldNode()
			{
				Position = locationTop,
				Name = "Top",
				XScale = 1,
				YScale = 1,
				Alpha = 1f,
				Color = UIColor.FromHSB((nfloat)0, 0, 0.2f)

			};

			leftUpperSprite = new FieldNode();
			leftUpperSprite.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			leftUpperSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			var leftUpperPoint = new CGPoint();
			leftUpperPoint.X =  (Frame.Width / 4);
			leftUpperPoint.Y =  ((Frame.Height / 4)*3);
			leftUpperSprite.Position = leftUpperPoint;		         
			AddChild(leftUpperSprite);

			myLabelSizePlus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "grösser",
				FontSize = 20,
				Position = leftUpperPoint,
				ZPosition = 5
			};
			myLabelSizePlus.Alpha = 0.9f;
			myLabelSizePlus.FontColor = UIColor.FromHSB((nfloat)0, 0, 0.0f);

			AddChild(myLabelSizePlus);

			rightUpperSprite = new FieldNode();
			rightUpperSprite.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			rightUpperSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			rightUpperSprite.Position = new CGPoint(((Frame.Width / 4)*3), ((Frame.Height / 4)*3));
			AddChild(rightUpperSprite);

			myLabelSpeedPlus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "schneller",
				FontSize = 20,
				Position = rightUpperSprite.Position,
				ZPosition = 5
			};
			myLabelSpeedPlus.Alpha = 0.9f;
			myLabelSpeedPlus.FontColor=UIColor.FromHSB((nfloat)0, 0, 0.0f);
			AddChild(myLabelSpeedPlus);

			leftLowerSprite = new FieldNode();
			leftLowerSprite.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			leftLowerSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			leftLowerSprite.Position = new CGPoint((Frame.Width / 4), (Frame.Height / 4));
			AddChild(leftLowerSprite);

			myLabelSizeMinus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "kleiner",
				FontSize = 20,
				Position = leftLowerSprite.Position,
				ZPosition = 5
			};
			myLabelSizeMinus.Alpha = 0.9f;
			myLabelSizeMinus.FontColor = UIColor.FromHSB((nfloat)0, 0, 0.0f);

			AddChild(myLabelSizeMinus);

			rightLowerSprite = new FieldNode();
			rightLowerSprite.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			rightLowerSprite.Size = new CGSize(Frame.Width / 2, Frame.Height / 2);
			rightLowerSprite.Position = new CGPoint(((Frame.Width /4 )*3), (Frame.Height / 4));
			AddChild(rightLowerSprite);

			myLabelSpeedMinus = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "langsamer",
				FontSize = 20,
				Position = rightLowerSprite.Position,
				ZPosition = 5
			};
			myLabelSpeedMinus.Alpha = 0.9f;
			myLabelSpeedMinus.FontColor = UIColor.FromHSB((nfloat)0, 0, 0.0f);

			AddChild(myLabelSpeedMinus);

			rightUpperSprite.ZPosition = 3;
			rightLowerSprite.ZPosition = 3;
			leftLowerSprite.ZPosition = 3;
			leftUpperSprite.ZPosition = 3;

			backGroundNode.ZPosition = 1;
			backGroundNode.Size = new CGSize(Frame.Width / 1, Frame.Height / 1);
			//AddChild(backGroundNode);

			//this.Size = new CGSize(1200, 200);
			this.BackgroundColor = UIColor.FromHSB(0, 0, 0);

			firstTouch = false;
			// Create Path for the line, between both sprites




			TimerState s = new TimerState();
			// Create the delegate that invokes methods for the timer.
			TimerCallback timerDelegate = new TimerCallback(CheckStatus);
			// Create a timer that waits one second, then invokes every second.
			Timer timerDelecate = new Timer(timerDelegate, s, 200, 100);
			// Keep a handle to the timer, so it can be disposed.
			s.tmr = timerDelecate;
			// The main thread does nothing until the timer is disposed.
			//while (s.tmr != null)
			//	Thread.Sleep(0);
		
		// The following method is called by the timer's delegate.




			myLabel.ZPosition = 1;
			//container.ZPosition = 0;


		

		}
		public void saveProto4Input()
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
			item.Dim1 = Proto4Dim1;
			item.Dim2 = Proto4Dim2;
			item.Dim3 = Proto4Dim3;
			item.PrototypeNr = 4;
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

		 void CheckStatus(Object state)
		{
			TimerState s = (TimerState)state;
			s.counter++;
			generateLine();
			(s.tmr).Change(100, 100);
			Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);
			if (s.counter == 19)
			{
				//	Console.WriteLine("disposing of timer...");
				s.tmr.Dispose();
				s.tmr = null;
			}
		}
		
		public void generateLine()
		{
			lineCounter++;


			LineNode yourline = new LineNode("line");

			double temp = 0.6 + ((double)1 / 5);
			yourline.Position = new CGPoint(Frame.Width / 20 * lineCounter, Frame.Height / 2 + 20);
			yourline.ZPosition = 5;
			yourline.XScale = 1.5f;
			AddChild(yourline);
			yourline.enableMove(temp, false);
			yourline.YScale = 1 + lineCounter / 20;

		}
		void CheckStatusUpdate(Object state)
		{
			TimerState s = (TimerState)state;
			s.counter++;
			//generateLine();
			int a = 0;
			foreach (var lineNode in Children.OfType<LineNode>())
			{
				a++;
				if (s.counter == a)
				{
					//lineNode.setUpdateSize(upperSize);
					lineNode.setUpdateSpeed(upperSpeed,upperSize,lowerSpeed,lowerSize);
					if (a == 19)
					{
						Proto4Dim1 =  Convert.ToInt32(lineNode.speedLocal * 10) ;
						Proto4Dim2 =  Convert.ToInt32(lineNode.sizeLocal * 10);
						Proto4Dim3 = Proto4Dim1 - Proto4Dim2;
					}
				}
			}
			//(s.tmr).Change(100, 100);
			//Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);
			if (s.counter == 19)
			{
				
				//	Console.WriteLine("disposing of timer...");
				s.tmr.Dispose();
				s.tmr = null;
			}
		}
		public void updateLines(bool upperSpeedTemp, bool upperSizeTemp, bool lowerSpeedTemp, bool lowerSizeTemp)
		{

			TimerState s = new TimerState();
			// Create the delegate that invokes methods for the timer.
			TimerCallback timerDelegate = new TimerCallback(CheckStatusUpdate);
			// Create a timer that waits one second, then invokes every second.
			Timer timerDelecate = new Timer(timerDelegate, s, 100, 100);
			// Keep a handle to the timer, so it can be disposed.
			s.tmr = timerDelecate;
			upperSize = upperSizeTemp;
			upperSpeed = upperSpeedTemp;
			lowerSize = lowerSizeTemp;
			lowerSpeed = lowerSpeedTemp;
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
				//updateEmitter(offsetX,offsetY);
				//fireEmitter.Position = location;

				/*if (oneSprite.Frame.Contains(((UITouch)touch).LocationInNode(this)))
				{
					//float offsetX = (float)(touch.LocationInView(View).X);
					//float offsetY = (float)(touch.LocationInView(View).Y);
				//	oneSprite.ScaleTo(new CGSize(oneSprite.Size.Width + (offsetX / 1000), oneSprite.Size.Height + (offsetY / 1000)));


				//	oneSprite.Position = location;
					//myLabel.Text = offsetX + " " + offsetY;
					//fireEmitter.Position = location;
				
				/*	var path = new CGPath();
					path.AddLines(new CGPoint[]{
						new CGPoint (secSprite.Position.X, secSprite.Position.Y),
						new CGPoint (offsetX, this.View.Frame.Height-offsetY),
					});
					path.CloseSubpath();

					yourline.Path = path;

				}

			*/}
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
			//	fireEmitter.Position = locationc;
			//	updateEmitter(checkX,checkY);
				//*******
				if (firstTouch == false)
				{
					//AddChild(oneSprite);
					//AddChild(fireEmitter);
					firstTouch = true;
				}
			

				UIColor coloring;
				var speed = 0;
				if (checkY > (Frame.Height / 2))
				{
					if (checkX > (Frame.Width / 2)) {
						updateLines(true, false, false, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 0.5);
						SKAction seq = SKAction.Sequence(act1, act2);
						rightUpperSprite.RunAction(seq);
					}
					else {
						updateLines(false, true, false, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 0.5);
						SKAction seq = SKAction.Sequence(act1, act2);
						leftUpperSprite.RunAction(seq);
					}
				}


				else {
					if (checkX > (Frame.Width / 2)) {
						updateLines(false, false, true, false);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 0.5);
						SKAction seq = SKAction.Sequence(act1, act2);
						rightLowerSprite.RunAction(seq);
					}
					else {
						updateLines(false, false, false, true);
						SKAction act1 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 1f), 1f, 0);
						SKAction act2 = SKAction.ColorizeWithColor(UIColor.FromHSB(0, 0, 0f), 0f, 0.5);
						SKAction seq = SKAction.Sequence(act1, act2);
						leftLowerSprite.RunAction(seq);
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
