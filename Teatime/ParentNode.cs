using System;
using CoreGraphics;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class ParentNode : SKSpriteNode
	{
		// Random will be used for calculations
		Random rnd = new Random();

		// Current center of this node
		public CGPoint centerOfNode
		{
			get;
			set;
		}

		// Fixed center
		public CGPoint centerOfNodeFixed
		{
			get;
			set;
		}

		// Set up Parent Node
		public ParentNode(String name)
		{
			UIImage image = UIImage.FromBundle("spark");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("spark"));
			this.Size = image.Size;

			// Create Physics Body 
			this.PhysicsBody = SKPhysicsBody.Create(this.Texture, this.Size);
			this.PhysicsBody.AffectedByGravity = false;
			this.PhysicsBody.AllowsRotation = true;
			this.PhysicsBody.CollisionBitMask = 0;

			// Calculate Damping
			this.PhysicsBody.LinearDamping = 0.2f;
			this.PhysicsBody.AngularDamping = (System.nfloat)(rnd.NextDouble() * (2 - 0.5) + 0.5);
			this.PhysicsBody.Mass = 0.8f;


		}

		// Set new Mass for physics 
		public void massOfBody(nfloat mass)
		{
			this.PhysicsBody.Mass = mass;
		}

		// Set exactly the same animation of related spark node
		public void moveAnimation(double speed, bool randomMove, SKAction sequence)
		{
			// Remove All Actions from that node
			this.RemoveAllActions();
			this.RunAction(SKAction.RepeatActionForever(sequence));

			// Set default texture
			changeTexture();
		}

		// Set Animation of current parent node
		public void moveAnimation(int disturbFactor, int newX, int newX1, int newX2, int newX3, int newX4, int newY, int newY1, int newY2, int newY3, int newY4, double pointSpeed1, double pointSpeed2, double pointSpeed3, double pointSpeed4, double pointSpeed5, bool vibrate)
		{
			// Remove All Actions from that node
			this.RemoveAllActions();

			// Set actions similar to related spark node's actions but add the disturb factor to the movement
			SKAction action1 = SKAction.MoveTo(new CGPoint(newX + disturbFactor, newY + disturbFactor), pointSpeed1);
			SKAction action2 = SKAction.MoveTo(new CGPoint(newX1 - disturbFactor, newY1 + disturbFactor), pointSpeed2);
			SKAction action3 = SKAction.MoveTo(new CGPoint(newX2 + disturbFactor, newY2 - disturbFactor), pointSpeed3);
			SKAction action4 = SKAction.MoveTo(new CGPoint(newX3 + disturbFactor, newY3 + disturbFactor), pointSpeed4);
			SKAction action5 = SKAction.MoveTo(new CGPoint(newX4, newY4), pointSpeed5);
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);

			// Repeat following the five routes
			this.RunAction(SKAction.RepeatActionForever(sequence));

			// Change texture
			changeTexture(vibrate, pointSpeed1);
		}

		// Change Texture to disturbed texture
		public void changeTexture(bool disturb, double PointSpeed1)
		{
			// Default texture
			SKTexture sparkTexture3 = SKTexture.FromImageNamed("spark3");

			// Disturbed texture
			SKTexture sparkTexture4 = SKTexture.FromImageNamed("spark4");

			// Set custom fade and rotation actions
			if (disturb == true)
			{
				// Set disturbed texture
				this.Texture = sparkTexture4;

				// Fade Actions on the node				
				SKAction action1 = SKAction.FadeAlphaBy(-0.2f, 1);
				SKAction action2 = SKAction.FadeAlphaBy(+0.2f, 1);
				SKAction sequence = SKAction.Sequence(action1, action2);

				// Generated random rotation factor
				double rotateBy = rnd.NextDouble() * (2 - 1) + 1;
				this.RunAction(SKAction.RepeatActionForever(SKAction.RotateByAngle(NMath.PI, rotateBy)));

				// Set X Scaling related to PointSpeed1
				SKAction changeX1 = SKAction.ScaleXTo(1.1f, PointSpeed1);
				SKAction changeX2 = SKAction.ScaleXTo(0.9f, PointSpeed1);
				var changeSequenceX = SKAction.Sequence(changeX1, changeX2);
				this.RunAction(SKAction.RepeatActionForever(changeSequenceX));

				// Set Y Scaling related to PointSpeed1
				SKAction changeY1 = SKAction.ScaleYTo(1.1f, PointSpeed1);
				SKAction changeY2 = SKAction.ScaleYTo(0.9f, PointSpeed1);
				var changeSequenceY = SKAction.Sequence(changeY1, changeY2);
				this.RunAction(SKAction.RepeatActionForever(changeSequenceY));
			}

			// Set default texture
			else
			{
				this.Texture = sparkTexture3;
			}
		}

		// Set default texture
		public void changeTexture()
		{
			this.Texture = SKTexture.FromImageNamed(("spark3"));
		}
	}
}