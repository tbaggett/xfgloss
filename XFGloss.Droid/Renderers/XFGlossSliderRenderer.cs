using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Utils;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Slider), typeof(XFGloss.Droid.Renderers.XFGlossSliderRenderer))]
namespace XFGloss.Droid.Renderers
{
	public class XFGlossSliderRenderer : SliderRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
			base.OnElementChanged(e);

			UpdateSliderProperties();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.MinTrackTintColorProperty.PropertyName ||
				e.PropertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				UpdateSliderProperties(e.PropertyName);
			}
		}

		void UpdateSliderProperties(string propertyName = null)
		{
			if (Element == null || Control == null)
			{
				return;
			}

			var defaultColor = AColor.Transparent;

			// MaxTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MaxTrackTintColorProperty.PropertyName)
			{
				var maxTrackTintColor = (Color)Element.GetValue(SliderGloss.MaxTrackTintColorProperty);

				Control.ProgressBackgroundTintList = 
					ColorStateList.ValueOf((maxTrackTintColor == Color.Default) ? 
					                       ThemeUtil.IntToColor(ThemeUtil.ColorControlNormal(Control.Context, defaultColor)) : 
				    						maxTrackTintColor.ToAndroid());
			}

			// MinTrackTintColor Property
			if (propertyName == null || propertyName == SliderGloss.MinTrackTintColorProperty.PropertyName)
			{
				var minTrackTintColor = (Color)Element.GetValue(SliderGloss.MinTrackTintColorProperty);

				Control.ProgressTintList = 
					ColorStateList.ValueOf((minTrackTintColor == Color.Default) ?
					                       ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
										   minTrackTintColor.ToAndroid());
			}

			// ThumbTintColor Property
			if (propertyName == null || propertyName == SliderGloss.ThumbTintColorProperty.PropertyName)
			{
				var thumbTintColor = (Color)Element.GetValue(SliderGloss.ThumbTintColorProperty);

				Control.ThumbTintList = 
					ColorStateList.ValueOf((thumbTintColor == Color.Default) ?
										   ThemeUtil.IntToColor(ThemeUtil.ColorAccent(Control.Context, defaultColor)) :
					                       thumbTintColor.ToAndroid());
			}
		}
	}
}