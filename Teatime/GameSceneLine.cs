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
		// Class declarations of the sprite nodes
		SKSpriteNode oneSprite;
		SKSpriteNode secSprite;
		//LineNode yourline;
		SKLabelNode myLabel;
		SKEmitterNode fireEmitter;
		FieldNode backGroundNode;
		int lineCounter = 0;
		int updateCounter = 0;
		bool upperSpeed;
		bool upperSize;
		Timer timer;
		bool firstTouch;
		protected GameSceneLine(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			// Setup Sprite Scene

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
			yourline.ZPosition = 2;
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
					lineNode.setUpdateSpeed(upperSpeed,upperSize);

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
		public void updateLines(bool upperSpeedTemp,bool upperSizeTemp)
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
					speed = 1;
					coloring = UIColor.Green;
					myLabel.Text = "Glücklich";
					myLabel.Position = new CGPoint(Frame.Width / 2, (7 * (Frame.Height / 8)));
					updateLines(true, true);
				}


				else {
					speed = 4;
					coloring = UIColor.Red;
					myLabel.Text = "Nervös";
					myLabel.Position = new CGPoint(Frame.Width / 2, (1 * (Frame.Height / 8)));
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
