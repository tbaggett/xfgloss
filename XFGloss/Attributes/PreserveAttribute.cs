// /*
//  * Copyright (C) 2016-2017 Ansuria Solutions LLC & Tommy Baggett: 
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
	/// Preserve attribute used to instruct Xamarin linkers to not exclude classes/methods
	/// </summary>
	public sealed class PreserveAttribute : System.Attribute
	{
		/// <summary>
		/// Specifies that all members should be preserved when attribute is applied to a class
		/// </summary>
		public bool AllMembers;
		/// <summary>
		/// Specifies that class/method that attribute is applied to should only be preserved if a containing class is preserved.
		/// </summary>
		public bool Conditional;
	}
}