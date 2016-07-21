using Xamarin.Forms;
using XFGloss;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class MinTrackTintColorPage : ContentPage
	{
		public MinTrackTintColorPage()
		{
			var stack = new StackLayout { Padding = 10 };

			// Slider demo

			stack.Children.Add(new Label { Text = "Slider MinTrackTintColor values set in C#", Margin = new Thickness(10) });
			stack.Children.Add(CreateMinTrackTintColorSlider(25, Color.Red));
			stack.Children.Add(CreateMinTrackTintColorSlider(50, Color.Green));
			stack.Children.Add(CreateMinTrackTintColorSlider(75, Color.Blue));

			Content = new ScrollView() { Content = stack };
		}

		Slider CreateMinTrackTintColorSlider(double value, Color colorValue)
		{
			var slider = new Slider { Minimum = 0, Maximum = 100, Value = value };

			SliderGloss.SetMinTrackTintColor(slider, colorValue);

			return slider;
		}
	}
}