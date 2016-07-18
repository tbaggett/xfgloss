using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Extensions;
using XFGloss.Models;

[assembly: ExportRenderer(typeof(Switch), typeof(XFGloss.Droid.Renderers.XFGlossSwitchRenderer))]
namespace XFGloss.Droid.Renderers
{
	public class XFGlossSwitchRenderer : SwitchRenderer
	{
        Views.Switch _properties;

		protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
		{
			base.OnElementChanged(e);

			_properties = (e.NewElement != null) ? new Views.Switch(e.NewElement) : null;

			Control.UpdateColorProperty(_properties, null);
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (_properties != null)
			{
				if (e.PropertyName == XFGlossPropertyNames.TintColor ||
					e.PropertyName == XFGlossPropertyNames.OnTintColor ||
					e.PropertyName == XFGlossPropertyNames.ThumbTintColor ||
					e.PropertyName == XFGlossPropertyNames.ThumbOnTintColor)
				{
					Control.UpdateColorProperty(_properties, e.PropertyName);
				}
				// Special handling of state change to make XF Switch and Switch property names consistent
				else if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
				{
                    Control.UpdateColorProperty(_properties, XFGlossPropertyNames.ValueChanged);
				}
			}

			base.OnElementPropertyChanged(sender, e);
		}
	}
}