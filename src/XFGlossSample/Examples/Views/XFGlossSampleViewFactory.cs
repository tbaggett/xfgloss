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
using System.Diagnostics;
using System.Reflection;
using Xamarin.Forms;
using XFGlossSample.Examples.ViewModels;
using XFGlossSample.Examples.Views;
using XFGlossSample.PerfTests.ViewModels;
using XFGlossSample.PerfTests.Views;

namespace XFGlossSample.Examples.Views
{
	// This class receives the name of a desired page type, uses runtime reflection to locate the needed views and, 
	// if found, constructs and returns a tabbed page that contains an info page and Xaml/C# example implementations.

	public static class XFGlossSampleViewFactory
	{
		const string examplesNamespace = "XFGlossSample.Examples.";
		const string perfTestNamespace = "XFGlossSample.PerfTests.";

		const string vmNamespace = "ViewModels.";
		const string viewsNamespace = "Views.";
		const string xamlViewNamespace = "Views.Xaml.";
		const string cSharpViewNamespace = "Views.CSharp.";

		const string pageName = "Page";
		const string vmName = "ViewModel";

		public static Page CreateSampleAppPage(string propertyName, string pageTitle = null)
		{
			try
			{
				// Use runtime reflection to instantiate the requested views and view models, then assign the VM as the
				// view's binding context.
				var vmType = Type.GetType(examplesNamespace + vmNamespace + propertyName + vmName);
				if (vmType != null)
				{
					var typeInst = Activator.CreateInstance(vmType);
					if (typeInst is IExamplesViewModel)
					{
						var examplesVM = typeInst as IExamplesViewModel;

						Page infoPage = new InfoPage();
						infoPage.BindingContext = examplesVM;
						infoPage.Title = pageTitle;

						Page xamlPage =
							(Page)Activator.CreateInstance(Type.GetType(examplesNamespace + xamlViewNamespace +
																		propertyName + pageName));
						xamlPage.BindingContext = examplesVM;
						xamlPage.Title = pageTitle;

						Page cSharpPage =
							(Page)Activator.CreateInstance(Type.GetType(examplesNamespace + cSharpViewNamespace +
																		propertyName + pageName));
						cSharpPage.BindingContext = examplesVM;
						cSharpPage.Title = pageTitle;

						TabbedPage examplePage = new TabbedPage();

						// Assign icons (iOS only) and titles to be used by each of the tabs to the navigation pages
						if (Device.RuntimePlatform == Device.iOS)
						{
							examplePage.Children.Add(new NavigationPage(infoPage) { Title = "Info", Icon = "infocircle.png" });
							examplePage.Children.Add(new NavigationPage(xamlPage) { Title = "Xaml", Icon = "xamlcode.png" });
							examplePage.Children.Add(new NavigationPage(cSharpPage) { Title = "C#", Icon = "csharp.png" });
						}
						else
						{
							examplePage.Children.Add(new NavigationPage(infoPage) { Title = "Info" });
							examplePage.Children.Add(new NavigationPage(xamlPage) { Title = "Xaml" });
							examplePage.Children.Add(new NavigationPage(cSharpPage) { Title = "C#" });
						}

						return examplePage;
					}
				}

				// Create the specified performance test page to demonstrate listview scrolling with
				// different caching strategies
				var pageType = Type.GetType(perfTestNamespace + viewsNamespace + propertyName + pageName);
				if (pageType != null)
				{
					var pageInst = Activator.CreateInstance(pageType);
					if (pageInst is IPerfTestPage)
					{
						Page perfTestPage = pageInst as Page;
						perfTestPage.Title = pageTitle;

						return new NavigationPage(perfTestPage);
					}
				}

				// Unrecognized page type!
				return null;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				// No need to handle any exceptions here. The caller is responsible for displaying an error message
				// to the user if the requested page couldn't be constructed.
			}

			return null;
		}
	}
}

