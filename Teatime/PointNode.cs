using System;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class PointNode : SKSpriteNode
	{
		public PointNode()
		{
		}

		public PointNode(String name)
		{
			UIImage image = UIImage.FromBundle(name);
			this.Name = name;
			this.ZPosition = 12;
			this.Texture = SKTexture.FromImageNamed((name));
			this.Size = image.Size;
		}
		public String Category
		{
			get;
			set;
		}
	}
}
