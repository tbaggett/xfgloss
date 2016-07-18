using System;

using Xamarin.Forms;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class ThumbOnTintColorPage : ContentPage
	{
		public ThumbOnTintColorPage()
		{
			/*
			This is a bit of a hack. Android's renderer for TableView always adds an empty header for a 
			TableSection declaration, while iOS doesn't. To compensate, I'm using a Label to display info text
			on iOS, and the TableSection on Android since there is no easy way to get rid of it.This is a
			long-standing bug in the XF TableView on Android.
			(https://forums.xamarin.com/discussion/18037/tablesection-w-out-header)
			*/
			TableSection section;
			if (Device.OS == TargetPlatform.Android)
			{
				section = new TableSection("SwitchCell ThumbOnTintColor values set in C#:");
			}
			else
			{
				section = new TableSection();
			}
			section.Add(CreateThumbOnTintColorCell("Red", Color.Red));
			section.Add(CreateThumbOnTintColorCell("Green", Color.Green));
			section.Add(CreateThumbOnTintColorCell("Blue", Color.Blue));


			var stack = new StackLayout();
			if (Device.OS == TargetPlatform.iOS)
			{
				stack.Children.Add(new Label { Text = "SwitchCell ThumbOnTintColor values set in C#:", Margin = new Thickness(10) });
			}

			stack.Children.Add(new TableView()
								{
									Intent = TableIntent.Data,
									Root = new TableRoot()
									{
										section
									}
								});
			stack.Children.Add(new Label()
								{
									Text = "Switch ThumbOnTintColor values set in C#:",
									Margin = 10
								});
			stack.Children.Add(CreateThumbOnTintColorSwitch("Red", Color.Red));
			stack.Children.Add(CreateThumbOnTintColorSwitch("Green", Color.Green));
			stack.Children.Add(CreateThumbOnTintColorSwitch("Blue", Color.Blue));

			Content = stack;
		}

		SwitchCell CreateThumbOnTintColorCell(string colorName, Color colorValue)
		{
			var result = new SwitchCell();
			result.Text = colorName;

			// Assign our gloss properties - You can use the standard static setter...
			XFGloss.Views.SwitchCell.SetThumbOnTintColor(result, colorValue);

			// ...or instantiate an instance of the Gloss properties you want to assign values to
			//	var gloss = new XFGloss.Views.SwitchCell(result);
			//	gloss.BackgroundColor = Color.Blue;
			//	gloss.TintColor = Color.Red;
			//	...

			return result;
		}

		StackLayout CreateThumbOnTintColorSwitch(string colorName, Color colorValue)
		{
			var result = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10),
				Children =
				{
					new Label()
					{
						Text = colorName,
						HorizontalOptions = LayoutOptions.StartAndExpand
					}
				}
			};

			var control = new Switch();
			XFGloss.Views.Switch.SetThumbOnTintColor(control, colorValue);

			result.Children.Add(control);

			return result;
		}
	}
}