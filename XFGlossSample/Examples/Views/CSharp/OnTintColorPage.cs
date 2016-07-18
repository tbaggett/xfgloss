using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class OnTintColorPage : ContentPage
	{
		public OnTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "OnTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


