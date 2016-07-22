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

using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Utils;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Slider), typeof(XFGloss.Droid.Renderers.XFGlossSliderRenderer))]
namespace XFGloss.Droid.Renderers
{
	public class XFGlossSliderRenderer : SliderRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
			base.OnElementChanged(e);

			UpdateSliderProperties();
		}

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

				Control.ProgressBackgroundTintList = 
					ColorStateList.ValueOf((maxTrackTintColor == Color.Default) ? 
					                       ThemeUtil.IntToColor(ThemeUtil.ColorControlNormal(Control.Context, defaultColor)) : 
				    						maxTrackTintColor.ToAndroid());
			}

			// MinTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MinTrackTintColorProperty.PropertyName)
			{
				var minTrackTintColor = (Color)Element.GetValue(SliderGloss.MinTrackTintColorProperty);

				Control.ProgressTintList = 
					ColorStateList.ValueOf((minTrackTintColor == Color.Default) ?
					                       ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
										   minTrackTintColor.ToAndroid());
			}

			// ThumbTintColor Property
			if (propertyName == null || propertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				var thumbTintColor = (Color)Element.GetValue(SliderGloss.ThumbTintColorProperty);

				Control.ThumbTintList = 
					ColorStateList.ValueOf((thumbTintColor == Color.Default) ?
										   ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
					                       thumbTintColor.ToAndroid());
			}
		}
	}
}