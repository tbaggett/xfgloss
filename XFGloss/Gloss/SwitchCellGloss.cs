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

using Xamarin.Forms;

namespace XFGloss
{
	public class SwitchCellGloss : CellGloss, ISwitchGloss
	{
		#region OnTintColor

		public static readonly BindableProperty OnTintColorProperty =
			BindableProperty.CreateAttached("OnTintColor", typeof(Color), typeof(SwitchCell), Color.Default);

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
			BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(SwitchCell), Color.Default);

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
			BindableProperty.CreateAttached("ThumbOnTintColor", typeof(Color), typeof(SwitchCell), Color.Default);

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

		public SwitchCellGloss(BindableObject bindable) : base(bindable)
		{
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