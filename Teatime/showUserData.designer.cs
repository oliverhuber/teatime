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
    [Register ("showUserData")]
    partial class showUserData
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton unwindUserData { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView userData { get; set; }

        void ReleaseDesignerOutlets ()
        {
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