using UIKit;
using CoreGraphics;
using Foundation;

namespace Teatime
{
	public class CustomCell : UITableViewCell
	{
		UILabel subheadingLabel;

		// Define Custom Cell 
		public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			subheadingLabel = new UILabel()
			{
				Alpha = 1f,
				Font = UIFont.FromName("AppleSDGothicNeo-Light", 12f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.FromRGB(78, 91, 216)
			};
			ContentView.AddSubviews(new UIView[] {  subheadingLabel });
		}

		// Update Cell
		public void UpdateCell(string subtitle)
		{
			subheadingLabel.Text = "       > "+subtitle;
		}

		// Layout
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			subheadingLabel.Frame = new CGRect( 0, 0, ContentView.Bounds.Width , 20);
		}
	}
}