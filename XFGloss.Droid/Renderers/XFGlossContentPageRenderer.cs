using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Drawables;
using XFGloss.Models;

//[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MasterDetailRenderer))]
[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.Droid.Renderers.XFGlossPageRenderer))]

namespace XFGloss.Droid.Renderers
{
	public class XFGlossPageRenderer : PageRenderer
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

			if (e.PropertyName == null || e.PropertyName == XFGlossPropertyNames.BackgroundGradient)
			{
				UpdateBackgroundGradient();
			}
		}

		void UpdateBackgroundGradient()
		{
			Gradient bkgrndGradient = (Gradient)Element.GetValue(Views.Page.BackgroundGradientProperty);
			// Initialize/update the painter and shader as needed if a gradient is assigned
			if (bkgrndGradient != null)
			{
				if (ViewGroup.Background is XFGlossPaintDrawable)
				{
					(ViewGroup.Background as XFGlossPaintDrawable).UpdateXFGlossGradient(bkgrndGradient);
					ViewGroup.Invalidate();
				}
				else
				{
					ViewGroup.Background = new XFGlossPaintDrawable(bkgrndGradient);
				}
			}
			else if (ViewGroup.Background is XFGlossPaintDrawable)
			{
				ViewGroup.Background?.Dispose();
				ViewGroup.Background = null;
			}
		}
	}
}