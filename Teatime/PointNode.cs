using SpriteKit;
using UIKit;

namespace Teatime
{
	public class PointNode : SKSpriteNode
	{
		public PointNode()
		{
		}

		public PointNode(string name)
		{
			UIImage image = UIImage.FromBundle(name);
			Name = name;
			ZPosition = 12;
			Texture = SKTexture.FromImageNamed((name));
			Size = image.Size;
		}
		public string Category
		{
			get;
			set;
		}
	}
}