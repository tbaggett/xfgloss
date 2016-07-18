using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.Code
{
	public class CodeIsVisiblePage : ContentPage
	{
		public CodeIsVisiblePage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeIsVisiblePage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


