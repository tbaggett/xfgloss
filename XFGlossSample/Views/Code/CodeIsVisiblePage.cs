using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeIsVisiblePage : ContentPage
	{
		public CodeIsVisiblePage()
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


