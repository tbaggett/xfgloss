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
	// Methods to be implemented in the platform-specific XFGlossRenderers. These methods are called by any 
	// XFGlossElement instance that the renderer registers with.

	public interface IXFGlossRenderer
	{
		bool IsUpdating(string propertyName);
		void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement;
		void RemoveNativeElement(string propertyName);
	}
}