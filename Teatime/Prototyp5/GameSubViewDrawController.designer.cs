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
    [Register ("GameSubViewDrawController")]
    partial class GameSubViewDrawController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UnwindProto6 { get; set; }

        [Action ("UnwindProto6_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UnwindProto6_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (UnwindProto6 != null) {
                UnwindProto6.Dispose ();
                UnwindProto6 = null;
            }
        }
    }
}