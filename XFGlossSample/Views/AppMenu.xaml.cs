using System;
using System.Diagnostics;
using Xamarin.Forms;
using XFGlossSample.ViewModels;
using XFGlossSample.Views;

namespace XFGlossSample
{
	public partial class AppMenu : MasterDetailPage
	{
		public AppMenu()
		{
			InitializeComponent();
		}

		void ShowAbout(object sender, System.EventArgs e)
		{
			MenuItemsList.SelectedItem = null;
			ShowPage(typeof(AboutPage));
		}

		void ShowPage(object sender, System.EventArgs e)
		{
			try
			{
				var pageType = ((sender as BindableObject).BindingContext as AppMenuItem).PageType;
				ShowPage(pageType);
			}
			catch (Exception)
			{
				DisplayAlert("Error Displaying Page", "The selected page could not be displayed.", "OK");
			}
		}

		void ShowPage(Type pageType)
		{
			Detail = new NavigationPage((Page)System.Activator.CreateInstance(pageType))
			{
				BarBackgroundColor = Color.Green,
				BarTextColor = Color.White
			};

			IsPresented = false;
		}
	}
}