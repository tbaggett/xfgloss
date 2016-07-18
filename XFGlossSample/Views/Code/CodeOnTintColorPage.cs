using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeOnTintColorPage : ContentPage
	{
		public CodeOnTintColorPage()
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


