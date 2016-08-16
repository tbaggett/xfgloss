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
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Utils;
using AColor = Android.Graphics.Color;
using ASwitch = Android.Widget.Switch;
using ASwitchCompat = Android.Support.V7.Widget.SwitchCompat;

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
			ApplyColorProperty(control, properties, propertyName);
		}

		/// <summary>
		/// An extension method that applies all of the current properties defined by the passed
		/// <see cref="T:XFGloss.ISwitchGloss"/> interface implementation to the Android SwitchCompat control
		/// </summary>
		/// <param name="control">Control.</param>
		/// <param name="properties">Properties.</param>
		/// <param name="propertyName">Property name.</param>
		public static void UpdateColorProperty(this Android.Support.V7.Widget.SwitchCompat control,
											   ISwitchGloss properties,
											   string propertyName)
		{
			// Use the internal ApplyColorProperty method in XFGlossSwitchExtensions to handle our color updates
			XFGlossSwitchExtensions.ApplyColorProperty(control, properties, propertyName);
		}

		/// <summary>
		/// Internal method used to do the work on behalf of the UpdateColorProperty extension method for both
		/// XFGlossSwitchExtensions and XFGlossSwitchCompatExtensions
		/// </summary>
		/// <param name="control">Control.</param>
		/// <param name="properties">Properties.</param>
		/// <param name="propertyName">Property name.</param>
		/// <typeparam name="TControl">The 1st type parameter.</typeparam>
		static void ApplyColorProperty<TControl>(TControl control, ISwitchGloss properties, string propertyName)
		{
			// We have to create a multiple state color list to set both the "off" and "on" (checked/unchecked)
			// states of the switch control.

			bool isSwitch = Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M && 
	                        control is ASwitch;

			bool isSwitchCompat = !isSwitch &&
								  XFGloss.Droid.Library.UsingAppCompat &&
								  control is ASwitchCompat;

			Android.Content.Context controlContext = (isSwitch) ? (control as ASwitch).Context :
													 (isSwitchCompat) ? (control as ASwitchCompat).Context :
													 null;

			int[][] states = new int[2][];
			int[] colors = new int[2];

			if (propertyName == null ||
			    propertyName == SwitchGloss.TintColorProperty.PropertyName ||
			    propertyName == SwitchGloss.OnTintColorProperty.PropertyName)
			{
				var tintColor = properties.TintColor;
				var onTintColor = properties.OnTintColor;

				// Skip assigning anything if all properties are being applied and the color is set to the default value
				if (isSwitchCompat || propertyName != null || tintColor != Color.Default || onTintColor != Color.Default)
				{
					var aTintColor = (tintColor != Color.Default) ?
									 tintColor.ToAndroid() :
									 new AColor(ThemeUtil.ColorControlNormal(controlContext,
									 ThemeUtil.DefaultColorControlTrack));

					var aOnTintColor = (onTintColor != Color.Default) ?
										onTintColor.ToAndroid() :
										new AColor(ThemeUtil.ColorControlActivated(controlContext,
												   ThemeUtil.DefaultColorControlTrackActivated));
					
					// Clamp the track tint colors to 30% opacity - API 24 automatically does this. AppCompat doesn't.
					if (isSwitchCompat)
					{
						aTintColor = new AColor(aTintColor.R, aTintColor.G, aTintColor.B, (byte)77);
						aOnTintColor = new AColor(aOnTintColor.R, aOnTintColor.G, aOnTintColor.B, (byte)77);
					}

					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = aTintColor;

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = aOnTintColor;

					var colorList = new ColorStateList(states, colors);

					if (isSwitch)
					{
						(control as ASwitch).TrackTintList = colorList;
					}
					else if (isSwitchCompat)
					{
						DrawableCompat.SetTintList((control as ASwitchCompat).TrackDrawable, colorList);
					}
					else
					{
						Console.WriteLine(XFGloss.Droid.Library.appCompatWarning);
					}
				}
			}

			if (propertyName == null ||
			    propertyName == SwitchGloss.ThumbTintColorProperty.PropertyName ||
			    propertyName == SwitchGloss.ThumbOnTintColorProperty.PropertyName)
			{
				var thumbTintColor = properties.ThumbTintColor;
				var thumbOnTintColor = properties.ThumbOnTintColor;

				// Skip assigning anything if all properties are being applied and the color is set to the default value
				if (propertyName != null || thumbTintColor != Color.Default || thumbOnTintColor != Color.Default)
				{
					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = (thumbTintColor != Color.Default) ?
								thumbTintColor.ToAndroid() :
							  	ThemeUtil.DefaultColorControlThumb;

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = (thumbOnTintColor != Color.Default) ?
								thumbOnTintColor.ToAndroid() :
								new AColor(ThemeUtil.ColorControlActivated(controlContext,
																		   ThemeUtil.DefaultColorControlThumbActivated));

					var colorList = new ColorStateList(states, colors);

					if (isSwitch)
					{
						(control as ASwitch).ThumbTintList = colorList;
					}
					else if (isSwitchCompat)
					{
						DrawableCompat.SetTintList((control as ASwitchCompat).ThumbDrawable, colorList);
					}
					else
					{
						Console.WriteLine(XFGloss.Droid.Library.appCompatWarning);
					}
				}
			}
		}
	}
}