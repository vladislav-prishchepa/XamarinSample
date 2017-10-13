// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using Xamarin.Forms;
using XamarinSample.UI.ViewModels;

namespace XamarinSample.UI.Views
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_OnAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "ShowTaskInfoMessage", async (s, args) => await DisplayAlert("Task info", args, "Got it!" ));
            await ViewModel.LoadDataAsync();
        }

        private void MainPage_OnDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<MainPageViewModel,string>(this, "ShowTaskInfoMessage");
            ViewModel.Cancel();
        }
    }
}
