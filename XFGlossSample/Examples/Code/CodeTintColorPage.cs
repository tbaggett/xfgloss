using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeTintColorPage : ContentPage
	{
		public CodeTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


