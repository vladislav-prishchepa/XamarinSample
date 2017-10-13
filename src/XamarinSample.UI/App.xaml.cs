// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.Logging.Core;
using ITCC.Logging.Core.Loggers;
using Xamarin.Forms;

namespace XamarinSample.UI
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : Application
    {
        public App()
        {
            Logger.Level = LogLevel.Trace;
            Logger.RegisterReceiver(new DebugLogger(LogLevel.Trace));

            InitializeComponent();
            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
