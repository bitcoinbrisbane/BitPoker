using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BitPokerMobile
{
	public partial class HomePage : ContentPage
	{
		private ViewModels.HomeViewModel _viewModel;

		public HomePage()
		{
			InitializeComponent();
			_viewModel = new ViewModels.HomeViewModel();
			BindingContext = _viewModel;

			PlayersList.ItemsSource = _viewModel.Players;
		}

		protected override void OnAppearing()
		{
			MessagingCenter.Subscribe<ViewModels.HomeViewModel, string>(this, "Hi", (sender, arg) =>
			{
				DisplayAlert("Message Received", "arg=" + arg, "OK");
			});

			MessagingCenter.Subscribe<ViewModels.HomeViewModel>(this, "Hi", (sender) =>
			{
				// do something whenever the "Hi" message is sent
				DisplayAlert("Success", _viewModel.BitcoinAddress, "Ok");
			});

			base.OnAppearing();
		}
	}
}