using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace BitPokerMobile.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private String _bitcoinAddress;

		public String BitcoinAddress
		{
			get
			{
				return _bitcoinAddress;
			}
			set
			{
				if (_bitcoinAddress != value)
				{
					_bitcoinAddress = value;
					OnPropertyChanged("BitcoinAddress");
				}
			}
		}

		public ObservableCollection<Models.ListItemModel> Players { get; set; }

		public ICommand GenerateKeyTapped { protected set; get; }

		public ICommand RefreshKeyTapped { protected set; get; }

		public HomeViewModel()
		{
			this.GenerateKeyTapped = new Command(OnGenerateKeyTappedAsync);
			this.RefreshKeyTapped = new Command(GetPlayersAsync);
			this.Players = new ObservableCollection<Models.ListItemModel>();
		}

		public async void OnGenerateKeyTappedAsync()
		{
			const String carol_wif = "91rahqyxZb6R1MMq2rdYomfB8GWsLVqkBMHrUnaepxks73KgfaQ";
			NBitcoin.BitcoinSecret carol_secret = new NBitcoin.BitcoinSecret(carol_wif, NBitcoin.Network.TestNet);

			this.BitcoinAddress = carol_secret.GetAddress().ToString();
		}

		public async void GetPlayersAsync()
		{
			using (Clients.IPlayerClient client = new Clients.BitPokerRestClient())
			{
				var players = await client.GetPlayersAsync();

				foreach (PCL.Models.PlayerInfo player in players)
				{
					this.Players.Add(new Models.ListItemModel() { Title = player.BitcoinAddress, Description = player.UserAgent });
				}
			}
		}
	}
}