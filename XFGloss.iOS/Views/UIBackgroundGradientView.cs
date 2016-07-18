using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using XFGloss.iOS.Extensions;
using XFGloss.Models;

namespace XFGloss.iOS.Views
{
	// Helper class used to resize background gradient layer when the cell size changes
	internal class UIBackgroundGradientView : UIView
	{
		public UIBackgroundGradientView(CGRect rect, Gradient gradientSource) : base(rect)
		{
			XFGlossGradientLayer.UpdateGradientLayer(this, gradientSource);
		}

		// Update our gradient layer's size to match our view's bounds whenever the bounds are changing
		public override void LayoutSubviews()
		{
			XFGlossGradientLayer.UpdateGradientLayer(this);

			base.LayoutSubviews();
		}
	}

	internal class XFGlossGradientLayer : CAGradientLayer
	{
		WeakReference<Gradient> _gradientSource;

		public XFGlossGradientLayer(Gradient gradient)
		{
			_gradientSource = new WeakReference<Gradient>(gradient);
			UpdateGradientLayerParams(gradient);
		}

		public Gradient GradientSource
		{
			get
			{
				Gradient result;
				if (_gradientSource != null && _gradientSource.TryGetTarget(out result))
				{
					return result;
				}

				return null;
			}
			set
			{
				Gradient existing;
				if (_gradientSource != null)
				{
					// Skip updating gradient if new one's values match the current one
					bool skipUpdate = false;
					if (_gradientSource.TryGetTarget(out existing) && existing == value)
					{
						skipUpdate = true;
					}

					_gradientSource.SetTarget(value);

					if (skipUpdate)
					{
						return;
					}
				}
				else
				{
					_gradientSource = new WeakReference<Gradient>(value);
				}

				UpdateGradientLayerParams(value);
			}
		}

		static public void UpdateGradientLayer(UIView view, Gradient gradientSource = null)
		{
			XFGlossGradientLayer gradientLayer = GetGradientLayer(view);
			if (gradientSource != null)
			{
				// Verify the gradient's angle has been set - it won't have been if initialized from Xaml and only
				// the start and end color values were specified
				if (gradientSource.Angle == Gradient.UNDEFINED_ANGLE)
				{
					gradientSource.Angle = Gradient.DEFAULT_ANGLE;
				}

				// Create or update the layer as needed if a new gradient was passed to us
				bool insertLayer = false;
				if (gradientLayer == null)
				{
					gradientLayer = new XFGlossGradientLayer(gradientSource);
					insertLayer = true;
				}
				else
				{
					gradientLayer.GradientSource = gradientSource;
				}

				// Clear any previously-assigned background color
				view.BackgroundColor = UIColor.Clear;

				// Insert the layer if we created a new one
				if (insertLayer)
				{
					view.Layer.InsertSublayer(gradientLayer, 0);
				}
			}

			// We should update the gradient layer's frame if one was created or already existed
			if (gradientLayer != null)
			{
				gradientLayer.Frame = view.Bounds;
			}
		}

		static public void RemoveGradientLayer(UIView view)
		{
			var layer = GetGradientLayer(view);
			if (layer != null)
			{
				layer.RemoveFromSuperLayer();
				layer.Dispose();
			}
		}

		static XFGlossGradientLayer GetGradientLayer(UIView view)
		{
			if (view.Layer.Sublayers != null && view.Layer.Sublayers.Length > 0 && view.Layer.Sublayers[0] is XFGlossGradientLayer)
			{
				return view.Layer.Sublayers[0] as XFGlossGradientLayer;
			}

			return null;
		}

		void UpdateGradientLayerParams(Gradient gradient)
		{
			Colors = gradient.ToCGColors();
			Locations = gradient.ToNSNumbers();
			StartPoint = new CGPoint(gradient.StartPoint.X, gradient.StartPoint.Y);
			EndPoint = new CGPoint(gradient.EndPoint.X, gradient.EndPoint.Y);
		}
	}
}