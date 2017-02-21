using System;
using SpriteKit;
using UIKit;
namespace Teatime
{
	public class LineNode : SKSpriteNode
	{
		public bool RndMove
		{
			get;
			set;
		}

		public double SpeedLocal
		{
			get;
			set;
		}

		public nfloat SizeLocal
		{
			get;
			set;
		}

		public LineNode()
		{
		}

		public LineNode(string name)
		{
			// Generate Line Node
			UIImage image = UIImage.FromBundle("line");
			Name = name;
			ZPosition = 12;
			Texture = SKTexture.FromImageNamed(("line"));
			Size = image.Size;
			RndMove = false;
		}

		public void EnableMove(double speedSec)
		{
			// Set size and speed
			SpeedLocal = speedSec;
			SizeLocal = 2f;

			// Define Line Node Movement
			SKAction scaleUp = SKAction.ScaleYTo(SizeLocal, SpeedLocal);
			SKAction scaleUpWaveUp = SKAction.ScaleYTo(SizeLocal + 0.3f, 0.1);
			SKAction scaleUpWaveDown = SKAction.ScaleYTo(SizeLocal, 0.1);
			SKAction scaleDown = SKAction.ScaleYTo(0.5f, SpeedLocal);
			SKAction scaleDownWaveDown = SKAction.ScaleYTo(0.5f - 0.3f, 0.1);
			SKAction scaleDownWaveUp = SKAction.ScaleYTo(0.5f, 0.1);
			SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleUpWaveUp, scaleUpWaveDown, scaleDown, scaleDownWaveDown, scaleDownWaveUp);

			RunAction(SKAction.RepeatActionForever(scaleSeq));
		}

		// Update Speed Method
		public void SetUpdateSpeed(bool speedUp, bool sizeUp, bool speedDown, bool sizeDown)
		{
			// Update Speed and Size, check if higher or lower and do actions
			RemoveAllActions();

			// One Time move to default size
			RunAction(SKAction.ScaleYTo(0.5f, 0.1));

			if (speedUp == true)
			{
				SpeedLocal = SpeedLocal - 0.1f;
			}
			if (speedDown == true)
			{
				SpeedLocal = SpeedLocal + 0.1f;
			}
			// Min Speed
			if (SpeedLocal < 0.1f)
			{
				SpeedLocal = 0.1f;
			}
			if (sizeUp == true)
			{
				SizeLocal = SizeLocal + 0.2f;
			}
			if (sizeDown == true)
			{
				SizeLocal = SizeLocal - 0.2f;
			}
			// Min Size
			if (SizeLocal < 0.1f)
			{
				SizeLocal = 0.1f;
			}

			// Updated actions
			SKAction scaleUp = SKAction.ScaleYTo(SizeLocal, SpeedLocal);
			SKAction scaleUpWaveUp = SKAction.ScaleYTo(SizeLocal + 0.3f, 0.1);
			SKAction scaleUpWaveDown = SKAction.ScaleYTo(SizeLocal, 0.1);
			SKAction scaleDown = SKAction.ScaleYTo(0.5f, SpeedLocal);
			SKAction scaleDownWaveDown = SKAction.ScaleYTo(0.5f - 0.3f, 0.1);
			SKAction scaleDownWaveUp = SKAction.ScaleYTo(0.5f, 0.1);
			SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleUpWaveUp, scaleUpWaveDown, scaleDown, scaleDownWaveDown, scaleDownWaveUp);
			RunAction(SKAction.RepeatActionForever(scaleSeq));
		}

		// Update Size Method
		public void SetUpdateSize(bool sizeUp)
		{
			RemoveAllActions();
			SKAction scaleUp;
			SKAction scaleDown;

			// One Time move to default size
			RunAction(SKAction.ScaleYTo(1f, 0.2));

			// Check Size higher or lower
			if (sizeUp == true)
			{
				SizeLocal = SizeLocal + 0.2f;
				scaleUp = SKAction.ScaleYTo(SizeLocal, SpeedLocal);
				scaleDown = SKAction.ScaleYTo(1f, SpeedLocal);
			}
			else
			{
				SizeLocal = SizeLocal - 0.2f;
				// Min Size
				if (SizeLocal < 0.1f)
				{
					SizeLocal = 0.1f;
				}
				scaleUp = SKAction.ScaleYTo(SizeLocal, SpeedLocal);
				scaleDown = SKAction.ScaleYTo(1f, SpeedLocal);
			}
			SKAction scaleSeq = SKAction.Sequence(scaleDown, scaleUp);
			RunAction(SKAction.RepeatActionForever(scaleSeq));
		}
	}
}