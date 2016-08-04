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
	public class SwitchGloss : ISwitchGloss
	{
		#region TintColor

		public static readonly BindableProperty TintColorProperty =
            BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		public static Color GetTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(TintColorProperty) ??
						   TintColorProperty.DefaultValue);
		}

		public static void SetTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(TintColorProperty, value);
		}

		#endregion

		#region OnTintColor

		public static readonly BindableProperty OnTintColorProperty =
            BindableProperty.CreateAttached("OnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		public static Color GetOnTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(OnTintColorProperty) ??
						   OnTintColorProperty.DefaultValue);
		}

		public static void SetOnTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(OnTintColorProperty, value);
		}

		#endregion

		#region ThumbTintColor

		public static readonly BindableProperty ThumbTintColorProperty =
            BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		public static Color GetThumbTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(ThumbTintColorProperty) ??
						   ThumbTintColorProperty.DefaultValue);
		}

		public static void SetThumbTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(ThumbTintColorProperty, value);
		}

		#endregion

		#region ThumbOnTintColor

		public static readonly BindableProperty ThumbOnTintColorProperty =
            BindableProperty.CreateAttached("ThumbOnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		public static Color GetThumbOnTintColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(ThumbOnTintColorProperty) ??
						   ThumbOnTintColorProperty.DefaultValue);
		}

		public static void SetThumbOnTintColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(ThumbOnTintColorProperty, value);
		}

		#endregion

		#region Interface implementation

		WeakReference<BindableObject> _bindable;

		public SwitchGloss(BindableObject bindable)
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

		public Color TintColor
		{
			get { return GetTintColor(Bindable); }
			set { SetTintColor(Bindable, value); }
		}

		public Color OnTintColor
		{
			get { return GetOnTintColor(Bindable); }
			set { SetOnTintColor(Bindable, value); }
		}

		public Color ThumbTintColor
		{
			get { return GetThumbTintColor(Bindable); }
			set { SetThumbTintColor(Bindable, value); }
		}

		public Color ThumbOnTintColor
		{
			get { return GetThumbOnTintColor(Bindable); }
			set { SetThumbOnTintColor(Bindable, value); }
		}
		#endregion
	}
}