using System;

using Xamarin.Forms;

namespace XFGlossSample.Views
{
	public class CodeAllSwitchCellPropertiesPage : ContentPage
	{
		public CodeAllSwitchCellPropertiesPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label
					{
						Text = "CodeAllSwitchCellPropertiesPage",
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}


