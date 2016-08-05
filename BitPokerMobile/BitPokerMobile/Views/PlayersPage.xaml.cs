using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BitPokerMobile
{
	public partial class PlayersPage : ContentPage
	{
		private ViewModels.PlayersViewModel _viewModel;

		public PlayersPage()
		{
			InitializeComponent();
			_viewModel = new ViewModels.PlayersViewModel();
			BindingContext = _viewModel;

			PlayersList.ItemsSource = _viewModel.Players;
		}
	}
}