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
using Xamarin.Forms;

namespace XFGloss
{
	/// <summary>
	/// Provides attached bindable properties for use with the <see cref="Xamarin.Forms.Slider"/> control and SliderGloss 
	/// instance-based accessors.
	/// </summary>
	public class SliderGloss
	{
		#region MaxTrackTintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the tint color for the track to the
		/// right of the thumb's current position.
		/// </summary>
		public static readonly BindableProperty MaxTrackTintColorProperty =
			BindableProperty.CreateAttached("MaxTrackTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> maximum track tint color value that is currently assigned to 
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the MaxTrackTintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetMaxTrackTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(MaxTrackTintColorProperty) ??
						   MaxTrackTintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned maximum track tint color
		/// for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the MaxTrackTintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetMaxTrackTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(MaxTrackTintColorProperty, value);
		}

		#endregion

		#region MinTrackTintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the tint color for the track to the
		/// left of the thumb's current position.
		/// </summary>
		public static readonly BindableProperty MinTrackTintColorProperty =
			BindableProperty.CreateAttached("MinTrackTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> minimum track tint color value that is currently assigned to 
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the MinTrackTintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetMinTrackTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(MinTrackTintColorProperty) ??
						   MinTrackTintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned minimum track tint color
		/// for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the MinTrackTintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetMinTrackTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(MinTrackTintColorProperty, value);
		}

		#endregion

		#region TintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned  as the tint color for the Slider 
		/// control's thumb.
		/// </summary>
		public static readonly BindableProperty ThumbTintColorProperty =
			BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> thumb tint color value that is currently assigned to 
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the ThumbTintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetThumbTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(ThumbTintColorProperty) ??
						   ThumbTintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned thumb tint color
		/// for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the ThumbTintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetThumbTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(ThumbTintColorProperty, value);
		}

		#endregion

		#region Instance properties

		WeakReference<BindableObject> _bindable;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.SliderGloss"/> class. Used as a convenient way to assign
		/// multiple XFGloss property values to the target <see cref="Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		public SliderGloss(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.BindableObject"/> that the <see cref="T:XFGloss.SliderGloss"/>
		/// instance methods will retrieve values from or assign values to.
		/// </summary>
		/// <value>The bindable.</value>
		public BindableObject Bindable
		{
			get
			{
				BindableObject bindable;
				if (_bindable.TryGetTarget(out bindable))
				{
					return bindable;
				}

				return null;
			}
			set
			{
				_bindable.SetTarget(value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the maximum track tint color for the assigned
		/// <see cref="T:XFGloss.SliderGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the maximum track tinting.</value>
		public Color MaxTrackTintColor
		{
			get { return GetMaxTrackTintColor(Bindable); }
			set { SetMaxTrackTintColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the minimum track tint color for the assigned
		/// <see cref="T:XFGloss.SliderGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the minimum track tinting.</value>
		public Color MinTrackTintColor
		{
			get { return GetMinTrackTintColor(Bindable); }
			set { SetMinTrackTintColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the thumb tint color for the assigned
		/// <see cref="T:XFGloss.SliderGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the maximum track tinting.</value>
		public Color ThumbTintColor
		{
			get { return GetThumbTintColor(Bindable); }
			set { SetThumbTintColor(Bindable, value); }
		}

		#endregion
	}
}