using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class ThumbOnTintColorPage : ContentPage
	{
		public ThumbOnTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "ThumbOnTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


