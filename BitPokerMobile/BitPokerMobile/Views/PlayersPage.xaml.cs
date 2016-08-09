using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

		//private void OnRefresh(object sender, EventArgs e)
		//{
		//	var list = (ListView)sender;

		//	//put your refreshing logic here
		//	_viewModel.GetPlayersAsync();

		//	//make sure to end the refresh state
		//	list.IsRefreshing = false;
		//}

		//void OnTap(object sender, ItemTappedEventArgs e)
		//{
		//	DisplayAlert("Item Tapped", e.Item.ToString(), "Ok");
		//}

		private void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}
			DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");

			//comment out if you want to keep selections
			ListView lst = (ListView)sender;
			lst.SelectedItem = null;
		}
	}
}