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
	/// <summary>
	/// A custom <see cref="T:Android.Graphics.Drawables.ShapeDrawable.PaintDrawable"/> implementation to handle initial 
	/// setup of the paint drawable, storage of needed properties and convenient access to updating the rotation and
	/// steps properties of the gradient fill.
	/// </summary>
	internal class XFGlossPaintDrawable : PaintDrawable
	{
		readonly Gradient _xfgGradient;
		Matrix _shaderMatrix;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Droid.Drawables.XFGlossPaintDrawable"/> class.
		/// </summary>
		/// <param name="xfgGradient">The <see cref="T:XFGloss.Gradient"/> instance whose properties should be 
		/// implemented by this renderer.</param>
		public XFGlossPaintDrawable(Gradient xfgGradient)
		{
			_xfgGradient = xfgGradient;
			_shaderMatrix = new Matrix();

			Paint.Dither = true;
			Shape = new RectShape();

			UpdateSteps(xfgGradient.Steps);
		}

		/// <summary>
		/// Provides public access to update the <see cref="T:XFGloss.GradientStepCollection"/> of
		/// <see cref="T:XFGloss.GradientStep"/> instances for an existing gradient fill.
		/// </summary>
		/// <param name="steps">The new gradient steps to be applied to the existing gradient fill</param>
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

		/// <summary>
		/// Provides public access to update the rotation angle for an existing gradient fill.
		/// </summary>
		/// <param name="rotation">The rotation angle, an integer number between 0 and 359</param>
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
			// Force screen updating, it won't occur otherwise
			InvalidateSelf();
		}
	}

	/// <summary>
	/// A custom <see cref="T:Android.Graphics.Drawables.ShapeDrawable.ShaderFactory"/> implementation to handle 
	/// updating the linear gradient's properties whenever the associated view is resized.
	/// </summary>
	class XFGlossShaderFactory : ShapeDrawable.ShaderFactory
	{
		Matrix _shaderMatrix;
		int rotation;
		int[] androidColorValues;
		float[] androidPercentages;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Droid.Drawables.XFGlossShaderFactory"/> class.
		/// </summary>
		/// <param name="xfgGradient">The <see cref="T:XFGloss.Gradient"/> instance being applied</param>
		/// <param name="shaderMatrix">The shared/common <see cref="T:Android.Graphics.Matrix"/> being applied</param>
		public XFGlossShaderFactory(Gradient xfgGradient, Matrix shaderMatrix)
		{
			rotation = xfgGradient.Rotation;
			androidColorValues = xfgGradient.ToAndroidColorValues();
			androidPercentages = xfgGradient.ToAndroidPercentages();

			_shaderMatrix = shaderMatrix;
		}

		/// <summary>
		/// Should be called when this instance is no longer needed so it can be prepared for garbage collection.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c>, references to other properties should be cleared</param>
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

		/// <summary>
		/// Called by the shader factory base class whenever the associated <see cref="T:Android.Views.View"/> is
		/// resized. Returns a new <see cref="T:Android.Graphics.Shader"/> instance that takes the view's new dimensions
		/// into account.
		/// </summary>
		/// <param name="width">The view's new width</param>
		/// <param name="height">The view's new height</param>
		public override Shader Resize(int width, int height)
		{
			// We avoid having to instantiate a new shader and shader factory every time the rotation angle is 
			// changed by creating the linear gradient at 0 degrees, then rotating the local matrix.
			var result = new LinearGradient(0, Math.Max(width, height), 0, 0,
			                                androidColorValues,
			                                androidPercentages,
			                                Shader.TileMode.Clamp);

			UpdateRotation(result, width, height, rotation);

			return result;
		}

		/// <summary>
		/// Updates the gradient shader's rotation angle without requiring a new shader to be instantiated. 
		/// </summary>
		/// <param name="shader">The existing LinearGradient shader</param>
		/// <param name="width">The associated view's current width</param>
		/// <param name="height">The associated view's current height</param>
		/// <param name="rotation">The new rotation angle to be applied</param>
		public void UpdateRotation(Shader shader, float width, float height, int rotation)
		{
			// No point in setting up the matrix if we're dealing with an invalid shader or empty rect
			if (shader != null && height > 0 && width > 0)
			{
				var maxDim = Math.Max(width, height);
				float halfMaxDim = maxDim / 2;

				// We avoid having to instantiate a new shader and shader factory every time the rotation angle is 
				// changed by creating the linear gradient at 0 degrees, then rotating the local matrix.
				_shaderMatrix.SetRotate(rotation, halfMaxDim, halfMaxDim);
				// Post-scaling is required to make the gradient fill's appearance on Android be consistent with the 
				// default gradient fill style on the iOS platform.
				_shaderMatrix.PostScale(Math.Min(width / height, 1), Math.Min(height / width, 1));

				shader.SetLocalMatrix(_shaderMatrix);
			}
		}
	}
}