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
	public class CellGloss
	{
		#region AccessoryType

		public static readonly BindableProperty AccessoryTypeProperty =
			BindableProperty.CreateAttached("AccessoryType", typeof(CellGlossAccessoryType), typeof(Cell), 
			                                CellGlossAccessoryType.None);

		public static CellGlossAccessoryType GetAccessoryType(BindableObject bindable)
		{
			return (CellGlossAccessoryType)(bindable?.GetValue(AccessoryTypeProperty) ?? 
			                                AccessoryTypeProperty.DefaultValue);
		}

		public static void SetAccessoryType(BindableObject bindable, CellGlossAccessoryType value)
		{
			bindable?.SetValue(AccessoryTypeProperty, value);
		}

		#endregion

		#region AccessoryDetailAction

		public static readonly BindableProperty AccessoryDetailActionProperty =
			BindableProperty.CreateAttached("AccessoryDetailAction", typeof(Action<CellGloss>), typeof(Cell), null);

		public static Action<CellGloss> GetAccessoryDetailAction(BindableObject bindable)
		{
			return (Action<CellGloss>)(bindable?.GetValue(AccessoryDetailActionProperty) ??
									   AccessoryDetailActionProperty.DefaultValue);
		}

		public static void SetAccessoryDetailAction(BindableObject bindable, Action<CellGloss> value)
		{
			bindable?.SetValue(AccessoryDetailActionProperty, value);
		}

		#endregion

		#region BackgroundColor

		public static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(Cell), Color.Default);

		public static Color GetBackgroundColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(BackgroundColorProperty) ??
						   BackgroundColorProperty.DefaultValue);
		}

		public static void SetBackgroundColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(BackgroundColorProperty, value);
		}

		#endregion

		#region BackgroundGradient

		public static readonly BindableProperty BackgroundGradientProperty =
			BindableProperty.CreateAttached("BackgroundGradient", typeof(Gradient), typeof(Cell), null);

		public static Gradient GetBackgroundGradient(BindableObject bindable)
		{
			return (Gradient)(bindable?.GetValue(BackgroundGradientProperty) ??
							  BackgroundGradientProperty.DefaultValue);
		}

		public static void SetBackgroundGradient(BindableObject bindable, Gradient value)
		{
			bindable?.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region TintColor

		public static readonly BindableProperty TintColorProperty =
			BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Cell), Color.Default);

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

		#region Instance access

		WeakReference<BindableObject> _bindable;

		public CellGloss(BindableObject bindable)
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

		public CellGlossAccessoryType AccessoryType
		{
			get { return GetAccessoryType(Bindable); }
			set { SetAccessoryType(Bindable, value); }
		}

		public Action<CellGloss> AccessoryDetailAction
		{
			get { return GetAccessoryDetailAction(Bindable); }
			set { SetAccessoryDetailAction(Bindable, value); }
		}

		public Color BackgroundColor
		{
			get { return GetBackgroundColor(Bindable); }
			set { SetBackgroundColor(Bindable, value); }
		}

		public Gradient BackgroundGradient
		{
			get { return GetBackgroundGradient(Bindable); }
			set { SetBackgroundGradient(Bindable, value); }
		}

		public Color TintColor
		{
			get { return GetTintColor(Bindable); }
			set { SetTintColor(Bindable, value); }
		}
		#endregion
	}
}