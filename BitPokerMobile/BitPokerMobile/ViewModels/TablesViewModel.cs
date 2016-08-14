using System;
using System.Collections.ObjectModel;

namespace BitPokerMobile.ViewModels
{
	public class TablesViewModel : BaseViewModel
	{
		public ObservableCollection<Models.ListItemModel> Tables { get; set; }

		public TablesViewModel()
		{
			this.Tables = new ObservableCollection<Models.ListItemModel>();
		}

		public async void GetTablesAsync()
		{
			this.IsBusy = true;

			using (Clients.ITableClient client = new Clients.BitPokerRestClient())
			{
				var tables = await client.GetTablesAsync();

				foreach (PCL.Models.TableInfo table in tables)
				{
					this.Tables.Add(new Models.ListItemModel() { Title = table.Id.ToString() });
				}
			}

			this.IsBusy = false;
		}
	}
}