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
    [Register ("GameSubViewLineController")]
    partial class GameSubViewLineController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UnwindProto5 { get; set; }

        [Action ("UnwindProto5_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UnwindProto5_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (UnwindProto5 != null) {
                UnwindProto5.Dispose ();
                UnwindProto5 = null;
            }
        }
    }
}