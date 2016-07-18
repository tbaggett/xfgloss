using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeThumbOnTintColorPage : ContentPage
	{
		public CodeThumbOnTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeThumbOnTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


