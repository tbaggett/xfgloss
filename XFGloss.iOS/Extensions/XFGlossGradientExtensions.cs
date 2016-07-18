using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Xamarin.Forms.Platform.iOS;
using XFGloss.Models;

namespace XFGloss.iOS.Extensions
{
	public static class XFGlossGradientExtensions
	{
		// Helper function that converts a list of Xamarin.Forms.Color instances to iOS CGColor instances
		static public CGColor[] ToCGColors(this Gradient self)
		{
			List<CGColor> result = new List<CGColor>();

			foreach (GradientStep step in self.Steps)
			{
				result.Add(step.StepColor.ToCGColor());
			}

			return result.ToArray();
		}

		// Helper function that converts a list of float instances to NSNumbers
		static public NSNumber[] ToNSNumbers(this Gradient self)
		{
			List<NSNumber> result = new List<NSNumber>();

			float lastStep = float.MinValue;
			foreach (GradientStep step in self.Steps)
			{
				if (lastStep > step.StepPercentage)
				{
					throw new ArgumentOutOfRangeException("GradientStep.StepPercentage", "The current StepPercentage " +
														  "value must be greater than zero and the previous " +
														  " StepPercentage value.");
				}

				result.Add(new NSNumber(step.StepPercentage));
			}

			return result.ToArray();
		}
	}
}

