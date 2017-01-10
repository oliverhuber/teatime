
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
	public class FieldNode : SKSpriteNode
	{
		public CGPoint centerOfNode
		{
			get;
			set;
		}
		public FieldNode()
		{
		}
	}
}
