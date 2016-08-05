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

		public ICommand CreateMockKeyTapped { protected set; get; }

		public HomeViewModel()
		{
			this.GenerateKeyTapped = new Command(OnGenerateKeyTappedAsync);
			this.RefreshKeyTapped = new Command(GetPlayersAsync);
			this.CreateMockKeyTapped = new Command(RefreshMocksAsync);
			this.Players = new ObservableCollection<Models.ListItemModel>();
		}

		public void OnGenerateKeyTappedAsync()
		{
			const String carol_wif = "91rahqyxZb6R1MMq2rdYomfB8GWsLVqkBMHrUnaepxks73KgfaQ";
			NBitcoin.BitcoinSecret carol_secret = new NBitcoin.BitcoinSecret(carol_wif, NBitcoin.Network.TestNet);

			this.BitcoinAddress = carol_secret.GetAddress().ToString();

			MessagingCenter.Send<HomeViewModel>(this, "Hi");
			//MessagingCenter.Send<HomeViewModel>(this, "Hi", this.BitcoinAddress);
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

		public async void RefreshMocksAsync()
		{
			this.IsBusy = true;

			using (Clients.BitPokerRestClient client = new Clients.BitPokerRestClient())
			{
				var result = await client.RefreshMocksAsync();
				//MessagingCenter.Send<HomeViewModel>(this, "Hi", new EventArgs());
			}

			this.IsBusy = false;
		}
	}
}