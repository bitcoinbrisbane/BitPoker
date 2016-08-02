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
	}
}