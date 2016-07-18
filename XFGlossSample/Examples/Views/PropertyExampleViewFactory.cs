using System;
using Xamarin.Forms;
using XFGlossSample.Examples.ViewModels;
using XFGlossSample.Examples.Views;

namespace XFGlossSample.Views
{
	// This class receives the name of a desired page type, uses runtime reflection to locate the needed views and, 
	// if found, constructs and returns a tabbed page that contains an info page and Xaml/C# example implementations.

	public static class PropertyExampleViewFactory
	{
		const string vmNamespace = "XFGlossSample.Examples.ViewModels.";
		const string xamlViewNamespace = "XFGlossSample.Examples.Views.Xaml.";
		const string cSharpViewNamespace = "XFGlossSample.Examples.Views.CSharp.";
		const string pageName = "Page";
		const string vmName = "ViewModel";

		public static Page CreateExampleView(string propertyName)
		{
			try
			{
				// Use runtime reflection to instantiate the requested views and view models, then assign the VM as the
				// view's binding context.
				IExamplesViewModel examplesVM =
					(IExamplesViewModel)Activator.CreateInstance(Type.GetType(vmNamespace + propertyName + vmName));
				Page infoPage = new InfoPage();
				infoPage.BindingContext = examplesVM;

				Page xamlPage = 
					(Page)Activator.CreateInstance(Type.GetType(xamlViewNamespace + propertyName + pageName));
				xamlPage.BindingContext = examplesVM;

				Page cSharpPage = 
					(Page)Activator.CreateInstance(Type.GetType(cSharpViewNamespace + propertyName + pageName));
				cSharpPage.BindingContext = examplesVM;

				TabbedPage exampleView = new TabbedPage();
				exampleView.Children.Add(new NavigationPage(infoPage));
				exampleView.Children.Add(new NavigationPage(xamlPage));
				exampleView.Children.Add(new NavigationPage(cSharpPage));

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

