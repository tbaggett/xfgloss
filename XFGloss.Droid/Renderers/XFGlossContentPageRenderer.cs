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

using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Drawables;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.Droid.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.Droid.Renderers
{
	public class XFGlossContentPageRenderer : PageRenderer, IGradientRenderer
	{
		#region IGradientRenderer implementation

		public void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement
		{
			if (element is Gradient)
			{
				// No need to check property name yet, BackgroundGradient is the only one being handled here.
				CreateBackgroundGradientDrawable(element as Gradient);
			}
		}

		public bool IsUpdating(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			return GetBackgroundGradientDrawable() != null;
		}

		public void RemoveNativeElement(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			RemoveBackgroundGradientDrawable();
		}

		public void UpdateRotation(string propertyName, int rotation)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			GetBackgroundGradientDrawable()?.UpdateRotation(rotation);
			Invalidate();
		}

		public void UpdateSteps(string propertyName, GradientStepCollection steps)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			GetBackgroundGradientDrawable()?.UpdateSteps(steps);
			Invalidate();
		}

		XFGlossPaintDrawable CreateBackgroundGradientDrawable(Gradient gradient)
		{
			RemoveBackgroundGradientDrawable();

			Background = new XFGlossPaintDrawable(gradient);

			return Background as XFGlossPaintDrawable;
		}

		XFGlossPaintDrawable GetBackgroundGradientDrawable()
		{
			if (Background is XFGlossPaintDrawable)
			{
				return Background as XFGlossPaintDrawable;
			}

			return null;
		}

		void RemoveBackgroundGradientDrawable()
		{
			Background?.Dispose();
			Background = null;
		}

		#endregion

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.DetachRenderer(this);
				}
			}

			base.Dispose(disposing);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanging -= OnElementPropertyChanging;

				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(e.OldElement);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.DetachRenderer(this);
				}
			}

			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanging += OnElementPropertyChanging;

				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(e.NewElement);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName,
														   this);
				}
			}
		}

		void OnElementPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
		{
			if (e.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.DetachRenderer(this);
				}
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName, this);
				}
			}
		}
	}
}