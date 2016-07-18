using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeThumbOnTintColorPage : ContentPage
	{
		public CodeThumbOnTintColorPage()
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


