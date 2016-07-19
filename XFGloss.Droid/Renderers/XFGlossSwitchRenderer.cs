using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Extensions;

[assembly: ExportRenderer(typeof(Switch), typeof(XFGloss.Droid.Renderers.XFGlossSwitchRenderer))]
namespace XFGloss.Droid.Renderers
{
	public class XFGlossSwitchRenderer : SwitchRenderer
	{
        SwitchGloss _properties;

		protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
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