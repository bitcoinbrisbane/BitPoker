using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace BitPokerMobile.ViewModels
{
	public class PlayersViewModel : BaseViewModel
	{
		public ObservableCollection<Models.ListItemModel> Players { get; set; }

		public ICommand RefreshKeyTapped { protected set; get; }

		public PlayersViewModel()
		{
			this.RefreshKeyTapped = new Command(GetPlayersAsync);
			this.Players = new ObservableCollection<Models.ListItemModel>();
		}

		public async void GetPlayersAsync()
		{
			this.IsBusy = true;

			using (Clients.IPlayerClient client = new Clients.BitPokerRestClient())
			{
				var players = await client.GetPlayersAsync();

				foreach (PCL.Models.PlayerInfo player in players)
				{
					this.Players.Add(new Models.ListItemModel() { Title = player.BitcoinAddress, Description = player.UserAgent });
				}
			}

			this.IsBusy = false;
		}
	}
}