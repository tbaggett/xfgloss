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

namespace XFGloss.Droid.Extensions
{
	/// <summary>
	/// Extension methods for the <see cref="T:Xamarin.Forms.Switch"/> control to apply the
	/// <see cref="T:Xamarin.Forms.Color"/> values to an Android Switch control
	/// </summary>
	public static class XFGlossSwitchExtensions
	{
		/// <summary>
		/// An extension method that applies all of the current properties defined by the passed
		/// <see cref="T:XFGloss.ISwitchGloss"/> interface implementation to the Android Switch control
		/// </summary>
		/// <param name="control">Control.</param>
		/// <param name="properties">Properties.</param>
		/// <param name="propertyName">Property name.</param>
		public static void UpdateColorProperty(this Android.Widget.Switch control, ISwitchGloss properties, string propertyName)
		{
			// We have to create a multiple state color list to set both the "off" and "on" (checked/unchecked)
			// states of the switch control. 

			var defaultColor = Android.Graphics.Color.Transparent;

			if (propertyName == null ||
			    propertyName == SwitchGloss.TintColorProperty.PropertyName ||
			    propertyName == SwitchGloss.OnTintColorProperty.PropertyName)
			{
				int[][] states = new int[2][];
				int[] colors = new int[2];

				var tintColor = properties.TintColor;
				var onTintColor = properties.OnTintColor;

				if (tintColor != Color.Default || onTintColor != Color.Default)
				{
					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = (tintColor != Color.Default) ?
								tintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlNormal(control.Context, defaultColor));

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = (onTintColor != Color.Default) ?
								onTintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlActivated(control.Context, defaultColor));

					var colorList = new ColorStateList(states, colors);
					control.TrackTintList = colorList;
				}
			}

			if (propertyName == null ||
			    propertyName == SwitchGloss.ThumbTintColorProperty.PropertyName ||
			    propertyName == SwitchGloss.ThumbOnTintColorProperty.PropertyName)
			{
				int[][] states = new int[2][];
				int[] colors = new int[2];

				var thumbTintColor = properties.ThumbTintColor;
				var thumbOnTintColor = properties.ThumbOnTintColor;

				if (thumbTintColor != Color.Default || thumbOnTintColor != Color.Default)
				{
					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = (thumbTintColor != Color.Default) ?
								thumbTintColor.ToAndroid() :
								// Have to hard code default thumb color...
								// Xamarin.Android doesn't have the needed ColorSwitchThumbNormal ID defined yet
								new Android.Graphics.Color(250, 250, 250);

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = (thumbOnTintColor != Color.Default) ?
								thumbOnTintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlActivated(control.Context, defaultColor));

					var colorList = new ColorStateList(states, colors);
					control.ThumbTintList = colorList;
				}
			}
		}
	}
}