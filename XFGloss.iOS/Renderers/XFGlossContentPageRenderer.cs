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
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Views;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.iOS.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.iOS.Renderers
{
	public class XFGlossContentPageRenderer : PageRenderer
	{
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			XFGlossGradientLayer.UpdateGradientLayer(NativeView);
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			}

			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += OnElementPropertyChanged;
				UpdateBackgroundGradient();
			}
		}

		void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == null || e.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				UpdateBackgroundGradient();
			}
		}

		void UpdateBackgroundGradient()
		{
			var gradientSource = (GlossGradient)Element.GetValue(ContentPageGloss.BackgroundGradientProperty);
			if (gradientSource == null)
			{
				XFGlossGradientLayer.RemoveGradientLayer(NativeView);
			}
			else
			{
				XFGlossGradientLayer.UpdateGradientLayer(NativeView, gradientSource);
			}
		}
	}
}

