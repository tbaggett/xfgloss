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

namespace XFGloss
{
	// Supported on iOS only currently

	// 
	// 
	// 

	/// <summary>
	/// An enumeration that specifies the accessory view type for a <see cref="T:Xamarin.Forms.Cell"/> on the iOS
	/// platform. The enumeration values are based on the available accessory view types on the iOS platform's
	/// <see cref="T:UIKit.UITableViewCell"/> class, however the DetailDisclosureButton and DetailButton are disabled 
	/// until we can access the detail button tapped method in the table view source for both the 
	/// <see cref="T:Xamarin.Forms.ListView"/> (currently not possible) and <see cref="T:Xamarin.Forms.TableView"/>
	/// (currently possible) classes. This enumeration is not used on the Android platform.
	/// </summary>
	public enum CellGlossAccessoryType
	{
		None,
		DisclosureIndicator,
		//DetailDisclosureButton,
		Checkmark,
		//DetailButton,
		EditIndicator
	}
}