/*
 * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
 * http://github.com/tbaggett
 * http://twitter.com/tbaggett
 * http://tommyb.com
 * http://ansuria.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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

			UpdateXFGlossGradientSteps(xfgGradient);
		}

		public void UpdateXFGlossGradientSteps(GlossGradient xfgGradient)
		{
			var sf = GetShaderFactory();
			sf?.Dispose();

			sf = new XFGlossShaderFactory(xfgGradient);
			SetShaderFactory(sf);

			Paint.Shader?.Dispose();
			Paint.SetShader(sf.Resize(Bounds.Width(), Bounds.Height()));
		}

		public void UpdateXFGlossGradientRotation(int angle)
		{
			// An XFGlossShaderFactory instance should be assigned if we've been properly initialized.
			// Don't make any changes if this sanity check fails.
			if (GetShaderFactory() is XFGlossShaderFactory == false)
			{
				return;
			}

			XFGlossShaderFactory.UpdateXFGlossGradientAngle(Paint.Shader, Bounds.Width(), Bounds.Height(), angle);
		}
	}

	internal class XFGlossShaderFactory : ShapeDrawable.ShaderFactory
	{
		int angle;
		int[] androidColorValues;
		float[] androidPercentages;

		public XFGlossShaderFactory(GlossGradient xfgGradient)
		{
			angle = xfgGradient.Angle;
			androidColorValues = xfgGradient.ToAndroidColorValues();
			androidPercentages = xfgGradient.ToAndroidPercentages();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				androidColorValues = null;
				androidPercentages = null;
			}

			base.Dispose(disposing);
		}

		public override Shader Resize(int width, int height)
		{
			var result = new LinearGradient(0, Math.Max(width, height), 0, 0,
			                                androidColorValues,
			                                androidPercentages,
			                                Shader.TileMode.Clamp);

			UpdateXFGlossGradientAngle(result, width, height, angle);

			return result;
		}

		internal static void UpdateXFGlossGradientAngle(Shader shader, float width, float height, int angle)
		{
			// No point in setting up the matrix if we're dealing with an invalid shader or empty rect
			if (shader != null && height > 0 && width > 0)
			{
				Matrix matrix = null;
				if (!shader.GetLocalMatrix(matrix))
				{
					matrix = new Matrix();
				}
				else
				{
					matrix.Reset();
				}

				var maxDim = Math.Max(width, height);
				float halfMaxDim = (float)maxDim / 2;
				matrix.SetRotate(angle, halfMaxDim, halfMaxDim);
				matrix.PostScale(Math.Min(width / height, 1), Math.Min(height / width, 1));
				shader.SetLocalMatrix(matrix);
			}
		}
	}
}