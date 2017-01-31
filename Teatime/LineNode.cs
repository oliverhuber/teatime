using System;
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
	public class LineNode : SKSpriteNode
	{
		Random rnd = new Random();
	/*	public CGPoint centerOfNode
		{
			get;
			set;
		}*/
		public bool rndMove
		{
			get;
			set;
		}
		public double speedLocal
		{
			get;
			set;
		}
		public nfloat sizeLocal
		{
			get;
			set;
		}
		public LineNode()
		{
		}
		public LineNode(String name)
		{
			UIImage image = UIImage.FromBundle("line");
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed(("line"));
			this.Size = image.Size;
			rndMove = false;
		}
		public void enableMove(double speedSec, bool rndMove)
		{

			SKAction scaleUp;
			SKAction scaleUpWaveUp;
			SKAction scaleDownWaveUp;
			SKAction scaleUpWaveDown;
			SKAction scaleDownWaveDown;
			SKAction scaleDown;

			//speedUp = speedUp + 1;
			var speed = speedSec;


			this.speedLocal = speedSec;
			this.sizeLocal = 2f;

			/*SKAction moveDown = SKAction.MoveToY(this.Position.Y - 200, speed);
			SKAction moveUp = SKAction.MoveToY(this.Position.Y + 200, speed);

			scaleUp = SKAction.ScaleYTo(2f, speed);
			scaleDown = SKAction.ScaleYTo(0.5f, speed);
			moveSeq = SKAction.Sequence(moveDown, moveUp);
			scaleSeq = SKAction.Sequence(scaleUp,scaleDown);
			this.RunAction(SKAction.RepeatActionForever(scaleSeq));
*/

			scaleUp = SKAction.ScaleYTo(sizeLocal, speedLocal);
			scaleUpWaveUp = SKAction.ScaleYTo(sizeLocal + 0.3f, 0.1);
			scaleUpWaveDown = SKAction.ScaleYTo(sizeLocal, 0.1);

			scaleDown = SKAction.ScaleYTo(0.5f, speedLocal);
			scaleDownWaveDown = SKAction.ScaleYTo(0.5f - 0.3f, 0.1);
			scaleDownWaveUp = SKAction.ScaleYTo(0.5f, 0.1);
			SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleUpWaveUp, scaleUpWaveDown, scaleDown, scaleDownWaveDown, scaleDownWaveUp);
			this.RunAction(SKAction.RepeatActionForever(scaleSeq));


		//	this.RunAction(SKAction.RepeatActionForever(moveSeq));

		}
		public void setUpdateSpeed(bool speedUp, bool sizeUp, bool speedDown, bool sizeDown)
		{


			this.RemoveAllActions();
			SKAction scaleUp;
			SKAction scaleUpWaveUp;
			SKAction scaleDownWaveUp;
			SKAction scaleUpWaveDown;
			SKAction scaleDownWaveDown;
			SKAction scaleDown;
			this.RunAction(SKAction.ScaleYTo(0.5f, 0.1));
			//while (this.HasActions == true)
			{

			}
			if (speedUp == true)
			{
				speedLocal = speedLocal - 0.1f;
			}
			if (speedDown == true)
			{
				speedLocal = speedLocal + 0.1f;

			}
			if (speedLocal < 0.1f)
			{
				speedLocal = 0.1f;
			}
			if (sizeUp == true)
			{
				sizeLocal = sizeLocal + 0.2f;
			}
			if (sizeDown == true)
			{
				sizeLocal = sizeLocal - 0.2f;

			}
			if (sizeLocal < 0.1f)
			{
				sizeLocal = 0.1f;
			}
			scaleUp = SKAction.ScaleYTo(sizeLocal, speedLocal);
			scaleUpWaveUp = SKAction.ScaleYTo(sizeLocal + 0.3f, 0.1);
			scaleUpWaveDown = SKAction.ScaleYTo(sizeLocal, 0.1);

			scaleDown = SKAction.ScaleYTo(0.5f, speedLocal);
			scaleDownWaveDown = SKAction.ScaleYTo(0.5f - 0.3f, 0.1);
			scaleDownWaveUp = SKAction.ScaleYTo(0.5f, 0.1);
			SKAction scaleSeq = SKAction.Sequence(scaleUp, scaleUpWaveUp, scaleUpWaveDown, scaleDown, scaleDownWaveDown, scaleDownWaveUp);
			this.RunAction(SKAction.RepeatActionForever(scaleSeq));


		}
		public void setUpdateSize(bool sizeUp)
		{
			this.RemoveAllActions();
			SKAction scaleUp;
			SKAction scaleDown;
			this.RunAction(SKAction.ScaleYTo(1f, 0.2));
			if (sizeUp == true)
			{
				sizeLocal = sizeLocal + 0.2f;
				scaleUp = SKAction.ScaleYTo(sizeLocal, speedLocal);
				scaleDown = SKAction.ScaleYTo(1f, speedLocal);
			}
			else
			{
				sizeLocal = sizeLocal - 0.2f;
				if (sizeLocal < 0.1f)
				{
					sizeLocal = 0.1f;
				}
				scaleUp = SKAction.ScaleYTo(sizeLocal, speedLocal);
				scaleDown = SKAction.ScaleYTo(1f, speedLocal);
			}
			SKAction scaleSeq = SKAction.Sequence(scaleDown, scaleUp);
			this.RunAction(SKAction.RepeatActionForever(scaleSeq));
		}
	}
}
