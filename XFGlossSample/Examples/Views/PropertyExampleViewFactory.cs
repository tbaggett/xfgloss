using System;
using Xamarin.Forms;
using XFGlossSample.Examples.ViewModels;
using XFGlossSample.Examples.Views;

namespace XFGlossSample.Examples.Views
{
	// This class receives the name of a desired page type, uses runtime reflection to locate the needed views and, 
	// if found, constructs and returns a tabbed page that contains an info page and Xaml/C# example implementations.

	public static class PropertyExampleViewFactory
	{
		const string rootNamespace = "XFGlossSample.Examples.";
		const string vmNamespace = rootNamespace + "ViewModels.";
		const string xamlViewNamespace = rootNamespace + "Views.Xaml.";
		const string cSharpViewNamespace = rootNamespace + "Views.CSharp.";
		const string pageName = "Page";
		const string vmName = "ViewModel";

		public static Page CreateExampleView(string propertyName, string pageTitle = null)
		{
			try
			{
				// Use runtime reflection to instantiate the requested views and view models, then assign the VM as the
				// view's binding context.
				IExamplesViewModel examplesVM =
					(IExamplesViewModel)Activator.CreateInstance(Type.GetType(vmNamespace + propertyName + vmName));
				
				Page infoPage = new InfoPage();
				infoPage.BindingContext = examplesVM;
				infoPage.Title = pageTitle;

				Page xamlPage = 
					(Page)Activator.CreateInstance(Type.GetType(xamlViewNamespace + propertyName + pageName));
				xamlPage.BindingContext = examplesVM;
				xamlPage.Title = pageTitle;

				Page cSharpPage = 
					(Page)Activator.CreateInstance(Type.GetType(cSharpViewNamespace + propertyName + pageName));
				cSharpPage.BindingContext = examplesVM;
				cSharpPage.Title = pageTitle;

				TabbedPage exampleView = new TabbedPage();

				// Assign icons (iOS only) and titles to be used by each of the tabs to the navigation pages
				if (Device.OS == TargetPlatform.iOS)
				{
					exampleView.Children.Add(new NavigationPage(infoPage) { Title = "Info", Icon = "infocircle.png" });
					exampleView.Children.Add(new NavigationPage(xamlPage) { Title = "Xaml", Icon = "xamlcode.png" });
					exampleView.Children.Add(new NavigationPage(cSharpPage) { Title = "C#", Icon = "csharp.png" });
				}
				else
				{
					exampleView.Children.Add(new NavigationPage(infoPage) { Title = "Info" });
					exampleView.Children.Add(new NavigationPage(xamlPage) { Title = "Xaml" });
					exampleView.Children.Add(new NavigationPage(cSharpPage) { Title = "C#" });
				}

				return exampleView;
			}
			catch (Exception)
			{
				// No need to handle any exceptions here. The caller is responsible for displaying an error message
				// to the user if the requested page couldn't be constructed.
			}

			return null;
		}
	}
}

