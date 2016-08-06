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
using Xamarin.Forms.Platform.Android;

namespace XFGloss.Droid.Extensions
{
	/// <summary>
	/// Extension methods for the <see cref="T:XFGloss.Gradient"/> class to convert <see cref="T:Xamarin.Forms.Color"/>
	/// and step percentage values to Android-specific data values.
	/// </summary>
	public static class XFGlossGradientExtensions
	{
		/// <summary>
		/// Converts each of the <see cref="T:XFGloss.Gradient"/> steps' <see cref="T:Xamarin.Forms.Color"/> values to
		/// Android color values
		/// </summary>
		/// <returns>An array of Android color values</returns>
		/// <param name="self">The <see cref="T:XFGloss.Gradient"/> instance to apply the method to</param>
		static public int[] ToAndroidColorValues(this Gradient self)
		{
			List<int> result = new List<int>();

			foreach (GradientStep step in self.Steps)
			{
				result.Add(step.StepColor.ToAndroid());
			}

			return result.ToArray();
		}

		/// <summary>
		/// Converts each of the <see cref="T:XFGloss.Gradient"/> steps' percentage values to Android step values
		/// </summary>
		/// <returns>An array of floats that represents the <see cref="T:XFGloss.Gradient"/> steps' percentages</returns>
		/// <param name="self">The <see cref="T:XFGloss.Gradient"/> instance to apply the method to</param>
		static public float[] ToAndroidPercentages(this Gradient self)
		{
			List<float> result = new List<float>();

			float lastStep = float.MinValue;
			foreach (GradientStep step in self.Steps)
			{
				if (lastStep > step.StepPercentage)
				{
					throw new ArgumentOutOfRangeException(nameof(GradientStep.StepPercentage), "The current StepPercentage " +
														  "value must be greater than zero and the previous " +
														  " StepPercentage value.");
				}

				result.Add((float)step.StepPercentage);
			}

			return result.ToArray();
		}
	}
}