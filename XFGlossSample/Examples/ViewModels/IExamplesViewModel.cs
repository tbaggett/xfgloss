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
using XFGloss;

namespace XFGlossSample.Examples.ViewModels
{
	public interface IExamplesViewModel
	{
		// The name of the property's type
		string PropertyType { get; }
		// The property's default value
		string PropertyDefault { get; }
		// One or more paragraphs of descriptive text
		string[] PropertyDescription { get; }
		// Comma separated list of classes that the property can be applied to
		string TargetClasses { get; }
	}
}