/*
 * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
 * http://github.com/tbaggett
 * http://twitter.com/tbaggett
 * http://tommyb.com
 * http://ansuria.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
			var prevDetail = Detail as ContentPage;
			Detail = new NavigationPage(new AboutPage());
			CleanupPreviousDetailPage(prevDetail);

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
			var examplePage = XFGlossSampleViewFactory.CreateSampleAppPage(propertyName, pageTitle);
			if (examplePage != null)
			{
				var prevDetail = Detail as ContentPage;
				Detail = examplePage;
				CleanupPreviousDetailPage(prevDetail);
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

		void CleanupPreviousDetailPage(ContentPage prevPage)
		{
			if (prevPage is IDisposable)
			{
				(prevPage as IDisposable).Dispose();
			}

			if (prevPage != null)
			{
				prevPage.Content = null;
			}
		}

		void DisplayPageError(string propertyName = "")
		{
			string errMsg = $"An error occurred while trying to display the {propertyName} page.";

			DisplayAlert("Display Error", errMsg, "OK");
		}
	}
}