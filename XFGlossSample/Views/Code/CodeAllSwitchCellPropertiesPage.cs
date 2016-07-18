using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeAllSwitchCellPropertiesPage : ContentPage
	{
		public CodeAllSwitchCellPropertiesPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


