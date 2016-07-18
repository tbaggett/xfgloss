using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeThumbTintColorPage : ContentPage
	{
		public CodeThumbTintColorPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeThumbTintColorPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


