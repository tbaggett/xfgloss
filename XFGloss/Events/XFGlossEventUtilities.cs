// /*
//  * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
//  * http://github.com/tbaggett
//  * http://twitter.com/tbaggett
//  * http://tommyb.com
//  * http://ansuria.com
//  * 
//  * The MIT License (MIT) see GitHub For more information
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */
//
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XFGloss
{
	/// <summary>
	/// Static extension method helper class to make it easier to register/deregister a property changed handler for a 
	/// property from a bindable object.
	/// </summary>
	public static class XFGlossEventUtilities
	{
		#region BindableObject Weak Event Extensions

		/// <summary>
		/// Registers a property changed handler for the specified bindable object's property.
		/// </summary>
		/// <returns>The bindable property changed handler.</returns>
		/// <param name="bindable">Bindable.</param>
		/// <param name="bindableProperty">Bindable property.</param>
		/// <param name="handler">Handler.</param>
		public static INotifyPropertyChanged RegisterBindablePropertyChangedHandler(this BindableObject bindable,
																  BindableProperty bindableProperty,
																  EventHandler<PropertyChangedEventArgs> handler)
		{
			var changeableProperty = (INotifyPropertyChanged)bindable.GetValue(bindableProperty);
			if (changeableProperty != null)
			{
				WeakEvent.RegisterEvent<INotifyPropertyChanged, PropertyChangedEventArgs>(changeableProperty,
																			nameof(changeableProperty.PropertyChanged),
																			handler);
			}

			return changeableProperty;
		}

		/// <summary>
		/// Deregisters a property changed handler for the specified bindable object's property.
		/// </summary>
		/// <returns>The bindable property changed handler.</returns>
		/// <param name="bindable">Bindable.</param>
		/// <param name="bindableProperty">Bindable property.</param>
		/// <param name="handler">Handler.</param>
		public static INotifyPropertyChanged DeregisterBindablePropertyChangedHandler(this BindableObject bindable,
																	BindableProperty bindableProperty,
																	EventHandler<PropertyChangedEventArgs> handler)
		{
			var changeableProperty = (INotifyPropertyChanged)bindable.GetValue(bindableProperty);
			if (changeableProperty != null)
			{
				WeakEvent.DeregisterEvent<INotifyPropertyChanged, PropertyChangedEventArgs>(changeableProperty,
																			  nameof(changeableProperty.PropertyChanged),
																			  handler);
			}

			return changeableProperty;
		}

		#endregion
	}
}