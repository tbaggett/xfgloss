using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class BackgroundGradientPage : ContentPage
	{
		public BackgroundGradientPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


