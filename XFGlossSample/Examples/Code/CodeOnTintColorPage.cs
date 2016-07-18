using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeOnTintColorPage : ContentPage
	{
		public CodeOnTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeOnTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


