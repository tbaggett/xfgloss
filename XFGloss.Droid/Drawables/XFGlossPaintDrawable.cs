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
using System.Collections.ObjectModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using XFGloss.Droid.Extensions;

namespace XFGloss.Droid.Drawables
{
	internal class XFGlossPaintDrawable : PaintDrawable
	{
		readonly Gradient _xfgGradient;
		Matrix _shaderMatrix;

		public XFGlossPaintDrawable(Gradient xfgGradient)
		{
			_xfgGradient = xfgGradient;
			_shaderMatrix = new Matrix();

			Paint.Dither = true;
			Shape = new RectShape();

			UpdateSteps(xfgGradient.Steps);
		}

		public void UpdateSteps(GradientStepCollection steps)
		{
			if (_xfgGradient == null)
			{
				return;
			}

			var sf = GetShaderFactory();
			sf?.Dispose();

			_xfgGradient.Steps = steps;

			sf = new XFGlossShaderFactory(_xfgGradient, _shaderMatrix);
			SetShaderFactory(sf);

			Paint.Shader?.Dispose();
			Paint.SetShader(sf.Resize(Bounds.Width(), Bounds.Height()));
		}

		public void UpdateRotation(int rotation)
		{
			if (_xfgGradient != null)
			{
				_xfgGradient.Rotation = rotation;
			}

			// An XFGlossShaderFactory instance should be assigned if we've been properly initialized.
			// Don't make any changes if this sanity check fails.
			var sf = GetShaderFactory();
			if (sf is XFGlossShaderFactory == false)
			{
				return;
			}

			(sf as XFGlossShaderFactory).UpdateRotation(Paint.Shader, Bounds.Width(), Bounds.Height(), rotation);
			InvalidateSelf();
		}
	}

	class XFGlossShaderFactory : ShapeDrawable.ShaderFactory
	{
		Matrix _shaderMatrix;
		int rotation;
		int[] androidColorValues;
		float[] androidPercentages;

		public XFGlossShaderFactory(Gradient xfgGradient, Matrix shaderMatrix)
		{
			rotation = xfgGradient.Rotation;
			androidColorValues = xfgGradient.ToAndroidColorValues();
			androidPercentages = xfgGradient.ToAndroidPercentages();

			_shaderMatrix = shaderMatrix;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				androidColorValues = null;
				androidPercentages = null;
				_shaderMatrix = null;
			}

			base.Dispose(disposing);
		}

		public override Shader Resize(int width, int height)
		{
			var result = new LinearGradient(0, Math.Max(width, height), 0, 0,
			                                androidColorValues,
			                                androidPercentages,
			                                Shader.TileMode.Clamp);

			UpdateRotation(result, width, height, rotation);

			return result;
		}

		public void UpdateRotation(Shader shader, float width, float height, int rotation)
		{
			// No point in setting up the matrix if we're dealing with an invalid shader or empty rect
			if (shader != null && height > 0 && width > 0)
			{
				var maxDim = Math.Max(width, height);
				float halfMaxDim = maxDim / 2;

				_shaderMatrix.SetRotate(rotation, halfMaxDim, halfMaxDim);
				_shaderMatrix.PostScale(Math.Min(width / height, 1), Math.Min(height / width, 1));

				shader.SetLocalMatrix(_shaderMatrix);
			}
		}
	}
}