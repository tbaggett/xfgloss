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
using System.Collections.ObjectModel;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Views;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.iOS.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.iOS.Renderers
{
	public class XFGlossContentPageRenderer : PageRenderer, IGradientRenderer
	{
		#region IGradientRenderer implementation

		public void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement
		{
			if (element is Gradient)
			{
				// No need to check property name yet, BackgroundGradient is the only one being handled here.
				XFGlossGradientLayer.CreateGradientLayer(NativeView, element as Gradient);
			}
		}

		public bool CanUpdate(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			return XFGlossGradientLayer.GetGradientLayer(NativeView) != null;
		}

		public void RemoveNativeElement(string gradientPropertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			XFGlossGradientLayer.RemoveGradientLayer(NativeView);
		}

		public void UpdateRotation(string gradientPropertyName, int rotation)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			XFGlossGradientLayer.GetGradientLayer(NativeView)?.UpdateRotation(rotation);
		}

		public void UpdateSteps(string gradientPropertyName, GradientStepCollection steps)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			XFGlossGradientLayer.GetGradientLayer(NativeView)?.UpdateSteps(steps);
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

		public override void ViewDidLayoutSubviews()
		{
			var layer = XFGlossGradientLayer.GetGradientLayer(NativeView);
			if (layer != null)
			{
				layer.Frame = new CGRect(CGPoint.Empty, NativeView.Frame.Size);
			}

			base.ViewDidLayoutSubviews();
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanging -= OnElementPropertyChanging;
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;

				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(e.OldElement);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.DetachRenderer(this);
				}
			}

			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanging += OnElementPropertyChanging;
				e.NewElement.PropertyChanged += OnElementPropertyChanged;

				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName,
														   this);
				}
			}
		}

		void OnElementPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs args)
		{
			if (args.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.DetachRenderer(this);
				}
			}
		}

		void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
				if (bkgrndGradient != null)
				{
					bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName, 
					                                       this);
				}
			}
		}
	}
}