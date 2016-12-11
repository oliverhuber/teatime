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
    [Register ("GameSubViewSpriteController")]
    partial class GameSubViewSpriteController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UnwindProtoSprite { get; set; }

        [Action ("UnwindProtoSprite_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UnwindProtoSprite_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (UnwindProtoSprite != null) {
                UnwindProtoSprite.Dispose ();
                UnwindProtoSprite = null;
            }
        }
    }
}