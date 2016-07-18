using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeAccessoryTypePage : ContentPage
	{
		public CodeAccessoryTypePage()
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


