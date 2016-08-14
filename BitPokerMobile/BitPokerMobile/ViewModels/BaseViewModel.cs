using System;
using System.ComponentModel;

namespace BitPokerMobile.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Boolean _isBusy;
		public Boolean IsBusy
		{
			set
			{
				_isBusy = value;
				OnPropertyChanged ("IsBusy");
			}
			get
			{
				return _isBusy;
			}
		}

		private Boolean _allowUserInteraction;
		public Boolean AllowUserInteraction
		{
			set
			{
				_allowUserInteraction = value;
				OnPropertyChanged ("AllowUserInteraction");
			}
			get
			{
				return _allowUserInteraction;
			}
		}

		private String _title;
		public String Title
		{
			set
			{
				_title = value;
				OnPropertyChanged ("Title");
			}
			get
			{
				return _title;
			}
		}

		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		//public event EventHandler<IDialogViewModel> NotificationRaised;

		//protected virtual void RaiseNotification(IDialogViewModel e)
		//{
		//	EventHandler<IDialogViewModel> handler = NotificationRaised;
		//	if (handler != null)
		//	{
		//		handler(this, e);
		//	}
		//}
	}
}