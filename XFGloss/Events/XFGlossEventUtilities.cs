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
	public static class XFGlossEventUtilities
	{
		#region BindableObject Weak Event Extensions

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

