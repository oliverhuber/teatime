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
	public class SparkNode : SKSpriteNode
	{
		public ParentNode parentNode
		{
			get;
			set;
		}
		Random rnd = new Random();
		public CGPoint centerOfNode {
			get;
			set;
		}
		public CGPoint centerOfNodeFixed
		{
			get;
			set;
		}
		public SparkNode(String name)
		{
			UIImage image = UIImage.FromBundle("spark");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("spark"));
			this.Size = image.Size;
			this.PhysicsBody = SKPhysicsBody.Create(this.Texture, this.Size);
			this.PhysicsBody.AffectedByGravity = false;
			this.PhysicsBody.AllowsRotation = true;
			this.PhysicsBody.CollisionBitMask = 0;
			this.PhysicsBody.LinearDamping = (System.nfloat)(rnd.NextDouble() * (1 - 0.2) + 0.2);
			this.PhysicsBody.AngularDamping = (System.nfloat)(rnd.NextDouble() * (1 - 0.2) + 0.2);
			this.PhysicsBody.Mass = 0.06f;

		}
		public void followDrag() {
			this.RemoveAllActions();
			float pointSpeed1 = 0.5f;
			float pointSpeed2 = 0.5f;
			int newX = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX1 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY1 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

		//	this.PhysicsBody.Velocity = new CGVector(newX, newY);
			SKAction action1 = SKAction.MoveTo(new CGPoint(newX, newY), pointSpeed1);
			SKAction action2 = SKAction.MoveTo(new CGPoint(newX1, newY1), pointSpeed2);
			var sequence = SKAction.Sequence(action1, action2);
			//this.RunAction(SKAction.RepeatActionForever(sequence));
			if (this.Position.X > newX && this.Position.Y > newY )
			{
				this.PhysicsBody.Velocity = new CGVector(-newX, -newY);
			} 
			else if (this.Position.X  > newX && this.Position.Y < newY  )
			{
				this.PhysicsBody.Velocity = new CGVector(-newX, newY);
			} 
			else if(this.Position.X  < newX && this.Position.Y > newY)
			{
				this.PhysicsBody.Velocity = new CGVector(newX, -newY);
			}
			else {
				this.PhysicsBody.Velocity = new CGVector(newX, newY);

			}

			this.parentNode.followDrag();
		}

		public void moveAnimation(double speed, bool randomMove, bool disturb, int disturbFactor, bool vibrate) { 
			this.RemoveAllActions();

			// this.RunAction(SKAction.ScaleTo(1f, 10));
			// SKAction[] actionArray;
			/*	for (int i = 0; i <= 4; i++)
				{
					int newX = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
					int newY = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);
					actionArray[i] = SKAction.MoveTo(new CGPoint(newX, newY), 1);
				}
			*/

			int newX = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX1 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY1 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX2 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY2 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX3 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY3 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX4 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY4 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);


			double minSpeed = speed;
			double maxSpeed = speed + 1;

			double pointSpeed1 = maxSpeed;
			double pointSpeed2 = maxSpeed;
			double pointSpeed3 = maxSpeed;
			double pointSpeed4 = maxSpeed;
			double pointSpeed5 = maxSpeed;

			SKAction action1;
			SKAction action2;
			SKAction action3;
			SKAction action4;
			SKAction action5;


			if (randomMove == true)
			{
				pointSpeed1 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                 pointSpeed2 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                 pointSpeed3 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                 pointSpeed4 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                 pointSpeed5 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;


				 action1 = SKAction.MoveTo(new CGPoint(newX, newY),pointSpeed1);
				 action2 = SKAction.MoveTo(new CGPoint(newX1, newY1), pointSpeed2);
				 action3 = SKAction.MoveTo(new CGPoint(newX2, newY2),pointSpeed3);
				 action4 = SKAction.MoveTo(new CGPoint(newX3, newY3), pointSpeed4);
                 action5 = SKAction.MoveTo(new CGPoint(newX4, newY4),pointSpeed5);

			}
			else {
				 action1 = SKAction.MoveTo(new CGPoint(newX, newY),  pointSpeed1);
				 action2 = SKAction.MoveTo(new CGPoint(newX1, newY1), pointSpeed2);
				 action3 = SKAction.MoveTo(new CGPoint(newX2, newY2), pointSpeed3);
				 action4 = SKAction.MoveTo(new CGPoint(newX3, newY3), pointSpeed4);
				 action5 = SKAction.MoveTo(new CGPoint(newX4, newY4), pointSpeed5);
			}


			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);
			this.RunAction(SKAction.RepeatActionForever(sequence));

			//this.RunAction(SKAction.RepeatActionForever(SKAction.RotateByAngle(NMath.PI*rnd.Next(1,6) , 2.0)));

			this.changeTexture();

			if (disturb)
			{
				if (parentNode != null)
				{
					this.parentNode.centerOfNode = this.centerOfNode;
					parentNode.moveAnimation(disturbFactor, newX, newX1, newX2, newX3, newX4, newY, newY1, newY2, newY3, newY4, pointSpeed1, pointSpeed2, pointSpeed3, pointSpeed4, pointSpeed5, vibrate);
				}
			}
			else {

				if (parentNode != null)
				{
					this.parentNode.centerOfNode = this.centerOfNode;
					parentNode.moveAnimation(speed, randomMove, action1, action2, action3, action4, action5);
				}
			}
		}

		public void changeTexture()
		{
			//this.RunAction(SKAction.FadeOutWithDuration(3));
			//this.RunAction(SKAction.WaitForDuration(2000));
		/*	SKAction action1;
			SKAction action2;
			SKAction action3;
			SKTexture sparkTexture = SKTexture.FromImageNamed(("spark"));
			SKTexture sparkTexture2 = SKTexture.FromImageNamed("spark2");
			action1 = SKAction.FadeAlphaBy(-0.2f, 1);
			action2 = SKAction.SetTexture(sparkTexture);
			action3 = SKAction.FadeAlphaBy(+0.2f, 1);


			var sequence = SKAction.Sequence(action1, action2, action3);
		///	this.RunAction(SKAction.RepeatActionForever(sequence));
			//if (this.Texture == SKTexture.FromImageNamed(("spark2")))
			//{
			//	this.Texture = SKTexture.FromImageNamed(("spark3"));

			//}
			//else {
			*/
				this.Texture = SKTexture.FromImageNamed(("spark3"));
		//	}
			parentNode.changeTexture();
			/*
			var path = new CGPath();
			path.AddLines(new CGPoint[]{
						new CGPoint (secSprite.Position.X, secSprite.Position.Y),
						new CGPoint (offsetX, this.View.Frame.Height-offsetY),
					});
			path.CloseSubpath();

			yourline.Path = path;
			//this.RunAction(SKAction.FadeInWithDuration(3));
*/
		}
	}
}
