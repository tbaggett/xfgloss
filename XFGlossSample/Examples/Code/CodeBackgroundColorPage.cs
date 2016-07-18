using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeBackgroundColorPage : ContentPage
	{
		public CodeBackgroundColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeBackgroundColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


