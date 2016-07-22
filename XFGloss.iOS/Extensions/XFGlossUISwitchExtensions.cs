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
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XFGloss.iOS.Extensions
{
	public static class XFGlossUISwitchExtensions
	{
		public static void UpdateColorProperty(this UISwitch control, ISwitchGloss properties, string propertyName)
		{
			if (control == null || properties == null)
			{
				return;
			}

			// TintColor property
			if (propertyName == null ||
			    propertyName == SwitchGloss.TintColorProperty.PropertyName)
			{
				var tintColor = properties.TintColor;
				if (tintColor != Color.Default)
				{
					control.TintColor = tintColor.ToUIColor();
				}
			}

			// OnTintColor property
			if (propertyName == null ||
			    propertyName == SwitchGloss.OnTintColorProperty.PropertyName)
			{
				var onTintColor = properties.OnTintColor;
				if (onTintColor != Color.Default)
				{
					control.OnTintColor = onTintColor.ToUIColor();
				}
			}

			// We always have to assign the thumb color since we are faking separate on and off thumb color states on iOS
			// Recycled cells won't be updated if we don't handle this.
			Color thumbTintColor = (control.On) ? properties.ThumbOnTintColor : properties.ThumbTintColor;
			UIColor uiThumbTintColor = (thumbTintColor == Color.Default) ? null : thumbTintColor.ToUIColor();

			// Special handling of switch value changing - delay applying property changes until
			// switch state change animation has completed
			if (propertyName == Switch.IsToggledProperty.PropertyName)
			{
				// Create a weak reference to the UISwitch control to avoid capturing a strong reference in the Task lambda
				WeakReference<UISwitch> controlRef = new WeakReference<UISwitch>(control);

				Task.Delay(100).ContinueWith(t =>
				{
					UISwitch switchCtrl;
					if (controlRef.TryGetTarget(out switchCtrl))
					{
						switchCtrl.ThumbTintColor = uiThumbTintColor;
					}
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}
			else
			{
				// If the value isn't being changed, set the thumb tint color immediately
				control.ThumbTintColor = uiThumbTintColor;
			}
		}
	}
}