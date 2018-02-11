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

using System.Collections.Generic;
using Xamarin.Forms;
using XFGloss;
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class BackgroundColorPage : ContentPage
	{
		public BackgroundColorPage()
		{
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
				section = new TableSection("Cell BackgroundColor values set in C#:");
			}
			else
			{
				section = new TableSection();
			}

			var cell = new TextCell { Text = "Red", TextColor = Color.White };
			CellGloss.SetBackgroundColor(cell, Color.Red);
			section.Add(cell);

			cell = new TextCell { Text = "Green", TextColor = Color.White };
			CellGloss.SetBackgroundColor(cell, Color.Green);
			section.Add(cell);

			cell = new TextCell { Text = "Blue", TextColor = Color.White };
			CellGloss.SetBackgroundColor(cell, Color.Blue);
			section.Add(cell);

			var stack = new StackLayout();
			if (Device.RuntimePlatform == Device.iOS)
			{
				stack.Children.Add(new Label { Text = "Cell BackgroundColor values set in C#:", Margin = new Thickness(10) });
			}
			stack.Children.Add(new TableView
			{
				Intent = TableIntent.Data,
				HeightRequest = XFGlossDevices.OnPlatform<double>(132, 190),
				Root = new TableRoot
				{
					section
				}
			});

			Content = stack;
		}
	}
}