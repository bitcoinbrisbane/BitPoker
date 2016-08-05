using System;
using Xamarin.Forms;

namespace BitPokerMobile
{
	public class MainPage : TabbedPage // CarouselPage
	{
		public MainPage()
		{
			HomePage homePage = new HomePage() { Title = "Home" };
			Children.Add(homePage);
			Children.Add(new TablesPage() { Title = "Tables" });
			Children.Add(new PlayersPage() { Title = "Players" });
		}
	}
}