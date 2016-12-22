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
	public class GameSceneScene : SKScene
	{
		protected GameSceneScene(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void DidMoveToView(SKView view)
		{
			generateSparks();
		}

		public void followDrag()
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var spark in Children.OfType<SKSpriteNode>())
			{
				//spark.followDrag();

			}
		}


		public void generateSparks()
		{


			bool blackOrWhite = false;
			// Generate 5 nodes per 10 rows
			for (int i = 1; i <= 3; i++)
			{
				for (int y = 1; y <= 7; y++)
				{
					// Calculate location of each Spark (5 Sparks per row, for 10 rows)
					var location = new CGPoint();
					location.X = (((this.View.Frame.Width / 6) * (2 * i)) - (this.View.Frame.Width / 6));
					location.Y = (((this.View.Frame.Height / 14) * (2 * y)) - (this.View.Frame.Height / 14));

					// Define Spark with location and alpha
					var sprite = new SKSpriteNode()
					{
						Position = location,
						XScale = 1,
						YScale = 1,
						Alpha = 0.3f,
						Color = UIColor.FromHSB((nfloat)0, 0, 0)

					};
					// Position in comparsion to other SpriteNodes
					sprite.ZPosition = 1;
					if (blackOrWhite == true)
					{
						sprite.Color = UIColor.FromHSB((nfloat)0, 0, 1);
						blackOrWhite = false;
					}
					else
					{
						sprite.Color = UIColor.FromHSB((nfloat)0, 0, 0);
						blackOrWhite = true;
					}
					sprite.Size = new CGSize(Frame.Width / 3, Frame.Height / 7);
					this.AddChild(sprite);
				}
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}
	}
}
