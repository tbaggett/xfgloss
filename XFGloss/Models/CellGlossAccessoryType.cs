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

	// DetailDisclosureButton and DetailButton are disabled until we can access the detail button 
	// tapped method in the table view source for both the ListView (currently not possible) and 
	// TableView (currently possible) classes.

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