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
using System.Collections.Generic;
using Xamarin.Forms;
using XFGloss;
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class BackgroundGradientPage : ContentPage
	{
		bool updateGradient;

		Gradient rotatingGradient;
		TextCell rotatingCell;

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

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
				section = new TableSection("Cell BackgroundGradient values set in C#:");
			}
			else
			{
				section = new TableSection();
			}

			section.Add(CreateBackgroundGradientCells());

			var stack = new StackLayout();

			if (Device.RuntimePlatform == Device.iOS)
			{
				stack.Children.Add(new Label { Text = "Cell BackgroundGradient values set in C#:", Margin = new Thickness(10) });
			}
			stack.Children.Add(new TableView
			{
				Intent = TableIntent.Data,
				BackgroundColor = Color.Transparent,
				VerticalOptions = LayoutOptions.Start,
				HeightRequest = XFGlossDevices.OnPlatform<double>(176, 232),
				Root = new TableRoot
				{
					section
				}
			});
			stack.Children.Add(new Label { Text = "ContentPage BackgroundGradient value set in C#:", Margin = new Thickness(10) });

			Content = stack;

			// Set the page's background gradient
			ContentPageGloss.SetBackgroundGradient(this, new Gradient(Color.White, Color.FromRgb(128, 0, 0)));
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			updateGradient = true;

			Device.StartTimer(new TimeSpan(1000000), UpdateGradient);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			updateGradient = false;
		}

		TextCell[] CreateBackgroundGradientCells()
		{
			List<TextCell> result = new List<TextCell>();

			Dictionary<string, Tuple<int, Color, Color>> Colors = new Dictionary<string, Tuple<int, Color, Color>>()
			{
				{ "Red", new Tuple<int, Color, Color>(Gradient.RotationTopToBottom, Color.Red, Color.Maroon) },
				{ "Green", new Tuple<int, Color, Color>(Gradient.RotationLeftToRight, Color.Lime, Color.Green) },
				{ "Blue", new Tuple<int, Color, Color>(Gradient.RotationBottomToTop, Color.Blue, Color.Navy) }
			};

			// Iterate through the color values, creating a new text cell for each entity
			var colorNames = Colors.Keys;
			foreach (string colorName in colorNames)
			{
				var cell = new TextCell();
				cell.Text = colorName;
				cell.TextColor = Color.White;

				// Assign our gloss properties - You can use the standard static setter...
				var cellInfo = Colors[colorName];
				CellGloss.SetBackgroundGradient(cell, new Gradient(cellInfo.Item1, cellInfo.Item2, cellInfo.Item3));

				// ...or instantiate an instance of the Gloss properties you want to assign values to
				//	var gloss = new XFGloss.Views.Cell(cell);
				//	gloss.AccessoryType = accType;
				//	gloss.BackgroundColor = Color.Blue;
				//	gloss.TintColor = Color.Red;
				//	...

				result.Add(cell);
			}

			// Add a multi-color gradient
			rotatingCell = new TextCell();
			rotatingCell.Text = "All Three";
			rotatingCell.TextColor = Color.White;

			// Manually construct a multi-color gradient at an angle of our choosing
			rotatingGradient = new Gradient()
			{
				Rotation = 135,
				Steps = new GradientStepCollection()
				{
					new GradientStep(Colors["Red"].Item2, 0),
					new GradientStep(Colors["Red"].Item3, .25),
					new GradientStep(Colors["Green"].Item2, .4),
					new GradientStep(Colors["Green"].Item3, .6),
					new GradientStep(Colors["Blue"].Item2, .75),
					new GradientStep(Colors["Blue"].Item3, 1),
				}
			};

			// You can also initialize a multi-color gradient in code like this:
			//rotatingGradient = new Gradient(135); // 135 degree angle
			//rotatingGradient.AddStep(Colors["Red"].Item2, 0);
			//rotatingGradient.AddStep(Colors["Red"].Item3, .25);
			//rotatingGradient.AddStep(Colors["Green"].Item2, .4);
			//rotatingGradient.AddStep(Colors["Green"].Item3, .6);
			//rotatingGradient.AddStep(Colors["Blue"].Item2, .75);
			//rotatingGradient.AddStep(Colors["Blue"].Item3, 1);

			CellGloss.SetBackgroundGradient(rotatingCell, rotatingGradient);

			result.Add(rotatingCell);

			return result.ToArray();
		}

		/******************************************
		 * 
		 * NOTE: This code is for gradient demonstration purposes only. I do NOT recommend you continuously update
		 * a gradient fill in a cell or page! 
		 * 
		 ******************************************/

		bool UpdateGradient()
		{
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				if (rotatingGradient.Rotation >= 355)
				{
					rotatingGradient.Rotation = 0;
				}
				else
				{
					rotatingGradient.Rotation += 5;
				}
			});

			return updateGradient;
		}
	}
}