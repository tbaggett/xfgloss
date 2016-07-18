using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XFGlossSample.Examples.ViewModels;

namespace XFGlossSample.Examples.Views
{
	public partial class InfoPage : ContentPage
	{
		public InfoPage()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			// Reset/add children to our PropertyDescription stack layout
			PropertyDescription.Children.Clear();

			if (BindingContext is IExamplesViewModel)
			{
				foreach (string description in (BindingContext as IExamplesViewModel).PropertyDescription)
				{
					PropertyDescription.Children.Add(new Label() { Text = description });
				}
			}
		}
	}
}

