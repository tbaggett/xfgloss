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
using CoreGraphics;
using Foundation;
using Xamarin.Forms.Platform.iOS;

namespace XFGloss.iOS.Extensions
{
	public static class XFGlossGradientExtensions
	{
		// Helper function that converts a list of Xamarin.Forms.Color instances to iOS CGColor instances
		static public CGColor[] ToCGColors(this GlossGradient self)
		{
			List<CGColor> result = new List<CGColor>();

			foreach (GlossGradientStep step in self.Steps)
			{
				result.Add(step.StepColor.ToCGColor());
			}

			return result.ToArray();
		}

		// Helper function that converts a list of float instances to NSNumbers
		static public NSNumber[] ToNSNumbers(this GlossGradient self)
		{
			List<NSNumber> result = new List<NSNumber>();

			float lastStep = float.MinValue;
			foreach (GlossGradientStep step in self.Steps)
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

