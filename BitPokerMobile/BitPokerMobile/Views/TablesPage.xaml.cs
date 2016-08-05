using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BitPokerMobile
{
	public partial class TablesPage : ContentPage
	{
		private ViewModels.TablesViewModel _viewModel;

		public TablesPage()
		{
			InitializeComponent();

			_viewModel = new ViewModels.TablesViewModel();
			BindingContext = _viewModel;

			TablesList.ItemsSource = _viewModel.Tables;
		}
	}
}