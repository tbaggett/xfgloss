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
using Android.Content;
using Android.OS;

namespace XFGloss.Droid
{
	public class Library
	{
		public static void Init(Context context, Bundle bundle)
		{
			// We don't currently need access to the context or bundle, but we probably will in the future. Requiring
			// them to be passed in now so there won't be a breaking change required when they're needed.
		}
	}
}