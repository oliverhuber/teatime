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
    [Register ("GameSubViewSceneController")]
    partial class GameSubViewSceneController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UnwindProto4 { get; set; }

        [Action ("UnwindProto4_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UnwindProto4_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (UnwindProto4 != null) {
                UnwindProto4.Dispose ();
                UnwindProto4 = null;
            }
        }
    }
}