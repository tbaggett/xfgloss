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

using Xamarin.Forms;
using XFGloss;

namespace XFGlossSample.Examples.Views.CSharp
{
	public class MaxTrackTintColorPage : ContentPage
	{
		public MaxTrackTintColorPage()
		{
			var stack = new StackLayout { Padding = 10 };

			// Slider demo

			stack.Children.Add(new Label { Text = "Slider MaxTrackTintColor values set in C#:", Margin = new Thickness(10) });
			stack.Children.Add(CreateMaxTrackTintColorSlider(25, Color.Red));
			stack.Children.Add(CreateMaxTrackTintColorSlider(50, Color.Green));
			stack.Children.Add(CreateMaxTrackTintColorSlider(75, Color.Blue));

			Content = new ScrollView() { Content = stack };
		}

		Slider CreateMaxTrackTintColorSlider(double value, Color colorValue)
		{
			var slider = new Slider { Minimum = 0, Maximum = 100, Value = value };

			SliderGloss.SetMaxTrackTintColor(slider, colorValue);

			return slider;
		}
	}
}