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
using Android.Runtime;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V7.Widget;
using Android.Widget;
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
	public class XFGlossSliderRenderer : ViewRenderer<Slider, SeekBar>, SeekBar.IOnSeekBarChangeListener
	{
		double _max;
		double _min;

		double Value
		{
			get { return _min + (_max - _min) * ((double)base.Control.Progress / 1000.0); }
			set
			{
				if (Math.Abs(value - Value) > Double.Epsilon)
				{
					base.Control.Progress = (int)((value - _min) / (_max - _min) * 1000.0);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Droid.Renderers.XFGlossSliderRenderer"/> class.
		/// </summary>
		public XFGlossSliderRenderer()
		{
			AutoPackage = false;
		}

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

			// BEGIN default XF SliderRenderer implementation
			if (e.OldElement == null)
			{
				SeekBar ctrl = null;
				if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.M &&
					XFGloss.Droid.Library.UsingAppCompat)
				{
					// We want to create an AppCompatSeekBar instance if we're running on pre-Marshmallow and using the
					// AppCompat library
					ctrl = new AppCompatSeekBar(Context);
				}
				else
				{
					// If we're running on Marshmallow or newer, or aren't using the AppCompat library, create a
					// standard SeekBar control
					ctrl = new SeekBar(Context);
				}
				if (ctrl != null)
				{
					SetNativeControl(ctrl);
					ctrl.Max = 1000;
					ctrl.SetOnSeekBarChangeListener(this);
				}

				Slider newElement = e.NewElement;
				_min = newElement.Minimum;
				_max = newElement.Maximum;
				Value = newElement.Value;
			}
			// END default XF SliderRenderer implementation

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

			// BEGIN default XF SliderRenderer implementation
			Slider element = Element;
			switch (e.PropertyName)
			{
				case nameof(Slider.Maximum):
					_max = element.Maximum;
					break;
					
				case nameof(Slider.Minimum):
					_min = element.Minimum;
					break;

				case nameof(Slider.Value):
					Value = element.Value;
					break;
			}
			Value = element.Value;
			// END default XF SliderRenderer implementation

			if (e.PropertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.MinTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				UpdateSliderProperties(e.PropertyName);
			}
		}

		/// <summary>
		/// Handle the slider control layout
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="l">The left position</param>
		/// <param name="t">The top position</param>
		/// <param name="r">The right position</param>
		/// <param name="b">The bottom position</param>
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
			{
				if (Control == null)
				{
					return;
				}

				SeekBar ctrl = Control;
				Drawable thumb = ctrl.Thumb;
				int thumbTop = ctrl.Height / 2 - thumb.IntrinsicHeight / 2;

				thumb.SetBounds(thumb.Bounds.Left, thumbTop, 
				                thumb.Bounds.Left + thumb.IntrinsicWidth, thumbTop + thumb.IntrinsicHeight);
			}
		}

		/// <summary>
		/// Method required for SeekBar.IOnSeekBarChangeListener implementation. Notifies us when the native control's
		/// value changes so we can transfer the new value back to the XF element.
		/// </summary>
		/// <param name="seekBar">Seek bar.</param>
		/// <param name="progress">Current progress value</param>
		/// <param name="fromUser">From User flag</param>
		void SeekBar.IOnSeekBarChangeListener.OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
		{
			((IElementController)Element).SetValueFromRenderer(Slider.ValueProperty, Value);
		}

		/// <summary>
		/// Method required for SeekBar.IOnSeekBarChangeListener implementation
		/// </summary>
		/// <param name="seekBar">Seek bar.</param>
		void SeekBar.IOnSeekBarChangeListener.OnStartTrackingTouch(SeekBar seekBar)
		{
		}

		/// <summary>
		/// Method required for SeekBar.IOnSeekBarChangeListener implementation
		/// </summary>
		/// <param name="seekBar">Seek bar.</param>
		void SeekBar.IOnSeekBarChangeListener.OnStopTrackingTouch(SeekBar seekBar)
		{
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

			// MaxTrackTintColor Property
			ApplyMaxTrackTintColor(propertyName);

			// MinTrackTintColor Property
			ApplyMinTrackTintColor(propertyName);

			// ThumbTintColor Property
			ApplyThumbTintColor(propertyName);
		}

		void ApplyMaxTrackTintColor(string propertyName, bool forceApply = false)
		{
			// We always want to force the application of the MaxTrackTintColor if we're running an API that is older
			// than Marshmallow and we're also using the AppCompat library so the track color will be appropriately
			// clamped.
			forceApply = forceApply || 
						 (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.M && 
				          XFGloss.Droid.Library.UsingAppCompat);

			if (propertyName == null ||
				propertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName ||
				forceApply)
			{
				var maxTrackTintColor = (Color)Element.GetValue(SliderGloss.MaxTrackTintColorProperty);
				// Skip assigning anything if all properties are being applied and the color is set to the default value
				if (propertyName != null || maxTrackTintColor != Color.Default || forceApply)
				{
					if (maxTrackTintColor != Color.Default)
					{
						maxTrackTintColor = new Color(maxTrackTintColor.R, maxTrackTintColor.G, maxTrackTintColor.B, 0.3);
					}

					var aMaxTrackTintColor =
										(maxTrackTintColor == Color.Default) ?
										ThemeUtil.IntToColor(ThemeUtil.ColorControlNormal(Control.Context,
										ThemeUtil.DefaultColorControlTrack)) :
												 maxTrackTintColor.ToAndroid();

					// Clamp the track tint colors to 30% opacity - API 24 automatically does this. AppCompat doesn't.
					aMaxTrackTintColor = new AColor(aMaxTrackTintColor.R, 
					                                aMaxTrackTintColor.G, 
					                                aMaxTrackTintColor.B, 
					                                (byte)77);

					if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
					{
						// FYI: Lollipop (API 21) has the ProgressBackgroundTintList implemented but it is broken.
						// Assigning a value to the property has no effect on API 21. It works as expected on API 22+.
						Control.ProgressBackgroundTintList = ColorStateList.ValueOf(aMaxTrackTintColor);
					}
					else if (XFGloss.Droid.Library.UsingAppCompat)
					{
						ThemeUtil.SetLayerTint(Control.ProgressDrawable as LayerDrawable, 0, aMaxTrackTintColor);
					}
					else
					{
						Console.WriteLine(XFGloss.Droid.Library.appCompatWarning);
					}
				}
			}
		}

		void ApplyMinTrackTintColor(string propertyName)
		{
			if (propertyName == null || propertyName == SliderGloss.MinTrackTintColorProperty.PropertyName)
			{
				var minTrackTintColor = (Color)Element.GetValue(SliderGloss.MinTrackTintColorProperty);
				// Skip assigning anything if all properties are being applied and the color is set to the default value
				if (propertyName != null || minTrackTintColor != Color.Default)
				{
					var aMinTrackTintColor = (minTrackTintColor == Color.Default) ?
										ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context,
															 ThemeUtil.DefaultColorControlTrackActivated)) :
															 minTrackTintColor.ToAndroid();

					if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
					{
						// FYI: Lollipop (API 21) has a broken implementation for the ProgressTintList property.
						// Assigning a value to the property causes the entire track to be colored with the assigned
						// value, instead of just the track to the left of the thumb. It works as expected on API 22+.
						Control.ProgressTintList = ColorStateList.ValueOf(aMinTrackTintColor);
					}
					else if (XFGloss.Droid.Library.UsingAppCompat)
					{
						DrawableCompat.SetTint(DrawableCompat.Wrap(Control.ProgressDrawable), aMinTrackTintColor);
						// We also have to explicitly set the MaxTrackTintColor to either the specified custom color or
						// the default color. Otherwise, setting the MinTrackTintColor tints both the left and right
						// sides of the track
						ApplyMaxTrackTintColor(null, true);
					}
					else
					{
						Console.WriteLine(XFGloss.Droid.Library.appCompatWarning);
					}
				}
			}
		}

		void ApplyThumbTintColor(string propertyName)
		{
			if (propertyName == null || propertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				var thumbTintColor = (Color)Element.GetValue(SliderGloss.ThumbTintColorProperty);
				// Skip assigning anything if all properties are being applied and the color is set to the default value
				if (propertyName != null || thumbTintColor != Color.Default)
				{
					var aThumbTintColor = (thumbTintColor == Color.Default) ?
										  ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context,
										  ThemeUtil.DefaultColorControlThumb)) :
										  thumbTintColor.ToAndroid();

					if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
					{
						Control.ThumbTintList = ColorStateList.ValueOf(aThumbTintColor);

					}
					else if (XFGloss.Droid.Library.UsingAppCompat)
					{
						DrawableCompat.SetTint(DrawableCompat.Wrap(Control.Thumb), aThumbTintColor);
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