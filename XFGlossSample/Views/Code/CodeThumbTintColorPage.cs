using System;

using Xamarin.Forms;

namespace XFGlossSample
{
	public class CodeThumbTintColorPage : ContentPage
	{
		public CodeThumbTintColorPage()
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


