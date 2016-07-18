using System;
using System.Collections.Generic;
using XFGloss.Models;
using Xamarin.Forms.Platform.Android;

namespace XFGloss.Droid.Extensions
{
	public static class XFGlossGradientExtensions
	{
		static public int[] ToAndroidColorValues(this Gradient self)
		{
			List<int> result = new List<int>();

			foreach (GradientStep step in self.Steps)
			{
				result.Add(step.StepColor.ToAndroid());
			}

			return result.ToArray();
		}

		static public float[] ToAndroidPercentages(this Gradient self)
		{
			List<float> result = new List<float>();

			float lastStep = float.MinValue;
			foreach (GradientStep step in self.Steps)
			{
				if (lastStep > step.StepPercentage)
				{
					throw new ArgumentOutOfRangeException("GradientStep.StepPercentage", "The current StepPercentage " +
														  "value must be greater than zero and the previous " +
														  " StepPercentage value.");
				}

				result.Add((float)step.StepPercentage);
			}

			return result.ToArray();
		}
	}
}