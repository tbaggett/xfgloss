using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class AccessoryTypePage : ContentPage
	{
		public AccessoryTypePage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "namespace XFGlossSample.Examples.Views.Code.AccessoryTypePage", 
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


