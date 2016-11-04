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
using XFGloss.iOS.Renderers;

namespace XFGloss.iOS
{
	/// <summary>
	/// Library class used to provide static initializer to be called from XFGloss iOS client projects to insure
	/// the XFGloss library is included in the client project's build.
	/// </summary>
	public class Library
	{
		/// <summary>
		/// Initializer to be called from XFGloss iOS client project to insure the XFGloss library is inclued in the
		/// client project's build.
		/// </summary>
		public static void Init()
		{
			// Have to instantiate one of the renderers to stop the Xamarin linker removing them
			// from release builds
#pragma warning disable CS0219 // Variable is assigned but its value is never used
			XFGlossContentPageRenderer r = new XFGlossContentPageRenderer();
#pragma warning restore CS0219 // Variable is assigned but its value is never used
		}
	}
}