using UIKit;
using System;
using CoreGraphics;
using Foundation;

namespace Teatime

{

	public class CustomCell : UITableViewCell
	{
		UILabel headingLabel, subheadingLabel;
		UIImageView imageView;
		public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			//ContentView.BackgroundColor = UIColor.FromRGB(218, 255, 127);
			//imageView = new UIImageView();
		/*	headingLabel = new UILabel()
			{
				Font = UIFont.FromName("AppleSDGothicNeo-UltraLight", 10f),
				//TextColor = UIColor.FromRGB(127, 51, 0),
				BackgroundColor = UIColor.Clear
			};*/
			subheadingLabel = new UILabel()
			{
				Alpha = 0.5f,
				Font = UIFont.FromName("AppleSDGothicNeo-Light", 12f),
				TextColor = UIColor.White,
				//TextColor = UIColor.FromRGB(38, 127, 0),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Purple
				                         

			};
			//subheadingLabel.
			ContentView.AddSubviews(new UIView[] {  subheadingLabel });

				//,imageView });

		}
		public void UpdateCell(string caption, string subtitle) //,UIImage image)
		{
			//imageView.Image = image;
		//	headingLabel.Text = caption;
			subheadingLabel.Text = "       > "+subtitle;
		}
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			//imageView.Frame = new CGRect(ContentView.Bounds.Width - 63, 5, 33, 33);
		//	headingLabel.Frame = new CGRect(5, 4, ContentView.Bounds.Width - 63, 25);
			subheadingLabel.Frame = new CGRect( 0, 0, ContentView.Bounds.Width , 20);
		}
	}

}
