using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class ThumbTintColorPage : ContentPage
	{
		public ThumbTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "ThumbTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


