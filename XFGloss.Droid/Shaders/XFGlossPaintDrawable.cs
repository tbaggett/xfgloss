using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Views;
using XFGloss.Droid.Extensions;
using XFGloss.Models;

namespace XFGloss.Droid.Shaders
{
	internal class XFGlossPaintDrawable : PaintDrawable
	{
		public XFGlossPaintDrawable(XFGlossGradient xfgGradient)
		{
			Shape = new RectShape();
			UpdateXFGlossGradient(xfgGradient);
		}

		public void UpdateXFGlossGradient(XFGlossGradient xfgGradient)
		{
			SetShaderFactory(new XFGlossShaderFactory(xfgGradient));
		}
	}

	internal class XFGlossShaderFactory : ShapeDrawable.ShaderFactory
	{
		XFGlossGradient _xfgGradient;

		public XFGlossShaderFactory(XFGlossGradient xfgGradient)
		{
			_xfgGradient = xfgGradient;
		}

		public override Shader Resize(int width, int height)
		{
			return new XFGlossLinearGradient(width, height, _xfgGradient);
		}
	}

	internal class XFGlossLinearGradient : LinearGradient
	{
		public XFGlossLinearGradient(int width, int height, XFGlossGradient xfgGradient)
			: base((float)(width * xfgGradient.StartPoint.X),
				   (float)(height * xfgGradient.StartPoint.Y),
				   (float)(width * xfgGradient.EndPoint.X),
				   (float)(height * xfgGradient.EndPoint.Y),
				   xfgGradient.ToAndroidColorValues(),
				   xfgGradient.ToAndroidPercentages(),
				   TileMode.Mirror)
		{
		}

		protected XFGlossLinearGradient(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public XFGlossLinearGradient(float x0, float y0, float x1, float y1, Color color0, Color color1, Shader.TileMode tile)
			: base(x0, y0, x1, y1, color0, color1, tile)
		{
		}

		public XFGlossLinearGradient(float x0, float y0, float x1, float y1, int[] colors, float[] positions, Shader.TileMode tile)
			: base(x0, y0, x1, y1, colors, positions, tile)
		{
		}
	}
}