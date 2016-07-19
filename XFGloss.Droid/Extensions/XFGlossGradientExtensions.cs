using System;
using System.Collections.Generic;
using Xamarin.Forms.Platform.Android;

namespace XFGloss.Droid.Extensions
{
	public static class XFGlossGradientExtensions
	{
		static public int[] ToAndroidColorValues(this GlossGradient self)
		{
			List<int> result = new List<int>();

			foreach (GlossGradientStep step in self.Steps)
			{
				result.Add(step.StepColor.ToAndroid());
			}

			return result.ToArray();
		}

		static public float[] ToAndroidPercentages(this GlossGradient self)
		{
			List<float> result = new List<float>();

			float lastStep = float.MinValue;
			foreach (GlossGradientStep step in self.Steps)
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