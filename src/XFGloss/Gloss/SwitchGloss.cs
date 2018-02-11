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
	/// Provides attached bindable properties for use with the <see cref="Xamarin.Forms.Switch"/> control and 
	/// SwitchGloss instance-based accessors.
	/// </summary>
	public class SwitchGloss : ISwitchGloss
	{
		#region TintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the track tint color when the Switch 
		/// control is set to the "off" position.
		/// </summary>
		public static readonly BindableProperty TintColorProperty =
            BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> track tint color value for the Switch control's "off" position 
		/// that is currently assigned to the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the TintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(TintColorProperty) ??
						   TintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned track tint color for the
		/// Switch control's "off" position for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the TintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(TintColorProperty, value);
		}

		#endregion

		#region OnTintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the track tint color when the Switch 
		/// control is set to the "on" position.
		/// </summary>
		public static readonly BindableProperty OnTintColorProperty =
            BindableProperty.CreateAttached("OnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> track tint color value for the Switch control's "on" position 
		/// that is currently assigned to the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the OnTintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetOnTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(OnTintColorProperty) ??
						   OnTintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned track tint color for the
		/// Switch control's "on" position for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the OnTintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetOnTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(OnTintColorProperty, value);
		}

		#endregion

		#region ThumbTintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the thumb tint color when the
		/// Switch control is set to the "off" position.
		/// </summary>
		public static readonly BindableProperty ThumbTintColorProperty =
            BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> thumb tint color value for the Switch control's "off" position 
		/// that is currently assigned to the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
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
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned thumb tint color for the
		/// Switch control's "off" position for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
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

		#region ThumbOnTintColor

		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the thumb tint color when the
		/// Switch control is set to the "on" position.
		/// </summary>
		public static readonly BindableProperty ThumbOnTintColorProperty =
            BindableProperty.CreateAttached("ThumbOnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> thumb tint color value for the Switch control's "on" position 
		/// that is currently assigned to the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the ThumbOnTintColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetThumbOnTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(ThumbOnTintColorProperty) ??
						   ThumbOnTintColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned thumb tint color for the
		/// Switch control's "on" position for the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the ThumbOnTintColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetThumbOnTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(ThumbOnTintColorProperty, value);
		}

		#endregion

		#region Interface implementation

		WeakReference<BindableObject> _bindable;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.SwitchGloss"/> class. Used as a convenient way to assign
		/// multiple XFGloss property values to the target <see cref="Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		public SwitchGloss(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.BindableObject"/> that the <see cref="T:XFGloss.SwitchGloss"/>
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
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the track tint color when the Switch control 
		/// is in the "off" position for the assigned <see cref="T:XFGloss.SwitchGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		public Color TintColor
		{
			get { return GetTintColor(Bindable); }
			set { SetTintColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the track tint color when the Switch control 
		/// is in the "on" position for the assigned <see cref="T:XFGloss.SwitchGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		public Color OnTintColor
		{
			get { return GetOnTintColor(Bindable); }
			set { SetOnTintColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the thumb tint color when the Switch control 
		/// is in the "off" position for the assigned <see cref="T:XFGloss.SwitchGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		public Color ThumbTintColor
		{
			get { return GetThumbTintColor(Bindable); }
			set { SetThumbTintColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the thumb tint color when the Switch control 
		/// is in the "on" position for the assigned <see cref="T:XFGloss.SwitchGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		public Color ThumbOnTintColor
		{
			get { return GetThumbOnTintColor(Bindable); }
			set { SetThumbOnTintColor(Bindable, value); }
		}
		#endregion
	}
}