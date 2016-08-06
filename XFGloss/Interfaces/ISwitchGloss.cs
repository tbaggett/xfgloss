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
using Xamarin.Forms;

namespace XFGloss
{
	/// <summary>
	/// An interface that defines instance property getters/setters that must be implemented by any implementors of the
	/// interface. The interface is implemented by both the <see cref="T:XFGloss.SwitchGloss"/> and
	/// <see cref="T:XFGloss.SwitchCellGloss"/> classes.
	/// </summary>
	public interface ISwitchGloss
	{
		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the track tint color when the Switch control 
		/// is in the "off" position for the assigned <see cref="T:Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		Color TintColor { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the track tint color when the Switch control 
		/// is in the "on" position for the assigned <see cref="T:Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		Color OnTintColor { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the thumb tint color when the Switch control 
		/// is in the "off" position for the assigned <see cref="T:Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		Color ThumbTintColor { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Xamarin.Forms.Color"/> value of the thumb tint color when the Switch control 
		/// is in the "on" position for the assigned <see cref="T:Xamarin.Forms.BindableObject"/> instance.
		/// </summary>
		/// <value>The color of the track tinting.</value>
		Color ThumbOnTintColor { get; set; }
	}
}