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
	/// Provides attached bindable properties for use with <see cref="Xamarin.Forms.Cell"/> derived classes and CellGloss 
	/// instance-based accessors.
	/// </summary>
	public class CellGloss
	{
		#region AccessoryType
		/// <summary>
		/// Allows a <see cref="T:XFGloss.CellGlossAccessoryType"/> to be set on cells when running on the iOS platform.
		/// </summary>
		public static readonly BindableProperty AccessoryTypeProperty =
			BindableProperty.CreateAttached("AccessoryType", typeof(CellGlossAccessoryType), typeof(Cell), 
			                                CellGlossAccessoryType.None);
		/// <summary>
		/// Gets the <see cref="T:XFGloss.CellGlossAccessoryType"/> value that is currently assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The assigned <see cref="T:XFGloss.CellGlossAccessoryType"/>.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the AccessoryType attached 
		/// property value should be retrieved from.</param>
		public static CellGlossAccessoryType GetAccessoryType(BindableObject bindable)
		{
			return (CellGlossAccessoryType)(bindable?.GetValue(AccessoryTypeProperty) ?? 
			                                AccessoryTypeProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:XFGloss.CellGlossAccessoryType"/> value as the currently assigned accessory type for
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the AccessoryType attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:XFGloss.CellGlossAccessoryType"/> that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetAccessoryType(BindableObject bindable, CellGlossAccessoryType value)
		{
			bindable?.SetValue(AccessoryTypeProperty, value);
		}

		#endregion

		// AccessoryDetailAction is disabled until the needed access is added to the Xamarin.Forms.ListView control
		/*
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
		*/

		#region BackgroundColor
		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be assigned as the background color for cells.
		/// </summary>
		public static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(Cell), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> value that is currently assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned background color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the BackgroundColor attached 
		/// property value should be retrieved from.</param>
		public static Color GetBackgroundColor(BindableObject bindable)
		{
			return (Color)(bindable?.GetValue(BackgroundColorProperty) ??
						   BackgroundColorProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:Xamarin.Forms.Color"/> value as the currently assigned background color for
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the BackgroundColor attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:Xamarin.Forms.Color"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetBackgroundColor(BindableObject bindable, Color value)
		{
			bindable?.SetValue(BackgroundColorProperty, value);
		}

		#endregion

		#region BackgroundGradient

		/// <summary>
		/// Allows a <see cref="T:XFGloss.Gradient"/> instance to be assigned as a multiple step gradient fill to cells.
		/// </summary>
		public static readonly BindableProperty BackgroundGradientProperty =
			BindableProperty.CreateAttached("BackgroundGradient", typeof(Gradient), typeof(Cell), null);

		/// <summary>
		/// Gets the <see cref="T:XFGloss.Gradient"/> instance that is currently assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned background gradient or null if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the BackgroundGradient attached 
		/// property value should be retrieved from.</param>
		public static Gradient GetBackgroundGradient(BindableObject bindable)
		{
			return (Gradient)(bindable?.GetValue(BackgroundGradientProperty) ??
							  BackgroundGradientProperty.DefaultValue);
		}

		/// <summary>
		/// Sets the passed <see cref="T:XFGloss.Gradient"/> value as the currently assigned background gradient for
		/// the passed <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the BackgroundGradient attached 
		/// property value should be assigned to.</param>
		/// <param name="value">The <see cref="T:XFGloss.Gradient"/> value that should be assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.</param>
		public static void SetBackgroundGradient(BindableObject bindable, Gradient value)
		{
			bindable?.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region TintColor
		/// <summary>
		/// Allows a <see cref="T:Xamarin.Forms.Color"/> value to be specified as the tint color for the accessory view
		/// (iOS only) and the <see cref="T:Xamarin.Forms.SwitchCell"/> cell type.
		/// </summary>
		public static readonly BindableProperty TintColorProperty =
			BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Cell), Color.Default);

		/// <summary>
		/// Gets the <see cref="T:Xamarin.Forms.Color"/> value that is currently assigned to the passed
		/// <see cref="T:Xamarin.Forms.BindableObject"/>.
		/// </summary>
		/// <returns>The currently assigned tint color or Color.Default if nothing is assigned.</returns>
		/// <param name="bindable">The <see cref="T:Xamarin.Forms.BindableObject"/> that the TintColor attached 
		/// property value should be retrieved from.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.CellGloss"/> class. Used as a convenient way to assign
		/// multiple XFGloss property values to the target <see cref="Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		public CellGloss(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.BindableObject"/> that the <see cref="T:XFGloss.CellGloss"/>
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
		/// Gets or sets the <see cref="T:XFGloss.CellGlossAccessoryType"/> value for the assigned 
		/// <see cref="T:XFGloss.CellGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The type of the accessory.</value>
		public CellGlossAccessoryType AccessoryType
		{
			get { return GetAccessoryType(Bindable); }
			set { SetAccessoryType(Bindable, value); }
		}

		// AccessoryDetailAction is disabled until the needed access is added to the Xamarin.Forms.ListView control
		/*
		public Action<CellGloss> AccessoryDetailAction
		{
			get { return GetAccessoryDetailAction(Bindable); }
			set { SetAccessoryDetailAction(Bindable, value); }
		}
		*/

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the background color for the assigned
		/// <see cref="T:XFGloss.CellGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the background.</value>
		public Color BackgroundColor
		{
			get { return GetBackgroundColor(Bindable); }
			set { SetBackgroundColor(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:XFGloss.Gradient"/> instance of the background gradient for the assigned
		/// <see cref="T:XFGloss.CellGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The background gradient.</value>
		public Gradient BackgroundGradient
		{
			get { return GetBackgroundGradient(Bindable); }
			set { SetBackgroundGradient(Bindable, value); }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value for the tinting of the accessory view (iOS only)
		/// or the <see cref="T:Xamarin.Forms.SwitchCell"/> cell type for the assigned 
		/// <see cref="T:XFGloss.CellGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The color of the tint.</value>
		public Color TintColor
		{
			get { return GetTintColor(Bindable); }
			set { SetTintColor(Bindable, value); }
		}
		#endregion
	}
}