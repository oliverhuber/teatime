﻿using System;
using CoreGraphics;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class SparkNode : SKSpriteNode
	{
		// Random will be used for calculations
		Random rnd = new Random();

		// Parent Node of this spark node instance
		public ParentNode parentNode
		{
			get;
			set;
		}

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

		// Set up Spark Node
		public SparkNode(String name)
		{
			UIImage image = UIImage.FromBundle("spark3");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("spark3"));
			this.Size = image.Size;

			// Create Physics Body 
			this.PhysicsBody = SKPhysicsBody.Create(this.Texture, this.Size);
			this.PhysicsBody.AffectedByGravity = false;
			this.PhysicsBody.AllowsRotation = true;
			this.PhysicsBody.CollisionBitMask = 0;

			// Calculate Damping
			this.PhysicsBody.LinearDamping = (System.nfloat)(rnd.NextDouble() * (0.5 - 0.2) + 0.2);
			this.PhysicsBody.AngularDamping = (System.nfloat)(rnd.NextDouble() * (2 - 0.2) + 0.2);
			this.PhysicsBody.Mass = 1.06f;

		}

		// Set new Mass for physics 
		public void massOfBody(nfloat mass)
		{
			this.PhysicsBody.Mass = mass;
		}

		// Set Animation of current node
		public void moveAnimation(double speed, bool randomMove, bool disturb, int disturbFactor, bool vibrate)
		{
			double pointSpeed1;
			double pointSpeed2;
			double pointSpeed3;
			double pointSpeed4;
			double pointSpeed5;

			SKAction action1;
			SKAction action2;
			SKAction action3;
			SKAction action4;
			SKAction action5;

			// Remove all actions from that node
			this.RemoveAllActions();

			// Move of the node only 50px away from current center, direction is random
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

			// Set min and max speed for all 5 points
			double minSpeed = speed;
			double maxSpeed = speed + 0.5;

			// If random move then point speed is random, other wise speed is constand for all
			if (randomMove == true)
			{
				pointSpeed1 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
				pointSpeed2 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
				pointSpeed3 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
				pointSpeed4 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
				pointSpeed5 = rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;

			}
			else
			{
				pointSpeed1 = maxSpeed;
				pointSpeed2 = maxSpeed;
				pointSpeed3 = maxSpeed;
				pointSpeed4 = maxSpeed;
				pointSpeed5 = maxSpeed;
			}

			action1 = SKAction.MoveTo(new CGPoint(newX, newY), pointSpeed1);
			action2 = SKAction.MoveTo(new CGPoint(newX1, newY1), pointSpeed2);
			action3 = SKAction.MoveTo(new CGPoint(newX2, newY2), pointSpeed3);
			action4 = SKAction.MoveTo(new CGPoint(newX3, newY3), pointSpeed4);
			action5 = SKAction.MoveTo(new CGPoint(newX4, newY4), pointSpeed5);
			var sequence = SKAction.Sequence(action1, action2, action3, action4, action5);

			// Repeat following the five routes
			this.RunAction(SKAction.RepeatActionForever(sequence));

			// If disturb is true update parent node's move animation with all parms, if false forward simply the SKActions
			if (disturb)
			{
				if (parentNode != null)
				{
					// Set parent nodes center
					this.parentNode.centerOfNode = this.centerOfNode;

					// Update parent node's animation (with all parms)
					parentNode.moveAnimation(disturbFactor, newX, newX1, newX2, newX3, newX4, newY, newY1, newY2, newY3, newY4, pointSpeed1, pointSpeed2, pointSpeed3, pointSpeed4, pointSpeed5, vibrate);
				}
			}
			else
			{
				if (parentNode != null)
				{
					// Set parent nodes center
					this.parentNode.centerOfNode = this.centerOfNode;

					// Update parent node's animation (with SKActions)
					parentNode.moveAnimation(speed, randomMove, sequence);
				}
			}
		}
	}
}