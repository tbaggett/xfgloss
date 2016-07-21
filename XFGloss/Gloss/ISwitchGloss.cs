using System;
using Xamarin.Forms;

namespace XFGloss
{
	public interface ISwitchGloss
	{
		Color TintColor { get; set; }
		Color OnTintColor { get; set; }
		Color ThumbTintColor { get; set; }
		Color ThumbOnTintColor { get; set; }
	}
}