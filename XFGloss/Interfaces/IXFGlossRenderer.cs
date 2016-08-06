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
	// 
	// 

	/// <summary>
	/// Methods to be implemented in the platform-specific XFGlossRenderers. These methods are called by any 
	/// XFGlossElement instance that a XFGlossRenderer registers with.
	/// </summary>
	public interface IXFGlossRenderer
	{
		/// <summary>
		/// Indicates if an existing element's properties can be updated or a new instance must be created
		/// </summary>
		/// <returns><c>true</c>, if updating is possible, <c>false</c> otherwise.</returns>
		/// <param name="propertyName">The attached BindableObject property name that a platform-specific implementation
		/// is being created or updated for.</param>
		bool CanUpdate(string propertyName);

		/// <summary>
		/// Requests a platform-specific implementation of the specified attached BindableObject property name be created.
		/// </summary>
		/// <param name="propertyName">The attached BindableObject property name that a platform-specific implementation
		/// is being created for.</param>
		/// <param name="element">The <see cref="T:XFGloss.XFGlossElement"/>-based class instance that the 
		/// platform-specific implementation is needed for.</param>
		/// <typeparam name="TElement">The class type of the <see cref="T:XFGloss.XFGlossElement"/>-based class.
		/// </typeparam>
		void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement;

		/// <summary>
		/// Requests the platform-specific implementation for the specified attached BindableObject property name be
		/// removed.
		/// </summary>
		/// <param name="propertyName">The attached BindableObject property name that a platform-specific implementation
		/// is being removed for.</param>
		void RemoveNativeElement(string propertyName);
	}
}