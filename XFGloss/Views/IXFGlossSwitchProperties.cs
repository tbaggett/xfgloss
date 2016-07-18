using System;
using Xamarin.Forms;

namespace XFGloss.Views
{
	public interface IXFGlossSwitchProperties
	{
		Color TintColor { get; set; }
		Color OnTintColor { get; set; }
		Color ThumbTintColor { get; set; }
		Color ThumbOnTintColor { get; set; }
	}
}