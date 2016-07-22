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