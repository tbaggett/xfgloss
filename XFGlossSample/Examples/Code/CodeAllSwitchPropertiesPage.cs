using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeAllSwitchPropertiesPage : ContentPage
	{
		public CodeAllSwitchPropertiesPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeAllSwitchPropertiesPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


