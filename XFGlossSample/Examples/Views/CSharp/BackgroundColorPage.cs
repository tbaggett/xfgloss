using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class BackgroundColorPage : ContentPage
	{
		public BackgroundColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "BackgroundColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


