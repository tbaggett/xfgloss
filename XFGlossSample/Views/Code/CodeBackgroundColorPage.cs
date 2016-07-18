using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeBackgroundColorPage : ContentPage
	{
		public CodeBackgroundColorPage()
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


