using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Views;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.iOS.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.iOS.Renderers
{
	public class XFGlossContentPageRenderer : PageRenderer
	{
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			XFGlossGradientLayer.UpdateGradientLayer(NativeView);
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			}

			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += OnElementPropertyChanged;
				UpdateBackgroundGradient();
			}
		}

		void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == null || e.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
			{
				UpdateBackgroundGradient();
			}
		}

		void UpdateBackgroundGradient()
		{
			var gradientSource = (GlossGradient)Element.GetValue(ContentPageGloss.BackgroundGradientProperty);
			if (gradientSource == null)
			{
				XFGlossGradientLayer.RemoveGradientLayer(NativeView);
			}
			else
			{
				XFGlossGradientLayer.UpdateGradientLayer(NativeView, gradientSource);
			}
		}
	}
}

