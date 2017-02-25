/*
 * Copyright (C) 2016-2017 Ansuria Solutions LLC & Tommy Baggett: 
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
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Slider), typeof(XFGloss.iOS.Renderers.XFGlossSliderRenderer))]
namespace XFGloss.iOS.Renderers
{
	/// <summary>
	/// The iOS platform-specific Xamarin.Forms renderer used for all <see cref="T:Xamarin.Forms.Slider"/>
	/// derived classes.
	/// </summary>
	[Preserve(AllMembers = true)]
	public class XFGlossSliderRenderer : SliderRenderer
	{
		/// <summary>
		/// <see cref="T:Xamarin.Forms.Platform.iOS.SliderRenderer"/> override that is called whenever the associated
		/// <see cref="T:Xamarin.Forms.Slider"/> instance changes
		/// </summary>
		/// <param name="e"><see cref="T:Xamarin.Forms.Platform.iOS.ElementChangedEventArgs"/> that specifies the
		/// previously and newly assigned <see cref="T:Xamarin.Forms.Slider"/> instances
		/// </param>
		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
			base.OnElementChanged(e);

			UpdateSliderProperties();
		}

		/// <summary>
		/// <see cref="T:Xamarin.Forms.Platform.iOS.SliderRenderer"/> override that is called whenever the
		/// <see cref="T:Xamarin.Forms.Slider.PropertyChanged"/> event is fired
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.MinTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				UpdateSliderProperties(e.PropertyName);
			}
		}

		/// <summary>
		/// Private helper method used to apply the <see cref="T:Xamarin.Forms.Slider"/> properties to the native
		/// <see cref="T:UIKit.UISlider"/> control.
		/// </summary>
		/// <param name="propertyName">Name of the <see cref="T:XFGloss.SliderGloss"/> property whose value changed</param>
		void UpdateSliderProperties(string propertyName = null)
		{
			if (Element == null || Control == null)
			{
				return;
			}

			// MaxTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName)
			{
				var maxTrackTintColor = (Color)Element.GetValue(SliderGloss.MaxTrackTintColorProperty);

				Control.MaximumTrackTintColor = (maxTrackTintColor == Color.Default) ? null : maxTrackTintColor.ToUIColor();
			}

			// MinTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MinTrackTintColorProperty.PropertyName)
			{
				var minTrackTintColor = (Color)Element.GetValue(SliderGloss.MinTrackTintColorProperty);

				Control.MinimumTrackTintColor = (minTrackTintColor == Color.Default) ? null : minTrackTintColor.ToUIColor();
			}

			// ThumbTintColor Property
			if (propertyName == null || propertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				var thumbTintColor = (Color)Element.GetValue(SliderGloss.ThumbTintColorProperty);

				Control.ThumbTintColor = (thumbTintColor == Color.Default) ? null : thumbTintColor.ToUIColor();
			}
		}
	}
}