
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
	public class ParentNode : SKSpriteNode
	{
		Random rnd = new Random();
		public CGPoint centerOfNode
		{
			get;
			set;
		}
		public CGPoint centerOfNodeFixed
		{
			get;
			set;
		}
		public ParentNode(String name)
		{
			UIImage image = UIImage.FromBundle("spark");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("spark"));
			this.Size = image.Size;

		}
		public void followDrag()
		{
			this.RemoveAllActions();
			float pointSpeed1 = 0.3f;
			float pointSpeed2 = 0.3f;
			int newX = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);

			int newX1 = rnd.Next((int)this.centerOfNode.X - 50, (int)this.centerOfNode.X + 50);
			int newY1 = rnd.Next((int)this.centerOfNode.Y - 50, (int)this.centerOfNode.Y + 50);
			SKAction action1 = SKAction.MoveTo(new CGPoint(newX, newY), pointSpeed1);
			SKAction action2 = SKAction.MoveTo(new CGPoint(newX1, newY1), pointSpeed2);
			var sequence = SKAction.Sequence(action1, action2);
			this.RunAction(SKAction.RepeatActionForever(sequence));

		}
		public void moveAnimation(int disturbFactor,int newX,int newX1,int newX2, int newX3, int newX4, int newY, int newY1 ,int newY2, int newY3, int newY4, double pointSpeed1, double pointSpeed2, double pointSpeed3, double pointSpeed4, double pointSpeed5, bool vibrate )
		{
			this.RemoveAllActions();
			SKAction action1 = SKAction.MoveTo(new CGPoint(newX + disturbFactor, newY + disturbFactor), pointSpeed1);
			SKAction action2 = SKAction.MoveTo(new CGPoint(newX1 - disturbFactor, newY1 + disturbFactor), pointSpeed2);
			SKAction action3 = SKAction.MoveTo(new CGPoint(newX2 + disturbFactor, newY2 - disturbFactor), pointSpeed3);
			SKAction action4 = SKAction.MoveTo(new CGPoint(newX3 + disturbFactor, newY3 + disturbFactor), pointSpeed4);
			SKAction action5 = SKAction.MoveTo(new CGPoint(newX4, newY4), pointSpeed5);
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);
			this.RunAction(SKAction.RepeatActionForever(sequence));

			changeTexture(vibrate,pointSpeed1);
		}

		public void moveAnimation(double speed, bool randomMove, SKAction action1, SKAction action2, SKAction action3, SKAction action4, SKAction action5)
		{
			this.RemoveAllActions();
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);
			this.RunAction(SKAction.RepeatActionForever(sequence));

			changeTexture();
		}
		public void changeTexture(bool disturb,double PointSpeed1)
		{
			//Random rnd = new Random();
			double rotateBy = rnd.NextDouble() * (3 - 2) + 2;
			//this.RunAction(SKAction.FadeOutWithDuration(3));
			//this.RunAction(SKAction.WaitForDuration(2000));
			SKAction action1;
			SKAction action2;
			SKAction action3;
			SKTexture sparkTexture = SKTexture.FromImageNamed(("spark"));
			SKTexture sparkTexture2 = SKTexture.FromImageNamed("spark2");
			SKTexture sparkTexture3 = SKTexture.FromImageNamed("spark3");
			SKTexture sparkTexture4 = SKTexture.FromImageNamed("spark4");

			action1 = SKAction.FadeAlphaBy(-0.2f, 1);
			action2 = SKAction.SetTexture(sparkTexture);
			action3 = SKAction.FadeAlphaBy(+0.2f, 1);


			var sequence = SKAction.Sequence(action1, action2, action3);

			if (disturb == true)
			{
				this.Texture = sparkTexture4;
				this.RunAction(SKAction.RepeatActionForever(SKAction.RotateByAngle(NMath.PI, rotateBy)));

				SKAction changeX1 = SKAction.ScaleXTo(1.1f, PointSpeed1);
				SKAction changeX2 = SKAction.ScaleXTo(0.9f, PointSpeed1);
				var changeSequenceX = SKAction.Sequence(changeX1, changeX2);
				this.RunAction(SKAction.RepeatActionForever(changeSequenceX));

				SKAction changeY1 = SKAction.ScaleYTo(1.1f, PointSpeed1);
				SKAction changeY2 = SKAction.ScaleYTo(0.9f, PointSpeed1);
				var changeSequenceY = SKAction.Sequence(changeY1, changeY2);
				this.RunAction(SKAction.RepeatActionForever(changeSequenceY));
			}
			else 
			{ 
				this.Texture = sparkTexture3;
			}
		}
		public void changeTexture()
		{
			this.Texture = SKTexture.FromImageNamed(("spark3"));
		}
	}
}

