// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Teatime
{
    [Register ("GameViewController")]
    partial class GameViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton gotoNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton gotoNext2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton gotoNext4 { get; set; }

        [Action ("GotoNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GotoNext_TouchUpInside (UIKit.UIButton sender);

        [Action ("GotoNext2_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GotoNext2_TouchUpInside (UIKit.UIButton sender);

        [Action ("GotoNext4_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GotoNext4_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (gotoNext != null) {
                gotoNext.Dispose ();
                gotoNext = null;
            }

            if (gotoNext2 != null) {
                gotoNext2.Dispose ();
                gotoNext2 = null;
            }

            if (gotoNext4 != null) {
                gotoNext4.Dispose ();
                gotoNext4 = null;
            }
        }
    }
}