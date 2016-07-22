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
	public class SliderGloss
	{
		#region MaxTrackTintColor

		public static readonly BindableProperty MaxTrackTintColorProperty =
			BindableProperty.CreateAttached("MaxTrackTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		public static Color GetMaxTrackTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(MaxTrackTintColorProperty);
		}

		public static void SetMaxTrackTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(MaxTrackTintColorProperty, value);
		}

		#endregion

		#region MinTrackTintColor

		public static readonly BindableProperty MinTrackTintColorProperty =
			BindableProperty.CreateAttached("MinTrackTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		public static Color GetMinTrackTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(MinTrackTintColorProperty);
		}

		public static void SetMinTrackTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(MinTrackTintColorProperty, value);
		}

		#endregion

		#region TintColor

		public static readonly BindableProperty ThumbTintColorProperty =
			BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.Slider), Color.Default);

		public static Color GetThumbTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(ThumbTintColorProperty);
		}

		public static void SetThumbTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(ThumbTintColorProperty, value);
		}

		#endregion

		#region Instance properties

		WeakReference<BindableObject> _bindable;

		public SliderGloss(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

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

		public Color MaxTrackTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SliderGloss.GetMaxTrackTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SliderGloss.SetMaxTrackTintColor(bindable, value);
				}
			}
		}

		public Color MinTrackTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SliderGloss.GetMinTrackTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SliderGloss.SetMinTrackTintColor(bindable, value);
				}
			}
		}

		public Color ThumbTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SliderGloss.GetThumbTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SliderGloss.SetThumbTintColor(bindable, value);
				}
			}
		}

		#endregion
	}
}