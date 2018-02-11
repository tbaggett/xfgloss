/*
 * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
 * http://github.com/tbaggett
 * http://twitter.com/tbaggett
 * http://tommyb.com
 * http://ansuria.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

using Xamarin.Forms;
using XFGloss;
using XFGlossSample.Examples.ViewModels;
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class TintColorPage : ContentPage
	{
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var stack = new StackLayout();
			if (Device.RuntimePlatform == Device.iOS)
			{
				stack.Children.Add(
					new StackLayout
					{
						Spacing = 0,
						IsVisible = (BindingContext as TintColorViewModel).isRunningiOS,
						Children =
						{
							new Label()
							{
								Text = "Cell accessory TintColor values set in C#:",
								Margin = 10
							},
							new TableView()
							{
								HeightRequest = 132,
								Root = new TableRoot()
								{
									new TableSection()
									{
										CreateTintColorCell("Red", Color.Red, CellGlossAccessoryType.Checkmark),
										CreateTintColorCell("Green", Color.Green, CellGlossAccessoryType.Checkmark),
										CreateTintColorCell("Blue", Color.Blue, CellGlossAccessoryType.EditIndicator)
									}
								}
							}
						}
					});

				stack.Children.Add(
					new Label()
					{
						Text = "SwitchCell TintColor values set in C#:",
						Margin = 10
					});
			}

			/*
			This is a bit of a hack. Android's renderer for TableView always adds an empty header for a 
			TableSection declaration, while iOS doesn't. To compensate, I'm using a Label to display info text
			on iOS, and the TableSection on Android since there is no easy way to get rid of it.This is a
			long-standing bug in the XF TableView on Android.
			(https://forums.xamarin.com/discussion/18037/tablesection-w-out-header)
			*/
			TableSection section;
			if (Device.RuntimePlatform == Device.Android)
			{
				section = new TableSection("SwitchCell TintColor values set in C#:");
			}
			else
			{
				section = new TableSection();
			}
			section.Add(CreateTintColorSwitchCell("Red", Color.Red));
			section.Add(CreateTintColorSwitchCell("Green", Color.Green));
			section.Add(CreateTintColorSwitchCell("Blue", Color.Blue));

			stack.Children.Add(
				new TableView()
				{
					Intent = TableIntent.Data,
					HeightRequest = XFGlossDevices.OnPlatform<double>(132, 190),
					Root = new TableRoot()
					{
						section
					}
				});

			stack.Children.Add(
				new Label()
				{
					Text = "Switch TintColor values set in C#:",
					Margin = 10
				});

			stack.Children.Add(CreateTintColorSwitch("Red", Color.Red));
			stack.Children.Add(CreateTintColorSwitch("Green", Color.Green));
			stack.Children.Add(CreateTintColorSwitch("Blue", Color.Blue));

			if (Device.RuntimePlatform == Device.iOS)
			{
				var scrollView = new ScrollView();
				scrollView.Content = stack;

				Content = scrollView;
			}
			else
			{
				Content = stack;
			}
		}

		Cell CreateTintColorCell(string colorName, Color colorValue, CellGlossAccessoryType accessoryType)
		{
			Cell result;
			if (accessoryType == CellGlossAccessoryType.EditIndicator)
			{
				result = new EntryCell();
				(result as EntryCell).Label = colorName;
				(result as EntryCell).Placeholder = "Optional";
				(result as EntryCell).HorizontalTextAlignment = TextAlignment.End;
			}
			else
			{
				result = new TextCell();
				(result as TextCell).Text = colorName;
			}

			// Instantiate an instance of the Gloss properties you want to assign values to
			var gloss = new CellGloss(result);
			gloss.TintColor = colorValue;
			gloss.AccessoryType = accessoryType;

			return result;
		}

		SwitchCell CreateTintColorSwitchCell(string colorName, Color colorValue)
		{
			var result = new SwitchCell();
			result.Text = colorName;

			// Assign our gloss properties - You can use the standard static setter...
			CellGloss.SetTintColor(result, colorValue);

			// ...or instantiate an instance of the Gloss properties you want to assign values to
			//	var gloss = new XFGloss.Views.SwitchCell(result);
			//	gloss.BackgroundColor = Color.Blue;
			//	gloss.TintColor = Color.Red;
			//	...

			return result;
		}

		StackLayout CreateTintColorSwitch(string colorName, Color colorValue)
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
			SwitchGloss.SetTintColor(control, colorValue);

			result.Children.Add(control);

			return result;
		}
	}
}