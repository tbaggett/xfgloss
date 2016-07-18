using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Views;
using XFGloss.Droid.Extensions;
using XFGloss.Models;

namespace XFGloss.Droid.Drawables
{
	internal class XFGlossPaintDrawable : PaintDrawable
	{
		public XFGlossPaintDrawable(Gradient xfgGradient)
		{
			Paint.Dither = true;
			Shape = new RectShape();
			UpdateXFGlossGradient(xfgGradient);
		}

		public void UpdateXFGlossGradient(Gradient xfgGradient)
		{
			var sf = GetShaderFactory();
			sf?.Dispose();

			sf = new XFGlossShaderFactory(xfgGradient);
			SetShaderFactory(sf);

			Paint.Shader?.Dispose();
			Paint.SetShader(sf.Resize(Bounds.Width(), Bounds.Height()));
		}
	}

	internal class XFGlossShaderFactory : ShapeDrawable.ShaderFactory
	{
		WeakReference<Gradient> _xfgGradient;

		public XFGlossShaderFactory(Gradient xfgGradient)
		{
			_xfgGradient = new WeakReference<Gradient>(xfgGradient);
		}

		public override Shader Resize(int width, int height)
		{
			Gradient xfgGradient;
			if (_xfgGradient.TryGetTarget(out xfgGradient))
			{
				return new LinearGradient((float)(width * xfgGradient.StartPoint.X),
										  (float)(height * xfgGradient.StartPoint.Y),
										  (float)(width * xfgGradient.EndPoint.X),
										  (float)(height * xfgGradient.EndPoint.Y),
										  xfgGradient.ToAndroidColorValues(),
										  xfgGradient.ToAndroidPercentages(),
										  Shader.TileMode.Clamp);
			}

			return null;
		}
	}
}