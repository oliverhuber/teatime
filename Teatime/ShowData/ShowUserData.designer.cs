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
    [Register ("ShowUserData")]
    partial class ShowUserData
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton unwindUserData { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView userData { get; set; }

        [Action ("ResetButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ResetButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (unwindUserData != null) {
                unwindUserData.Dispose ();
                unwindUserData = null;
            }

            if (userData != null) {
                userData.Dispose ();
                userData = null;
            }
        }
    }
}