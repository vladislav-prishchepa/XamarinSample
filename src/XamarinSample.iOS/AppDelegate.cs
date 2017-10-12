// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Foundation;
using UIKit;

namespace XamarinSample.iOS
{
    // ReSharper disable once PartialTypeWithSinglePart
    // ReSharper disable once UnusedMember.Global
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            LoadApplication(new UI.App());

            return base.FinishedLaunching(app, options);
        }
    }
}
