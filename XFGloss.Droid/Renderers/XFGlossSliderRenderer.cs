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
using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Utils;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Slider), typeof(XFGloss.Droid.Renderers.XFGlossSliderRenderer))]
namespace XFGloss.Droid.Renderers
{
	/// <summary>
	/// The Android platform-specific Xamarin.Forms renderer used for all <see cref="T:Xamarin.Forms.Slider"/>
	/// derived classes.
	/// </summary>
	public class XFGlossSliderRenderer : SliderRenderer
	{
		/// <summary>
		/// <see cref="T:Xamarin.Forms.Platform.Android.SliderRenderer"/> override that is called whenever the associated
		/// <see cref="T:Xamarin.Forms.Slider"/> instance changes
		/// </summary>
		/// <param name="e"><see cref="T:Xamarin.Forms.Platform.Android.ElementChangedEventArgs"/> that specifies the
		/// previously and newly assigned <see cref="T:Xamarin.Forms.Slider"/> instances
		/// </param>
		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
			base.OnElementChanged(e);

			UpdateSliderProperties();
		}

		/// <summary>
		/// <see cref="T:Xamarin.Forms.Platform.Android.SliderRenderer"/> override that is called whenever the
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
		/// <see cref="T:Android.Widget.Seekbar"/> control.
		/// </summary>
		/// <param name="propertyName">Name of the <see cref="T:XFGloss.SliderGloss"/> property whose value changed</param>
		void UpdateSliderProperties(string propertyName = null)
		{
			if (Element == null || Control == null)
			{
				return;
			}

			var defaultColor = AColor.Transparent;

			// MaxTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName)
			{
				var maxTrackTintColor = (Color)Element.GetValue(SliderGloss.MaxTrackTintColorProperty);

				if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
				{
					Control.ProgressBackgroundTintList =
						ColorStateList.ValueOf((maxTrackTintColor == Color.Default) ?
											   ThemeUtil.IntToColor(ThemeUtil.ColorControlNormal(Control.Context, defaultColor)) :
												maxTrackTintColor.ToAndroid());
				}
				else
				{
					#if DEBUG
					Console.WriteLine("XFGloss: Android.Widget.Slider tinting isn't supported prior to Android API 23" +
									  " (Marshmallow).");
					#endif
				}
			}

			// MinTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MinTrackTintColorProperty.PropertyName)
			{
				var minTrackTintColor = (Color)Element.GetValue(SliderGloss.MinTrackTintColorProperty);

				if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
				{
					Control.ProgressTintList = 
						ColorStateList.ValueOf((minTrackTintColor == Color.Default) ?
						                       ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
											   minTrackTintColor.ToAndroid());
				}
				else
				{
					#if DEBUG
					Console.WriteLine("XFGloss: Android.Widget.Slider tinting isn't supported prior to Android API 23" +
									  " (Marshmallow).");
					#endif
				}
			}

			// ThumbTintColor Property
			if (propertyName == null || propertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				var thumbTintColor = (Color)Element.GetValue(SliderGloss.ThumbTintColorProperty);

				if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
				{
					Control.ThumbTintList = 
						ColorStateList.ValueOf((thumbTintColor == Color.Default) ?
											   ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
						                       thumbTintColor.ToAndroid());
				}
			}
			else
			{
				#if DEBUG
				Console.WriteLine("XFGloss: Android.Widget.Slider tinting isn't supported prior to Android API 23" +
								  " (Marshmallow).");
				#endif
			}
		}
	}
}