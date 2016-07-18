using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeTintColorPage : ContentPage
	{
		public CodeTintColorPage()
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


