using System;
using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Utils;
using XFGloss.Models;
using XFGloss.Views;

namespace XFGloss.Droid.Extensions
{
	public static class XFGlossSwitchExtensions
	{
		public static void UpdateColorProperty(this Android.Widget.Switch control, IXFGlossSwitchProperties properties, string propertyName)
		{
			var defaultColor = Android.Graphics.Color.Transparent;

			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.TintColor ||
				propertyName == XFGlossPropertyNames.OnTintColor)
			{
				int[][] states = new int[2][];
				int[] colors = new int[2];

				var tintColor = properties.TintColor;
				var onTintColor = properties.OnTintColor;

				if (tintColor != Color.Default || onTintColor != Color.Default)
				{
					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = (tintColor != Color.Default) ?
								tintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlNormal(control.Context, defaultColor));

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = (onTintColor != Color.Default) ?
								onTintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlActivated(control.Context, defaultColor));

					var colorList = new ColorStateList(states, colors);
					control.TrackTintList = colorList;
				}
			}

			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.ThumbTintColor ||
				propertyName == XFGlossPropertyNames.ThumbOnTintColor)
			{
				int[][] states = new int[2][];
				int[] colors = new int[2];

				var thumbTintColor = properties.ThumbTintColor;
				var thumbOnTintColor = properties.ThumbOnTintColor;

				if (thumbTintColor != Color.Default || thumbOnTintColor != Color.Default)
				{
					states[0] = new int[] { -Android.Resource.Attribute.StateChecked };
					colors[0] = (thumbTintColor != Color.Default) ?
								thumbTintColor.ToAndroid() :
								// Have to hard code default thumb color...
								// Xamarin.Android doesn't have the needed ColorSwitchThumbNormal ID defined yet
								new Android.Graphics.Color(250, 250, 250);

					states[1] = new int[] { Android.Resource.Attribute.StateChecked };
					colors[1] = (thumbOnTintColor != Color.Default) ?
								thumbOnTintColor.ToAndroid() :
								new Android.Graphics.Color(ThemeUtil.ColorControlActivated(control.Context, defaultColor));

					var colorList = new ColorStateList(states, colors);
					control.ThumbTintList = colorList;
				}
			}
		}
	}
}