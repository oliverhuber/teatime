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

		public int ID
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

		public bool SizeUp
		{
			get;
			set;
		}

		public bool SizeDown
		{
			get;
			set;
		}

		public bool SpeedUp
		{
			get;
			set;
		}

		public bool SpeedDown
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

		public void SetUpdatedValues(bool speedUp, bool sizeUp, bool speedDown, bool sizeDown)
		{
			SpeedUp = speedUp;
			SizeUp = sizeUp;
			SpeedDown = speedDown;
			SizeDown = sizeDown;
		}

		public void SetExactUpdatedValues(nfloat speedLocal, nfloat sizeLocal)
		{
			SpeedLocal = speedLocal;
			SizeLocal = sizeLocal;

			// Updated actions
			RemoveAllActions();

			// One Time move to default size
			//RunAction(SKAction.ScaleYTo(0.5f, 0.1));

			SKAction scaleUp = SKAction.ScaleYTo(SizeLocal, SpeedLocal);
			SKAction scaleUpWaveUp = SKAction.ScaleYTo(SizeLocal + 0.3f, 0.1);
			SKAction scaleUpWaveDown = SKAction.ScaleYTo(SizeLocal, 0.1);
			SKAction scaleDown = SKAction.ScaleYTo(0.5f, SpeedLocal);
			SKAction scaleDownWaveDown = SKAction.ScaleYTo(0.5f - 0.3f, 0.1);
			SKAction scaleDownWaveUp = SKAction.ScaleYTo(0.5f, 0.1);
			SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleUpWaveUp, scaleUpWaveDown, scaleDown, scaleDownWaveDown, scaleDownWaveUp);
			RunAction(SKAction.RepeatActionForever(scaleSeq));
		}
	}
}