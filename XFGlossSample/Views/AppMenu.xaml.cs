using System;
using Xamarin.Forms;
using XFGlossSample.Examples.Views;
using XFGlossSample.ViewModels;
using XFGlossSample.Views;

namespace XFGlossSample
{
	public partial class AppMenu : MasterDetailPage
	{
		AppMenuItem _lastSelectedItem;

		public AppMenu()
		{
			InitializeComponent();
		}

		void ShowAbout(object sender, System.EventArgs e)
		{
			MenuItemsList.SelectedItem = _lastSelectedItem = null;
			Detail = new NavigationPage(new AboutPage());
			IsPresented = false;
		}

		void ShowPage(object sender, System.EventArgs e)
		{
			try
			{
				var appMenuItem = (sender as BindableObject).BindingContext as AppMenuItem;
				if (ShowPage(appMenuItem.PropertyName, appMenuItem.Title))
				{
					_lastSelectedItem = (AppMenuItem)MenuItemsList.SelectedItem;
				}
			}
			catch (Exception)
			{
				DisplayPageError();
			}
		}

		bool ShowPage(string propertyName, string pageTitle = null)
		{
			bool result = false;
			var examplePage = PropertyExampleViewFactory.CreateExampleView(propertyName, pageTitle);
			if (examplePage != null)
			{
				Detail = examplePage;
				result = true;
			}
			else
			{
				// The newly-selected page failed to be displayed. Restore the menu's previous selection
				// so the selection will match the currently-displayed page.
				MenuItemsList.SelectedItem = _lastSelectedItem;
				DisplayPageError(propertyName);
			}

			IsPresented = false;

			return result;
		}

		void DisplayPageError(string propertyName = "")
		{
			string errMsg = $"An error occurred while trying to display the {propertyName} page.";

			DisplayAlert("Display Error", errMsg, "OK");
		}
	}
}