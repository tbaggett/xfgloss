// /*
//  * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
//  * http://github.com/tbaggett
//  * http://twitter.com/tbaggett
//  * http://tommyb.com
//  * http://ansuria.com
//  * 
//  * The MIT License (MIT) see GitHub For more information
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */
//

namespace XFGloss
{
	/// <summary>
	/// <see cref="T:XFGloss.IXFGlossRenderer"/>-based interface that defines methods for updating the
	/// <see cref="T:XFGloss.Gradient"/> class's rotation and steps properties on a platform-specific basis.
	/// </summary>
	public interface IGradientRenderer : IXFGlossRenderer
	{
		/// <summary>
		/// Updates the rotation value, an integer number between 0 and 359. The property name specifies the attached
		/// bindable property that is being updated, such as the BackgroundGradient property.
		/// </summary>
		/// <param name="propertyName">The name of the attached bindable property that is being updated.</param>
		/// <param name="rotation">The rotation value, an integer number between 0 and 359.</param>
		void UpdateRotation(string propertyName, int rotation);

		/// <summary>
		/// Updates the gradient steps. The property name specifies the attached bindable property that is being
		/// updated, such as the BackgroundGradient property.
		/// </summary>
		/// <param name="propertyName">The name of the attached bindable property that is being updated.</param>
		/// <param name="steps">A <see cref="T:XFGloss.GradientStepCollection"/> of <see cref="T:XFGloss.GradientStep"/>
		/// instances that specify the <see cref="T:Xamarin.Forms.Color"/> value and fill percentage (a double value
		/// between 0, which indicates the beginning of the fill, and 1, which indicates the end of the fill).</param>
		void UpdateSteps(string propertyName, GradientStepCollection steps);
	}
}