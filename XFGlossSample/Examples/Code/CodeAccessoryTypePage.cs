using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeAccessoryTypePage : ContentPage
	{
		public CodeAccessoryTypePage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeAccessoryTypePage", 
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


