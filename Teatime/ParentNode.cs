
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
		public ParentNode(String name)
		{
			UIImage image = UIImage.FromBundle("spark");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("spark"));
			this.Size = image.Size;

		}
		public void moveAnimation(int disturbFactor,int newX,int newX1,int newX2, int newX3, int newX4, int newY, int newY1 ,int newY2, int newY3, int newY4, double pointSpeed1, double pointSpeed2, double pointSpeed3, double pointSpeed4, double pointSpeed5, bool vibrate )
		{

			SKAction action1 = SKAction.MoveTo(new CGPoint(newX + disturbFactor, newY + disturbFactor), pointSpeed1);
			SKAction action2 = SKAction.MoveTo(new CGPoint(newX1 - disturbFactor, newY1 + disturbFactor), pointSpeed2);
			SKAction action3 = SKAction.MoveTo(new CGPoint(newX2 + disturbFactor, newY2 - disturbFactor), pointSpeed3);
			SKAction action4 = SKAction.MoveTo(new CGPoint(newX3 + disturbFactor, newY3 + disturbFactor), pointSpeed4);
			SKAction action5 = SKAction.MoveTo(new CGPoint(newX4, newY4), pointSpeed5);
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);
			this.RunAction(SKAction.RepeatActionForever(sequence));

			changeTexture(vibrate);
		}

		public void moveAnimation(double speed, bool randomMove, SKAction action1, SKAction action2, SKAction action3, SKAction action4, SKAction action5)
		{
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);
			this.RunAction(SKAction.RepeatActionForever(sequence));

			changeTexture();
		}
		public void changeTexture(bool disturb)
		{
			//Random rnd = new Random();
			double rotateBy = rnd.NextDouble() * (2 - 1) + 1;
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

