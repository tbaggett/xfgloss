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
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.Xaml
{
	public partial class BackgroundGradientPage : ContentPage
	{
		Timer updater;
		bool updateGradient;

		public BackgroundGradientPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			updateGradient = true;
			UpdateGradient();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			updateGradient = false;
		}

		/******************************************
		 * 
		 * NOTE: This code is for gradient demonstration purposes only. I do NOT recommend you continuously update
		 * a gradient fill in a cell or page! 
		 * 
		 ******************************************/

		void UpdateGradient(object gradient = null)
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

			updater?.Dispose();
			// Continue updating the gradient as long as we're visible
			// Note that checking IsVisible was always returning true.
			updater = (updateGradient) ? new Timer(UpdateGradient, rotatingGradient, 100, -1) : null;
		}
	}
}