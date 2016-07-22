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
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Drawables;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.Droid.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.Droid.Renderers
{
	public class XFGlossContentPageRenderer : PageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				UpdateBackgroundGradient();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == null || e.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				UpdateBackgroundGradient();
			}
		}

		void UpdateBackgroundGradient()
		{
			GlossGradient bkgrndGradient = (GlossGradient)Element.GetValue(ContentPageGloss.BackgroundGradientProperty);
			// Initialize/update the painter and shader as needed if a gradient is assigned
			if (bkgrndGradient != null)
			{
				SetBackgroundColor(Color.Transparent.ToAndroid());

				if (Background is XFGlossPaintDrawable)
				{
					(Background as XFGlossPaintDrawable).UpdateXFGlossGradient(bkgrndGradient);
					Invalidate();
				}
				else
				{
					Background = new XFGlossPaintDrawable(bkgrndGradient);
				}
			}
			else if (Background is XFGlossPaintDrawable)
			{
				Background.Dispose();
				Background = null;
			}
		}
	}
}