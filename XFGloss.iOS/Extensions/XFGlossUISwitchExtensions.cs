using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.Models;
using XFGloss.Views;

namespace XFGloss.iOS.Extensions
{
	public static class XFGlossSwitchExtensions
	{
		public static void UpdateColorProperty(this UISwitch control, IXFGlossSwitchProperties properties, string propertyName)
		{
			// TintColor property
			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.TintColor)
			{
				var tintColor = properties.TintColor;
				if (tintColor != Color.Default)
				{
					control.TintColor = tintColor.ToUIColor();
				}
			}

			// OnTintColor property
			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.OnTintColor)
			{
				var onTintColor = properties.OnTintColor;
				if (onTintColor != Color.Default)
				{
					control.OnTintColor = onTintColor.ToUIColor();
				}
			}

			// We always have to assign the thumb color since we are faking separate on and off thumb color states on iOS
			// Recycled cells won't be updated if we don't handle this.
			Color thumbTintColor = (control.On) ? properties.ThumbOnTintColor : properties.ThumbTintColor;
			UIColor uiThumbTintColor = (thumbTintColor == Color.Default) ? null : thumbTintColor.ToUIColor();

			// Special handling of switch value changing - delay applying property changes until
			// switch state change animation has completed
			if (propertyName == XFGlossPropertyNames.ValueChanged)
			{
				// Create a weak reference to the UISwitch control to avoid capturing a strong reference in the Task lambda
				WeakReference<UISwitch> controlRef = new WeakReference<UISwitch>(control);

				Task.Delay(100).ContinueWith(t =>
				{
					UISwitch switchCtrl;
					if (controlRef.TryGetTarget(out switchCtrl))
					{
						switchCtrl.ThumbTintColor = uiThumbTintColor;
					}
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}
			else
			{
				// If the value isn't being changed, set the thumb tint color immediately
				control.ThumbTintColor = uiThumbTintColor;
			}
		}
	}
}