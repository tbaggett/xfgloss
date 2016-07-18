using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Extensions;
using XFGloss.Models;

[assembly: ExportRenderer(typeof(Switch), typeof(XFGloss.iOS.Renderers.XFGlossSwitchRenderer))]
namespace XFGloss.iOS.Renderers
{
	public class XFGlossSwitchRenderer : SwitchRenderer
	{
		XFGloss.Views.Switch _properties;

		protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
		{
			base.OnElementChanged(e);

			_properties = (e.NewElement != null) ? new XFGloss.Views.Switch(e.NewElement) : null;

			if (Control != null && _properties != null)
			{
				Control.UpdateColorProperty(_properties, null);
			}
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