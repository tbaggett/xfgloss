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
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace XFGloss.iOS.Views
{
	// UIBackgroundGradientView is a helper class used to resize background gradient layer when the cell size changes.
	// This class is NOT used in the ContentPage renderer. The XFGlossGradientLayer is directly attached to the page's
	// main view in that case.
	class UIBackgroundGradientView : UIView
	{
		public UIBackgroundGradientView(CGRect rect, Gradient gradientSource) : base(rect)
		{
			XFGlossGradientLayer.CreateGradientLayer(this, gradientSource);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				XFGlossGradientLayer.RemoveGradientLayer(this);
			}

			base.Dispose(disposing);
		}

		// Update our gradient layer's size to match our view's bounds whenever the bounds are changing
		public override void LayoutSubviews()
		{
			var layer = XFGlossGradientLayer.GetGradientLayer(this);
			if (layer != null)
			{
				layer.Frame = new CGRect(CGPoint.Empty, Frame.Size);
			}

			base.LayoutSubviews();
		}

		public void UpdateRotation(int rotation)
		{
			var layer = XFGlossGradientLayer.GetGradientLayer(this);
			layer?.UpdateRotation(rotation);
		}

		public void UpdateSteps(GradientStepCollection steps)
		{
			var layer = XFGlossGradientLayer.GetGradientLayer(this);
			layer?.UpdateSteps(steps);
		}
	}

	class XFGlossGradientLayer : CAGradientLayer
	{
		#region Static methods

		// Creates a new gradient layer and inserts it as a subview if a gradient layer doesn't already exist.
		// If one does exist, the existing one is updated.
		static public void CreateGradientLayer(UIView view, Gradient gradient)
		{
			// Clear any previously-assigned background color
			view.BackgroundColor = UIColor.Clear;

			XFGlossGradientLayer gradientLayer = GetGradientLayer(view);
			// Create or update the layer as needed if a new gradient was passed to us
			bool insertLayer = false;
			if (gradientLayer == null)
			{
				gradientLayer = new XFGlossGradientLayer(view, gradient);
				insertLayer = true;
			}
			else
			{
				gradientLayer.GradientSource = gradient;
			}

			// Insert the layer if we created a new one
			if (insertLayer)
			{
				view.Layer.InsertSublayer(gradientLayer, 0);
			}

			//// Update the gradient layer's frame
			//gradientLayer.Frame = view.Bounds;
		}

		static public void RemoveGradientLayer(UIView view)
		{
			var layer = GetGradientLayer(view);
			if (layer != null)
			{
				layer.RemoveFromSuperLayer();
				layer.Dispose();
				layer = null;
			}
		}

		static public XFGlossGradientLayer GetGradientLayer(UIView view)
		{
			if (view.Layer.Sublayers != null && view.Layer.Sublayers.Length > 0 && view.Layer.Sublayers[0] is XFGlossGradientLayer)
			{
				return view.Layer.Sublayers[0] as XFGlossGradientLayer;
			}

			return null;
		}

		// Helper function that converts a list of Xamarin.Forms.Color instances to iOS CGColor instances
		static CGColor[] ToCGColors(GradientStepCollection steps)
		{
			List<CGColor> result = new List<CGColor>();

			foreach (GradientStep step in steps)
			{
				result.Add(step.StepColor.ToCGColor());
			}

			return result.ToArray();
		}

		// Helper function that converts a list of float instances to NSNumbers
		static public NSNumber[] ToNSNumbers(GradientStepCollection steps)
		{
			List<NSNumber> result = new List<NSNumber>();

			float lastStep = float.MinValue;
			foreach (GradientStep step in steps)
			{
				if (lastStep > step.StepPercentage)
				{
					throw new ArgumentOutOfRangeException(nameof(GradientStep.StepPercentage), "The current StepPercentage " +
														  "value must be greater than zero and the previous " +
														  " StepPercentage value.");
				}

				result.Add(new NSNumber(step.StepPercentage));
			}

			return result.ToArray();
		}

		#endregion

		#region Instance properties/methods

		Gradient _gradientSource;
		WeakReference<UIView> _gradientView;

		public XFGlossGradientLayer(UIView gradientView, Gradient gradient)
		{
			_gradientSource = new Gradient();
			_gradientView = new WeakReference<UIView>(gradientView);

			UpdateGradientLayerParams(gradient);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_gradientSource = null;
				_gradientView = null;
			}

			base.Dispose(disposing);
		}

		public Gradient GradientSource
		{
			get
			{
				return _gradientSource;
			}
			set
			{
				// Check for changes
				if ((_gradientSource == null && value == null) || (bool)_gradientSource?.Equals(value))
				{
					// no changes detected, so nothing to do
					return;
				}

				UpdateGradientLayerParams(value);
			}
		}

		void UpdateGradientLayerParams(Gradient gradient)
		{
			// If a layer currently exists but the gradient is being set to null...
			if (gradient == null)
			{
				UIView view = null;
				if ((bool)_gradientView?.TryGetTarget(out view) && view != null)
				{
					RemoveGradientLayer(view);
				}
				return;
			}

			UpdateRotation(gradient.Rotation);
			UpdateSteps(gradient.Steps);
		}

		public void UpdateRotation(int rotation)
		{
			var gradient = GradientSource ?? new Gradient();

			if (rotation >= 0 && rotation < 360 && rotation != gradient.Rotation)
			{
				gradient.Rotation = rotation;
				var points = new RotationToPositionsConverter(rotation);
				StartPoint = new CGPoint(points.StartPoint.X, points.StartPoint.Y);
				EndPoint = new CGPoint(points.EndPoint.X, points.EndPoint.Y);

				if (GradientSource == null)
				{
					GradientSource = gradient;
				}
				else
				{
					GradientSource.Rotation = gradient.Rotation;
				}
			}
		}

		public void UpdateSteps(GradientStepCollection steps)
		{
			var gradient = GradientSource ?? new Gradient();

			if (gradient != null && !gradient.Steps.Equals(steps))
			{
				gradient.Steps = new GradientStepCollection(steps);
				Colors = ToCGColors(steps);
				Locations = ToNSNumbers(steps);

				if (GradientSource == null)
				{
					GradientSource = gradient;
				}
				else
				{
					GradientSource.Steps = gradient.Steps;
				}
			}
		}

		#endregion
	}
}