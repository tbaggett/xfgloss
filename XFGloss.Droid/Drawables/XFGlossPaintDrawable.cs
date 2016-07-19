using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using XFGloss.Droid.Extensions;

namespace XFGloss.Droid.Drawables
{
	internal class XFGlossPaintDrawable : PaintDrawable
	{
		public XFGlossPaintDrawable(GlossGradient xfgGradient)
		{
			Paint.Dither = true;
			Shape = new RectShape();
			UpdateXFGlossGradient(xfgGradient);
		}

		public void UpdateXFGlossGradient(GlossGradient xfgGradient)
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
		WeakReference<GlossGradient> _xfgGradient;

		public XFGlossShaderFactory(GlossGradient xfgGradient)
		{
			_xfgGradient = new WeakReference<GlossGradient>(xfgGradient);
		}

		public override Shader Resize(int width, int height)
		{
			GlossGradient xfgGradient;
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