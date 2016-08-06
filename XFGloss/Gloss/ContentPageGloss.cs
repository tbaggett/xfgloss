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
	/// Provides attached bindable properties for use with <see cref="T:Xamarin.Forms.ContentPage"/> derived classes and
	/// CellGloss instance-based accessors.
	/// </summary>
	public class ContentPageGloss
	{
		#region BackgroundGradient

		/// <summary>
		/// Allows a <see cref="T:XFGloss.Gradient"/> instance to be assigned as a multiple step gradient fill to
		/// <see cref="T:Xamarin.Forms.ContentPage"/> derived classes.
		/// </summary>
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

		#region Instance access

		WeakReference<BindableObject> _bindable;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.ContentPageGloss"/> class. Used as a convenient way 
		/// to assign multiple XFGloss property values to the target <see cref="Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		public ContentPageGloss(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.BindableObject"/> that the 
		/// <see cref="T:XFGloss.ContentPageGloss"/> instance methods will retrieve values from or assign values to.
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
		/// Gets or sets the <see cref="T:XFGloss.Gradient"/> instance of the background gradient for the assigned
		/// <see cref="T:XFGloss.ContentPageGloss.Bindable"/> instance.
		/// </summary>
		/// <value>The background gradient.</value>
		public Gradient BackgroundGradient
		{
			get { return GetBackgroundGradient(Bindable); }
			set { SetBackgroundGradient(Bindable, value); }
		}

		#endregion
	}
}