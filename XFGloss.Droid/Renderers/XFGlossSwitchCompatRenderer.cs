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
using XFGloss.Droid.Extensions;

namespace XFGloss.Droid.Renderers
{
	public class XFGlossSwitchCompatRenderer : Xamarin.Forms.Platform.Android.AppCompat.SwitchRenderer
	{
		SwitchGloss _properties;

		/// <summary>
		/// <see cref="T:Xamarin.Forms.Platform.Android.AppCompat.SwitchRenderer"/> override that is called whenever the associated
		/// <see cref="T:Xamarin.Forms.Switch"/> instance changes
		/// </summary>
		/// <param name="e"><see cref="T:Xamarin.Forms.Platform.Android.ElementChangedEventArgs"/> that specifies the
		/// previously and newly assigned <see cref="T:Xamarin.Forms.Switch"/> instances
		/// </param>
		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Switch> e)
		{
			base.OnElementChanged(e);

			_properties = (e.NewElement != null) ? new SwitchGloss(e.NewElement) : null;

			Control.UpdateColorProperty(_properties, null);
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (_properties != null)
			{
				if (e.PropertyName == SwitchGloss.TintColorProperty.PropertyName ||
					e.PropertyName == SwitchGloss.OnTintColorProperty.PropertyName ||
					e.PropertyName == SwitchGloss.ThumbTintColorProperty.PropertyName ||
					e.PropertyName == SwitchGloss.ThumbOnTintColorProperty.PropertyName)
				{
					Control.UpdateColorProperty(_properties, e.PropertyName);
				}
			}

			base.OnElementPropertyChanged(sender, e);
		}
	}
}